using System.Runtime.InteropServices;
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
    [Pure]
    public static double Distance([NotNull] this Line line1, [NotNull] Line line2)
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

        if (dev < 1e-9)
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
    ///     Creates clean joins between two elements that share a common face
    /// </summary>
    /// <remarks>
    ///    The visible edge between joined elements is removed. The joined elements then share the same line weight and fill pattern.
    ///    This functionality is not available for family documents.
    /// </remarks>
    /// <param name="document">The document containing the two elements</param>
    /// <param name="firstElement">The first element to be joined</param>
    /// <param name="secondElement">
    ///    The second element to be joined. This element must not be joined to the first element
    /// </param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Document is not a project document<br />
    ///    The element firstElement was not found in the given document<br />
    ///    The element secondElement was not found in the given document<br />
    ///    The elements are already joined<br />
    ///    The elements cannot be joined<br />
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Please remove or add segments on curtain grids instead of joining or unjoining geometry of the panels
    /// </exception>
    public static void JoinGeometry([NotNull] this Element firstElement, [NotNull] Element secondElement, Document document)
    {
        JoinGeometryUtils.JoinGeometry(document, firstElement, secondElement);
    }

    /// <summary>
    ///     Removes a join between two elements
    /// </summary>
    /// <remarks>This functionality is not available for family documents</remarks>
    /// <param name="document">The document containing the two elements</param>
    /// <param name="firstElement">The first element to be unjoined</param>
    /// <param name="secondElement">
    ///    The second element to be unjoined. This element must be joined to the fist element
    /// </param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Document is not a project document<br />
    ///    The element firstElement was not found in the given document<br />
    ///    The element secondElement was not found in the given document<br />
    ///    The elements are not joined<br />
    ///    The elements cannot be unjoined
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Please remove or add segments on curtain grids instead of joining or unjoining geometry of the panels
    /// </exception>
    public static void UnjoinGeometry([NotNull] this Element firstElement, [NotNull] Element secondElement, Document document)
    {
        JoinGeometryUtils.UnjoinGeometry(document, firstElement, secondElement);
    }

    /// <summary>
    ///     Determines whether two elements are joined
    /// </summary>
    /// <remarks>This functionality is not available for family documents</remarks>
    /// <param name="document">The document containing the two elements</param>
    /// <param name="firstElement">The first element</param>
    /// <param name="secondElement">The second element</param>
    /// <returns>True if the two elements are joined</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Document is not a project document<br />
    ///    The element firstElement was not found in the given document<br />
    ///    The element secondElement was not found in the given document
    /// </exception>
    [Pure]
    [return: MarshalAs(UnmanagedType.U1)]
    public static bool AreElementsJoined([NotNull] this Element firstElement, [NotNull] Element secondElement, Document document)
    {
        return JoinGeometryUtils.AreElementsJoined(document, firstElement, secondElement);
    }

    /// <summary>
    ///     Returns all elements joined to given element
    /// </summary>
    /// <remarks>This functionality is not available for family documents</remarks>
    /// <param name="document">The document containing the element</param>
    /// <param name="element">The element</param>
    /// <returns>The set of elements that are joined to the given element</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Document is not a project document<br />
    ///    The element element was not found in the given document
    /// </exception>
    [Pure]
    public static ICollection<ElementId> GetJoinedElements([NotNull] this Element element, Document document)
    {
        return JoinGeometryUtils.GetJoinedElements(document, element);
    }

    /// <summary>
    ///     Reverses the order in which two elements are joined
    /// </summary>
    /// <remarks>
    ///    The cutting element becomes the cut element and vice versa after the join order is switched
    ///    This functionality is not available for family documents
    /// </remarks>
    /// <param name="document">The document containing the two elements</param>
    /// <param name="firstElement">The first element</param>
    /// <param name="secondElement">
    ///    The second element. This element must be joined to the first element
    /// </param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Document is not a project document<br />
    ///    The element firstElement was not found in the given document<br />
    ///    The element secondElement was not found in the given document<br />
    ///    The elements are not joined<br />
    ///    The elements cannot be joined
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    Unable to switch the join order of these elements
    /// </exception>
    public static void SwitchJoinOrder([NotNull] this Element firstElement, [NotNull] Element secondElement, Document document)
    {
        JoinGeometryUtils.SwitchJoinOrder(document, firstElement, secondElement);
    }

    /// <summary>
    ///    Determines whether the first of two joined elements is cutting the second element
    /// </summary>
    /// <remarks>This functionality is not available for family documents</remarks>
    /// <param name="document">The document containing the two elements</param>
    /// <param name="firstElement">The first element</param>
    /// <param name="secondElement">The second element</param>
    /// <returns>
    ///    True if the secondElement is cut by the firstElement, false if the secondElement is cut by the firstElement
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    Document is not a project document<br />
    ///    The element firstElement was not found in the given document<br />
    ///    The element secondElement was not found in the given document<br />
    ///    The elements are not joined
    /// </exception>
    [Pure]
    [return: MarshalAs(UnmanagedType.U1)]
    public static bool IsCuttingElementInJoin([NotNull] this Element firstElement, [NotNull] Element secondElement, Document document)
    {
        return JoinGeometryUtils.IsCuttingElementInJoin(document, firstElement, secondElement);
    }

    /// <summary>
    ///     Creates an instance of a curve with a new coordinate
    /// </summary>
    /// <param name="line">Initial curve</param>
    /// <param name="x">New coordinate</param>
    /// <returns>The new bound line</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
    ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
    /// </exception>
    [Pure]
    private static Line SetCoordinateX([NotNull] this Line line, double x)
    {
        var endPoint0 = line.GetEndPoint(0);
        var endPoint1 = line.GetEndPoint(1);
        return Line.CreateBound(new XYZ(x, endPoint0.Y, endPoint0.Z), new XYZ(x, endPoint1.Y, endPoint1.Z));
    }

    /// <summary>
    ///     Creates an instance of a curve with a new coordinate
    /// </summary>
    /// <param name="line">Initial curve</param>
    /// <param name="y">New coordinate</param>
    /// <returns>The new bound line</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
    ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
    /// </exception>
    [Pure]
    private static Line SetCoordinateY([NotNull] this Line line, double y)
    {
        var endPoint0 = line.GetEndPoint(0);
        var endPoint1 = line.GetEndPoint(1);
        return Line.CreateBound(new XYZ(endPoint0.X, y, endPoint0.Z), new XYZ(endPoint1.X, y, endPoint1.Z));
    }

    /// <summary>
    ///     Creates an instance of a curve with a new coordinate
    /// </summary>
    /// <param name="line">Initial curve</param>
    /// <param name="z">New coordinate</param>
    /// <returns>The new bound line</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
    ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
    /// </exception>
    [Pure]
    private static Line SetCoordinateZ([NotNull] this Line line, double z)
    {
        var endPoint0 = line.GetEndPoint(0);
        var endPoint1 = line.GetEndPoint(1);
        return Line.CreateBound(new XYZ(endPoint0.X, endPoint0.Y, z), new XYZ(endPoint1.X, endPoint1.Y, z));
    }

    /// <summary>
    ///     Creates an instance of a curve with a new coordinate
    /// </summary>
    /// <param name="arc">Initial curve</param>
    /// <param name="x">New coordinate</param>
    /// <returns>The new bound line</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
    ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
    /// </exception>
    [Pure]
    private static Arc SetCoordinateX([NotNull] this Arc arc, double x)
    {
        var endPoint0 = arc.GetEndPoint(0);
        var endPoint1 = arc.GetEndPoint(1);
        var centerPoint = arc.Evaluate(0.5, true);
        return Arc.Create(new XYZ(x, endPoint0.Y, endPoint0.Z), new XYZ(x, endPoint1.Y, endPoint1.Z), new XYZ(x, centerPoint.Y, centerPoint.Z));
    }

    /// <summary>
    ///     Creates an instance of a curve with a new coordinate
    /// </summary>
    /// <param name="arc">Initial curve</param>
    /// <param name="y">New coordinate</param>
    /// <returns>The new bound line</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
    ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
    /// </exception>
    [Pure]
    private static Arc SetCoordinateY([NotNull] this Arc arc, double y)
    {
        var endPoint0 = arc.GetEndPoint(0);
        var endPoint1 = arc.GetEndPoint(1);
        var centerPoint = arc.Evaluate(0.5, true);
        return Arc.Create(new XYZ(endPoint0.X, y, endPoint0.Z), new XYZ(endPoint1.X, y, endPoint1.Z), new XYZ(centerPoint.X, y, centerPoint.Z));
    }

    /// <summary>
    ///     Creates an instance of a curve with a new coordinate
    /// </summary>
    /// <param name="arc">Initial curve</param>
    /// <param name="z">New coordinate</param>
    /// <returns>The new bound line</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
    ///    Curve length is too small for Revit's tolerance (as identified by Application.ShortCurveTolerance)
    /// </exception>
    [Pure]
    private static Arc SetCoordinateZ([NotNull] this Arc arc, double z)
    {
        var endPoint0 = arc.GetEndPoint(0);
        var endPoint1 = arc.GetEndPoint(1);
        var centerPoint = arc.Evaluate(0.5, true);
        return Arc.Create(new XYZ(endPoint0.X, endPoint0.Y, z), new XYZ(endPoint1.X, endPoint1.Y, z), new XYZ(centerPoint.X, centerPoint.Y, z));
    }
}