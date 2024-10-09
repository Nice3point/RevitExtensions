using System.Windows;
using System.Windows.Interop;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Extensions to simplify the interaction of custom windows with Revit
/// </summary>
public static class ApplicationExtensions
{
    /// <summary>
    ///     Opens a window and returns without waiting for the newly opened window to close. Sets the owner of a child window
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///     <see cref="M:System.Windows.Window.Show" /> is called on a window that is closing (<see cref="E:System.Windows.Window.Closing" />) or has been closed (
    ///     <see cref="E:System.Windows.Window.Closed" />)
    /// </exception>
    /// <param name="window">Child window</param>
    /// <param name="handle">Owner window handle</param>
    /// <example>
    ///     <code>
    ///         _view.Show(uiApplication.MainWindowHandle)
    ///     </code>
    /// </example>
    public static void Show(this Window window, IntPtr handle)
    {
        _ = new WindowInteropHelper(window)
        {
            Owner = handle
        };

        window.Show();
    }
}