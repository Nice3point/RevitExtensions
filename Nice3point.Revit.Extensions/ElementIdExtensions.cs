using Autodesk.Revit.DB;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Element Extensions
/// </summary>
public static class ElementIdExtensions
{
    /// <summary>
    ///     Gets Element from ElementId
    /// </summary>
    public static Element ToElement(this ElementId id, Document document)
    {
        return document.GetElement(id);
    }

    /// <summary>
    ///     Gets Element from ElementId and cast to type T
    /// </summary>
    /// <typeparam name="T">A type derived from Element</typeparam>
    public static T ToElement<T>(this ElementId id, Document document) where T : Element
    {
        return (T) document.GetElement(id);
    }

    /// <summary>
    ///     Checks if ElementID is a category identifier
    /// </summary>
    public static bool AreEquals(this ElementId elementId, BuiltInCategory category)
    {
        return elementId.IntegerValue == (int) category;
    }
}