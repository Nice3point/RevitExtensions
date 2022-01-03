namespace Nice3point.Revit.Extensions;

/// <summary>
///     System.Double Extensions
/// </summary>
public static class DoubleExtensions
{
    /// <summary>
    ///     Rounds a value within the minimum allowed by Revit
    /// </summary>
    [Pure]
    public static double Round(this double source)
    {
        return Math.Round(source, 9);
    }

    /// <summary>
    ///     Rounds a decimal value to a specified number of fractional digits, and rounds midpoint values to the nearest even number
    /// </summary>
    /// <exception cref="System.ArgumentOutOfRangeException">digits is less than 0 or greater than 15</exception>
    [Pure]
    public static double Round(this double source, int digits)
    {
        return Math.Round(source, digits);
    }

    /// <summary>
    ///     Compares a decimal value within the minimum allowed by Revit
    /// </summary>
    /// <returns>True if equal</returns>
    /// <example>1e-15.IsAlmostEqual(0)</example>
    [Pure]
    public static bool IsAlmostEqual(this double source, double value)
    {
        return Math.Abs(source - value) < 1e-9;
    }

    /// <summary>
    ///     Compares the decimal value to the specified tolerance
    /// </summary>
    /// <returns>True if equal</returns>
    /// <example>0.09999.IsAlmostEqual(0.1, 1e-3)</example>
    [Pure]
    public static bool IsAlmostEqual(this double source, double value, double tolerance)
    {
        return Math.Abs(source - value) < tolerance;
    }
}