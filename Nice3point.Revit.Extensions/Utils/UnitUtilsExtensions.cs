// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.UnitUtils"/> class.
/// </summary>
public static class UnitUtilsExtensions
{
    /// <summary>
    ///     Converts the specified unit to internal Revit format
    /// </summary>
    /// <returns>The converted value</returns>
    [Pure]
#if REVIT2021_OR_GREATER
    public static double FromUnit(this double value, ForgeTypeId unitId)
    {
        return UnitUtils.ConvertToInternalUnits(value, unitId);
    }
#else
    public static double FromUnit(this double value, DisplayUnitType unitId)
    {
        return UnitUtils.ConvertToInternalUnits(value, unitId);
    }
#endif

    /// <summary>
    ///     Converts a Revit internal format value to the specified unit
    /// </summary>
    /// <returns>The converted value</returns>
    [Pure]
#if REVIT2021_OR_GREATER
    public static double ToUnit(this double value, ForgeTypeId unitId)
    {
        return UnitUtils.ConvertFromInternalUnits(value, unitId);
    }
#else
    public static double ToUnit(this double value, DisplayUnitType unitId)
    {
        return UnitUtils.ConvertFromInternalUnits(value, unitId);
    }
#endif

    /// <summary>
    ///     Converts millimeters to internal Revit format
    /// </summary>
    /// <returns>Value in feet</returns>
    [Pure]
    public static double FromMillimeters(this double millimeters)
    {
#if REVIT2021_OR_GREATER
        return UnitUtils.ConvertToInternalUnits(millimeters, UnitTypeId.Millimeters);
#else
        return UnitUtils.ConvertToInternalUnits(millimeters, DisplayUnitType.DUT_MILLIMETERS);
#endif
    }

    /// <summary>
    ///     Converts a Revit internal format value to millimeters
    /// </summary>
    /// <returns>Value in millimeters</returns>
    [Pure]
    public static double ToMillimeters(this double feet)
    {
#if REVIT2021_OR_GREATER
        return UnitUtils.ConvertFromInternalUnits(feet, UnitTypeId.Millimeters);
#else
        return UnitUtils.ConvertFromInternalUnits(feet, DisplayUnitType.DUT_MILLIMETERS);
#endif
    }

    /// <summary>
    ///     Converts meters to internal Revit format
    /// </summary>
    /// <returns>Value in feet</returns>
    [Pure]
    public static double FromMeters(this double meters)
    {
#if REVIT2021_OR_GREATER
        return UnitUtils.ConvertToInternalUnits(meters, UnitTypeId.Meters);
#else
        return UnitUtils.ConvertToInternalUnits(meters, DisplayUnitType.DUT_METERS);
#endif
    }

    /// <summary>
    ///     Converts a Revit internal format value to meters
    /// </summary>
    /// <returns>Value in meters</returns>
    [Pure]
    public static double ToMeters(this double feet)
    {
#if REVIT2021_OR_GREATER
        return UnitUtils.ConvertFromInternalUnits(feet, UnitTypeId.Meters);
#else
        return UnitUtils.ConvertFromInternalUnits(feet, DisplayUnitType.DUT_METERS);
#endif
    }

    /// <summary>
    ///     Converts inches to internal Revit format
    /// </summary>
    /// <returns>Value in feet</returns>
    [Pure]
    public static double FromInches(this double inches)
    {
#if REVIT2021_OR_GREATER
        return UnitUtils.ConvertToInternalUnits(inches, UnitTypeId.Inches);
#else
        return UnitUtils.ConvertToInternalUnits(inches, DisplayUnitType.DUT_DECIMAL_INCHES);
#endif
    }

    /// <summary>
    ///     Converts a Revit internal format value to inches
    /// </summary>
    /// <returns>Value in inches</returns>
    [Pure]
    public static double ToInches(this double feet)
    {
#if REVIT2021_OR_GREATER
        return UnitUtils.ConvertFromInternalUnits(feet, UnitTypeId.Inches);
#else
        return UnitUtils.ConvertFromInternalUnits(feet, DisplayUnitType.DUT_DECIMAL_INCHES);
#endif
    }

    /// <summary>
    ///     Converts degrees to internal Revit format
    /// </summary>
    /// <returns>Value in radians</returns>
    [Pure]
    public static double FromDegrees(this double degrees)
    {
#if REVIT2021_OR_GREATER
        return UnitUtils.ConvertToInternalUnits(degrees, UnitTypeId.Degrees);
#else
        return UnitUtils.ConvertToInternalUnits(degrees, DisplayUnitType.DUT_DECIMAL_DEGREES);
#endif
    }

