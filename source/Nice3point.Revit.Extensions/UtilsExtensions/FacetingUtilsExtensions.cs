// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.FacetingUtils" /> class.
/// </summary>
[PublicAPI]
public static class FacetingUtilsExtensions
{
    /// <param name="triangulation">The source triangulation.</param>
    extension(TriangulationInterface triangulation)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.FacetingUtils.ConvertTrianglesToQuads(Autodesk.Revit.DB.TriangulationInterface)" />
        [Pure]
        public IList<TriOrQuadFacet> ConvertTrianglesToQuads()
        {
            return FacetingUtils.ConvertTrianglesToQuads(triangulation);
        }
    }
}
