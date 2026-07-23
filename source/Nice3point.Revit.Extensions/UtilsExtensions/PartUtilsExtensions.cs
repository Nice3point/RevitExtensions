

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
        /// <summary>Is the Part the result of a merge.</summary>
        /// <returns>True if the Part is the result of a merge operation.</returns>
        public bool IsMergedPart => PartUtils.IsMergedPart(part);

        /// <summary>Is the Part derived from link geometry.</summary>
        public bool IsPartDerivedFromLink => PartUtils.IsPartDerivedFromLink(part);

        /// <summary>
        ///    Calculates the length of the longest chain of divisions/merges to reach to an original non-Part element that is the source of the tested part.
        /// </summary>
        /// <returns>The length of the longest chain.</returns>
        [Pure]
        public int GetChainLengthToOriginal()
        {
            return PartUtils.GetChainLengthToOriginal(part);
        }

        /// <summary>Retrieves the element ids of the source elements of a merged part.</summary>
        /// <returns>
        ///    The element ids of the parts that were merged to create the specified merged part.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The specified Part is not a merged part.
        /// </exception>
        [Pure]
        public ICollection<ElementId> GetMergedParts()
        {
            return PartUtils.GetMergedParts(part);
        }

        /// <summary>Identifies the curves that were used to create the part.</summary>
        /// <returns>
        ///    The curves that created the part. Empty if partId is not a Part or Part is not divided.
        /// </returns>
        [Pure]
        public IList<Curve> GetSplittingCurves()
        {
            return PartUtils.GetSplittingCurves(part.Document, part.Id);
        }

        /// <summary>
        ///    Identifies the curves that were used to create the part and the plane in which they reside.
        /// </summary>
        /// <param name="sketchPlane">The plane in which the division curves were sketched.</param>
        /// <returns>
        ///    The curves that created the part. Empty if partId is not a part or Part is not divided.
        /// </returns>
        [Pure]
        public IList<Curve> GetSplittingCurves(out Plane sketchPlane)
        {
            return PartUtils.GetSplittingCurves(part.Document, part.Id, out sketchPlane);
        }

        /// <summary>
        ///    Identifies the elements ( reference planes, levels, grids ) that were used to create the part.
        /// </summary>
        /// <returns>
        ///    The elements that created the part. Empty if partId is not a Part or Part is not divided.
        /// </returns>
        [Pure]
        public ISet<ElementId> GetSplittingElements()
        {
            return PartUtils.GetSplittingElements(part.Document, part.Id);
        }
    }

    /// <param name="partMaker">The source part maker.</param>
    extension(PartMaker partMaker)
    {
        /// <summary>
        ///    Obtains the object allowing access to the divided volume
        ///    properties of the PartMaker.
        /// </summary>
        /// <returns>
        ///    The object handle. Returns <see langword="null" /> if the
        ///    PartMaker does not represent divided volumes.
        /// </returns>
        [Pure]
        public PartMakerMethodToDivideVolumes? GetPartMakerMethodToDivideVolumeFw()
        {
            return PartUtils.GetPartMakerMethodToDivideVolumeFW(partMaker);
        }
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>Checks if an element has associated parts.</summary>
        /// <returns>True if the element has associated Parts.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public bool HasAssociatedParts => PartUtils.HasAssociatedParts(element.Document, element.Id);

        /// <summary>Returns all Parts that are associated with the given element.</summary>
        /// <param name="includePartsWithAssociatedParts">
        ///    If true, include parts that have associated parts.
        /// </param>
        /// <param name="includeAllChildren">
        ///    If true, return all associated Parts recursively for all children.
        ///    If false, only return immediate children.
        /// </param>
        /// <returns>Parts that are associated to the element.</returns>
        [Pure]
        public ICollection<ElementId> GetAssociatedParts(bool includePartsWithAssociatedParts, bool includeAllChildren)
        {
            return PartUtils.GetAssociatedParts(element.Document, element.Id, includePartsWithAssociatedParts, includeAllChildren);
        }

        /// <summary>Gets associated PartMaker for an element.</summary>
        /// <returns>
        ///    The PartMaker element that is making Parts for this element.
        ///    <see langword="null" /> if there is no associated PartMaker.
        /// </returns>
        [Pure]
        public PartMaker? GetAssociatedPartMaker()
        {
            return PartUtils.GetAssociatedPartMaker(element.Document, element.Id);
        }
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>Checks if an element has associated parts.</summary>
        /// <returns>True if the element has associated Parts.</returns>
        [Pure]
        public bool HasAssociatedParts(Document document)
        {
            return PartUtils.HasAssociatedParts(document, elementId);
        }

        /// <summary>Returns all Parts that are associated with the given element.</summary>
        /// <param name="document">The document of the element.</param>
        /// <param name="includePartsWithAssociatedParts">
        ///    If true, include parts that have associated parts.
        /// </param>
        /// <param name="includeAllChildren">
        ///    If true, return all associated Parts recursively for all children.
        ///    If false, only return immediate children.
        /// </param>
        /// <returns>Parts that are associated to the element.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ICollection<ElementId> GetAssociatedParts(Document document, bool includePartsWithAssociatedParts, bool includeAllChildren)
        {
            return PartUtils.GetAssociatedParts(document, elementId, includePartsWithAssociatedParts, includeAllChildren);
        }

        /// <summary>Gets associated PartMaker for an element.</summary>
        /// <param name="document">The document</param>
        /// <returns>
        ///    The PartMaker element that is making Parts for this element.
        ///    <see langword="null" /> if there is no associated PartMaker.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public PartMaker? GetAssociatedPartMaker(Document document)
        {
            return PartUtils.GetAssociatedPartMaker(document, elementId);
        }

        /// <summary>Identifies the curves that were used to create the part.</summary>
        /// <param name="document">The source document of the part.</param>
        /// <returns>
        ///    The curves that created the part. Empty if partId is not a Part or Part is not divided.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public IList<Curve> GetSplittingCurves(Document document)
        {
            return PartUtils.GetSplittingCurves(document, elementId);
        }

        /// <summary>
        ///    Identifies the curves that were used to create the part and the plane in which they reside.
        /// </summary>
        /// <param name="document">The source document of the part.</param>
        /// <param name="sketchPlane">The plane in which the division curves were sketched.</param>
        /// <returns>
        ///    The curves that created the part. Empty if partId is not a part or Part is not divided.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public IList<Curve> GetSplittingCurves(Document document, out Plane sketchPlane)
        {
            return PartUtils.GetSplittingCurves(document, elementId, out sketchPlane);
        }

        /// <summary>
        ///    Identifies the elements ( reference planes, levels, grids ) that were used to create the part.
        /// </summary>
        /// <param name="document">The source document of the part.</param>
        /// <returns>
        ///    The elements that created the part. Empty if partId is not a Part or Part is not divided.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ISet<ElementId> GetSplittingElements(Document document)
        {
            return PartUtils.GetSplittingElements(document, elementId);
        }
    }

    /// <param name="elements">The source element ids.</param>
    extension(ICollection<ElementId> elements)
    {
        /// <summary>Identifies if the given elements can be used to create parts.</summary>
        /// <param name="document">The document.</param>
        /// <returns>True if all member ids are valid, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool AreElementsValidForCreateParts(Document document)
        {
            return PartUtils.AreElementsValidForCreateParts(document, elements);
        }

        /// <summary>Identifies if provided members are valid for dividing parts.</summary>
        /// <param name="document">The document.</param>
        /// <returns>True if all member ids are valid, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool ArePartsValidForDivide(Document document)
        {
            return PartUtils.ArePartsValidForDivide(document, elements);
        }

        /// <summary>Identifies whether Part elements may be merged.</summary>
        /// <param name="document">The document.</param>
        /// <returns>
        ///    True if all element ids correspond to Part elements,
        ///    none of the parts already has associated parts,
        ///    the parts have contiguous geometry, all report the same materials,
        ///    and all have the same creation and demolition phases.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool ArePartsValidForMerge(Document document)
        {
            return PartUtils.ArePartsValidForMerge(document, elements);
        }

        /// <summary>Creates a new set of parts out of the original elements.</summary>
        /// <remarks>Parts will be added to the model after regeneration.</remarks>
        /// <param name="document">The document containing the elements.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    One or more element ids was not permitted for creating parts.
        ///    Elements should be of a valid category and the ids should be valid and should not already be divided into parts.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void CreateParts(Document document)
        {
            PartUtils.CreateParts(document, elements);
        }

        /// <summary>
        ///    Create a single merged part which represents the Parts
        ///    specified by partsToMerge.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>
        ///    The newly created PartMaker. <see langword="null" /> if no parts are merged.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    One or more element ids was not suitable for merging with the others.
        ///    Specified elements should all be Parts, report the same material,
        ///    creation and demolition phases, and have contiguous geometry.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public PartMaker? CreateMergedPart(Document document)
        {
            return PartUtils.CreateMergedPart(document, elements);
        }

        /// <summary>
        ///    Segregates a set of elements into subsets which are valid for merge.
        /// </summary>
        /// <remarks>
        ///    Element ids in the input set that do not correspond to Part
        ///    elements will be ignored, as will element ids corresponding
        ///    to Part elements that already have associated parts.
        /// </remarks>
        /// <param name="document">The document.</param>
        /// <returns>
        ///    An array of clusters such that all the elements in a single cluster
        ///    are valid for merge. Each cluster will be maximal in that appending
        ///    any of the other Parts specified as input will result in a collection
        ///    that is not valid for merge.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public IList<ICollection<ElementId>> FindMergeableClusters(Document document)
        {
            return PartUtils.FindMergeableClusters(document, elements);
        }

        /// <summary>Creates divided parts out of parts.</summary>
        /// <param name="document">The document containing the parts.</param>
        /// <param name="intersectingReferenceIds">
        ///    Intersecting references that will divide the elements.
        /// </param>
        /// <param name="curveArray">Array of curves that will divide the elements.</param>
        /// <param name="sketchPlaneId">SketchPlane id for the curves that divide the elements.</param>
        /// <returns>
        ///    The newly created PartMaker. <see langword="null" /> if no parts are divided.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    One or more element ids was not permitted for dividing parts.
        ///    Elements should be parts that are not yet divided and maximum distance from an original has not yet been reached.
        ///    -or-
        ///    One or more element ids was not permitted as intersecting references.
        ///    Intersecting references should be levels, grids, or reference planes.
        ///    -or-
        ///    The element id should refer to a valid SketchPlane.
        ///    -or-
        ///    The input curveArray contains at least one helical curve and is not supported for this operation.
        ///    -or-
        ///    The input curveArray contains at least one null pointer and is not supported for this operation.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public PartMaker DivideParts(Document document, ICollection<ElementId> intersectingReferenceIds, IList<Curve> curveArray, ElementId sketchPlaneId)
        {
            return PartUtils.DivideParts(document, elements, intersectingReferenceIds, curveArray, sketchPlaneId);
        }
    }

    /// <param name="hostOrLinkElements">The host or link element ids.</param>
    extension(ICollection<LinkElementId> hostOrLinkElements)
    {
        /// <summary>Creates a new set of parts out of the original elements.</summary>
        /// <remarks>
        ///    Parts will be added to the model after regeneration.
        ///    To get the ids of the parts created by this method use PartUtils.GetAssociatedParts() with the contents of hostOrLinkElementIds.
        /// </remarks>
        /// <param name="document">The document containing the elements.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    One or more element ids was not permitted for creating parts.
        ///    HostOrLinkElements should be of a valid category and the ids should be valid and should not already be divided into parts.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void CreateParts(Document document)
        {
            PartUtils.CreateParts(document, hostOrLinkElements);
        }
    }

    /// <param name="linkElementId">The link element id.</param>
    extension(LinkElementId linkElementId)
    {
        /// <summary>Identifies if the given element can be used to create parts.</summary>
        /// <param name="document">The document of the element</param>
        /// <returns>True if this id is valid, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool IsValidForCreateParts(Document document)
        {
            return PartUtils.IsValidForCreateParts(document, linkElementId);
        }

        /// <summary>Checks if an element has associated parts.</summary>
        /// <param name="document">The document of the element</param>
        /// <returns>True if the element has associated Parts.</returns>
        [Pure]
        public bool HasAssociatedParts(Document document)
        {
            return PartUtils.HasAssociatedParts(document, linkElementId);
        }

        /// <summary>Returns all Parts that are associated with the given element</summary>
        /// <param name="document">The document of the element</param>
        /// <param name="includePartsWithAssociatedParts">
        ///    If true, include parts that have associated parts
        /// </param>
        /// <param name="includeAllChildren">
        ///    If true, return all associated Parts recursively for all children
        ///    If false, only return immediate children
        /// </param>
        /// <returns>Parts that are associated to the element</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ICollection<ElementId> GetAssociatedParts(Document document, bool includePartsWithAssociatedParts, bool includeAllChildren)
        {
            return PartUtils.GetAssociatedParts(document, linkElementId, includePartsWithAssociatedParts, includeAllChildren);
        }

        /// <summary>Gets associated PartMaker for an element.</summary>
        /// <param name="document">The document of the element</param>
        /// <returns>
        ///    The PartMaker element that is making Parts for this element.
        ///    <see langword="null" /> if there is no associated PartMaker.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public PartMaker? GetAssociatedPartMaker(Document document)
        {
            return PartUtils.GetAssociatedPartMaker(document, linkElementId);
        }
    }
}