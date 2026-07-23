using Autodesk.Revit.DB.Mechanical;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Mechanical;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Mechanical.MechanicalUtils"/> class.
/// </summary>
[PublicAPI]
public static class MechanicalUtilsExtensions
{
    /// <param name="duct">The source duct.</param>
    extension(Duct duct)
    {
        /// <summary>Breaks the duct curve into two parts at the given position.</summary>
        /// <remarks>This method is not applicable for breaking the flex duct.</remarks>
        /// <param name="breakPoint">The break point on the duct curve.</param>
        /// <returns>
        ///    The new duct curve element id if successful otherwise if a failure occurred an invalidElementId is returned.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    "The element is neither a duct nor a duct placeholder."
        ///    -or-
        ///    "The given point is not on the duct curve."
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public ElementId BreakCurve(XYZ breakPoint)
        {
            return MechanicalUtils.BreakCurve(duct.Document, duct.Id, breakPoint);
        }

        /// <summary>
        ///    Connects an air terminal to a duct directly (without the need for a tee or takeoff).
        /// </summary>
        /// <remarks>
        ///    The current location of the air terminal will be projected to the duct centerline, and if the point can be successfully projected,
        ///    the air terminal will be placed on the most suitable face of the duct.
        /// </remarks>
        /// <param name="airTerminalId">The air terminal id.</param>
        /// <returns>True if connection succeeds, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The familyinstance is not air terminal.
        ///    -or-
        ///    The element is not duct curve.
        ///    -or-
        ///    The air terminal already has physical connection.
        ///    -or-
        ///    The air terminal connector origin doesn't project within the center line of the duct.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public bool ConnectAirTerminal(ElementId airTerminalId)
        {
            return MechanicalUtils.ConnectAirTerminalOnDuct(duct.Document, airTerminalId, duct.Id);
        }
    }

    /// <param name="connector">The first connector.</param>
    extension(Connector connector)
    {
        /// <summary>Connects a pair of placeholders that can intersect in an Elbow connection.</summary>
        /// <remarks>
        ///    The placeholders may have a physical intersection but this is not required.
        ///    If they are not intersecting the connectors must be coplanar and able to be moved to
        ///    intersect each other.
        ///    If connection fails, the placeholders cannot be physically connected.
        /// </remarks>
        /// <param name="other">The end connector of the second placeholder.</param>
        /// <returns>True if connection succeeds, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The owner of connector is not duct placeholder.
        ///    -or-
        ///    The owners of connectors belong to different types of system.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public bool ConnectDuctPlaceholdersAtElbow(Connector other)
        {
            return MechanicalUtils.ConnectDuctPlaceholdersAtElbow(connector.ConnectorManager.Owner.Document, connector, other);
        }

        /// <summary>Connects a trio of placeholders that can intersect in a Tee connection.</summary>
        /// <remarks>
        /// The three placeholders may or may not have physical connections. However,
        /// the first connector should be collinear with the second connector, and the third connector must have
        /// be able to be extended to have an intersection with first and second.
        /// <p>If first placeholder and second placeholder have the same size, the second one
        /// is merged with first one and original placeholder element will become invalid.
        /// If connection fails, the placeholders cannot be physically connected.</p></remarks>
        /// <param name="connector2">The end connector of second placeholder.</param>
        /// <param name="connector3">The end connector of the third placeholder.</param>
        /// <returns>True if connection succeeds, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The owner of connector is not duct placeholder.
        ///    -or-
        ///    The owners of connectors belong to different types of system.
        ///    -or-
        ///    The curves of connector1 and connector2 are not collinear or either the connecto1 or connector2 is not connector of curve end.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public bool ConnectDuctPlaceholdersAtTee(Connector connector2, Connector connector3)
        {
            return MechanicalUtils.ConnectDuctPlaceholdersAtTee(connector.ConnectorManager.Owner.Document, connector, connector2, connector3);
        }

        /// <summary>Connects a group of placeholders that can intersect in a Cross connection.</summary>
        /// <remarks>
        ///    The placeholders may or may not have physical connection. However:
        ///    <list type="bullet"><item>The ends of four connectors should intersect at same point.</item><item>The first and second placeholders should be collinear each other.</item><item>The third and fourth placeholders should be collinear each other.</item><item>The third and fourth should have intersection with first or second placeholder.</item></list>
        ///    If connection fails, the placeholders cannot be physically connected.
        /// </remarks>
        /// <param name="connector2">The end connector of the second placeholder.</param>
        /// <param name="connector3">The end connector of the third placeholder.</param>
        /// <param name="connector4">The end connector of the fourth placeholder.</param>
        /// <returns>True if connection succeeds, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The owner of connector is not duct placeholder.
        ///    -or-
        ///    The owners of connectors belong to different types of system.
        ///    -or-
        ///    The curves of connector1 and connector2 are not collinear or either the connecto1 or connector2 is not connector of curve end.
        ///    -or-
        ///    The curves of connector3 and connector4 are not collinear or either the connecto1 or connector2 is not connector of curve end.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public bool ConnectDuctPlaceholdersAtCross(Connector connector2, Connector connector3, Connector connector4)
        {
            return MechanicalUtils.ConnectDuctPlaceholdersAtCross(connector.ConnectorManager.Owner.Document, connector, connector2, connector3, connector4);
        }
    }

    /// <param name="placeholderIds">The placeholder element ids.</param>
    extension(ICollection<ElementId> placeholderIds)
    {
        /// <summary>Converts a collection of duct placeholder elements into duct elements.</summary>
        /// <remarks>
        ///    Once conversion succeeds, the duct placeholder elements are deleted.
        ///    The new duct and fitting elements are created and connections are established.
        /// </remarks>
        /// <param name="document">The document.</param>
        /// <returns>A collection of element IDs of ducts and fittings.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The given element id set is empty.
        ///    -or-
        ///    The given element IDs (placeholderIds) are not duct placeholders.
        ///    -or-
        ///    The elements belong to different types of system.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public ICollection<ElementId> ConvertDuctPlaceholders(Document document)
        {
            return MechanicalUtils.ConvertDuctPlaceholders(document, placeholderIds);
        }
    }
}