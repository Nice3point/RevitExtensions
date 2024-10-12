#if FEATURE_PARTS
// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.PartUtils"/> class.
/// </summary>
public static class PartUtilsExtensions
{
    /// <summary>Creates a new set of parts out of the original element.</summary>
    /// <remarks>Parts will be added to the model after regeneration.</remarks>
    /// <param name="element">The element that parts will be created from.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Element was not permitted for creating parts.
    ///    Element should be of a valid category and should not already be divided into parts.
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
    public static void CreateParts(this Element element)
    {
        PartUtils.CreateParts(element.Document, [element.Id]);
    }
    
    /// <summary>Creates a new set of parts out of the original elements.</summary>
    /// <remarks>Parts will be added to the model after regeneration.</remarks>
    /// <param name="document">The document containing the elements.</param>
    /// <param name="elementIds">The elements that parts will be created from.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    One or more element ids was not permitted for creating parts.
    ///    Elements should be of a valid category and the ids should be valid and should not already be divided into parts.
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
    public static void CreateParts(this ICollection<ElementId> elementIds, Document document)
    {
        PartUtils.CreateParts(document, elementIds);
    }

    /// <summary>Creates a new set of parts out of the original elements.</summary>
    /// <remarks>
    ///    Parts will be added to the model after regeneration.
    ///    To get the ids of the parts created by this method use PartUtils.GetAssociatedParts() with the contents of hostOrLinkElementIds.
    /// </remarks>
    /// <param name="document">The document containing the elements.</param>
    /// <param name="hostOrLinkElementIds">The elements that parts will be created from.</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    One or more element ids was not permitted for creating parts.
    ///    HostOrLinkElements should be of a valid category and the ids should be valid and should not already be divided into parts.
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
    public static void CreateParts(this ICollection<LinkElementId> hostOrLinkElementIds, Document document)
    {
        PartUtils.CreateParts(document, hostOrLinkElementIds);
    }

    /// <summary>Creates divided parts out of parts.</summary>
    /// <param name="document">The document containing the parts.</param>
    /// <param name="elementIdsToDivide">The elements that will be divided.</param>
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
    ///    The input curveArray contains at least one NULL pointer and is not supported for this operation.
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
    public static PartMaker? DivideParts(this ICollection<ElementId> elementIdsToDivide,
        ICollection<ElementId> intersectingReferenceIds,
        Document document,
        IList<Curve> curveArray,
        ElementId sketchPlaneId)
    {
        return PartUtils.DivideParts(document, elementIdsToDivide, intersectingReferenceIds, curveArray, sketchPlaneId);
    }

    /// <summary>Identifies if the given element can be used to create parts.</summary>
    /// <param name="document">The document.</param>
    /// <param name="hostOrLinkElementId">Id to be tested for validity for creating part.</param>
    /// <returns>True if this id is valid, false otherwise.</returns>
    [Pure]
    public static bool IsValidForCreateParts(this LinkElementId hostOrLinkElementId, Document document)
    {
        return PartUtils.IsValidForCreateParts(document, hostOrLinkElementId);
    }

    /// <summary>
    ///    Identifies the elements ( reference planes, levels, grids ) that were used to create the part.
    /// </summary>
    /// <param name="part">The part</param>
    /// <returns>
    ///    The elements that created the part. Empty if partId is not a Part or Part is not divided.
    /// </returns> 
    [Pure]
    public static IList<Element> GetSplittingElements(this Part part)
    {
        var elementIds = PartUtils.GetSplittingElements(part.Document, part.Id);
        if (elementIds.Count == 0) return [];

        return new FilteredElementCollector(part.Document, elementIds).ToElements();
    }

    /// <summary>
    ///    Identifies the curves that were used to create the part and the plane in which they reside.
    /// </summary>
    /// <param name="part">The part.</param>
    /// <param name="sketchPlane">The plane in which the division curves were sketched.</param>
    /// <returns>
    ///    The curves that created the part. Empty if partId is not a part or Part is not divided.
    /// </returns> 
    [Pure]
    public static IList<Curve> GetSplittingCurves(this Part part, out Plane sketchPlane)
    {
        return PartUtils.GetSplittingCurves(part.Document, part.Id, out sketchPlane);
    }

    /// <summary>Identifies the curves that were used to create the part.</summary>
    /// <param name="part">The part.</param>
    /// <returns>
    ///    The curves that created the part. Empty if partId is not a Part or Part is not divided.
    /// </returns> 
    [Pure]
    public static IList<Curve> GetSplittingCurves(this Part part)
    {
        return PartUtils.GetSplittingCurves(part.Document, part.Id);
    }

