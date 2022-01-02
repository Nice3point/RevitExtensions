using Autodesk.Revit.DB;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Element Extensions
/// </summary>
public static class ElementExtensions
{
    /// <summary>
    ///     Returns the parameter found on an instance or type
    /// </summary>
    [CanBeNull]
    [Pure]
    public static Parameter GetParameter(this Element element, BuiltInParameter parameter)
    {
        var instanceParameter = element.get_Parameter(parameter);
        if (instanceParameter is not null && instanceParameter.HasValue) return instanceParameter;
        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;
        var elementType = element.Document.GetElement(elementTypeId);
        var symbolParameter = elementType.get_Parameter(parameter);
        return symbolParameter ?? instanceParameter;
    }

    /// <summary>
    ///     Returns the parameter found on an instance or type
    /// </summary>
    [CanBeNull]
    [Pure]
    public static Parameter GetParameter(this Element element, string parameter)
    {
        var instanceParameter = element.LookupParameter(parameter);
        if (instanceParameter is not null && instanceParameter.HasValue) return instanceParameter;
        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;
        var elementType = element.Document.GetElement(elementTypeId);
        var symbolParameter = elementType.LookupParameter(parameter);
        return symbolParameter ?? instanceParameter;
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
    public static void Mirror(this Element element, Plane plane)
    {
        ElementTransformUtils.MirrorElement(element.Document, element.Id, plane);
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
    public static void Move(this Element element, double deltaX, double deltaY, double deltaZ)
    {
        ElementTransformUtils.MoveElement(element.Document, element.Id, new XYZ(deltaX, deltaY, deltaZ));
    }

    /// <summary>
    ///     Moves the element by the specified vector
    /// </summary>
    /// <param name="element">The element to move</param>
    /// <param name="vector">The translation vector for the elements</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///     If we are not able to move the element (for example, if it is pinned) or move operation failed
    /// </exception>
    public static void Move(this Element element, XYZ vector)
    {
        ElementTransformUtils.MoveElement(element.Document, element.Id, vector);
    }

    /// <summary>Rotates an element about the given axis and angle</summary>
    /// <param name="element">The element to rotate</param>
    /// <param name="axis">The axis of rotation</param>
    /// <param name="angle">The angle of rotation in radians</param>
    public static void Rotate(this Element element, Line axis, double angle)
    {
        ElementTransformUtils.RotateElement(element.Document, element.Id, axis, angle);
    }

    /// <summary>Determines whether element can be mirrored</summary>
    /// <returns>True if the element can be mirrored</returns>
    public static bool CanBeMirrored(this Element element)
    {
        return ElementTransformUtils.CanMirrorElement(element.Document, element.Id);
    }
}