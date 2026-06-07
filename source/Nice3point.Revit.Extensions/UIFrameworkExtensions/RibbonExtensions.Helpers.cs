using System.Windows.Threading;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using UIFramework;
using UIFrameworkServices;
using RibbonItem = Autodesk.Revit.UI.RibbonItem;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;
#if REVIT2024_OR_GREATER
using System.ComponentModel;
using System.IO;
using System.Windows.Media.Imaging;
using RibbonButton = Autodesk.Revit.UI.RibbonButton;
#endif
#if NET8_0_OR_GREATER
using Nice3point.Revit.Extensions.Internal;

#else
using System.Reflection;
#endif

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.UI;

public static partial class RibbonExtensions
{
#if REVIT2024_OR_GREATER
    /// <summary>
    ///     List of Ribbon buttons that are monitored for theme changes.
    /// </summary>
    private static HashSet<RibbonButton> _themedButtons = [];
#endif

    /// <summary>
    ///     List of pending keyboard shortcut changes.
    /// </summary>
    private static readonly List<(string ItemId, string Representation, bool CheckConflicts)> PendingShortcuts = [];

    /// <summary>
    ///     Shortcuts reserved by the current batch update.
    /// </summary>
    private static HashSet<string>? _reservedShortcuts;

    /// <summary>
    ///     Indicates whether a deferred shortcut flush is already scheduled.
    /// </summary>
    private static bool _shortcutsFlushScheduled;

    /// <summary>
    ///     Adds keyboard shortcuts for the specified <see cref="PushButton"/> using the provided string representation.
    /// </summary>
    /// <param name="button">The <see cref="PushButton"/> to which the shortcuts will be applied.</param>
    /// <param name="representation">A string representation of the shortcuts, where each shortcut is separated by the '#' character.</param>
    private static void AddButtonShortcuts(PushButton button, string representation)
    {
        var internalItem = button.GetInternalItem();
        ScheduleShortcutsUpdate(internalItem.Id, representation, checkUsage: false);
    }

    /// <summary>
    ///     Attempts to add keyboard shortcuts for the specified <see cref="PushButton"/> using the provided string representation.
    ///     Shortcuts are added only if they do not conflict with existing commands.
    /// </summary>
    /// <param name="button">The <see cref="PushButton"/> to which the shortcuts will be applied.</param>
    /// <param name="representation">A string representation of the shortcuts, where each shortcut is separated by the '#' character.</param>
    /// <returns><see langword="true"/> if at least one shortcut was successfully added; otherwise, <see langword="false"/>.</returns>
    private static bool TryAddButtonShortcuts(PushButton button, string representation)
    {
        var newShortcuts = representation.Split(['#'], StringSplitOptions.RemoveEmptyEntries);
        if (newShortcuts.Length == 0) return false;

        _reservedShortcuts ??= LoadUsedShortcuts();

        var shortcutAdded = false;
        foreach (var newShortcut in newShortcuts)
        {
            shortcutAdded |= _reservedShortcuts.Add(newShortcut);
        }

        if (!shortcutAdded) return false;

        var internalItem = button.GetInternalItem();
        ScheduleShortcutsUpdate(internalItem.Id, representation, checkUsage: true);
        return true;
    }

    /// <summary>
    ///     Schedule shortcuts update on the Ribbon dispatcher.
    /// </summary>
    /// <param name="itemId">The ID of the internal Ribbon item to which the shortcuts will be applied.</param>
    /// <param name="representation">A string representation of the shortcuts, where each shortcut is separated by the '#' character.</param>
    /// <param name="checkUsage">Whether the shortcuts must be re-validated against existing commands before applying.</param>
    private static void ScheduleShortcutsUpdate(string itemId, string representation, bool checkUsage)
    {
        PendingShortcuts.Add((itemId, representation, checkUsage));

        if (_shortcutsFlushScheduled) return;
        _shortcutsFlushScheduled = true;

        var dispatcher = ComponentManager.Ribbon.Dispatcher ?? Dispatcher.CurrentDispatcher;
        dispatcher.InvokeAsync(FlushShortcuts, DispatcherPriority.ApplicationIdle);
    }

    /// <summary>
    ///     Applies all pending shortcut changes in a single batch.
    /// </summary>
    private static void FlushShortcuts()
    {
        _reservedShortcuts = null;
        _shortcutsFlushScheduled = false;
        if (PendingShortcuts.Count == 0) return;

        var hasChanges = false;
        var usedShortcuts = LoadUsedShortcuts();

        foreach (var (itemId, representation, checkUsage) in PendingShortcuts)
        {
            if (!ShortcutsHelper.Commands.TryGetValue(itemId, out var shortcutItem)) continue;

            if (checkUsage)
            {
                foreach (var newShortcut in representation.Split(['#'], StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!usedShortcuts.Add(newShortcut)) continue;

                    shortcutItem.Shortcuts.Add(newShortcut);
                    hasChanges = true;
                }
            }
            else
            {
                if (shortcutItem.ShortcutsRep is not null) continue;

                shortcutItem.ShortcutsRep = representation;
                foreach (var shortcut in shortcutItem.Shortcuts)
                {
                    usedShortcuts.Add(shortcut);
                }

                hasChanges = true;
            }
        }

        PendingShortcuts.Clear();

        if (hasChanges)
        {
            KeyboardShortcutService.applyShortcutChanges(ShortcutsHelper.Commands);
        }
    }

