// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.FormUtils" /> class.
/// </summary>
[PublicAPI]
public static class FormUtilsExtensions
{
    extension(Form)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.FormUtils.CanBeDissolved(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})" />
        [Pure]
        public static bool CanBeDissolved(Document document, ICollection<ElementId> elements)
        {
            return FormUtils.CanBeDissolved(document, elements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FormUtils.DissolveForms(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})" />
        public static ICollection<ElementId> DissolveForms(Document document, ICollection<ElementId> elements)
        {
            return FormUtils.DissolveForms(document, elements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.FormUtils.DissolveForms(Autodesk.Revit.DB.Document,System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId},out System.Collections.Generic.ICollection{Autodesk.Revit.DB.ElementId})" />
        public static ICollection<ElementId> DissolveForms(Document document, ICollection<ElementId> elements, out ICollection<ElementId> profileOriginPointSet)
        {
            return FormUtils.DissolveForms(document, elements, out profileOriginPointSet);
        }
    }
}
