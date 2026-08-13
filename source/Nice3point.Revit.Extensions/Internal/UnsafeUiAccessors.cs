#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using RibbonItem = Autodesk.Windows.RibbonItem;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;

namespace Nice3point.Revit.Extensions.Internal;

internal static class UnsafeUiAccessors
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    public static extern RibbonPanel CreateRibbonPanel(Autodesk.Windows.RibbonPanel panel, string tabId);

    [UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "m_ItemsNameDictionary")]
    public static extern ref Dictionary<string, Dictionary<string, RibbonPanel>> GetCachedTabs(UIApplication? uiApplication);

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "m_RibbonItem")]
    public static extern ref RibbonItem GetInternalItem(Autodesk.Revit.UI.RibbonItem itemPointer);

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "m_RibbonPanel")]
    public static extern ref Autodesk.Windows.RibbonPanel GetInternalPanel(RibbonPanel panelPointer);

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "addItemToRowPanel")]
    public static extern Autodesk.Revit.UI.RibbonItem AddItemToRowPanel(RibbonPanel panelPointer, RibbonRowPanel panel, RibbonItemData itemData);

    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    public static extern UIControlledApplication CreateUiControlledApplication(UIApplication uiApplication);
}
#endif
