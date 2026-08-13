// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ElementTransformUtils" /> class.
/// </summary>
[PublicAPI]
public static class ElementTransformUtilsExtensions
{
    /// <param name="element">The element to transform.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.CanMirrorElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public bool CanBeMirrored => ElementTransformUtils.CanMirrorElement(element.Document, element.Id);

        /// <summary></summary>
        [Pure]
        [Obsolete("Use CanBeMirrored() instead")]
        [CodeTemplate(
            "$expr$.CanMirrorElement()",
            Message = "CanMirrorElement is obsolete, use CanBeMirrored instead",
            ReplaceTemplate = "$expr$.CanBeMirrored",
            ReplaceMessage = "Replace with CanBeMirrored")]
        public bool CanMirrorElement()
        {
            return ElementTransformUtils.CanMirrorElement(element.Document, element.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.CopyElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.XYZ)" />
        public ICollection<ElementId> Copy(XYZ vector)
        {
            return ElementTransformUtils.CopyElement(element.Document, element.Id, vector);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.MirrorElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Plane)" />
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

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.RotateElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Line,System.Double)" />
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

    /// <param name="elementId">The element id to transform.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.CanMirrorElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public bool CanBeMirrored(Document document)
        {
            return ElementTransformUtils.CanMirrorElement(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.CopyElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.XYZ)" />
        public ICollection<ElementId> Copy(Document document, XYZ vector)
        {
            return ElementTransformUtils.CopyElement(document, elementId, vector);
        }

        /// <summary>Copies an element and places the copy at a location indicated by a given transformation</summary>
        /// <param name="document">The document containing the element.</param>
        /// <param name="deltaX">Offset along the X axis</param>
        /// <param name="deltaY">Offset along the Y axis</param>
        /// <param name="deltaZ">Offset along the Z axis</param>
        /// <returns>The ids of the newly created copied elements. More than one element may be created due to dependencies</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     If we are not able to copy the element
        /// </exception>
        public ICollection<ElementId> Copy(Document document, double deltaX, double deltaY, double deltaZ)
        {
            return ElementTransformUtils.CopyElement(document, elementId, new XYZ(deltaX, deltaY, deltaZ));
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.MirrorElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Plane)" />
        /// <returns>The element id after mirroring</returns>
        public ElementId Mirror(Document document, Plane plane)
        {
            ElementTransformUtils.MirrorElement(document, elementId, plane);
            return elementId;
        }

        /// <summary>Moves the element by the specified offset</summary>
        /// <param name="document">The document containing the element.</param>
        /// <param name="deltaX">Offset along the X axis</param>
        /// <param name="deltaY">Offset along the Y axis</param>
        /// <param name="deltaZ">Offset along the Z axis</param>
        /// <returns>The element id after moving</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     If we are not able to move the element (for example, if it is pinned) or move operation failed
        /// </exception>
        public ElementId Move(Document document, double deltaX = 0d, double deltaY = 0d, double deltaZ = 0d)
        {
            ElementTransformUtils.MoveElement(document, elementId, new XYZ(deltaX, deltaY, deltaZ));
            return elementId;
        }

        /// <summary>Moves the element by the specified vector</summary>
        /// <param name="document">The document containing the element.</param>
        /// <param name="vector">The translation vector for the elements</param>
        /// <returns>The element id after moving</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///     If we are not able to move the element (for example, if it is pinned) or move operation failed
        /// </exception>
        public ElementId Move(Document document, XYZ vector)
        {
            ElementTransformUtils.MoveElement(document, elementId, vector);
            return elementId;
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.RotateElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.Line,System.Double)" />
        /// <returns>The element id after rotation</returns>
        public ElementId Rotate(Document document, Line axis, double angle)
        {
            ElementTransformUtils.RotateElement(document, elementId, axis, angle);
            return elementId;
        }
    }

