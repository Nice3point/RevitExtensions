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
        /// <inheritdoc cref="Autodesk.Revit.DB.Document.GetDocumentVersion(Autodesk.Revit.DB.Document)" />
        public DocumentVersion Version => Document.GetDocumentVersion(document);
#if REVIT2023_OR_GREATER
        /// <inheritdoc cref="Autodesk.Revit.DB.Document.IsValidVersionGUID(Autodesk.Revit.DB.Document,System.Guid)"/>
        public bool IsValidVersionGuid(Guid versionGuid)
        {
            return Document.IsValidVersionGUID(document, versionGuid);
        }
#endif
    }
}
