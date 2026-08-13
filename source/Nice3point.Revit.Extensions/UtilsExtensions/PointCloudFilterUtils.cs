using Autodesk.Revit.DB.PointClouds;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.PointClouds;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.PointClouds.PointCloudFilterUtils" /> class.
/// </summary>
[PublicAPI]
public static class PointCloudFilterUtilsExtensions
{
    /// <param name="filter">The source point cloud filter.</param>
    extension(PointCloudFilter filter)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.PointClouds.PointCloudFilterUtils.GetFilteredOutline(Autodesk.Revit.DB.PointClouds.PointCloudFilter,Autodesk.Revit.DB.Outline)" />
        [Pure]
        public Outline GetFilteredOutline(Outline box)
        {
            return PointCloudFilterUtils.GetFilteredOutline(filter, box);
        }
    }
}
