#if REVIT2022_OR_GREATER
// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ParameterUtils"/> class.
/// </summary>
[PublicAPI]
public static class ParameterUtilsExtensions
{
    /// <param name="parameterTypeId">The parameter identifier.</param>
    extension(ForgeTypeId parameterTypeId)
    {
        /// <summary>Checks whether a ForgeTypeId identifies a built-in parameter.</summary>
        /// <remarks>
        ///    A ForgeTypeId identifies a built-in parameter if it corresponds to a valid BuiltInParameter value.
        /// </remarks>
        /// <returns>True if the ForgeTypeId identifies a built-in parameter, false otherwise.</returns>
        [Pure]
        public bool IsBuiltInParameter()
        {
            return ParameterUtils.IsBuiltInParameter(parameterTypeId);
        }

        /// <summary> Gets the BuiltInParameter value corresponding to built-in parameter identified by the given ForgeTypeId.</summary>
        /// <returns>The BuiltInParameter value corresponding to the given parameter identifier.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    parameterTypeId is not a built-in parameter identifier. See IsBuiltInParameter(ForgeTypeId) and GetParameterTypeId(BuiltInParameter).
        /// </exception>
        [Pure]
        public BuiltInParameter GetBuiltInParameter()
        {
            return ParameterUtils.GetBuiltInParameter(parameterTypeId);
        }

        /// <summary>Checks whether a ForgeTypeId identifies a built-in parameter group.</summary>
        /// <remarks>
        ///    A ForgeTypeId identifies a built-in parameter group if it corresponds to a valid BuiltInParameterGroup value.
        /// </remarks>
        /// <returns>
        ///    True if the ForgeTypeId identifies a built-in parameter group, false otherwise.
        /// </returns>
        [Pure]
        public bool IsBuiltInGroup()
        {
            return ParameterUtils.IsBuiltInGroup(parameterTypeId);
        }

        /// <summary>
        ///    Retrieves settings associated with the given parameter from the Parameters Service.
        /// </summary>
        /// <remarks>
        ///    The settings associated with a parameter definition are accessible only to an authorized user.
        ///    To retrieve them, the user must be signed in.
        /// </remarks>
        /// <returns>Settings associated with a parameter.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.AccessDeniedException">
        ///    Thrown when the user is not authorized to access the requested information.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Thrown when the given parameter identifier is empty.
        /// </exception>
        /// <exception cref="!:Autodesk::Revit::Exceptions::NetworkCommunicationError">
        ///    Thrown when communication with the Parameters Service is unsuccessful.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ResourceNotFoundException">
        ///    Thrown when the requested parameter is not found.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ServerInternalException">
        ///    Thrown when the Parameters Service reports an internal error.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.UnauthenticatedException">
        ///    Thrown when the user is not signed in.
        /// </exception>
        public ParameterDownloadOptions DownloadParameterOptions()
        {
            return ParameterUtils.DownloadParameterOptions(parameterTypeId);
        }

#if REVIT2024_OR_GREATER
        /// <summary>
        ///    Downloads the name of the given parameter's owning account and records it in the given document. If the owning
        ///    account's name is already recorded in the given document, this method returns the name without downloading it
        ///    again.
        /// </summary>
        /// <remarks>In Revit, the account name appears in the parameter tooltip if available.</remarks>
        /// <param name="document">
        ///    Document in which to record the name of the parameter's owning account.
        /// </param>
        /// <returns>Name of the owning account.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.AccessDeniedException">
        ///    Thrown when the user is not authorized to access the requested information.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Thrown when the parameter identifier does not include an account identifier.
        /// </exception>
        /// <exception cref="!:Autodesk::Revit::Exceptions::NetworkCommunicationError">
        ///    Thrown when communication with the remote service is unsuccessful.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ResourceNotFoundException">
        ///    Thrown when the requested information is not found.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ServerInternalException">
        ///    Thrown when the remote service reports an internal error.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.UnauthenticatedException">
        ///    Thrown when the user is not signed in.
        /// </exception>
        public string DownloadCompanyName(Document document)
        {
            return ParameterUtils.DownloadCompanyName(document, parameterTypeId);
        }

