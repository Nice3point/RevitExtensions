

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.WorksharingUtils"/> class.
/// </summary>
[PublicAPI]
public static class WorksharingUtilsExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.GetCheckoutStatus(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public CheckoutStatus GetCheckoutStatus()
        {
            return WorksharingUtils.GetCheckoutStatus(element.Document, element.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.GetCheckoutStatus(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,out System.String)"/>
        [Pure]
        public CheckoutStatus GetCheckoutStatus(out string owner)
        {
            return WorksharingUtils.GetCheckoutStatus(element.Document, element.Id, out owner);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.GetWorksharingTooltipInfo(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public WorksharingTooltipInfo GetWorksharingTooltipInfo()
        {
            return WorksharingUtils.GetWorksharingTooltipInfo(element.Document, element.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.GetModelUpdatesStatus(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public ModelUpdatesStatus GetModelUpdatesStatus()
        {
            return WorksharingUtils.GetModelUpdatesStatus(element.Document, element.Id);
        }
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.GetCheckoutStatus(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public CheckoutStatus GetCheckoutStatus(Document document)
        {
            return WorksharingUtils.GetCheckoutStatus(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.GetCheckoutStatus(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,out System.String)"/>
        [Pure]
        public CheckoutStatus GetCheckoutStatus(Document document, out string owner)
        {
            return WorksharingUtils.GetCheckoutStatus(document, elementId, out owner);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.GetWorksharingTooltipInfo(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public WorksharingTooltipInfo GetWorksharingTooltipInfo(Document document)
        {
            return WorksharingUtils.GetWorksharingTooltipInfo(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.GetModelUpdatesStatus(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public ModelUpdatesStatus GetModelUpdatesStatus(Document document)
        {
            return WorksharingUtils.GetModelUpdatesStatus(document, elementId);
        }
    }

    /// <param name="modelPath">The path to the central model.</param>
    extension(ModelPath modelPath)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.CreateNewLocal(Autodesk.Revit.DB.ModelPath,Autodesk.Revit.DB.ModelPath)"/>
        public ModelPath CreateNewLocal(ModelPath target)
        {
            WorksharingUtils.CreateNewLocal(modelPath, target);
            return target;
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.GetUserWorksetInfo(Autodesk.Revit.DB.ModelPath)"/>
        [Pure]
        public IList<WorksetPreview> GetUserWorksetInfo()
        {
            return WorksharingUtils.GetUserWorksetInfo(modelPath);
        }
    }

    /// <param name="document">The document containing the elements and worksets.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.RelinquishOwnership(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.RelinquishOptions,Autodesk.Revit.DB.TransactWithCentralOptions)"/>
        public RelinquishedItems RelinquishOwnership(RelinquishOptions generalCategories, TransactWithCentralOptions? options)
        {
            return WorksharingUtils.RelinquishOwnership(document, generalCategories, options);
        }
    }

    /// <param name="worksets">The source worksets ids.</param>
    extension(ISet<WorksetId> worksets)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.CheckoutWorksets(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.WorksetId})"/>
        public ICollection<WorksetId> CheckoutWorksets(Document document)
        {
            return WorksharingUtils.CheckoutWorksets(document, worksets);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.CheckoutWorksets(Autodesk.Revit.DB.Document,System.Collections.Generic.ISet{Autodesk.Revit.DB.WorksetId},Autodesk.Revit.DB.TransactWithCentralOptions)"/>
        public ICollection<WorksetId> CheckoutWorksets(Document document, TransactWithCentralOptions options)
        {
            return WorksharingUtils.CheckoutWorksets(document, worksets, options);
        }
    }

    /// <param name="elementsToCheckout">The ids of the elements to attempt to check out.</param>
    extension(ISet<ElementId> elementsToCheckout)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.CheckoutElements(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        public ICollection<ElementId> CheckoutElements(Document document)
        {
            return WorksharingUtils.CheckoutElements(document, elementsToCheckout);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WorksharingUtils.CheckoutElements(Autodesk.Revit.DB.Document,System.Collections.Generic.ISet{Autodesk.Revit.DB.ElementId},Autodesk.Revit.DB.TransactWithCentralOptions)"/>
        public ICollection<ElementId> CheckoutElements(Document document, TransactWithCentralOptions? options)
        {
            return WorksharingUtils.CheckoutElements(document, elementsToCheckout, options);
        }
    }
}