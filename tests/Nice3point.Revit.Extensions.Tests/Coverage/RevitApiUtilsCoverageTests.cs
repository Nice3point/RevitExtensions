using Nice3point.Revit.Extensions.Tests.Abstractions;
using Nice3point.Revit.Extensions.Tests.Artifacts;
using Nice3point.Revit.Extensions.Tests.Coverage.Formatters;

namespace Nice3point.Revit.Extensions.Tests.Coverage;

/// <summary>
///     Reports the Revit API utility methods and the library files wrapping them.
/// </summary>
public sealed class RevitApiUtilsCoverageTests : ApiCoverageTest
{
    /// <summary>
    ///     Attaches a Markdown table of every Revit API utility method and the library file wrapping it.
    /// </summary>
    [Test]
    public async Task CoversEveryStaticMethod()
    {
        // Arrange
        var assembly = typeof(Document).Assembly;

        // Act
        var rows = GetUtilityMethodRows(assembly);

        // Assert
        await Assert.That(rows).IsNotEmpty();

        await rows
            .OrderBy(row => row.ImplementationFiles.Count is 0 ? 0 : 1)
            .ThenBy(row => row.QualifiedName, StringComparer.Ordinal)
            .ToMarkdownTable()
            .CreateMarkdownArtifactAsync($"{assembly.GetName().Name}-utils-{Application.VersionNumber}");
    }
}
