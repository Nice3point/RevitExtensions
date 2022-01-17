﻿using System.Drawing;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
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
    [NotNull]
    public static RibbonPanel CreatePanel(this UIControlledApplication application, string panelName)
    {
        var ribbonPanel = application.GetRibbonPanels(Tab.AddIns).FirstOrDefault(panel => panel.Name.Equals(panelName));
        return ribbonPanel ?? application.CreateRibbonPanel(panelName);
    }

    /// <summary>
    ///     Creates a panel in the specified tab
    /// </summary>
    /// <returns>New or existing Ribbon panel</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">panelName is Empty</exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">If more than 100 panels were created</exception>
    [NotNull]
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
    [NotNull]
    public static PushButton AddPushButton(this RibbonPanel panel, Type command, string buttonText)
    {
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command).Location, command.FullName);
        return (PushButton) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a PullDownButton to the Ribbon
    /// </summary>
    /// <returns>The added PullDownButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PullDownButton already exists in the panel</exception>
    [NotNull]
    public static PulldownButton AddPullDownButton(this RibbonPanel panel, string name, string buttonText)
    {
        var pushButtonData = new PulldownButtonData(name, buttonText);
        return (PulldownButton) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a SplitButton to the Ribbon
    /// </summary>
    /// <returns>The added SplitButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a SplitButton already exists in the panel</exception>
    [NotNull]
    public static SplitButton AddSplitButton(this RibbonPanel panel, string name, string buttonText)
    {
        var pushButtonData = new SplitButtonData(name, buttonText);
        return (SplitButton) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a RadioButtonGroup to the Ribbon
    /// </summary>
    /// <returns>The added RadioButtonGroup</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a RadioButtonGroup already exists in the panel</exception>
    [NotNull]
    public static RadioButtonGroup AddRadioButtonGroup(this RibbonPanel panel, string name)
    {
        var pushButtonData = new RadioButtonGroupData(name);
        return (RadioButtonGroup) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a ComboBox to the Ribbon
    /// </summary>
    /// <returns>The added ComboBox</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a ComboBox already exists in the panel</exception>
    [NotNull]
    public static ComboBox AddComboBox(this RibbonPanel panel, string name)
    {
        var pushButtonData = new ComboBoxData(name);
        return (ComboBox) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a TextBox to the Ribbon
    /// </summary>
    /// <returns>The added TextBox</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a TextBox already exists in the panel</exception>
    [NotNull]
    public static TextBox AddTextBox(this RibbonPanel panel, string name)
    {
        var pushButtonData = new TextBoxData(name);
        return (TextBox) panel.AddItem(pushButtonData);
    }

    /// <summary>
    ///     Adds a PushButton to the PullDownButton
    /// </summary>
    /// <returns>The newly added PushButton</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">Thrown when a PushButton already exists in the PushButton</exception>
    [NotNull]
    public static PushButton AddPushButton(this PulldownButton pullDownButton, Type command, string buttonText)
    {
        var pushButtonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command).Location, command.FullName);
        return pullDownButton.AddPushButton(pushButtonData);
    }

    /// <summary>
    ///     Set image from the Resource.resx
    /// </summary>
    /// <param name="button">Button to which the icon will be added</param>
    /// <para name="bitmap">Image from resources</para>
    /// <example>button.SetImage("Resources.RibbonIcon")</example>
    public static void SetImage(this RibbonButton button, Bitmap bitmap)
    {
        button.Image = ConvertFromImage(bitmap).Resize(16);
    }

    /// <summary>
    ///     Set large image from the Resource.resx
    /// </summary>
    /// <param name="button">Button to which the icon will be added</param>
    /// <param name="bitmap">Image from resources</param>
    /// <example>button.SetLargeImage("Resources.RibbonIcon")</example>
    public static void SetLargeImage(this RibbonButton button, Bitmap bitmap)
    {
        button.LargeImage = ConvertFromImage(bitmap).Resize(32);
    }
    private static BitmapSource ConvertFromImage(Bitmap image)
    {
        IntPtr hBitmap = image.GetHbitmap();

        try
        {
            var bs = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return bs;

        }
        finally
        {
            DeleteObject(hBitmap);
        }

    }
    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);
    /// <summary>
    /// Resize ImageResource
    /// </summary>
    /// <param name="imageSource"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private static ImageSource Resize(this ImageSource imageSource, int size)
    {
        return Thumbnail(imageSource, size);
    }

    private static ImageSource Thumbnail(ImageSource source, int size)
    {
        Rect rect = new Rect(0, 0, size, size);
        DrawingVisual drawingVisual = new DrawingVisual();
        using (DrawingContext drawingContext = drawingVisual.RenderOpen())
        {
            drawingContext.DrawImage(source, rect);
        }
        RenderTargetBitmap resizedImage = new RenderTargetBitmap((int)rect.Width, (int)rect.Height, 96, 96, PixelFormats.Default);
        resizedImage.Render(drawingVisual);

        return resizedImage;
    }
}