// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ElementTransformUtils"/> class.
/// </summary>
[PublicAPI]
public static class ElementTransformUtilsExtensions
{
    /// <param name="element">The element to transform.</param>
    extension(Element element)
    {
        /// <summary>Determines whether element can be mirrored</summary>
        /// <returns>True if the element can be mirrored</returns>
        [Pure]
        public bool CanBeMirrored()
        {
            return ElementTransformUtils.CanMirrorElement(element.Document, element.Id);
        }

        /// <summary>
        ///     Copies an element and places the copy at a location indicated by a given transformation
        /// </summary>
        /// <param name="vector">The translation vector for the new element</param>
        /// <returns>The ids of the newly created copied elements. More than one element may be created due to dependencies</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     If we are not able to copy the element
        /// </exception>
        public ICollection<ElementId> Copy(XYZ vector)
        {
            return ElementTransformUtils.CopyElement(element.Document, element.Id, vector);
        }

        /// <summary>Creates a mirrored copy of an element about a given plane</summary>
        /// <param name="plane">The mirror plane</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     Element cannot be mirrored
        /// </exception>
        public Element Mirror(Plane plane)
        {
            ElementTransformUtils.MirrorElement(element.Document, element.Id, plane);
            return element;
        }

        /// <summary>
        ///     Moves the element by the specified offset
        /// </summary>
        /// <param name="deltaX">Offset along the X axis</param>
        /// <param name="deltaY">Offset along the Y axis</param>
        /// <param name="deltaZ">Offset along the Z axis</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     If we are not able to move the element (for example, if it is pinned) or move operation failed
        /// </exception>
        public Element Move(double deltaX = 0d, double deltaY = 0d, double deltaZ = 0d)
        {
            ElementTransformUtils.MoveElement(element.Document, element.Id, new XYZ(deltaX, deltaY, deltaZ));
            return element;
        }

        /// <summary>
        ///     Moves the element by the specified vector
        /// </summary>
        /// <param name="vector">The translation vector for the elements</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     If we are not able to move the element (for example, if it is pinned) or move operation failed
        /// </exception>
        public Element Move(XYZ vector)
        {
            ElementTransformUtils.MoveElement(element.Document, element.Id, vector);
            return element;
        }

        /// <summary>Rotates an element about the given axis and angle</summary>
        /// <param name="axis">The axis of rotation</param>
        /// <param name="angle">The angle of rotation in radians</param>
        public Element Rotate(Line axis, double angle)
        {
            ElementTransformUtils.RotateElement(element.Document, element.Id, axis, angle);
            return element;
        }

        /// <summary>
        ///     Copies an element and places the copy at a location indicated by a given transformation
        /// </summary>
        /// <param name="deltaX">Offset along the X axis</param>
        /// <param name="deltaY">Offset along the Y axis</param>
        /// <param name="deltaZ">Offset along the Z axis</param>
        /// <returns>The ids of the newly created copied elements. More than one element may be created due to dependencies</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     If we are not able to copy the element
        /// </exception>
        public ICollection<ElementId> Copy(double deltaX, double deltaY, double deltaZ)
        {
            return ElementTransformUtils.CopyElement(element.Document, element.Id, new XYZ(deltaX, deltaY, deltaZ));
        }
    }

    /// <param name="view">The source view.</param>
    extension(View view)
    {
        /// <summary>
        ///    Returns a transformation that is applied to elements when copying from this view to another view.
        /// </summary>
        /// <remarks>
        ///    Both this view and the destination view must be 2D graphics views capable of drawing details and view-specific elements (floor and ceiling plans, elevations, sections, drafting views.)
        ///    The result is a transformation needed to copy an element from the drawing plane of this view to the drawing plane of the destination view.
        ///    The destination view can be in the same document as this view.
        ///    The destination view can be the same as this view.
        /// </remarks>
        /// <param name="destinationView">The destination view</param>
        /// <returns>The transformation from this view to the destination view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The specified view cannot be used as a source or destination for copying elements between two views.
        /// </exception>
        [Pure]
        public Transform GetTransformFromViewToView(View destinationView)
        {
            return ElementTransformUtils.GetTransformFromViewToView(view, destinationView);
        }
    }

    /// <param name="elements">The source elements collection.</param>
    extension(ICollection<ElementId> elements)
    {
        /// <summary>Determines whether elements can be mirrored.</summary>
        /// <param name="document">The document where the elements reside.</param>
        /// <returns>True if the elements can be mirrored.</returns>
        [Pure]
        public bool CanMirrorElements(Document document)
        {
            return ElementTransformUtils.CanMirrorElements(document, elements);
        }

        /// <summary>
        ///    Copies a set of elements and places the copies at a location indicated by a given translation.
        /// </summary>
        /// <remarks>
        ///    This method is not suitable for elements that are hosted in other elements as it does not perform rehosting. If you need to rehost your elements in addition
        ///    to copying them, use one of the other CopyElements() overloads.
        /// </remarks>
        /// <param name="document">The document that owns the elements.</param>
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
        public ICollection<ElementId> CopyElements(Document document, XYZ translation)
        {
            return ElementTransformUtils.CopyElements(document, elements, translation);
        }

