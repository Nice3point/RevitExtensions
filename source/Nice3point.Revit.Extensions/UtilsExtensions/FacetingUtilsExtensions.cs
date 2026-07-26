

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.FacetingUtils"/> class.
/// </summary>
[PublicAPI]
public static class FacetingUtilsExtensions
{
    /// <param name="triangulation">The source triangulation.</param>
    extension(TriangulationInterface triangulation)
    {
        /// <summary>Replaces pairs of adjacent, coplanar triangles by quadrilaterals.</summary>
        /// <returns>
        ///    A collection of triangles and quadrilaterals representing the original triangulated object.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        [Pure]
        public IList<TriOrQuadFacet> ConvertTrianglesToQuads()
        {
            return FacetingUtils.ConvertTrianglesToQuads(triangulation);
        }
    }
}