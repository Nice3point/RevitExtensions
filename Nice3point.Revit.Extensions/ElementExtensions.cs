namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Element Extensions
/// </summary>
[PublicAPI]
public static class ElementExtensions
{
#if REVIT2022_OR_GREATER
    /// <summary>
    ///     Retrieves a parameter from the instance or symbol given identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">Identifier of the built-in parameter</param>
    /// <param name="snoopType">True if you want to snoop the symbol parameter</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     ForgeTypeId does not identify a built-in parameter. See Parameter.IsBuiltInParameter(ForgeTypeId) and Parameter.GetParameterTypeId(BuiltInParameter).
    /// </exception>
    [Pure]
    [Obsolete("Use FindParameter() instead")]
    public static Parameter? GetParameter(this Element element, ForgeTypeId parameter, bool snoopType)
    {
        return FindParameter(element, parameter);
    }

#endif
    /// <summary>
    ///     Retrieves a parameter from the instance or symbol given identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">The built-in parameter ID</param>
    [Pure]
    [Obsolete("Use FindParameter() instead")]
    public static Parameter? GetParameter(this Element element, BuiltInParameter parameter)
    {
        return FindParameter(element, parameter);
    }

    /// <summary>
    ///     Retrieves a parameter from the instance or symbol given identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">The name of the parameter to be retrieved</param>
    [Pure]
    [Obsolete("Use FindParameter() instead")]
    public static Parameter? GetParameter(this Element element, string parameter)
    {
        return FindParameter(element, parameter);
    }

#if REVIT2022_OR_GREATER
    /// <summary>
    ///     Find a parameter in the instance or symbol by identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">Identifier of the built-in parameter</param>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///     ForgeTypeId does not identify a built-in parameter. See Parameter.IsBuiltInParameter(ForgeTypeId) and Parameter.GetParameterTypeId(BuiltInParameter).
    /// </exception>
    [Pure]
    public static Parameter? FindParameter(this Element element, ForgeTypeId parameter)
    {
        var instanceParameter = element.GetParameter(parameter);
        if (instanceParameter is not null) return instanceParameter;

        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;

        var elementType = element.Document.GetElement(elementTypeId);
        return elementType.GetParameter(parameter);
    }

#endif

    /// <summary>
    ///     Find a parameter in the instance or symbol by identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">The built-in parameter ID</param>
    [Pure]
    public static Parameter? FindParameter(this Element element, BuiltInParameter parameter)
    {
        var instanceParameter = element.get_Parameter(parameter);
        if (instanceParameter is not null) return instanceParameter;

        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;

        var elementType = element.Document.GetElement(elementTypeId);
        return elementType.get_Parameter(parameter);
    }

    /// <summary>
    ///     Find a parameter in the instance or symbol by identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="definition">The internal or external definition of the parameter</param>
    [Pure]
    public static Parameter? FindParameter(this Element element, Definition definition)
    {
        var instanceParameter = element.get_Parameter(definition);
        if (instanceParameter is not null) return instanceParameter;

        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;

        var elementType = element.Document.GetElement(elementTypeId);
        return elementType.get_Parameter(definition);
    }

    /// <summary>
    ///     Find a parameter in the instance or symbol by identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="guid">The unique id associated with the shared parameter</param>
    [Pure]
    public static Parameter? FindParameter(this Element element, Guid guid)
    {
        var instanceParameter = element.get_Parameter(guid);
        if (instanceParameter is not null) return instanceParameter;

        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;

        var elementType = element.Document.GetElement(elementTypeId);
        return elementType.get_Parameter(guid);
    }

    /// <summary>
    ///     Find a parameter in the instance or symbol by identifier
    /// </summary>
    /// <param name="element">The element</param>
    /// <param name="parameter">The name of the parameter to be found</param>
    [Pure]
    public static Parameter? FindParameter(this Element element, string parameter)
    {
        var instanceParameter = element.LookupParameter(parameter);
        if (instanceParameter is not null) return instanceParameter;

        var elementTypeId = element.GetTypeId();
        if (elementTypeId == ElementId.InvalidElementId) return null;

        var elementType = element.Document.GetElement(elementTypeId);
        return elementType.LookupParameter(parameter);
    }
}