    /// <summary>Identifies if the given element can be used to create parts.</summary>
    /// <param name="element">Element to be tested for validity for creating parts.</param>
    /// <returns>True if element is valid, false otherwise.</returns>
    [Pure]
    public static bool IsValidForCreateParts(this Element element)
    {
        return PartUtils.AreElementsValidForCreateParts(element.Document, [element.Id]);
    }

    /// <summary>Identifies if the given elements can be used to create parts.</summary>
    /// <param name="document">The document.</param>
    /// <param name="elementIds">Element ids to be tested for validity for creating parts.</param>
    /// <returns>True if all member ids are valid, false otherwise.</returns>  
    [Pure]
    public static bool AreElementsValidForCreateParts(this ICollection<ElementId> elementIds, Document document)
    {
        return PartUtils.AreElementsValidForCreateParts(document, elementIds);
    }

    /// <summary>Identifies if provided part are valid for dividing parts.</summary>
    /// <param name="partToDivide">
    ///   Part to be tested for validity for dividing parts.
    /// </param>
    /// <returns>True if part is valid, false otherwise.</returns> 
    [Pure]
    public static bool IsValidForDivide(this Part partToDivide)
    {
        return PartUtils.ArePartsValidForDivide(partToDivide.Document, [partToDivide.Id]);
    }
    
    /// <summary>Identifies if provided members are valid for dividing parts.</summary>
    /// <param name="document">The document.</param>
    /// <param name="elementIdsToDivide">
    ///    Element ids to be tested for validity for dividing parts.
    /// </param>
    /// <returns>True if all member ids are valid, false otherwise.</returns> 
    [Pure]
    public static bool ArePartsValidForDivide(this ICollection<ElementId> elementIdsToDivide, Document document)
    {
        return PartUtils.ArePartsValidForDivide(document, elementIdsToDivide);
    }

    /// <summary>
    ///    Calculates the length of the longest chain of divisions/merges to reach to an original non-Part element that is the source of the tested part.
    /// </summary>
    /// <param name="part">The part to be tested</param>
    /// <returns>The length of the longest chain.</returns>  
    [Pure]
    public static int GetChainLengthToOriginal(this Part part)
    {
        return PartUtils.GetChainLengthToOriginal(part);
    }

    /// <summary>Checks if an element has associated parts.</summary>
    /// <param name="element">The element to be checked for associated Parts</param>
    /// <returns>True if the element has associated Parts.</returns> 
    [Pure]
    public static bool HasAssociatedParts(this Element element)
    {
        return PartUtils.HasAssociatedParts(element.Document, element.Id);
    }

    /// <summary>Checks if an element has associated parts.</summary>
    /// <param name="hostDocument">The document.</param>
    /// <param name="hostOrLinkElementId">The element to be checked for associated Parts.</param>
    /// <returns>True if the element has associated Parts.</returns> 
    [Pure]
    public static bool HasAssociatedParts(this LinkElementId hostOrLinkElementId, Document hostDocument)
    {
        return PartUtils.HasAssociatedParts(hostDocument, hostOrLinkElementId);
    }

    /// <summary>Returns all Parts that are associated with the given element.</summary>
    /// <param name="element">The element to be checked for associated Parts.</param>
    /// <param name="includePartsWithAssociatedParts">
    ///    If true, include parts that have associated parts.
    /// </param>
    /// <param name="includeAllChildren">
    ///    If true, return all associated Parts recursively for all children.
    ///    If false, only return immediate children.
    /// </param>
    /// <returns>Parts that are associated to the element.</returns>  
    [Pure]
    public static IList<Part> GetAssociatedParts(this Element element, bool includePartsWithAssociatedParts, bool includeAllChildren)
    {
        var partIds = PartUtils.GetAssociatedParts(element.Document, element.Id, includePartsWithAssociatedParts, includeAllChildren);
        if (partIds.Count == 0) return [];

        var collector = new FilteredElementCollector(element.Document, partIds);
        return Enumerable.Cast<Part>(collector).ToList();
    }

    /// <summary>Returns all Parts that are associated with the given element</summary>
    /// <param name="hostDocument">The document of the element</param>
    /// <param name="hostOrLinkElementId">The element to be checked for associated Parts.</param>
    /// <param name="includePartsWithAssociatedParts">
    ///    If true, include parts that have associated parts
    /// </param>
    /// <param name="includeAllChildren">
    ///    If true, return all associated Parts recursively for all children
    ///    If false, only return immediate children
    /// </param>
    /// <returns>Parts that are associated to the element</returns>  
    [Pure]
    public static IList<Part> GetAssociatedParts(this LinkElementId hostOrLinkElementId,
        Document hostDocument,
        bool includePartsWithAssociatedParts,
        bool includeAllChildren)
    {
        var partIds = PartUtils.GetAssociatedParts(hostDocument, hostOrLinkElementId, includePartsWithAssociatedParts, includeAllChildren);
        if (partIds.Count == 0) return [];

        var collector = new FilteredElementCollector(hostDocument, partIds);
        return Enumerable.Cast<Part>(collector).ToList();
    }

