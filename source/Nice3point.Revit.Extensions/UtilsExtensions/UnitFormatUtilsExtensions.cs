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
        /// <summary>Formats a number with units into a string</summary>
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
        public string Format(ForgeTypeId specTypeId, double value, bool forEditing, FormatValueOptions options)
        {
            return UnitFormatUtils.Format(units, specTypeId, value, forEditing, options);
        }

        /// <summary>Parses a formatted string into a number with units if possible.</summary>
        /// <param name="specTypeId">Identifier of the target spec for the value.</param>
        /// <param name="stringToParse">The string to parse.</param>
        /// <param name="value">
        ///    The parsed value. Ignore this value if the function returns false.
        /// </param>
        /// <param name="message">
        ///    A localized message that, if the parsing fails, explains the reason for failure.
        /// </param>
        /// <returns>True if the string can be parsed, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(ForgeTypeId).
        /// </exception>
        public bool TryParse(ForgeTypeId specTypeId, string stringToParse, out double value, out string message)
        {
            return UnitFormatUtils.TryParse(units, specTypeId, stringToParse, out value, out message);
        }

        /// <summary>Parses a formatted string into a number with units if possible.</summary>
        /// <param name="specTypeId">Identifier of the target spec for the value.</param>
        /// <param name="stringToParse">The string to parse.</param>
        /// <param name="value">
        ///    The parsed value. Ignore this value if the function returns false.
        /// </param>
        /// <returns>True if the string can be parsed, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(ForgeTypeId).
        /// </exception>
        public bool TryParse(ForgeTypeId specTypeId, string stringToParse, out double value)
        {
            return UnitFormatUtils.TryParse(units, specTypeId, stringToParse, out value);
        }

        /// <summary>Parses a formatted string into a number with units if possible.</summary>
        /// <param name="specTypeId">Identifier of the target spec for the value.</param>
        /// <param name="stringToParse">The string to parse.</param>
        /// <param name="valueParsingOptions">Additional parsing options.</param>
        /// <param name="value">
        ///    The parsed value. Ignore this value if the function returns false.
        /// </param>
        /// <param name="message">
        ///    A localized message that, if the parsing fails, explains the reason for failure.
        /// </param>
        /// <returns>True if the string can be parsed, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(ForgeTypeId).
        ///    -or-
        ///    The unit in the FormatOptions in valueParsingOptions is not a valid unit for specTypeId. See UnitUtils.IsValidUnit(ForgeTypeId, ForgeTypeId) and UnitUtils.GetValidUnits(ForgeTypeId).
        /// </exception>
        public bool TryParse(ForgeTypeId specTypeId, string stringToParse, ValueParsingOptions valueParsingOptions, out double value, out string message)
        {
            return UnitFormatUtils.TryParse(units, specTypeId, stringToParse, valueParsingOptions, out value, out message);
        }

        /// <summary>Formats a number with units into a string</summary>
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
        public string Format(ForgeTypeId specTypeId, double value, bool forEditing)
        {
            return UnitFormatUtils.Format(units, specTypeId, value, forEditing);
        }

        /// <summary>Parses a formatted string into a number with units if possible.</summary>
        /// <param name="specTypeId">Identifier of the target spec for the value.</param>
        /// <param name="stringToParse">The string to parse.</param>
        /// <param name="valueParsingOptions">Additional parsing options.</param>
        /// <param name="value">
        ///    The parsed value. Ignore this value if the function returns false.
        /// </param>
        /// <returns>True if the string can be parsed, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    specTypeId is not a measurable spec identifier. See UnitUtils.IsMeasurableSpec(ForgeTypeId).
        ///    -or-
        ///    The unit in the FormatOptions in valueParsingOptions is not a valid unit for specTypeId. See UnitUtils.IsValidUnit(ForgeTypeId, ForgeTypeId) and UnitUtils.GetValidUnits(ForgeTypeId).
        /// </exception>
        public bool TryParse(ForgeTypeId specTypeId, string stringToParse, ValueParsingOptions valueParsingOptions, out double value)
        {
            return UnitFormatUtils.TryParse(units, specTypeId, stringToParse, valueParsingOptions, out value);
        }
#endif
#if !REVIT2022_OR_GREATER
        /// <summary>Formats a number with units into a string</summary>
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
        public string FormatUnit(UnitType unitType, double value, bool maxAccuracy, bool forEditing)
        {
            return UnitFormatUtils.Format(units, unitType, value, maxAccuracy, forEditing);
        }

        /// <summary>Formats a number with units into a string</summary>
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
        public string FormatUnit(UnitType unitType, double value, bool maxAccuracy, bool forEditing, FormatValueOptions options)
        {
            return UnitFormatUtils.Format(units, unitType, value, maxAccuracy, forEditing, options);
        }
#endif
    }
}