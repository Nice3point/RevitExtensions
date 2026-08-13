// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ReferenceableViewUtils" /> class.
/// </summary>
[PublicAPI]
public static class ReferenceableViewUtilsExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ReferenceableViewUtils.GetReferencedViewId(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public ElementId GetReferencedViewId()
        {
            return ReferenceableViewUtils.GetReferencedViewId(element.Document, element.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ReferenceableViewUtils.ChangeReferencedView(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        public void ChangeReferencedView(ElementId desiredViewId)
        {
            ReferenceableViewUtils.ChangeReferencedView(element.Document, element.Id, desiredViewId);
        }
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ReferenceableViewUtils.GetReferencedViewId(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public ElementId GetReferencedViewId(Document document)
        {
            return ReferenceableViewUtils.GetReferencedViewId(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ReferenceableViewUtils.ChangeReferencedView(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        public void ChangeReferencedView(Document document, ElementId desiredViewId)
        {
            ReferenceableViewUtils.ChangeReferencedView(document, elementId, desiredViewId);
        }
    }
}
