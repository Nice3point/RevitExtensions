using Autodesk.Revit.DB.Plumbing;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils"/> class.
/// </summary>
public static class PlumbingUtilsExtensions
{
    /// <summary>Connects placeholders that looks like elbow connection.</summary>
    /// <remarks>
    ///    The placeholders may have physical connection or may not connect at all.
    ///    In the latter case, the first one connects to the end of second one.
    ///    If connection fails, the placeholders cannot be physically connected.
    /// </remarks>
    /// <param name="connector1">The first end connector of placeholder to be connected to.</param>
    /// <param name="connector2">The second end connector of placeholder to be connected to.</param>
    /// <returns>True if connection succeeds, false otherwise.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The owner of connector is not pipe placeholder.
    ///    -or-
    ///    The owners of connectors belong to different types of system.
    /// </exception>
    public static bool ConnectPipePlaceholdersAtElbow(this Connector connector1, Connector connector2)
    {
        var document = connector1.ConnectorManager.Owner.Document;
        return PlumbingUtils.ConnectPipePlaceholdersAtElbow(document, connector1, connector2);
    }

    /// <summary>Connects three placeholders that looks like Tee connection.</summary>
    /// <remarks>
    ///    The three placeholders may or may not have physically connections. However,
    ///    the first one should be collinear with the second and third one must have
    ///    intersection with first and second.
    ///    If first placeholder and second placeholder have the same size, the second one
    ///    is merged with first one and original placeholder element will be invalid.
    ///    If connection fails, the placeholders cannot be physically connected.
    /// </remarks>
    /// <param name="connector1">
    ///    The first end connector of placeholder to be connected to the second.
    /// </param>
    /// <param name="connector2">
    ///    The second end connector of placeholder to be connected to the first.
    /// </param>
    /// <param name="connector3">
    ///    The third end connector of placeholder to be connected to the first or second.
    /// </param>
    /// <returns>True if connection succeeds, false otherwise.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The owner of connector is not pipe placeholder.
    ///    -or-
    ///    The owners of connectors belong to different types of system.
    ///    -or-
    ///    The curves of connector1 and connector2 are not collinear or either the connecto1 or connector2 is not connector of curve end.
    /// </exception>
    public static bool ConnectPipePlaceholdersAtTee(this Connector connector1, Connector connector2, Connector connector3)
    {
        var document = connector1.ConnectorManager.Owner.Document;
        return PlumbingUtils.ConnectPipePlaceholdersAtTee(document, connector1, connector2, connector3);
    }

    /// <summary>Connects placeholders that looks like Cross connection.</summary>
    /// <remarks>
    ///    The placeholders may or may not have physical connection. However
    ///    a) The ends of four connectors should intersect at same point;
    ///    b) the first and second placeholders should be collinear each other;
    ///    c) the third and fourth placeholders should be collinear each other and
    ///    d) the third and fourth should have intersection with first or second placeholder.
    ///    If connection fails, the placeholders cannot be physically connected.
    /// </remarks>
    /// <param name="connector1">
    ///    The first end connector of placeholder to be connected to the second.
    /// </param>
    /// <param name="connector2">
    ///    The second end connector of placeholder to be connected to the first.
    /// </param>
    /// <param name="connector3">
    ///    The third end connector of placeholder to be connected to the forth.
    /// </param>
    /// <param name="connector4">
    ///    The fourth end connector of placeholder to be connected to the third.
    /// </param>
    /// <returns>True if connection succeeds, false otherwise.</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The owner of connector is not pipe placeholder.
    ///    -or-
    ///    The owners of connectors belong to different types of system.
    ///    -or-
    ///    The curves of connector1 and connector2 are not collinear or either the connecto1 or connector2 is not connector of curve end.
    /// </exception>
    public static bool ConnectPipePlaceholdersAtCross(this Connector connector1, Connector connector2, Connector connector3, Connector connector4)
    {
        var document = connector1.ConnectorManager.Owner.Document;
        return PlumbingUtils.ConnectPipePlaceholdersAtCross(document, connector1, connector2, connector3, connector4);
    }

    /// <summary>
    ///    Places caps on the open connectors of the pipe curve.
    /// </summary>
    /// <remarks>
    ///    In order to place the cap, the cap type should be defined in the routing preferences that associates with the pipe type of the given element.
    ///    If the typeId is a valid element id, it will be used to override the pipe type that associates with the pipe type of the given element.
    /// </remarks>
    /// <param name="pipe">Pipe curve</param>
    /// <param name="typeId">
    ///    Pipe type element id.
    ///    Default is invalidElementId.
    /// </param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The pipe has no opened piping connector.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    this operation failed.
    /// </exception>
    public static void PlaceCapOnOpenEnds(this Pipe pipe, ElementId typeId)
    {
        PlumbingUtils.PlaceCapOnOpenEnds(pipe.Document, pipe.Id, typeId);
    }

    /// <summary>
    ///    Places caps on the open connectors of the pipe curve.
    /// </summary>
    /// <remarks>
    ///    In order to place the cap, the cap type should be defined in the routing preferences that associates with the pipe type of the given element.
    /// </remarks>
    /// <param name="pipe">Pipe curve</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The pipe has no opened piping connector.
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    this operation failed.
    /// </exception>
    public static void PlaceCapOnOpenEnds(this Pipe pipe)
    {
        PlumbingUtils.PlaceCapOnOpenEnds(pipe.Document, pipe.Id, null);
    }

    /// <summary>
    ///    Checks if there is open piping connector for the given pipe curve.
    /// </summary>
    /// <param name="pipe">Pipe to check.</param>
    /// <returns>True if given pipe has open piping connector, false otherwise.</returns>
    [Pure]
    public static bool HasOpenConnector(this Pipe pipe)
    {
        return PlumbingUtils.HasOpenConnector(pipe.Document, pipe.Id);
    }

    /// <summary>Breaks the pipe curve into two parts at the given position.</summary>
    /// <remarks>This method is not applicable for breaking the flex pipe.</remarks>
    /// <param name="pipe">The pipe curve to break.</param>
    /// <param name="breakPoint">The break point on the pipe curve.</param>
    /// <returns>
    ///    The new pipe curve element id if successful otherwise if a failure occurred an invalidElementId is returned.
    /// </returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    The given point is not on the pipe curve.
    /// </exception>
    public static ElementId BreakCurve(this Pipe pipe, XYZ breakPoint)
    {
        return PlumbingUtils.BreakCurve(pipe.Document, pipe.Id, breakPoint);
    }
}