using System.IO;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     System.String Extensions
/// </summary>
[PublicAPI]
public static class SystemExtensions
{
    /// <summary>
    ///     Converts an object's type to <typeparamref name="T"/> type
    /// </summary>
    [Pure]
    public static T Cast<T>(this object obj)
    {
        return (T)obj;
    }

    /// <summary>
    ///     Rounds a value within the minimum allowed by Revit
    /// </summary>
    /// <param name="source">A double-precision floating-point number to be rounded</param>
    [Pure]
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

    /// <summary>
    ///     Indicates whether the specified string is null or an empty string ("")
    /// </summary>
    /// <param name="source">The string to test</param>
    /// <returns>True if the value parameter is null or an empty string (""); otherwise, false</returns>
    [Pure]
    [ContractAnnotation("null=>true", true)]
    public static bool IsNullOrEmpty(this string? source)
    {
        return string.IsNullOrEmpty(source);
    }

    /// <summary>
    ///     Indicates whether a specified string is null, empty, or consists only of white-space characters
    /// </summary>
    /// <param name="source">The string to test</param>
    /// <returns>True if the value parameter is null or Empty, or if value consists exclusively of white-space characters</returns>
    [Pure]
    [ContractAnnotation("null=>true", true)]
    public static bool IsNullOrWhiteSpace(this string? source)
    {
        return string.IsNullOrWhiteSpace(source);
    }

    /// <summary>
    ///     Combines strings into a path
    /// </summary>
    /// <returns>
    ///     The combined paths.
    ///     If one of the specified paths is a zero-length string, this method returns the other path.
    ///     If path2 contains an absolute path, this method returns path2.
    /// </returns>
    /// <exception cref="System.ArgumentException">
    ///     NET Framework and .NET Core versions older than 2.1: path1 or path2 contains one or more of the invalid characters defined in <see cref="Path.GetInvalidPathChars" />
    /// </exception>
    /// <exception cref="System.ArgumentNullException">source or path is null</exception>
    [Pure]
    public static string AppendPath(this string source, string path)
    {
        return Path.Combine(source, path);
    }

    /// <summary>
    ///     Combines strings into a path
    /// </summary>
    /// <returns>The combined paths</returns>
    /// <exception cref="System.ArgumentException">
    ///     NET Framework and .NET Core versions older than 2.1: path1 or path2 contains one or more of the invalid characters defined in <see cref="Path.GetInvalidPathChars" />
    /// </exception>
    /// <exception cref="System.ArgumentNullException">source or path is null</exception>
    [Pure]
    public static string AppendPath(this string source, params string[] paths)
    {
        var strings = new string[paths.Length + 1];
        strings[0] = source;
        for (var i = 1; i < strings.Length; i++)
        {
            strings[i] = paths[i - 1];
        }

        return Path.Combine(strings);
    }
#if !NETCOREAPP

    /// <summary>
    ///     Returns a value indicating whether a specified substring occurs within this string.
    /// </summary>
    /// <param name="source">Source string</param>
    /// <param name="value">The string to seek</param>
    /// <param name="comparison">One of the enumeration values that specifies the rules for the search</param>
    /// <returns>True if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false</returns>
    [Pure]
    public static bool Contains(this string source, string value, StringComparison comparison)
    {
        return source.IndexOf(value, comparison) >= 0;
    }
#endif
}