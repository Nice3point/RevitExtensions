using System.Reflection;
using System.Runtime.InteropServices;

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Category Extensions
/// </summary>
[PublicAPI]
public static class CategoryExtensions
{
    /// <param name="builtInCategory">The source category</param>
    extension(BuiltInCategory builtInCategory)
    {
        /// <summary>
        /// Converts a BuiltInCategory into a Revit Category object.
        /// </summary>
        /// <param name="document">The Revit Document associated with the category conversion.</param>
        /// <returns>A Category object corresponding to the specified BuiltInCategory.</returns>
        /// <remarks>This method performs low-level operation to instantiate a Category object.</remarks>
        public Category ToCategory(Document document)
        {
            const BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly;

            var documentType = typeof(Document);
            var categoryType = typeof(Category);
            var assembly = Assembly.GetAssembly(categoryType)!;
            var aDocumentType = assembly.GetType("ADocument")!;
            var elementIdType = assembly.GetType("ElementId")!;
            var elementIdIdType = elementIdType.GetField("<alignment member>", bindingFlags)!;
            var getADocumentType = documentType.GetMethod("getADocument", bindingFlags)!;
            var categoryCtorType = categoryType.GetConstructor(bindingFlags, null, [aDocumentType.MakePointerType(), elementIdType.MakePointerType()], null)!;

            var elementId = Activator.CreateInstance(elementIdType)!;
            elementIdIdType.SetValue(elementId, builtInCategory);

            var handle = GCHandle.Alloc(elementId);
            var elementIdPointer = GCHandle.ToIntPtr(handle);
            Marshal.StructureToPtr(elementId, elementIdPointer, true);

            var category = (Category)categoryCtorType.Invoke([getADocumentType.Invoke(document, null), elementIdPointer]);
            handle.Free();

            return category;
        }
    }
}