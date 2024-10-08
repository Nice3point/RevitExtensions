namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Unit Extensions
/// </summary>
[PublicAPI]
public static class UnitExtensions
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
#if !REVIT2022_OR_GREATER

    /// <summary>Formats a number with units into a string</summary>
    /// <param name="document">Document that stores units</param>
    /// <param name="unitType">The unit type of the value to format</param>
    /// <param name="value">The value to format, in Revit's internal units</param>
    /// <param name="maxAccuracy">
    ///     True if the value should be rounded to an increased accuracy level appropriate for editing or understanding the precise value stored in the model.
    ///     False if the accuracy specified by the FormatOptions should be used, appropriate for printed drawings
    /// </param>
    /// <param name="forEditing">
    ///     True if the formatting should be modified as necessary so that the formatted string can be successfully parsed, for example by suppressing digit grouping.
    ///     False if unmodified settings should be used, suitable for display only
    /// </param>
    /// <returns>The formatted string</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     unitType is an invalid unit type. See UnitUtils.IsValidUnitType() and UnitUtils.GetValidUnitTypes().
    ///     Or the given value for value is not finite
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
    ///     A value passed for an enumeration argument is not a member of that enumeration
    /// </exception>
    [Pure]
#if REVIT2021
    [Obsolete("This method is deprecated in Revit 2021")]
#endif
    public static string FormatUnit(this Document document, UnitType unitType, double value, bool maxAccuracy, bool forEditing)
    {
        return UnitFormatUtils.Format(document.GetUnits(), unitType, value, maxAccuracy, forEditing);
    }

    /// <summary>Formats a number with units into a string</summary>
    /// <param name="document">Document that stores units</param>
    /// <param name="unitType">The unit type of the value to format</param>
    /// <param name="value">The value to format, in Revit's internal units</param>
    /// <param name="maxAccuracy">
    ///     True if the value should be rounded to an increased accuracy level appropriate for editing or understanding the precise value stored in the model.
    ///     False if the accuracy specified by the FormatOptions should be used, appropriate for printed drawings
    /// </param>
    /// <param name="forEditing">
    ///     True if the formatting should be modified as necessary so that the formatted string can be successfully parsed, for example by suppressing digit grouping.
    ///     False if unmodified settings should be used, suitable for display only
    /// </param>
    /// <param name="options">Additional formatting options</param>
    /// <returns>The formatted string</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     unitType is an invalid unit type. See UnitUtils.IsValidUnitType() and UnitUtils.GetValidUnitTypes().
    ///     Or the given value for value is not finite.
    ///     Or the display unit in the FormatOptions in formatValueOptions is not a valid display unit for unitType.
    ///     See UnitUtils.IsValidDisplayUnit(UnitType, DisplayUnitType) and UnitUtils.GetValidDisplayUnits(UnitType)
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
    ///     A value passed for an enumeration argument is not a member of that enumeration
    /// </exception>
    [Pure]
#if REVIT2021
    [Obsolete("This method is deprecated in Revit 2021")]
#endif
    public static string FormatUnit(this Document document, UnitType unitType, double value, bool maxAccuracy, bool forEditing, FormatValueOptions options)
    {
        return UnitFormatUtils.Format(document.GetUnits(), unitType, value, maxAccuracy, forEditing, options);
    }
#endif
#if REVIT2021_OR_GREATER
    /// <summary>Formats a number with units into a string</summary>
    /// <param name="document">Document that stores units</param>
    /// <param name="specTypeId">Identifier of the spec of the value to format</param>
    /// <param name="value">The value to format, in Revit's internal units</param>
    /// <param name="forEditing">
    ///     True if the formatting should be modified as necessary so that the formatted string can be successfully parsed, for example by suppressing digit grouping.
    ///     False if unmodified settings should be used, suitable for display only
    /// </param>
    /// <returns>The formatted string</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(ForgeTypeId).
    ///     Or the given value for value is not finite
    /// </exception>
    [Pure]
    public static string FormatUnit(this Document document, ForgeTypeId specTypeId, double value, bool forEditing)
    {
        return UnitFormatUtils.Format(document.GetUnits(), specTypeId, value, forEditing);
    }

    /// <summary>Formats a number with units into a string</summary>
    /// <param name="document">Document that stores units</param>
    /// <param name="specTypeId">Identifier of the spec of the value to format</param>
    /// <param name="value">The value to format, in Revit's internal units</param>
    /// <param name="forEditing">
    ///     True if the formatting should be modified as necessary so that the formatted string can be successfully parsed, for example by suppressing digit grouping.
    ///     False if unmodified settings should be used, suitable for display only
    /// </param>
    /// <param name="options">Additional formatting options</param>
    /// <returns>The formatted string</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(ForgeTypeId).
    ///     Or the given value for value is not finite.
    ///     Or the unit in the FormatOptions in formatValueOptions is not a valid unit for specTypeId.
    ///     See UnitUtils.IsValidUnit(ForgeTypeId, ForgeTypeId) and UnitUtils.GetValidUnits(ForgeTypeId)
    /// </exception>
    [Pure]
    public static string FormatUnit(this Document document, ForgeTypeId specTypeId, double value, bool forEditing, FormatValueOptions options)
    {
        return UnitFormatUtils.Format(document.GetUnits(), specTypeId, value, forEditing, options);
    }
#endif
}