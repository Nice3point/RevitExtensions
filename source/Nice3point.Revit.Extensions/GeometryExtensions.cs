namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Geometry Extensions
/// </summary>
[PublicAPI]
public static class GeometryExtensions
{
    private const double Tolerance = 1e-9;

    /// <param name="source">The source line</param>
    extension(Line source)
    {
        /// <summary>
        ///     Returns the distance between Lines. The Lines are considered to be endless
        /// </summary>
        /// <returns>Distance between lines. Returns 0 if the lines intersect</returns>
        [Pure]
        public double Distance(Line other)
        {
            double distance;

            var v1 = source.Direction;
            var p1 = source.GetEndPoint(0);
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
        ///     Creates an instance of a curve with a new coordinate
        /// </summary>
        /// <param name="x">New coordinate</param>
        /// <returns>The new bound line</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Line SetCoordinateX(double x)
        {
            var endPoint0 = source.GetEndPoint(0);
            var endPoint1 = source.GetEndPoint(1);
            return Line.CreateBound(new XYZ(x, endPoint0.Y, endPoint0.Z), new XYZ(x, endPoint1.Y, endPoint1.Z));
        }

        /// <summary>
        ///     Creates an instance of a curve with a new coordinate
        /// </summary>
        /// <param name="y">New coordinate</param>
        /// <returns>The new bound line</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Line SetCoordinateY(double y)
        {
            var endPoint0 = source.GetEndPoint(0);
            var endPoint1 = source.GetEndPoint(1);
            return Line.CreateBound(new XYZ(endPoint0.X, y, endPoint0.Z), new XYZ(endPoint1.X, y, endPoint1.Z));
        }

        /// <summary>
        ///     Creates an instance of a curve with a new coordinate
        /// </summary>
        /// <param name="z">New coordinate</param>
        /// <returns>The new bound line</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Line SetCoordinateZ(double z)
        {
            var endPoint0 = source.GetEndPoint(0);
            var endPoint1 = source.GetEndPoint(1);
            return Line.CreateBound(new XYZ(endPoint0.X, endPoint0.Y, z), new XYZ(endPoint1.X, endPoint1.Y, z));
        }
    }

    /// <param name="source">The source box</param>
    extension(BoundingBoxXYZ source)
    {
        /// <summary>
        ///     Determines whether the specified point is contained within this BoundingBox
        /// </summary>
        /// <param name="point">The <see cref="Autodesk.Revit.DB.XYZ"/> point to check for containment within the source <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/></param>
        /// <returns><c>true</c> if the specified point is within the bounds of the <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/></returns>
        [Pure]
        public bool Contains(XYZ point)
        {
            return Contains(source, point, false);
        }

        /// <summary>
        ///     Determines whether the specified point is contained within this BoundingBox
        /// </summary>
        /// <param name="point">The <see cref="Autodesk.Revit.DB.XYZ"/> point to check for containment within the source <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/></param>
        /// <param name="strict"><c>true</c> if the point needs to be fully on the inside of the source. A point coinciding with the box border will be considered 'outside'.</param>
        /// <returns><c>true</c> if the specified point is within the bounds of the <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/></returns>
        [Pure]
        public bool Contains(XYZ point, bool strict)
        {
            if (!source.Transform.IsIdentity)
            {
                point = source.Transform.Inverse.OfPoint(point);
            }

            var insideX = strict
                ? point.X > source.Min.X + Tolerance && point.X < source.Max.X - Tolerance
                : point.X >= source.Min.X - Tolerance && point.X <= source.Max.X + Tolerance;

            var insideY = strict
                ? point.Y > source.Min.Y + Tolerance && point.Y < source.Max.Y - Tolerance
                : point.Y >= source.Min.Y - Tolerance && point.Y <= source.Max.Y + Tolerance;

            var insideZ = strict
                ? point.Z > source.Min.Z + Tolerance && point.Z < source.Max.Z - Tolerance
                : point.Z >= source.Min.Z - Tolerance && point.Z <= source.Max.Z + Tolerance;

            return insideX && insideY && insideZ;
        }

        /// <summary>
        /// Determines whether this <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> contains another <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/>
        /// </summary>
        /// <param name="other">The <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> instance to compare with the source</param>
        /// <returns><c>true</c> if the source <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> contains the other <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> instance</returns>
        [Pure]
        public bool Contains(BoundingBoxXYZ other)
        {
            return Contains(source, other, false);
        }

        /// <summary>
        /// Determines whether this <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> contains another <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/>
        /// </summary>
        /// <param name="other">The <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> instance to compare with the source</param>
        /// <param name="strict"><c>true</c> if the box needs to be fully on the inside of the source. Coincident boxes will be considered 'outside'.</param>
        /// <returns><c>true</c> if the source <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> contains the other <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> instance</returns>
        [Pure]
        public bool Contains(BoundingBoxXYZ other, bool strict)
        {
            var sourceMin = source.Transform.IsIdentity ? source.Min : source.Transform.OfPoint(source.Min);
            var sourceMax = source.Transform.IsIdentity ? source.Max : source.Transform.OfPoint(source.Max);
            var otherMin = other.Transform.IsIdentity ? other.Min : other.Transform.OfPoint(other.Min);
            var otherMax = other.Transform.IsIdentity ? other.Max : other.Transform.OfPoint(other.Max);

            var insideX = strict
                ? otherMin.X > sourceMin.X + Tolerance && otherMax.X < sourceMax.X - Tolerance
                : otherMin.X >= sourceMin.X - Tolerance && otherMax.X <= sourceMax.X + Tolerance;

            var insideY = strict
                ? otherMin.Y > sourceMin.Y + Tolerance && otherMax.Y < sourceMax.Y - Tolerance
                : otherMin.Y >= sourceMin.Y - Tolerance && otherMax.Y <= sourceMax.Y + Tolerance;

            var insideZ = strict
                ? otherMin.Z > sourceMin.Z + Tolerance && otherMax.Z < sourceMax.Z - Tolerance
                : otherMin.Z >= sourceMin.Z - Tolerance && otherMax.Z <= sourceMax.Z + Tolerance;

            return insideX && insideY && insideZ;
        }

