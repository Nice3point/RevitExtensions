// ReSharper disable once CheckNamespace

using JetBrains.Annotations;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.UnitUtils"/> class.
/// </summary>
[PublicAPI]
public static class UnitUtilsExtensions
{
    /// <param name="value">The numeric value</param>
    extension(double value)
    {
        /// <summary>
        ///     Converts the specified unit to internal Revit format
        /// </summary>
        /// <returns>The converted value</returns>
        [Pure]
#if REVIT2021_OR_GREATER
        public double FromUnit(ForgeTypeId unitId)
        {
            return UnitUtils.ConvertToInternalUnits(value, unitId);
        }
#else
        public double FromUnit(DisplayUnitType unitId)
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
        public double ToUnit(ForgeTypeId unitId)
        {
            return UnitUtils.ConvertFromInternalUnits(value, unitId);
        }
#else
        public double ToUnit(DisplayUnitType unitId)
        {
            return UnitUtils.ConvertFromInternalUnits(value, unitId);
        }
#endif

        /// <summary>
        ///     Converts millimeters to internal Revit format
        /// </summary>
        /// <returns>Value in feet</returns>
        [Pure]
        public double FromMillimeters()
        {
#if REVIT2021_OR_GREATER
            return UnitUtils.ConvertToInternalUnits(value, UnitTypeId.Millimeters);
#else
            return UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_MILLIMETERS);
#endif
        }

        /// <summary>
        ///     Converts a Revit internal format value to millimeters
        /// </summary>
        /// <returns>Value in millimeters</returns>
        [Pure]
        public double ToMillimeters()
        {
#if REVIT2021_OR_GREATER
            return UnitUtils.ConvertFromInternalUnits(value, UnitTypeId.Millimeters);
#else
            return UnitUtils.ConvertFromInternalUnits(value, DisplayUnitType.DUT_MILLIMETERS);
#endif
        }

        /// <summary>
        ///     Converts meters to internal Revit format
        /// </summary>
        /// <returns>Value in feet</returns>
        [Pure]
        public double FromMeters()
        {
#if REVIT2021_OR_GREATER
            return UnitUtils.ConvertToInternalUnits(value, UnitTypeId.Meters);
#else
            return UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_METERS);
#endif
        }

        /// <summary>
        ///     Converts a Revit internal format value to meters
        /// </summary>
        /// <returns>Value in meters</returns>
        [Pure]
        public double ToMeters()
        {
#if REVIT2021_OR_GREATER
            return UnitUtils.ConvertFromInternalUnits(value, UnitTypeId.Meters);
#else
            return UnitUtils.ConvertFromInternalUnits(value, DisplayUnitType.DUT_METERS);
#endif
        }

        /// <summary>
        ///     Converts inches to internal Revit format
        /// </summary>
        /// <returns>Value in feet</returns>
        [Pure]
        public double FromInches()
        {
#if REVIT2021_OR_GREATER
            return UnitUtils.ConvertToInternalUnits(value, UnitTypeId.Inches);
#else
            return UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_DECIMAL_INCHES);
#endif
        }

        /// <summary>
        ///     Converts a Revit internal format value to inches
        /// </summary>
        /// <returns>Value in inches</returns>
        [Pure]
        public double ToInches()
        {
#if REVIT2021_OR_GREATER
            return UnitUtils.ConvertFromInternalUnits(value, UnitTypeId.Inches);
#else
            return UnitUtils.ConvertFromInternalUnits(value, DisplayUnitType.DUT_DECIMAL_INCHES);
#endif
        }

        /// <summary>
        ///     Converts degrees to internal Revit format
        /// </summary>
        /// <returns>Value in radians</returns>
        [Pure]
        public double FromDegrees()
        {
#if REVIT2021_OR_GREATER
            return UnitUtils.ConvertToInternalUnits(value, UnitTypeId.Degrees);
#else
            return UnitUtils.ConvertToInternalUnits(value, DisplayUnitType.DUT_DECIMAL_DEGREES);
#endif
        }

        /// <summary>
        ///     Converts a Revit internal format value to degrees
        /// </summary>
        /// <returns>Value in radians</returns>
        [Pure]
        public double ToDegrees()
        {
#if REVIT2021_OR_GREATER
            return UnitUtils.ConvertFromInternalUnits(value, UnitTypeId.Degrees);
#else
            return UnitUtils.ConvertFromInternalUnits(value, DisplayUnitType.DUT_DECIMAL_DEGREES);
#endif
        }
    }

#if REVIT2021_OR_GREATER
    /// <param name="typeId">Unique identifier</param>
    extension(ForgeTypeId typeId)
    {
        /// <summary>Checks whether a ForgeTypeId identifies a symbol.</summary>
        /// <remarks>The SymbolTypeId class offers symbol identifiers.</remarks>
        /// <returns>True if the ForgeTypeId identifies a symbol, false otherwise.</returns>
        public bool IsSymbol => UnitUtils.IsSymbol(typeId);

        /// <summary>Checks whether a ForgeTypeId identifies a unit.</summary>
        /// <remarks>The UnitTypeId class offers unit identifiers.</remarks>
        /// <returns>True if the ForgeTypeId identifies a unit, false otherwise.</returns>
        public bool IsUnit => UnitUtils.IsUnit(typeId);

        /// <summary>Gets the identifiers of all valid units for a given measurable spec.</summary>
        /// <returns>Identifiers of the valid units.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(typeId).
        /// </exception>
        [Pure]
        public IList<ForgeTypeId> GetValidUnits()
        {
            return UnitUtils.GetValidUnits(typeId);
        }

        /// <summary>Gets the string used in type catalogs to identify a given measurable spec.</summary>
        /// <returns>
        ///    The type catalog string, or an empty string if the measurable spec cannot be used in type catalogs.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(typeId).
        /// </exception>
        [Pure]
        public string GetTypeCatalogStringForSpec()
        {
            return UnitUtils.GetTypeCatalogStringForSpec(typeId);
        }

        /// <summary>Checks whether a unit is valid for a given measurable spec.</summary>
        /// <param name="unitTypeId">Identifier of the unit to check.</param>
        /// <returns>True if the unit is valid, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(typeId).
        /// </exception>
        [Pure]
        public bool IsValidUnit(ForgeTypeId unitTypeId)
        {
            return UnitUtils.IsValidUnit(typeId, unitTypeId);
        }

        /// <summary>Gets the string used in type catalogs to identify a given unit.</summary>
        /// <returns>
        ///    The type catalog string, or an empty string if the unit cannot be used in type catalogs.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    unitTypeId is not a unit identifier. See UnitUtils.IsUnit(typeId) and UnitUtils.GetUnitTypeId(DisplayUnitType).
        /// </exception>
        [Pure]
        public string GetTypeCatalogStringForUnit()
        {
            return UnitUtils.GetTypeCatalogStringForUnit(typeId);
        }
#if REVIT2022_OR_GREATER
        /// <summary>
        ///    Checks whether a ForgeTypeId identifies a spec associated with units of measurement.
        /// </summary>
        /// <returns>True if the ForgeTypeId identifies a measurable spec, false otherwise.</returns>
        public bool IsMeasurableSpec => UnitUtils.IsMeasurableSpec(typeId);

        /// <summary>Gets the discipline for a given measurable spec.</summary>
        /// <returns>Identifier of the discipline.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(typeId).
        /// </exception>
        [Pure]
        public ForgeTypeId GetDiscipline()
        {
            return UnitUtils.GetDiscipline(typeId);
        }
#endif
    }
#endif
}