    /// <summary>
    ///     Reloads the command list and collects all shortcuts currently assigned to commands.
    /// </summary>
    /// <returns>A set of the shortcuts currently in use.</returns>
    private static HashSet<string> LoadUsedShortcuts()
    {
        ShortcutsHelper.LoadCommands();

        var shortcuts = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var command in ShortcutsHelper.Commands.Values)
        {
            if (command.Shortcuts is null) continue;
            foreach (var shortcut in command.Shortcuts)
            {
                shortcuts.Add(shortcut);
            }
        }

        return shortcuts;
    }

    /// <summary>
    ///     Creates a new <see cref="Autodesk.Revit.UI.RibbonPanel"/> using the specified internal <see cref="Autodesk.Windows.RibbonPanel"/>.
    /// </summary>
    /// <param name="panel">The internal <see cref="Autodesk.Windows.RibbonPanel"/> instance.</param>
    /// <param name="tabId">The ID of the tab where the panel should be added.</param>
    /// <returns>The created <see cref="Autodesk.Revit.UI.RibbonPanel"/>.</returns>
    private static RibbonPanel CreatePanel(Autodesk.Windows.RibbonPanel panel, string tabId)
    {
#if NET8_0_OR_GREATER
        return UnsafeUiAccessors.CreateRibbonPanel(panel, tabId);
#else
        var type = typeof(RibbonPanel);
#if NET
        var constructorInfo = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly,
            [typeof(Autodesk.Windows.RibbonPanel), typeof(string)])!;
#else
        var constructorInfo = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly,
            null,
            [typeof(Autodesk.Windows.RibbonPanel), typeof(string)],
            null)!;
#endif
        return (RibbonPanel)constructorInfo.Invoke([panel, tabId]);
