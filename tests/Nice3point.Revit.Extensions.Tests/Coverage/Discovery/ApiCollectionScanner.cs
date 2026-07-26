using System.Collections;
using System.Reflection;
using Nice3point.Revit.Extensions.Tests.Coverage.Models;

namespace Nice3point.Revit.Extensions.Tests.Coverage.Discovery;

/// <summary>
///     Discovers the collections an assembly exposes and the members each one leaves to an extension.
/// </summary>
/// <remarks>
///     The interop layer mirrors the native C++ containers: iteration runs through an iterator factory, an entry key lives
///     on the concrete iterator, and the collection contract stops at the non-generic <see cref="IEnumerable"/>.
/// </remarks>
internal static class ApiCollectionScanner
{
    private const BindingFlags PublicInstance = BindingFlags.Public | BindingFlags.Instance;

    private const string KeyPropertyName = nameof(IDictionaryEnumerator.Key);
    private const string TryGetValueMethodName = nameof(IReadOnlyDictionary<,>.TryGetValue);

    /// <summary>
    ///     The name the CLR gives an indexer.
    /// </summary>
    private const string IndexerPropertyName = "Item";

    /// <summary>
    ///     The iteration entry point of the native C++ API, kept in the managed wrapper next to <c>GetEnumerator</c>.
    /// </summary>
    private const string IteratorFactoryName = "ForwardIterator";

    /// <summary>
    ///     The removal method of the native C++ API, kept in the managed wrapper in place of <c>Remove</c>.
    /// </summary>
    private const string EraseMethodName = "Erase";

    /// <summary>
    ///     The insertion methods of the native C++ API, kept in the managed wrapper in place of <c>Add</c>.
    /// </summary>
    private static readonly string[] InsertionMethodNames = ["Insert", "Append"];

    /// <summary>
    ///     The methods carrying the element type in their signature, in the order the element type is read from them.
    /// </summary>
    private static readonly string[] ElementTypeSourceMethodNames = [..InsertionMethodNames, nameof(IList.Contains), EraseMethodName];

    /// <summary>
    ///     Builds one report row per collection the assembly exposes.
    /// </summary>
    /// <param name="assembly">The assembly to scan.</param>
    /// <param name="sourceFileIndex">The index resolving which library source files extend each collection.</param>
    [Pure]
    public static IReadOnlyList<ApiCollectionRow> ScanCollections(Assembly assembly, SourceFileIndex sourceFileIndex)
    {
        return DiscoverCollections(assembly)
            .Select(shape => CreateCollectionRow(shape, sourceFileIndex))
            .ToList();
    }

    /// <summary>
    ///     Builds one report row per collection whose iterator carries the key of the current entry.
    /// </summary>
    /// <param name="assembly">The assembly to scan.</param>
    /// <param name="sourceFileIndex">The index resolving which library source files extend each map.</param>
    [Pure]
    public static IReadOnlyList<ApiMapRow> ScanMaps(Assembly assembly, SourceFileIndex sourceFileIndex)
    {
        return DiscoverCollections(assembly)
            .Where(shape => shape.Kind is ApiCollectionKind.Map)
            .Select(shape => CreateMapRow(shape, sourceFileIndex))
            .ToList();
    }

    private static IReadOnlyList<ApiCollectionShape> DiscoverCollections(Assembly assembly)
    {
        var shapes = new List<ApiCollectionShape>();

        foreach (var type in GetLoadableTypes(assembly))
        {
            var shape = TryDescribeCollection(type);
            if (shape is null)
            {
                continue;
            }

            shapes.Add(shape);
        }

        return shapes;
    }

    /// <summary>
    ///     Reflects the collection shape of the type, skipping a type whose signatures name a type the process cannot load.
    /// </summary>
    private static ApiCollectionShape? TryDescribeCollection(Type type)
    {
        try
        {
            return DescribeCollection(type);
        }
        catch (Exception exception) when (exception is FileNotFoundException or TypeLoadException)
        {
            return null;
        }
    }

    /// <summary>
    ///     Reflects the collection shape of the type, or returns <c>null</c> for a type that enumerates nothing.
    /// </summary>
    private static ApiCollectionShape? DescribeCollection(Type type)
    {
        if (!type.IsVisible)
        {
            return null;
        }

        if (!type.IsClass)
        {
            return null;
        }

        if (!typeof(IEnumerable).IsAssignableFrom(type))
        {
            return null;
        }

        var iteratorFactory = FindParameterlessMethod(type, IteratorFactoryName);
        var iteratorType = iteratorFactory?.ReturnType ?? FindEnumeratorType(type) ?? typeof(IEnumerator);
        var keyProperty = FindProperty(iteratorType, KeyPropertyName);
        var indexer = FindIndexer(type, typeof(int));

        return new ApiCollectionShape
        {
            CollectionType = type,
            Kind = FindKind(keyProperty, indexer),
            IteratorType = iteratorType,
            HasIteratorFactory = iteratorFactory is not null,
            KeyType = keyProperty?.PropertyType,
            ElementType = keyProperty is null ? FindElementType(type, indexer) : FindValueType(type, keyProperty.PropertyType),
            EnumeratedType = FindEnumeratedType(type)
        };
    }

