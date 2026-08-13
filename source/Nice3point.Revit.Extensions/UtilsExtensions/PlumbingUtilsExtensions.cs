using Autodesk.Revit.DB.Plumbing;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Plumbing;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils" /> class.
/// </summary>
[PublicAPI]
public static class PlumbingUtilsExtensions
{
    /// <param name="connector1">The first end connector of placeholder to be connected to.</param>
    extension(Connector connector1)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils.ConnectPipePlaceholdersAtElbow(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector)" />
        public bool ConnectPipePlaceholdersAtElbow(Connector connector2)
        {
            var document = connector1.ConnectorManager.Owner.Document;
            return PlumbingUtils.ConnectPipePlaceholdersAtElbow(document, connector1, connector2);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils.ConnectPipePlaceholdersAtTee(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector)" />
        public bool ConnectPipePlaceholdersAtTee(Connector connector2, Connector connector3)
        {
            var document = connector1.ConnectorManager.Owner.Document;
            return PlumbingUtils.ConnectPipePlaceholdersAtTee(document, connector1, connector2, connector3);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils.ConnectPipePlaceholdersAtCross(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector,Autodesk.Revit.DB.Connector)" />
        public bool ConnectPipePlaceholdersAtCross(Connector connector2, Connector connector3, Connector connector4)
        {
            var document = connector1.ConnectorManager.Owner.Document;
            return PlumbingUtils.ConnectPipePlaceholdersAtCross(document, connector1, connector2, connector3, connector4);
        }
    }

    /// <param name="pipe">The source pipe curve.</param>
    extension(Pipe pipe)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils.HasOpenConnector(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        /// <summary>
        ///     Checks if there is open piping connector for the given pipe curve.
        /// </summary>
        /// <returns>True if given pipe has open piping connector, false otherwise.</returns>
        public bool HasOpenConnector => PlumbingUtils.HasOpenConnector(pipe.Document, pipe.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils.PlaceCapOnOpenEnds(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        /// <summary>
        ///     Places caps on the open connectors of the pipe curve.
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     The pipe has no opened piping connector.
        /// </exception>
        public void PlaceCapOnOpenEnds()
        {
            PlumbingUtils.PlaceCapOnOpenEnds(pipe.Document, pipe.Id, ElementId.InvalidElementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils.PlaceCapOnOpenEnds(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId)" />
        /// <summary>
        ///     Places caps on the open connectors of the pipe curve.
        /// </summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     The pipe has no opened piping connector.
        /// </exception>
        public void PlaceCapOnOpenEnds(ElementId typeId)
        {
            PlumbingUtils.PlaceCapOnOpenEnds(pipe.Document, pipe.Id, typeId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils.BreakCurve(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.XYZ)" />
        public ElementId BreakCurve(XYZ breakPoint)
        {
            return PlumbingUtils.BreakCurve(pipe.Document, pipe.Id, breakPoint);
        }
    }

    /// <param name="placeholderIds">The source placeholders.</param>
    extension(ICollection<ElementId> placeholderIds)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Plumbing.PlumbingUtils.ConvertPipePlaceholders(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})" />
        public ICollection<ElementId> ConvertPipePlaceholders(Document document)
        {
            return PlumbingUtils.ConvertPipePlaceholders(document, placeholderIds);
        }
    }
}
