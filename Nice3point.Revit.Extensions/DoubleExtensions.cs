using Autodesk.Revit.DB;

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
    /// <returns>true if equal</returns>
    public static bool IsAlmostEqual(this double source, double value)
    {
        return Math.Abs(source - value) < 1e-9;
    }
    
    /// <summary>
    ///     Compares the decimal value to the specified tolerance
    /// </summary>
    /// <returns>true if equal</returns>
    /// <example>0.09999.IsAlmostEqual(0.1, 1e-3)</example>
    public static bool IsAlmostEqual(this double source, double value, double tolerance)
    {
        return Math.Abs(source - value) < tolerance;
    }
}