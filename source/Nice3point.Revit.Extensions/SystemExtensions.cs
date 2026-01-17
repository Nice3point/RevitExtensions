using System.IO;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.System;

/// <summary>
///     System Extensions
/// </summary>
[PublicAPI]
public static class SystemExtensions
{
    /// <param name="obj">The source object</param>
    extension(object obj)
    {
        /// <summary>
        ///     Converts an object's type to <typeparamref name="T"/> type
        /// </summary>
        [Pure]
        public T Cast<T>()
        {
            return (T)obj;
        }
    }

    /// <param name="source">A double-precision floating-point number</param>
    extension(double source)
    {
        /// <summary>
        ///     Rounds a value within the minimum allowed by Revit
        /// </summary>
        [Pure]
        public double Round()
        {
            return Math.Round(source, 9);
        }

        /// <summary>
        ///     Rounds a decimal value to a specified number of fractional digits, and rounds midpoint values to the nearest even number
        /// </summary>
        /// <param name="digits">The number of fractional digits in the return value</param>
        /// <exception cref="System.ArgumentOutOfRangeException">Digits is less than 0 or greater than 15</exception>
        [Pure]
        public double Round(int digits)
        {
            return Math.Round(source, digits);
        }

        /// <summary>
        ///     Compares a decimal value within the minimum allowed by Revit
        /// </summary>
        /// <returns>True if equal</returns>
        /// <example>1e-15.IsAlmostEqual(0)</example>
        [Pure]
        public bool IsAlmostEqual(double value)
        {
            return Math.Abs(source - value) < 1e-9;
        }

        /// <summary>
        ///     Compares the decimal value to the specified tolerance
        /// </summary>
        /// <returns>True if equal</returns>
        /// <example>0.09999.IsAlmostEqual(0.1, 1e-3)</example>
        [Pure]
        public bool IsAlmostEqual(double value, double tolerance)
        {
            return Math.Abs(source - value) < tolerance;
        }
    }

    /// <param name="source">The source string</param>
    extension(string? source)
    {
        /// <summary>
        ///     Indicates whether the specified string is null or an empty string ("")
        /// </summary>
        /// <returns>True if the value parameter is null or an empty string (""); otherwise, false</returns>
        [Pure]
        public bool IsNullOrEmpty()
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        ///     Indicates whether a specified string is null, empty, or consists only of white-space characters
        /// </summary>
        /// <returns>True if the value parameter is null or Empty, or if value consists exclusively of white-space characters</returns>
        [Pure]
        public bool IsNullOrWhiteSpace()
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
        public string AppendPath(string path)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

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
        public string AppendPath(params string[] paths)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var strings = new string[paths.Length + 1];
            strings[0] = source;
            for (var i = 1; i < strings.Length; i++)
            {
                strings[i] = paths[i - 1];
            }

            return Path.Combine(strings);
        }
    }
}