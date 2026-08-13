// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.HostObjectUtils" /> class.
/// </summary>
[PublicAPI]
public static class HostObjectUtilsExtensions
{
    /// <param name="host">The source host object.</param>
    extension(HostObject host)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.HostObjectUtils.GetBottomFaces(Autodesk.Revit.DB.HostObject)" />
        [Pure]
        public IList<Reference> GetBottomFaces()
        {
            return HostObjectUtils.GetBottomFaces(host);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.HostObjectUtils.GetSideFaces(Autodesk.Revit.DB.HostObject,Autodesk.Revit.DB.ShellLayerType)" />
        [Pure]
        public IList<Reference> GetSideFaces(ShellLayerType side)
        {
            return HostObjectUtils.GetSideFaces(host, side);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.HostObjectUtils.GetTopFaces(Autodesk.Revit.DB.HostObject)" />
        [Pure]
        public IList<Reference> GetTopFaces()
        {
            return HostObjectUtils.GetTopFaces(host);
        }
    }
}
