namespace Nice3point.Revit.Extensions;

/// <summary>
///     System.Double Extensions
/// </summary>
public static class DoubleExtensions
{
    /// <summary>
    ///     Rounds a value within the minimum allowed by Revit
    /// </summary>
    /// <param name="source">A double-precision floating-point number to be rounded</param>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Round(this double source)
    {
        return Math.Round(source, 9);
    }

    /// <summary>
    ///     Rounds a decimal value to a specified number of fractional digits, and rounds midpoint values to the nearest even number
    /// </summary>
    /// <param name="source">A double-precision floating-point number to be rounded</param>
    /// <param name="digits">The number of fractional digits in the return value</param>
    /// <exception cref="System.ArgumentOutOfRangeException">Digits is less than 0 or greater than 15</exception>
    [Pure]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAlmostEqual(this double source, double value, double tolerance)
    {
        return Math.Abs(source - value) < tolerance;
    }
}