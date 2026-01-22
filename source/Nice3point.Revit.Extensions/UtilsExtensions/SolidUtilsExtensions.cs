// ReSharper disable once CheckNamespace

using JetBrains.Annotations;

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

        /// <summary>Tests if the input solid or shell is valid for tessellation</summary>
        /// <returns>True if the solid or shell is valid for tessellation, false otherwise</returns>
        public bool IsValidForTessellation => SolidUtils.IsValidForTessellation(solid);

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