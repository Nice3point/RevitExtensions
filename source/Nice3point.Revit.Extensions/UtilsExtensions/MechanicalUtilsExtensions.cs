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
        /// <inheritdoc cref="Autodesk.Revit.DB.Mechanical.MechanicalUtils.BreakCurve(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.XYZ)"/>
        public ElementId BreakCurve(XYZ breakPoint)
        {
            return MechanicalUtils.BreakCurve(duct.Document, duct.Id, breakPoint);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Mechanical.MechanicalUtils.ConnectAirTerminalOnDuct(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)"/>
        public bool ConnectAirTerminal(ElementId airTerminalId)
        {
            return MechanicalUtils.ConnectAirTerminalOnDuct(duct.Document, airTerminalId, duct.Id);
        }
    }

    /// <param name="connector">The first connector.</param>
    extension(Connector connector)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Mechanical.MechanicalUtils.ConnectDuctPlaceholdersAtElbow(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector)"/>
        public bool ConnectDuctPlaceholdersAtElbow(Connector other)
        {
            return MechanicalUtils.ConnectDuctPlaceholdersAtElbow(connector.ConnectorManager.Owner.Document, connector, other);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Mechanical.MechanicalUtils.ConnectDuctPlaceholdersAtTee(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector)"/>
        public bool ConnectDuctPlaceholdersAtTee(Connector connector2, Connector connector3)
        {
            return MechanicalUtils.ConnectDuctPlaceholdersAtTee(connector.ConnectorManager.Owner.Document, connector, connector2, connector3);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Mechanical.MechanicalUtils.ConnectDuctPlaceholdersAtCross(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector)"/>
        public bool ConnectDuctPlaceholdersAtCross(Connector connector2, Connector connector3, Connector connector4)
        {
            return MechanicalUtils.ConnectDuctPlaceholdersAtCross(connector.ConnectorManager.Owner.Document, connector, connector2, connector3, connector4);
        }
    }

    /// <param name="placeholderIds">The placeholder element ids.</param>
    extension(ICollection<ElementId> placeholderIds)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Mechanical.MechanicalUtils.ConvertDuctPlaceholders(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        public ICollection<ElementId> ConvertDuctPlaceholders(Document document)
        {
            return MechanicalUtils.ConvertDuctPlaceholders(document, placeholderIds);
        }
    }
}