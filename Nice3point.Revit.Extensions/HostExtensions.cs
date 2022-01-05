using Autodesk.Revit.DB;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Host Extensions
/// </summary>
public static class HostExtensions
{
    /// <summary>Returns the bottom faces for this host object</summary>
    /// <remarks>
    ///     This utility supports host objects whose bottom faces represent one of the boundaries of CompoundStructure (such as roofs, floors or ceilings)
    /// </remarks>
    /// <param name="host">The host object</param>
    /// <returns>
    ///     An array of references to the faces which are at the bottom of this element.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     This host object does not support access to top or bottom faces.
    /// </exception>
    [Pure]
    public static IList<Reference> GetBottomFaces([NotNull] this HostObject host)
    {
        return HostObjectUtils.GetBottomFaces(host);
    }

    /// <summary>
    ///     Returns the major side faces for this host object
    /// </summary>
    /// <remarks>
    ///     This utility supports host objects whose CompoundStructure is nominally oriented vertically.
    ///     It outputs faces which are at the boundary of the CompoundStructure (such as Walls and FaceWalls).
    /// </remarks>
    /// <param name="host">The host object</param>
    /// <param name="side">The side of the host object</param>
    /// <returns>
    ///     An array of references to the faces which are on the given side of this element
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     This host object does not support access to side faces
    /// </exception>
    [Pure]
    public static IList<Reference> GetSideFaces([NotNull] this HostObject host, ShellLayerType side)
    {
        return HostObjectUtils.GetSideFaces(host, side);
    }

    /// <summary>
    ///     Returns the top faces for this host object
    /// </summary>
    /// <remarks>
    ///     This utility supports host objects whose top faces represent one of the boundaries of CompoundStructure (such as roofs, floors or ceilings)
    /// </remarks>
    /// <param name="host">The host object</param>
    /// <returns>An array of references to the faces which are at the top of this element</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     This host object does not support access to top or bottom faces
    /// </exception>
    [Pure]
    public static IList<Reference> GetTopFaces([NotNull] this HostObject host)
    {
        return HostObjectUtils.GetTopFaces(host);
    }
}