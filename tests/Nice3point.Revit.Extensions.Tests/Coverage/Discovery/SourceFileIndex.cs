using System.Text.RegularExpressions;

namespace Nice3point.Revit.Extensions.Tests.Coverage.Discovery;

/// <summary>
///     Searches the library sources for the files mentioning a <c>Type.Member</c> name or extending a type.
/// </summary>
/// <remarks>
///     The match is textual and case sensitive. A name written in a comment or in documentation counts as a mention.
/// </remarks>
internal sealed partial class SourceFileIndex
{
    private const string ReceiverExpression = @"(?:extension\(|\(\s*this\s+)\s*(?:[\w.]+\.)?(?<type>\w+)\b";

    private readonly List<(string FileName, string Content)> _files;
    private readonly Dictionary<string, List<string>> _fileNamesByReceiverType;

    private SourceFileIndex(List<(string FileName, string Content)> files)
    {
        _files = files;
        _fileNamesByReceiverType = BuildReceiverIndex(files);
    }

    /// <summary>
    ///     Reads every C# file below the directory.
    /// </summary>
    /// <param name="sourceDirectory">The library source directory.</param>
    /// <exception cref="DirectoryNotFoundException">The directory does not exist.</exception>
    public static SourceFileIndex Build(string sourceDirectory)
    {
        if (!Directory.Exists(sourceDirectory))
        {
            throw new DirectoryNotFoundException($"The library source directory does not exist: {sourceDirectory}");
        }

        var files = Directory
            .EnumerateFiles(sourceDirectory, "*.cs", SearchOption.AllDirectories)
            .Select(filePath => (FileName: Path.GetFileName(filePath), Content: File.ReadAllText(filePath)))
            .ToList();

        return new SourceFileIndex(files);
    }

    /// <summary>
    ///     Reads the files mentioning the name anywhere in their text.
    /// </summary>
    /// <param name="qualifiedName">The <c>Type.Member</c> name to look up.</param>
    [Pure]
    public IReadOnlyList<string> FindReferencingFiles(string qualifiedName)
    {
        return _files
            .Where(file => file.Content.Contains(qualifiedName, StringComparison.Ordinal))
            .Select(file => file.FileName)
            .ToList();
    }

    /// <summary>
    ///     Reads the files declaring an extension over the type.
    /// </summary>
    /// <param name="typeName">The short name of the extended type.</param>
    /// <remarks>
    ///     A call to an instance member never appears in a wrapper as a <c>Type.Member</c> name. The extension receiver
    ///     carries the only textual trace of the wrapped type.
    /// </remarks>
    [Pure]
    public IReadOnlyList<string> FindExtendingFiles(string typeName)
    {
        return _fileNamesByReceiverType.TryGetValue(typeName, out var fileNames) ? fileNames : [];
    }

    /// <summary>
    ///     Groups the file names by the short name of every type they extend.
    /// </summary>
    private static Dictionary<string, List<string>> BuildReceiverIndex(List<(string FileName, string Content)> files)
    {
        var index = new Dictionary<string, List<string>>(StringComparer.Ordinal);

        foreach (var file in files)
        {
            foreach (var match in ReceiverPattern().Matches(file.Content).Cast<Match>())
            {
                var typeName = match.Groups["type"].Value;

                if (!index.TryGetValue(typeName, out var fileNames))
                {
                    fileNames = [];
                    index[typeName] = fileNames;
                }

                // The matches of one file arrive in a row. The last entry holds every repeated receiver.
                if (fileNames is [.., var lastFileName] && lastFileName == file.FileName)
                {
                    continue;
                }

                fileNames.Add(file.FileName);
            }
        }

        return index;
    }
#if NET

    /// <summary>
    ///     Matches an extension block receiver and a classic extension method receiver, capturing the short type name.
    /// </summary>
    [GeneratedRegex(ReceiverExpression)]
    private static partial Regex ReceiverPattern();
#else

    private static readonly Regex CompiledReceiverPattern = new(ReceiverExpression, RegexOptions.Compiled);

    /// <summary>
    ///     Matches an extension block receiver and a classic extension method receiver, capturing the short type name.
    /// </summary>
    private static Regex ReceiverPattern()
    {
        return CompiledReceiverPattern;
    }
#endif
}
