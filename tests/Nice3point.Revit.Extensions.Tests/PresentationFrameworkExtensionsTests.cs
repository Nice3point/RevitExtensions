using System.Windows;
using System.Windows.Interop;
using Nice3point.Revit.Extensions.UI;
using Nice3point.TUnit.Revit;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class PresentationFrameworkExtensionsTests : RevitApiUiTest
{
    [Test]
    public async Task Show_RevitWindowHandle_ShowsWindowOwnedByRevit()
    {
        // Arrange
        var window = new Window
        {
            ShowActivated = false,
            ShowInTaskbar = false,
            Width = 1,
            Height = 1
        };

        try
        {
            // Act
            window.Show(UiApplication.MainWindowHandle);

            // Assert
            using (Assert.Multiple())
            {
                await Assert.That(window.IsVisible).IsTrue();
                await Assert.That(new WindowInteropHelper(window).Owner).IsEqualTo(UiApplication.MainWindowHandle);
            }
        }
        finally
        {
            window.Close();
        }
    }
}
