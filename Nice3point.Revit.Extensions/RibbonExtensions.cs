using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using ComboBox = Autodesk.Revit.UI.ComboBox;
using RibbonButton = Autodesk.Revit.UI.RibbonButton;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;
using TextBox = Autodesk.Revit.UI.TextBox;
#if R25_OR_GREATER
using ContextMenu = Autodesk.Revit.UI.ContextMenu;
#endif

// ReSharper disable InvertIf
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable LoopCanBeConvertedToQuery

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Ribbon Extensions
/// </summary>
public static class RibbonExtensions
{
    /// <summary>
    ///     Creates a panel in the "Add-ins" tab
    /// </summary>
    /// <returns>New or existing Ribbon panel</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">panelName is Empty</exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">If more than 100 panels were created</exception>
    public static RibbonPanel CreatePanel(this UIControlledApplication application, string panelName)
    {
        RibbonPanel ribbonPanel = null;
        foreach (var panel in application.GetRibbonPanels(Tab.AddIns))
        {
            if (panel.Name.Equals(panelName))
            {
                ribbonPanel = panel;
                break;
            }
        }

        return ribbonPanel ?? application.CreateRibbonPanel(panelName);
    }

    /// <summary>
    ///     Creates a panel in the specified tab
    /// </summary>
    /// <returns>New or existing Ribbon panel</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">panelName or tabName is Empty</exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">If more than 100 panels were created</exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">Too many custom tabs have been created in this session. (Maximum is 20)</exception>
    public static RibbonPanel CreatePanel(this UIControlledApplication application, string panelName, string tabName)
    {
        RibbonTab ribbonTab = null;
        foreach (var tab in ComponentManager.Ribbon.Tabs)
        {
            if (tab.Id.Equals(tabName))
            {
                ribbonTab = tab;
                break;
            }
        }

        if (ribbonTab is null)
        {
            application.CreateRibbonTab(tabName);
            return application.CreateRibbonPanel(tabName, panelName);
        }

        RibbonPanel ribbonPanel = null;
        foreach (var panel in application.GetRibbonPanels(tabName))
        {
            if (panel.Name.Equals(panelName))
            {
                ribbonPanel = panel;
                break;
            }
        }

        return ribbonPanel ?? application.CreateRibbonPanel(tabName, panelName);
    }

