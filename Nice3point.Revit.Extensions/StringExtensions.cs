using System.IO;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     System.String Extensions
/// </summary>
public static class StringExtensions
{
    /// <summary>
    ///     Indicates whether the specified string is null or an empty string ("")
    /// </summary>
    /// <returns>True if the value parameter is null or an empty string (""); otherwise, false</returns>
    [Pure]
    public static bool IsNullOrEmpty(this string source)
    {
        return string.IsNullOrEmpty(source);
    }

    /// <summary>
    ///     Indicates whether a specified string is null, empty, or consists only of white-space characters
    /// </summary>
    /// <returns>True if the value parameter is null or Empty, or if value consists exclusively of white-space characters</returns>
    [Pure]
    public static bool IsNullOrWhiteSpace(this string source)
    {
        return string.IsNullOrWhiteSpace(source);
    }

    /// <summary>
    ///     Combines strings into a path
    /// </summary>
    /// <returns>The combined paths</returns>
    [NotNull]
    [Pure]
    public static string AppendPath(this string source, string path)
    {
        return Path.Combine(source, path);
    }

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
        return source?.IndexOf(value, comparison) >= 0;
    }
}