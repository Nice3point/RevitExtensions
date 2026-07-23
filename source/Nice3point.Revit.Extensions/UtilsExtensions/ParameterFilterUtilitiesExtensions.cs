

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
        /// <summary>
        ///    Returns the set of categories that may be used in a ParameterFilterElement.
        /// </summary>
        /// <returns>The set of all filterable categories.</returns>
        [Pure]
        public static ICollection<ElementId> GetAllFilterableCategories()
        {
            return ParameterFilterUtilities.GetAllFilterableCategories();
        }

        /// <summary>Returns the filterable parameters common to the given categories.</summary>
        /// <remarks>
        ///      <p>A filterable category in Revit has a set of filterable parameters.
        /// The filterable parameters common to a set of categories is the
        /// intersection of all these filterable parameter sets.</p>
        ///      <p>This set defines the set of parameters that may be used to define
        /// a rule on a ParameterFilterElement with a certain set of categories.</p>
        ///    </remarks>
        /// <param name="document">The document containing the categories and parameters to query.</param>
        /// <param name="categories">The categories for which to determine the common parameters.</param>
        /// <returns>The set of filterable parameters common to the given categories.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static ICollection<ElementId> GetFilterableParametersInCommon(Document document, ICollection<ElementId> categories)
        {
            return ParameterFilterUtilities.GetFilterableParametersInCommon(document, categories);
        }

        /// <summary>
        ///    Returns the parameters that are not among the set of filterable
        ///    parameters common to the given categories.
        /// </summary>
        /// <param name="document">The document containing the categories and parameters to query.</param>
        /// <param name="categories">
        ///    The categories that define the set of possibly filterable parameters.
        /// </param>
        /// <param name="parameters">The parameters desired for use in a parameter filter.</param>
        /// <returns>
        ///    A list of parameters from the given array that are not valid
        ///    for use in a parameter filter with the given categories.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static IList<ElementId> GetInapplicableParameters(Document document, ICollection<ElementId> categories, IList<ElementId> parameters)
        {
            return ParameterFilterUtilities.GetInapplicableParameters(document, categories, parameters);
        }
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>Used to determine whether the element supports the given parameter.</summary>
        /// <remarks>
        ///    This operation is supported for external (e.g., shared, project)
        ///    parameters only.
        /// </remarks>
        /// <param name="parameterId">The parameter Id for which to query support.</param>
        /// <returns>True if the element supports the given parameter, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool IsParameterApplicable(ElementId parameterId)
        {
            return ParameterFilterUtilities.IsParameterApplicable(element, parameterId);
        }

        /// <summary>Used to determine whether the element supports the given parameter.</summary>
        /// <remarks>
        ///    This operation is supported for external (e.g., shared, project)
        ///    parameters only.
        /// </remarks>
        /// <param name="parameter">The parameter for which to query support.</param>
        /// <returns>True if the element supports the given parameter, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool IsParameterApplicable(Parameter parameter)
        {
            return ParameterFilterUtilities.IsParameterApplicable(element, parameter.Id);
        }
    }

    /// <param name="categories">The source category ids.</param>
    extension(ICollection<ElementId> categories)
    {
        /// <summary>Removes from the given set the categories that are not filterable.</summary>
        [Pure]
        public ICollection<ElementId> RemoveUnfilterableCategories()
        {
            return ParameterFilterUtilities.RemoveUnfilterableCategories(categories);
        }
    }
}