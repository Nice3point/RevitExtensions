using System.Windows;
using System.Windows.Interop;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Application Extensions
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    ///     Gets list of function names supported by formula engine
    /// </summary>
    [Pure]
    public static IList<string> GetFormulaFunctions(this Application application)
    {
        return FormulaManager.GetFunctions();
    }
    
    /// <summary>
    ///     Gets list of operator names supported by formula engine
    /// </summary>
    [Pure]
    public static IList<string> GetFormulaOperators(this Application application)
    {
        return FormulaManager.GetOperators();
    }
}