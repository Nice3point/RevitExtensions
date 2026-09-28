using System.Windows.Media;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using Nice3point.Revit.Extensions.UI;
using Nice3point.TUnit.Revit;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;

namespace Nice3point.Revit.Extensions.Tests.Abstractions;

/// <summary>
///     Supplies ribbon tests with a controlled application and removes every panel a test creates.
/// </summary>
public abstract class RibbonUiTest : RevitApiUiTest
{
    private List<RibbonPanel> CreatedPanels => field ??= [];

    /// <summary>
    ///     The controlled application the ribbon extensions run against.
    /// </summary>
    private protected static UIControlledApplication ControlledApplication => field ??= UiApplication.AsControlledApplication();

    [After(Test)]
    public void RemoveCreatedPanels()
    {
        foreach (var panel in CreatedPanels)
        {
            panel.RemovePanel();
        }
    }

    /// <summary>
    ///     Returns a name no other test uses, for a panel or a tab.
    /// </summary>
    private protected static string CreateUniqueName()
    {
        return Guid.NewGuid().ToString("N")[..12];
    }

    /// <summary>
    ///     Creates a panel in a new tab and removes it after the test.
    /// </summary>
    private protected RibbonPanel CreateTestPanel()
    {
        var panel = ControlledApplication.CreatePanel(CreateUniqueName(), CreateUniqueName());
        CreatedPanels.Add(panel);

        return panel;
    }

    /// <summary>
    ///     Removes the panel after the test.
    /// </summary>
    private protected void RemoveAfterTest(RibbonPanel panel)
    {
        CreatedPanels.Add(panel);
    }

    /// <summary>
    ///     Writes a blank 16x16 PNG image to the named directory under the temporary folder.
    /// </summary>
    private protected static string CreateImageFile(string directoryName, string fileName)
    {
        var directory = Path.Combine(Path.GetTempPath(), directoryName);
        Directory.CreateDirectory(directory);

        const int size = 16;
        var pixels = new byte[size * size * 4];
        var bitmap = BitmapSource.Create(size, size, 96, 96, PixelFormats.Bgra32, null, pixels, size * 4);
        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmap));

        var path = Path.Combine(directory, fileName);
        using var stream = File.Create(path);
        encoder.Save(stream);

        return path;
    }

    /// <summary>
    ///     Finds the tab the ribbon shows by its identifier.
    /// </summary>
    private protected static RibbonTab? FindRibbonTab(string tabId)
    {
        return ComponentManager.Ribbon.Tabs.FirstOrDefault(tab => tab.Id == tabId);
    }

    /// <summary>
    ///     Finds the panel the ribbon shows under the specified title.
    /// </summary>
    private protected static Autodesk.Windows.RibbonPanel FindRibbonPanel(string panelTitle)
    {
        return ComponentManager.Ribbon.Tabs
            .SelectMany(tab => tab.Panels)
            .Single(panel => panel.Source.Title == panelTitle);
    }
}