        /// <summary>
        ///    Create a shared parameter element in the given document according to a parameter definition downloaded from the Parameters Service.
        /// </summary>
        /// <remarks>
        ///      <p>The identifier of a user-defined parameter definition on the Parameters Service has the form
        /// "parameters.&lt;accountId&gt;:&lt;schemaId&gt;-&lt;versionNumber&gt;", where &lt;versionNumber&gt; is a
        /// semantic version number such as "1.0.0" and &lt;accountId&gt; and &lt;schemaId&gt; are GUIDs consisting of
        /// 32 hexadecimal digits. Revit will extract the &lt;schemaId&gt; GUID to identify the shared parameter
        /// element.</p>
        ///      <p>If a shared parameter with a matching GUID is not yet present in the document, this method will attempt
        /// to obtain the parameter and apply the given bindings. If the parameter definition is already available
        /// locally, Revit will use the local definition. Otherwise, Revit will attempt to download the requested
        /// parameter definition from the Parameters Service.</p>
        ///      <p>The given document may be either a project or a family document. The rules for adding parameters to
        /// project and family documents differ.</p>
        ///      <p>For family documents, requesting a parameter with a GUID matching that of a shared parameter already
        /// present in the family document is an error.</p>
        ///      <p>Family parameters must have unique names. There is an error if the target document is a family and the
        /// downloaded parameter is found to have a name that matches that of a parameter already present in the family
        /// document.</p>
        ///      <p>Family parameters must be initialized to a default value. There is an error if the target document is a
        /// family and the downloaded parameter is a Family Type parameter and no family of the requisite category
        /// exists in the family document.</p>
        ///      <p>When the target document is a project, if a parameter exactly matching the given ForgeTypeId is already
        /// present in the document, this method will not download anything. Otherwise, if a local shared parameter with
        /// a GUID colliding with the given ForgeTypeId is already present in the project document, this method will
        /// download the requested parameter from the Parameters Service, validate that the requested parameter is
        /// compatible with the existing local definition, and overwrite the existing local definition according to the
        /// downloaded definition. Attempting to download an incompatible definition that collides with an existing
        /// local shared parameter is an error. If the parameter or a compatible local parameter is already present in
        /// the target project document, this method will update the existing parameter's bindings according to the
        /// given bindings. When updating bindings, new category bindings may be added to the existing parameter but
        /// existing category bindings will not be removed.</p>
        ///    </remarks>
        /// <param name="document">
        ///    Document in which to create a shared parameter from a downloaded definition.
        /// </param>
        /// <param name="options">Parameter download options.</param>
        /// <returns>The shared parameter instance.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Thrown when the parameter identifier does not include a GUID, when required bindings are not assigned, when
        ///    the requested group identifier does not identify a group that accommodates user-defined parameters, when a
        ///    parameter with a matching GUID is already present in the given family document, when the given project
        ///    document already contains an incompatible parameter definition with the same GUID, or when a parameter with
        ///    a matching name is already present in the given family document.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.DefaultValueException">
        ///    Thrown when the target document is a family and the downloaded parameter is a Family Type parameter and no
        ///    family of the requisite category exists in the family document.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.NetworkCommunicationException">
        ///    Thrown when communication with the Parameters Service is unsuccessful.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ResourceNotFoundException">
        ///    Thrown when the requested parameter definition is not found on the Parameters Service.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.SchemaException">
        ///    Thrown when there is an error interpreting a downloaded parameter definition.
        /// </exception>
        [Pure]
        public SharedParameterElement DownloadParameter(Document document, ParameterDownloadOptions options)
        {
            return ParameterUtils.DownloadParameter(document, options, parameterTypeId);
        }
#endif
    }

    /// <param name="builtInParameter">The BuiltInParameter value.</param>
    extension(BuiltInParameter builtInParameter)
    {
        /// <summary> Gets the ForgeTypeId identifying the built-in parameter corresponding to the given BuiltInParameter value.</summary>
        /// <returns>Identifier of the parameter corresponding to the given BuiltInParameter value.</returns>
        [Pure]
        public ForgeTypeId GetParameterTypeId()
        {
            return ParameterUtils.GetParameterTypeId(builtInParameter);
        }
    }

#if REVIT2023_OR_GREATER
    /// <param name="parameter">The parameter.</param>
    extension(Parameter parameter)
    {
        /// <summary>Checks whether a Parameter identifies a built-in parameter.</summary>
        /// <remarks>
        ///    A Parameter identifies a built-in parameter if it corresponds to a valid BuiltInParameter value.
        /// </remarks>
        /// <returns>True if the Parameter identifies a built-in parameter, false otherwise.</returns>
        [Pure]
        public bool IsBuiltInParameter()
        {
            return ParameterUtils.IsBuiltInParameter(parameter.Id);
        }
    }
#endif
}
#endif