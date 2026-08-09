

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.UnitFormatUtils"/> class.
/// </summary>
[PublicAPI]
public static class UnitFormatUtilsExtensions
{
    /// <param name="units">The units formatting settings.</param>
    extension(Units units)
    {
#if REVIT2021_OR_GREATER
        /// <inheritdoc cref="Autodesk.Revit.DB.UnitFormatUtils.Format(Autodesk.Revit.DB.Units,Autodesk.Revit.DB.ForgeTypeId,System.Double,System.Boolean,Autodesk.Revit.DB.FormatValueOptions)"/>
        [Pure]
        public string Format(ForgeTypeId specTypeId, double value, bool forEditing, FormatValueOptions options)
        {
            return UnitFormatUtils.Format(units, specTypeId, value, forEditing, options);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitFormatUtils.TryParse(Autodesk.Revit.DB.Units,Autodesk.Revit.DB.ForgeTypeId,System.String,out System.Double,out System.String)"/>
        public bool TryParse(ForgeTypeId specTypeId, string stringToParse, out double value, out string message)
        {
            return UnitFormatUtils.TryParse(units, specTypeId, stringToParse, out value, out message);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitFormatUtils.TryParse(Autodesk.Revit.DB.Units,Autodesk.Revit.DB.ForgeTypeId,System.String,out System.Double)"/>
        public bool TryParse(ForgeTypeId specTypeId, string stringToParse, out double value)
        {
            return UnitFormatUtils.TryParse(units, specTypeId, stringToParse, out value);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitFormatUtils.TryParse(Autodesk.Revit.DB.Units,Autodesk.Revit.DB.ForgeTypeId,System.String,Autodesk.Revit.DB.ValueParsingOptions,out System.Double,out System.String)"/>
        public bool TryParse(ForgeTypeId specTypeId, string stringToParse, ValueParsingOptions valueParsingOptions, out double value, out string message)
        {
            return UnitFormatUtils.TryParse(units, specTypeId, stringToParse, valueParsingOptions, out value, out message);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitFormatUtils.Format(Autodesk.Revit.DB.Units,Autodesk.Revit.DB.ForgeTypeId,System.Double,System.Boolean)"/>
        [Pure]
        public string Format(ForgeTypeId specTypeId, double value, bool forEditing)
        {
            return UnitFormatUtils.Format(units, specTypeId, value, forEditing);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitFormatUtils.TryParse(Autodesk.Revit.DB.Units,Autodesk.Revit.DB.ForgeTypeId,System.String,Autodesk.Revit.DB.ValueParsingOptions,out System.Double)"/>
        public bool TryParse(ForgeTypeId specTypeId, string stringToParse, ValueParsingOptions valueParsingOptions, out double value)
        {
            return UnitFormatUtils.TryParse(units, specTypeId, stringToParse, valueParsingOptions, out value);
        }
#endif
#if !REVIT2022_OR_GREATER
        /// <inheritdoc cref="Autodesk.Revit.DB.UnitFormatUtils.Format(Autodesk.Revit.DB.Units,Autodesk.Revit.DB.UnitType,System.Double,System.Boolean,System.Boolean)"/>
        [Pure]
#if REVIT2021
        [Obsolete("This method is deprecated in Revit 2021")]
#endif
        public string FormatUnit(UnitType unitType, double value, bool maxAccuracy, bool forEditing)
        {
            return UnitFormatUtils.Format(units, unitType, value, maxAccuracy, forEditing);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.UnitFormatUtils.Format(Autodesk.Revit.DB.Units,Autodesk.Revit.DB.UnitType,System.Double,System.Boolean,System.Boolean,Autodesk.Revit.DB.FormatValueOptions)"/>
        [Pure]
#if REVIT2021
        [Obsolete("This method is deprecated in Revit 2021")]
#endif
        public string FormatUnit(UnitType unitType, double value, bool maxAccuracy, bool forEditing, FormatValueOptions options)
        {
            return UnitFormatUtils.Format(units, unitType, value, maxAccuracy, forEditing, options);
        }
#endif
    }
}