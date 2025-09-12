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
public static partial class RibbonExtensions
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
        var convertedColor = (Color)ColorConverter.ConvertFromString(color);

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
    ///         panel.SetBackground(Brushes.Red);
    ///         panel.SetBackground(new SolidColorBrush(Colors.Red));
    ///         panel.SetBackground(new LinearGradientBrush(
    ///         [
    ///             new GradientStop(Colors.Red, 0),
    ///             new GradientStop(Colors.Black, 1)
    ///         ], 45));
    ///     </code>
    /// </example>
    public static RibbonPanel SetBackground(this RibbonPanel panel, Brush brush)
    {
        var internalPanel = GetInternalPanel(panel);
        internalPanel.CustomPanelBackground = brush;

        return panel;
    }

    /// <summary>
    ///     Sets the panel title bar background color.
    /// </summary>
    /// <param name="panel">The target Ribbon panel</param>
    /// <param name="color">The color string representing the background color.</param>
    /// <returns>The Ribbon panel with the updated title bar background.</returns>
    /// <example>
    ///     <code>
    ///         panel.SetTitleBarBackground("Red");
    ///         panel.SetTitleBarBackground("#FF6669");
    ///         panel.SetTitleBarBackground("#FFFF6669");
    ///     </code>
    /// </example>
    public static RibbonPanel SetTitleBarBackground(this RibbonPanel panel, string color)
    {
        var convertedColor = (Color)ColorConverter.ConvertFromString(color);

        var internalPanel = GetInternalPanel(panel);
        internalPanel.CustomPanelTitleBarBackground = new SolidColorBrush(convertedColor);

        return panel;
    }

    /// <summary>
    ///     Sets the panel title bar background color.
    /// </summary>
    /// <param name="panel">The target Ribbon panel</param>
    /// <param name="color">The Color object representing the background color.</param>
    /// <returns>The Ribbon panel with the updated title bar background.</returns>
    /// <example>
    ///     <code>
    ///         panel.SetTitleBarBackground(Colors.Red);
    ///         panel.SetTitleBarBackground(Color.FromRgb(255, 0, 0));
    ///     </code>
    /// </example>
    public static RibbonPanel SetTitleBarBackground(this RibbonPanel panel, Color color)
    {
        var internalPanel = GetInternalPanel(panel);
        internalPanel.CustomPanelTitleBarBackground = new SolidColorBrush(color);

        return panel;
    }

    /// <summary>
    ///     Sets the panel title bar background color.
    /// </summary>
    /// <param name="panel">The target Ribbon panel</param>
    /// <param name="brush">The Brush representing the background.</param>
    /// <returns>The Ribbon panel with the updated title bar background.</returns>
    /// <example>
    ///     <code>
    ///         panel.SetTitleBarBackground(Brushes.Red);
    ///         panel.SetTitleBarBackground(new SolidColorBrush(Colors.Red));
    ///         panel.SetTitleBarBackground(new LinearGradientBrush(
    ///         [
    ///             new GradientStop(Colors.Red, 0),
    ///             new GradientStop(Colors.Black, 1)
    ///         ], 45));
    ///     </code>
    /// </example>
    public static RibbonPanel SetTitleBarBackground(this RibbonPanel panel, Brush brush)
    {
        var internalPanel = GetInternalPanel(panel);
        internalPanel.CustomPanelTitleBarBackground = brush;

        return panel;
    }

    /// <summary>
    ///     Sets the slide-out panel background color for the target panel.
    /// </summary>
    /// <param name="panel">The target Ribbon panel</param>
    /// <param name="color">The color string representing the background color.</param>
    /// <returns>The Ribbon panel with the updated slide-out panel background.</returns>
    /// <example>
    ///     <code>
    ///         panel.SetSlideOutPanelBackground("Red");
    ///         panel.SetSlideOutPanelBackground("#FF6669");
    ///         panel.SetSlideOutPanelBackground("#FFFF6669");
    ///     </code>
    /// </example>
    public static RibbonPanel SetSlideOutPanelBackground(this RibbonPanel panel, string color)
    {
        var convertedColor = (Color)ColorConverter.ConvertFromString(color);

        var internalPanel = GetInternalPanel(panel);
        internalPanel.CustomSlideOutPanelBackground = new SolidColorBrush(convertedColor);

        return panel;
    }

    /// <summary>
    ///     Sets the slide-out panel background color for the target panel.
    /// </summary>
    /// <param name="panel">The target Ribbon panel</param>
    /// <param name="color">The Color object representing the background color.</param>
    /// <returns>The Ribbon panel with the updated slide-out panel background.</returns>
    /// <example>
    ///     <code>
    ///         panel.SetSlideOutPanelBackground(Colors.Red);
    ///         panel.SetSlideOutPanelBackground(Color.FromRgb(255, 0, 0));
    ///     </code>
    /// </example>
    public static RibbonPanel SetSlideOutPanelBackground(this RibbonPanel panel, Color color)
    {
        var internalPanel = GetInternalPanel(panel);
        internalPanel.CustomSlideOutPanelBackground = new SolidColorBrush(color);

        return panel;
    }

    /// <summary>
    ///     Sets the slide-out panel background color for the target panel.
    /// </summary>
    /// <param name="panel">The target Ribbon panel</param>
    /// <param name="brush">The Brush representing the background.</param>
    /// <returns>The Ribbon panel with the updated slide-out panel background.</returns>
    /// <example>
    ///     <code>
    ///         panel.SetSlideOutPanelBackground(Brushes.Red);
    ///         panel.SetSlideOutPanelBackground(new SolidColorBrush(Colors.Red));
    ///         panel.SetSlideOutPanelBackground(new LinearGradientBrush(
    ///         [
    ///             new GradientStop(Colors.Red, 0),
    ///             new GradientStop(Colors.Black, 1)
    ///         ], 45));
    ///     </code>
    /// </example>
    public static RibbonPanel SetSlideOutPanelBackground(this RibbonPanel panel, Brush brush)
    {
        var internalPanel = GetInternalPanel(panel);
        internalPanel.CustomSlideOutPanelBackground = brush;

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
    ///     Adds keyboard shortcuts to the specified <see cref="PushButton"/> using the provided string representation.
    /// </summary>
    /// <param name="button">The <see cref="PushButton"/> to which the shortcuts will be added.</param>
    /// <param name="representation">A string representation of the shortcuts.</param>
    /// <returns>The <see cref="PushButton"/> with the added shortcuts.</returns>
    /// <remarks>The representation can be a single shortcut, or a group of shortcuts with a '#' delimiter</remarks>
    /// <example>
    ///     <code>
    ///         button.AddShortcuts("RE");
    ///     </code>
    /// </example>
    public static PushButton AddShortcuts(this PushButton button, string representation)
    {
        AddButtonShortcuts(button, representation);
        return button;
    }
    
    /// <summary>
    ///     Adds keyboard shortcuts to the specified <see cref="PushButton"/> using the provided collection of shortcut strings.
    /// </summary>
    /// <param name="button">The <see cref="PushButton"/> to which the shortcuts will be added.</param>
    /// <param name="shortcuts">A collection of shortcut strings to be added to the button.</param>
    /// <returns>The <see cref="PushButton"/> with the added shortcuts.</returns>
    /// <example>
    ///     <code>
    ///         button.AddShortcuts("RE", "NP", "QQ");
    ///     </code>
    /// </example>
    public static PushButton AddShortcuts(this PushButton button, params IEnumerable<string> shortcuts)
    {
        AddButtonShortcuts(button, string.Join("#", shortcuts));
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
    
    /// <summary>
    ///     Sets contextual help for the specified <see cref="RibbonItem"/> using a URL.
    /// </summary>
    /// <param name="button">The <see cref="RibbonItem"/> to which contextual help will be assigned.</param>
    /// <param name="helpPath">A URL pointing to online help content (e.g., documentation, knowledge base, or support page).</param>
    /// <returns>The same <see cref="RibbonItem"/> instance with contextual help configured.</returns>
    /// <remarks>
    ///     Contextual help is displayed when the user clicks the help button (question mark) in the extended tooltip.
    ///     Only URL-based help is supported by this helper method. If you need to use other help types (e.g., chm / context id), create a <see cref="ContextualHelp"/> instance manually and call Revit's native SetContextualHelp method.
    /// </remarks>
    /// <example>
    ///     <code>
    ///         button.SetContextualHelp("https://mydomain.com/docs/command-help");
    ///     </code>
    /// </example>
    public static RibbonItem SetContextualHelp(this RibbonItem button, string helpPath)
    {
        button.SetContextualHelp(new ContextualHelp(ContextualHelpType.Url, helpPath));
        return button;
    }
}