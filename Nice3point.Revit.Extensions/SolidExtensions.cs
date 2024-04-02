namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Solid Extensions
/// </summary>
public static class SolidExtensions
{
    /// <summary>Creates a new Solid which is a copy of the input Solid</summary>
    /// <param name="solid">The input solid to be copied</param>
    /// <returns>The newly created Solid</returns>
    [Pure]
    public static Solid Clone([NotNull] this Solid solid)
    {
        return SolidUtils.Clone(solid);
    }

    /// <summary>Creates a new Solid which is the transformation of the input Solid</summary>
    /// <param name="solid">The input solid to be transformed</param>
    /// <param name="transform">The transform (which must be conformal)</param>
    /// <returns>The newly created Solid</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
    ///    Transform is not conformal.
    ///    Or transform has a scale that is negative or zero
    /// </exception>
    [Pure]
    public static Solid CreateTransformed([NotNull] this Solid solid, Transform transform)
    {
        return SolidUtils.CreateTransformed(solid, transform);
    }

    /// <summary>Splits a solid geometry into several separate solids</summary>
    /// <remarks>
    ///    If no splitting is done, a copy of the input solid will be returned in the output array
    /// </remarks>
    /// <param name="solid">The solid</param>
    /// <returns>The split solid geometries</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Failed to split the solid geometry
    /// </exception>
    [Pure]
    public static IList<Solid> SplitVolumes([NotNull] this Solid solid)
    {
        return SolidUtils.SplitVolumes(solid);
    }

    /// <summary>Tests if the input solid or shell is valid for tessellation</summary>
    /// <param name="solid">The solid or shell</param>
    /// <returns>True if the solid or shell is valid for tessellation, false otherwise</returns>
    [Pure]
    public static bool IsValidForTessellation([NotNull] this Solid solid)
    {
        return SolidUtils.IsValidForTessellation(solid);
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
    /// <param name="solid">The solid or shell to be faceted</param>
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
    public static TriangulatedSolidOrShell TessellateSolidOrShell([NotNull] this Solid solid, SolidOrShellTessellationControls tessellationControls)
    {
        return SolidUtils.TessellateSolidOrShell(solid, tessellationControls);
    }
#if REVIT2021_OR_GREATER

    /// <summary>Find all EdgeEndPoints at a vertex identified by the input EdgeEndPoint</summary>
    /// <param name="edgeEndPoint">The input EdgeEndPoint that identifies the vertex</param>
    /// <returns>All EdgeEndPoints at the vertex. The input EdgeEndPoint is also included</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Failed to find all EdgeEndPoints at a vertex identified by the input EdgeEndPoint
    /// </exception>
    [Pure]
    public static IList<EdgeEndPoint> FindAllEdgeEndPointsAtVertex([NotNull] this EdgeEndPoint edgeEndPoint)
    {
        return SolidUtils.FindAllEdgeEndPointsAtVertex(edgeEndPoint);
    }
#endif
}