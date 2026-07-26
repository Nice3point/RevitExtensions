using Autodesk.Revit.DB.PointClouds;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.PointClouds;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.PointClouds.PointCloudFilterUtils"/> class.
/// </summary>
[PublicAPI]
public static class PointCloudFilterUtilsExtensions
{
    /// <param name="filter">The source point cloud filter.</param>
    extension(PointCloudFilter filter)
    {
        /// <summary>Computes outline of a part of a box that satisfies given PointCloudFilter.</summary>
        /// <param name="box">A box aligned with coordinate axes.</param>
        /// <returns>
        ///    The bounding box of the set of all points within the original box that satisfy the filter.
        ///    Not every point within the resulting outline satisfies the filter, but any point that is contained
        ///    in the original box and satisfies the filter is guaranteed to be within the resulting outline.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public Outline GetFilteredOutline(Outline box)
        {
            return PointCloudFilterUtils.GetFilteredOutline(filter, box);
        }
    }
}