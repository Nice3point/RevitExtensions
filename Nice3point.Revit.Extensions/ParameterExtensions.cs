using Autodesk.Revit.DB;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Parameter Extensions
/// </summary>
public static class ParameterExtensions
{
    /// <summary>
    ///     Provides access to the boolean value within the parameter
    /// </summary>
    /// <param name="parameter">The parameter</param>
    /// <returns>The bool value contained in the parameter</returns>
    /// <remarks>The AsBool method should only be used if the StorageType property returns that the internal contents of the parameter is an integer</remarks>
    [Pure]
    public static bool AsBool([NotNull] this Parameter parameter)
    {
        return parameter.AsInteger() == 1;
    }

    /// <summary>
    ///     Provides access to the Color within the parameter
    /// </summary>
    /// <param name="parameter">The parameter</param>
    /// <returns>The Color value contained in the parameter</returns>
    /// <remarks>The AsColor method should only be used if the StorageType property returns that the internal contents of the parameter is an integer</remarks>
    [Pure]
    public static Color AsColor([NotNull] this Parameter parameter)
    {
        var argb = parameter.AsInteger();
        return new Color((byte) (argb & byte.MaxValue), (byte) ((argb >> 8) & byte.MaxValue), (byte) ((argb >> 16) & byte.MaxValue));
    }

    /// <summary>
    ///     Provides access to the Element within the parameter
    /// </summary>
    /// <param name="parameter">The parameter</param>
    /// <returns>The element contained in the parameter as an ElementId</returns>
    /// <remarks>The AsElement method should only be used if the StorageType property returns that the internal contents of the parameter is an ElementId</remarks>
    [Pure]
    [CanBeNull]
    public static Element AsElement([NotNull] this Parameter parameter)
    {
        var elementId = parameter.AsElementId();
        return elementId == ElementId.InvalidElementId ? null : parameter.Element.Document.GetElement(elementId);
    }

    /// <summary>
    ///     Provides access to the Element within the parameter
    /// </summary>
    /// <param name="parameter">The parameter</param>
    /// <returns>The element contained in the parameter as an ElementId</returns>
    /// <remarks>The AsElement method should only be used if the StorageType property returns that the internal contents of the parameter is an ElementId</remarks>
    /// <typeparam name="T">Type inherited from <see cref="Autodesk.Revit.DB.Element" /></typeparam>
    [Pure]
    [CanBeNull]
    public static T AsElement<T>([NotNull] this Parameter parameter) where T : Element
    {
        var elementId = parameter.AsElementId();
        return elementId == ElementId.InvalidElementId ? null : parameter.Element.Document.GetElement(elementId) as T;
    }

    /// <summary>Sets the parameter to a new bool value</summary>
    /// <param name="parameter">The parameter</param>
    /// <param name="value">The new bool value to which the parameter is to be set</param>
    /// <returns>The Set method will return True if the parameter was successfully set to the new value, otherwise false</returns>
    /// <remarks>You should only use this method if the StorageType property reports the type of the parameter as an integer</remarks>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">The parameter is read-only</exception>
    public static bool Set([NotNull] this Parameter parameter, bool value)
    {
        return parameter.Set(value ? 1 : 0);
    }

    /// <summary>Sets the parameter to a new color</summary>
    /// <param name="parameter">The parameter</param>
    /// <param name="value">The new color to which the parameter is to be set</param>
    /// <returns>The Set method will return True if the parameter was successfully set to the new value, otherwise false</returns>
    /// <remarks>You should only use this method if the StorageType property reports the type of the parameter as an integer</remarks>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">The parameter is read-only</exception>
    public static bool Set([NotNull] this Parameter parameter, Color value)
    {
        return parameter.Set((value.Red << 0) | (value.Green << 8) | (value.Blue << 16));
    }
}