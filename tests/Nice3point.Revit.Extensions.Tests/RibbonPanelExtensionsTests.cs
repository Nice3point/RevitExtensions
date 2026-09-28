using System.Windows.Media;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using Nice3point.Revit.Extensions.Tests.Abstractions;
using Nice3point.Revit.Extensions.UI;
using Color = System.Windows.Media.Color;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class RibbonPanelExtensionsTests : RibbonUiTest
{
    private const string BuiltInTabTitle = "Manage";

    [Test]
    public async Task CreatePanel_NewPanelName_AddsPanelToAddInsTab()
    {
        // Arrange
        var panelName = CreateUniqueName();

        // Act
        var panel = ControlledApplication.CreatePanel(panelName);
        RemoveAfterTest(panel);

        // Assert
        var panelNames = ControlledApplication.GetRibbonPanels(Tab.AddIns).Select(ribbonPanel => ribbonPanel.Name);
        await Assert.That(panelNames).Contains(panelName);
    }

    [Test]
    public async Task CreatePanel_EmptyPanelName_ThrowsArgumentException()
    {
        // Act & Assert
        await Assert.That(() => ControlledApplication.CreatePanel(string.Empty)).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }

    [Test]
    public async Task CreatePanel_EmptyPanelNameInTab_ThrowsArgumentException()
    {
        // Act & Assert
        await Assert.That(() => ControlledApplication.CreatePanel(string.Empty, CreateUniqueName())).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }

    [Test]
    public async Task CreatePanel_EmptyTabName_ThrowsArgumentException()
    {
        // Arrange
        var panelName = CreateUniqueName();

        // Act & Assert
        using (Assert.Multiple())
        {
            await Assert.That(() => ControlledApplication.CreatePanel(panelName, string.Empty)).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
            await Assert.That(ComponentManager.Ribbon.Tabs.SelectMany(tab => tab.Panels).Select(ribbonPanel => ribbonPanel.Source.Title)).DoesNotContain(panelName);
        }
    }

    [Test]
    public async Task CreatePanel_ExistingAddInsPanelName_ReturnsExistingPanel()
    {
        // Arrange
        var panelName = CreateUniqueName();
        var panel = ControlledApplication.CreatePanel(panelName);
        RemoveAfterTest(panel);

        // Act
        var existingPanel = ControlledApplication.CreatePanel(panelName);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(existingPanel.Name).IsEqualTo(panelName);
            await Assert.That(ControlledApplication.GetRibbonPanels(Tab.AddIns).Count(ribbonPanel => ribbonPanel.Name == panelName)).IsEqualTo(1);
        }
    }

    [Test]
    public async Task CreatePanel_NewTabName_CreatesTabWithThePanel()
    {
        // Arrange
        var panelName = CreateUniqueName();
        var tabName = CreateUniqueName();

        // Act
        var panel = ControlledApplication.CreatePanel(panelName, tabName);
        RemoveAfterTest(panel);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(FindRibbonTab(tabName)).IsNotNull();
            await Assert.That(ControlledApplication.GetRibbonPanels(tabName).Select(ribbonPanel => ribbonPanel.Name)).IsEquivalentTo([panelName]);
        }
    }

    [Test]
    public async Task CreatePanel_ExistingPanelInTab_ReturnsTheSamePanel()
    {
        // Arrange
        var panelName = CreateUniqueName();
        var tabName = CreateUniqueName();
        var panel = ControlledApplication.CreatePanel(panelName, tabName);
        RemoveAfterTest(panel);

        // Act
        var existingPanel = ControlledApplication.CreatePanel(panelName, tabName);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(existingPanel).IsSameReferenceAs(panel);
            await Assert.That(FindRibbonTab(tabName)!.Panels.Count).IsEqualTo(1);
        }
    }

    [Test]
    public async Task CreatePanel_SecondPanelInTab_AddsPanelToTheSameTab()
    {
        // Arrange
        var firstPanelName = CreateUniqueName();
        var secondPanelName = CreateUniqueName();
        var tabName = CreateUniqueName();
        RemoveAfterTest(ControlledApplication.CreatePanel(firstPanelName, tabName));

        // Act
        var secondPanel = ControlledApplication.CreatePanel(secondPanelName, tabName);
        RemoveAfterTest(secondPanel);

        // Assert
        var panelTitles = FindRibbonTab(tabName)!.Panels.Select(ribbonPanel => ribbonPanel.Source.Title);
        await Assert.That(panelTitles).IsEquivalentTo([firstPanelName, secondPanelName]);
    }

    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task CreatePanel_BuiltInTab_AddsPanelToTheBuiltInTab(bool useTabId)
    {
        // Arrange
        var panelName = CreateUniqueName();
        var builtInTab = FindBuiltInTab();
        var tabsCount = ComponentManager.Ribbon.Tabs.Count;
        var tabName = useTabId ? builtInTab.Id : builtInTab.Title;

        // Act
        var panel = ControlledApplication.CreatePanel(panelName, tabName);
        RemoveAfterTest(panel);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(panel.Name).IsEqualTo(panelName);
            await Assert.That(builtInTab.Panels.Select(ribbonPanel => ribbonPanel.Source.Title)).Contains(panelName);
            await Assert.That(ComponentManager.Ribbon.Tabs.Count).IsEqualTo(tabsCount);
        }
    }

    [Test]
    public async Task CreatePanel_ExistingPanelInBuiltInTab_ReturnsTheSamePanel()
    {
        // Arrange
        var panelName = CreateUniqueName();
        var builtInTab = FindBuiltInTab();
        var panel = ControlledApplication.CreatePanel(panelName, builtInTab.Id);
        RemoveAfterTest(panel);

        // Act
        var existingPanel = ControlledApplication.CreatePanel(panelName, builtInTab.Id);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(existingPanel).IsSameReferenceAs(panel);
            await Assert.That(builtInTab.Panels.Count(ribbonPanel => ribbonPanel.Source.Title == panelName)).IsEqualTo(1);
        }
    }

    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task CreatePanel_ExistingPanelInBuiltInTabWithDistinctTitle_ReturnsTheSamePanel(bool useTabId)
    {
        // Arrange
        var panelName = CreateUniqueName();
        var builtInTab = ComponentManager.Ribbon.Tabs.FirstOrDefault(tab => tab.IsVisible && tab.Panels.Count > 0 && tab.Id != tab.Title);
        if (builtInTab is null)
        {
            Skip.Test("The ribbon has no visible tab with a title distinct from its identifier.");
        }

        var tabName = useTabId ? builtInTab!.Id : builtInTab!.Title;
        var panel = ControlledApplication.CreatePanel(panelName, tabName);
        RemoveAfterTest(panel);

        // Act
        var existingPanel = ControlledApplication.CreatePanel(panelName, tabName);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(existingPanel).IsSameReferenceAs(panel);
            await Assert.That(builtInTab.Panels.Count(ribbonPanel => ribbonPanel.Source.Title == panelName)).IsEqualTo(1);
        }
    }

    [Test]
    public async Task RemovePanel_LastPanelInTab_KeepsTheEmptyTab()
    {
        // Arrange
        var tabName = CreateUniqueName();
        var panel = ControlledApplication.CreatePanel(CreateUniqueName(), tabName);

        // Act
        panel.RemovePanel();

        // Assert
        await Assert.That(FindRibbonTab(tabName)?.Panels.Count).IsEqualTo(0);
    }

    [Test]
    public async Task RemovePanel_OneOfTwoPanelsInTab_KeepsTheOtherPanel()
    {
        // Arrange
        var tabName = CreateUniqueName();
        var keptPanelName = CreateUniqueName();
        RemoveAfterTest(ControlledApplication.CreatePanel(keptPanelName, tabName));
        var removedPanel = ControlledApplication.CreatePanel(CreateUniqueName(), tabName);

        // Act
        removedPanel.RemovePanel();

        // Assert
        var panelTitles = FindRibbonTab(tabName)!.Panels.Select(ribbonPanel => ribbonPanel.Source.Title);
        await Assert.That(panelTitles).IsEquivalentTo([keptPanelName]);
    }

    [Test]
    public async Task RemovePanel_AddInsPanel_RemovesPanelFromAddInsTab()
    {
        // Arrange
        var panelName = CreateUniqueName();
        var panel = ControlledApplication.CreatePanel(panelName);

        // Act
        panel.RemovePanel();

        // Assert
        var panelTitles = ComponentManager.Ribbon.Tabs
            .SelectMany(tab => tab.Panels)
            .Select(ribbonPanel => ribbonPanel.Source.Title);

        await Assert.That(panelTitles).DoesNotContain(panelName);
    }

    [Test]
    [Arguments(true)]
    [Arguments(false)]
    public async Task RemovePanel_AlreadyRemovedPanel_DoesNothing(bool lastPanelInTab)
    {
        // Arrange
        var tabName = CreateUniqueName();
        var keptPanelName = CreateUniqueName();
        if (!lastPanelInTab)
        {
            RemoveAfterTest(ControlledApplication.CreatePanel(keptPanelName, tabName));
        }

        var removedPanel = ControlledApplication.CreatePanel(CreateUniqueName(), tabName);
        removedPanel.RemovePanel();

        // Act & Assert
        await Assert.That(() => removedPanel.RemovePanel()).ThrowsNothing();

        var ribbonTab = FindRibbonTab(tabName);
        if (lastPanelInTab)
        {
            await Assert.That(ribbonTab?.Panels.Count).IsEqualTo(0);
        }
        else
        {
            await Assert.That(ribbonTab!.Panels.Select(ribbonPanel => ribbonPanel.Source.Title)).IsEquivalentTo([keptPanelName]);
        }
    }

    [Test]
    public async Task CreatePanel_AddInsTabOfRemovedLastPanel_AddsPanelToTheTab()
    {
        // Arrange
        var removedPanel = ControlledApplication.CreatePanel(CreateUniqueName());
        var removedInternalPanel = FindRibbonPanel(removedPanel.Name);
        var addInsTab = removedInternalPanel.Tab;
        var otherPanels = addInsTab.Panels.Where(ribbonPanel => !ReferenceEquals(ribbonPanel, removedInternalPanel)).ToList();
        foreach (var otherPanel in otherPanels)
        {
            addInsTab.Panels.Remove(otherPanel);
        }

        try
        {
            removedPanel.RemovePanel();

            // Act
            var panelName = CreateUniqueName();
            var panel = ControlledApplication.CreatePanel(panelName);
            RemoveAfterTest(panel);

            // Assert
            using (Assert.Multiple())
            {
                await Assert.That(ControlledApplication.GetRibbonPanels(Tab.AddIns).Select(ribbonPanel => ribbonPanel.Name)).Contains(panelName);
                await Assert.That(addInsTab.Panels.Select(ribbonPanel => ribbonPanel.Source.Title)).Contains(panelName);
            }
        }
        finally
        {
            foreach (var otherPanel in otherPanels)
            {
                addInsTab.Panels.Add(otherPanel);
            }
        }
    }

    [Test]
    public async Task RemovePanel_BuiltInTabPanel_KeepsTheBuiltInTab()
    {
        // Arrange
        var panelName = CreateUniqueName();
        var builtInTab = FindBuiltInTab();
        var panel = ControlledApplication.CreatePanel(panelName, builtInTab.Id);

        // Act
        panel.RemovePanel();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(builtInTab.Panels.Select(ribbonPanel => ribbonPanel.Source.Title)).DoesNotContain(panelName);
            await Assert.That(ComponentManager.Ribbon.Tabs.Any(tab => ReferenceEquals(tab, builtInTab))).IsTrue();
        }
    }

    [Test]
    public async Task CreatePanel_PanelNameOfRemovedPanel_CreatesTheNewPanel()
    {
        // Arrange
        var panelName = CreateUniqueName();
        var tabName = CreateUniqueName();
        RemoveAfterTest(ControlledApplication.CreatePanel(CreateUniqueName(), tabName));
        var removedPanel = ControlledApplication.CreatePanel(panelName, tabName);
        removedPanel.RemovePanel();

        // Act
        var panel = ControlledApplication.CreatePanel(panelName, tabName);
        RemoveAfterTest(panel);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(panel).IsNotSameReferenceAs(removedPanel);
            await Assert.That(FindRibbonTab(tabName)!.Panels.Select(ribbonPanel => ribbonPanel.Source.Title)).Contains(panelName);
        }
    }

    [Test]
    public async Task CreatePanel_PanelNameOfRemovedBuiltInTabPanel_CreatesTheNewPanel()
    {
        // Arrange
        var panelName = CreateUniqueName();
        var builtInTab = FindBuiltInTab();
        var removedPanel = ControlledApplication.CreatePanel(panelName, builtInTab.Id);
        removedPanel.RemovePanel();

        // Act
        var panel = ControlledApplication.CreatePanel(panelName, builtInTab.Id);
        RemoveAfterTest(panel);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(panel).IsNotSameReferenceAs(removedPanel);
            await Assert.That(builtInTab.Panels.Count(ribbonPanel => ribbonPanel.Source.Title == panelName)).IsEqualTo(1);
        }
    }

    [Test]
    public async Task CreatePanel_TabOfRemovedLastPanel_AddsPanelToTheTab()
    {
        // Arrange
        var panelName = CreateUniqueName();
        var tabName = CreateUniqueName();
        ControlledApplication.CreatePanel(panelName, tabName).RemovePanel();

        // Act
        var panel = ControlledApplication.CreatePanel(panelName, tabName);
        RemoveAfterTest(panel);

        // Assert
        var ribbonTab = FindRibbonTab(tabName);
        using (Assert.Multiple())
        {
            await Assert.That(ribbonTab).IsNotNull();
            await Assert.That(ribbonTab!.Panels.Select(ribbonPanel => ribbonPanel.Source.Title)).Contains(panelName);
        }
    }

    [Test]
    public async Task CreatePanel_TabOfRemovedLastPanel_KeepsTheTabPosition()
    {
        // Arrange
        var tabName = CreateUniqueName();
        var removedPanel = ControlledApplication.CreatePanel(CreateUniqueName(), tabName);
        RemoveAfterTest(ControlledApplication.CreatePanel(CreateUniqueName(), CreateUniqueName()));
        var tabIndex = ComponentManager.Ribbon.Tabs.IndexOf(FindRibbonTab(tabName)!);
        removedPanel.RemovePanel();

        // Act
        var panel = ControlledApplication.CreatePanel(CreateUniqueName(), tabName);
        RemoveAfterTest(panel);

        // Assert
        await Assert.That(ComponentManager.Ribbon.Tabs.IndexOf(FindRibbonTab(tabName)!)).IsEqualTo(tabIndex);
    }

    [Test]
    [Arguments("Red", "#FFFF0000")]
    [Arguments("#FF6669", "#FFFF6669")]
    [Arguments("#80FF6669", "#80FF6669")]
    public async Task SetBackground_ColorString_SetsPanelBackground(string color, string expectedColor)
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        panel.SetBackground(color);

        // Assert
        var background = FindRibbonPanel(panel.Name).CustomPanelBackground;
        var brush = await Assert.That(background).IsTypeOf<SolidColorBrush>();
        await Assert.That(brush!.Color.ToString()).IsEqualTo(expectedColor);
    }

    [Test]
    public async Task SetBackground_Color_SetsPanelBackground()
    {
        // Arrange
        var panel = CreateTestPanel();
        var color = Color.FromRgb(255, 102, 105);

        // Act
        panel.SetBackground(color);

        // Assert
        var background = FindRibbonPanel(panel.Name).CustomPanelBackground;
        var brush = await Assert.That(background).IsTypeOf<SolidColorBrush>();
        await Assert.That(brush!.Color).IsEqualTo(color);
    }

    [Test]
    public async Task SetBackground_Brush_SetsPanelBackground()
    {
        // Arrange
        var panel = CreateTestPanel();
        var brush = new LinearGradientBrush(Colors.Red, Colors.Black, 45);

        // Act
        panel.SetBackground(brush);

        // Assert
        await Assert.That(FindRibbonPanel(panel.Name).CustomPanelBackground).IsSameReferenceAs(brush);
    }

    [Test]
    [Arguments("Red", "#FFFF0000")]
    [Arguments("#FF6669", "#FFFF6669")]
    [Arguments("#80FF6669", "#80FF6669")]
    public async Task SetTitleBarBackground_ColorString_SetsTitleBarBackground(string color, string expectedColor)
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        panel.SetTitleBarBackground(color);

        // Assert
        var background = FindRibbonPanel(panel.Name).CustomPanelTitleBarBackground;
        var brush = await Assert.That(background).IsTypeOf<SolidColorBrush>();
        await Assert.That(brush!.Color.ToString()).IsEqualTo(expectedColor);
    }

    [Test]
    public async Task SetTitleBarBackground_Color_SetsTitleBarBackground()
    {
        // Arrange
        var panel = CreateTestPanel();
        var color = Color.FromRgb(255, 102, 105);

        // Act
        panel.SetTitleBarBackground(color);

        // Assert
        var background = FindRibbonPanel(panel.Name).CustomPanelTitleBarBackground;
        var brush = await Assert.That(background).IsTypeOf<SolidColorBrush>();
        await Assert.That(brush!.Color).IsEqualTo(color);
    }

    [Test]
    public async Task SetTitleBarBackground_Brush_SetsTitleBarBackground()
    {
        // Arrange
        var panel = CreateTestPanel();
        var brush = new LinearGradientBrush(Colors.Red, Colors.Black, 45);

        // Act
        panel.SetTitleBarBackground(brush);

        // Assert
        await Assert.That(FindRibbonPanel(panel.Name).CustomPanelTitleBarBackground).IsSameReferenceAs(brush);
    }

    [Test]
    public async Task CreatePanel_BuiltInTabTitleSharedByTabs_AddsPanelToTheVisibleTab()
    {
        // Arrange
        var panelName = CreateUniqueName();
        var builtInTab = FindBuiltInTab();

        // Act
        var panel = ControlledApplication.CreatePanel(panelName, BuiltInTabTitle);
        RemoveAfterTest(panel);

        // Assert
        var tabsWithPanel = ComponentManager.Ribbon.Tabs.Where(tab => tab.Panels.Any(ribbonPanel => ribbonPanel.Source.Title == panelName)).ToList();
        using (Assert.Multiple())
        {
            await Assert.That(tabsWithPanel.Count).IsEqualTo(1);
            await Assert.That(tabsWithPanel[0]).IsSameReferenceAs(builtInTab);
        }
    }

    [Test]
    [Arguments("Red", "#FFFF0000")]
    [Arguments("#FF6669", "#FFFF6669")]
    [Arguments("#80FF6669", "#80FF6669")]
    public async Task SetSlideOutPanelBackground_ColorString_SetsSlideOutPanelBackground(string color, string expectedColor)
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        panel.SetSlideOutPanelBackground(color);

        // Assert
        var background = FindRibbonPanel(panel.Name).CustomSlideOutPanelBackground;
        var brush = await Assert.That(background).IsTypeOf<SolidColorBrush>();
        await Assert.That(brush!.Color.ToString()).IsEqualTo(expectedColor);
    }

    [Test]
    public async Task SetSlideOutPanelBackground_Color_SetsSlideOutPanelBackground()
    {
        // Arrange
        var panel = CreateTestPanel();
        var color = Color.FromRgb(255, 102, 105);

        // Act
        panel.SetSlideOutPanelBackground(color);

        // Assert
        var background = FindRibbonPanel(panel.Name).CustomSlideOutPanelBackground;
        var brush = await Assert.That(background).IsTypeOf<SolidColorBrush>();
        await Assert.That(brush!.Color).IsEqualTo(color);
    }

    [Test]
    public async Task SetSlideOutPanelBackground_Brush_SetsSlideOutPanelBackground()
    {
        // Arrange
        var panel = CreateTestPanel();
        var brush = new LinearGradientBrush(Colors.Red, Colors.Black, 45);

        // Act
        panel.SetSlideOutPanelBackground(brush);

        // Assert
        await Assert.That(FindRibbonPanel(panel.Name).CustomSlideOutPanelBackground).IsSameReferenceAs(brush);
    }

    /// <summary>
    ///     Finds the built-in tab the user sees; Revit keeps more than one tab under this title.
    /// </summary>
    private static RibbonTab FindBuiltInTab()
    {
        return ComponentManager.Ribbon.Tabs.Single(tab => tab.Title == BuiltInTabTitle && tab.IsVisible);
    }
}
