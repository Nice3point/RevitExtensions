using Autodesk.Revit.DB.DirectContext3D;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.DirectContext3D;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.DirectContext3D.DirectContext3DDocumentUtils" /> class.
/// </summary>
[PublicAPI]
public static class DirectContext3DDocumentUtilsExtensions
{
    /// <param name="category">The source category.</param>
    extension(Category category)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.DirectContext3D.DirectContext3DDocumentUtils.IsADirectContext3DHandleCategory(Autodesk.Revit.DB.ElementId)" />
        public bool IsADirectContext3DHandleCategory => DirectContext3DDocumentUtils.IsADirectContext3DHandleCategory(category.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.DirectContext3D.DirectContext3DDocumentUtils.GetDirectContext3DHandleInstances(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public ISet<ElementId> GetDirectContext3DHandleInstances(Document document)
        {
            return DirectContext3DDocumentUtils.GetDirectContext3DHandleInstances(document, category.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.DirectContext3D.DirectContext3DDocumentUtils.GetDirectContext3DHandleTypes(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        [Pure]
        public ISet<ElementId> GetDirectContext3DHandleTypes(Document document)
        {
            return DirectContext3DDocumentUtils.GetDirectContext3DHandleTypes(document, category.Id);
        }
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.DirectContext3D.DirectContext3DDocumentUtils.IsADirectContext3DHandleInstance(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public bool IsADirectContext3DHandleInstance => DirectContext3DDocumentUtils.IsADirectContext3DHandleInstance(element.Document, element.Id);

        /// <inheritdoc cref="Autodesk.Revit.DB.DirectContext3D.DirectContext3DDocumentUtils.IsADirectContext3DHandleType(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)" />
        public bool IsADirectContext3DHandleType => DirectContext3DDocumentUtils.IsADirectContext3DHandleType(element.Document, element.Id);
    }
}
