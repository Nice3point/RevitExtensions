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
        ///     Stores data in the element. Existing data is overwritten
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
            if (field is null)
            {
                return false;
            }

            var entity = GetEntity(schema, element);
            entity.Set(field, data);
            element.SetEntity(entity);
            return true;
        }
#if REVIT2021_OR_GREATER
        /// <summary>
        ///    Stores data measured in the specified unit in the element. Existing data is overwritten
        /// </summary>
        /// <param name="schema">Existing schema</param>
        /// <param name="data">Type of data</param>
        /// <param name="fieldName">The Field name</param>
        /// <param name="unitTypeId">Identifier of the unit the value is converted from. Must be compatible with the field spec</param>
        /// <typeparam name="T">The type of data to be stored in the schema. The type must match the type of data specified in the SchemaBuilder</typeparam>
        /// <returns>True if entity save succeeded</returns>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The unitTypeId value is not compatible with the field description.
        /// </exception>
        /// <example>
        ///     <code>
        ///         wall.SaveEntity(schema, 0.5, "Thickness", UnitTypeId.Meters)
        ///     </code>
        /// </example>
        public bool SaveEntity<T>(Schema schema, T data, string fieldName, ForgeTypeId unitTypeId)
        {
            var field = schema.GetField(fieldName);
            if (field is null) return false;

            var entity = GetEntity(schema, element);
            entity.Set(field, data, unitTypeId);
            element.SetEntity(entity);
            return true;
        }
#else
        /// <summary>
        ///     Stores data measured in the specified unit in the element. Existing data is overwritten
        /// </summary>
        /// <param name="schema">Existing schema</param>
        /// <param name="data">Type of data</param>
        /// <param name="fieldName">The Field name</param>
        /// <param name="displayUnitType">The unit the value is converted from. Must be compatible with the field unit type</param>
        /// <typeparam name="T">The type of data to be stored in the schema. The type must match the type of data specified in the SchemaBuilder</typeparam>
        /// <returns>True if entity save succeeded</returns>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The displayUnitType value is not compatible with the field description.
        /// </exception>
        /// <example>
        ///     <code>
        ///         wall.SaveEntity(schema, 0.5, "Thickness", DisplayUnitType.DUT_METERS)
        ///     </code>
        /// </example>
        public bool SaveEntity<T>(Schema schema, T data, string fieldName, DisplayUnitType displayUnitType)
        {
            var field = schema.GetField(fieldName);
            if (field is null)
            {
                return false;
            }

            var entity = GetEntity(schema, element);
            entity.Set(field, data, displayUnitType);
            element.SetEntity(entity);
            return true;
        }
#endif

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
            if (field is null)
            {
                return default;
            }

            var entity = element.GetEntity(schema);
            if (!entity.IsValid())
            {
                return default;
            }

            return entity.Get<T>(field);
        }
#if REVIT2021_OR_GREATER
        /// <summary>
        ///     Retrieves the value stored in the schema from the element, converted to the specified unit
        /// </summary>
        /// <param name="schema">Existing schema</param>
        /// <param name="fieldName">The Field name</param>
        /// <param name="unitTypeId">Identifier of the unit the value is converted to. Must be compatible with the field spec</param>
        /// <typeparam name="T">The type of data to be stored in the schema. The type must match the type of data specified in the SchemaBuilder</typeparam>
        /// <returns>Data stored in the element. null will be returned if the field does not exist or the data has not been saved before</returns>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The unitTypeId value is not compatible with the field description.
        /// </exception>
        /// <example>
        ///     <code>
        ///         var value = wall.LoadEntity&lt;double&gt;(schema, "Thickness", UnitTypeId.Meters)
        ///     </code>
        /// </example>
        [Pure]
        public T? LoadEntity<T>(Schema schema, string fieldName, ForgeTypeId unitTypeId)
        {
            var field = schema.GetField(fieldName);
            if (field is null) return default;

            var entity = element.GetEntity(schema);
            if (!entity.IsValid()) return default;

            return entity.Get<T>(field, unitTypeId);
        }
#else
        /// <summary>
        ///     Retrieves the value stored in the schema from the element, converted to the specified unit
        /// </summary>
        /// <param name="schema">Existing schema</param>
        /// <param name="fieldName">The Field name</param>
        /// <param name="displayUnitType">The unit the value is converted to. Must be compatible with the field unit type</param>
        /// <typeparam name="T">The type of data to be stored in the schema. The type must match the type of data specified in the SchemaBuilder</typeparam>
        /// <returns>Data stored in the element. null will be returned if the field does not exist or the data has not been saved before</returns>
        /// <exception cref="Autodesk.Revit.Exceptions.ArgumentException">
        ///     The displayUnitType value is not compatible with the field description.
        /// </exception>
        /// <example>
        ///     <code>
        ///         var value = wall.LoadEntity&lt;double&gt;(schema, "Thickness", DisplayUnitType.DUT_METERS)
        ///     </code>
        /// </example>
        [Pure]
        public T? LoadEntity<T>(Schema schema, string fieldName, DisplayUnitType displayUnitType)
        {
            var field = schema.GetField(fieldName);
            if (field is null)
            {
                return default;
            }

            var entity = element.GetEntity(schema);
            if (!entity.IsValid())
            {
                return default;
            }

            return entity.Get<T>(field, displayUnitType);
        }
#endif
    }

    private static Entity GetEntity(Schema schema, Element element)
    {
        var entity = element.GetEntity(schema);
        return entity.IsValid() ? entity : new Entity(schema);
    }
}
