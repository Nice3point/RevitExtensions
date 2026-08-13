using System.IO;

namespace Nice3point.Revit.Extensions.Internal.Formats;

/// <summary>
///     Helper class to easier work with theme dependent resource URIs
/// </summary>
internal static class ThemeUriUtils
{
    private const string LightThemeToken = "light";
    private const string DarkThemeToken = "dark";

    /// <summary>
    ///     Attempts to modify the given URI to match the requested UI theme.
    /// </summary>
    /// <param name="uri">The original URI.</param>
    /// <param name="darkTheme"><see langword="true" /> to request the dark theme; <see langword="false" /> to request the light theme.</param>
    /// <param name="result">The modified URI corresponding to the requested UI theme, or the original URI if no modifications were made.</param>
    /// <returns><see langword="true" /> if the URI contains a theme token; otherwise, <see langword="false" />.</returns>
    /// <remarks>
    ///     The light theme token takes precedence over the dark one, and only its last occurrence is considered.
    ///     The token is replaced only when it belongs to the last URI segment.
    /// </remarks>
    [Pure]
    internal static bool TryGetThemedUri(string uri, bool darkTheme, out string result)
    {
        var uriToken = LightThemeToken;
        var tokenIndex = uri.LastIndexOf(LightThemeToken, StringComparison.OrdinalIgnoreCase);
        if (tokenIndex == -1)
        {
            uriToken = DarkThemeToken;
            tokenIndex = uri.LastIndexOf(DarkThemeToken, StringComparison.OrdinalIgnoreCase);
            if (tokenIndex == -1)
            {
                result = uri;
                return false;
            }
        }

        var requestedToken = darkTheme ? DarkThemeToken : LightThemeToken;
        result = uriToken == requestedToken ? uri : ReplaceThemeToken(uri, uriToken, requestedToken, tokenIndex);

        return true;
    }

    /// <summary>
    ///     Replaces the theme token located in the last URI segment.
    /// </summary>
    /// <param name="uri">The original URI.</param>
    /// <param name="uriToken">The theme token contained in the URI.</param>
    /// <param name="requestedToken">The theme token to apply.</param>
    /// <param name="tokenIndex">The index of the theme token in the URI.</param>
    /// <returns>The URI with the replaced token, or the original URI if the token belongs to a directory name.</returns>
    [Pure]
    private static string ReplaceThemeToken(string uri, string uriToken, string requestedToken, int tokenIndex)
    {
#if NET
        var uriSpan = uri.AsSpan();
        var prefix = uriSpan[..tokenIndex];
        var suffix = uriSpan[(tokenIndex + uriToken.Length)..];
#else
        var prefix = uri[..tokenIndex];
        var suffix = uri[(tokenIndex + uriToken.Length)..];
#endif

        if (suffix.IndexOf(Path.AltDirectorySeparatorChar) >= 0)
        {
            return uri;
        }

        if (suffix.IndexOf(Path.DirectorySeparatorChar) >= 0)
        {
            return uri;
        }

        return string.Concat(prefix, requestedToken, suffix);
    }
}
