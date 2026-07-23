

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExternalResourceUtils"/> class.
/// </summary>
[PublicAPI]
public static class ExternalResourceUtilsExtensions
{
    /// <param name="document">The source document.</param>
    extension(Document document)
    {
        /// <summary>Gets the ids of all elements which refer to external resources.</summary>
        /// <remarks>
        ///    This function will not return the ids of nested Revit links;
        ///    it only returns top-level references.
        /// </remarks>
        /// <returns>The ids of all elements which refer to external resources.</returns>
        [Pure]
        public ISet<ElementId> GetAllExternalResourceReferences()
        {
            return ExternalResourceUtils.GetAllExternalResourceReferences(document);
        }

        /// <summary>
        ///    Gets the ids of all elements which refer to a specific type of external resource.
        /// </summary>
        /// <param name="resourceType">The type of external resource.</param>
        /// <returns>
        ///    The ids of all elements which refer to external resources of the specified type.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ISet<ElementId> GetAllExternalResourceReferences(ExternalResourceType resourceType)
        {
            return ExternalResourceUtils.GetAllExternalResourceReferences(document, resourceType);
        }
    }
}