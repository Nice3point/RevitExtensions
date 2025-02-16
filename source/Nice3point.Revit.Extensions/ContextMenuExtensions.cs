#if REVIT2025_OR_GREATER
using System.Reflection;
using Autodesk.Revit.UI;
using Nice3point.Revit.Extensions.Internal;
using ContextMenu = Autodesk.Revit.UI.ContextMenu;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Context Menu Extensions
/// </summary>
[PublicAPI]
public static class ContextMenuExtensions
{
    /// <summary>
    ///     Adds a menu item to the Context Menu
    /// </summary>
    /// <param name="menu">The menu item parent</param>
    /// <param name="name">The name will show on the menu</param>
    /// <typeparam name="TCommand">Type inherited from <see cref="Autodesk.Revit.UI.IExternalCommand"/></typeparam>
    public static CommandMenuItem AddMenuItem<TCommand>(this ContextMenu menu, string name) where TCommand : IExternalCommand, new()
    {
        var command = typeof(TCommand);
        var menuItem = new CommandMenuItem(name, command.FullName, Assembly.GetAssembly(command)!.Location);
        menu.AddItem(menuItem);

        return menuItem;
    }

    /// <summary>
    ///     Adds a separator to the Context Menu
    /// </summary>
    /// <param name="menu">The separator parent</param>
    public static SeparatorItem AddSeparator(this ContextMenu menu)
    {
        var separator = new SeparatorItem();
        menu.AddItem(separator);

        return separator;
    }

    /// <summary>
    ///     Adds a sub menu to the Context Menu
    /// </summary>
    /// <param name="menu">The sub menu parent</param>
    /// <param name="name">The name will show on sub menu</param>
    /// <param name="subMenu">The sub menu object</param>
    public static ContextMenu AddSubMenu(this ContextMenu menu, string name, ContextMenu subMenu)
    {
        var subMenuItem = new SubMenuItem(name, subMenu);
        menu.AddItem(subMenuItem);

        return subMenu;
    }

    /// <summary>
    ///     Specifies the class type that decides the availability of menu item
    /// </summary>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.UI.IExternalCommandAvailability"/></typeparam>
    public static CommandMenuItem SetAvailabilityController<T>(this CommandMenuItem menuItem) where T : IExternalCommandAvailability, new()
    {
        menuItem.SetAvailabilityClassName(typeof(T).FullName);

        return menuItem;
    }

    /// <summary>
    ///     Registers an action used to configure a Context menu
    /// </summary>
    /// <param name="application">The Revit application</param>
    /// <param name="configuration">The action used to configure the context menu</param>
    /// <remarks>The assembly name will be used for root Context Menu title</remarks>
    public static UIControlledApplication ConfigureContextMenu(this UIControlledApplication application, Action<ContextMenu> configuration)
    {
        var caller = Assembly.GetCallingAssembly();
        var creator = new ContextMenuCreator(configuration);
        application.RegisterContextMenu(caller.GetName().Name, creator);

        return application;
    }

    /// <summary>
    ///     Registers an action used to configure a Context menu
    /// </summary>
    /// <param name="application">The Revit application</param>
    /// <param name="title">The Context menu root title</param>
    /// <param name="configuration">The action used to configure the context menu</param>
    public static UIControlledApplication ConfigureContextMenu(this UIControlledApplication application, string title, Action<ContextMenu> configuration)
    {
        var creator = new ContextMenuCreator(configuration);
        application.RegisterContextMenu(title, creator);

        return application;
    }
}
#endif