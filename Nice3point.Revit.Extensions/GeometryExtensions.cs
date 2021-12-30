using Autodesk.Revit.DB;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Geometry Extensions
/// </summary>
public static class GeometryExtensions
{
    /// <summary>
    ///     Returns the distance between Lines. The Lines are considered to be endless
    /// </summary>
    /// <returns>Distance between lines. Returns 0 if the lines intersect</returns>
    public static double Distance(this Line line1, Line line2)
    {
        double distance;

        var v1 = line1.Direction;
        var p1 = line1.GetEndPoint(0);
        var v2 = line2.Direction;
        var p2 = line2.GetEndPoint(0);

        var a = p1.X - p2.X;
        var b = p1.Y - p2.Y;
        var c = p1.Z - p2.Z;
        var pq = v1.X * v2.Y - v2.X * v1.Y;
        var qr = v1.Y * v2.Z - v2.Y * v1.Z;
        var rp = v1.Z * v2.X - v2.Z * v1.X;
        var dev = Math.Sqrt(Math.Pow(qr, 2) + Math.Pow(rp, 2) + Math.Pow(pq, 2));

        if (dev < 1e-4)
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
}