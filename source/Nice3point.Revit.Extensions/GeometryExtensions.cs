namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Geometry Extensions
/// </summary>
[PublicAPI]
public static class GeometryExtensions
{
    private const double Tolerance = 1e-9;

    /// <param name="line">The line</param>
    extension(Line line)
    {
        /// <summary>
        ///     Returns the distance between Lines. The Lines are considered to be endless
        /// </summary>
        /// <returns>Distance between lines. Returns 0 if the lines intersect</returns>
        [Pure]
        public double Distance(Line other)
        {
            double distance;

            var v1 = line.Direction;
            var p1 = line.GetEndPoint(0);
            var v2 = other.Direction;
            var p2 = other.GetEndPoint(0);

            var a = p1.X - p2.X;
            var b = p1.Y - p2.Y;
            var c = p1.Z - p2.Z;
            var pq = v1.X * v2.Y - v2.X * v1.Y;
            var qr = v1.Y * v2.Z - v2.Y * v1.Z;
            var rp = v1.Z * v2.X - v2.Z * v1.X;
            var dev = Math.Sqrt(Math.Pow(qr, 2) + Math.Pow(rp, 2) + Math.Pow(pq, 2));

            if (dev < Tolerance)
            {
                var bp = b * v1.X;
                var br = b * v1.Z;
                var cp = c * v1.X;
                var cq = c * v1.Y;
                var aq = a * v1.Y;
                var ar = a * v1.Z;
                distance = Math.Sqrt(Math.Pow(br - cq, 2) + Math.Pow(cp - ar, 2) + Math.Pow(aq - bp, 2)) /
                           Math.Sqrt(Math.Pow(v1.X, 2) + Math.Pow(v1.Y, 2) + Math.Pow(v1.Z, 2));
            }
            else
            {
                distance = Math.Abs((qr * a + rp * b + pq * c) / dev);
            }

            return distance;
        }

        /// <summary>
        ///     Creates an instance of a line with a new X coordinate
        /// </summary>
        /// <param name="x">New X coordinate</param>
        /// <returns>The new bound line</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Line SetCoordinateX(double x)
        {
            var endPoint0 = line.GetEndPoint(0);
            var endPoint1 = line.GetEndPoint(1);
            return Line.CreateBound(new XYZ(x, endPoint0.Y, endPoint0.Z), new XYZ(x, endPoint1.Y, endPoint1.Z));
        }

        /// <summary>
        ///     Creates an instance of a line with a new Y coordinate
        /// </summary>
        /// <param name="y">New Y coordinate</param>
        /// <returns>The new bound line</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Line SetCoordinateY(double y)
        {
            var endPoint0 = line.GetEndPoint(0);
            var endPoint1 = line.GetEndPoint(1);
            return Line.CreateBound(new XYZ(endPoint0.X, y, endPoint0.Z), new XYZ(endPoint1.X, y, endPoint1.Z));
        }

        /// <summary>
        ///     Creates an instance of a line with a new Z coordinate
        /// </summary>
        /// <param name="z">New Z coordinate</param>
        /// <returns>The new bound line</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Line SetCoordinateZ(double z)
        {
            var endPoint0 = line.GetEndPoint(0);
            var endPoint1 = line.GetEndPoint(1);
            return Line.CreateBound(new XYZ(endPoint0.X, endPoint0.Y, z), new XYZ(endPoint1.X, endPoint1.Y, z));
        }
    }

    /// <param name="box">The bounding box</param>
    extension(BoundingBoxXYZ box)
    {
        /// <summary>
        ///     Determines whether the specified point is contained within this bounding box
        /// </summary>
        /// <param name="point">The <see cref="Autodesk.Revit.DB.XYZ"/> point to check for containment within this bounding box</param>
        /// <returns><c>true</c> if the specified point is within the bounds of this bounding box</returns>
        [Pure]
        public bool Contains(XYZ point)
        {
            return Contains(box, point, false);
        }

        /// <summary>
        ///     Determines whether the specified point is contained within this bounding box
        /// </summary>
        /// <param name="point">The <see cref="Autodesk.Revit.DB.XYZ"/> point to check for containment within this bounding box</param>
        /// <param name="strict"><c>true</c> if the point needs to be fully on the inside of this bounding box. A point coinciding with the box border will be considered 'outside'.</param>
        /// <returns><c>true</c> if the specified point is within the bounds of this bounding box</returns>
        [Pure]
        public bool Contains(XYZ point, bool strict)
        {
            if (!box.Transform.IsIdentity)
            {
                point = box.Transform.Inverse.OfPoint(point);
            }

            var insideX = strict
                ? point.X > box.Min.X + Tolerance && point.X < box.Max.X - Tolerance
                : point.X >= box.Min.X - Tolerance && point.X <= box.Max.X + Tolerance;

            var insideY = strict
                ? point.Y > box.Min.Y + Tolerance && point.Y < box.Max.Y - Tolerance
                : point.Y >= box.Min.Y - Tolerance && point.Y <= box.Max.Y + Tolerance;

            var insideZ = strict
                ? point.Z > box.Min.Z + Tolerance && point.Z < box.Max.Z - Tolerance
                : point.Z >= box.Min.Z - Tolerance && point.Z <= box.Max.Z + Tolerance;

            return insideX && insideY && insideZ;
        }

        /// <summary>
        /// Determines whether this bounding box contains another bounding box
        /// </summary>
        /// <param name="other">The <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> instance to compare with this bounding box</param>
        /// <returns><c>true</c> if this bounding box contains the other bounding box</returns>
        [Pure]
        public bool Contains(BoundingBoxXYZ other)
        {
            return Contains(box, other, false);
        }

