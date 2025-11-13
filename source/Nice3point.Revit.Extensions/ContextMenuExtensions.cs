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
    /// <param name="application">The Revit UI application</param>
    extension(UIControlledApplication application)
    {
        /// <summary>
        ///     Registers an action used to configure a Context menu
        /// </summary>
        /// <param name="configuration">The action used to configure the context menu</param>
        /// <remarks>The assembly name will be used for root Context Menu title</remarks>
        public UIControlledApplication ConfigureContextMenu(Action<ContextMenu> configuration)
        {
            var caller = Assembly.GetCallingAssembly();
            var creator = new ContextMenuCreator(configuration);
            application.RegisterContextMenu(caller.GetName().Name, creator);

            return application;
        }

        /// <summary>
        ///     Registers an action used to configure a Context menu
        /// </summary>
        /// <param name="title">The Context menu root title</param>
        /// <param name="configuration">The action used to configure the context menu</param>
        public UIControlledApplication ConfigureContextMenu(string title, Action<ContextMenu> configuration)
        {
            var creator = new ContextMenuCreator(configuration);
            application.RegisterContextMenu(title, creator);

            return application;
        }
    }

    /// <param name="menu">The menu item parent</param>
    extension(ContextMenu menu)
    {
        /// <summary>
        ///     Adds a menu item to the Context Menu
        /// </summary>
        /// <param name="name">The name will show on the menu</param>
        /// <typeparam name="TCommand">Type inherited from <see cref="Autodesk.Revit.UI.IExternalCommand"/></typeparam>
        public CommandMenuItem AddMenuItem<TCommand>(string name) where TCommand : IExternalCommand, new()
        {
            var command = typeof(TCommand);
            var menuItem = new CommandMenuItem(name, command.FullName, Assembly.GetAssembly(command)!.Location);
            menu.AddItem(menuItem);

            return menuItem;
        }

        /// <summary>
        ///     Adds a separator to the Context Menu
        /// </summary>
        public SeparatorItem AddSeparator()
        {
            var separator = new SeparatorItem();
            menu.AddItem(separator);

            return separator;
        }

        /// <summary>
        ///     Adds a sub menu to the Context Menu
        /// </summary>
        /// <param name="name">The name will show on sub menu</param>
        /// <param name="subMenu">The sub menu object</param>
        public ContextMenu AddSubMenu(string name, ContextMenu subMenu)
        {
            var subMenuItem = new SubMenuItem(name, subMenu);
            menu.AddItem(subMenuItem);

            return subMenu;
        }
    }

    /// <param name="menuItem">The single command menu item</param>
    extension(CommandMenuItem menuItem)
    {
        /// <summary>
        ///     Specifies the class type that decides the availability of menu item
        /// </summary>
        /// <typeparam name="TController">Type inherited from <see cref="Autodesk.Revit.UI.IExternalCommandAvailability"/></typeparam>
        public CommandMenuItem SetAvailabilityController<TController>() where TController : IExternalCommandAvailability, new()
        {
            menuItem.SetAvailabilityClassName(typeof(TController).FullName);

            return menuItem;
        }
    }
}
#endif