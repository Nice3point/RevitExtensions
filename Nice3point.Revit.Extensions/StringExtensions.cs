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
    [ContractAnnotation("null=>true", true)]
    public static bool IsNullOrEmpty(this string source)
    {
        return string.IsNullOrEmpty(source);
    }

    /// <summary>
    ///     Indicates whether a specified string is null, empty, or consists only of white-space characters
    /// </summary>
    /// <returns>True if the value parameter is null or Empty, or if value consists exclusively of white-space characters</returns>
    [Pure]
    [ContractAnnotation("null=>true", true)]
    public static bool IsNullOrWhiteSpace(this string source)
    {
        return string.IsNullOrWhiteSpace(source);
    }

    /// <summary>
    ///     Combines strings into a path
    /// </summary>
    /// <returns>The combined paths</returns>
    /// <exception cref="System.ArgumentException">
    ///     source or path contains one or more of the invalid characters defined in <see cref="Path.GetInvalidPathChars" />
    /// </exception>
    /// <exception cref="System.ArgumentNullException">source or path is null</exception>
    [Pure]
    [NotNull]
    public static string AppendPath([NotNull] [LocalizationRequired(false)] this string source, [NotNull] [LocalizationRequired(false)] string path)
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
    [ContractAnnotation("source:null => false; value:null => false")]
    public static bool Contains(this string source, string value, StringComparison comparison)
    {
        if (source is null) return false;
        if (value is null) return false;
        return source.IndexOf(value, comparison) >= 0;
    }
}