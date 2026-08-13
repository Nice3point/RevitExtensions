using Autodesk.Revit.DB.Structure;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.RebarShapeParameters" /> class.
/// </summary>
[PublicAPI]
public static class RebarShapeParametersExtensions
{
    /// <param name="rebarShape">The source rebar shape.</param>
    extension(RebarShape rebarShape)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarShapeParameters.GetAllRebarShapeParameters(Autodesk.Revit.DB.Document)" />
        [Pure]
        public IList<ElementId> GetAllParameters()
        {
            return RebarShapeParameters.GetAllRebarShapeParameters(rebarShape.Document);
        }
    }

    /// <param name="externalDefinition">The source shared parameter.</param>
    extension(ExternalDefinition externalDefinition)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarShapeParameters.IsValidExternalDefinition(Autodesk.Revit.DB.ExternalDefinition)" />
        public bool IsValidRebarShapeParameter => RebarShapeParameters.IsValidExternalDefinition(externalDefinition);

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarShapeParameters.GetElementIdForExternalDefinition(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ExternalDefinition)" />
        [Pure]
        public ElementId GetRebarShapeParameterElementId(Document document)
        {
            return RebarShapeParameters.GetElementIdForExternalDefinition(document, externalDefinition);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarShapeParameters.GetOrCreateElementIdForExternalDefinition(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ExternalDefinition)" />
        public ElementId GetOrCreateRebarShapeParameterElementId(Document document)
        {
            return RebarShapeParameters.GetOrCreateElementIdForExternalDefinition(document, externalDefinition);
        }
    }

    /// <param name="definitionFile">The source definition file.</param>
    extension(DefinitionFile definitionFile)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarShapeParameters.GetExternalDefinitionForElementId(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.DefinitionFile)" />
        [Pure]
        public ExternalDefinition? SearchExternalDefinition(Document document, ElementId parameterId)
        {
            return RebarShapeParameters.GetExternalDefinitionForElementId(document, parameterId, definitionFile);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarShapeParameters.GetExternalDefinitionForElementId(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.DefinitionFile)" />
        /// <param name="parameter">The shared parameter</param>
        [Pure]
        public ExternalDefinition? SearchExternalDefinition(Parameter parameter)
        {
            return RebarShapeParameters.GetExternalDefinitionForElementId(parameter.Element.Document, parameter.Id, definitionFile);
        }
    }
}
