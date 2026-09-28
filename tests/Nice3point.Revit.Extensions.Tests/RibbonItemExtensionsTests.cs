using System.Windows.Media;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using Nice3point.Revit.Extensions.Tests.Abstractions;
using Nice3point.Revit.Extensions.Tests.Commands;
using Nice3point.Revit.Extensions.UI;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class RibbonItemExtensionsTests : RibbonUiTest
{
    [Test]
    public async Task AddPushButton_GenericCommand_AddsButtonForTheCommand()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var button = panel.AddPushButton<EmptyCommand>("Run");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(button.ItemText).IsEqualTo("Run");
            await Assert.That(button.ClassName).IsEqualTo(typeof(EmptyCommand).FullName);
            await Assert.That(button.AssemblyName).IsEqualTo(typeof(EmptyCommand).Assembly.Location);
            await Assert.That(panel.GetItems().Select(item => item.Name)).IsEquivalentTo([button.Name]);
        }
    }

    [Test]
    public async Task AddPushButton_CommandType_AddsButtonForTheCommand()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var button = panel.AddPushButton(typeof(EmptyCommand), "Run");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(button.ItemText).IsEqualTo("Run");
            await Assert.That(button.ClassName).IsEqualTo(typeof(EmptyCommand).FullName);
            await Assert.That(button.AssemblyName).IsEqualTo(typeof(EmptyCommand).Assembly.Location);
            await Assert.That(panel.GetItems().Select(item => item.Name)).IsEquivalentTo([button.Name]);
        }
    }

    [Test]
    public async Task AddPushButton_CommandAlreadyInPanel_ThrowsArgumentException()
    {
        // Arrange
        var panel = CreateTestPanel();
        panel.AddPushButton<EmptyCommand>("Run");

        // Act & Assert
        await Assert.That(() => panel.AddPushButton<EmptyCommand>("Run again")).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }

    [Test]
    public async Task AddPullDownButton_ButtonText_AddsButtonsWithUniqueNames()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var firstButton = panel.AddPullDownButton("Tools");
        var secondButton = panel.AddPullDownButton("Tools");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(firstButton.ItemText).IsEqualTo("Tools");
            await Assert.That(secondButton.ItemText).IsEqualTo("Tools");
            await Assert.That(firstButton.Name).IsNotEqualTo(secondButton.Name);
            await Assert.That(panel.GetItems().Count).IsEqualTo(2);
        }
    }

    [Test]
    public async Task AddPullDownButton_InternalName_AddsButtonWithTheName()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var button = panel.AddPullDownButton("ToolsPullDown", "Tools");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(button.Name).IsEqualTo("ToolsPullDown");
            await Assert.That(button.ItemText).IsEqualTo("Tools");
        }
    }

    [Test]
    public async Task AddPullDownButton_InternalNameAlreadyInPanel_ThrowsArgumentException()
    {
        // Arrange
        var panel = CreateTestPanel();
        panel.AddPullDownButton("ToolsPullDown", "Tools");

        // Act & Assert
        await Assert.That(() => panel.AddPullDownButton("ToolsPullDown", "Tools")).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }

    [Test]
    public async Task AddSplitButton_ButtonText_AddsButtonsWithUniqueNames()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var firstButton = panel.AddSplitButton("Tools");
        var secondButton = panel.AddSplitButton("Tools");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(firstButton.ItemText).IsEqualTo("Tools");
            await Assert.That(secondButton.ItemText).IsEqualTo("Tools");
            await Assert.That(firstButton.Name).IsNotEqualTo(secondButton.Name);
            await Assert.That(panel.GetItems().Count).IsEqualTo(2);
        }
    }

    [Test]
    public async Task AddSplitButton_InternalName_AddsButtonWithTheName()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var button = panel.AddSplitButton("ToolsSplit", "Tools");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(button.Name).IsEqualTo("ToolsSplit");
            await Assert.That(button.ItemText).IsEqualTo("Tools");
        }
    }

    [Test]
    public async Task AddSplitButton_InternalNameAlreadyInPanel_ThrowsArgumentException()
    {
        // Arrange
        var panel = CreateTestPanel();
        panel.AddSplitButton("ToolsSplit", "Tools");

        // Act & Assert
        await Assert.That(() => panel.AddSplitButton("ToolsSplit", "Tools")).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }

    [Test]
    public async Task AddRadioButtonGroup_WithoutName_AddsGroupsWithUniqueNames()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var firstGroup = panel.AddRadioButtonGroup();
        var secondGroup = panel.AddRadioButtonGroup();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(firstGroup.Name).IsNotEqualTo(secondGroup.Name);
            await Assert.That(panel.GetItems().Count).IsEqualTo(2);
        }
    }

    [Test]
    public async Task AddRadioButtonGroup_InternalName_AddsGroupWithTheName()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var group = panel.AddRadioButtonGroup("Modes");

        // Assert
        await Assert.That(group.Name).IsEqualTo("Modes");
    }

    [Test]
    public async Task AddRadioButtonGroup_InternalNameAlreadyInPanel_ThrowsArgumentException()
    {
        // Arrange
        var panel = CreateTestPanel();
        panel.AddRadioButtonGroup("Modes");

        // Act & Assert
        await Assert.That(() => panel.AddRadioButtonGroup("Modes")).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }

    [Test]
    public async Task AddComboBox_WithoutName_AddsComboBoxesWithUniqueNames()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var firstComboBox = panel.AddComboBox();
        var secondComboBox = panel.AddComboBox();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(firstComboBox.Name).IsNotEqualTo(secondComboBox.Name);
            await Assert.That(panel.GetItems().Count).IsEqualTo(2);
        }
    }

    [Test]
    public async Task AddComboBox_InternalName_AddsComboBoxWithTheName()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var comboBox = panel.AddComboBox("Levels");

        // Assert
        await Assert.That(comboBox.Name).IsEqualTo("Levels");
    }

    [Test]
    public async Task AddComboBox_InternalNameAlreadyInPanel_ThrowsArgumentException()
    {
        // Arrange
        var panel = CreateTestPanel();
        panel.AddComboBox("Levels");

        // Act & Assert
        await Assert.That(() => panel.AddComboBox("Levels")).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }

    [Test]
    public async Task AddTextBox_WithoutName_AddsTextBoxesWithUniqueNames()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var firstTextBox = panel.AddTextBox();
        var secondTextBox = panel.AddTextBox();

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(firstTextBox.Name).IsNotEqualTo(secondTextBox.Name);
            await Assert.That(panel.GetItems().Count).IsEqualTo(2);
        }
    }

    [Test]
    public async Task AddTextBox_InternalName_AddsTextBoxWithTheName()
    {
        // Arrange
        var panel = CreateTestPanel();

        // Act
        var textBox = panel.AddTextBox("Search");

        // Assert
        await Assert.That(textBox.Name).IsEqualTo("Search");
    }

    [Test]
    public async Task AddTextBox_InternalNameAlreadyInPanel_ThrowsArgumentException()
    {
        // Arrange
        var panel = CreateTestPanel();
        panel.AddTextBox("Search");

        // Act & Assert
        await Assert.That(() => panel.AddTextBox("Search")).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }

    [Test]
    public async Task AddPushButton_GenericCommandInPullDown_AddsButtonToThePullDown()
    {
        // Arrange
        var pullDownButton = CreateTestPanel().AddPullDownButton("Tools");

        // Act
        var button = pullDownButton.AddPushButton<EmptyCommand>("Run");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(button.ItemText).IsEqualTo("Run");
            await Assert.That(button.ClassName).IsEqualTo(typeof(EmptyCommand).FullName);
            await Assert.That(button.AssemblyName).IsEqualTo(typeof(EmptyCommand).Assembly.Location);
            await Assert.That(pullDownButton.GetItems().Select(item => item.Name)).IsEquivalentTo([button.Name]);
        }
    }

    [Test]
    public async Task AddPushButton_CommandTypeInPullDown_AddsButtonToThePullDown()
    {
        // Arrange
        var pullDownButton = CreateTestPanel().AddPullDownButton("Tools");

        // Act
        var button = pullDownButton.AddPushButton(typeof(EmptyCommand), "Run");

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(button.ItemText).IsEqualTo("Run");
            await Assert.That(button.ClassName).IsEqualTo(typeof(EmptyCommand).FullName);
            await Assert.That(button.AssemblyName).IsEqualTo(typeof(EmptyCommand).Assembly.Location);
            await Assert.That(pullDownButton.GetItems().Select(item => item.Name)).IsEquivalentTo([button.Name]);
        }
    }

    [Test]
    public async Task AddPushButton_CommandAlreadyInPullDown_ThrowsArgumentException()
    {
        // Arrange
        var pullDownButton = CreateTestPanel().AddPullDownButton("Tools");
        pullDownButton.AddPushButton<EmptyCommand>("Run");

        // Act & Assert
        await Assert.That(() => pullDownButton.AddPushButton<EmptyCommand>("Run again")).Throws<Autodesk.Revit.Exceptions.ArgumentException>();
    }

    [Test]
    public async Task SetAvailabilityController_ControllerType_SetsTheControllerClassName()
    {
        // Arrange
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");

        // Act
        button.SetAvailabilityController<AlwaysAvailableController>();

        // Assert
        await Assert.That(button.AvailabilityClassName).IsEqualTo(typeof(AlwaysAvailableController).FullName);
    }

    [Test]
    public async Task SetImage_UnthemedUri_SetsTheImage()
    {
        // Arrange
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        var imagePath = CreateImageFile(CreateUniqueName(), "RibbonIcon16.png");

        // Act
        button.SetImage(imagePath);

        // Assert
        var image = await Assert.That(button.Image).IsTypeOf<BitmapImage>();
        await Assert.That(image!.UriSource.OriginalString).IsEqualTo(imagePath);
    }

    [Test]
    public async Task SetLargeImage_UnthemedUri_SetsTheLargeImage()
    {
        // Arrange
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        var imagePath = CreateImageFile(CreateUniqueName(), "RibbonIcon32.png");

        // Act
        button.SetLargeImage(imagePath);

        // Assert
        var image = await Assert.That(button.LargeImage).IsTypeOf<BitmapImage>();
        await Assert.That(image!.UriSource.OriginalString).IsEqualTo(imagePath);
    }

