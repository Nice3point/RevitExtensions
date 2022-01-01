using Autodesk.Revit.DB;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Element Extensions
/// </summary>
public static class ElementExtensions
{
    /// <summary>
    ///     Returns the parameter found on an instance or type
    /// </summary>
    [CanBeNull]
    [Pure]
    public static Parameter GetParameter(this Element element, BuiltInParameter parameter)
    {
        var instanceParameter = element.get_Parameter(parameter);
        if (instanceParameter is not null && instanceParameter.HasValue) return instanceParameter;
        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;
        var elementType = element.Document.GetElement(elementTypeId);
        var symbolParameter = elementType.get_Parameter(parameter);
        return symbolParameter ?? instanceParameter;
    }

    /// <summary>
    ///     Returns the parameter found on an instance or type
    /// </summary>
    [CanBeNull]
    [Pure]
    public static Parameter GetParameter(this Element element, string parameter)
    {
        var instanceParameter = element.LookupParameter(parameter);
        if (instanceParameter is not null && instanceParameter.HasValue) return instanceParameter;
        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;
        var elementType = element.Document.GetElement(elementTypeId);
        var symbolParameter = elementType.LookupParameter(parameter);
        return symbolParameter ?? instanceParameter;
    }
}