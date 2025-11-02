namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit BoundingBoxXyz Extensions
/// </summary>
[PublicAPI]
public static class BoundingBoxXyzExtensions
{
    /// <param name="boundingBox">The bounding box from which the centroid is calculated.</param>
    extension(BoundingBoxXYZ boundingBox)
    {
        /// <summary>
        ///     Computes the geometric center point of the bounding box.
        /// </summary>
        /// <returns>A point representing the centroid of the bounding box.</returns>
        /// <remarks>BoundingBoxXYZ <see cref="Autodesk.Revit.DB.Transform"/> is not applied</remarks>
        [Pure]
        public XYZ ComputeCentroid()
        {
            return (boundingBox.Min + boundingBox.Max) / 2;
        }

        /// <summary>
        ///     Retrieves the coordinates of the eight vertices that define the bounding box.
        /// </summary>
        /// <returns>An array of points representing the vertices of the bounding box.</returns>
        /// <remarks>BoundingBoxXYZ <see cref="Autodesk.Revit.DB.Transform"/> is not applied</remarks>
        [Pure]
        public XYZ[] ComputeVertices()
        {
            return
            [
                new XYZ(boundingBox.Min.X, boundingBox.Min.Y, boundingBox.Min.Z),
                new XYZ(boundingBox.Min.X, boundingBox.Min.Y, boundingBox.Max.Z),
                new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z),
                new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Max.Z),
                new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z),
                new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Max.Z),
                new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z),
                new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Max.Z)
            ];
        }

        /// <summary>
        ///     Calculates the volume enclosed by the bounding box.
        /// </summary>
        /// <returns>The volume of the bounding box.</returns>
        [Pure]
        public double ComputeVolume()
        {
            var length = boundingBox.Max.X - boundingBox.Min.X;
            var width = boundingBox.Max.Y - boundingBox.Min.Y;
            var height = boundingBox.Max.Z - boundingBox.Min.Z;

            return length * width * height;
        }

        /// <summary>
        ///     Calculates the total surface area of the bounding box.
        /// </summary>
        /// <returns>The total surface area of the bounding box.</returns>
        [Pure]
        public double ComputeSurfaceArea()
        {
            var length = boundingBox.Max.X - boundingBox.Min.X;
            var width = boundingBox.Max.Y - boundingBox.Min.Y;
            var height = boundingBox.Max.Z - boundingBox.Min.Z;

            var area1 = length * width;
            var area2 = length * height;
            var area3 = width * height;

            return 2 * (area1 + area2 + area3);
        }
    }
}