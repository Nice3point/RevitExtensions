using Autodesk.Revit.DB.Fabrication;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Fabrication;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Fabrication.FabricationUtils"/> class.
/// </summary>
[PublicAPI]
public static class FabricationUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Fabrication.FabricationUtils.ExportToPCF(Autodesk.Revit.DB.Document,System.Collections.Generic.IList{Autodesk.Revit.DB.ElementId},System.String)"/>
        public void ExportToPcf(string filename, IList<ElementId> ids)
        {
            FabricationUtils.ExportToPCF(document, ids, filename);
        }
    }

    /// <param name="connector">The first connector.</param>
    extension(Connector connector)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Fabrication.FabricationUtils.ValidateConnectivity(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector)"/>
        [Pure]
        public bool ValidateFabricationConnectivity(Connector other)
        {
            var document = connector.ConnectorManager.Owner.Document;
            return FabricationUtils.ValidateConnectivity(document, connector, other);
        }
    }
}