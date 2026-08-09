

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ParameterFilterUtilities"/> class.
/// </summary>
[PublicAPI]
public static class ParameterFilterUtilitiesExtensions
{
    /// <param name="parameterFilter">The source parameter filter.</param>
    extension(ParameterFilterElement parameterFilter)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterUtilities.GetAllFilterableCategories"/>
        [Pure]
        public static ICollection<ElementId> GetAllFilterableCategories()
        {
            return ParameterFilterUtilities.GetAllFilterableCategories();
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterUtilities.GetFilterableParametersInCommon(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        [Pure]
        public static ICollection<ElementId> GetFilterableParametersInCommon(Document document, ICollection<ElementId> categories)
        {
            return ParameterFilterUtilities.GetFilterableParametersInCommon(document, categories);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterUtilities.GetInapplicableParameters(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},System.Collections.Generic.IList{Autodesk.Revit.DB.ElementId})"/>
        [Pure]
        public static IList<ElementId> GetInapplicableParameters(Document document, ICollection<ElementId> categories, IList<ElementId> parameters)
        {
            return ParameterFilterUtilities.GetInapplicableParameters(document, categories, parameters);
        }
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterUtilities.IsParameterApplicable(Autodesk.Revit.DB.Element,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public bool IsParameterApplicable(ElementId parameterId)
        {
            return ParameterFilterUtilities.IsParameterApplicable(element, parameterId);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterUtilities.IsParameterApplicable(Autodesk.Revit.DB.Element,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public bool IsParameterApplicable(Parameter parameter)
        {
            return ParameterFilterUtilities.IsParameterApplicable(element, parameter.Id);
        }
    }

    /// <param name="categories">The source category ids.</param>
    extension(ICollection<ElementId> categories)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ParameterFilterUtilities.RemoveUnfilterableCategories(System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})"/>
        [Pure]
        public ICollection<ElementId> RemoveUnfilterableCategories()
        {
            return ParameterFilterUtilities.RemoveUnfilterableCategories(categories);
        }
    }
}