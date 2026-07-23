using Autodesk.Revit.DB.Structure;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.RebarShapeParameters"/> class.
/// </summary>
[PublicAPI]
public static class RebarShapeParametersExtensions
{
    /// <param name="rebarShape">The source rebar shape.</param>
    extension(RebarShape rebarShape)
    {
        /// <summary>
        ///    List all shape parameters used by all the existing RebarShapes in the specified document.
        /// </summary>
        /// <remarks>
        ///    This method replaces RebarShape.GetAllRebarShapeParameters() from prior releases.
        /// </remarks>
        /// <returns>ElementIds corresponding to the external parameters.</returns>
        [Pure]
        public IList<ElementId> GetAllParameters()
        {
            return RebarShapeParameters.GetAllRebarShapeParameters(rebarShape.Document);
        }
    }

    /// <param name="externalDefinition">The source shared parameter.</param>
    extension(ExternalDefinition externalDefinition)
    {
        /// <summary>
        ///    Checks that an ExternalDefinition (shared parameter) may be used as a Rebar Shape parameter.
        /// </summary>
        /// <remarks>
        ///    A Rebar Shape parameter must be an ExternalDefinition with a ParameterType of Length.
        /// </remarks>
        /// <returns>True if the definition is of type Length, false otherwise.</returns>
        public bool IsValidRebarShapeParameter => RebarShapeParameters.IsValidExternalDefinition(externalDefinition);

        /// <summary>
        ///    Retrieve the ElementId corresponding to an external rebar shape parameter
        ///    in the document, if it exists; otherwise, return InvalidElementId.
        /// </summary>
        /// <remarks>
        ///    Before a parameter can be used in a RebarShapeDefinition, it must
        ///    exist in the definition's document. There are two ways to achieve this.
        ///    It can be bound to one or more categories in the document using
        ///    the Document.ParameterBindings property, or it can be created by
        ///    calling RebarShapeParameters.GetOrCreateElementIdForExternalDefinition().
        /// </remarks>
        /// <returns>
        ///    An ElementId representing the shared parameter stored in the document,
        ///    or InvalidElementId if the parameter is not stored in the document.
        /// </returns>
        /// <param name="document">A document.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was <see langword="null" />.
        /// </exception>
        [Pure]
        public ElementId GetRebarShapeParameterElementId(Document document)
        {
            return RebarShapeParameters.GetElementIdForExternalDefinition(document, externalDefinition);
        }

        /// <summary>
        ///    Retrieve the ElementId corresponding to an external rebar shape parameter
        ///    in the document, if it exists; otherwise, add the parameter to the document
        ///    and generate a new ElementId.
        /// </summary>
        /// <remarks>
        ///    Before a parameter can be used in a RebarShapeDefinition, it must
        ///    exist in the definition's document. There are two ways to achieve this.
        ///    It can be bound to one or more categories in the document using
        ///    the Document.ParameterBindings property, or it can be created with this
        ///    method.
        /// </remarks>
        /// <returns>An ElementId representing the shared parameter stored in the document.</returns>
        /// <param name="document">A document.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was <see langword="null" />.
        /// </exception>
        public ElementId GetOrCreateRebarShapeParameterElementId(Document document)
        {
            return RebarShapeParameters.GetOrCreateElementIdForExternalDefinition(document, externalDefinition);
        }
    }

    /// <param name="definitionFile">The source definition file.</param>
    extension(DefinitionFile definitionFile)
    {
        /// <summary>
        /// Search a DefinitionFile for the ExternalDefinition corresponding to a parameter in a document.
        /// </summary>
        /// <returns>
        ///    The external parameter corresponding to the parameter's ElementId,
        ///    or null if the Id does not correspond to an external parameter,
        ///    or the parameter is not in the definition file.
        /// </returns>
        /// <param name="document">A document.</param>
        /// <param name="parameterId">The id of a shared parameter in the document.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was <see langword="null" />.
        /// </exception>
        [Pure]
        public ExternalDefinition? SearchExternalDefinition(Document document, ElementId parameterId)
        {
            return RebarShapeParameters.GetExternalDefinitionForElementId(document, parameterId, definitionFile);
        }

        /// <summary>
        /// Search a DefinitionFile for the ExternalDefinition corresponding to a parameter in a document.
        /// </summary>
        /// <returns>
        ///    The external parameter corresponding to the parameter's ElementId,
        ///    or null if the Id does not correspond to an external parameter,
        ///    or the parameter is not in the definition file.
        /// </returns>
        /// <param name="parameter">The shared parameter</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was <see langword="null" />.
        /// </exception>
        [Pure]
        public ExternalDefinition? SearchExternalDefinition(Parameter parameter)
        {
            return RebarShapeParameters.GetExternalDefinitionForElementId(parameter.Element.Document, parameter.Id, definitionFile);
        }
    }
}