    private static ApiCollectionKind FindKind(PropertyInfo? keyProperty, PropertyInfo? indexer)
    {
        if (keyProperty is not null)
        {
            return ApiCollectionKind.Map;
        }

        if (indexer is not null)
        {
            return ApiCollectionKind.IndexedSequence;
        }

        return ApiCollectionKind.Sequence;
    }

    private static Type? FindEnumeratorType(Type collectionType)
    {
        return FindParameterlessMethod(collectionType, nameof(IEnumerable.GetEnumerator))?.ReturnType;
    }

    /// <summary>
    ///     Resolves the type a <c>foreach</c> over the collection yields, following the enumerator the pattern binds to.
    /// </summary>
    private static Type FindEnumeratedType(Type collectionType)
    {
        var enumeratorType = FindEnumeratorType(collectionType);
        var currentProperty = enumeratorType is null ? null : FindProperty(enumeratorType, nameof(IEnumerator.Current));
        if (currentProperty is not null)
        {
            return currentProperty.PropertyType;
        }

        return FindEnumerableArgument(collectionType) ?? typeof(object);
    }

    /// <summary>
    ///     Resolves the value type of a map from the indexer taking the key, falling back to the insertion method.
    /// </summary>
    private static Type FindValueType(Type collectionType, Type keyType)
    {
        var indexer = FindIndexer(collectionType, keyType);
        if (indexer is not null)
        {
            return indexer.PropertyType;
        }

        var insertMethod = collectionType
            .GetMethods(PublicInstance)
            .Where(method => InsertionMethodNames.Contains(method.Name))
            .Where(method => method.GetParameters() is [var key, _] && key.ParameterType == keyType)
            .OrderBy(method => method.DeclaringType == collectionType ? 0 : 1)
            .FirstOrDefault();

        if (insertMethod is not null)
        {
            return insertMethod.GetParameters()[1].ParameterType;
        }

        return typeof(object);
    }

    /// <summary>
    ///     Resolves the element type of a sequence from the generic contract, the indexer, or a single-element member.
    /// </summary>
    private static Type FindElementType(Type collectionType, PropertyInfo? indexer)
    {
        var enumerableArgument = FindEnumerableArgument(collectionType);
        if (enumerableArgument is not null)
        {
            return enumerableArgument;
        }

        if (indexer is not null)
        {
            return indexer.PropertyType;
        }

        foreach (var methodName in ElementTypeSourceMethodNames)
        {
            var method = collectionType
                .GetMethods(PublicInstance)
                .Where(candidate => candidate.Name == methodName)
                .OrderBy(candidate => candidate.DeclaringType == collectionType ? 0 : 1)
                .FirstOrDefault(candidate => candidate.GetParameters().Length == 1);

            if (method is not null)
            {
                return method.GetParameters()[0].ParameterType;
            }
        }

        return typeof(object);
    }

    private static IReadOnlyList<string> FindIssues(ApiCollectionShape shape)
    {
        var issues = new List<string>();

        if (FindEnumerableArgument(shape.CollectionType) is null)
        {
            issues.Add(ApiCollectionIssues.NoGenericEnumerable);
        }

        if (shape.EnumeratedType == typeof(object))
        {
            issues.Add(ApiCollectionIssues.UntypedEnumeration);
        }

        if (shape.Kind is ApiCollectionKind.Map && !IsKeyValueShape(shape.EnumeratedType))
        {
            issues.Add(ApiCollectionIssues.KeyOutsideEnumeration);
        }

        if (shape.Kind is ApiCollectionKind.Map && !HasTryGetValue(shape.CollectionType, shape.KeyType!))
        {
            issues.Add(ApiCollectionIssues.NoTryGetValue);
        }

        if (shape.HasIteratorFactory && typeof(IDisposable).IsAssignableFrom(shape.IteratorType))
        {
            issues.Add(ApiCollectionIssues.DisposableIterator);
        }

        return issues;
    }

    private static bool IsKeyValueShape(Type enumeratedType)
    {
        if (enumeratedType == typeof(DictionaryEntry))
        {
            return true;
        }

        if (!enumeratedType.IsGenericType)
        {
            return false;
        }

        return enumeratedType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
    }

