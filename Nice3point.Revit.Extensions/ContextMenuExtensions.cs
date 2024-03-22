#if R25_OR_GREATER
using System.Reflection;
using Autodesk.Revit.UI;
using ContextMenu = Autodesk.Revit.UI.ContextMenu;

namespace Nice3point.Revit.Extensions;

public static class ContextMenuExtensions
{
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
}
#endif