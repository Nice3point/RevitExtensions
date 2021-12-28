using System.Text;
using System.Text.RegularExpressions;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Imperial Units Extensions
/// </summary>
public static class ImperialExtensions
{
    private const string Expr = "^\\s*(?<sign>-)?\\s*(((?<feet>[\\d.]+)')?[\\s-]*((?<inch>(\\d+)?(\\.)?\\d+)?[\\s-]*((?<numerator>\\d+)/(?<denominator>\\d+))?\"?)?)\\s*$";
    private static readonly Regex Regex = new(Expr, RegexOptions.Compiled);

    /// <summary>
    ///     Converts a number to text representation for the Imperial system with denominator 32
    /// </summary>
    /// <param name="source">Inch value</param>
    /// <param name="denominator">Rounding</param>
    /// <example>
    ///     1 will be converted to 1"<br />
    ///     3.24997 will be converted to 3 1/4"<br />
    ///     -25.231 will be converted to -2'-1 7/32"
    /// </example>
    public static string ToFraction(this double source, int denominator)
    {
        var divider = denominator;
        var inches = (int) Math.Abs(source);
        var numerator = (int) ((Math.Abs(source) - Math.Abs(inches)) * divider + 0.5);

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

        var feet = Math.DivRem(inches, 12, out inches);

        var valueBuilder = new StringBuilder();
        if (source + 1d / denominator < 0) valueBuilder.Insert(0, "-");

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
    /// <param name="source">Inch value</param>
    /// <example>
    ///     1 will be converted to 1"<br />
    ///     3.24997 will be converted to 3 1/4"<br />
    ///     -25.231 will be converted to -2'-1 7/32"
    /// </example>
    public static string ToFraction(this double source)
    {
        return ToFraction(source, 32);
    }

    /// <summary>
    ///     Converts the textual representation of the Imperial system number to double
    /// </summary>
    /// <param name="source">Imperial number</param>
    /// <param name="value">Inch value</param>
    /// <returns>true if conversion is successful</returns>
    public static bool FromFraction(this string source, out double value)
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
    /// <returns>Inch value</returns>
    /// <exception cref="FormatException">Invalid number format</exception>
    public static double FromFraction(this string source)
    {
        if (source.Trim() == string.Empty) return 0;

        var match = Regex.Match(source);
        if (!match.Success) throw new FormatException("Invalid number format");

        return ParseFraction(match);
    }

    private static double ParseFraction(Match match)
    {
        var sign = match.Groups["sign"].Success ? -1 : 1;
        var feet = match.Groups["feet"].Success ? double.Parse(match.Groups["feet"].Value) : 0;
        var inch = match.Groups["inch"].Success ? double.Parse(match.Groups["inch"].Value) : 0;
        var numerator = match.Groups["numerator"].Success ? double.Parse(match.Groups["numerator"].Value) : 0;
        var denominator = match.Groups["denominator"].Success ? double.Parse(match.Groups["denominator"].Value) : 1;

        return sign * (feet * 12 + inch + numerator / denominator);
    }
}