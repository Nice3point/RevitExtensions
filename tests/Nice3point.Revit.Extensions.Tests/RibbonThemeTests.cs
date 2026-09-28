#if REVIT2024_OR_GREATER
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Autodesk.Revit.UI;
using Nice3point.Revit.Extensions.Tests.Abstractions;
using Nice3point.Revit.Extensions.Tests.Commands;
using Nice3point.Revit.Extensions.UI;

namespace Nice3point.Revit.Extensions.Tests;

/// <summary>
///     Switches the Revit theme and restores the theme of the user after every test.
/// </summary>
public sealed class RibbonThemeTests : RibbonUiTest
{
    private bool _userUsesDarkTheme;
    private bool _userFollowsSystemTheme;

    [Before(Test)]
    public void SaveUserTheme()
    {
        _userUsesDarkTheme = UIThemeManager.CurrentTheme == UITheme.Dark;
        _userFollowsSystemTheme = UIThemeManager.FollowSystemColorTheme;
        UIThemeManager.FollowSystemColorTheme = false;
    }

    [After(Test)]
    public void RestoreUserTheme()
    {
        SwitchTheme(_userUsesDarkTheme ? UITheme.Dark : UITheme.Light);
        UIThemeManager.FollowSystemColorTheme = _userFollowsSystemTheme;
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task ThemeChanged_ThemedImages_SwitchesImagesToTheNewTheme(bool fromDarkTheme)
    {
        // Arrange
        var initialTheme = fromDarkTheme ? UITheme.Dark : UITheme.Light;
        var newTheme = Opposite(initialTheme);
        SwitchTheme(initialTheme);
        var directory = CreateUniqueName();
        var image = CreateThemedImageFiles(directory, "RibbonIcon16");
        var largeImage = CreateThemedImageFiles(directory, "RibbonIcon32");
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        button.SetImage(image.Light);
        button.SetLargeImage(largeImage.Light);

        // Act
        SwitchTheme(newTheme);

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(GetUriSource(button.Image)).IsEqualTo(image.For(newTheme), StringComparison.OrdinalIgnoreCase);
            await Assert.That(GetUriSource(button.LargeImage)).IsEqualTo(largeImage.For(newTheme), StringComparison.OrdinalIgnoreCase);
        }
    }

    [Test]
    public async Task ThemeChanged_ThemeSwitchedBack_RestoresTheInitialImages()
    {
        // Arrange
        var initialTheme = UIThemeManager.CurrentTheme;
        var image = CreateThemedImageFiles(CreateUniqueName(), "RibbonIcon16");
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        button.SetImage(image.Light);

        // Act
        SwitchTheme(Opposite(initialTheme));
        SwitchTheme(initialTheme);

        // Assert
        await Assert.That(GetUriSource(button.Image)).IsEqualTo(image.For(initialTheme), StringComparison.OrdinalIgnoreCase);
    }

    [Test]
    public async Task ThemeChanged_UnthemedImage_KeepsTheImage()
    {
        // Arrange
        var initialTheme = UIThemeManager.CurrentTheme;
        var directory = CreateUniqueName();
        var largeImage = CreateThemedImageFiles(directory, "RibbonIcon32");
        var imagePath = CreateImageFile(directory, "RibbonIcon16.png");
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        button.SetImage(imagePath);
        button.SetLargeImage(largeImage.Light);

        // Act
        SwitchTheme(Opposite(initialTheme));

        // Assert
        using (Assert.Multiple())
        {
            await Assert.That(GetUriSource(button.Image)).IsEqualTo(imagePath);
            await Assert.That(GetUriSource(button.LargeImage)).IsEqualTo(largeImage.For(Opposite(initialTheme)), StringComparison.OrdinalIgnoreCase);
        }
    }

    [Test]
    public async Task ThemeChanged_ImageLoadedFromStream_SwitchesTheThemedLargeImage()
    {
        // Arrange
        var initialTheme = UIThemeManager.CurrentTheme;
        var directory = CreateUniqueName();
        var largeImage = CreateThemedImageFiles(directory, "RibbonIcon32");
        var streamImagePath = CreateImageFile(directory, "RibbonIcon16.png");
        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        button.SetLargeImage(largeImage.Light);
        button.Image = LoadFromStream(streamImagePath);

        // Act
        SwitchTheme(Opposite(initialTheme));

        // Assert
        await Assert.That(GetUriSource(button.LargeImage)).IsEqualTo(largeImage.For(Opposite(initialTheme)), StringComparison.OrdinalIgnoreCase);
    }

    [Test]
    public async Task ThemeChanged_ButtonOfRemovedPanel_SwitchesImagesOfTheRemainingButtons()
    {
        // Arrange
        var initialTheme = UIThemeManager.CurrentTheme;
        var image = CreateThemedImageFiles(CreateUniqueName(), "RibbonIcon16");
        var removedPanel = ControlledApplication.CreatePanel(CreateUniqueName(), CreateUniqueName());
        removedPanel.AddPushButton<EmptyCommand>("Removed").SetImage(image.Light);
        removedPanel.RemovePanel();

        var button = CreateTestPanel().AddPushButton<EmptyCommand>("Run");
        button.SetImage(image.Light);

        // Act
        SwitchTheme(Opposite(initialTheme));

        // Assert
        await Assert.That(GetUriSource(button.Image)).IsEqualTo(image.For(Opposite(initialTheme)), StringComparison.OrdinalIgnoreCase);
    }

    private static void SwitchTheme(UITheme theme)
    {
        UIThemeManager.CurrentTheme = theme;
        Dispatcher.CurrentDispatcher.Invoke(static () => { }, DispatcherPriority.ContextIdle);
    }

    private static UITheme Opposite(UITheme theme)
    {
        return theme == UITheme.Dark ? UITheme.Light : UITheme.Dark;
    }

    private static string? GetUriSource(System.Windows.Media.ImageSource? image)
    {
        return (image as BitmapImage)?.UriSource?.OriginalString;
    }

    private static BitmapImage LoadFromStream(string path)
    {
        using var stream = File.OpenRead(path);

        var image = new BitmapImage();
        image.BeginInit();
        image.CacheOption = BitmapCacheOption.OnLoad;
        image.StreamSource = stream;
        image.EndInit();

        return image;
    }

    private static ThemedImage CreateThemedImageFiles(string directoryName, string name)
    {
        return new ThemedImage(
            CreateImageFile(directoryName, $"{name}Light.png"),
            CreateImageFile(directoryName, $"{name}Dark.png"));
    }

    private sealed record ThemedImage(string Light, string Dark)
    {
        public string For(UITheme theme) => theme == UITheme.Dark ? Dark : Light;
    }
}
#endif
