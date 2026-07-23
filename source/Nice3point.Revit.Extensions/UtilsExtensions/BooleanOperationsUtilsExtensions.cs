

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.BooleanOperationsUtils"/> class.
/// </summary>
[PublicAPI]
public static class BooleanOperationsUtilsExtensions
{
    /// <param name="solid">The source solid.</param>
    extension(Solid solid)
    {
        /// <summary>
        ///    Creates a new Solid which is the intersection of the input Solid with the half-space on the positive side of the given Plane. The positive side of the plane is the side to which Plane.Normal points.
        /// </summary>
        /// <param name="plane">
        ///    The cut plane.  The space on the positive side of the normal of the plane will be intersected with the input Solid.
        /// </param>
        /// <returns>The newly created Solid.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public Solid CutWithHalfSpace(Plane plane)
        {
            return BooleanOperationsUtils.CutWithHalfSpace(solid, plane);
        }

        /// <summary>
        ///    Modifies the input Solid preserving only the volume on the positive side of the given Plane. The positive side of the plane is the side to which Plane.Normal points.
        /// </summary>
        /// <remarks>This operation modifies the original input Geometry objects.</remarks>
        /// <param name="plane">
        ///    The cut plane.  The space on the positive side of the normal of the plane will be intersected with the input Solid.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Thrown when the original solid object is the geometry of the Revit model.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was NULL
        /// </exception>
        public void CutWithHalfSpaceModifyingOriginalSolid(Plane plane)
        {
            BooleanOperationsUtils.CutWithHalfSpaceModifyingOriginalSolid(solid, plane);
        }

        /// <summary>
        ///    Perform a boolean geometric operation between two solids, and return a new solid to represent the result.
        /// </summary>
        /// <param name="other">
        ///    The second solid object. A copy will be taken of the input object, so any solid whether obtained from a Revit element or not would be accepted.
        /// </param>
        /// <param name="booleanType">boolean operation type.</param>
        /// <returns>The result geometry.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to perform the Boolean operation for the two solids. This may be due to geometric inaccuracies in the solids, such as slightly misaligned faces or edges.
        ///    If so, eliminating the inaccuracies by making sure the solids are accurately aligned may solve the problem. This also may be due to one or both solids having
        ///    complexities such as more than two faces geometrically meeting along a single edge, or two coincident edges, etc. Eliminating such conditions, or performing a
        ///    sequence of Boolean operations in an order that avoids such conditions, may solve the problem.
        /// </exception>
        /// <since>2012</since>
        [Pure]
        public Solid ExecuteBooleanOperation(Solid other, BooleanOperationsType booleanType)
        {
            return BooleanOperationsUtils.ExecuteBooleanOperation(solid, other, booleanType);
        }

        /// <summary>
        ///    Perform a boolean geometric operation between two solids, and modify the original solid to represent the result.
        /// </summary>
        /// <remarks>This operation modifies the original input Geometry objects.</remarks>
        /// <param name="other">
        ///    The second solid object. A copy will be taken of the input object, so any solid whether obtained from a Revit element or not would be accepted.
        /// </param>
        /// <param name="booleanType">boolean operation type.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Thrown when the original solid object is the geometry of the Revit model.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was NULL
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to perform the Boolean operation for the two solids. This may be due to geometric inaccuracies in the solids, such as slightly misaligned faces or edges.
        ///    If so, eliminating the inaccuracies by making sure the solids are accurately aligned may solve the problem. This also may be due to one or both solids having
        ///   complexities such as more than two faces geometrically meeting along a single edge, or two coincident edges, etc. Eliminating such conditions, or performing a
        ///    sequence of Boolean operations in an order that avoids such conditions, may solve the problem."
        /// </exception>
        public void ExecuteBooleanOperationModifyingOriginalSolid(Solid other, BooleanOperationsType booleanType)
        {
            BooleanOperationsUtils.ExecuteBooleanOperationModifyingOriginalSolid(solid, other, booleanType);
        }
    }
}