#if REVIT2025_OR_GREATER
using Nice3point.Revit.Extensions.Structure;
using Nice3point.Revit.Extensions.Tests.Abstractions;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class RebarSpliceTypeUtilsExtensionsTests : RevitModelSampleTest
{
    [Test]
    [MethodDataSource(nameof(RevitModels))]
    public async Task NewRebarSpliceType_ValidName_ReturnsCreatedType(string path)
    {
        // Arrange
        const string typeName = "Rebar type";
        var document = ModelDocuments[path];

        // Act
        using var transaction = new Transaction(document);
        transaction.Start("Create rebar type");
        var elementType = document.Create.NewRebarSpliceType(typeName);
        transaction.Commit();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(elementType).IsNotNull();
            await Assert.That(elementType.Name).IsEqualTo(typeName);
        }
    }
}
#endif
