using Autodesk.Revit.DB.ExtensibleStorage;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit schema extensions
/// </summary>
public static class SchemaExtensions
{
    /// <summary>
    ///    Stores data in the element. Existing data is overwritten
    /// </summary>
    /// <param name="element">The element that will store the data</param>
    /// <param name="schema">Existing schema</param>
    /// <param name="data">Type of data</param>
    /// <param name="fieldName">The Field name</param>
    /// <typeparam name="T">The type of data to be stored in the schema. The type must match the type of data specified in the SchemaBuilder</typeparam>
    /// <returns>True if entity save succeeded</returns>
    /// <example>
    ///     <code>
    ///         document.ProjectInformation.SaveEntity(schema, "data", "schemaField")
    ///     </code>
    /// </example>
    public static bool SaveEntity<T>([NotNull] this Element element, Schema schema, T data, string fieldName)
    {
        var field = schema.GetField(fieldName);
        if (field is null) return false;
        var entity = schema.GetEntity(element);
        if (entity is null) return false;
        entity.Set(field, data);
        element.SetEntity(entity);
        return true;
    }

    /// <summary>
    ///     Retrieves the value stored in the schema from the element
    /// </summary>
    /// <param name="element">The element that stores the data</param>
    /// <param name="schema">Existing schema</param>
    /// <param name="fieldName">The Field name</param>
    /// <typeparam name="T">The type of data to be stored in the schema. The type must match the type of data specified in the SchemaBuilder</typeparam>
    /// <returns>Data stored in the element. null will be returned if the field does not exist or the data has not been saved before</returns>
    /// <example>
    ///     <code>
    ///         document.ProjectInformation.LoadEntity&lt;string&lt;(schema, "schemaField")
    ///     </code>
    /// </example>
    [Pure]
    [CanBeNull]
    public static T LoadEntity<T>([NotNull] this Element element, Schema schema, string fieldName)
    {
        var field = schema.GetField(fieldName);
        var entity = schema.GetEntity(element);
        return entity is null || field is null ? default : entity.Get<T>(field);
    }

    [CanBeNull]
    [ContractAnnotation("element:null => null")]
    private static Entity GetEntity(this Schema schema, [CanBeNull] Element element)
    {
        if (element is null) return null;
        var entity = element.GetEntity(schema);
        if (entity.Schema is not null) return entity;
        entity = new Entity(schema);
        return entity;
    }
}