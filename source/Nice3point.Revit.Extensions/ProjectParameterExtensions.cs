using JetBrains.Annotations;


namespace Nice3point.Revit.Extensions;

/// <summary>
///     Specifies which parameter bindings to include when querying project parameters.
/// </summary>
[Flags]
[PublicAPI]
public enum ParameterBindingKind
{
    /// <summary>Instance bindings only.</summary>
    Instance = 1,

    /// <summary>Type bindings only.</summary>
    Type = 2,

    /// <summary>Both instance and type bindings.</summary>
    Any = Instance | Type
}

/// <summary>
///     Revit project parameter Extensions
/// </summary>
[PublicAPI]
public static class ProjectParameterExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <summary>
        ///     Retrieves the names of all project parameters bound in the document.
        /// </summary>
        /// <param name="bindingKind">The binding kinds to include. Defaults to <see cref="ParameterBindingKind.Any"/>.</param>
        /// <returns>A sorted, read-only list of project parameter names.</returns>
        [Pure]
        public IReadOnlyList<string> GetProjectParameterNames(ParameterBindingKind bindingKind = ParameterBindingKind.Any)
        {
            var result = new List<string>();
            var iterator = document.ParameterBindings.ForwardIterator();
            while (iterator.MoveNext())
            {
                if (iterator.Key is not { } definition) continue;
                if (iterator.Current is not ElementBinding binding) continue;
                if (!MatchesBindingKind(binding, bindingKind)) continue;

                result.Add(definition.Name);
            }

            result.Sort();
            return result;
        }

        /// <summary>
        ///     Retrieves the names of project parameters bound to the specified category.
        /// </summary>
        /// <param name="category">The category the parameter must be bound to.</param>
        /// <param name="bindingKind">The binding kinds to include. Defaults to <see cref="ParameterBindingKind.Any"/>.</param>
        /// <returns>A sorted, read-only list of project parameter names.</returns>
        [Pure]
        public IReadOnlyList<string> GetProjectParameterNames(BuiltInCategory category, ParameterBindingKind bindingKind = ParameterBindingKind.Any)
        {
            var result = new List<string>();
            var iterator = document.ParameterBindings.ForwardIterator();
            while (iterator.MoveNext())
            {
                if (iterator.Key is not { } definition) continue;
                if (iterator.Current is not ElementBinding binding) continue;
                if (!MatchesBindingKind(binding, bindingKind)) continue;
                if (!HasCategory(binding, category)) continue;

                result.Add(definition.Name);
            }

            result.Sort();
            return result;
        }

#if REVIT2022_OR_GREATER
        /// <summary>
        ///     Retrieves the names of project parameters of the specified data type.
        /// </summary>
        /// <param name="dataType">The parameter data type identifier to match.</param>
        /// <param name="bindingKind">The binding kinds to include. Defaults to <see cref="ParameterBindingKind.Any"/>.</param>
        /// <returns>A sorted, read-only list of project parameter names.</returns>
        [Pure]
        public IReadOnlyList<string> GetProjectParameterNames(ForgeTypeId dataType, ParameterBindingKind bindingKind = ParameterBindingKind.Any)
        {
            var result = new List<string>();
            var iterator = document.ParameterBindings.ForwardIterator();
            while (iterator.MoveNext())
            {
                if (iterator.Key is not { } definition) continue;
                if (iterator.Current is not ElementBinding binding) continue;
                if (!MatchesBindingKind(binding, bindingKind)) continue;
                if (definition.GetDataType() != dataType) continue;

                result.Add(definition.Name);
            }

            result.Sort();
            return result;
        }
#else
        /// <summary>
        ///     Retrieves the names of project parameters of the specified data type.
        /// </summary>
        /// <param name="dataType">The parameter data type to match.</param>
        /// <param name="bindingKind">The binding kinds to include. Defaults to <see cref="ParameterBindingKind.Any"/>.</param>
        /// <returns>A sorted, read-only list of project parameter names.</returns>
        [Pure]
        public IReadOnlyList<string> GetProjectParameterNames(ParameterType dataType, ParameterBindingKind bindingKind = ParameterBindingKind.Any)
        {
            var result = new List<string>();
            var iterator = document.ParameterBindings.ForwardIterator();
            while (iterator.MoveNext())
            {
                if (iterator.Key is not { } definition) continue;
                if (iterator.Current is not ElementBinding binding) continue;
                if (!MatchesBindingKind(binding, bindingKind)) continue;
                if (definition.ParameterType != dataType) continue;

                result.Add(definition.Name);
            }

            result.Sort();
            return result;
        }
#endif

        /// <summary>
        ///     Finds the definition of a project parameter by name.
        /// </summary>
        /// <param name="parameterName">The parameter name to search for, compared case-insensitively.</param>
        /// <returns>The matching <see cref="Definition"/>, or <see langword="null"/> if none is found.</returns>
        [Pure]
        public Definition? GetProjectParameterDefinition(string parameterName)
        {
            var iterator = document.ParameterBindings.ForwardIterator();
            while (iterator.MoveNext())
            {
                if (iterator.Key is not { } definition) continue;
                if (!definition.Name.Equals(parameterName, StringComparison.OrdinalIgnoreCase)) continue;

                return definition;
            }

            return null;
        }
    }

    private static bool MatchesBindingKind(ElementBinding binding, ParameterBindingKind kind)
    {
        return binding switch
        {
            InstanceBinding => kind.HasFlag(ParameterBindingKind.Instance),
            TypeBinding => kind.HasFlag(ParameterBindingKind.Type),
            _ => false
        };
    }

    private static bool HasCategory(ElementBinding binding, BuiltInCategory target)
    {
        var categories = binding.Categories;
        if (categories is null) return false;

        foreach (Category category in categories)
        {
#if REVIT2023_OR_GREATER
            if (category.BuiltInCategory == target) return true;
#else
            if ((BuiltInCategory)category.Id.IntegerValue == target) return true;
#endif
        }

        return false;
    }
}