using Autodesk.Revit.DB;

namespace Nice3point.Revit.Extensions;

public static class CollectorExtensions
{
    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, BuiltInCategory category)
    {
        return CollectInstances(document, category).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, category, filter).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, category, filters).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document)
    {
        return CollectInstances(document).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, ElementFilter filter)
    {
        return CollectInstances(document, filter).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetInstances(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, filters).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, BuiltInCategory category)
    {
        return CollectInstances(document, category);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, category, filter);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, category, filters);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document)
    {
        return CollectInstances(document);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, ElementFilter filter)
    {
        return CollectInstances(document, filter);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateInstances(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, filters);
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, BuiltInCategory category) where T : Element
    {
        var elements = CollectInstances(document, category).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, BuiltInCategory category, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, category, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, category, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document) where T : Element
    {
        var elements = CollectInstances(document).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateInstances<T>(this Document document, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance;
        }
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, BuiltInCategory category)
    {
        return CollectInstances(document, category).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, category, filter).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, category, filters).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document)
    {
        return CollectInstances(document).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, ElementFilter filter)
    {
        return CollectInstances(document, filter).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetInstanceIds(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectInstances(document, filters).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, BuiltInCategory category)
    {
        foreach (var element in CollectInstances(document, category)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        foreach (var element in CollectInstances(document, category, filter)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        foreach (var element in CollectInstances(document, category, filters)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document)
    {
        foreach (var element in CollectInstances(document)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, ElementFilter filter)
    {
        foreach (var element in CollectInstances(document, filter)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds(this Document document, IEnumerable<ElementFilter> filters)
    {
        foreach (var element in CollectInstances(document, filters)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, BuiltInCategory category) where T : Element
    {
        var elements = CollectInstances(document, category).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, BuiltInCategory category, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, category, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, category, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document) where T : Element
    {
        var elements = CollectInstances(document).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, ElementFilter filter) where T : Element
    {
        var elements = CollectInstances(document, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateInstanceIds<T>(this Document document, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectInstances(document, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var instance = (T) element;
            yield return instance.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document, BuiltInCategory category)
    {
        return CollectTypes(document, category).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectTypes(document, category, filter).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, category, filters).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document)
    {
        return CollectTypes(document).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document, ElementFilter filter)
    {
        return CollectTypes(document, filter).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IList<Element> GetTypes(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, filters).ToElements();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document, BuiltInCategory category)
    {
        return CollectTypes(document, category);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectTypes(document, category, filter);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, category, filters);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document)
    {
        return CollectTypes(document);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document, ElementFilter filter)
    {
        return CollectTypes(document, filter);
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    [Pure]
    public static IEnumerable<Element> EnumerateTypes(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, filters);
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document, BuiltInCategory category) where T : Element
    {
        var elements = CollectTypes(document, category).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document, BuiltInCategory category, ElementFilter filter) where T : Element
    {
        var elements = CollectTypes(document, category, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectTypes(document, category, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document) where T : Element
    {
        var elements = CollectTypes(document).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document, ElementFilter filter) where T : Element
    {
        var elements = CollectTypes(document, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of elements</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<T> EnumerateTypes<T>(this Document document, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectTypes(document, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type;
        }
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document, BuiltInCategory category)
    {
        return CollectTypes(document, category).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectTypes(document, category, filter).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, category, filters).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document)
    {
        return CollectTypes(document).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document, ElementFilter filter)
    {
        return CollectTypes(document, filter).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static ICollection<ElementId> GetTypeIds(this Document document, IEnumerable<ElementFilter> filters)
    {
        return CollectTypes(document, filters).ToElementIds();
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document, BuiltInCategory category)
    {
        foreach (var element in CollectTypes(document, category)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document, BuiltInCategory category, ElementFilter filter)
    {
        foreach (var element in CollectTypes(document, category, filter)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        foreach (var element in CollectTypes(document, category, filters)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document)
    {
        foreach (var element in CollectTypes(document)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document, ElementFilter filter)
    {
        foreach (var element in CollectTypes(document, filter)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds(this Document document, IEnumerable<ElementFilter> filters)
    {
        foreach (var element in CollectTypes(document, filters)) yield return element.Id;
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document, BuiltInCategory category) where T : Element
    {
        var elements = CollectTypes(document, category).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document, BuiltInCategory category, ElementFilter filter) where T : Element
    {
        var elements = CollectTypes(document, category, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The category</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectTypes(document, category, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document) where T : Element
    {
        var elements = CollectTypes(document).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filter">Filter that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document, ElementFilter filter) where T : Element
    {
        var elements = CollectTypes(document, filter).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Searches for elements in a document by class of type T
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="filters">Filters that accepts or rejects elements based upon criteria</param>
    /// <returns>The complete set of element ids</returns>
    /// <typeparam name="T">Type inherited from Element</typeparam>
    [Pure]
    public static IEnumerable<ElementId> EnumerateTypeIds<T>(this Document document, IEnumerable<ElementFilter> filters) where T : Element
    {
        var elements = CollectTypes(document, filters).OfClass(typeof(T));
        foreach (var element in elements)
        {
            var type = (T) element;
            yield return type.Id;
        }
    }

    /// <summary>
    ///     Retrieves a category object corresponding to the BuiltInCategory
    /// </summary>
    /// <param name="document">The document</param>
    /// <param name="category">The BuiltInCategory</param>
    [Pure]
    [NotNull]
    public static Category GetCategory(this Document document, BuiltInCategory category)
    {
        return CollectTypes(document, category)
            .FirstElement()
            .Category;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectInstances(Document document)
    {
        return new FilteredElementCollector(document).WhereElementIsNotElementType();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectInstances(Document document, BuiltInCategory category)
    {
        return CollectInstances(document).OfCategory(category);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectInstances(Document document, ElementFilter filter)
    {
        return CollectInstances(document).WherePasses(filter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectInstances(Document document, IEnumerable<ElementFilter> filters)
    {
        var elements = CollectInstances(document);
        ApplyFilters(elements, filters);
        return elements;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectInstances(Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectInstances(document, category).WherePasses(filter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectInstances(Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        var elements = CollectInstances(document, category);
        ApplyFilters(elements, filters);
        return elements;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectTypes(Document document)
    {
        return new FilteredElementCollector(document).WhereElementIsElementType();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectTypes(Document document, BuiltInCategory category)
    {
        return CollectTypes(document).OfCategory(category);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectTypes(Document document, ElementFilter filter)
    {
        return CollectTypes(document).WherePasses(filter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectTypes(Document document, IEnumerable<ElementFilter> filters)
    {
        var elements = CollectTypes(document);
        ApplyFilters(elements, filters);
        return elements;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectTypes(Document document, BuiltInCategory category, ElementFilter filter)
    {
        return CollectTypes(document, category).WherePasses(filter);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static FilteredElementCollector CollectTypes(Document document, BuiltInCategory category, IEnumerable<ElementFilter> filters)
    {
        var elements = CollectTypes(document, category);
        ApplyFilters(elements, filters);
        return elements;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void ApplyFilters(FilteredElementCollector elements, IEnumerable<ElementFilter> filters)
    {
        foreach (var elementFilter in filters) elements.WherePasses(elementFilter);
    }
}