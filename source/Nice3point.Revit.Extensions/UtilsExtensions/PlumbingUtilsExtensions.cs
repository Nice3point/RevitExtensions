using Autodesk.Revit.DB.Plumbing;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils"/> class.
/// </summary>
[PublicAPI]
public static class PlumbingUtilsExtensions
{
    /// <param name="connector1">The first end connector of placeholder to be connected to.</param>
    extension(Connector connector1)
    {
        /// <summary>Connects placeholders that looks like elbow connection.</summary>
        /// <remarks>
        ///    The placeholders may have physical connection or may not connect at all.
        ///    In the latter case, the first one connects to the end of second one.
        ///    If connection fails, the placeholders cannot be physically connected.
        /// </remarks>
        /// <param name="connector2">The second end connector of placeholder to be connected to.</param>
        /// <returns>True if connection succeeds, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The owner of connector is not pipe placeholder.
        ///    -or-
        ///    The owners of connectors belong to different types of system.
        /// </exception>
        public bool ConnectPipePlaceholdersAtElbow(Connector connector2)
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
        public bool ConnectPipePlaceholdersAtTee(Connector connector2, Connector connector3)
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
        public bool ConnectPipePlaceholdersAtCross(Connector connector2, Connector connector3, Connector connector4)
        {
            var document = connector1.ConnectorManager.Owner.Document;
            return PlumbingUtils.ConnectPipePlaceholdersAtCross(document, connector1, connector2, connector3, connector4);
        }
    }

    /// <param name="pipe">The source pipe curve.</param>
    extension(Pipe pipe)
    {
        /// <summary>
        ///    Places caps on the open connectors of the pipe curve.
        /// </summary>
        /// <remarks>
        ///    In order to place the cap, the cap type should be defined in the routing preferences that associates with the pipe type of the given element.
        /// </remarks>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The pipe has no opened piping connector.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    this operation failed.
        /// </exception>
        public void PlaceCapOnOpenEnds()
        {
            PlumbingUtils.PlaceCapOnOpenEnds(pipe.Document, pipe.Id, ElementId.InvalidElementId);
        }

        /// <summary>
        ///    Places caps on the open connectors of the pipe curve.
        /// </summary>
        /// <remarks>
        ///    In order to place the cap, the cap type should be defined in the routing preferences that associates with the pipe type of the given element.
        ///    If the typeId is a valid element id, it will be used to override the pipe type that associates with the pipe type of the given element.
        /// </remarks>
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
        public void PlaceCapOnOpenEnds(ElementId typeId)
        {
            PlumbingUtils.PlaceCapOnOpenEnds(pipe.Document, pipe.Id, typeId);
        }

        /// <summary>
        ///    Checks if there is open piping connector for the given pipe curve.
        /// </summary>
        /// <returns>True if given pipe has open piping connector, false otherwise.</returns>
        public bool HasOpenConnector => PlumbingUtils.HasOpenConnector(pipe.Document, pipe.Id);

        /// <summary>Breaks the pipe curve into two parts at the given position.</summary>
        /// <remarks>This method is not applicable for breaking the flex pipe.</remarks>
        /// <param name="breakPoint">The break point on the pipe curve.</param>
        /// <returns>
        ///    The new pipe curve element if successful otherwise null if a failure occurred.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The given point is not on the pipe curve.
        /// </exception>
        public Pipe? BreakCurve(XYZ breakPoint)
        {
            var curveId = PlumbingUtils.BreakCurve(pipe.Document, pipe.Id, breakPoint);
            if (curveId == ElementId.InvalidElementId) return null;

            return curveId.ToElement<Pipe>(pipe.Document);
        }
    }
}