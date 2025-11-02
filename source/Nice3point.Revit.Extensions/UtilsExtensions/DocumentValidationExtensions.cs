// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.DocumentValidation"/> class.
/// </summary>
[PublicAPI]
public static class DocumentValidationExtensions
{
    /// <param name="element">The element to check.</param>
    extension(Element element)
    {
        /// <summary>Indicates if an element can be deleted.</summary>
        /// <returns>True if the element can be deleted, false otherwise.</returns>
        [Pure]
        public bool CanDeleteElement()
        {
            return DocumentValidation.CanDeleteElement(element.Document, element.Id);
        }
    }
}