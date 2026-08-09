using Nice3point.Revit.Extensions.Tests.Abstractions;
using Nice3point.Revit.Extensions.Tests.Artifacts;
using Nice3point.Revit.Extensions.Tests.Coverage.Formatters;

namespace Nice3point.Revit.Extensions.Tests.Coverage;

/// <summary>
///     Reports the Revit API collections and the members each one leaves to an extension.
/// </summary>
public sealed class RevitApiCollectionsCoverageTests : ApiCoverageTest
{
    /// <summary>
    ///     Attaches a Markdown table of every Revit API collection, the members it leaves to an extension, and the library file extending it.
    /// </summary>
    [Test]
    public async Task CoversEveryCollection()
    {
        // Arrange
        var assembly = typeof(Document).Assembly;

        // Act
        var rows = GetCollectionRows(assembly);

        // Assert
        await Assert.That(rows).IsNotEmpty();

        await rows
            .OrderBy(row => row.Issues.Count is 0 ? 1 : 0)
            .ThenBy(row => row.ImplementationFiles.Count is 0 ? 0 : 1)
            .ThenBy(row => row.TypeName, StringComparer.Ordinal)
            .ToMarkdownTable()
            .CreateMarkdownArtifactAsync($"{assembly.GetName().Name}-collections-{Application.VersionNumber}");
    }
}