    /// <param name="view">The source view.</param>
    extension(View view)
    {
        /// <summary>
        ///     Returns a transformation that is applied to elements when copying from this view to another view.
        /// </summary>
        /// <remarks>
        ///     Both this view and the destination view must be 2D graphics views capable of drawing details and view-specific elements (floor and ceiling plans, elevations, sections, drafting views.)
        ///     The result is a transformation needed to copy an element from the drawing plane of this view to the drawing plane of the destination view.
        ///     The destination view can be in the same document as this view.
        ///     The destination view can be the same as this view.
        /// </remarks>
        /// <param name="destinationView">The destination view</param>
        /// <returns>The transformation from this view to the destination view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     The specified view cannot be used as a source or destination for copying elements between two views.
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
        /// <summary></summary>
        [Pure]
        [Obsolete("Use CanBeMirrored() instead")]
        [CodeTemplate(
            "$expr$.CanMirrorElements($document$)",
            Message = "CanMirrorElements is obsolete, use CanBeMirrored instead",
            ReplaceTemplate = "$expr$.CanBeMirrored($document$)",
            ReplaceMessage = "Replace with CanBeMirrored()")]
        public bool CanMirrorElements(Document document)
        {
            return ElementTransformUtils.CanMirrorElements(document, elements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.CanMirrorElements(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})" />
        [Pure]
        public bool CanBeMirrored(Document document)
        {
            return ElementTransformUtils.CanMirrorElements(document, elements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.CopyElements(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},Autodesk.Revit.DB.XYZ)" />
        public ICollection<ElementId> CopyElements(Document document, XYZ translation)
        {
            return ElementTransformUtils.CopyElements(document, elements, translation);
        }

        /// <inheritdoc
        ///     cref="Autodesk.Revit.DB.ElementTransformUtils.CopyElements(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Transform,Autodesk.Revit.DB.CopyPasteOptions)" />
        public ICollection<ElementId> CopyElements(Document sourceDocument,
            Document destinationDocument,
            Transform transform,
            CopyPasteOptions options)
        {
            return ElementTransformUtils.CopyElements(sourceDocument, elements, destinationDocument, transform, options);
        }

        /// <inheritdoc
        ///     cref="Autodesk.Revit.DB.ElementTransformUtils.CopyElements(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Transform,Autodesk.Revit.DB.CopyPasteOptions)" />
        public ICollection<ElementId> CopyElements(Document sourceDocument, Document destinationDocument)
        {
            return ElementTransformUtils.CopyElements(sourceDocument, elements, destinationDocument, null, null);
        }

        /// <inheritdoc
        ///     cref="Autodesk.Revit.DB.ElementTransformUtils.CopyElements(Autodesk.Revit.DB.View,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},Autodesk.Revit.DB.View,Autodesk.Revit.DB.Transform,Autodesk.Revit.DB.CopyPasteOptions)" />
        public ICollection<ElementId> CopyElements(View sourceView,
            View destinationView,
            Transform additionalTransform,
            CopyPasteOptions options)
        {
            return ElementTransformUtils.CopyElements(sourceView, elements, destinationView, additionalTransform, options);
        }

        /// <inheritdoc
        ///     cref="Autodesk.Revit.DB.ElementTransformUtils.CopyElements(Autodesk.Revit.DB.View,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},Autodesk.Revit.DB.View,Autodesk.Revit.DB.Transform,Autodesk.Revit.DB.CopyPasteOptions)" />
        public ICollection<ElementId> CopyElements(View sourceView, View destinationView)
        {
            return ElementTransformUtils.CopyElements(sourceView, elements, destinationView, null, null);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.MirrorElements(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},Autodesk.Revit.DB.Plane,System.Boolean)" />
        public ICollection<ElementId> MirrorElements(Document document, Plane plane, bool mirrorCopies)
        {
            return ElementTransformUtils.MirrorElements(document, elements, plane, mirrorCopies);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.MoveElements(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},Autodesk.Revit.DB.XYZ)" />
        public ICollection<ElementId> MoveElements(Document document, XYZ translation)
        {
            ElementTransformUtils.MoveElements(document, elements, translation);
            return elements;
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ElementTransformUtils.RotateElements(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},Autodesk.Revit.DB.Line,System.Double)" />
        public ICollection<ElementId> RotateElements(Document document, Line axis, double angle)
        {
            ElementTransformUtils.RotateElements(document, elements, axis, angle);
            return elements;
        }
    }
}
