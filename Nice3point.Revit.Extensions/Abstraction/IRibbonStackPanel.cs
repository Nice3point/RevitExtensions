// ReSharper disable once CheckNamespace

namespace Autodesk.Revit.UI;

/// <summary>
///     Represents an interface for adding UI elements to a vertical stack panel on the Revit Ribbon.
/// </summary>
/// <remarks>By default, StackPanel accommodates 3 elements vertically. If the added items exceed the maximum threshold, they will be automatically added to a new column.</remarks>
public interface IRibbonStackPanel
{
    /// <summary>
    ///     Adds a PushButton to the vertical stack panel.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command that implements <see cref="Autodesk.Revit.UI.IExternalCommand"/>.</typeparam>
    /// <param name="buttonText">The label text for the PushButton.</param>
    /// <returns>The added PushButton.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a PushButton with the same Command already exists in the panel.
    /// </exception>
    PushButton AddPushButton<TCommand>(string buttonText) where TCommand : IExternalCommand, new();

    /// <summary>
    ///     Adds a PullDownButton to the vertical stack panel.
    /// </summary>
    /// <param name="buttonText">The label text for the PullDownButton.</param>
    /// <returns>The added PullDownButton.</returns>
    PulldownButton AddPullDownButton(string buttonText);

    /// <summary>
    ///     Adds a PullDownButton to the vertical stack panel with a unique internal name.
    /// </summary>
    /// <param name="buttonText">The label text for the PullDownButton.</param>
    /// <param name="internalName">A unique internal name for the PullDownButton.</param>
    /// <returns>The added PullDownButton.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a PullDownButton with the same internalName already exists in the panel.
    /// </exception>
    PulldownButton AddPullDownButton(string buttonText, string internalName);

    /// <summary>
    ///     Adds a SplitButton to the vertical stack panel.
    /// </summary>
    /// <param name="buttonText">The label text for the SplitButton.</param>
    /// <returns>The added SplitButton.</returns>
    SplitButton AddSplitButton(string buttonText);
    
    /// <summary>
    ///     Adds a SplitButton to the vertical stack panel with a unique internal name.
    /// </summary>
    /// <param name="buttonText">The label text for the SplitButton.</param>
    /// <param name="internalName">A unique internal name for the SplitButton.</param>
    /// <returns>The added SplitButton.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a SplitButton with the same internalName already exists in the panel.
    /// </exception>
    SplitButton AddSplitButton(string buttonText, string internalName);
    
    /// <summary>
    ///     Adds a ComboBox to the vertical stack panel.
    /// </summary>
    /// <returns>The added ComboBox.</returns>
    ComboBox AddComboBox();
    
    /// <summary>
    ///     Adds a ComboBox to the vertical stack panel with a unique internal name.
    /// </summary>
    /// <param name="internalName">A unique internal name for the ComboBox.</param>
    /// <returns>The added ComboBox.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a ComboBox with the same internalName already exists in the panel.
    /// </exception>
    ComboBox AddComboBox(string internalName);
    
    /// <summary>
    ///     Adds a TextBox to the vertical stack panel.
    /// </summary>
    /// <returns>The added TextBox.</returns>
    TextBox AddTextBox();
    
    /// <summary>
    ///     Adds a TextBox to the vertical stack panel with a unique internal name.
    /// </summary>
    /// <param name="internalName">A unique internal name for the TextBox.</param>
    /// <returns>The added TextBox.</returns>
    /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
    ///     Thrown if a TextBox with the same internalName already exists in the panel.
    /// </exception>
    TextBox AddTextBox(string internalName);
}