        /// <summary>
        /// Determines whether this bounding box contains another bounding box
        /// </summary>
        /// <param name="other">The <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> instance to compare with this bounding box</param>
        /// <param name="strict"><c>true</c> if the other box needs to be fully on the inside of this bounding box. Coincident boxes will be considered 'outside'.</param>
        /// <returns><c>true</c> if this bounding box contains the other bounding box</returns>
        [Pure]
        public bool Contains(BoundingBoxXYZ other, bool strict)
        {
            var boxMin = box.Transform.IsIdentity ? box.Min : box.Transform.OfPoint(box.Min);
            var boxMax = box.Transform.IsIdentity ? box.Max : box.Transform.OfPoint(box.Max);
            var otherMin = other.Transform.IsIdentity ? other.Min : other.Transform.OfPoint(other.Min);
            var otherMax = other.Transform.IsIdentity ? other.Max : other.Transform.OfPoint(other.Max);

            var insideX = strict
                ? otherMin.X > boxMin.X + Tolerance && otherMax.X < boxMax.X - Tolerance
                : otherMin.X >= boxMin.X - Tolerance && otherMax.X <= boxMax.X + Tolerance;

            var insideY = strict
                ? otherMin.Y > boxMin.Y + Tolerance && otherMax.Y < boxMax.Y - Tolerance
                : otherMin.Y >= boxMin.Y - Tolerance && otherMax.Y <= boxMax.Y + Tolerance;

            var insideZ = strict
                ? otherMin.Z > boxMin.Z + Tolerance && otherMax.Z < boxMax.Z - Tolerance
                : otherMin.Z >= boxMin.Z - Tolerance && otherMax.Z <= boxMax.Z + Tolerance;

            return insideX && insideY && insideZ;
        }

        /// <summary>
        ///     Determines whether this bounding box overlaps with another bounding box
        /// </summary>
        /// <param name="other">The <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> instance to compare with this bounding box</param>
        /// <returns><c>true</c> if the two bounding boxes have at least one common point</returns>
        [Pure]
        public bool Overlaps(BoundingBoxXYZ other)
        {
            var boxMin = box.Transform.IsIdentity ? box.Min : box.Transform.OfPoint(box.Min);
            var boxMax = box.Transform.IsIdentity ? box.Max : box.Transform.OfPoint(box.Max);
            var otherMin = other.Transform.IsIdentity ? other.Min : other.Transform.OfPoint(other.Min);
            var otherMax = other.Transform.IsIdentity ? other.Max : other.Transform.OfPoint(other.Max);

            var overlapX = !(boxMax.X < otherMin.X - Tolerance || boxMin.X > otherMax.X + Tolerance);
            var overlapY = !(boxMax.Y < otherMin.Y - Tolerance || boxMin.Y > otherMax.Y + Tolerance);
            var overlapZ = !(boxMax.Z < otherMin.Z - Tolerance || boxMin.Z > otherMax.Z + Tolerance);

            return overlapX && overlapY && overlapZ;
        }
    }

    /// <param name="arc">The arc</param>
    extension(Arc arc)
    {
        /// <summary>
        ///     Creates an instance of an arc with a new X coordinate
        /// </summary>
        /// <param name="x">New X coordinate</param>
        /// <returns>The new arc</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Arc SetCoordinateX(double x)
        {
            var endPoint0 = arc.GetEndPoint(0);
            var endPoint1 = arc.GetEndPoint(1);
            var centerPoint = arc.Evaluate(0.5, true);
            return Arc.Create(
                new XYZ(x, endPoint0.Y, endPoint0.Z),
                new XYZ(x, endPoint1.Y, endPoint1.Z),
                new XYZ(x, centerPoint.Y, centerPoint.Z));
        }

        /// <summary>
        ///     Creates an instance of an arc with a new Y coordinate
        /// </summary>
        /// <param name="y">New Y coordinate</param>
        /// <returns>The new arc</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Arc SetCoordinateY(double y)
        {
            var endPoint0 = arc.GetEndPoint(0);
            var endPoint1 = arc.GetEndPoint(1);
            var centerPoint = arc.Evaluate(0.5, true);
            return Arc.Create(
                new XYZ(endPoint0.X, y, endPoint0.Z),
                new XYZ(endPoint1.X, y, endPoint1.Z),
                new XYZ(centerPoint.X, y, centerPoint.Z));
        }

        /// <summary>
        ///     Creates an instance of an arc with a new Z coordinate
        /// </summary>
        /// <param name="z">New Z coordinate</param>
        /// <returns>The new arc</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Arc SetCoordinateZ(double z)
        {
            var endPoint0 = arc.GetEndPoint(0);
            var endPoint1 = arc.GetEndPoint(1);
            var centerPoint = arc.Evaluate(0.5, true);
            return Arc.Create(
                new XYZ(endPoint0.X, endPoint0.Y, z),
                new XYZ(endPoint1.X, endPoint1.Y, z),
                new XYZ(centerPoint.X, centerPoint.Y, z));
        }
    }

    /// <param name="point">The point</param>
    extension(XYZ point)
    {
        /// <summary>
        ///     Creates an instance of a point with a new X coordinate
        /// </summary>
        /// <param name="x">New X coordinate</param>
        [Pure]
        public XYZ SetX(double x)
        {
            return new XYZ(x, point.Y, point.Z);
        }

        /// <summary>
        ///     Creates an instance of a point with a new Y coordinate
        /// </summary>
        /// <param name="y">New Y coordinate</param>
        [Pure]
        public XYZ SetY(double y)
        {
            return new XYZ(point.X, y, point.Z);
        }

        /// <summary>
        ///     Creates an instance of a point with a new Z coordinate
        /// </summary>
        /// <param name="z">New Z coordinate</param>
        [Pure]
        public XYZ SetZ(double z)
        {
            return new XYZ(point.X, point.Y, z);
        }
    }
}