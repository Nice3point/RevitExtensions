using System.Reflection;
#if NET
using System.Runtime.CompilerServices;

#else
using System.Runtime.InteropServices;
#endif

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Revit Category Extensions
/// </summary>
[PublicAPI]
public static class CategoryExtensions
{
    private static readonly Assembly CategoryAssembly = Assembly.GetAssembly(typeof(Category))!;
    private static readonly Type ADocumentType = CategoryAssembly.GetType("ADocument")!;
    private static readonly Type ElementIdType = CategoryAssembly.GetType("ElementId")!;
    private static readonly MethodInfo GetADocumentMethod = typeof(Document).GetMethod("getADocument", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)!;
    private static readonly ConstructorInfo CategoryConstructor = typeof(Category).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, null, [ADocumentType.MakePointerType(), ElementIdType.MakePointerType()], null)!;

    /// <param name="builtInCategory">The source category</param>
    extension(BuiltInCategory builtInCategory)
    {
        /// <summary>
        /// Converts a BuiltInCategory into a Revit Category object.
        /// </summary>
        /// <param name="document">The Revit Document associated with the category conversion.</param>
        /// <returns>A Category object corresponding to the specified BuiltInCategory.</returns>
        /// <remarks>This method performs low-level operation to instantiate a Category object.</remarks>
        public
#if NET
            unsafe
#endif
            Category ToCategory(Document document)
        {
#if REVIT2024_OR_GREATER
            var elementId = (long)builtInCategory;
#else
            var elementId = (int)builtInCategory;
#endif
#if NET
            var aDocument = GetADocumentMethod.Invoke(document, null);
            var category = (Category)CategoryConstructor.Invoke([aDocument, (nint)Unsafe.AsPointer(ref elementId)]);

            return category;
#else
            var aDocument = GetADocumentMethod.Invoke(document, null);

            var handle = GCHandle.Alloc(elementId, GCHandleType.Pinned);
            var category = (Category)CategoryConstructor.Invoke([aDocument, handle.AddrOfPinnedObject()]);
            handle.Free();

            return category;
#endif
        }

        /// <summary>
        ///     Create an ElementId handle with the given BuiltInCategory id.
        /// </summary>
        [Pure]
        public ElementId ToElementId()
        {
            return new ElementId(builtInCategory);
        }

        /// <summary>
        ///     Retrieves the numeric value of the BuiltInCategory.
        /// </summary>
        /// <returns>The value the category constant holds.</returns>
        [Pure]
        public long ToLong()
        {
            return (long)builtInCategory;
        }
    }

    /// <param name="elementId">The unique identification for an element.</param>
    extension(ElementId elementId)
    {
        /// <summary>
        ///     Checks if ElementID is a category identifier
        /// </summary>
        [Pure]
        [Obsolete("Use IsCategory() instead")]
        [CodeTemplate(
            searchTemplate: "$expr$.AreEquals($category$)",
            Message = "AreEquals is obsolete, use IsCategory instead",
            ReplaceTemplate = "$expr$.IsCategory($category$)",
            ReplaceMessage = "Replace with IsCategory")]
        public bool AreEquals(BuiltInCategory category)
        {
            return elementId.IsCategory(category);
        }

        /// <summary>
        ///     Checks if ElementID is a category identifier
        /// </summary>
        [Pure]
        public bool IsCategory(BuiltInCategory category)
        {
#if REVIT2024_OR_GREATER
            return elementId.Value == (long)category;
#else
            return elementId.IntegerValue == (int)category;
#endif
        }
    }
}