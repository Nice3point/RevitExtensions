#if REVIT2025_OR_GREATER
using Autodesk.Revit.UI;
using Nice3point.Revit.Extensions.Internal;
using Nice3point.Revit.Extensions.UI;
using Nice3point.TUnit.Revit;

namespace Nice3point.Revit.Extensions.Tests;

public sealed class ContextMenuExtensionsTests : RevitApiUiTest
{
    [Test]
    public async Task BuildContextMenu_Configuration_ConfiguresTheBuiltMenu()
    {
        // Arrange
        ContextMenu? configuredMenu = null;
        var creator = new ContextMenuCreator(menu => configuredMenu = menu);
        var contextMenu = new ContextMenu();

        // Act
        creator.BuildContextMenu(contextMenu);

        // Assert
        await Assert.That(configuredMenu).IsSameReferenceAs(contextMenu);
    }

    [Test]
    public async Task AddSubMenu_SubMenu_ReturnsTheSubMenu()
    {
        // Arrange
        var contextMenu = new ContextMenu();
        var subMenu = new ContextMenu();

        // Act
        var result = contextMenu.AddSubMenu("Tools", subMenu);

        // Assert
        await Assert.That(result).IsSameReferenceAs(subMenu);
    }
}
#endif