    private static Type? FindEnumerableArgument(Type collectionType)
    {
        return collectionType
            .GetInterfaces()
            .Where(candidate => candidate.IsGenericType)
            .Where(candidate => candidate.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            .Select(candidate => candidate.GetGenericArguments()[0])
            .FirstOrDefault();
    }

    private static bool HasTryGetValue(Type collectionType, Type keyType)
    {
        return collectionType
            .GetMethods(PublicInstance)
            .Where(method => method.Name == TryGetValueMethodName)
            .Any(method => method.GetParameters() is [var key, { IsOut: true }] && key.ParameterType == keyType);
    }

    private static PropertyInfo? FindIndexer(Type type, Type indexType)
    {
        return type
            .GetProperties(PublicInstance)
            .Where(property => property.Name == IndexerPropertyName)
            .Where(property => property.GetIndexParameters() is [var index] && index.ParameterType == indexType)
            .OrderBy(property => property.DeclaringType == type ? 0 : 1)
            .FirstOrDefault();
    }

    /// <summary>
    ///     Finds the most derived declaration of a parameterless method.
    /// </summary>
    /// <remarks>
    ///     The interop layer declares the iterator factory on the base collection and narrows the return type on the derived one,
    ///     a pair of overloads <see cref="Type.GetMethod(string, BindingFlags)"/> reports as an ambiguous match.
    /// </remarks>
    private static MethodInfo? FindParameterlessMethod(Type type, string methodName)
    {
        return type
            .GetMethods(PublicInstance)
            .Where(method => method.Name == methodName)
            .Where(method => method.GetParameters().Length == 0)
            .OrderBy(method => method.DeclaringType == type ? 0 : 1)
            .FirstOrDefault();
    }

    /// <summary>
    ///     Finds the most derived declaration of a property taking no index.
    /// </summary>
    private static PropertyInfo? FindProperty(Type type, string propertyName)
    {
        return type
            .GetProperties(PublicInstance)
            .Where(property => property.Name == propertyName)
            .Where(property => property.GetIndexParameters().Length == 0)
            .OrderBy(property => property.DeclaringType == type ? 0 : 1)
            .FirstOrDefault();
    }

    private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            return exception.Types.OfType<Type>();
        }
    }

    private static ApiCollectionRow CreateCollectionRow(ApiCollectionShape shape, SourceFileIndex sourceFileIndex)
    {
        return new ApiCollectionRow
        {
            Kind = shape.Kind,
            TypeName = FormatTypeName(shape.CollectionType),
            ElementType = FormatTypeName(shape.ElementType),
            IteratorType = FormatTypeName(shape.IteratorType),
            EnumeratedType = FormatTypeName(shape.EnumeratedType),
            Issues = FindIssues(shape),
            ImplementationFiles = FindExtendingFiles(shape.CollectionType, sourceFileIndex)
        };
    }

    /// <exception cref="InvalidOperationException">The shape carries no key.</exception>
    private static ApiMapRow CreateMapRow(ApiCollectionShape shape, SourceFileIndex sourceFileIndex)
    {
        var keyType = shape.KeyType ?? throw new InvalidOperationException($"The '{shape.CollectionType.Name}' shape carries no key.");

        return new ApiMapRow
        {
            TypeName = FormatTypeName(shape.CollectionType),
            KeyType = FormatTypeName(keyType),
            ValueType = FormatTypeName(shape.ElementType),
            IteratorType = FormatTypeName(shape.IteratorType),
            EnumeratedType = FormatTypeName(shape.EnumeratedType),
            Issues = FindIssues(shape),
            ImplementationFiles = FindExtendingFiles(shape.CollectionType, sourceFileIndex)
        };
    }

    /// <summary>
    ///     Reads the files extending the collection or any collection it derives from.
    /// </summary>
    /// <remarks>
    ///     An extension declared over a base collection applies to every collection deriving from it. <c>BindingMap</c> reaches
    ///     its members through <c>DefinitionBindingMap</c>.
    ///     The walk stops at the first base type that enumerates nothing. A collection deriving from <see cref="Element"/>
    ///     inherits the members of an element, never the members of a collection.
    /// </remarks>
    private static IReadOnlyList<string> FindExtendingFiles(Type collectionType, SourceFileIndex sourceFileIndex)
    {
        var fileNames = new List<string>();

        for (var type = collectionType; type is not null && typeof(IEnumerable).IsAssignableFrom(type); type = type.BaseType)
        {
            foreach (var fileName in sourceFileIndex.FindExtendingFiles(type.Name))
            {
                if (fileNames.Contains(fileName))
                {
                    continue;
                }

                fileNames.Add(fileName);
            }
        }

        return fileNames;
    }

    /// <summary>
    ///     Renders the short name of a type with its generic arguments.
    /// </summary>
    private static string FormatTypeName(Type type)
    {
        if (!type.IsGenericType)
        {
            return type.Name;
        }

        var definitionIndex = type.Name.IndexOf('`');
        var definitionName = definitionIndex < 0 ? type.Name : type.Name[..definitionIndex];
        var arguments = string.Join(", ", type.GetGenericArguments().Select(FormatTypeName));

        return $"{definitionName}<{arguments}>";
    }
}
