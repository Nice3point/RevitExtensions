using Autodesk.Revit.ApplicationServices;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.LabelUtils"/> class.
/// </summary>
[PublicAPI]
public static class LabelUtilsExtensions
{
#if REVIT2021_OR_GREATER
    /// <param name="source">Unique identifier</param>
    extension(ForgeTypeId source)
    {
        /// <summary>
        ///     Gets the user-visible name for a spec
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     The given identifier is neither a spec nor a category
        /// </exception>
        /// <remarks>
        ///     The name is obtained in the current Revit language.
        ///     If the given identifier is a category, this method returns the name of the Family Type spec with that category, e.g. "Family Type: Walls"
        /// </remarks>
        [Pure]
        public string ToSpecLabel()
        {
            return LabelUtils.GetLabelForSpec(source);
        }

        /// <summary>
        ///     Gets the user-visible name for a symbol
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     Symbol must have a definition
        /// </exception>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
        public string ToSymbolLabel()
        {
            return LabelUtils.GetLabelForSymbol(source);
        }

        /// <summary>
        ///     Gets the user-visible name for a unit
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     Cannot find DisplayUnitTypeInfo for the given unit identifier
        /// </exception>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
        public string ToUnitLabel()
        {
            return LabelUtils.GetLabelForUnit(source);
        }

#if REVIT2022_OR_GREATER
        /// <summary>
        ///     Gets the user-visible name for a discipline
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     Discipline must have a definition
        /// </exception>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
        public string ToDisciplineLabel()
        {
            return LabelUtils.GetLabelForDiscipline(source);
        }

        /// <summary>
        ///     Gets the user-visible name for a ForgeTypeId
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     The ForgeTypeId is not valid in the context of the current API version
        /// </exception>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
        public string ToLabel()
        {
            if (source.Empty()) return string.Empty;

            if (ParameterUtils.IsBuiltInParameter(source)) return LabelUtils.GetLabelForBuiltInParameter(source);
            if (ParameterUtils.IsBuiltInGroup(source)) return LabelUtils.GetLabelForGroup(source);
            if (UnitUtils.IsUnit(source)) return LabelUtils.GetLabelForUnit(source);
            if (UnitUtils.IsSymbol(source)) return LabelUtils.GetLabelForSymbol(source);
            if (SpecUtils.IsSpec(source)) return LabelUtils.GetLabelForSpec(source);
            return LabelUtils.GetLabelForDiscipline(source);
        }

        /// <summary>
        ///     Gets the user-visible name for a built-in parameter group
        /// </summary>
        /// <remarks>
        ///     The name is obtained in the current Revit language
        /// </remarks>
        [Pure]
        public string ToGroupLabel()
        {
            return LabelUtils.GetLabelForGroup(source);
        }

        /// <summary>
        ///     Gets the user-visible name for a built-in parameter
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     Thrown when the built-in parameter cannot be found
        /// </exception>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
        public string ToParameterLabel()
        {
            return LabelUtils.GetLabelForBuiltInParameter(source);
        }
#endif
    }
#endif

    /// <param name="source">The builtin parameter</param>
    extension(BuiltInParameter source)
    {
        /// <summary>
        ///     Gets the user-visible name for a BuiltInParameter
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     Thrown when the BuiltInParameter cannot be found
        /// </exception>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(source);
        }

        /// <summary>
        ///     Gets the user-visible name for a BuiltInParameter in a specific LanguageType
        /// </summary>
        /// <param name="language">The desired LanguageType to get the user-visible name in</param>
        /// <returns>The BuiltInParameter name in the desired LanguageType</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     Thrown when the BuiltInParameter cannot be found
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     Thrown when the desired LanguageType cannot be found for the BuiltInParameter name
        /// </exception>
        [Pure]
        public string ToLabel(LanguageType language)
        {
            return LabelUtils.GetLabelFor(source, language);
        }
    }

    /// <param name="source">The builtin category</param>
    extension(BuiltInCategory source)
    {
        /// <summary>
        ///     Gets the user-visible name for a BuiltInCategory
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     Thrown when the builtin category is not valid
        /// </exception>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(source);
        }
    }

#if !REVIT2025_OR_GREATER
    /// <param name="source">The builtin parameter group</param>
    extension(BuiltInParameterGroup source)
    {
        /// <summary>
        ///     Gets the user-visible name for a BuiltInParameterGroup
        /// </summary>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
#if REVIT2024
        [Obsolete("This method is deprecated in Revit 2024 and may be removed in a future version of Revit. Please use the `GetLabelForGroup(ForgeTypeId)` method instead.")]
#endif
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(source);
        }
    }

#endif
#if !REVIT2022_OR_GREATER
    /// <param name="source">The display unit type</param>
    extension(DisplayUnitType source)
    {
        /// <summary>
        ///     Gets the user-visible name for a DisplayUnitType
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Cannot find DisplayUnitTypeInfo for the DisplayUnitType
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
#if REVIT2021
        [Obsolete("This method is deprecated in Revit 2021")]
#endif
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(source);
        }
    }

#endif
#if !REVIT2023_OR_GREATER
    /// <param name="source">The parameter type</param>
    extension(ParameterType source)
    {
        /// <summary>
        ///     Gets the user-visible name for a ParameterType
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     Thrown when failed to get user-visible name for the input ParameterType
        /// </exception>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
#if REVIT2022
    [Obsolete("This method is deprecated in Revit 2022")]
#endif
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(source);
        }
    }
#endif
}