    /// <summary>Gets associated PartMaker for an element.</summary>
    /// <param name="element">The element to be checked for associated Parts</param>
    /// <returns>
    ///    The PartMaker element that is making Parts for this element.
    ///    <see langword="null" /> if there is no associated PartMaker.
    /// </returns>  
    [Pure]
    public static PartMaker? GetAssociatedPartMaker(this Element element)
    {
        return PartUtils.GetAssociatedPartMaker(element.Document, element.Id);
    }

    /// <summary>Gets associated PartMaker for an element.</summary>
    /// <param name="hostDocument">The document</param>
    /// <param name="hostOrLinkElementId">
    ///    The id for the element to be checked for associated Parts
    /// </param>
    /// <returns>
    ///    The PartMaker element that is making Parts for this element.
    ///    <see langword="null" /> if there is no associated PartMaker.
    /// </returns>  
    [Pure]
    public static PartMaker? GetAssociatedPartMaker(this LinkElementId hostOrLinkElementId, Document hostDocument)
    {
        return PartUtils.GetAssociatedPartMaker(hostDocument, hostOrLinkElementId);
    }

    /// <summary>Identifies whether Part elements may be merged.</summary>
    /// <param name="document">The document.</param>
    /// <param name="partIds">Element ids of Parts.</param>
    /// <returns>
    ///    True if all element ids correspond to Part elements,
    ///    none of the parts already has associated parts,
    ///    the parts have contiguous geometry, all report the same materials,
    ///    and all have the same creation and demolition phases.
    /// </returns>
    [Pure]
    public static bool ArePartsValidForMerge(this ICollection<ElementId> partIds, Document document)
    {
        return PartUtils.ArePartsValidForMerge(document, partIds);
    }

    /// <summary>
    ///    Create a single merged part which represents the Parts specified by partsToMerge.
    /// </summary>
    /// <param name="document">The document.</param>
    /// <param name="partIds">The elements that the merged part will be created from.</param>
    /// <returns>
    ///    The newly created PartMaker. <see langword="null" /> if no parts are merged.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    One or more element ids was not suitable for merging with the others.
    ///    Specified elements should all be Parts, report the same material,
    ///    creation and demolition phases, and have contiguous geometry.
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
    public static PartMaker? CreateMergedPart(this ICollection<ElementId> partIds, Document document)
    {
        return PartUtils.CreateMergedPart(document, partIds);
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
    /// <param name="partIds">A set of element ids.</param>
    /// <returns>
    ///    An array of clusters such that all the elements in a single cluster
    ///    are valid for merge. Each cluster will be maximal in that appending
    ///    any of the other Parts specified as input will result in a collection
    ///    that is not valid for merge.
    /// </returns>  
    [Pure]
    public static IList<ICollection<ElementId>> FindMergeableClusters(this ICollection<ElementId> partIds, Document document)
    {
        return PartUtils.FindMergeableClusters(document, partIds);
    }

    /// <summary>Is the Part the result of a merge.</summary>
    /// <returns>True if the Part is the result of a merge operation.</returns> 
    [Pure]
    public static bool IsMergedPart(this Part part)
    {
        return PartUtils.IsMergedPart(part);
    }

    /// <summary>Retrieves the element ids of the source elements of a merged part.</summary>
    /// <param name="part">A merged part.</param>
    /// <returns>
    ///    Parts that were merged to create the specified merged part.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The specified Part is not a merged part.
    /// </exception>  
    [Pure]
    public static IList<Part> GetMergedParts(this Part part)
    {
        var partIds = PartUtils.GetMergedParts(part);
        if (partIds.Count == 0) return [];

        var collector = new FilteredElementCollector(part.Document, partIds);
        return Enumerable.Cast<Part>(collector).ToList();
    }

    /// <summary>Is the Part derived from link geometry.</summary> 
    public static bool IsPartDerivedFromLink(this Part part)
    {
        return PartUtils.IsPartDerivedFromLink(part);
    }

    /// <summary>
    ///    Obtains the object allowing access to the divided volume
    ///    properties of the PartMaker.
    /// </summary>
    /// <param name="partMaker">The PartMaker.</param>
    /// <returns>
    ///    The object handle. Returns <see langword="null" /> if the
    ///    PartMaker does not represent divided volumes.
    /// </returns>
    [Pure]
    public static PartMakerMethodToDivideVolumes? GetPartMakerMethodToDivideVolumeFw(this PartMaker partMaker)
    {
        return PartUtils.GetPartMakerMethodToDivideVolumeFW(partMaker);
    }
}
#endif