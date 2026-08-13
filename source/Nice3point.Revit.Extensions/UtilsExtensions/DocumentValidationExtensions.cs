// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.DocumentValidation" /> class.
/// </summary>
[PublicAPI]
public static class DocumentValidationExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.DocumentValidation.CanDeleteElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public bool CanBeDeleted => DocumentValidation.CanDeleteElement(element.Document, element.Id);

        /// <summary></summary>
        [Pure]
        [Obsolete("Use CanBeDeleted() instead")]
        [CodeTemplate(
            "$expr$.CanDeleteElement()",
            Message = "CanDeleteElement is obsolete, use CanBeDeleted instead",
            ReplaceTemplate = "$expr$.CanBeDeleted",
            ReplaceMessage = "Replace with CanBeDeleted")]
        public bool CanDeleteElement()
        {
            return DocumentValidation.CanDeleteElement(element.Document, element.Id);
        }
    }

    /// <param name="elementId">The element id to check.</param>
    extension(ElementId elementId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.DocumentValidation.CanDeleteElement(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public bool CanBeDeleted(Document document)
        {
            return DocumentValidation.CanDeleteElement(document, elementId);
        }
    }
}