    /// <summary>
    ///     Adds a PushButton to the Ribbon
    /// </summary>
    /// <returns>The added PushButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PushButton already exists in the panel</exception>
    public static PushButton AddPushButton(this RibbonPanel panel, Type command, string buttonText)
    {
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command)!.Location, command.FullName);
        return (PushButton)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a PushButton to the Ribbon
    /// </summary>
    /// <returns>The added PushButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PushButton already exists in the panel</exception>
    public static PushButton AddPushButton<TCommand>(this RibbonPanel panel, string buttonText) where TCommand : IExternalCommand, new()
    {
        var command = typeof(TCommand);
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command)!.Location, command.FullName);
        return (PushButton)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a PullDownButton to the Ribbon
    /// </summary>
    /// <returns>The added PullDownButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PullDownButton already exists in the panel</exception>
    public static PulldownButton AddPullDownButton(this RibbonPanel panel, string internalName, string buttonText)
    {
        var pushButtonData = new PulldownButtonData(internalName, buttonText);
        return (PulldownButton)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a SplitButton to the Ribbon
    /// </summary>
    /// <returns>The added SplitButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a SplitButton already exists in the panel</exception>
    public static SplitButton AddSplitButton(this RibbonPanel panel, string internalName, string buttonText)
    {
        var pushButtonData = new SplitButtonData(internalName, buttonText);
        return (SplitButton)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a RadioButtonGroup to the Ribbon
    /// </summary>
    /// <returns>The added RadioButtonGroup</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a RadioButtonGroup already exists in the panel</exception>
    public static RadioButtonGroup AddRadioButtonGroup(this RibbonPanel panel, string internalName)
    {
        var pushButtonData = new RadioButtonGroupData(internalName);
        return (RadioButtonGroup)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a ComboBox to the Ribbon
    /// </summary>
    /// <returns>The added ComboBox</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a ComboBox already exists in the panel</exception>
    public static ComboBox AddComboBox(this RibbonPanel panel, string internalName)
    {
        var pushButtonData = new ComboBoxData(internalName);
        return (ComboBox)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a TextBox to the Ribbon
    /// </summary>
    /// <returns>The added TextBox</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a TextBox already exists in the panel</exception>
    public static TextBox AddTextBox(this RibbonPanel panel, string internalName)
    {
        var pushButtonData = new TextBoxData(internalName);
        return (TextBox)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a PushButton to the PullDownButton
    /// </summary>
    /// <returns>The newly added PushButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PushButton already exists in the PushButton</exception>
    public static PushButton AddPushButton(this PulldownButton pullDownButton, Type command, string buttonText)
    {
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command)!.Location, command.FullName);
        return pullDownButton.AddPushButton(pushButtonData);
    }

    /// <summary>
    ///     Adds a PushButton to the PullDownButton
    /// </summary>
    /// <returns>The newly added PushButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PushButton already exists in the PushButton</exception>
    public static PushButton AddPushButton<TCommand>(this PulldownButton pullDownButton, string buttonText) where TCommand : IExternalCommand, new()
    {
        var command = typeof(TCommand);
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command)!.Location, command.FullName);
        return pullDownButton.AddPushButton(pushButtonData);
    }

    /// <summary>
    ///     Adds a 16x16px-96dpi image from the URI source
    /// </summary>
    /// <param name="button">Button to which the icon will be added</param>
    /// <param name="uri">Relative or Absolute path to the icon</param>
    /// <example>button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16.png")</example>
    public static RibbonButton SetImage(this RibbonButton button, string uri)
    {
        button.Image = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
        return button;
    }

    /// <summary>
    ///     Adds a 32x32px-96dpi image from the URI source
    /// </summary>
    /// <param name="button">Button to which the icon will be added</param>
    /// <param name="uri">Relative or Absolute path to the icon</param>
    /// <example>button.SetLargeImage("/RevitAddIn;component/Resources/Icons/RibbonIcon32.png")</example>
    public static RibbonButton SetLargeImage(this RibbonButton button, string uri)
    {
        button.LargeImage = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
        return button;
    }

    /// <summary>
    ///     Specifies the class that decides the availability of push button
    /// </summary>
    /// <param name="button">The button that will be restricted on the ribbon</param>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.UI.IExternalCommandAvailability"/></typeparam>
    /// <remarks>Class T should share the same assembly with add-in External Command</remarks>
    public static RibbonButton SetAvailabilityController<T>(this PushButton button) where T : IExternalCommandAvailability, new()
    {
        button.AvailabilityClassName = typeof(T).FullName;
        return button;
    }
#if R25_OR_GREATER

    /// <summary>
    ///     Adds a menu to the Context Menu
    /// </summary>
    /// <param name="menu">Menu item owner</param>
    /// <param name="name">The name will show on the menu</param>
    /// <typeparam name="TCommand">Type inherited from <see cref="Autodesk.Revit.UI.IExternalCommand"/></typeparam>
    public static ContextMenu AddMenuItem<TCommand>(this ContextMenu menu, string name) where TCommand : IExternalCommand, new()
    {
        var command = typeof(TCommand);
        var menuItem = new CommandMenuItem(name, command.FullName, Assembly.GetAssembly(command)!.Location);
        menu.AddItem(menuItem);

        return menu;
    }

    /// <summary>
    ///     Adds a separator to the Context Menu
    /// </summary>
    /// <param name="menu">Menu item owner</param>
    public static ContextMenu AddSeparator(this ContextMenu menu)
    {
        var separator = new SeparatorItem();
        menu.AddItem(separator);

        return menu;
    }

    /// <summary>
    ///     Adds a sub menu to the Context Menu
    /// </summary>
    /// <param name="menu">Menu item owner</param>
    /// <param name="name">The name will show on menu</param>
    /// <param name="subMenu">The child of Context Menu</param>
    public static ContextMenu AddSubMenu(this ContextMenu menu, string name, ContextMenu subMenu)
    {
        var separator = new SubMenuItem(name, subMenu);
        menu.AddItem(separator);

        return menu;
    }

    /// <summary>
    ///     Specifies the class that decides the availability of menu item
    /// </summary>
    /// <param name="menuItem">The menu item that will be restricted on the context menu</param>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.UI.IExternalCommandAvailability"/></typeparam>
    /// <remarks>Class T should share the same assembly with add-in External Command</remarks>
    public static CommandMenuItem SetAvailabilityController<T>(this CommandMenuItem menuItem) where T : IExternalCommandAvailability, new()
    {
        menuItem.SetAvailabilityClassName(typeof(T).FullName);
        return menuItem;
    }

    /// <summary>
    ///     Register and configure the newly created Context Menu
    /// </summary>
    public static UIControlledApplication RegisterContextMenu(this UIControlledApplication application, Action<ContextMenu> handler)
    {
        var callingAssembly = Assembly.GetCallingAssembly();

        var creator = new ContextMenuCreator(handler);
        application.RegisterContextMenu(callingAssembly.FullName, creator);
        return application;
    }

    private class ContextMenuCreator(Action<ContextMenu> handler) : IContextMenuCreator
    {
        public void BuildContextMenu(ContextMenu menu) => handler.Invoke(menu);
    }
#endif
}