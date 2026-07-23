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
        /// <summary>Exports a list of fabrication parts into PCF format.</summary>
        /// <param name="ids">
        ///    An array of FabricationPart element identifiers. Non-fabrication parts are ignored.
        /// </param>
        /// <param name="filename">The name given to the output file.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Fabrication configuration is missing.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void ExportToPcf(string filename, IList<ElementId> ids)
        {
            FabricationUtils.ExportToPCF(document, ids, filename);
        }
    }

    /// <param name="connector">The first connector.</param>
    extension(Connector connector)
    {
        /// <summary>Check if two connectors are valid to connect directly without couplings.</summary>
        /// <param name="other">Second connector to check against.</param>
        /// <returns>True if connection is valid otherwise false.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool ValidateFabricationConnectivity(Connector other)
        {
            var document = connector.ConnectorManager.Owner.Document;
            return FabricationUtils.ValidateConnectivity(document, connector, other);
        }
    }
}