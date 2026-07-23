

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.SolidUtils"/> class.
/// </summary>
[PublicAPI]
public static class SolidUtilsExtensions
{
    /// <param name="solid">The source solid.</param>
    extension(Solid solid)
    {
        /// <summary>Tests if the input solid or shell is valid for tessellation</summary>
        /// <returns>True if the solid or shell is valid for tessellation, false otherwise</returns>
        public bool IsValidForTessellation => SolidUtils.IsValidForTessellation(solid);

        /// <summary>Creates a new Solid which is a copy of the input Solid</summary>
        /// <returns>The newly created Solid</returns>
        [Pure]
        public Solid Clone()
        {
            return SolidUtils.Clone(solid);
        }

        /// <summary>Creates a new Solid which is the transformation of the input Solid</summary>
        /// <param name="transform">The transform (which must be conformal)</param>
        /// <returns>The newly created Solid</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    Transform is not conformal.
        ///    Or transform has a scale that is negative or zero
        /// </exception>
        [Pure]
        public Solid CreateTransformed(Transform transform)
        {
            return SolidUtils.CreateTransformed(solid, transform);
        }

        /// <summary>Splits a solid geometry into several separate solids</summary>
        /// <remarks>
        ///    If no splitting is done, a copy of the input solid will be returned in the output array
        /// </remarks>
        /// <returns>The split solid geometries</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to split the solid geometry
        /// </exception>
        [Pure]
        public IList<Solid> SplitVolumes()
        {
            return SolidUtils.SplitVolumes(solid);
        }

        /// <summary>
        ///    This function facets (i.e., triangulates) a solid or an open shell. Each boundary
        ///    component of the solid or shell is represented by a single triangulated structure
        /// </summary>
        /// <remarks>
        ///    Every point on the triangulation of a boundary component of the solid (or
        ///    shell) should lie within the 3D distance specified by the "accuracy" input of some
        ///    point on the triangulation, and vice-versa. In some cases, this constraint may be
        ///    implemented heuristically (not rigorously)
        /// </remarks>
        /// <param name="tessellationControls">
        ///    This input controls various aspects of the triangulation
        /// </param>
        /// <returns>
        ///    The triangulated structures corresponding to the boundary components of the
        ///    input solid or the components of the input shell
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    solidOrShell is not valid for triangulation (for example, it contains no faces)
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Unable to triangulate the solid or shell
        /// </exception>
        [Pure]
        public TriangulatedSolidOrShell TessellateSolidOrShell(SolidOrShellTessellationControls tessellationControls)
        {
            return SolidUtils.TessellateSolidOrShell(solid, tessellationControls);
        }
#if REVIT2026_OR_GREATER

        /// <summary>
        ///    Computes whether the input Solid is geometrically closed to within Revit's tolerances.
        /// </summary>
        /// <remarks>
        ///    A solid is geometrically closed if it is topologically closed and also meets certain
        ///    geometric criteria. In particular, every pair of faces adjoining an edge must intersect
        ///    along the edge, and edge loops must have no gaps between consecutive edges of the loop,
        ///    when evaluated on the edge loop's face.
        ///    If the geometry contains multiple connected components, the function returns true
        ///    if and only if every connected component is geometrically closed. If the input Solid
        ///    contains grossly invalid geometry, an InvalidOperationException will be thrown.
        /// </remarks>
        /// <returns>True if the geometry is geometrically closed, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to compute whether the geometry is geometrically closed.
        /// </exception>
        [Pure]
        public bool ComputeIsGeometricallyClosed()
        {
            return SolidUtils.ComputeIsGeometricallyClosed(solid);
        }

        /// <summary>Compute whether the input Solid is topologically closed.</summary>
        /// <remarks>
        ///    A solid is topologically closed if every face has at least one edge loop and
        ///    every edge is shared by exactly two faces. If the geometry contains multiple
        ///    connected components, the function returns true if and only if every connected
        ///    component is topologically closed.
        /// </remarks>
        /// <returns>True if the geometry is topologically closed, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool ComputeIsTopologicallyClosed()
        {
            return SolidUtils.ComputeIsTopologicallyClosed(solid);
        }
#endif
    }

#if REVIT2021_OR_GREATER
    /// <param name="endPoint">The source EdgeEndPoint.</param>
    extension(EdgeEndPoint endPoint)
    {
        /// <summary>Find all EdgeEndPoints at a vertex identified by the input EdgeEndPoint</summary>
        /// <returns>All EdgeEndPoints at the vertex. The input EdgeEndPoint is also included</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to find all EdgeEndPoints at a vertex identified by the input EdgeEndPoint
        /// </exception>
        [Pure]
        public IList<EdgeEndPoint> FindAllEdgeEndPointsAtVertex()
        {
            return SolidUtils.FindAllEdgeEndPointsAtVertex(endPoint);
        }
    }
#endif
}