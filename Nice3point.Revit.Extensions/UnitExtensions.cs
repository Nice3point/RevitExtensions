using Autodesk.Revit.DB;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Unit Extensions
/// </summary>
public static class UnitExtensions
{
    /// <summary>
    ///     Converts millimeters to internal Revit format
    /// </summary>
    /// <returns>Value in feet</returns>
    [Pure]
    public static double FromMillimeters(this double millimeters)
    {
#if R19 || R20
        return UnitUtils.ConvertToInternalUnits(millimeters, DisplayUnitType.DUT_MILLIMETERS);
#else
        return UnitUtils.ConvertToInternalUnits(millimeters, UnitTypeId.Millimeters);
#endif
    }

    /// <summary>
    ///     Converts a Revit internal format value to millimeters
    /// </summary>
    /// <returns>Value in millimeters</returns>
    [Pure]
    public static double ToMillimeters(this double feet)
    {
#if R19 || R20
        return UnitUtils.ConvertFromInternalUnits(feet, DisplayUnitType.DUT_MILLIMETERS);
#else
        return UnitUtils.ConvertFromInternalUnits(feet, UnitTypeId.Millimeters);
#endif
    }

    /// <summary>
    ///     Converts meters to internal Revit format
    /// </summary>
    /// <returns>Value in feet</returns>
    [Pure]
    public static double FromMeters(this double meters)
    {
#if R19 || R20
        return UnitUtils.ConvertToInternalUnits(meters, DisplayUnitType.DUT_METERS);
#else
        return UnitUtils.ConvertToInternalUnits(meters, UnitTypeId.Meters);
#endif
    }

    /// <summary>
    ///     Converts a Revit internal format value to meters
    /// </summary>
    /// <returns>Value in meters</returns>
    [Pure]
    public static double ToMeters(this double feet)
    {
#if R19 || R20
        return UnitUtils.ConvertFromInternalUnits(feet, DisplayUnitType.DUT_METERS);
#else
        return UnitUtils.ConvertFromInternalUnits(feet, UnitTypeId.Meters);
#endif
    }

    /// <summary>
    ///      Converts inches to internal Revit format
    /// </summary>
    /// <returns>Value in feet</returns>
    [Pure]
    public static double FromInches(this double inches)
    {
#if R19 || R20
        return UnitUtils.ConvertToInternalUnits(inches, DisplayUnitType.DUT_DECIMAL_INCHES);
#else
        return UnitUtils.ConvertToInternalUnits(inches, UnitTypeId.Inches);
#endif
    }

    /// <summary>
    ///     Converts a Revit internal format value to inches
    /// </summary>
    /// <returns>Value in inches</returns>
    [Pure]
    public static double ToInches(this double feet)
    {
#if R19 || R20
        return UnitUtils.ConvertFromInternalUnits(feet, DisplayUnitType.DUT_DECIMAL_INCHES);
#else
        return UnitUtils.ConvertFromInternalUnits(feet, UnitTypeId.Inches);
#endif
    }

    /// <summary>
    ///      Converts degrees to internal Revit format
    /// </summary>
    /// <returns>Value in radians</returns>
    [Pure]
    public static double FromDegrees(this double degrees)
    {
#if R19 || R20
        return UnitUtils.ConvertToInternalUnits(degrees, DisplayUnitType.DUT_DECIMAL_DEGREES);
#else
        return UnitUtils.ConvertToInternalUnits(degrees, UnitTypeId.Degrees);
#endif
    }

    /// <summary>
    ///     Converts a Revit internal format value to degrees
    /// </summary>
    /// <returns>Value in radians</returns>
    [Pure]
    public static double ToDegrees(this double radians)
    {
#if R19 || R20
        return UnitUtils.ConvertFromInternalUnits(radians, DisplayUnitType.DUT_DECIMAL_DEGREES);
#else
        return UnitUtils.ConvertFromInternalUnits(radians, UnitTypeId.Degrees);
#endif
    }
}