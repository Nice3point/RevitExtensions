using Autodesk.Windows;
using Nice3point.Revit.Extensions.Tests.Abstractions;
using Nice3point.Revit.Extensions.Tests.Commands;
using Nice3point.Revit.Extensions.UI;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class RibbonStackPanelTests : RibbonUiTest
{
    [Test]
    [Arguments(1, 1)]
    [Arguments(2, 1)]
    [Arguments(3, 1)]
    [Arguments(4, 2)]
    [Arguments(6, 2)]
    [Arguments(7, 3)]
    public async Task AddStackPanel_ItemsCount_StacksUpToThreeItemsPerColumn(int itemsCount, int expectedColumnsCount)
    {
        // Arrange
        var panel = CreateTestPanel();
        var stackPanel = panel.AddStackPanel();

        // Act
        for (var i = 0; i < itemsCount; i++)
        {
            stackPanel.AddPullDownButton($"Tools {i}");
        }

        // Assert
        var columns = FindRibbonPanel(panel.Name).Source.Items.OfType<RibbonRowPanel>().ToList();
        var columnItemsCounts = columns.Select(column => column.Items.Count(item => item is not RibbonRowBreak)).ToList();
        using (Assert.Multiple())
        {
            await Assert.That(columns.Count).IsEqualTo(expectedColumnsCount);
            await Assert.That(columnItemsCounts.Sum()).IsEqualTo(itemsCount);
            await Assert.That(columnItemsCounts.Max()).IsLessThanOrEqualTo(3);
        }
    }

    [Test]
    [Arguments(2)]
    [Arguments(3)]
    public async Task AddStackPanel_StackedItems_SeparatesItemsWithRowBreaks(int itemsCount)
    {
        // Arrange
        var panel = CreateTestPanel();
        var stackPanel = panel.AddStackPanel();

        // Act
        for (var i = 0; i < itemsCount; i++)
        {
            stackPanel.AddTextBox();
        }

        // Assert
        var column = FindRibbonPanel(panel.Name).Source.Items.OfType<RibbonRowPanel>().Single();
        var rowBreakPositions = column.Items.Select(item => item is RibbonRowBreak).ToList();
        var expectedPositions = Enumerable.Range(0, itemsCount * 2 - 1).Select(index => index % 2 == 1).ToList();
        await Assert.That(rowBreakPositions.SequenceEqual(expectedPositions)).IsTrue();
    }

    [Test]
    public async Task AddLabel_StackPanel_AddsLabelBetweenTheItems()
    {
        // Arrange
        var panel = CreateTestPanel();
        var stackPanel = panel.AddStackPanel();

        // Act
        stackPanel.AddPushButton<EmptyCommand>("Run");
        stackPanel.AddLabel("Label");
        stackPanel.AddComboBox();

        // Assert
        var column = FindRibbonPanel(panel.Name).Source.Items.OfType<RibbonRowPanel>().Single();
        var items = column.Items.Where(item => item is not RibbonRowBreak).ToList();
        using (Assert.Multiple())
        {
            await Assert.That(items.Count).IsEqualTo(3);
            await Assert.That(items[1]).IsTypeOf<RibbonLabel>();
            await Assert.That((items[1] as RibbonLabel)?.Text).IsEqualTo("Label");
        }
    }

    [Test]
    public async Task AddStackPanel_TwoStackPanels_CreatesSeparateColumns()
    {
        // Arrange
        var panel = CreateTestPanel();
        var firstStackPanel = panel.AddStackPanel();
        var secondStackPanel = panel.AddStackPanel();

        // Act
        firstStackPanel.AddTextBox();
        secondStackPanel.AddTextBox();

        // Assert
        var columns = FindRibbonPanel(panel.Name).Source.Items.OfType<RibbonRowPanel>().ToList();
        await Assert.That(columns.Count).IsEqualTo(2);
    }

    [Test]
    public async Task AddPushButton_StackPanel_AddsButtonForTheCommand()
    {
        // Arrange
        var panel = CreateTestPanel();
        var stackPanel = panel.AddStackPanel();

        // Act
        var button = stackPanel.AddPushButton<EmptyCommand>("Run");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(button.ItemText).IsEqualTo("Run");
            await Assert.That(button.ClassName).IsEqualTo(typeof(EmptyCommand).FullName);
            await Assert.That(button.AssemblyName).IsEqualTo(typeof(EmptyCommand).Assembly.Location);
            await Assert.That(panel.GetItems().Select(item => item.Name)).Contains(button.Name);
        }
    }

    [Test]
    public async Task AddPushButton_CommandAlreadyInStackPanel_ThrowsArgumentException()
    {
        // Arrange
        var stackPanel = CreateTestPanel().AddStackPanel();
        stackPanel.AddPushButton<EmptyCommand>("Run");

        // Act & Assert
        await Assert.That(() => stackPanel.AddPushButton<EmptyCommand>("Run again")).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }

    [Test]
    public async Task AddPullDownButton_StackPanelSameText_AddsButtonsWithUniqueNames()
    {
        // Arrange
        var stackPanel = CreateTestPanel().AddStackPanel();

        // Act
        var firstButton = stackPanel.AddPullDownButton("Tools");
        var secondButton = stackPanel.AddPullDownButton("Tools");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(firstButton.ItemText).IsEqualTo("Tools");
            await Assert.That(firstButton.Name).IsNotEqualTo(secondButton.Name);
        }
    }

    [Test]
    public async Task AddPullDownButton_StackPanelInternalName_AddsButtonWithTheName()
    {
        // Arrange
        var stackPanel = CreateTestPanel().AddStackPanel();

        // Act
        var button = stackPanel.AddPullDownButton("Tools", "ToolsPullDown");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(button.Name).IsEqualTo("ToolsPullDown");
            await Assert.That(button.ItemText).IsEqualTo("Tools");
        }
    }

    [Test]
    public async Task AddSplitButton_StackPanelInternalName_AddsButtonWithTheName()
    {
        // Arrange
        var stackPanel = CreateTestPanel().AddStackPanel();

        // Act
        var button = stackPanel.AddSplitButton("Tools", "ToolsSplit");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(button.Name).IsEqualTo("ToolsSplit");
            await Assert.That(button.ItemText).IsEqualTo("Tools");
        }
    }

    [Test]
    public async Task AddComboBox_StackPanelInternalName_AddsComboBoxWithTheName()
    {
        // Arrange
        var stackPanel = CreateTestPanel().AddStackPanel();

        // Act
        var comboBox = stackPanel.AddComboBox("Levels");

        // Assert
        await Assert.That(comboBox.Name).IsEqualTo("Levels");
    }

    [Test]
    public async Task AddTextBox_StackPanelInternalName_AddsTextBoxWithTheName()
    {
        // Arrange
        var stackPanel = CreateTestPanel().AddStackPanel();

        // Act
        var textBox = stackPanel.AddTextBox("Search");

        // Assert
        await Assert.That(textBox.Name).IsEqualTo("Search");
    }

    [Test]
    public async Task AddTextBox_StackPanelInternalNameAlreadyInPanel_ThrowsArgumentException()
    {
        // Arrange
        var stackPanel = CreateTestPanel().AddStackPanel();
        stackPanel.AddTextBox("Search");

        // Act & Assert
        await Assert.That(() => stackPanel.AddTextBox("Search")).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }
}
