// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.UnitUtils" /> class.
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
        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.IsSymbol(Autodesk.Revit.DB.ForgeTypeId)"/>
        public bool IsSymbol => UnitUtils.IsSymbol(typeId);

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.IsUnit(Autodesk.Revit.DB.ForgeTypeId)"/>
        public bool IsUnit => UnitUtils.IsUnit(typeId);

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.GetValidUnits(Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public IList<ForgeTypeId> GetValidUnits()
        {
            return UnitUtils.GetValidUnits(typeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.GetTypeCatalogStringForSpec(Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public string GetTypeCatalogStringForSpec()
        {
            return UnitUtils.GetTypeCatalogStringForSpec(typeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.IsValidUnit(Autodesk.Revit.DB.ForgeTypeId,Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public bool IsValidUnit(ForgeTypeId unitTypeId)
        {
            return UnitUtils.IsValidUnit(typeId, unitTypeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.GetTypeCatalogStringForUnit(Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public string GetTypeCatalogStringForUnit()
        {
            return UnitUtils.GetTypeCatalogStringForUnit(typeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.GetAllUnits"/>
        public static IList<ForgeTypeId> GetAllUnits()
        {
            return UnitUtils.GetAllUnits();
        }
#if REVIT2022_OR_GREATER

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.IsMeasurableSpec(Autodesk.Revit.DB.ForgeTypeId)"/>
        public bool IsMeasurableSpec => UnitUtils.IsMeasurableSpec(typeId);

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.GetDiscipline(Autodesk.Revit.DB.ForgeTypeId)"/>
        [Pure]
        public ForgeTypeId GetDiscipline()
        {
            return UnitUtils.GetDiscipline(typeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.GetAllDisciplines"/>
        [Pure]
        public static IList<ForgeTypeId> GetAllDisciplines()
        {
            return UnitUtils.GetAllDisciplines();
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitUtils.GetAllMeasurableSpecs"/>
        [Pure]
        public static IList<ForgeTypeId> GetAllMeasurableSpecs()
        {
            return UnitUtils.GetAllMeasurableSpecs();
        }
#endif
    }
#endif
}