        /// <summary>Copies a set of elements from source document to destination document.</summary>
        /// <remarks>
        ///   <p>Copies are placed at their respective original locations or locations specified by the optional transformation.</p>
        ///   <p>This method can be used for copying non-view specific elements only. For copying view-specific elements, use the view-specific form of the CopyElements method.</p>
        ///   <p>The destination document can be the same as the source document.</p>
        ///   <p>This method performs rehosting of elements where applicable.</p>
        /// </remarks>
        /// <param name="sourceDocument">The document that contains the elements to copy.</param>
        /// <param name="destinationDocument">
        ///    The destination document to paste the elements into.
        /// </param>
        /// <param name="transform">
        ///    The transform for the new elements.
        /// </param>
        /// <param name="options">
        ///    Optional settings.
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
        public ICollection<ElementId> CopyElements(Document sourceDocument,
            Document destinationDocument,
            Transform transform,
            CopyPasteOptions options)
        {
            return ElementTransformUtils.CopyElements(sourceDocument, elements, destinationDocument, transform, options);
        }

        /// <summary>Copies a set of elements from source document to destination document.</summary>
        /// <remarks>
        ///   <p>Copies are placed at their respective original locations or locations specified by the optional transformation.</p>
        ///   <p>This method can be used for copying non-view specific elements only. For copying view-specific elements, use the view-specific form of the CopyElements method.</p>
        ///   <p>The destination document can be the same as the source document.</p>
        ///   <p>This method performs rehosting of elements where applicable.</p>
        /// </remarks>
        /// <param name="sourceDocument">The document that contains the elements to copy.</param>
        /// <param name="destinationDocument">
        ///    The destination document to paste the elements into.
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
        public ICollection<ElementId> CopyElements(Document sourceDocument, Document destinationDocument)
        {
            return ElementTransformUtils.CopyElements(sourceDocument, elements, destinationDocument, null, null);
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
        /// <param name="destinationView">
        ///    The view in the destination document that the elements will be pasted into.
        /// </param>
        /// <param name="additionalTransform">
        ///    The transform for the new elements, in addition to the transformation between the source and destination views. The transformation must be within the plane of the destination view.
        /// </param>
        /// <param name="options">
        ///    Optional settings.
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
        public ICollection<ElementId> CopyElements(View sourceView,
            View destinationView,
            Transform additionalTransform,
            CopyPasteOptions options)
        {
            return ElementTransformUtils.CopyElements(sourceView, elements, destinationView, additionalTransform, options);
        }

        /// <summary>Copies a set of elements from source view to destination view.</summary>
        /// <remarks>
        ///      <p>This method can be used for both view-specific and model elements.</p>
        ///      <p>Both source and destination views must be 2D graphics views capable of drawing details and view-specific elements (floor and ceiling plans, elevations, sections, drafting views.)
        /// Drafting views cannot be used as a destination for model elements.</p>
        ///      <p>The pasted elements are repositioned to ensure proper placement in the destination view (e.g. elevation is changed when copying from a level to a different level.)</p>
        ///      <p>The destination view can be in the same document as the source view.</p>
        ///      <p>The destination view can be the same as the source view.</p>
        ///      <p>All view-specific elements in the set must be specific to the source view. Elements specific to views other than the source view or to multiple views cannot be copied.</p>
        ///      <p>This method performs rehosting of elements where applicable.</p>
        ///    </remarks>
        /// <param name="sourceView">
        ///    The view in the source document that contains the elements to copy.
        /// </param>
        /// <param name="destinationView">
        ///    The view in the destination document that the elements will be pasted into.
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
        public ICollection<ElementId> CopyElements(View sourceView, View destinationView)
        {
            return ElementTransformUtils.CopyElements(sourceView, elements, destinationView, null, null);
        }

        /// <summary>Mirrors a set of elements about a given plane.</summary>
        /// <remarks>
        ///    Optionally, copies of the elements can be created prior to the operation and mirroring is then performed on the copies instead of the original elements.
        /// </remarks>
        /// <param name="document">The document that owns the elements.</param>
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
        public ICollection<ElementId> MirrorElements(Document document, Plane plane, bool mirrorCopies)
        {
            return ElementTransformUtils.MirrorElements(document, elements, plane, mirrorCopies);
        }

        /// <summary>Moves a set of elements by a given transformation.</summary>
        /// <param name="document">The document that owns the elements.</param>
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
        public ICollection<ElementId> MoveElements(Document document, XYZ translation)
        {
            ElementTransformUtils.MoveElements(document, elements, translation);
            return elements;
        }

        /// <summary>Rotates a set of elements about the given axis and angle.</summary>
        /// <param name="document">The document that owns the elements.</param>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="angle">The angle of rotation in radians.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The given element id set is empty.
        ///    -or-
        ///    One or more elements in elementsToRotate do not exist in the document.
        /// </exception>
        public ICollection<ElementId> RotateElements(Document document, Line axis, double angle)
        {
            ElementTransformUtils.RotateElements(document, elements, axis, angle);
            return elements;
        }
    }
}