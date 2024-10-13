using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using Nice3point.Revit.Extensions.Abstraction;
using ComboBox = Autodesk.Revit.UI.ComboBox;
using RibbonButton = Autodesk.Revit.UI.RibbonButton;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;
using TextBox = Autodesk.Revit.UI.TextBox;

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
    ///     If the tab doesn't exist, it will be created first. 
    ///     If a panel with the specified name already exists within the tab, it will return that panel; otherwise, a new one will be created.
    /// </remarks>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">Thrown when panelName or tabName is empty.</exception>
    /// <exception cref="Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     Thrown if more than 100 panels were created, or if the maximum number of custom tabs (20) has been exceeded.
    /// </exception>
    public static RibbonPanel CreatePanel(this UIControlledApplication application, string panelName, string tabName)
    {
        foreach (var tab in ComponentManager.Ribbon.Tabs)
        {
            if (tab.Id == tabName)
            {
                foreach (var panel in application.GetRibbonPanels(tabName))
                {
                    if (panel.Name == panelName)
                    {
                        return panel;
                    }
                }

                return application.CreateRibbonPanel(tabName, panelName);
            }
        }

        application.CreateRibbonTab(tabName);
        return application.CreateRibbonPanel(tabName, panelName);
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
        button.Image = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
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
        button.LargeImage = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
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
}