    /// <summary>
    ///     Converts a Revit internal format value to degrees
    /// </summary>
    /// <returns>Value in radians</returns>
    [Pure]
    public static double ToDegrees(this double radians)
    {
#if REVIT2021_OR_GREATER
        return UnitUtils.ConvertFromInternalUnits(radians, UnitTypeId.Degrees);
#else
        return UnitUtils.ConvertFromInternalUnits(radians, DisplayUnitType.DUT_DECIMAL_DEGREES);
#endif
    }
#if REVIT2021_OR_GREATER

    /// <summary>Checks whether a unit is valid for a given measurable spec.</summary>
    /// <param name="specTypeId">Identifier of the measurable spec.</param>
    /// <param name="unitTypeId">Identifier of the unit to check.</param>
    /// <returns>True if the unit is valid, false otherwise.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(ForgeTypeId).
    /// </exception>
    [Pure]
    public static bool IsValidUnit(this ForgeTypeId specTypeId, ForgeTypeId unitTypeId)
    {
        return UnitUtils.IsValidUnit(specTypeId, unitTypeId);
    }

    /// <summary>Checks whether a ForgeTypeId identifies a symbol.</summary>
    /// <remarks>The SymbolTypeId class offers symbol identifiers.</remarks>
    /// <param name="symbolTypeId">The identifier to check.</param>
    /// <returns>True if the ForgeTypeId identifies a symbol, false otherwise.</returns>
    [Pure]
    public static bool IsSymbol(this ForgeTypeId symbolTypeId)
    {
        return UnitUtils.IsSymbol(symbolTypeId);
    }

    /// <summary>Checks whether a ForgeTypeId identifies a unit.</summary>
    /// <remarks>The UnitTypeId class offers unit identifiers.</remarks>
    /// <param name="unitTypeId">The identifier to check.</param>
    /// <returns>True if the ForgeTypeId identifies a unit, false otherwise.</returns>
    [Pure]
    public static bool IsUnit(this ForgeTypeId unitTypeId)
    {
        return UnitUtils.IsUnit(unitTypeId);
    }
    
    /// <summary>Gets the identifiers of all valid units for a given measurable spec.</summary>
    /// <param name="specTypeId">Identifier of the measurable spec.</param>
    /// <returns>Identifiers of the valid units.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(ForgeTypeId).
    /// </exception>
    [Pure]
    public static IList<ForgeTypeId> GetValidUnits(this ForgeTypeId specTypeId)
    {
        return UnitUtils.GetValidUnits(specTypeId);
    }

    /// <summary>Gets the string used in type catalogs to identify a given measurable spec.</summary>
    /// <param name="specTypeId">Identifier of the measurable spec.</param>
    /// <returns>
    ///    The type catalog string, or an empty string if the measurable spec cannot be used in type catalogs.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(ForgeTypeId).
    /// </exception>
    [Pure]
    public static string GetTypeCatalogStringForSpec(this ForgeTypeId specTypeId)
    {
        return UnitUtils.GetTypeCatalogStringForSpec(specTypeId);
    }

    /// <summary>Gets the string used in type catalogs to identify a given unit.</summary>
    /// <param name="unitTypeId">Identifier of the unit.</param>
    /// <returns>
    ///    The type catalog string, or an empty string if the unit cannot be used in type catalogs.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    unitTypeId is not a unit identifier. See UnitUtils.IsUnit(ForgeTypeId) and UnitUtils.GetUnitTypeId(DisplayUnitType).
    /// </exception>
    [Pure]
    public static string GetTypeCatalogStringForUnit(this ForgeTypeId unitTypeId)
    {
        return UnitUtils.GetTypeCatalogStringForUnit(unitTypeId);
    }
#endif
#if REVIT2022_OR_GREATER

    /// <summary>
    ///    Checks whether a ForgeTypeId identifies a spec associated with units of measurement.
    /// </summary>
    /// <param name="specTypeId">The identifier to check.</param>
    /// <returns>True if the ForgeTypeId identifies a measurable spec, false otherwise.</returns>
    [Pure]
    public static bool IsMeasurableSpec(this ForgeTypeId specTypeId)
    {
        return UnitUtils.IsMeasurableSpec(specTypeId);
    }

    /// <summary>Gets the discipline for a given measurable spec.</summary>
    /// <param name="specTypeId">Identifier of the measurable spec.</param>
    /// <returns>Identifier of the discipline.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(ForgeTypeId).
    /// </exception>
    [Pure]
    public static ForgeTypeId GetDiscipline(this ForgeTypeId specTypeId)
    {
        return UnitUtils.GetDiscipline(specTypeId);
    }
#endif
}