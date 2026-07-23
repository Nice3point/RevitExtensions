

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ReferenceableViewUtils"/> class.
/// </summary>
[PublicAPI]
public static class ReferenceableViewUtilsExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>
        ///    Gets the id of the view referenced by a reference view (such as a reference section or reference callout).
        /// </summary>
        /// <returns>The id of the referenced view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    referenceId is not a valid reference view.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ElementId GetReferencedViewId()
        {
            return ReferenceableViewUtils.GetReferencedViewId(element.Document, element.Id);
        }

        /// <summary>
        ///    Changes a particular reference view (such as a reference section or reference callout) to refer to a different View.
        /// </summary>
        /// <remarks>
        ///    Reference views may not refer to a View in which their own graphics (such as the section or callout
        ///    graphics) will appear.  If the reference view's ViewFamilyType is not appropriate
        ///    for the new View, Revit will automatically change the ViewFamilyType during regeneration.  This
        ///    typically occurs when the referenced view is changed from a model View to a drafting View or
        ///    vice-versa.
        /// </remarks>
        /// <param name="desiredViewId">
        ///    The id of the View that the reference section or callout will refer to.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    referenceId is not a valid reference view.
        ///    -or-
        ///    desiredViewId is not a view that can be referenced by referenceId.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void ChangeReferencedView(ElementId desiredViewId)
        {
            ReferenceableViewUtils.ChangeReferencedView(element.Document, element.Id, desiredViewId);
        }
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>
        ///    Gets the id of the view referenced by a reference view (such as a reference section or reference callout).
        /// </summary>
        /// <param name="document">The document containing the elements.</param>
        /// <returns>The id of the referenced view.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    referenceId is not a valid reference view.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ElementId GetReferencedViewId(Document document)
        {
            return ReferenceableViewUtils.GetReferencedViewId(document, elementId);
        }

        /// <summary>
        ///    Changes a particular reference view (such as a reference section or reference callout) to refer to a different View.
        /// </summary>
        /// <remarks>
        ///    Reference views may not refer to a View in which their own graphics (such as the section or callout
        ///    graphics) will appear.  If the reference view's ViewFamilyType is not appropriate
        ///    for the new View, Revit will automatically change the ViewFamilyType during regeneration.  This
        ///    typically occurs when the referenced view is changed from a model View to a drafting View or
        ///    vice-versa.
        /// </remarks>
        /// <param name="document">The document containing the elements.</param>
        /// <param name="desiredViewId">
        ///    The id of the View that the reference section or callout will refer to.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    referenceId is not a valid reference view.
        ///    -or-
        ///    desiredViewId is not a view that can be referenced by referenceId.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void ChangeReferencedView(Document document, ElementId desiredViewId)
        {
            ReferenceableViewUtils.ChangeReferencedView(document, elementId, desiredViewId);
        }
    }
}