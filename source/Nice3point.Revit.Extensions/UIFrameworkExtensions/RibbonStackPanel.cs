using System.Reflection;
using Autodesk.Revit.UI;
using Autodesk.Windows;
using RibbonItem = Autodesk.Revit.UI.RibbonItem;
using RibbonPanel = Autodesk.Revit.UI.RibbonPanel;

namespace Nice3point.Revit.Extensions.UIFrameworkExtensions;

internal sealed class RibbonStackPanel : IRibbonStackPanel
{
    private const int MaxStackPanelItemsCount = 5;

    private RibbonRowPanel _currentPanel;
    private readonly RibbonPanel _host;
    private readonly MethodInfo _addItemMethod;
    private readonly Autodesk.Windows.RibbonPanel _rawPanel;

    internal RibbonStackPanel(RibbonPanel host)
    {
        _host = host;
        var ribbonPanelType = host.GetType();

        _addItemMethod = ribbonPanelType
            .GetMethod("addItemToRowPanel", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!;

        _rawPanel = (Autodesk.Windows.RibbonPanel)ribbonPanelType
            .GetField("m_RibbonPanel", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly)!
            .GetValue(host)!;

        _currentPanel = new RibbonRowPanel();
    }

    public PushButton AddPushButton<TCommand>(string buttonText) where TCommand : IExternalCommand, new()
    {
        var command = typeof(TCommand);
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command)!.Location, command.FullName);
        return (PushButton)AddStakedItem(pushButtonData);
    }

    public PulldownButton AddPullDownButton(string buttonText)
    {
        return AddPullDownButton(buttonText, Guid.NewGuid().ToString());
    }

    public PulldownButton AddPullDownButton(string buttonText, string internalName)
    {
        var pushButtonData = new PulldownButtonData(internalName, buttonText);
        return (PulldownButton)AddStakedItem(pushButtonData);
    }

    public SplitButton AddSplitButton(string buttonText)
    {
        return AddSplitButton(buttonText, Guid.NewGuid().ToString());
    }

    public SplitButton AddSplitButton(string buttonText, string internalName)
    {
        var pushButtonData = new SplitButtonData(internalName, buttonText);
        return (SplitButton)AddStakedItem(pushButtonData);
    }

    public ComboBox AddComboBox()
    {
        return AddComboBox(Guid.NewGuid().ToString());
    }

    public ComboBox AddComboBox(string internalName)
    {
        var pushButtonData = new ComboBoxData(internalName);
        return (ComboBox)AddStakedItem(pushButtonData);
    }

    public TextBox AddTextBox()
    {
        return AddTextBox(Guid.NewGuid().ToString());
    }

    public TextBox AddTextBox(string internalName)
    {
        var pushButtonData = new TextBoxData(internalName);
        return (TextBox)AddStakedItem(pushButtonData);
    }

    public void AddLabel(string labelText)
    {
        var ribbonLabel = new RibbonLabel
        {
            Text = labelText
        };

        AddStakedItem(ribbonLabel);
    }

    private RibbonItem AddStakedItem(RibbonItemData itemData)
    {
        if (_currentPanel.Items.Count >= MaxStackPanelItemsCount)
        {
            _currentPanel = new RibbonRowPanel();
        }

        if ((_currentPanel.Items.Count + 1) % 2 == 0)
        {
            _currentPanel.Items.Add(new RibbonRowBreak());
        }

        object item;
        try
        {
            item = _addItemMethod.Invoke(_host, [_currentPanel, itemData])!;
        }
        catch (TargetInvocationException exception)
        {
            throw exception.InnerException!;
        }

        if (_currentPanel.Items.Count < 2)
        {
            _rawPanel.Source.Items.Add(_currentPanel);
        }

        return (RibbonItem)item;
    }

    private void AddStakedItem(Autodesk.Windows.RibbonItem itemData)
    {
        if (_currentPanel.Items.Count >= MaxStackPanelItemsCount)
        {
            _currentPanel = new RibbonRowPanel();
        }

        if ((_currentPanel.Items.Count + 1) % 2 == 0)
        {
            _currentPanel.Items.Add(new RibbonRowBreak());
        }

        _currentPanel.Items.Add(itemData);

        if (_currentPanel.Items.Count < 2)
        {
            _rawPanel.Source.Items.Add(_currentPanel);
        }
    }
}