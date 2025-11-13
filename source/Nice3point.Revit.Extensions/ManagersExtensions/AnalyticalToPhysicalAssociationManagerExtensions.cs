#if REVIT2023_OR_GREATER
using Autodesk.Revit.DB.Structure;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.AnalyticalToPhysicalAssociationManager"/> class.
/// </summary>
[PublicAPI]
public static class AnalyticalToPhysicalAssociationManagerExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>Returns true if the element is an analytical element.</summary>
        [Pure]
        public bool IsAnalyticalElement()
        {
            return AnalyticalToPhysicalAssociationManager.IsAnalyticalElement(element.Document, element.Id);
        }

        /// <summary>Returns true if the element is a physical element.</summary>
        [Pure]
        public bool IsPhysicalElement()
        {
            return AnalyticalToPhysicalAssociationManager.IsPhysicalElement(element.Document, element.Id);
        }
    }

    /// <param name="document">The Revit document.</param>
    extension(Document document)
    {
        /// <summary>Returns the AnalyticalToPhysicalAssociationManager for this document.</summary>
        [Pure]
        public AnalyticalToPhysicalAssociationManager GetAnalyticalToPhysicalAssociationManager()
        {
            return AnalyticalToPhysicalAssociationManager.GetAnalyticalToPhysicalAssociationManager(document);
        }
    }
}
#endif