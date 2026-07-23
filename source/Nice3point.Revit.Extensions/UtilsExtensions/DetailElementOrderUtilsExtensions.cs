

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.DetailElementOrderUtils"/> class.
/// </summary>
[PublicAPI]
public static class DetailElementOrderUtilsExtensions
{
    /// <param name="detailElementId">The detail element.</param>
    extension(Element detailElementId)
    {
        /// <summary>
        ///    Indicates if the element is a detail element that participates in detail draw ordering in the view.
        /// </summary>
        /// <param name="view">The view in which the detail appears.</param>
        /// <returns>True if the detail element is orderable in the view, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool IsDetailElement(View view)
        {
            return DetailElementOrderUtils.IsDetailElement(detailElementId.Document, view, detailElementId.Id);
        }

        /// <summary>
        ///    Moves the given detail instance one step closer to the front of all other detail instances in the view.
        /// </summary>
        /// <param name="view">The view in which the detail appears.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    The element detailElementId is not a detail or it does not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void BringForward(View view)
        {
            DetailElementOrderUtils.BringForward(detailElementId.Document, view, detailElementId.Id);
        }

        /// <summary>
        ///    Places the given detail instance in the front of all other detail instances in the view.
        /// </summary>
        /// <param name="view">The view in which the detail appears.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    The element detailElementId is not a detail or it does not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void BringToFront(View view)
        {
            DetailElementOrderUtils.BringToFront(detailElementId.Document, view, detailElementId.Id);
        }

        /// <summary>
        ///    Moves the given detail instance one step closer to the back of all other detail instances in the view.
        /// </summary>
        /// <param name="view">The view in which the detail appears.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    The element detailElementId is not a detail or it does not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void SendBackward(View view)
        {
            DetailElementOrderUtils.SendBackward(detailElementId.Document, view, detailElementId.Id);
        }

        /// <summary>Places the given detail instance behind all detail instances in the view.</summary>
        /// <param name="view">The view in which the detail appears.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    The element detailElementId is not a detail or it does not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void SendToBack(View view)
        {
            DetailElementOrderUtils.SendToBack(detailElementId.Document, view, detailElementId.Id);
        }
    }

    /// <param name="elementId">The detail element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>
        ///    Indicates if the element is a detail element that participates in detail draw ordering in the view.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="view">The view in which the detail appears.</param>
        /// <returns>True if the detail element is orderable in the view, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool IsDetailElement(Document document, View view)
        {
            return DetailElementOrderUtils.IsDetailElement(document, view, elementId);
        }

        /// <summary>
        ///    Moves the given detail instance one step closer to the front of all other detail instances in the view.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="view">The view in which the detail appears.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    The element detailElementId is not a detail or it does not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void BringForward(Document document, View view)
        {
            DetailElementOrderUtils.BringForward(document, view, elementId);
        }

        /// <summary>
        ///    Places the given detail instance in the front of all other detail instances in the view.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="view">The view in which the detail appears.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    The element detailElementId is not a detail or it does not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void BringToFront(Document document, View view)
        {
            DetailElementOrderUtils.BringToFront(document, view, elementId);
        }

        /// <summary>
        ///    Moves the given detail instance one step closer to the back of all other detail instances in the view.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="view">The view in which the detail appears.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    The element detailElementId is not a detail or it does not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void SendBackward(Document document, View view)
        {
            DetailElementOrderUtils.SendBackward(document, view, elementId);
        }

        /// <summary>Places the given detail instance behind all detail instances in the view.</summary>
        /// <param name="document">The document.</param>
        /// <param name="view">The view in which the detail appears.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    The element detailElementId is not a detail or it does not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void SendToBack(Document document, View view)
        {
            DetailElementOrderUtils.SendToBack(document, view, elementId);
        }
    }

    /// <param name="detailElementIds">The detail element ids.</param>
    extension(ICollection<ElementId> detailElementIds)
    {
        /// <summary>
        ///    Indicates if the elements are all detail elements that participate in detail draw ordering in the view.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="view">The view in which the details appear.</param>
        /// <returns>True if the detail elements are orderable in the view, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool AreDetailElements(Document document, View view)
        {
            return DetailElementOrderUtils.AreDetailElements(document, view, detailElementIds);
        }

        /// <summary>
        ///    Moves the given detail instances one step closer to the front of all other detail instances in the view,
        ///    while keeping the order of the given ones.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="view">The view in which the details appear.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    detailElementIds is empty or it contains elements that do not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void BringForward(Document document, View view)
        {
            DetailElementOrderUtils.BringForward(document, view, detailElementIds);
        }

        /// <summary>
        ///    Places the given detail instances in the front of all other detail instances in the view, while
        ///    keeping the order of the given ones.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="view">The view in which the details appear.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    detailElementIds is empty or it contains elements that do not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void BringToFront(Document document, View view)
        {
            DetailElementOrderUtils.BringToFront(document, view, detailElementIds);
        }

        /// <summary>
        ///    Moves the given detail instances one step closer to the back with relation to all other detail
        ///    instances in the view, while keeping the order of the given ones.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="view">The view in which the details appear.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    detailElementIds is empty or it contains elements that do not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void SendBackward(Document document, View view)
        {
            DetailElementOrderUtils.SendBackward(document, view, detailElementIds);
        }

        /// <summary>
        ///    Places the given detail instances behind all other detail instances in the view, while keeping
        ///    the order of the given ones.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="view">The view in which the details appear.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    detailElementIds is empty or it contains elements that do not participate in detail draw ordering. Details must be visible in the view.
        ///    -or-
        ///    In 3d families, detail draw order can only be adjusted in views that are parallel to the document's X, Y or Z axes.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void SendToBack(Document document, View view)
        {
            DetailElementOrderUtils.SendToBack(document, view, detailElementIds);
        }
    }
#if REVIT2024_OR_GREATER

    /// <param name="view">The source view.</param>
    extension(View view)
    {
        /// <summary>
        ///    Returns the given detail elements according to the currently specified draw order for the detail elements in a given view.
        /// </summary>
        /// <remarks>
        ///    The sort order is from back to front, with earlier elements drawing first and appearing under later elements.
        /// </remarks>
        /// <param name="detailIdsToSort">The detail to be sorted by draw order.</param>
        /// <returns>
        ///    The detail ids sorted from back to front, with earlier elements drawing first and appearing under later elements.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document does not support detail draw order.  Only projects and 3d families support draw order.  2d families and in-place families do not support draw order.
        ///    -or-
        ///    detailIdsToSort is empty or it contains elements are not visible in the view.
        ///    -or-
        ///    detailIdsToSort is empty or it contains elements that do not participate in detail draw ordering. Details must be visible in the view.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public IList<ElementId> GetDrawOrderForDetails(ISet<ElementId> detailIdsToSort)
        {
            return DetailElementOrderUtils.GetDrawOrderForDetails(view, detailIdsToSort);
        }
    }
#endif
}