using Nice3point.TUnit.Revit;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class ApplicationExtensionsTests : RevitApiTest
{
    [Test]
    public async Task AsControlledApplication_ValidApplication_ReturnsNotNull()
    {
        // Act
        var controlledApplication = Application.AsControlledApplication();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(controlledApplication).IsNotNull();
            await Assert.That(controlledApplication.VersionBuild).IsNotNullOrEmpty();
        }
    }
}