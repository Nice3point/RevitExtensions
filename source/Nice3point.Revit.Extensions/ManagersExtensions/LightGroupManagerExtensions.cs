using Autodesk.Revit.DB.Lighting;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Lighting.LightGroupManager"/> class.
/// </summary>
[PublicAPI]
public static class LightGroupManagerExtensions
{
    /// <param name="document">The document.</param>
    extension(Document document)
    {
        /// <summary>Creates a light group manager object from the given document</summary>
        /// <returns>The newly created Light group manager object</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The document is not valid because it is not a project (rvt) document
        /// </exception>
        [Pure]
        public LightGroupManager GetLightGroupManager()
        {
            return LightGroupManager.GetLightGroupManager(document);
        }
    }
}