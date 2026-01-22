// ReSharper disable once CheckNamespace

using JetBrains.Annotations;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.DocumentValidation"/> class.
/// </summary>
[PublicAPI]
public static class DocumentValidationExtensions
{
    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary></summary>
        [Pure]
        [Obsolete("Use CanBeDeleted() instead")]
        [CodeTemplate(
            searchTemplate: "$expr$.CanDeleteElement()",
            Message = "CanDeleteElement is obsolete, use CanBeDeleted instead",
            ReplaceTemplate = "$expr$.CanBeDeleted()",
            ReplaceMessage = "Replace with CanBeDeleted()")]
        public bool CanDeleteElement()
        {
            return DocumentValidation.CanDeleteElement(element.Document, element.Id);
        }

        /// <summary>Indicates if an element can be deleted.</summary>
        /// <returns>True if the element can be deleted, false otherwise.</returns>
        [Pure]
        public bool CanBeDeleted()
        {
            return DocumentValidation.CanDeleteElement(element.Document, element.Id);
        }
    }

    /// <param name="elementId">The element id to check.</param>
    extension(ElementId elementId)
    {
        /// <summary>Indicates if an element can be deleted.</summary>
        /// <param name="document">The document containing the element.</param>
        /// <returns>True if the element can be deleted, false otherwise.</returns>
        [Pure]
        public bool CanBeDeleted(Document document)
        {
            return DocumentValidation.CanDeleteElement(document, elementId);
        }
    }
}