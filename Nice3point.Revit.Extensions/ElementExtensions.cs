using Autodesk.Revit.DB;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Element Extensions
/// </summary>
public static class ElementExtensions
{
    /// <summary>
    ///     Gets Element from ElementId
    /// </summary>
    [PublicAPI]
    public static Element ToElement(this ElementId id, Document document)
    {
        return document.GetElement(id);
    }

    /// <summary>
    ///     Gets Element from ElementId and cast to type T
    /// </summary>
    /// <typeparam name="T">A type derived from Element</typeparam>
    [PublicAPI]
    public static T ToElement<T>(this ElementId id, Document document) where T : Element
    {
        return (T) document.GetElement(id);
    }

    /// <summary>
    ///     Checks if ElementID is a category identifier
    /// </summary>
    [PublicAPI]
    public static bool AreEquals(this ElementId elementId, BuiltInCategory category)
    {
        return elementId.IntegerValue == (int) category;
    }

    /// <summary>
    ///     Returns the parameter found on an instance or type
    /// </summary>
    [PublicAPI]
    [CanBeNull]
    public static Parameter GetParameter(this Element element, BuiltInParameter parameter)
    {
        var instanceParameter = element.get_Parameter(parameter);
        if (instanceParameter is not null && instanceParameter.HasValue) return instanceParameter;
        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;
        var elementType = element.Document.GetElement(elementTypeId);
        var symbolParameter = elementType.get_Parameter(parameter);
        return symbolParameter;
    }

    /// <summary>
    ///     Returns the parameter found on an instance or type
    /// </summary>
    [PublicAPI]
    [CanBeNull]
    public static Parameter GetParameter(this Element element, string parameter)
    {
        var instanceParameter = element.LookupParameter(parameter);
        if (instanceParameter is not null && instanceParameter.HasValue) return instanceParameter;
        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;
        var elementType = element.Document.GetElement(elementTypeId);
        var symbolParameter = elementType.LookupParameter(parameter);
        return symbolParameter;
    }
}