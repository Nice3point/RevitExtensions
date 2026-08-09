

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.PartUtils"/> class.
/// </summary>
[PublicAPI]
public static class PartUtilsExtensions
{
    /// <param name="part">The source part.</param>
    extension(Part part)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.IsMergedPart(Autodesk.Revit.DB.Part)"/>
        public bool IsMergedPart => PartUtils.IsMergedPart(part);

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.IsPartDerivedFromLink(Autodesk.Revit.DB.Part)"/>
        public bool IsPartDerivedFromLink => PartUtils.IsPartDerivedFromLink(part);

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetChainLengthToOriginal(Autodesk.Revit.DB.Part)"/>
        [Pure]
        public int GetChainLengthToOriginal()
        {
            return PartUtils.GetChainLengthToOriginal(part);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetMergedParts(Autodesk.Revit.DB.Part)"/>
        [Pure]
        public ICollection<ElementId> GetMergedParts()
        {
            return PartUtils.GetMergedParts(part);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetSplittingCurves(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public IList<Curve> GetSplittingCurves()
        {
            return PartUtils.GetSplittingCurves(part.Document, part.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetSplittingCurves(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,out Autodesk.Revit.DB.Plane)"/>
        [Pure]
        public IList<Curve> GetSplittingCurves(out Plane sketchPlane)
        {
            return PartUtils.GetSplittingCurves(part.Document, part.Id, out sketchPlane);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetSplittingElements(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public ISet<ElementId> GetSplittingElements()
        {
            return PartUtils.GetSplittingElements(part.Document, part.Id);
        }
    }

    /// <param name="partMaker">The source part maker.</param>
    extension(PartMaker partMaker)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetPartMakerMethodToDivideVolumeFW(Autodesk.Revit.DB.PartMaker)"/>
        [Pure]
        public PartMakerMethodToDivideVolumes? GetPartMakerMethodToDivideVolumeFw()
        {
            return PartUtils.GetPartMakerMethodToDivideVolumeFW(partMaker);
        }
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.HasAssociatedParts(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public bool HasAssociatedParts => PartUtils.HasAssociatedParts(element.Document, element.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetAssociatedParts(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Boolean,System.Boolean)"/>
        [Pure]
        public ICollection<ElementId> GetAssociatedParts(bool includePartsWithAssociatedParts, bool includeAllChildren)
        {
            return PartUtils.GetAssociatedParts(element.Document, element.Id, includePartsWithAssociatedParts, includeAllChildren);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetAssociatedPartMaker(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public PartMaker? GetAssociatedPartMaker()
        {
            return PartUtils.GetAssociatedPartMaker(element.Document, element.Id);
        }
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.HasAssociatedParts(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public bool HasAssociatedParts(Document document)
        {
            return PartUtils.HasAssociatedParts(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetAssociatedParts(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Boolean,System.Boolean)"/>
        [Pure]
        public ICollection<ElementId> GetAssociatedParts(Document document, bool includePartsWithAssociatedParts, bool includeAllChildren)
        {
            return PartUtils.GetAssociatedParts(document, elementId, includePartsWithAssociatedParts, includeAllChildren);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetAssociatedPartMaker(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public PartMaker? GetAssociatedPartMaker(Document document)
        {
            return PartUtils.GetAssociatedPartMaker(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetSplittingCurves(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public IList<Curve> GetSplittingCurves(Document document)
        {
            return PartUtils.GetSplittingCurves(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetSplittingCurves(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,out Autodesk.Revit.DB.Plane)"/>
        [Pure]
        public IList<Curve> GetSplittingCurves(Document document, out Plane sketchPlane)
        {
            return PartUtils.GetSplittingCurves(document, elementId, out sketchPlane);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetSplittingElements(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public ISet<ElementId> GetSplittingElements(Document document)
        {
            return PartUtils.GetSplittingElements(document, elementId);
        }
    }

    /// <param name="elements">The source element ids.</param>
    extension(ICollection<ElementId> elements)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.AreElementsValidForCreateParts(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        [Pure]
        public bool AreElementsValidForCreateParts(Document document)
        {
            return PartUtils.AreElementsValidForCreateParts(document, elements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.ArePartsValidForDivide(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        [Pure]
        public bool ArePartsValidForDivide(Document document)
        {
            return PartUtils.ArePartsValidForDivide(document, elements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.ArePartsValidForMerge(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        [Pure]
        public bool ArePartsValidForMerge(Document document)
        {
            return PartUtils.ArePartsValidForMerge(document, elements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.CreateParts(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        public void CreateParts(Document document)
        {
            PartUtils.CreateParts(document, elements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.CreateMergedPart(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        public PartMaker? CreateMergedPart(Document document)
        {
            return PartUtils.CreateMergedPart(document, elements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.FindMergeableClusters(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        [Pure]
        public IList<ICollection<ElementId>> FindMergeableClusters(Document document)
        {
            return PartUtils.FindMergeableClusters(document, elements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.DivideParts(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},System.Collections.Generic.IList{Autodesk.Revit.DB.Curve},Autodesk.Revit.DB.ElementId)"/>
        public PartMaker DivideParts(Document document, ICollection<ElementId> intersectingReferenceIds, IList<Curve> curveArray, ElementId sketchPlaneId)
        {
            return PartUtils.DivideParts(document, elements, intersectingReferenceIds, curveArray, sketchPlaneId);
        }
    }

    /// <param name="hostOrLinkElements">The host or link element ids.</param>
    extension(ICollection<LinkElementId> hostOrLinkElements)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.CreateParts(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.LinkElementId})"/>
        public void CreateParts(Document document)
        {
            PartUtils.CreateParts(document, hostOrLinkElements);
        }
    }

    /// <param name="linkElementId">The link element id.</param>
    extension(LinkElementId linkElementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.IsValidForCreateParts(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.LinkElementId)"/>
        [Pure]
        public bool IsValidForCreateParts(Document document)
        {
            return PartUtils.IsValidForCreateParts(document, linkElementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.HasAssociatedParts(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.LinkElementId)"/>
        [Pure]
        public bool HasAssociatedParts(Document document)
        {
            return PartUtils.HasAssociatedParts(document, linkElementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetAssociatedParts(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.LinkElementId,System.Boolean,System.Boolean)"/>
        [Pure]
        public ICollection<ElementId> GetAssociatedParts(Document document, bool includePartsWithAssociatedParts, bool includeAllChildren)
        {
            return PartUtils.GetAssociatedParts(document, linkElementId, includePartsWithAssociatedParts, includeAllChildren);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.PartUtils.GetAssociatedPartMaker(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.LinkElementId)"/>
        [Pure]
        public PartMaker? GetAssociatedPartMaker(Document document)
        {
            return PartUtils.GetAssociatedPartMaker(document, linkElementId);
        }
    }
}