#endif
    }

    /// <summary>
    ///     Creates a new internal <see cref="Autodesk.Windows.RibbonPanel"/> and its corresponding <see cref="Autodesk.Revit.UI.RibbonPanel"/> for the specified tab and panel name.
    /// </summary>
    /// <param name="tabId">The ID of the tab where the panel should be added.</param>
    /// <param name="panelName">The name of the panel to create.</param>
    /// <returns>A tuple containing the internal <see cref="Autodesk.Windows.RibbonPanel"/> and the corresponding <see cref="Autodesk.Revit.UI.RibbonPanel"/>.</returns>
    private static (Autodesk.Windows.RibbonPanel internalPanel, RibbonPanel panel) CreateInternalPanel(string tabId, string panelName)
    {
        var internalPanel = new Autodesk.Windows.RibbonPanel
        {
            Source = new RibbonPanelSource
            {
                Title = panelName
            }
        };

        var cachedTabs = GetCachedTabs();
        if (!cachedTabs.TryGetValue(tabId, out var cachedPanels))
        {
            cachedTabs[tabId] = cachedPanels = new Dictionary<string, RibbonPanel>();
        }

        var panel = CreatePanel(internalPanel, tabId);
        panel.Name = panelName;
        cachedPanels[panelName] = panel;

        return (internalPanel, panel);
    }

    /// <summary>
    ///     Retrieves the cached dictionary of tabs and panels within the Revit application.
    /// </summary>
    /// <returns>A dictionary where keys are tab IDs and values are dictionaries of tab names and their corresponding <see cref="Autodesk.Revit.UI.RibbonPanel"/> instances.</returns>
    [Pure]
    private static Dictionary<string, Dictionary<string, RibbonPanel>> GetCachedTabs()
    {
#if NET8_0_OR_GREATER
        return UnsafeUiAccessors.GetCachedTabs(null);
#else
        var applicationType = typeof(UIApplication);
        var panelsField = applicationType.GetField("m_ItemsNameDictionary", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)!;
        return (Dictionary<string, Dictionary<string, RibbonPanel>>)panelsField.GetValue(null)!;
#endif
    }

    /// <summary>
    ///     Retrieves the internal <see cref="Autodesk.Windows.RibbonItem"/> instance associated with the specified <see cref="RibbonItem"/>.
    ///     This method uses reflection to access the private field "m_RibbonItem" within the provided <see cref="RibbonItem"/>.
    /// </summary>
    /// <param name="ribbonItem">The <see cref="RibbonItem"/> to extract the internal <see cref="Autodesk.Windows.RibbonItem"/> from.</param>
    /// <returns>The internal <see cref="Autodesk.Windows.RibbonItem"/> instance.</returns>
    private static Autodesk.Windows.RibbonItem GetInternalItem(this RibbonItem ribbonItem)
    {
#if NET8_0_OR_GREATER
        return UnsafeUiAccessors.GetInternalItem(ribbonItem);
#else
        var internalField = typeof(RibbonItem).GetField("m_RibbonItem", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        return (Autodesk.Windows.RibbonItem)internalField.GetValue(ribbonItem)!;
#endif
    }

    /// <summary>
    ///     Retrieves the internal <see cref="Autodesk.Windows.RibbonPanel"/> instance associated with the specified <see cref="RibbonPanel"/>.
    ///     This method uses reflection to access the private field "m_RibbonPanel" within the provided <see cref="RibbonPanel"/>.
    /// </summary>
    /// <param name="panel">The <see cref="RibbonPanel"/> to extract the internal <see cref="Autodesk.Windows.RibbonPanel"/> from.</param>
    /// <returns>The internal <see cref="Autodesk.Windows.RibbonPanel"/> instance.</returns>
    private static Autodesk.Windows.RibbonPanel GetInternalPanel(this RibbonPanel panel)
    {
#if NET8_0_OR_GREATER
        return UnsafeUiAccessors.GetInternalPanel(panel);
#else
        var internalField = panel.GetType().GetField("m_RibbonPanel", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        return (Autodesk.Windows.RibbonPanel)internalField.GetValue(panel)!;
#endif
    }

#if REVIT2024_OR_GREATER
    /// <summary>
    ///     Monitors the button for theme changes and updates the image accordingly.
    /// </summary>
    /// <param name="button">The Ribbon button to monitor.</param>
    private static void MonitorButtonTheme(RibbonButton button)
    {
        ApplicationTheme.CurrentTheme.PropertyChanged -= OnApplicationThemeChanged;
        ApplicationTheme.CurrentTheme.PropertyChanged += OnApplicationThemeChanged;

        _themedButtons.Add(button);
    }

    /// <summary>
    ///     Handles the ApplicationThemeChanged event and updates the button image to match the current UI theme.
    /// </summary>
    private static void OnApplicationThemeChanged(object? sender, PropertyChangedEventArgs args)
    {
        if (args.PropertyName != nameof(ApplicationTheme.CurrentTheme.RibbonPanelBackgroundBrush)) return;
        if (UIThemeManager.CurrentTheme.ToString() == ApplicationTheme.CurrentTheme.RibbonTheme.Name) return;

        foreach (var button in _themedButtons)
        {
            if (button.Image is BitmapImage image)
            {
                TryGetThemedUri(image.UriSource.OriginalString, out var themedIconUri);
                button.Image = new BitmapImage(new Uri(themedIconUri, UriKind.RelativeOrAbsolute));
            }

            if (button.LargeImage is BitmapImage largeImage)
            {
                TryGetThemedUri(largeImage.UriSource.OriginalString, out var themedIconUri);
                button.LargeImage = new BitmapImage(new Uri(themedIconUri, UriKind.RelativeOrAbsolute));
            }
        }
    }

    /// <summary>
    ///     Attempts to modify the given URI to match the current UI theme.
    /// </summary>
    /// <param name="uri">The original URI.</param>
    /// <param name="result">The modified URI corresponding to the current UI theme, or the original URI if no modifications were made.</param>
    /// <returns><see langword="true"/> if the URI was modified to match the current UI theme; otherwise, <see langword="false"/>.</returns>
    private static bool TryGetThemedUri(string uri, out string result)
    {
        var theme = UITheme.Light;
        var themeIndex = uri.LastIndexOf("light", StringComparison.OrdinalIgnoreCase);
        if (themeIndex == -1)
        {
            theme = UITheme.Dark;
            themeIndex = uri.LastIndexOf("dark", StringComparison.OrdinalIgnoreCase);
            if (themeIndex == -1)
            {
                result = uri;
                return false;
            }
        }

        result = UIThemeManager.CurrentTheme switch
        {
            UITheme.Light when theme == UITheme.Dark => UpdateThemeUri(uri, "dark", "light", themeIndex),
            UITheme.Dark when theme == UITheme.Light => UpdateThemeUri(uri, "light", "dark", themeIndex),
            _ => uri
        };

        return true;

        static string UpdateThemeUri(string source, string currentTheme, string newTheme, int themeIndex)
        {
#if NET
            var sourceSpan = source.AsSpan();
            var before = sourceSpan[..themeIndex];
            var after = sourceSpan[(themeIndex + currentTheme.Length)..];
#else
            var before = source[..themeIndex];
            var after = source[(themeIndex + currentTheme.Length)..];
#endif
            return after.IndexOf(Path.AltDirectorySeparatorChar) >= 0 || after.IndexOf(Path.DirectorySeparatorChar) >= 0 ? source : string.Concat(before, newTheme, after);
        }
    }
#endif
}