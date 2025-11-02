using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Imperial Units Extensions
/// </summary>
[PublicAPI]
[Obsolete("Use UnitsNet package instead")]
public static
#if NETCOREAPP
    partial
#endif
    class ImperialExtensions
{
    private const string ImperialExpression =
        """
        ^\s*(?<sign>-)?\s*(((?<feet>[\d.]+)')?[\s-]*((?<inch>(\d+)?(\.)?\d+)?[\s-]*((?<numerator>\d+)/(?<denominator>\d+))?"?)?)\s*$
        """;

#if NETCOREAPP
    private static readonly Regex ImperialRegex = InvokeImperialRegexGenerator();
    [GeneratedRegex(ImperialExpression, RegexOptions.Compiled)] private static partial Regex InvokeImperialRegexGenerator();
#else
    private static readonly Regex ImperialRegex = new(ImperialExpression, RegexOptions.Compiled);
#endif

    /// <param name="source">The Imperial system number as a string (e.g., 1'-3/32").</param>
    extension(string? source)
    {
        /// <summary>
        ///     Converts a string representation of a measurement in the Imperial system (feet and inches) to a double value.
        /// </summary>
        /// <returns>The equivalent value in feet as a double.</returns>
        /// <exception cref="FormatException">Thrown when the input string has an invalid format.</exception>
        /// <remarks>
        ///     This method handles input in the format of feet, inches, and fractional inches. It supports several variations:
        ///     <list type="bullet">
        ///         <item>Whole feet (e.g., 1')</item>
        ///         <item>Fractional inches (e.g., 1/8")</item>
        ///         <item>Feet and fractional inches (e.g., 1'-3/32")</item>
        ///         <item>Feet and decimal inches (e.g., 1'1.75")</item>
        ///     </list>
        ///     The method will convert these values into a total feet measurement, returning the result as a double.
        /// </remarks>
        [Pure]
        public double FromFraction()
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var match = ImperialRegex.Match(source);
            if (!match.Success) throw new FormatException($"Invalid imperial format: {source}");

            return ParseFraction(match);
        }

        /// <summary>
        ///     Attempts to convert the string representation of a measurement in the Imperial system (feet and inches)
        ///     to a double value, returning a success flag instead of throwing an exception on failure.
        /// </summary>
        /// <param name="value">When this method returns, contains the converted value in feet if the conversion was successful; otherwise, 0.</param>
        /// <returns>
        ///     <c>true</c> if the string was successfully converted to a double; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        ///     The method does not throw exceptions on invalid input, but instead returns <c>false</c>.
        ///     It supports several variations of the Imperial system format:
        ///     <list type="bullet">
        ///         <item>Whole feet (e.g., 1')</item>
        ///         <item>Fractional inches (e.g., 1/8")</item>
        ///         <item>Feet and fractional inches (e.g., 1'-3/32")</item>
        ///         <item>Feet and decimal inches (e.g., 1'1.75")</item>
        ///     </list>
        /// </remarks>
        [Pure]
        public bool TryFromFraction(out double value)
        {
            value = 0;

            if (string.IsNullOrWhiteSpace(source)) return false;

            var match = ImperialRegex.Match(source);
            if (!match.Success) return false;

            value = ParseFraction(match);
            return true;
        }

        /// <summary>
        ///     Attempts to convert the string representation of a measurement in the Imperial system.
        /// </summary>
        [Pure]
        [Obsolete("Use TryFromFraction instead")]
        public bool FromFraction(out double value)
        {
            return TryFromFraction(source, out value);
        }
    }

    /// <param name="source">The measurement in feet as a double.</param>
    extension(double source)
    {
        /// <summary>
        ///     Converts a double value representing a measurement in feet to its string representation in the Imperial system,
        ///     expressed as feet, inches, and fractional inches with a specified denominator.
        /// </summary>
        /// <param name="denominator">
        ///     The denominator used for fractional inches (8 for 1/8", 16 for 1/16"). 
        /// </param>
        /// <returns>
        ///     A string representation of the measurement in feet, inches, and fractional inches.
        ///     For example:
        ///     <list type="bullet">
        ///         <item>0.0123 input with a denominator 8 returns 1/8"</item>
        ///         <item>12.006 input with a denominator 16 returns 12'-1/16"</item>
        ///         <item>25.222 input with a denominator 32 returns 25'-2 21/32"</item>
        ///     </list>
        /// </returns>
        [Pure]
        public string ToFraction(int denominator)
        {
            if (denominator < 1) throw new ArgumentException("Denominator must be greater than or equal to 1", nameof(denominator));

            var divider = denominator;
            var feet = (int)Math.Abs(source);
            var decimalInches = Math.Abs((int)(source * 12 % 12));
            var inches = (int)Math.Abs(decimalInches);
            var numerator = (int)((decimalInches - inches) * divider + 0.5);

            while (numerator % 2 == 0 && divider % 2 == 0)
            {
                numerator /= 2;
                divider /= 2;
            }

            if (divider == numerator)
            {
                if (source < 0) inches--;
                else inches++;
                numerator = 0;
            }

            var valueBuilder = new StringBuilder();
            if (source + 1d / denominator < 0) valueBuilder.Append('-');

            if (feet > 0)
            {
                valueBuilder.Append(feet);
                valueBuilder.Append('\'');
            }

            if (inches > 0 || numerator != 0)
            {
                if (feet > 0) valueBuilder.Append('-');

                if (inches > 0)
                {
                    valueBuilder.Append(inches);
                }

                if (numerator != 0)
                {
                    if (inches > 0) valueBuilder.Append(' ');
                    valueBuilder.Append(numerator);
                    valueBuilder.Append('/');
                    valueBuilder.Append(divider);
                }

                valueBuilder.Append('"');
            }

            return valueBuilder.Length == 0 ? "0\"" : valueBuilder.ToString();
        }

        /// <summary>
        ///     Converts a double value representing a measurement in feet to its string representation in the Imperial system,
        ///     expressed as feet, inches, and fractional inches. The default denominator for fractional inches is 8.
        /// </summary>
        /// <returns>
        ///     A string representation of the measurement in feet, inches, and fractional inches, using 1/8" increments.
        ///     For example:
        ///     <list type="bullet">
        ///         <item>0.0123 input returns 1/8"</item>
        ///         <item>12.011 input returns 12'-1/8"</item>
        ///         <item>25.222 input returns 25'-2 1/8"</item>
        ///     </list>
        /// </returns>
        [Pure]
        public string ToFraction()
        {
            return ToFraction(source, 8);
        }
    }

    private static double ParseFraction(Match match)
    {
        var sign = match.Groups["sign"].Success ? -1 : 1;
        var feet = match.Groups["feet"].Success ? double.Parse(match.Groups["feet"].Value, CultureInfo.InvariantCulture) : 0;
        var inch = match.Groups["inch"].Success ? double.Parse(match.Groups["inch"].Value, CultureInfo.InvariantCulture) : 0;
        var numerator = match.Groups["numerator"].Success ? double.Parse(match.Groups["numerator"].Value, CultureInfo.InvariantCulture) : 0;
        var denominator = match.Groups["denominator"].Success ? double.Parse(match.Groups["denominator"].Value, CultureInfo.InvariantCulture) : 1;

        return sign * (feet + (inch + numerator / denominator) / 12);
    }
}