        /// <summary>
        ///     Determines whether this BoundingBox overlaps with another BoundingBox
        /// </summary>
        /// <param name="other">The <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> instance to compare with the source</param>
        /// <returns><c>true</c> if the two <see cref="Autodesk.Revit.DB.BoundingBoxXYZ"/> instances have at least one common point</returns>
        [Pure]
        public bool Overlaps(BoundingBoxXYZ other)
        {
            var sourceMin = source.Transform.IsIdentity ? source.Min : source.Transform.OfPoint(source.Min);
            var sourceMax = source.Transform.IsIdentity ? source.Max : source.Transform.OfPoint(source.Max);
            var otherMin = other.Transform.IsIdentity ? other.Min : other.Transform.OfPoint(other.Min);
            var otherMax = other.Transform.IsIdentity ? other.Max : other.Transform.OfPoint(other.Max);

            var overlapX = !(sourceMax.X < otherMin.X - Tolerance || sourceMin.X > otherMax.X + Tolerance);
            var overlapY = !(sourceMax.Y < otherMin.Y - Tolerance || sourceMin.Y > otherMax.Y + Tolerance);
            var overlapZ = !(sourceMax.Z < otherMin.Z - Tolerance || sourceMin.Z > otherMax.Z + Tolerance);

            return overlapX && overlapY && overlapZ;
        }
    }

    /// <param name="source">The source arc</param>
    extension(Arc source)
    {
        /// <summary>
        ///     Creates an instance of a curve with a new coordinate
        /// </summary>
        /// <param name="x">New coordinate</param>
        /// <returns>The new bound line</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Arc SetCoordinateX(double x)
        {
            var endPoint0 = source.GetEndPoint(0);
            var endPoint1 = source.GetEndPoint(1);
            var endParameter0 = source.GetEndParameter(0);
            var endParameter1 = source.GetEndParameter(1);
            var centerPoint = source.Evaluate((endParameter0 + endParameter1) / 2, true);
            return Arc.Create(
                new XYZ(x, endPoint0.Y, endPoint0.Z),
                new XYZ(x, endPoint1.Y, endPoint1.Z),
                new XYZ(x, centerPoint.Y, centerPoint.Z));
        }

        /// <summary>
        ///     Creates an instance of a curve with a new coordinate
        /// </summary>
        /// <param name="y">New coordinate</param>
        /// <returns>The new bound line</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Arc SetCoordinateY(double y)
        {
            var endPoint0 = source.GetEndPoint(0);
            var endPoint1 = source.GetEndPoint(1);
            var endParameter0 = source.GetEndParameter(0);
            var endParameter1 = source.GetEndParameter(1);
            var centerPoint = source.Evaluate((endParameter0 + endParameter1) / 2, true);
            return Arc.Create(
                new XYZ(endPoint0.X, y, endPoint0.Z),
                new XYZ(endPoint1.X, y, endPoint1.Z),
                new XYZ(centerPoint.X, y, centerPoint.Z));
        }

        /// <summary>
        ///     Creates an instance of a curve with a new coordinate
        /// </summary>
        /// <param name="z">New coordinate</param>
        /// <returns>The new bound line</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
        /// </exception>
        [Pure]
        public Arc SetCoordinateZ(double z)
        {
            var endPoint0 = source.GetEndPoint(0);
            var endPoint1 = source.GetEndPoint(1);
            var endParameter0 = source.GetEndParameter(0);
            var endParameter1 = source.GetEndParameter(1);
            var centerPoint = source.Evaluate((endParameter0 + endParameter1) / 2, true);
            return Arc.Create(
                new XYZ(endPoint0.X, endPoint0.Y, z),
                new XYZ(endPoint1.X, endPoint1.Y, z),
                new XYZ(centerPoint.X, centerPoint.Y, z));
        }
    }

    /// <param name="source">The source point</param>
    extension(XYZ source)
    {
        /// <summary>
        ///     Creates an instance of a point with a new coordinate
        /// </summary>
        /// <param name="x">New coordinate</param>
        [Pure]
        public XYZ SetX(double x)
        {
            return new XYZ(x, source.Y, source.Z);
        }

        /// <summary>
        ///     Creates an instance of a point with a new coordinate
        /// </summary>
        /// <param name="y">New coordinate</param>
        [Pure]
        public XYZ SetY(double y)
        {
            return new XYZ(source.X, y, source.Z);
        }

        /// <summary>
        ///     Creates an instance of a point with a new coordinate
        /// </summary>
        /// <param name="z">New coordinate</param>
        [Pure]
        public XYZ SetZ(double z)
        {
            return new XYZ(source.X, source.Y, z);
        }
    }
}