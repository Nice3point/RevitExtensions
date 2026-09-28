using Nice3point.Revit.Extensions.UI;
using Nice3point.TUnit.Revit;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class UiApplicationExtensionsTests : RevitApiUiTest
{
    [Test]
    public async Task AsControlledApplication_UiApplication_WrapsTheSameApplication()
    {
        // Act
        var controlledApplication = UiApplication.AsControlledApplication();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(controlledApplication.ControlledApplication.VersionBuild).IsEqualTo(UiApplication.Application.VersionBuild);
            await Assert.That(controlledApplication.ControlledApplication.Language).IsEqualTo(UiApplication.Application.Language);
            await Assert.That(controlledApplication.MainWindowHandle).IsEqualTo(UiApplication.MainWindowHandle);
        }
    }

    [Test]
    public async Task AsControlledApplication_UiApplication_ReadsTheSameRibbon()
    {
        // Act
        var controlledApplication = UiApplication.AsControlledApplication();

        // Assert
        var controlledPanels = controlledApplication.GetRibbonPanels().Select(panel => panel.Name);
        var applicationPanels = UiApplication.GetRibbonPanels().Select(panel => panel.Name);
        await Assert.That(controlledPanels).IsEquivalentTo(applicationPanels);
    }
}
