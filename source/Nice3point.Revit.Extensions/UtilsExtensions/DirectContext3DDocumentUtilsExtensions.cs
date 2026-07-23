using Autodesk.Revit.DB.DirectContext3D;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.DirectContext3D;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.DirectContext3D.DirectContext3DDocumentUtils"/> class.
/// </summary>
[PublicAPI]
public static class DirectContext3DDocumentUtilsExtensions
{
    /// <param name="category">The source category.</param>
    extension(Category category)
    {
        /// <summary>
        ///    Checks whether the provided category ID is one of the categories used by DirectContext3D handle elements.
        /// </summary>
        /// <returns>
        ///    True, if the category is valid for DirectContext3D handle elements, false otherwise.
        /// </returns>
        public bool IsADirectContext3DHandleCategory => DirectContext3DDocumentUtils.IsADirectContext3DHandleCategory(category.Id);

        /// <summary>
        ///    Returns all DirectContext3D handle instances of the given category in the document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>The set of DirectContext3D handle instances of the given category.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    the category ID handleCategory is not valid for DirectContext3D handles.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ISet<ElementId> GetDirectContext3DHandleInstances(Document document)
        {
            return DirectContext3DDocumentUtils.GetDirectContext3DHandleInstances(document, category.Id);
        }

        /// <summary>
        ///    Returns all DirectContext3D handle types of the given category in the document.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>The set of DirectContext3D handle types of the given category.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    the category ID handleCategory is not valid for DirectContext3D handles.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public ISet<ElementId> GetDirectContext3DHandleTypes(Document document)
        {
            return DirectContext3DDocumentUtils.GetDirectContext3DHandleTypes(document, category.Id);
        }
    }

    /// <param name="element">The source element.</param>
    extension(Element element)
    {
        /// <summary>
        ///    Checks whether the provided Element corresponds to a DirectContext3D handle instance element.
        /// </summary>
        /// <remarks>
        ///   <p>DirectContext3D handle instances are DirectShapes.</p>
        /// </remarks>
        /// <returns>
        ///    True, if the element is a valid DirectContext3D handle instance, false otherwise.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    aDocument is not a project document.
        /// </exception>
        public bool IsADirectContext3DHandleInstance => DirectContext3DDocumentUtils.IsADirectContext3DHandleInstance(element.Document, element.Id);

        /// <summary>
        ///    Checks whether the provided Element corresponds to a DirectContext3D handle type element.
        /// </summary>
        /// <remarks>
        ///   <p>DirectContext3D handle types are DirectShapeTypes.</p>
        /// </remarks>
        /// <returns>
        ///    True, if the element is a valid DirectContext3D handle type, false otherwise.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    aDocument is not a project document.
        /// </exception>
        public bool IsADirectContext3DHandleType => DirectContext3DDocumentUtils.IsADirectContext3DHandleType(element.Document, element.Id);
    }
}