using JetBrains.Annotations;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Element Extensions
/// </summary>
[PublicAPI]
public static class ElementExtensions
{
    /// <param name="element">The source element</param>
    extension(Element element)
    {
#if REVIT2022_OR_GREATER
        /// <summary>
        ///     Find a parameter in the instance or symbol by identifier
        /// </summary>
        /// <param name="parameter">Identifier of the built-in parameter</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///     ForgeTypeId does not identify a built-in parameter. See Parameter.IsBuiltInParameter(ForgeTypeId) and Parameter.GetParameterTypeId(BuiltInParameter).
        /// </exception>
        [Pure]
        public Parameter? FindParameter(ForgeTypeId parameter)
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
        /// <param name="parameter">The built-in parameter ID</param>
        [Pure]
        public Parameter? FindParameter(BuiltInParameter parameter)
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
        /// <param name="definition">The internal or external definition of the parameter</param>
        [Pure]
        public Parameter? FindParameter(Definition definition)
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
        /// <param name="guid">The unique id associated with the shared parameter</param>
        [Pure]
        public Parameter? FindParameter(Guid guid)
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
        /// <param name="parameter">The name of the parameter to be found</param>
        [Pure]
        public Parameter? FindParameter(string parameter)
        {
            var instanceParameter = element.LookupParameter(parameter);
            if (instanceParameter is not null) return instanceParameter;

            var elementTypeId = element.GetTypeId();
            if (elementTypeId == ElementId.InvalidElementId) return null;

            var elementType = element.Document.GetElement(elementTypeId);
            return elementType.LookupParameter(parameter);
        }
    }
}