// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.DocumentValidation"/> class.
/// </summary>
[PublicAPI]
public static class DocumentValidationExtensions
{
    /// <summary>Indicates if an element can be deleted.</summary>
    /// <param name="element">The element to check.</param>
    /// <returns>True if the element can be deleted, false otherwise.</returns>
    [Pure]
    public static bool CanDeleteElement(this Element element)
    {
        return DocumentValidation.CanDeleteElement(element.Document, element.Id);
    }
}