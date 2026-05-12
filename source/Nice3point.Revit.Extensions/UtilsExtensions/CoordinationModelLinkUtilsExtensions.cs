#if REVIT2026_OR_GREATER
using Autodesk.Revit.DB.ExternalData;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.ExternalData;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExternalData.CoordinationModelLinkUtils"/> class.
/// </summary>
[PublicAPI]
public static class CoordinationModelLinkUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <summary>Gets all Coordination Model instance ids in the document.</summary>
        /// <returns>
        ///    Returns the set of element ids of all Coordination Model instances in the document.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public ISet<ElementId> GetAllCoordinationModelInstanceIds()
        {
            return CoordinationModelLinkUtils.GetAllCoordinationModelInstanceIds(document);
        }

        /// <summary>Gets all Coordination Model type ids in the document.</summary>
        /// <returns>
        ///    Returns the set of element ids of all Coordination Model types in the document.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public ISet<ElementId> GetAllCoordinationModelTypeIds()
        {
            return CoordinationModelLinkUtils.GetAllCoordinationModelTypeIds(document);
        }

        /// <summary>
        ///    Creates a Coordination Model instance using the absolute path of a .nwc or .nwd file and the linking options.
        /// </summary>
        /// <remarks>
        ///   <p> In case the file path is not an absolute path, it is considered to be relative to the Revit document's saved path.</p>
        /// </remarks>
        /// <param name="filePath">The file's absolute or relative path.</param>
        /// <param name="linkOptions">Options for linking.</param>
        /// <returns>
        ///    Returns the newly added Coordination Model instance of the local .nwc or .nwd file.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Failed to validate file type.
        ///    -or-
        ///    document is not a project document.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.FileArgumentNotFoundException">
        ///    The given filePath does not exist.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    Failed to create Coordination Model instance from specified path.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public Element LinkCoordinationModelFromLocalPath(string filePath, CoordinationModelLinkOptions linkOptions)
        {
            return CoordinationModelLinkUtils.LinkCoordinationModelFromLocalPath(document, filePath, linkOptions);
        }

        /// <summary>
        ///    Creates a Coordination Model instance based on the information provided by the specified Autodesk Docs data and linking options.
        /// </summary>
        /// <remarks>
        ///      <p> Authentication in Revit is a prerequisite for linking Coordination Models from Autodesk Docs.</p>
        ///      <p> The Autodesk Docs data projectId, fileId and viewName can be retrieved from the web URL of the 3D view.
        /// The Autodesk Docs account id can be retrieved from the Autodesk Docs Account settings.
        /// This data can also be retrieved via the Data Management API or Autodesk Construction Cloud API.</p>
        ///    </remarks>
        /// <param name="accountId">The id of the Autodesk Docs account.</param>
        /// <param name="projectId">The id of the Autodesk Docs project.</param>
        /// <param name="fileId">
        ///    The id of the Autodesk Docs file.
        ///    A valid file id should start with "urn:WIPENVIRONMENT:dm.lineage:", followed by an unique identifier.
        ///    The WIPENVIRONEMNT varies from Region to Region.
        ///    For example, for an account created in US Region, WIPENVIRONMENT = adsk.wipprod and a valid file id would be urn:adsk.wipprod:dm.lineage:AoV26TGqRjuNs4ANq84ncQ.
        /// </param>
        /// <param name="viewName">View name.</param>
        /// <param name="linkOptions">Options for linking.</param>
        /// <returns>
        ///    Returns the newly added Coordination Model instance of the 3D view from Autodesk Docs.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Failed to validate file id.
        ///    -or-
        ///    document is not a project document.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to validate web services environment.
        ///    -or-
        ///    Failed to validate authentication.
        ///    -or-
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    Failed to create Coordination Model instance using specified data or an internal error occured.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public Element Link3DViewFromAutodeskDocs(string accountId, string projectId, string fileId, string viewName, CoordinationModelLinkOptions linkOptions)
        {
            return CoordinationModelLinkUtils.Link3DViewFromAutodeskDocs(document, accountId, projectId, fileId, viewName, linkOptions);
        }
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>Checks whether an element is a Coordination Model instance.</summary>
        /// <returns>True if the element is Coordination Model instance; false otherwise.</returns>
        public bool IsCoordinationModelInstance => CoordinationModelLinkUtils.IsCoordinationModelInstance(element.Document, element);

        /// <summary>
        ///    Gets all the properties for the provided Coordination Model instance reference.
        /// </summary>
        /// <remarks>
        ///   <p> Properties are available only for elements inside a Coordination Model from Autodesk Docs.</p>
        /// </remarks>
        /// <param name="reference">
        ///    The reference to the element inside the provided Coordination Model instance.
        /// </param>
        /// <returns>
        ///    Returns a list of properties for the provided Coordination Model instance reference.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided Reference is not a valid element inside the provided Coordination Model instance.
        ///    -or-
        ///    The provided element is not a Autodesk Docs Coordination Model instance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to validate web services environment.
        ///    -or-
        ///    Failed to validate authentication.
        ///    -or-
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public IList<CoordinationModelElementProperty> GetAllPropertiesForReferenceInsideCoordinationModel(Reference reference)
        {
            return CoordinationModelLinkUtils.GetAllPropertiesForReferenceInsideCoordinationModel(element.Document, element, reference);
        }

        /// <summary>
        ///    Returns the category name for the provided element reference inside the provided Coordination Model instance.
        /// </summary>
        /// <param name="reference">
        ///    The reference to the element inside the provided Coordination Model instance.
        /// </param>
        /// <returns>
        ///    Returns the category name for the provided reference inside the Coordination Model instance.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Autodesk Docs Coordination Model instance.
        ///    -or-
        ///    The provided Reference is not a valid element inside the provided Coordination Model instance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public string GetCategoryForReferenceInsideCoordinationModel(Reference reference)
        {
            return CoordinationModelLinkUtils.GetCategoryForReferenceInsideCoordinationModel(element.Document, element, reference);
        }

        /// <summary>
        ///    Gets the visibility override for the provided Coordination Model instance or type.
        /// </summary>
        /// <remarks>
        ///      <p> By default, the visibility setting has no override and its value is true.</p>
        ///      <p> This setting does not reflect the actual visibility in canvas.
        /// For example, if a Coordination Model instance has this visibility setting on true, it won't be visible if its Coordination Model type is hidden.</p>
        ///    </remarks>
        /// <param name="view">The view.</param>
        /// <returns>
        ///    Returns the visibility override for the provided Coordination Model instance or type.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Coordination Model instance or type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public bool GetCoordinationModelVisibilityOverride(View view)
        {
            return CoordinationModelLinkUtils.GetVisibilityOverride(element.Document, view, element);
        }

        /// <summary>
        ///    Sets the visibility override for the provided Coordination Model instance or type.
        ///    A value of true means that the graphics are visible.
        /// </summary>
        /// <remarks>
        ///      <p> By default, the visibility setting has no override and its value is true.</p>
        ///      <p> This setting does not reflect the actual visibility in canvas.
        /// For example, if a Coordination Model instance has the visibility setting on true, it won't be visible if its Coordination Model type is hidden.</p>
        ///    </remarks>
        /// <param name="view">The view.</param>
        /// <param name="visible">
        ///    Sets the visibility override for the provided Coordination Model instance or type.
        ///    A value of true means that the graphics are visible.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Coordination Model instance or type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void SetCoordinationModelVisibilityOverride(View view, bool visible)
        {
            CoordinationModelLinkUtils.SetVisibilityOverride(element.Document, view, element, visible);
        }

        /// <summary>
        ///    Gets the visibility for the provided reference inside the Coordination Model from Autodesk Docs.
        /// </summary>
        /// <remarks>
        ///   <p> The reference should be a subelement of a Coordination Model instance.</p>
        ///   <p> By default, the visibility setting has no override and its value is true.</p>
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <param name="reference">
        ///    The reference to the element inside the provided Coordination Model instance.
        /// </param>
        /// <returns>
        ///    Returns the visibility for the provided reference inside the Coordination Model instance.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided Reference is not a valid element inside the provided Coordination Model instance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public bool GetVisibilityOverrideForReferenceInsideCoordinationModel(View view, Reference reference)
        {
            return CoordinationModelLinkUtils.GetVisibilityOverrideForReferenceInsideCoordinationModel(element.Document, view, element, reference);
        }

        /// <summary>
        ///    Sets the visibility override for the provided reference inside the Coordination Model instance.
        ///    A value of true means that the graphics are visible.
        /// </summary>
        /// <remarks>
        ///   <p> The reference should be a subelement of a Coordination Model from Autodesk Docs.</p>
        ///   <p> The referenced element will have its visibility updated in all instances of the Coordination Model type of the provided Coordination Model instance.</p>
        ///   <p> By default, the visibility setting has no override and its value is true.</p>
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <param name="reference">
        ///    The reference to the element inside the provided Coordination Model instance.
        /// </param>
        /// <param name="visible">
        ///    Sets the visibility override for the provided reference inside the Coordination Model instance.
        ///    A value of true means that the graphics are visible.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided Reference is not a valid element inside the provided Coordination Model instance.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void SetVisibilityOverrideForReferenceInsideCoordinationModel(View view, Reference reference, bool visible)
        {
            CoordinationModelLinkUtils.SetVisibilityOverrideForReferenceInsideCoordinationModel(element.Document, view, element, reference, visible);
        }
    }

    /// <param name="elementType">The source coordination model type.</param>
    extension(ElementType elementType)
    {
        /// <summary>Checks whether an element is a Coordination Model type.</summary>
        /// <returns>True if the element is Coordination Model type; false otherwise.</returns>
        public bool IsCoordinationModelType => CoordinationModelLinkUtils.IsCoordinationModelType(elementType.Document, elementType);

        /// <summary>Gets link data for the provided Coordination Model type.</summary>
        /// <returns>
        ///    Returns Coordination Model type data defining the link to the Autodesk Docs 3D view or the .nwc or .nwd file.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public CoordinationModelLinkData GetCoordinationModelTypeData()
        {
            return CoordinationModelLinkUtils.GetCoordinationModelTypeData(elementType.Document, elementType);
        }

        /// <summary>Gets the color override value for the provided Coordination Model type.</summary>
        /// <remarks>
        ///   <p> By default, the color setting has no override and its value is InvalidColorValue.</p>
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <returns>
        ///    Returns the color override value of the Coordination Model type. InvalidColorValue means no override is set.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public Color GetCoordinationModelColorOverride(View view)
        {
            return CoordinationModelLinkUtils.GetColorOverride(elementType.Document, view, elementType);
        }

        /// <summary>Sets the color override value for the provided Coordination Model type.</summary>
        /// <remarks>
        ///   <p> By default, the color setting has no override and its value is InvalidColorValue.</p>
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <param name="color">
        ///    Value of the color for the override. InvalidColorValue means no override is set.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void SetCoordinationModelColorOverride(View view, Color color)
        {
            CoordinationModelLinkUtils.SetColorOverride(elementType.Document, view, elementType, color);
        }

        /// <summary>
        ///    Gets the transparency override value for the provided Coordination Model type.
        /// </summary>
        /// <remarks>
        ///   <p> Transparency must be greater than 0 and less than 100 (0 = opaque, 100 = fully transparent).</p>
        ///   <p> By default, the transparency setting has no override and its value is 0.</p>
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <returns>
        ///    Returns the transparency value (in percentage). A 0-value transparency means no override is set.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public int GetCoordinationModelTransparencyOverride(View view)
        {
            return CoordinationModelLinkUtils.GetTransparencyOverride(elementType.Document, view, elementType);
        }

        /// <summary>
        ///    Sets the transparency override value for the provided Coordination Model type.
        /// </summary>
        /// <remarks>
        ///   <p> Transparency must be greater than 0 and less than 100 (0 = opaque, 100 = fully transparent).</p>
        ///   <p> By default, the transparency setting has no override and its value is 0.</p>
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <param name="transparency">
        ///    The transparency value to apply (in percentage). A 0-value transparency means no override is set.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    The value is invalid. The valid range is 0 through 100
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void SetCoordinationModelTransparencyOverride(View view, int transparency)
        {
            CoordinationModelLinkUtils.SetTransparencyOverride(elementType.Document, view, elementType, transparency);
        }

        /// <summary>
        ///    Checks whether a provided string is a element category name in the provided AutodeskDocs Coordination Model type.
        /// </summary>
        /// <param name="categoryName">The element category name to check.</param>
        /// <returns>
        ///    True if the categoryName is a element category name in the provided AutodeskDocs Coordination Model type; false otherwise.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    categoryName is an empty string.
        ///    -or-
        ///    The provided element is not a Autodesk Docs Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool ContainsCoordinationModelCategory(string categoryName)
        {
            return CoordinationModelLinkUtils.ContainsCategory(elementType.Document, elementType, categoryName);
        }

        /// <summary>
        ///    Returns the color override value for the provided element category name inside the provided Coordination Model type.
        /// </summary>
        /// <remarks>
        ///   <p> By default, the color setting has no override and its value is InvalidColorValue.</p>
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <param name="categoryName">
        ///    The name of the element category inside the provided Coordination Model type.
        /// </param>
        /// <returns>
        ///    Returns the color override value for the provided element category name inside the provided Coordination Model type. InvalidColorValue means no override is set.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The name categoryName is not a category name in the provided AutodeskDocs Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public Color GetCoordinationModelColorOverrideForCategory(View view, string categoryName)
        {
            return CoordinationModelLinkUtils.GetColorOverrideForCategory(elementType.Document, view, elementType, categoryName);
        }

        /// <summary>
        ///    Set the color override value for the provided element category name inside the provided Coordination Model type.
        /// </summary>
        /// <remarks>
        ///   <p> By default, the color setting has no override and its value is InvalidColorValue.</p>
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <param name="categoryName">
        ///    The name of the element category inside the provided Coordination Model type.
        /// </param>
        /// <param name="color">
        ///    Color override value for the provided element category name inside the provided Coordination Model type. InvalidColorValue means no override is set.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The name categoryName is not a category name in the provided AutodeskDocs Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void SetCoordinationModelColorOverrideForCategory(View view, string categoryName, Color color)
        {
            CoordinationModelLinkUtils.SetColorOverrideForCategory(elementType.Document, view, elementType, categoryName, color);
        }

        /// <summary>
        ///    Gets the visibility override for the provided element category name in the provided Coordination Model type.
        /// </summary>
        /// <remarks>
        ///   <p> By default, the visibility setting has no override and its value is true.</p>
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <param name="categoryName">
        ///    The name of the element category inside the provided Coordination Model type.
        /// </param>
        /// <returns>
        ///    Returns the visibility override for the provided element category name in the provided Coordination Model type.
        ///    A value of true means that the graphics are visible.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The name categoryName is not a category name in the provided AutodeskDocs Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        [Pure]
        public bool GetCoordinationModelVisibilityOverrideForCategory(View view, string categoryName)
        {
            return CoordinationModelLinkUtils.GetVisibilityOverrideForCategory(elementType.Document, view, elementType, categoryName);
        }

        /// <summary>
        ///    Sets the visibility override for the provided element category name inside the provided Coordination Model type.
        /// </summary>
        /// <remarks>
        ///   <p> By default, the visibility setting has no override and its value is true.</p>
        /// </remarks>
        /// <param name="view">The view.</param>
        /// <param name="categoryName">
        ///    The name of the category inside the provided Coordination Model type.
        /// </param>
        /// <param name="visible">
        ///    Sets the visibility override for the provided element category name inside the provided Coordination Model type.
        /// </param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The name categoryName is not a category name in the provided AutodeskDocs Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void SetCoordinationModelVisibilityOverrideForCategory(View view, string categoryName, bool visible)
        {
            CoordinationModelLinkUtils.SetVisibilityOverrideForCategory(elementType.Document, view, elementType, categoryName, visible);
        }

        /// <summary>Reloads the provided Coordination Model type element.</summary>
        /// <remarks>
        ///      <p> For a Coordination Model from Autodesk Docs, reload will get the latest version for the provided Coordination Model type element.
        /// Authentication in Revit is a prerequisite for reloading Coordination Models from Autodesk Docs.</p>
        ///    </remarks>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to validate web services environment.
        ///    -or-
        ///    Failed to validate user authentication.
        ///    -or-
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    Failed to reload Coordination Model.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void ReloadCoordinationModel()
        {
            CoordinationModelLinkUtils.Reload(elementType.Document, elementType);
        }

        /// <summary>
        ///    Reloads a Autodesk Docs Coordination Model type from the specified Autodesk Docs data.
        /// </summary>
        /// <remarks>
        ///      <p> Authentication in Revit is a prerequisite for reloading Coordination Models from Autodesk Docs.</p>
        ///      <p> The Autodesk Docs data projectId, fileId and viewName can be retrieved from the web URL of the 3D view.
        /// The Autodesk Docs account id can be retrieved from the Autodesk Docs Account settings.
        /// This data can also be retrieved via the Data Management API or Autodesk Construction Cloud API.</p>
        ///    </remarks>
        /// <param name="accountId">The id of the Autodesk Docs account.</param>
        /// <param name="projectId">The id of the Autodesk Docs project.</param>
        /// <param name="fileId">
        ///    The id of the Autodesk Docs file.
        ///    A valid file id should start with "urn:WIPENVIRONMENT:dm.lineage:", followed by an unique identifier.
        ///    The WIPENVIRONEMNT varies from Region to Region.
        ///    For example, for an account created in US Region, WIPENVIRONMENT = adsk.wipprod and a valid file id would be urn:adsk.wipprod:dm.lineage:AoV26TGqRjuNs4ANq84ncQ.
        /// </param>
        /// <param name="viewName">View name.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Failed to validate file id.
        ///    -or-
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Autodesk Docs Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    Failed to validate web services environment.
        ///    -or-
        ///    Failed to validate authentication.
        ///    -or-
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    Failed to reload Coordination Model type from specified Autodesk Docs data.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void ReloadAutodeskDocsCoordinationModelFrom(string accountId, string projectId, string fileId, string viewName)
        {
            CoordinationModelLinkUtils.ReloadAutodeskDocsCoordinationModelFrom(elementType.Document, elementType, accountId, projectId, fileId, viewName);
        }

        /// <summary>
        ///    Reloads a local Coordination Model type from the specified absolute path of a .nwc or .nwd file.
        /// </summary>
        /// <remarks>
        ///   <p> In case the file path is not an absolute path, it is considered to be relative to the Revit document's saved path.</p>
        /// </remarks>
        /// <param name="filePath">The file's absolute or relative path to reload from.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    Failed to validate file type.
        ///    -or-
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a local Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.FileArgumentNotFoundException">
        ///    The given filePath does not exist.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    Failed to reload Coordination Model type from specified path.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void ReloadLocalCoordinationModelFrom(string filePath)
        {
            CoordinationModelLinkUtils.ReloadLocalCoordinationModelFrom(elementType.Document, elementType, filePath);
        }

        /// <summary>Unloads the provided Coordination Model type element.</summary>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    document is not a project document.
        ///    -or-
        ///    The provided element is not a Coordination Model type.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    Failed to unload Coordination Model.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationForbiddenException">
        ///    The document is in failure mode: an operation has failed,
        ///    and Revit requires the user to either cancel the operation
        ///    or fix the problem (usually by deleting certain elements).
        ///    -or-
        ///    The document is being loaded, or is in the midst of another
        ///    sensitive process.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ModificationOutsideTransactionException">
        ///    The document has no open transaction.
        /// </exception>
        public void UnloadCoordinationModel()
        {
            CoordinationModelLinkUtils.Unload(elementType.Document, elementType);
        }
    }
}
#endif