using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using RibbonButton = Autodesk.Revit.UI.RibbonButton;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;

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
        var ribbonPanel = application.GetRibbonPanels(Tab.AddIns).FirstOrDefault(panel => panel.Name.Equals(panelName));
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
        var ribbonTab = ComponentManager.Ribbon.Tabs.FirstOrDefault(tab => tab.Id.Equals(tabName));
        if (ribbonTab is null)
        {
            application.CreateRibbonTab(tabName);
            return application.CreateRibbonPanel(tabName, panelName);
        }

        var ribbonPanel = application.GetRibbonPanels(tabName).FirstOrDefault(panel => panel.Name.Equals(panelName));
        return ribbonPanel ?? application.CreateRibbonPanel(tabName, panelName);
    }

    /// <summary>
    ///     Adds a PushButton to the Ribbon
    /// </summary>
    /// <returns>The added PushButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PushButton already exists in the panel</exception>
    public static PushButton AddPushButton(this RibbonPanel panel, Type command, string buttonText)
    {
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command).Location, command.FullName);
        return (PushButton) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a PushButton to the Ribbon
    /// </summary>
    /// <returns>The added PushButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PushButton already exists in the panel</exception>
    public static PushButton AddPushButton<TCommand>(this RibbonPanel panel, string buttonText) where TCommand : IExternalCommand
    {
        var command = typeof(TCommand);
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command).Location, command.FullName);
        return (PushButton) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a PullDownButton to the Ribbon
    /// </summary>
    /// <returns>The added PullDownButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PullDownButton already exists in the panel</exception>
    public static PulldownButton AddPullDownButton(this RibbonPanel panel, string internalName, string buttonText)
    {
        var pushButtonData = new PulldownButtonData(internalName, buttonText);
        return (PulldownButton) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a SplitButton to the Ribbon
    /// </summary>
    /// <returns>The added SplitButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a SplitButton already exists in the panel</exception>
    public static SplitButton AddSplitButton(this RibbonPanel panel, string internalName, string buttonText)
    {
        var pushButtonData = new SplitButtonData(internalName, buttonText);
        return (SplitButton) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a RadioButtonGroup to the Ribbon
    /// </summary>
    /// <returns>The added RadioButtonGroup</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a RadioButtonGroup already exists in the panel</exception>
    public static RadioButtonGroup AddRadioButtonGroup(this RibbonPanel panel, string internalName)
    {
        var pushButtonData = new RadioButtonGroupData(internalName);
        return (RadioButtonGroup) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a ComboBox to the Ribbon
    /// </summary>
    /// <returns>The added ComboBox</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a ComboBox already exists in the panel</exception>
    public static ComboBox AddComboBox(this RibbonPanel panel, string internalName)
    {
        var pushButtonData = new ComboBoxData(internalName);
        return (ComboBox) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a TextBox to the Ribbon
    /// </summary>
    /// <returns>The added TextBox</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a TextBox already exists in the panel</exception>
    public static TextBox AddTextBox(this RibbonPanel panel, string internalName)
    {
        var pushButtonData = new TextBoxData(internalName);
        return (TextBox) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a PushButton to the PullDownButton
    /// </summary>
    /// <returns>The newly added PushButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PushButton already exists in the PushButton</exception>
    public static PushButton AddPushButton(this PulldownButton pullDownButton, Type command, string buttonText)
    {
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command).Location, command.FullName);
        return pullDownButton.AddPushButton(pushButtonData);
    }

    /// <summary>
    ///     Adds a PushButton to the PullDownButton
    /// </summary>
    /// <returns>The newly added PushButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PushButton already exists in the PushButton</exception>
    public static PushButton AddPushButton<TCommand>(this PulldownButton pullDownButton, string buttonText) where TCommand : IExternalCommand
    {
        var command = typeof(TCommand);
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command).Location, command.FullName);
        return pullDownButton.AddPushButton(pushButtonData);
    }

    /// <summary>
    ///     Adds a 16x16px-96dpi image from the URI source
    /// </summary>
    /// <param name="button">Button to which the icon will be added</param>
    /// <param name="uri">Relative or Absolute path to the icon</param>
    /// <example>button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16.png")</example>
    public static void SetImage(this RibbonButton button, string uri)
    {
        button.Image = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
    }

    /// <summary>
    ///     Adds a 32x32px-96dpi image from the URI source
    /// </summary>
    /// <param name="button">Button to which the icon will be added</param>
    /// <param name="uri">Relative or Absolute path to the icon</param>
    /// <example>button.SetLargeImage("/RevitAddIn;component/Resources/Icons/RibbonIcon32.png")</example>
    public static void SetLargeImage(this RibbonButton button, string uri)
    {
        button.LargeImage = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
    }
}