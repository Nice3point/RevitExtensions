using Autodesk.Revit.ApplicationServices;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Label Extensions
/// </summary>
[PublicAPI]
public static class LabelExtensions
{
    /// <summary>
    ///     Gets the user-visible name for a BuiltInParameter
    /// </summary>
    /// <param name="source">The BuiltInParameter to get the user-visible name</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     Thrown when the BuiltInParameter cannot be found
    /// </exception>
    /// <remarks>The name is obtained in the current Revit language</remarks>
    [Pure]
    public static string ToLabel(this BuiltInParameter source)
    {
        return LabelUtils.GetLabelFor(source);
    }

    /// <summary>
    ///     Gets the user-visible name for a BuiltInParameter in a specific LanguageType
    /// </summary>
    /// <param name="source">The BuiltInParameter to get the user-visible name</param>
    /// <param name="language">The desired LanguageType to get the user-visible name in</param>
    /// <returns>The BuiltInParameter name in the desired LanguageType</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     Thrown when the BuiltInParameter cannot be found
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     Thrown when the desired LanguageType cannot be found for the BuiltInParameter name
    /// </exception>
    [Pure]
    public static string ToLabel(this BuiltInParameter source, LanguageType language)
    {
        return LabelUtils.GetLabelFor(source, language);
    }

#if !REVIT2025_OR_GREATER
    /// <summary>
    ///     Gets the user-visible name for a BuiltInParameterGroup
    /// </summary>
    /// <param name="source">The BuiltInParameterGroup to get the user-visible name</param>
    /// <remarks>The name is obtained in the current Revit language</remarks>
    [Pure]
#if REVIT2024
    [Obsolete("This method is deprecated in Revit 2024 and may be removed in a future version of Revit. Please use the `GetLabelForGroup(ForgeTypeId)` method instead.")]
#endif
    public static string ToLabel(this BuiltInParameterGroup source)
    {
        return LabelUtils.GetLabelFor(source);
    }
#endif

    /// <summary>
    ///     Gets the user-visible name for a BuiltInCategory
    /// </summary>
    /// <param name="source">The BuiltInCategory to get the user-visible name</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     Thrown when the builtin category is not valid
    /// </exception>
    /// <remarks>The name is obtained in the current Revit language</remarks>
    [Pure]
    public static string ToLabel(this BuiltInCategory source)
    {
        return LabelUtils.GetLabelFor(source);
    }
#if !REVIT2022_OR_GREATER
    /// <summary>
    ///     Gets the user-visible name for a DisplayUnitType
    /// </summary>
    /// <param name="source">The DisplayUnitType to get the user-visible name</param>
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
    public static string ToLabel(this DisplayUnitType source)
    {
        return LabelUtils.GetLabelFor(source);
    }
#endif
#if !REVIT2023_OR_GREATER

    /// <summary>
    ///     Gets the user-visible name for a ParameterType
    /// </summary>
    /// <param name="source">The ParameterType to get the user-visible name</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     Thrown when failed to get user-visible name for the input ParameterType
    /// </exception>
    /// <remarks>The name is obtained in the current Revit language</remarks>
    [Pure]
#if REVIT2022
    [Obsolete("This method is deprecated in Revit 2022")]
#endif
    public static string ToLabel(this ParameterType source)
    {
        return LabelUtils.GetLabelFor(source);
    }
#endif
#if REVIT2022_OR_GREATER

    /// <summary>
    ///     Gets the user-visible name for a ForgeTypeId
    /// </summary>
    /// <param name="source">The ForgeTypeId to get the user-visible name</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     The ForgeTypeId is not valid in the context of the current API version
    /// </exception>
    /// <remarks>The name is obtained in the current Revit language</remarks>
    [Pure]
    public static string ToLabel(this ForgeTypeId source)
    {
        if (ParameterUtils.IsBuiltInParameter(source)) return LabelUtils.GetLabelForBuiltInParameter(source);
        if (ParameterUtils.IsBuiltInGroup(source)) return LabelUtils.GetLabelForGroup(source);
        if (UnitUtils.IsUnit(source)) return LabelUtils.GetLabelForUnit(source);
        if (UnitUtils.IsSymbol(source)) return LabelUtils.GetLabelForSymbol(source);
        if (SpecUtils.IsSpec(source)) return LabelUtils.GetLabelForSpec(source);
        return LabelUtils.GetLabelForDiscipline(source);
    }
#endif
#if REVIT2022_OR_GREATER

    /// <summary>
    ///     Gets the user-visible name for a discipline
    /// </summary>
    /// <param name="source">Identifier of the discipline</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     Discipline must have a definition
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null
    /// </exception>
    /// <remarks>The name is obtained in the current Revit language</remarks>
    [Pure]
    public static string ToDisciplineLabel(this ForgeTypeId source)
    {
        return LabelUtils.GetLabelForDiscipline(source);
    }
#endif
#if REVIT2022_OR_GREATER

    /// <summary>
    ///     Gets the user-visible name for a built-in parameter group
    /// </summary>
    /// <param name="source">The identifier of the parameter group to get the user-visible name</param>
    /// <remarks>
    ///     The name is obtained in the current Revit language
    /// </remarks>
    [Pure]
    public static string ToGroupLabel(this ForgeTypeId source)
    {
        return LabelUtils.GetLabelForGroup(source);
    }
#endif
#if REVIT2021_OR_GREATER

    /// <summary>
    ///     Gets the user-visible name for a spec
    /// </summary>
    /// <param name="source">Identifier of the spec to get the user-visible name</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     The given identifier is neither a spec nor a category
    /// </exception>
    /// <remarks>
    ///     The name is obtained in the current Revit language.
    ///     If the given identifier is a category, this method returns the name of the Family Type spec with that category, e.g. "Family Type: Walls"
    /// </remarks>
    [Pure]
    public static string ToSpecLabel(this ForgeTypeId source)
    {
        return LabelUtils.GetLabelForSpec(source);
    }
#endif
#if REVIT2021_OR_GREATER

    /// <summary>
    ///     Gets the user-visible name for a symbol
    /// </summary>
    /// <param name="source">Identifier of the symbol to get the user-visible name</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     Symbol must have a definition
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null
    /// </exception>
    /// <remarks>The name is obtained in the current Revit language</remarks>
    [Pure]
    public static string ToSymbolLabel(this ForgeTypeId source)
    {
        return LabelUtils.GetLabelForSymbol(source);
    }
#endif
#if REVIT2021_OR_GREATER

    /// <summary>
    ///     Gets the user-visible name for a unit
    /// </summary>
    /// <param name="source">Identifier of the unit to get the user-visible name</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     Cannot find DisplayUnitTypeInfo for the given unit identifier
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
    ///     A non-optional argument was null
    /// </exception>
    /// <remarks>The name is obtained in the current Revit language</remarks>
    [Pure]
    public static string ToUnitLabel(this ForgeTypeId source)
    {
        return LabelUtils.GetLabelForUnit(source);
    }
#endif
#if REVIT2022_OR_GREATER

    /// <summary>
    ///     Gets the user-visible name for a built-in parameter
    /// </summary>
    /// <param name="source">Identifier of the built-in parameter to get the user-visible name</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     Thrown when the built-in parameter cannot be found
    /// </exception>
    /// <remarks>The name is obtained in the current Revit language</remarks>
    [Pure]
    public static string ToParameterLabel(this ForgeTypeId source)
    {
        return LabelUtils.GetLabelForBuiltInParameter(source);
    }
#endif
}