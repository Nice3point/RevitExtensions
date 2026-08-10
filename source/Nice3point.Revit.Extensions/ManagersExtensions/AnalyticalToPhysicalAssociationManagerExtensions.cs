#if REVIT2023_OR_GREATER
using Autodesk.Revit.DB.Structure;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.AnalyticalToPhysicalAssociationManager"/> class.
/// </summary>
[PublicAPI]
public static class AnalyticalToPhysicalAssociationManagerExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.AnalyticalToPhysicalAssociationManager.IsAnalyticalElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public bool IsAnalyticalElement => AnalyticalToPhysicalAssociationManager.IsAnalyticalElement(element.Document, element.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.AnalyticalToPhysicalAssociationManager.IsPhysicalElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        public bool IsPhysicalElement => AnalyticalToPhysicalAssociationManager.IsPhysicalElement(element.Document, element.Id);
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.AnalyticalToPhysicalAssociationManager.IsAnalyticalElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public bool IsAnalyticalElement(Document document)
        {
            return AnalyticalToPhysicalAssociationManager.IsAnalyticalElement(document, elementId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.AnalyticalToPhysicalAssociationManager.IsPhysicalElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public bool IsPhysicalElement(Document document)
        {
            return AnalyticalToPhysicalAssociationManager.IsPhysicalElement(document, elementId);
        }
    }

    /// <param name="document">The Revit document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.AnalyticalToPhysicalAssociationManager.GetAnalyticalToPhysicalAssociationManager(Autodesk.Revit.DB.Document)"/>
        [Pure]
        public AnalyticalToPhysicalAssociationManager GetAnalyticalToPhysicalAssociationManager()
        {
            return AnalyticalToPhysicalAssociationManager.GetAnalyticalToPhysicalAssociationManager(document);
        }
    }
}
#endif