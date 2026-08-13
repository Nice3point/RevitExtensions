// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.DetailElementOrderUtils" /> class.
/// </summary>
[PublicAPI]
public static class DetailElementOrderUtilsExtensions
{
    /// <param name="detailElementId">The detail element.</param>
    extension(Element detailElementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.IsDetailElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public bool IsDetailElement(View view)
        {
            return DetailElementOrderUtils.IsDetailElement(detailElementId.Document, view, detailElementId.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.BringForward(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementId)" />
        public void BringForward(View view)
        {
            DetailElementOrderUtils.BringForward(detailElementId.Document, view, detailElementId.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.BringToFront(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementId)" />
        public void BringToFront(View view)
        {
            DetailElementOrderUtils.BringToFront(detailElementId.Document, view, detailElementId.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.SendBackward(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementId)" />
        public void SendBackward(View view)
        {
            DetailElementOrderUtils.SendBackward(detailElementId.Document, view, detailElementId.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.SendToBack(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementId)" />
        public void SendToBack(View view)
        {
            DetailElementOrderUtils.SendToBack(detailElementId.Document, view, detailElementId.Id);
        }
    }

    /// <param name="elementId">The detail element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.IsDetailElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public bool IsDetailElement(Document document, View view)
        {
            return DetailElementOrderUtils.IsDetailElement(document, view, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.BringForward(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementId)" />
        public void BringForward(Document document, View view)
        {
            DetailElementOrderUtils.BringForward(document, view, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.BringToFront(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementId)" />
        public void BringToFront(Document document, View view)
        {
            DetailElementOrderUtils.BringToFront(document, view, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.SendBackward(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementId)" />
        public void SendBackward(Document document, View view)
        {
            DetailElementOrderUtils.SendBackward(document, view, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.SendToBack(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,Autodesk.Revit.DB.ElementId)" />
        public void SendToBack(Document document, View view)
        {
            DetailElementOrderUtils.SendToBack(document, view, elementId);
        }
    }

    /// <param name="detailElementIds">The detail element ids.</param>
    extension(ICollection<ElementId> detailElementIds)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.AreDetailElements(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})" />
        [Pure]
        public bool AreDetailElements(Document document, View view)
        {
            return DetailElementOrderUtils.AreDetailElements(document, view, detailElementIds);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.BringForward(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})" />
        public void BringForward(Document document, View view)
        {
            DetailElementOrderUtils.BringForward(document, view, detailElementIds);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.BringToFront(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})" />
        public void BringToFront(Document document, View view)
        {
            DetailElementOrderUtils.BringToFront(document, view, detailElementIds);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.SendBackward(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})" />
        public void SendBackward(Document document, View view)
        {
            DetailElementOrderUtils.SendBackward(document, view, detailElementIds);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.SendToBack(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.View,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})" />
        public void SendToBack(Document document, View view)
        {
            DetailElementOrderUtils.SendToBack(document, view, detailElementIds);
        }
    }
#if REVIT2024_OR_GREATER
    /// <param name="view">The source view.</param>
    extension(View view)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.DetailElementOrderUtils.GetDrawOrderForDetails(Autodesk.Revit.DB.View,System.Collections.Generic.ISet{Autodesk.Revit.DB.ElementId})"/>
        [Pure]
        public IList<ElementId> GetDrawOrderForDetails(ISet<ElementId> detailIdsToSort)
        {
            return DetailElementOrderUtils.GetDrawOrderForDetails(view, detailIdsToSort);
        }
    }
#endif
}
