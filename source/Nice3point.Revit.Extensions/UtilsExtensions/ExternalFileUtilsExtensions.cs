

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExternalFileUtils"/> class.
/// </summary>
[PublicAPI]
public static class ExternalFileUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <summary>Gets the ids of all elements which are external file references.</summary>
        /// <remarks>
        ///    This function will not return the ids of nested Revit links;
        ///    it only returns top-level references.
        /// </remarks>
        /// <returns>The ids of all elements which are external file references.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ICollection<ElementId> GetAllExternalFileReferences()
        {
            return ExternalFileUtils.GetAllExternalFileReferences(document);
        }
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>Determines whether the given element represents an external file.</summary>
        /// <remarks>
        ///      <p>CAD imports are not external file references, as their
        /// data is brought fully into Revit. No connection is maintained
        /// to the original file.</p>
        ///      <p>A link may be an external resource without being an external file.</p>
        ///    </remarks>
        /// <returns>True if the given element represents an external file; false otherwise.</returns>
        public bool IsExternalFileReference => ExternalFileUtils.IsExternalFileReference(element.Document, element.Id);

        /// <summary>Gets the external file referencing data for the given element.</summary>
        /// <returns>
        ///    An object containing path and type information for the given element's external file.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    elemId does not represent an external file reference.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ExternalFileReference GetExternalFileReference()
        {
            return ExternalFileUtils.GetExternalFileReference(element.Document, element.Id);
        }
    }

    /// <param name="elementId">The element id.</param>
    extension(ElementId elementId)
    {
        /// <summary>Determines whether the given element represents an external file.</summary>
        /// <remarks>
        ///      <p>CAD imports are not external file references, as their
        /// data is brought fully into Revit. No connection is maintained
        /// to the original file.</p>
        ///      <p>A link may be an external resource without being an external file.</p>
        ///    </remarks>
        /// <param name="document">A Revit Document.</param>
        /// <returns>True if the given element represents an external file; false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element elemId does not exist in the document
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public bool IsExternalFileReference(Document document)
        {
            return ExternalFileUtils.IsExternalFileReference(document, elementId);
        }

        /// <summary>Gets the external file referencing data for the given element.</summary>
        /// <param name="document">A Revit Document.</param>
        /// <returns>
        ///    An object containing path and type information for the given element's external file.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The element elemId does not exist in the document
        ///    -or-
        ///    elemId does not represent an external file reference.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ExternalFileReference GetExternalFileReference(Document document)
        {
            return ExternalFileUtils.GetExternalFileReference(document, elementId);
        }
    }
}