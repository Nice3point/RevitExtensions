using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using Nice3point.Revit.Extensions.UIFrameworkExtensions;
using UIFramework;
using Color = System.Windows.Media.Color;
using ComboBox = Autodesk.Revit.UI.ComboBox;
using RibbonButton = Autodesk.Revit.UI.RibbonButton;
using RibbonItem = Autodesk.Revit.UI.RibbonItem;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;
using TextBox = Autodesk.Revit.UI.TextBox;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Contains extension methods for creating and managing custom Ribbon elements in the Revit UI.
///     These extensions provide simplified methods for adding panels, buttons, and other controls 
///     to the "Add-ins" tab or any custom tab in the Revit ribbon.
///     These utilities streamline the process of integrating external commands and tools 
///     into the Revit user interface.
/// </summary>
[PublicAPI]
public static class RibbonExtensions
{
    /// <summary>
    ///     Creates or retrieves an existing panel in the "Add-ins" tab of the Revit ribbon.
    /// </summary>
    /// <param name="application">The Revit application instance.</param>
    /// <param name="panelName">The name of the panel to create.</param>
    /// <returns>The created or existing Ribbon panel.</returns>
    /// <remarks>
    ///     If a panel with the specified name already exists in the "Add-ins" tab, it will return that panel.
    ///     Otherwise, a new panel will be created with the given name.
    /// </remarks>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">Thrown when the panelName is empty.</exception>
    /// <exception cref="Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     Thrown if more than 100 panels were created in the session.
    /// </exception>
    public static RibbonPanel CreatePanel(this UIControlledApplication application, string panelName)
    {
        foreach (var panel in application.GetRibbonPanels(Tab.AddIns))
        {
            if (panel.Name == panelName)
            {
                return panel;
            }
        }

        return application.CreateRibbonPanel(panelName);
    }

    /// <summary>
    ///     Creates or retrieves an existing panel in a specified tab of the Revit ribbon.
    /// </summary>
    /// <param name="application">The Revit application instance.</param>
    /// <param name="panelName">The name of the panel to create.</param>
    /// <param name="tabName">The name of the tab in which the panel should be located.</param>
    /// <returns>The created or existing Ribbon panel.</returns>
    /// <remarks>
    ///     If the tab doesn't exist, it will be created first. <br />
    ///     If a panel with the specified name already exists within the tab, it will return that panel; otherwise, a new one will be created. <br />
    ///     Adding a panel also supports built-in tabs.
    ///     To add a panel to the built-in Revit tab, specify the panel ID or name as the <paramref name="tabName"/> parameter.
    /// </remarks>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">Thrown when <paramref name="panelName"/> or <paramref name="tabName"/> is empty.</exception>
    /// <exception cref="Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     Thrown if more than 100 panels were created, or if the maximum number of custom tabs (20) has been exceeded.
    /// </exception>
    public static RibbonPanel CreatePanel(this UIControlledApplication application, string panelName, string tabName)
    {
        var cachedTabs = GetCachedTabs();
        if (cachedTabs.TryGetValue(tabName, out var cachedPanels))
        {
            if (cachedPanels.TryGetValue(panelName, out var cachedPanel))
            {
                return cachedPanel;
            }
        }

        var tabsCollection = new List<RibbonTab>();
        foreach (var tab in RevitRibbonControl.RibbonControl.Tabs)
        {
            if (tab.Id == tabName || tab.Title == tabName)
            {
                tabsCollection.Add(tab);
            }
        }

        var existedTab = tabsCollection.Count switch
        {
            1 => tabsCollection[0],
            0 => null,
            _ => tabsCollection.FirstOrDefault(tab => tab.IsVisible) ?? tabsCollection[0]
        };

        if (existedTab is not null)
        {
            var (internalPanel, panel) = CreateInternalPanel(existedTab.Id, panelName);
            existedTab.Panels.Add(internalPanel);
            return panel;
        }

        application.CreateRibbonTab(tabName);
        return application.CreateRibbonPanel(tabName, panelName);
    }

