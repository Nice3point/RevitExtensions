using Nice3point.Revit.Extensions.Internal.Formats;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class ThemeUriUtilsTests
{
    [Test]
    [Arguments("")]
    [Arguments("/RevitAddIn;component/Resources/Icons/RibbonIcon16.png")]
    [Arguments("/RevitAddIn;component/Resources/Themed/RibbonIcon16.png")]
    [Arguments(@"C:\Program Files\RevitAddIn\Resources\Icons\RibbonIcon16.png")]
    public async Task TryGetThemedUri_UriWithoutThemeToken_ReturnsFalse(string uri)
    {
        var themed = ThemeUriUtils.TryGetThemedUri(uri, darkTheme: true, out var result);

        using (Assert.Multiple())
        {
            await Assert.That(themed).IsFalse();
            await Assert.That(result).IsEqualTo(uri);
        }
    }

    [Test]
    [Arguments("/RevitAddIn;component/Resources/Icons/RibbonIcon16Light.png", RequestedTheme.Light)]
    [Arguments("/RevitAddIn;component/Resources/Icons/RibbonIcon16Dark.png", RequestedTheme.Dark)]
    [Arguments("/RevitAddIn;component/Resources/Icons/RIBBONICON16DARK.png", RequestedTheme.Dark)]
    [Arguments("/RevitAddIn;component/Resources/Light/RibbonIcon16.png", RequestedTheme.Light)]
    [Arguments(@"C:\Program Files\RevitAddIn\Resources\Dark\RibbonIcon16.png", RequestedTheme.Dark)]
    public async Task TryGetThemedUri_ThemeTokenMatchesRequestedTheme_KeepsUri(string uri, RequestedTheme requestedTheme)
    {
        var themed = ThemeUriUtils.TryGetThemedUri(uri, requestedTheme is RequestedTheme.Dark, out var result);

        using (Assert.Multiple())
        {
            await Assert.That(themed).IsTrue();
            await Assert.That(result).IsEqualTo(uri);
        }
    }

    [Test]
    [Arguments("/RevitAddIn;component/Resources/Icons/RibbonIcon16Light.png", RequestedTheme.Dark, "/RevitAddIn;component/Resources/Icons/RibbonIcon16dark.png")]
    [Arguments("/RevitAddIn;component/Resources/Icons/RibbonIcon16Dark.png", RequestedTheme.Light, "/RevitAddIn;component/Resources/Icons/RibbonIcon16light.png")]
    [Arguments("/RevitAddIn;component/Resources/Icons/RIBBONICON16DARK.png", RequestedTheme.Light, "/RevitAddIn;component/Resources/Icons/RIBBONICON16light.png")]
    [Arguments("/RevitAddIn;component/Resources/Icons/DarkRibbonIcon16Dark.png", RequestedTheme.Light, "/RevitAddIn;component/Resources/Icons/DarkRibbonIcon16light.png")]
    [Arguments(@"C:\Program Files\RevitAddIn\Resources\Icons\RibbonIcon16Light.png", RequestedTheme.Dark, @"C:\Program Files\RevitAddIn\Resources\Icons\RibbonIcon16dark.png")]
    [Arguments("/RevitAddIn;component/Resources/Light/RibbonIcon16.png", RequestedTheme.Dark, "/RevitAddIn;component/Resources/Light/RibbonIcon16.png")]
    [Arguments("/RevitAddIn;component/Resources/Light/RibbonIcon16Dark.png", RequestedTheme.Dark, "/RevitAddIn;component/Resources/Light/RibbonIcon16Dark.png")]
    [Arguments(@"C:\Program Files\RevitAddIn\Resources\Dark\RibbonIcon16.png", RequestedTheme.Light, @"C:\Program Files\RevitAddIn\Resources\Dark\RibbonIcon16.png")]
    public async Task TryGetThemedUri_ThemeTokenDiffersFromRequestedTheme_ReplacesFileNameTokenOnly(string uri, RequestedTheme requestedTheme, string expected)
    {
        var themed = ThemeUriUtils.TryGetThemedUri(uri, requestedTheme is RequestedTheme.Dark, out var result);

        using (Assert.Multiple())
        {
            await Assert.That(themed).IsTrue();
            await Assert.That(result).IsEqualTo(expected);
        }
    }
}

public enum RequestedTheme
{
    Light,
    Dark
}
