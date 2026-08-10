using Nice3point.Revit.Extensions.Tests.Abstractions;
using Nice3point.Revit.Extensions.Tests.Artifacts;
using Nice3point.Revit.Extensions.Tests.Coverage.Formatters;

namespace Nice3point.Revit.Extensions.Tests.Coverage;

/// <summary>
///     Reports the Revit API collections whose iterator carries the key of the current entry.
/// </summary>
public sealed class RevitApiMapsCoverageTests : ApiCoverageTest
{
    /// <summary>
    ///     Attaches a Markdown table of every Revit API map, the key and value a <c>foreach</c> never pairs up, and the library file extending it.
    /// </summary>
    [Test]
    public async Task CoversEveryMap()
    {
        // Arrange
        var assembly = typeof(Document).Assembly;

        // Act
        var rows = GetMapRows(assembly);

        // Assert
        await Assert.That(rows).IsNotEmpty();

        await rows
            .OrderBy(row => row.ImplementationFiles.Count is 0 ? 0 : 1)
            .ThenBy(row => row.TypeName, StringComparer.Ordinal)
            .ToMarkdownTable()
            .CreateMarkdownArtifactAsync($"{assembly.GetName().Name}-maps-{Application.VersionNumber}");
    }
}
