using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Imperial Units Extensions
/// </summary>
[PublicAPI]
public static class ImperialExtensions
{
    private const string Expression =
        """
        ^\s*(?<sign>-)?\s*(((?<feet>[\d.]+)')?[\s-]*((?<inch>(\d+)?(\.)?\d+)?[\s-]*((?<numerator>\d+)/(?<denominator>\d+))?"?)?)\s*$
        """;

    private static readonly Regex Regex = new(Expression, RegexOptions.Compiled);

    /// <summary>
    ///     Converts a number to text representation for the Imperial system with denominator 32
    /// </summary>
    /// <param name="source">Feet value</param>
    /// <param name="denominator">Rounding. Denominator must be greater than or equal to 1</param>
    /// <example>
    ///     1 will be converted to 1'-0"<br />
    ///     0.0123 will be converted to 0 5/32"<br />
    ///     25.231 will be converted to 25'-2 25/32"
    /// </example>
    [Pure]
    public static string ToFraction(this double source, int denominator)
    {
        if (denominator < 1) throw new ArgumentException("Denominator must be greater than or equal to 1", nameof(denominator));
        var divider = denominator;

        var feet = (int)Math.Abs(source);
        var decimalInches = source * 12 % 12;
        var inches = (int)Math.Abs(decimalInches);
        var numerator = (int)((Math.Abs(decimalInches) - Math.Abs(inches)) * divider + 0.5);

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
        if (source + 1d / denominator < 0) valueBuilder.Append("-");

        if (feet > 0)
        {
            valueBuilder.Append(feet);
            valueBuilder.Append("'");
            valueBuilder.Append("-");
        }

        valueBuilder.Append(inches);

        if (numerator != 0)
        {
            valueBuilder.Append(" ");
            valueBuilder.Append(numerator);
            valueBuilder.Append("/");
            valueBuilder.Append(divider);
        }

        valueBuilder.Append('"');
        return valueBuilder.ToString();
    }

    /// <summary>
    ///     Converts a number to text representation for the Imperial system
    /// </summary>
    /// <param name="source">Feet value</param>
    /// <example>
    ///     1 will be converted to 1'-0"<br />
    ///     0.0123 will be converted to 0 5/32"<br />
    ///     25.231 will be converted to 25'-2 25/32"
    /// </example>
    [Pure]
    public static string ToFraction(this double source)
    {
        return ToFraction(source, 32);
    }

    /// <summary>
    ///     Converts the textual representation of the Imperial system number to double
    /// </summary>
    /// <param name="source">Imperial number</param>
    /// <param name="value">Feet value</param>
    /// <returns>True if conversion is successful</returns>
    /// <example>
    ///     1' will be converted to 1<br />
    ///     1/8" will be converted to 0.010<br />
    ///     1'-3/32" will be converted to 1.007<br />
    ///     1'1.75" will be converted to 1.145
    /// </example>
    [Pure]
    [ContractAnnotation("source:null => false")]
    [Obsolete("Use TryFromFraction instead")]
    public static bool FromFraction(this string source, out double value)
    {
        return TryFromFraction(source, out value);
    }

    /// <summary>
    ///     Converts the textual representation of the Imperial system number to double
    /// </summary>
    /// <param name="source">Imperial number</param>
    /// <param name="value">Feet value</param>
    /// <returns>True if conversion is successful</returns>
    /// <example>
    ///     1' will be converted to 1<br />
    ///     1/8" will be converted to 0.010<br />
    ///     1'-3/32" will be converted to 1.007<br />
    ///     1'1.75" will be converted to 1.145
    /// </example>
    [Pure]
    [ContractAnnotation("source:null => false")]
    public static bool TryFromFraction(this string? source, out double value)
    {
        value = 0;
        if (source is null) return false;
        if (source.Trim() == string.Empty) return true;

        var match = Regex.Match(source);
        if (!match.Success) return false;

        value = ParseFraction(match);
        return true;
    }

    /// <summary>
    ///     Converts the textual representation of the Imperial system number to double
    /// </summary>
    /// <param name="source">Imperial number</param>
    /// <returns>Feet value</returns>
    /// <exception cref="FormatException">Invalid number format</exception>
    /// <example>
    ///     1' will be converted to 1<br />
    ///     1/8" will be converted to 0.010<br />
    ///     1'-3/32" will be converted to 1.007<br />
    ///     1'1.75" will be converted to 1.145
    /// </example>
    [Pure]
    public static double FromFraction(this string source)
    {
        if (source.Trim() == string.Empty) return 0;

        var match = Regex.Match(source);
        if (!match.Success) throw new FormatException($"Invalid number format: {source}");

        return ParseFraction(match);
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