#if REVIT2024_OR_GREATER
    [Test]
    [Arguments("RibbonIcon16Light.png")]
    [Arguments("RibbonIcon16Dark.png")]
    public async Task SetImage_ThemedUri_SetsTheImageOfTheCurrentTheme(string fileName)
    {
        // Arrange
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        var directory = CreateUniqueName();
        var lightImagePath = CreateImageFile(directory, "RibbonIcon16Light.png");
        var darkImagePath = CreateImageFile(directory, "RibbonIcon16Dark.png");
        var expectedPath = UIThemeManager.CurrentTheme == UITheme.Dark ? darkImagePath : lightImagePath;

        // Act
        button.SetImage(Path.Combine(Path.GetDirectoryName(lightImagePath)!, fileName));

        // Assert
        var image = await Assert.That(button.Image).IsTypeOf<BitmapImage>();
        await Assert.That(image!.UriSource.OriginalString).IsEqualTo(expectedPath, StringComparison.OrdinalIgnoreCase);
    }

    [Test]
    [Arguments("RibbonIcon32Light.png")]
    [Arguments("RibbonIcon32Dark.png")]
    public async Task SetLargeImage_ThemedUri_SetsTheLargeImageOfTheCurrentTheme(string fileName)
    {
        // Arrange
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        var directory = CreateUniqueName();
        var lightImagePath = CreateImageFile(directory, "RibbonIcon32Light.png");
        var darkImagePath = CreateImageFile(directory, "RibbonIcon32Dark.png");
        var expectedPath = UIThemeManager.CurrentTheme == UITheme.Dark ? darkImagePath : lightImagePath;

        // Act
        button.SetLargeImage(Path.Combine(Path.GetDirectoryName(lightImagePath)!, fileName));

        // Assert
        var image = await Assert.That(button.LargeImage).IsTypeOf<BitmapImage>();
        await Assert.That(image!.UriSource.OriginalString).IsEqualTo(expectedPath, StringComparison.OrdinalIgnoreCase);
    }
#endif
}
