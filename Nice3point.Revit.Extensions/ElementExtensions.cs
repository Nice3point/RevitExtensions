namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Element Extensions
/// </summary>
[PublicAPI]
public static class ElementExtensions
{
#if REVIT2022_OR_GREATER
    /// <summary>
    ///     Retrieves a parameter from the instance or symbol given identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">Identifier of the built-in parameter</param>
    /// <param name="snoopType">True if you want to snoop the symbol parameter</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     ForgeTypeId does not identify a built-in parameter. See Parameter.IsBuiltInParameter(ForgeTypeId) and Parameter.GetParameterTypeId(BuiltInParameter).
    /// </exception>
    [Pure]
    [Obsolete("Use FindParameter() instead")]
    public static Parameter? GetParameter(this Element element, ForgeTypeId parameter, bool snoopType)
    {
        return FindParameter(element, parameter);
    }

#endif
    /// <summary>
    ///     Retrieves a parameter from the instance or symbol given identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">The built-in parameter ID</param>
    [Pure]
    [Obsolete("Use FindParameter() instead")]
    public static Parameter? GetParameter(this Element element, BuiltInParameter parameter)
    {
        return FindParameter(element, parameter);
    }

    /// <summary>
    ///     Retrieves a parameter from the instance or symbol given identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">The name of the parameter to be retrieved</param>
    [Pure]
    [Obsolete("Use FindParameter() instead")]
    public static Parameter? GetParameter(this Element element, string parameter)
    {
        return FindParameter(element, parameter);
    }

#if REVIT2022_OR_GREATER
    /// <summary>
    ///     Find a parameter in the instance or symbol by identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">Identifier of the built-in parameter</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     ForgeTypeId does not identify a built-in parameter. See Parameter.IsBuiltInParameter(ForgeTypeId) and Parameter.GetParameterTypeId(BuiltInParameter).
    /// </exception>
    [Pure]
    public static Parameter? FindParameter(this Element element, ForgeTypeId parameter)
    {
        var instanceParameter = element.GetParameter(parameter);
        if (instanceParameter is not null) return instanceParameter;

        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;

        var elementType = element.Document.GetElement(elementTypeId);
        return elementType.GetParameter(parameter);
    }

#endif

    /// <summary>
    ///     Find a parameter in the instance or symbol by identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">The built-in parameter ID</param>
    [Pure]
    public static Parameter? FindParameter(this Element element, BuiltInParameter parameter)
    {
        var instanceParameter = element.get_Parameter(parameter);
        if (instanceParameter is not null) return instanceParameter;

        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;

        var elementType = element.Document.GetElement(elementTypeId);
        return elementType.get_Parameter(parameter);
    }

    /// <summary>
    ///     Find a parameter in the instance or symbol by identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="definition">The internal or external definition of the parameter</param>
    [Pure]
    public static Parameter? FindParameter(this Element element, Definition definition)
    {
        var instanceParameter = element.get_Parameter(definition);
        if (instanceParameter is not null) return instanceParameter;

        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;

        var elementType = element.Document.GetElement(elementTypeId);
        return elementType.get_Parameter(definition);
    }

    /// <summary>
    ///     Find a parameter in the instance or symbol by identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="guid">The unique id associated with the shared parameter</param>
    [Pure]
    public static Parameter? FindParameter(this Element element, Guid guid)
    {
        var instanceParameter = element.get_Parameter(guid);
        if (instanceParameter is not null) return instanceParameter;

        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;

        var elementType = element.Document.GetElement(elementTypeId);
        return elementType.get_Parameter(guid);
    }

    /// <summary>
    ///     Find a parameter in the instance or symbol by identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">The name of the parameter to be found</param>
    [Pure]
    public static Parameter? FindParameter(this Element element, string parameter)
    {
        var instanceParameter = element.LookupParameter(parameter);
        if (instanceParameter is not null) return instanceParameter;

        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;

        var elementType = element.Document.GetElement(elementTypeId);
        return elementType.LookupParameter(parameter);
    }

    /// <summary>
    ///     Copies an element and places the copy at a location indicated by a given transformation
    /// </summary>
    /// <param name="element">The element to copy</param>
    /// <param name="deltaX">Offset along the X axis</param>
    /// <param name="deltaY">Offset along the Y axis</param>
    /// <param name="deltaZ">Offset along the Z axis</param>
    /// <returns>The ids of the newly created copied elements. More than one element may be created due to dependencies</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     If we are not able to copy the element
    /// </exception>
    public static ICollection<ElementId> Copy(this Element element, double deltaX, double deltaY, double deltaZ)
    {
        return ElementTransformUtils.CopyElement(element.Document, element.Id, new XYZ(deltaX, deltaY, deltaZ));
    }

    /// <summary>
    ///     Copies an element and places the copy at a location indicated by a given transformation
    /// </summary>
    /// <param name="element">The element to copy</param>
    /// <param name="vector">The translation vector for the new element</param>
    /// <returns>The ids of the newly created copied elements. More than one element may be created due to dependencies</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     If we are not able to copy the element
    /// </exception>
    public static ICollection<ElementId> Copy(this Element element, XYZ vector)
    {
        return ElementTransformUtils.CopyElement(element.Document, element.Id, vector);
    }

    /// <summary>Creates a mirrored copy of an element about a given plane</summary>
    /// <param name="element">The element to mirror</param>
    /// <param name="plane">The mirror plane</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     Element cannot be mirrored or element does not exist in the document
    /// </exception>
    public static Element Mirror(this Element element, Plane plane)
    {
        ElementTransformUtils.MirrorElement(element.Document, element.Id, plane);
        return element;
    }

    /// <summary>
    ///     Moves the element by the specified offset
    /// </summary>
    /// <param name="element">The element to move</param>
    /// <param name="deltaX">Offset along the X axis</param>
    /// <param name="deltaY">Offset along the Y axis</param>
    /// <param name="deltaZ">Offset along the Z axis</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     If we are not able to move the element (for example, if it is pinned) or move operation failed
    /// </exception>
    public static Element Move(this Element element, double deltaX = 0d, double deltaY = 0d, double deltaZ = 0d)
    {
        ElementTransformUtils.MoveElement(element.Document, element.Id, new XYZ(deltaX, deltaY, deltaZ));
        return element;
    }

    /// <summary>
    ///     Moves the element by the specified vector
    /// </summary>
    /// <param name="element">The element to move</param>
    /// <param name="vector">The translation vector for the elements</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     If we are not able to move the element (for example, if it is pinned) or move operation failed
    /// </exception>
    public static Element Move(this Element element, XYZ vector)
    {
        ElementTransformUtils.MoveElement(element.Document, element.Id, vector);
        return element;
    }

    /// <summary>Rotates an element about the given axis and angle</summary>
    /// <param name="element">The element to rotate</param>
    /// <param name="axis">The axis of rotation</param>
    /// <param name="angle">The angle of rotation in radians</param>
    public static Element Rotate(this Element element, Line axis, double angle)
    {
        ElementTransformUtils.RotateElement(element.Document, element.Id, axis, angle);
        return element;
    }

    /// <summary>Determines whether element can be mirrored</summary>
    /// <returns>True if the element can be mirrored</returns>
    public static bool CanBeMirrored(this Element element)
    {
        return ElementTransformUtils.CanMirrorElement(element.Document, element.Id);
    }
}