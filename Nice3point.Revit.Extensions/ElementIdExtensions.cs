using Autodesk.Revit.DB;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Element Extensions
/// </summary>
public static class ElementIdExtensions
{
    /// <summary>
    ///     Gets the Element referenced by the input ElementId
    /// </summary>
    /// <param name="document">Document associated with Element</param>
    /// <param name="id">
    ///     The ElementId, whose referenced Element will be retrieved from the model.
    /// </param>
    /// <returns>The element referenced by the input argument</returns>
    /// <remarks>
    ///     <see langword="null" /> will be returned if the input ElementId doesn't reference to a valid Element.
    /// </remarks>
    [CanBeNull]
    [Pure]
    public static Element ToElement(this ElementId id, Document document)
    {
        return document.GetElement(id);
    }

    /// <summary>
    ///     Gets the Element referenced by the input ElementId and cast to type T
    /// </summary>
    /// <param name="document">Document associated with Element</param>
    /// <param name="id">
    ///     The ElementId, whose referenced Element will be retrieved from the model
    /// </param>
    /// <typeparam name="T">A type derived from Element</typeparam>
    /// <returns>The element referenced by the input argument, casted to type T</returns>
    /// <remarks>
    ///     <see langword="null" /> will be returned if the input ElementId doesn't reference to a valid Element.
    /// </remarks>
    [Pure]
    public static T ToElement<T>(this ElementId id, Document document) where T : Element
    {
        return (T) document.GetElement(id);
    }

    /// <summary>
    ///     Checks if ElementID is a category identifier
    /// </summary>
    [Pure]
    public static bool AreEquals(this ElementId elementId, BuiltInCategory category)
    {
#if R24_OR_GREATER
        return elementId.Value == (long) category;
#else
        return elementId.IntegerValue == (int) category;
#endif
    }

    /// <summary>
    ///     Checks if ElementID is a parameter identifier
    /// </summary>
    [Pure]
    public static bool AreEquals(this ElementId elementId, BuiltInParameter parameter)
    {
#if R24_OR_GREATER
        return elementId.Value == (long) parameter;
#else
        return elementId.IntegerValue == (int) parameter;
#endif
    }
}