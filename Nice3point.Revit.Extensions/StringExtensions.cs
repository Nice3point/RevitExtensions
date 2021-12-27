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
    public static bool IsNullOrEmpty(this string source)
    {
        return string.IsNullOrEmpty(source);
    }

    /// <summary>
    ///     Indicates whether a specified string is null, empty, or consists only of white-space characters
    /// </summary>
    /// <returns>True if the value parameter is null or Empty, or if value consists exclusively of white-space characters</returns>
    public static bool IsNullOrWhiteSpace(this string source)
    {
        return string.IsNullOrWhiteSpace(source);
    }
    
    /// <summary>
    ///     Combines strings into a path
    /// </summary>
    /// <returns>The combined paths</returns>
    public static string AppendPath(this string source, string path)
    {
        return Path.Combine(source, path);
    }
}