    /// <summary>
    ///     Retrieves the internal <see cref="Autodesk.Windows.RibbonPanel"/> instance associated with the specified <see cref="RibbonPanel"/>.
    ///     This method uses reflection to access the private field "m_RibbonPanel" within the provided <see cref="RibbonPanel"/>.
    /// </summary>
    /// <param name="panel">The <see cref="RibbonPanel"/> to extract the internal <see cref="Autodesk.Windows.RibbonPanel"/> from.</param>
    /// <returns>The internal <see cref="Autodesk.Windows.RibbonPanel"/> instance.</returns>
    private static Autodesk.Windows.RibbonPanel GetInternalPanel(this RibbonPanel panel)
    {
        var panelField = panel.GetType().GetField("m_RibbonPanel", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
        return (Autodesk.Windows.RibbonPanel)panelField.GetValue(panel)!;
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
    ///     Creates a new <see cref="Autodesk.Revit.UI.RibbonPanel"/> using the specified internal <see cref="Autodesk.Windows.RibbonPanel"/>.
    /// </summary>
    /// <param name="panel">The internal <see cref="Autodesk.Windows.RibbonPanel"/> instance.</param>
    /// <param name="tabId">The ID of the tab where the panel should be added.</param>
    /// <returns>The created <see cref="Autodesk.Revit.UI.RibbonPanel"/>.</returns>
    private static RibbonPanel CreatePanel(Autodesk.Windows.RibbonPanel panel, string tabId)
    {
        var type = typeof(RibbonPanel);
#if NETCOREAPP
        var constructorInfo = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly,
            [typeof(Autodesk.Windows.RibbonPanel), typeof(string)])!;
#else
        var constructorInfo = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly,
            null,
            [typeof(Autodesk.Windows.RibbonPanel), typeof(string)],
            null)!;
#endif
        return (RibbonPanel)constructorInfo.Invoke([panel, tabId]);
    }

    /// <summary>
    ///     Retrieves the cached dictionary of tabs and panels within the Revit application.
    /// </summary>
    /// <returns>A dictionary where keys are tab IDs and values are dictionaries of tab names and their corresponding <see cref="Autodesk.Revit.UI.RibbonPanel"/> instances.</returns>
    [Pure]
    private static Dictionary<string, Dictionary<string, RibbonPanel>> GetCachedTabs()
    {
        var applicationType = typeof(UIApplication);
        var panelsField = applicationType.GetField("m_ItemsNameDictionary", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)!;
        return (Dictionary<string, Dictionary<string, RibbonPanel>>)panelsField.GetValue(null)!;
    }

    /// <summary>
    ///     Removes a specified <see cref="Autodesk.Revit.UI.RibbonPanel"/> from the Revit ribbon.
    /// </summary>
    /// <param name="panel">The <see cref="Autodesk.Revit.UI.RibbonPanel"/> to remove.</param>
    public static void RemovePanel(this RibbonPanel panel)
    {
        var cachedPanels = GetCachedTabs();

        var internalPanel = panel.GetInternalPanel();
        var internalTab = internalPanel.Tab;

        internalTab.Panels.Remove(internalPanel);
        if (internalTab.Panels.Count == 0)
        {
            ComponentManager.Ribbon.Tabs.Remove(internalTab);
        }

        var ribbonPanels = cachedPanels[internalTab.Id];
        ribbonPanels.Remove(panel.Name);
        if (ribbonPanels.Count == 0)
        {
            cachedPanels.Remove(internalTab.Id);
        }
    }
    
    /// <summary>
    ///     Sets the panel background color.
    /// </summary>
    /// <param name="panel">The target Ribbon panel</param>
    /// <param name="color">The color string representing the background color.</param>
    /// <returns>The Ribbon panel with the updated background.</returns>
    /// <example>
    ///     <code>
    ///         panel.SetBackground("Red");
    ///         panel.SetBackground("#FF6669");
    ///         panel.SetBackground("#FFFF6669");
    ///     </code>
    /// </example>
    public static RibbonPanel SetBackground(this RibbonPanel panel, string color)
    {
        var convertedColor = (Color) ColorConverter.ConvertFromString(color);
        
        var internalPanel = GetInternalPanel(panel);
        internalPanel.CustomPanelBackground = new SolidColorBrush(convertedColor);

        return panel;
    }
    
    /// <summary>
    ///     Sets the panel background color.
    /// </summary>
    /// <param name="panel">The target Ribbon panel</param>
    /// <param name="color">The Color object representing the background color.</param>
    /// <returns>The Ribbon panel with the updated background.</returns>
    /// <example>
    ///     <code>
    ///         panel.SetBackground(Colors.Red);
    ///         panel.SetBackground(Color.FromRgb(255, 0, 0));
    ///     </code>
    /// </example>
    public static RibbonPanel SetBackground(this RibbonPanel panel, Color color)
    {
        var internalPanel = GetInternalPanel(panel);
        internalPanel.CustomPanelBackground = new SolidColorBrush(color);

        return panel;
    }
    
