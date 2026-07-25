namespace Nice3point.Revit.Extensions.Tests.Coverage.Discovery;

/// <summary>
///     Searches the library sources for the files mentioning a <c>Type.Member</c> name.
/// </summary>
/// <remarks>
///     The match is textual and case sensitive. A name written in a comment or in documentation counts as a mention.
/// </remarks>
internal sealed class SourceFileIndex
{
    private readonly List<(string FileName, string Content)> _files;

    private SourceFileIndex(List<(string FileName, string Content)> files)
    {
        _files = files;
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

    /// <param name="qualifiedName">The <c>Type.Member</c> name to look up.</param>
    public IReadOnlyList<string> FindReferencingFiles(string qualifiedName)
    {
        return _files
            .Where(file => file.Content.Contains(qualifiedName, StringComparison.Ordinal))
            .Select(file => file.FileName)
            .ToList();
    }
}
