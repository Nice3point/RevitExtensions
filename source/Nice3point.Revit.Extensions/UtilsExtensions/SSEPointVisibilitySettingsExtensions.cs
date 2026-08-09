#if REVIT2024_OR_GREATER
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.SSEPointVisibilitySettings"/> class.
/// </summary>
[PublicAPI]
public static class SsePointVisibilitySettingsExtensions
{
    /// <param name="category">The source category.</param>
    extension(Category category)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.SSEPointVisibilitySettings.GetVisibility(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId)"/>
        [Pure]
        public bool GetSsePointVisibility(Document document)
        {
            return SSEPointVisibilitySettings.GetVisibility(document, category.Id);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.SSEPointVisibilitySettings.SetVisibility(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,System.Boolean)"/>
        public void SetSsePointVisibility(Document document, bool isVisible)
        {
            SSEPointVisibilitySettings.SetVisibility(document, category.Id, isVisible);
        }
    }
}
#endif