    /// <summary>
    ///     Sets the panel background color.
    /// </summary>
    /// <param name="panel">The target Ribbon panel</param>
    /// <param name="brush">The Brush representing the background.</param>
    /// <returns>The Ribbon panel with the updated background.</returns>
    /// <example>
    ///     <code>
    ///         panel.SetBackground(new SolidColorBrush(Colors.Red));
    ///         
    ///          panel.SetBackground(new LinearGradientBrush(
    ///              new GradientStopCollection
    ///              {
    ///                  new GradientStop(Colors.Red, 0),
    ///                  new GradientStop(Colors.Black, 1)
    ///              }, 45));
    ///     </code>
    /// </example>
    public static RibbonPanel SetBackground(this RibbonPanel panel, Brush brush)
    {
        var internalPanel = GetInternalPanel(panel);
        internalPanel.CustomPanelBackground = brush;

        return panel;
    }

    /// <summary>
    ///     Adds a vertical stack panel to the specified Ribbon panel.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the stack panel will be added.</param>
    /// <returns>An <see cref="IRibbonStackPanel"/> instance representing the newly added stack panel.</returns>
    /// <remarks>
    ///     By default, the StackPanel accommodates one to three elements vertically. If the added items exceed the maximum threshold, they will be automatically added to a new column.
    /// </remarks>
    /// <example>
    ///      The added 5 buttons will create 2 vertical panels, one will contain 3 items and the other 2 items
    ///     <code>
    ///         var stackPanel = panel.AddStackPanel();
    ///         stackPanel.AddPushButton&lt;StartupCommand&lt;("Execute");
    ///         stackPanel.AddPushButton&lt;StartupCommand&lt;("Execute");
    ///         stackPanel.AddPushButton&lt;StartupCommand&lt;("Execute");
    ///         stackPanel.AddPushButton&lt;StartupCommand&lt;("Execute");
    ///         stackPanel.AddPushButton&lt;StartupCommand&lt;("Execute");
    ///     </code>
    /// </example>
    public static IRibbonStackPanel AddStackPanel(this RibbonPanel panel)
    {
        return new RibbonStackPanel(panel);
    }

