

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.AdaptiveComponentInstanceUtils"/> class.
/// </summary>
[PublicAPI]
public static class AdaptiveComponentInstanceUtilsExtensions
{
    /// <param name="familyInstance">The source family instance.</param>
    extension(FamilyInstance familyInstance)
    {
        /// <summary>Verifies if a FamilyInstance has an Adaptive Family Symbol.</summary>
        /// <returns>True if the FamilyInstance has an Adaptive Family Symbol.</returns>
        public bool HasAdaptiveFamilySymbol => AdaptiveComponentInstanceUtils.HasAdaptiveFamilySymbol(familyInstance);

        /// <summary>Verifies if a FamilyInstance is an Adaptive Component Instance.</summary>
        /// <returns>True if the FamilyInstance has an Adaptive Component Instances.</returns>
        public bool IsAdaptiveComponentInstance => AdaptiveComponentInstanceUtils.IsAdaptiveComponentInstance(familyInstance);

        /// <summary>Gets the value of the flip parameter on the adaptive instance.</summary>
        /// <returns>True if the instance is flipped.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The FamilyInstance famInst is not an Adaptive Family Instance.
        ///    -or-
        ///    The FamilyInstance famInst does not have an Adaptive Family Symbol.
        /// </exception>
        public bool IsAdaptiveInstanceFlipped => AdaptiveComponentInstanceUtils.IsInstanceFlipped(familyInstance);

        /// <summary>
        ///    Gets Placement Adaptive Point Element Ref ids to which the instance geometry adapts.
        /// </summary>
        /// <remarks>
        ///    The output contains placement point ref ids. The element ids are sorted in
        ///    by the placement numbers (increasing order).
        /// </remarks>
        /// <returns>
        ///    The Placement Adaptive Point Element Ref ids to which the instance geometry adapts.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The FamilyInstance famInst is not an Adaptive Family Instance.
        ///    -or-
        ///    The FamilyInstance famInst does not have an Adaptive Family Symbol.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        [Pure]
        public IList<ElementId> GetAdaptivePlacementPointElementRefIds()
        {
            return AdaptiveComponentInstanceUtils.GetInstancePlacementPointElementRefIds(familyInstance);
        }

        /// <summary>Gets Adaptive Point Element Ref ids to which the instance geometry adapts.</summary>
        /// <remarks>
        ///    The output contains both placement point ref ids and the shape handles
        ///    point ref ids. The order corresponds to the same order as that
        ///    of the Adaptive Points in the Family (which may not be ordered as per
        ///    their placement number).
        /// 
        ///    Will return an empty array if there are no placement points and shape
        ///    handles. To manipulate such an instance the following methods can be
        ///    useful:
        ///    1) RehostAdaptiveComponentInstanceWithNoPlacementPoints()
        ///    2) MoveAdaptiveComponentInstance()
        /// </remarks>
        /// <returns>The Adaptive Point Element Ref ids to which the instance geometry adapts.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The FamilyInstance famInst is not an Adaptive Family Instance.
        ///    -or-
        ///    The FamilyInstance famInst does not have an Adaptive Family Symbol.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        [Pure]
        public IList<ElementId> GetAdaptivePointElementRefIds()
        {
            return AdaptiveComponentInstanceUtils.GetInstancePointElementRefIds(familyInstance);
        }


        /// <summary>
        ///    Gets Shape Handle Adaptive Point Element Ref ids to which the instance geometry adapts.
        /// </summary>
        /// <remarks>
        ///    The output contains shape handle point ref ids.
        /// 
        ///    If there are no shape handle points defined in the Family then the output
        ///    is empty.
        /// </remarks>
        /// <returns>
        ///    The Shape Handle Adaptive Point Element Ref ids to which the instance geometry adapts.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The FamilyInstance famInst is not an Adaptive Family Instance.
        ///    -or-
        ///    The FamilyInstance famInst does not have an Adaptive Family Symbol.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        [Pure]
        public IList<ElementId> GetAdaptiveShapeHandlePointElementRefIds()
        {
            return AdaptiveComponentInstanceUtils.GetInstanceShapeHandlePointElementRefIds(familyInstance);
        }

        /// <summary>Sets the value of the flip parameter on the adaptive instance.</summary>
        /// <param name="flip">The flip flag</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The FamilyInstance famInst is not an Adaptive Family Instance.
        ///    -or-
        ///    The FamilyInstance famInst does not have an Adaptive Family Symbol.
        ///    -or-
        ///    The FamilyInstance famInst cannot be flipped or unflipped.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        public void SetAdaptiveInstanceFlipped(bool flip)
        {
            AdaptiveComponentInstanceUtils.SetInstanceFlipped(familyInstance, flip);
        }

        /// <summary>Moves Adaptive Component Instance by the specified transformation.</summary>
        /// <remarks>
        ///    This method will attempt a rigid body motion of all the individual references and hence
        ///    the instance itself. However if unHost parameter is 'false' and any of the instance's
        ///    references are hosted to any geometry, then those references will move within the constraints.
        ///    The instance then adapts to where its references are moved to.
        /// </remarks>
        /// <param name="transform">The Transformation</param>
        /// <param name="unHost">
        ///    True if the move should disassociate the Point Element Refs from their hosts.
        ///    False if the Point Element Refs remain hosted.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    transform is not a rigid body transformation.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Unable to move the adaptive component instance.
        /// </exception>
        public void MoveAdaptiveComponentInstance(Transform transform, bool unHost)
        {
            AdaptiveComponentInstanceUtils.MoveAdaptiveComponentInstance(familyInstance, transform, unHost);
        }
    }

    /// <param name="familySymbol">The source family symbol.</param>
    extension(FamilySymbol familySymbol)
    {
        /// <summary>Verifies if a FamilySymbol is a valid Adaptive Family Symbol.</summary>
        /// <returns>True if the FamilySymbol is a valid Adaptive Family Symbol.</returns>
        public bool IsAdaptiveFamilySymbol => AdaptiveComponentInstanceUtils.IsAdaptiveFamilySymbol(familySymbol);

        /// <summary>Creates a FamilyInstance of Adaptive Component Family.</summary>
        /// <remarks>
        ///    This method creates an Adaptive FamilyInstance and its PointElement references.
        ///    The references can be accessed by methods like GetInstancePointElementRefIds().
        ///    The PointElement references can be moved, rehosted or manipulated just like any
        ///    other PointElements. The FamilyInstance would then 'adapt' to these references.
        /// </remarks>
        /// <returns>The Family Instance</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Symbol is not an Adaptive Family Symbol.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Unable to create adaptive component instance.
        /// </exception>
        public FamilyInstance CreateAdaptiveComponentInstance()
        {
            return AdaptiveComponentInstanceUtils.CreateAdaptiveComponentInstance(familySymbol.Document, familySymbol);
        }
    }
}