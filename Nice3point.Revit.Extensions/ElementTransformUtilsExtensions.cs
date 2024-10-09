namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ElementTransformUtils"/> class.
/// </summary>
public static class ElementTransformUtilsExtensions
{
    /// <summary>Determines whether element can be mirrored</summary>
    /// <returns>True if the element can be mirrored</returns>
    [Pure]
    public static bool CanBeMirrored(this Element element)
    {
        return ElementTransformUtils.CanMirrorElement(element.Document, element.Id);
    }

    /// <summary>Determines whether elements can be mirrored.</summary>
    /// <param name="document">The document where the elements reside.</param>
    /// <param name="elementIds">The elements identified by id.</param>
    /// <returns>True if the elements can be mirrored.</returns>
    public static void CanMirrorElements(this ICollection<ElementId> elementIds, Document document)
    {
        ElementTransformUtils.CanMirrorElements(document, elementIds);
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

    /// <summary>Mirrors a set of elements about a given plane.</summary>
    /// <remarks>
    ///    Optionally, copies of the elements can be created prior to the operation and mirroring is then performed on the copies instead of the original elements.
    /// </remarks>
    /// <param name="document">The document that owns the elements.</param>
    /// <param name="elementsToMirror">The set of elements to mirror.</param>
    /// <param name="plane">The mirror plane.</param>
    /// <param name="mirrorCopies">
    ///    True if mirroring should be performed on copies of the elements, leaving the original elements intact.
    ///    False if no copies should be created and the elements should be mirrored directly.
    /// </param>
    /// <returns>
    ///    A collection of ids of newly created elements - mirrored copies. It is empty if the mirrorCopies arguments is false.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    elementsToMirror cannot be mirrored.
    ///    -or-
    ///    The given element id set is empty.
    ///    -or-
    ///    One or more elements in elementsToMirror do not exist in the document.
    ///    -or-
    ///    Some of the elements cannot be copied, because they belong to different views.
    ///    -or-
    ///    The input set of elements contains Sketch members along with other elements or there is no active Sketch edit mode.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Thrown when the elements cannot be moved (e.g. due to some of the elements being pinned).
    /// </exception>
    public static ICollection<ElementId> MirrorElements(this ICollection<ElementId> elementsToMirror, Document document, Plane plane, bool mirrorCopies)
    {
        return ElementTransformUtils.MirrorElements(document, elementsToMirror, plane, mirrorCopies);
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

    /// <summary>Moves a set of elements by a given transformation.</summary>
    /// <param name="document">The document that owns the elements.</param>
    /// <param name="elementsToMove">The set of elements to move.</param>
    /// <param name="translation">The translation vector for the elements.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The given element id set is empty.
    ///    -or-
    ///    One or more elements in elementsToMove do not exist in the document.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    If we are not able to move all the elements (for example, if one or more elements is pinned).
    ///    -or-
    ///    Move operation failed.
    /// </exception>
    public static void MoveElements(this ICollection<ElementId> elementsToMove, Document document, XYZ translation)
    {
        ElementTransformUtils.MoveElements(document, elementsToMove, translation);
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

    /// <summary>Rotates a set of elements about the given axis and angle.</summary>
    /// <param name="document">The document that owns the elements.</param>
    /// <param name="elementsToRotate">The set of elements to rotate.</param>
    /// <param name="axis">The axis of rotation.</param>
    /// <param name="angle">The angle of rotation in radians.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The given element id set is empty.
    ///    -or-
    ///    One or more elements in elementsToRotate do not exist in the document.
    /// </exception>
    public static void RotateElements(this ICollection<ElementId> elementsToRotate, Document document, Line axis, double angle)
    {
        ElementTransformUtils.RotateElements(document, elementsToRotate, axis, angle);
    }

    /// <summary>
    ///    Returns a transformation that is applied to elements when copying from one view to another view.
    /// </summary>
    /// <remarks>
    ///    Both source and destination views must be 2D graphics views capable of drawing details and view-specific elements (floor and ceiling plans, elevations, sections, drafting views.)
    ///    The result is a transformation needed to copy an element from drawing plane of the source view to the drawing plane of the destination view.
    ///    The destination view can be in the same document as the source view.
    ///    The destination view can be the same as the source view.
    /// </remarks>
    /// <param name="sourceView">The source view</param>
    /// <param name="destinationView">The destination view</param>
    /// <returns>The transformation from source view to destination view.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The specified view cannot be used as a source or destination for copying elements between two views.
    /// </exception>
    [Pure]
    public static Transform GetTransformFromViewToView(this View sourceView, View destinationView)
    {
        return ElementTransformUtils.GetTransformFromViewToView(sourceView, destinationView);
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

    /// <summary>Copies a set of elements from source view to destination view.</summary>
    /// <remarks>
    ///      <p>This method can be used for both view-specific and model elements.</p>
    ///      <p>Both source and destination views must be 2D graphics views capable of drawing details and view-specific elements (floor and ceiling plans, elevations, sections, drafting views.)
    /// Drafting views cannot be used as a destination for model elements.</p>
    ///      <p>The pasted elements are repositioned to ensure proper placement in the destination view (e.g. elevation is changed when copying from a level to a different level.)
    /// Additional transformation within the destination view can be performed by providing additionalTransform argument. This additional transformation must be within the plane of the destination view.</p>
    ///      <p>The destination view can be in the same document as the source view.</p>
    ///      <p>The destination view can be the same as the source view.</p>
    ///      <p>All view-specific elements in the set must be specific to the source view. Elements specific to views other than the source view or to multiple views cannot be copied.</p>
    ///      <p>This method performs rehosting of elements where applicable.</p>
    ///    </remarks>
    /// <param name="sourceView">
    ///    The view in the source document that contains the elements to copy.
    /// </param>
    /// <param name="elementsToCopy">The set of elements to copy.</param>
    /// <param name="destinationView">
    ///    The view in the destination document that the elements will be pasted into.
    /// </param>
    /// <param name="additionalTransform">
    ///    The transform for the new elements, in addition to the transformation between the source and destination views. Can be <see langword="null" /> if no transform is required. The transformation must be within the plane of the destination view.
    /// </param>
    /// <param name="options">
    ///    Optional settings. Can be <see langword="null" /> if default settings should be used.
    /// </param>
    /// <returns>The ids of the newly created copied elements.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The given element id set is empty.
    ///    -or-
    ///    The specified view cannot be used as a source or destination for copying elements between two views.
    ///    -or-
    ///    Some of the elements cannot be copied, because they belong to a different document.
    ///    -or-
    ///    Some of the elements cannot be copied, because they belong to a different view.
    ///    -or-
    ///    The elements cannot be copied into the destination view. Drafting views cannot contain model elements.
    ///    -or-
    ///    The transformation is not within the plane of the destination view.
    ///    -or-
    ///    The input set of elements contains Sketch members along with other elements and the Sketch Id of those members isn't in the set.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    It is not allowed to copy Sketch members between non-parallel sketches.
    ///    -or-
    ///    The elements cannot be copied.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.OperationCanceledException">
    ///    User cancelled the operation.
    /// </exception>
    public static ICollection<ElementId> CopyElements(this ICollection<ElementId> elementsToCopy,
        View sourceView,
        View destinationView,
        Transform? additionalTransform,
        CopyPasteOptions? options)
    {
        return ElementTransformUtils.CopyElements(sourceView, elementsToCopy, destinationView, additionalTransform, options);
    }

    /// <summary>Copies a set of elements from source document to destination document.</summary>
    /// <remarks>
    ///   <p>Copies are placed at their respective original locations or locations specified by the optional transformation.</p>
    ///   <p>This method can be used for copying non-view specific elements only. For copying view-specific elements, use the view-specific form of the CopyElements method.</p>
    ///   <p>The destination document can be the same as the source document.</p>
    ///   <p>This method performs rehosting of elements where applicable.</p>
    /// </remarks>
    /// <param name="sourceDocument">The document that contains the elements to copy.</param>
    /// <param name="elementsToCopy">The set of elements to copy.</param>
    /// <param name="destinationDocument">
    ///    The destination document to paste the elements into.
    /// </param>
    /// <param name="transform">
    ///    The transform for the new elements. Can be <see langword="null" /> if no transform is required.
    /// </param>
    /// <param name="options">
    ///    Optional settings. Can be <see langword="null" /> if default settings should be used.
    /// </param>
    /// <returns>The ids of the newly created copied elements.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The given element id set is empty.
    ///    -or-
    ///    One or more elements in elementsToCopy do not exist in the document.
    ///    -or-
    ///    Some of the elements cannot be copied, because they are view-specific.
    ///    -or-
    ///    The input set of elements contains Sketch members along with other elements or there is no active Sketch edit mode.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    It is not allowed to copy Sketch members between non-parallel sketches.
    ///    -or-
    ///    The elements cannot be copied.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.OperationCanceledException">
    ///    User cancelled the operation.
    /// </exception>
    public static ICollection<ElementId> CopyElements(this ICollection<ElementId> elementsToCopy,
        Document sourceDocument,
        Document destinationDocument,
        Transform? transform,
        CopyPasteOptions? options)
    {
        return ElementTransformUtils.CopyElements(sourceDocument, elementsToCopy, destinationDocument, transform, options);
    }

    /// <summary>
    ///    Copies a set of elements and places the copies at a location indicated by a given translation.
    /// </summary>
    /// <remarks>
    ///    This method is not suitable for elements that are hosted in other elements as it does not perform rehosting. If you need to rehost your elements in addition
    ///    to copying them, use one of the other CopyElements() overloads.
    /// </remarks>
    /// <param name="document">The document that owns the elements.</param>
    /// <param name="elementsToCopy">The set of elements to copy.</param>
    /// <param name="translation">The translation vector for the new elements.</param>
    /// <returns>The ids of the newly created copied elements.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The given element id set is empty.
    ///    -or-
    ///    One or more elements in elementsToCopy do not exist in the document.
    ///    -or-
    ///    Some of the elements cannot be copied, because they belong to different views.
    ///    -or-
    ///    The input set of elements contains Sketch members along with other elements or there is no active Sketch edit mode.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    It is not allowed to copy Sketch members between non-parallel sketches.
    ///    -or-
    ///    If we are not able to copy all the elements.
    /// </exception>
    public static ICollection<ElementId> CopyElements(this ICollection<ElementId> elementsToCopy, Document document, XYZ translation)
    {
        return ElementTransformUtils.CopyElements(document, elementsToCopy, translation);
    }
}