namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Document Extensions
/// </summary>
[PublicAPI]
public static class DocumentExtensions
{
    /// <param name="document">The source document</param>
    extension(Document document)
    {
        /// <summary>Gets the DocumentVersion that corresponds to a document.</summary>
        /// <remarks>
        ///    This property can be combined with <see cref="P:Autodesk.Revit.DB.Document.IsModified" />
        ///    to see whether a document in memory is different from a version on disk. The documents
        ///    are different if the document is modified or if the two DocumentVersions differ.
        /// </remarks>
        /// <returns>The DocumentVersion corresponding to the given document.</returns>
        public DocumentVersion Version => Document.GetDocumentVersion(document);
#if REVIT2023_OR_GREATER

        /// <summary>
        ///    Checks whether the GUID is valid for the given document. Empty GUID is allowed.
        /// </summary>
        /// <param name="versionGuid">The GUID to check.</param>
        /// <returns>True if the GUID is valid.</returns>
        public bool IsValidVersionGuid(Guid versionGuid)
        {
            return Document.IsValidVersionGUID(document, versionGuid);
        }
#endif
    }
}