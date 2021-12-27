namespace Nice3point.Revit.Extensions;

/// <summary>
///     System.Double Extensions
/// </summary>
public static class DoubleExtensions
{
    /// <summary>
    ///     Rounds a value within the minimum allowed by Revit
    /// </summary>
    public static double Round(this double source)
    {
        return Math.Round(source, 9);
    }

    /// <summary>
    ///     Rounds a value to the specified decimal place
    /// </summary>
    public static double Round(this double source, int digits)
    {
        return Math.Round(source, digits);
    }

    /// <summary>
    ///     Compares a decimal value within the minimum allowed by Revit
    /// </summary>
    /// <returns>True if equal</returns>
    public static bool AreEqual(this double source, double value)
    {
        return Math.Abs(source - value) < 1e-9;
    }

    /// <summary>
    ///     Compares a decimal value within the minimum allowed by Revit
    /// </summary>
    /// <returns>True if not equal</returns>
    public static bool AreNotEqual(this double source, double value)
    {
        return Math.Abs(source - value) >= 1e-9;
    }
}