    /// <summary>
    ///     Adds a PushButton to the specified Ribbon panel.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the button will be added.</param>
    /// <param name="command">The type of the external command associated with the button.</param>
    /// <param name="buttonText">The label text for the button.</param>
    /// <returns>The newly added PushButton.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a PushButton with the same Command already exists in the panel.
    /// </exception>
    public static PushButton AddPushButton(this RibbonPanel panel, Type command, string buttonText)
    {
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command)!.Location, command.FullName);
        return (PushButton)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a PushButton to the specified Ribbon panel.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command that implements <see cref="IExternalCommand"/>.</typeparam>
    /// <param name="panel">The Ribbon panel to which the button will be added.</param>
    /// <param name="buttonText">The label text for the button.</param>
    /// <returns>The newly added PushButton.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a PushButton with the same Command already exists in the panel.
    /// </exception>
    public static PushButton AddPushButton<TCommand>(this RibbonPanel panel, string buttonText) where TCommand : IExternalCommand, new()
    {
        var command = typeof(TCommand);
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command)!.Location, command.FullName);
        return (PushButton)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a PushButton to the specified PullDownButton in the Ribbon.
    /// </summary>
    /// <param name="pullDownButton">The PullDownButton to which the PushButton will be added.</param>
    /// <param name="command">The type of the external command associated with the button.</param>
    /// <param name="buttonText">The label text for the button.</param>
    /// <returns>The newly added PushButton.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a PushButton with the same Command already exists in the PullDownButton.
    /// </exception>
    public static PushButton AddPushButton(this PulldownButton pullDownButton, Type command, string buttonText)
    {
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command)!.Location, command.FullName);
        return pullDownButton.AddPushButton(pushButtonData);
    }

    /// <summary>
    ///     Adds a PushButton to the specified PullDownButton in the Ribbon.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command that implements <see cref="IExternalCommand"/>.</typeparam>
    /// <param name="pullDownButton">The PullDownButton to which the PushButton will be added.</param>
    /// <param name="buttonText">The label text for the button.</param>
    /// <returns>The newly added PushButton.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a PushButton with the same Command already exists in the PullDownButton.
    /// </exception>
    public static PushButton AddPushButton<TCommand>(this PulldownButton pullDownButton, string buttonText) where TCommand : IExternalCommand, new()
    {
        var command = typeof(TCommand);
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command)!.Location, command.FullName);
        return pullDownButton.AddPushButton(pushButtonData);
    }

    /// <summary>
    ///     Adds a PullDownButton to the specified Ribbon panel.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the PullDownButton will be added.</param>
    /// <param name="buttonText">The label text for the PullDownButton.</param>
    /// <returns>The added PullDownButton.</returns>
    public static PulldownButton AddPullDownButton(this RibbonPanel panel, string buttonText)
    {
        return AddPullDownButton(panel, Guid.NewGuid().ToString(), buttonText);
    }

    /// <summary>
    ///     Adds a PullDownButton to the specified Ribbon panel with a unique internal name.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the PullDownButton will be added.</param>
    /// <param name="internalName">A unique internal name for the PullDownButton.</param>
    /// <param name="buttonText">The label text for the PullDownButton.</param>
    /// <returns>The added PullDownButton.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a PullDownButton with the same internalName already exists in the panel.
    /// </exception>
    public static PulldownButton AddPullDownButton(this RibbonPanel panel, string internalName, string buttonText)
    {
        var pushButtonData = new PulldownButtonData(internalName, buttonText);
        return (PulldownButton)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a SplitButton to the specified Ribbon panel.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the SplitButton will be added.</param>
    /// <param name="buttonText">The label text for the SplitButton.</param>
    /// <returns>The added SplitButton.</returns>
    public static SplitButton AddSplitButton(this RibbonPanel panel, string buttonText)
    {
        return AddSplitButton(panel, Guid.NewGuid().ToString(), buttonText);
    }

    /// <summary>
    ///     Adds a SplitButton to the specified Ribbon panel with a unique internal name.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the SplitButton will be added.</param>
    /// <param name="internalName">A unique internal name for the SplitButton.</param>
    /// <param name="buttonText">The label text for the SplitButton.</param>
    /// <returns>The added SplitButton.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a SplitButton with the same internalName already exists in the panel.
    /// </exception>
    public static SplitButton AddSplitButton(this RibbonPanel panel, string internalName, string buttonText)
    {
        var pushButtonData = new SplitButtonData(internalName, buttonText);
        return (SplitButton)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a RadioButtonGroup to the specified Ribbon panel.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the RadioButtonGroup will be added.</param>
    /// <returns>The added RadioButtonGroup.</returns>
    public static RadioButtonGroup AddRadioButtonGroup(this RibbonPanel panel)
    {
        return AddRadioButtonGroup(panel, Guid.NewGuid().ToString());
    }

    /// <summary>
    ///     Adds a RadioButtonGroup to the specified Ribbon panel with a unique internal name.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the RadioButtonGroup will be added.</param>
    /// <param name="internalName">A unique internal name for the RadioButtonGroup.</param>
    /// <returns>The added RadioButtonGroup.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a RadioButtonGroup with the same internalName already exists in the panel.
    /// </exception>
    public static RadioButtonGroup AddRadioButtonGroup(this RibbonPanel panel, string internalName)
    {
        var pushButtonData = new RadioButtonGroupData(internalName);
        return (RadioButtonGroup)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a ComboBox to the specified Ribbon panel.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the ComboBox will be added.</param>
    /// <returns>The added ComboBox.</returns>
    public static ComboBox AddComboBox(this RibbonPanel panel)
    {
        return AddComboBox(panel, Guid.NewGuid().ToString());
    }

    /// <summary>
    ///     Adds a ComboBox to the specified Ribbon panel with a unique internal name.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the ComboBox will be added.</param>
    /// <param name="internalName">A unique internal name for the ComboBox.</param>
    /// <returns>The added ComboBox.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a ComboBox with the same internalName already exists in the panel.
    /// </exception>
    public static ComboBox AddComboBox(this RibbonPanel panel, string internalName)
    {
        var pushButtonData = new ComboBoxData(internalName);
        return (ComboBox)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a TextBox to the specified Ribbon panel.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the TextBox will be added.</param>
    /// <returns>The added TextBox.</returns>
    public static TextBox AddTextBox(this RibbonPanel panel)
    {
        return AddTextBox(panel, Guid.NewGuid().ToString());
    }

    /// <summary>
    ///     Adds a TextBox to the specified Ribbon panel with a unique internal name.
    /// </summary>
    /// <param name="panel">The Ribbon panel to which the TextBox will be added.</param>
    /// <param name="internalName">A unique internal name for the TextBox.</param>
    /// <returns>The added TextBox.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a TextBox with the same internalName already exists in the panel.
    /// </exception>
    public static TextBox AddTextBox(this RibbonPanel panel, string internalName)
    {
        var pushButtonData = new TextBoxData(internalName);
        return (TextBox)panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Sets a 16x16 pixel, 96dpi image from the specified URI source to the given Ribbon button.
    /// </summary>
    /// <param name="button">The Ribbon button to which the image will be added.</param>
    /// <param name="uri">The URI path to the image. Can be relative or absolute.</param>
    /// <returns>The Ribbon button with the added image.</returns>
    /// <example>
    ///     button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16.png")
    /// </example>
    public static RibbonButton SetImage(this RibbonButton button, string uri)
    {
#if REVIT2024_OR_GREATER
        if (TryGetThemedUri(uri, out var themedIconUri))
        {
            MonitorButtonTheme(button);
        }
#else
        var themedIconUri = uri;
#endif

        button.Image = new BitmapImage(new Uri(themedIconUri, UriKind.RelativeOrAbsolute));
        return button;
    }

    /// <summary>
    ///     Sets a 32x32 pixel, 96dpi image from the specified URI source to the given Ribbon button.
    /// </summary>
    /// <param name="button">The Ribbon button to which the large image will be added.</param>
    /// <param name="uri">The URI path to the image. Can be relative or absolute.</param>
    /// <returns>The Ribbon button with the added large image.</returns>
    /// <example>
    ///     button.SetLargeImage("/RevitAddIn;component/Resources/Icons/RibbonIcon32.png")
    /// </example>
    public static RibbonButton SetLargeImage(this RibbonButton button, string uri)
    {
#if REVIT2024_OR_GREATER
        if (TryGetThemedUri(uri, out var themedIconUri))
        {
            MonitorButtonTheme(button);
        }
#else
        var themedIconUri = uri;
#endif

        button.LargeImage = new BitmapImage(new Uri(themedIconUri, UriKind.RelativeOrAbsolute));
        return button;
    }

    /// <summary>
    ///     Sets the availability controller class for a PushButton. 
    ///     This class determines when the PushButton will be enabled or disabled in the Revit UI.
    /// </summary>
    /// <typeparam name="T">A class that implements <see cref="Autodesk.Revit.UI.IExternalCommandAvailability"/>.</typeparam>
    /// <param name="button">The PushButton to which the availability controller will be set.</param>
    /// <returns>The PushButton with the set availability controller.</returns>
    /// <remarks>
    ///     The availability controller must share the same assembly as the external command.
    /// </remarks>
    public static PushButton SetAvailabilityController<T>(this PushButton button) where T : IExternalCommandAvailability, new()
    {
        button.AvailabilityClassName = typeof(T).FullName;
        return button;
    }

    /// <summary>
    ///     Sets the tooltip text for the RibbonItem.
    /// </summary>
    /// <param name="button">The RibbonItem to which the tooltip will be added.</param>
    /// <param name="tooltip">The text to display as a tooltip when the mouse pointer hovers over the button.</param>
    /// <returns>The RibbonItem with the specified tooltip text.</returns>
    /// <remarks>
    ///     The tooltip text appears when the mouse pointer hovers over the button in the Revit UI. 
    ///     This method does not affect SplitButton or RadioButtonGroup controls. For SplitButton, the current PushButton's tooltip will always be displayed.
    ///     RadioButtonGroup controls do not support tooltips.
    /// </remarks>
    public static RibbonItem SetToolTip(this RibbonItem button, string tooltip)
    {
        button.ToolTip = tooltip;
        return button;
    }

    /// <summary>
    ///     Sets the extended tooltip description for the RibbonItem.
    /// </summary>
    /// <param name="button">The RibbonItem to which the extended tooltip description will be added.</param>
    /// <param name="description">
    ///     The text to display as part of the button's extended tooltip. 
    ///     This text is shown when the mouse hovers over the button for a prolonged period. 
    ///     Use &lt;p&gt; tags to separate the text into multiple paragraphs if needed.
    /// </param>
    /// <returns>The RibbonItem with the specified extended tooltip description.</returns>
    /// <remarks>
    ///     The extended tooltip provides additional information about the command and is optional. 
    ///     If neither this property nor the TooltipImage is set, the button will not have an extended tooltip. 
    ///     SplitButton and RadioButtonGroup controls cannot display the extended tooltip set by this method. 
    ///     For SplitButton, the current PushButton's tooltip is always displayed, while RadioButtonGroup does not support extended tooltips.
    /// </remarks>
    public static RibbonItem SetLongDescription(this RibbonItem button, string description)
    {
        button.LongDescription = description;
        return button;
    }

#if REVIT2024_OR_GREATER

    /// <summary>
    ///     List of Ribbon buttons that are monitored for theme changes.
    /// </summary>
    private static HashSet<RibbonButton> _themedButtons = [];

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
#if NETCOREAPP
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