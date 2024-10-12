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
    /// <summary>Returns the AnalyticalToPhysicalAssociationManager for this document.</summary>
    /// <param name="document">Revit document.</param>
    [Pure]
    public static AnalyticalToPhysicalAssociationManager GetAnalyticalToPhysicalAssociationManager(this Document document)
    {
        return AnalyticalToPhysicalAssociationManager.GetAnalyticalToPhysicalAssociationManager(document);
    }

    /// <summary>Returns true if the element is an analytical element.</summary>
    /// <param name="element">The element to be checked.</param>
    [Pure]
    public static bool IsAnalyticalElement(this Element element)
    {
        return AnalyticalToPhysicalAssociationManager.IsAnalyticalElement(element.Document, element.Id);
    }

    /// <summary>Returns true if the element is a physical element.</summary>
    /// <param name="element">The element to be checked.</param>
    [Pure]
    public static bool IsPhysicalElement(this Element element)
    {
        return AnalyticalToPhysicalAssociationManager.IsPhysicalElement(element.Document, element.Id);
    }
}
#endif