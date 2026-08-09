using Autodesk.Revit.DB.Lighting;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Lighting;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Lighting.LightGroupManager"/> class.
/// </summary>
[PublicAPI]
public static class LightGroupManagerExtensions
{
    /// <param name="document">The document.</param>
    extension(Document document)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Lighting.LightGroupManager.GetLightGroupManager(Autodesk.Revit.DB.Document)"/>
        [Pure]
        public LightGroupManager GetLightGroupManager()
        {
            return LightGroupManager.GetLightGroupManager(document);
        }
    }
}