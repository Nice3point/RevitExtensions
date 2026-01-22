using Autodesk.Revit.ApplicationServices;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.LabelUtils"/> class.
/// </summary>
[PublicAPI]
public static class LabelUtilsExtensions
{
#if REVIT2021_OR_GREATER
    /// <param name="typeId">Unique identifier</param>
    extension(ForgeTypeId typeId)
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
            return LabelUtils.GetLabelForSpec(typeId);
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
            return LabelUtils.GetLabelForSymbol(typeId);
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
            return LabelUtils.GetLabelForUnit(typeId);
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
            return LabelUtils.GetLabelForDiscipline(typeId);
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
            if (typeId.Empty()) return string.Empty;

            if (ParameterUtils.IsBuiltInParameter(typeId)) return LabelUtils.GetLabelForBuiltInParameter(typeId);
            if (ParameterUtils.IsBuiltInGroup(typeId)) return LabelUtils.GetLabelForGroup(typeId);
            if (UnitUtils.IsUnit(typeId)) return LabelUtils.GetLabelForUnit(typeId);
            if (UnitUtils.IsSymbol(typeId)) return LabelUtils.GetLabelForSymbol(typeId);
            if (SpecUtils.IsSpec(typeId)) return LabelUtils.GetLabelForSpec(typeId);
            return LabelUtils.GetLabelForDiscipline(typeId);
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
            return LabelUtils.GetLabelForGroup(typeId);
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
            return LabelUtils.GetLabelForBuiltInParameter(typeId);
        }
#endif
    }
#endif

    /// <param name="parameter">The builtin parameter</param>
    extension(BuiltInParameter parameter)
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
            return LabelUtils.GetLabelFor(parameter);
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
            return LabelUtils.GetLabelFor(parameter, language);
        }
    }

    /// <param name="category">The builtin category</param>
    extension(BuiltInCategory category)
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
            return LabelUtils.GetLabelFor(category);
        }
    }

#if !REVIT2025_OR_GREATER
    /// <param name="parameterGroup">The builtin parameter group</param>
    extension(BuiltInParameterGroup parameterGroup)
    {
        /// <summary>
        ///     Gets the user-visible name for a BuiltInParameterGroup
        /// </summary>
        /// <remarks>The name is obtained in the current Revit language</remarks>
        [Pure]
#if REVIT2024
        [Obsolete("This method is deprecated in Revit 2024 and may be removed in a future version of Revit. Please use the `GetLabelForGroup(typeId)` method instead.")]
#endif
        public string ToLabel()
        {
            return LabelUtils.GetLabelFor(parameterGroup);
        }
    }

#endif
#if !REVIT2022_OR_GREATER
    /// <param name="displayUnitType">The display unit type</param>
    extension(DisplayUnitType displayUnitType)
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
            return LabelUtils.GetLabelFor(displayUnitType);
        }
    }

#endif
#if !REVIT2023_OR_GREATER
    /// <param name="parameterType">The parameter type</param>
    extension(ParameterType parameterType)
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
            return LabelUtils.GetLabelFor(parameterType);
        }
    }
#endif
}