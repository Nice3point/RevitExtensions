using Nice3point.Revit.Extensions.Tests.Abstractions;
using Nice3point.Revit.Extensions.Tests.Artifacts;
using Nice3point.Revit.Extensions.Tests.Coverage.Formatters;

namespace Nice3point.Revit.Extensions.Tests.Coverage;

public sealed class RevitApiUtilsCoverageTests : ApiCoverageTest
{
    /// <summary>
    ///     Attaches a Markdown table of every Revit API utility method and the library file wrapping it.
    /// </summary>
    [Test]
    public async Task CoversEveryStaticMethod()
    {
        var assembly = typeof(Document).Assembly;

        var rows = GetUtilityMethodRows(assembly);

        await Assert.That(rows).IsNotEmpty();

        // Unwrapped methods lead the table: the report doubles as the extension backlog.
        await rows
            .OrderBy(row => row.ImplementationFiles.Count is 0 ? 0 : 1)
            .ThenBy(row => row.QualifiedName, StringComparer.Ordinal)
            .ToMarkdownTable()
            .CreateMarkdownArtifactAsync($"{assembly.GetName().Name}-Utils-{Application.VersionNumber}");
    }
}
