using Autodesk.Revit.DB.ExtensibleStorage;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit schema extensions
/// </summary>
[PublicAPI]
public static class SchemaExtensions
{
    /// <param name="element">The element that stores the data.</param>
    extension(Element element)
    {
        /// <summary>
        ///    Stores data in the element. Existing data is overwritten
        /// </summary>
        /// <param name="schema">Existing schema</param>
        /// <param name="data">Type of data</param>
        /// <param name="fieldName">The Field name</param>
        /// <typeparam name="T">The type of data to be stored in the schema. The type must match the type of data specified in the SchemaBuilder</typeparam>
        /// <returns>True if entity save succeeded</returns>
        /// <example>
        ///     <code>
        ///         wall.SaveEntity(schema, "Factory", "Manufacturer")
        ///     </code>
        /// </example>
        public bool SaveEntity<T>(Schema schema, T data, string fieldName)
        {
            var field = schema.GetField(fieldName);
            if (field is null) return false;

            var entity = GetEntity(schema, element);
            entity.Set(field, data);
            element.SetEntity(entity);
            return true;
        }

        /// <summary>
        ///     Retrieves the value stored in the schema from the element
        /// </summary>
        /// <param name="schema">Existing schema</param>
        /// <param name="fieldName">The Field name</param>
        /// <typeparam name="T">The type of data to be stored in the schema. The type must match the type of data specified in the SchemaBuilder</typeparam>
        /// <returns>Data stored in the element. null will be returned if the field does not exist or the data has not been saved before</returns>
        /// <example>
        ///     <code>
        ///         var value = wall.LoadEntity&lt;string&lt;(schema, "Manufacturer")
        ///     </code>
        /// </example>
        [Pure]
        public T? LoadEntity<T>(Schema schema, string fieldName)
        {
            var field = schema.GetField(fieldName);
            var entity = GetEntity(schema, element);
            return field is null ? default : entity.Get<T>(field);
        }
    }

    private static Entity GetEntity(Schema schema, Element element)
    {
        var entity = element.GetEntity(schema);
        if (entity.Schema is not null) return entity;

        entity = new Entity(schema);
        return entity;
    }
}