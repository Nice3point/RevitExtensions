#if REVIT2026_OR_GREATER
using Autodesk.Revit.ApplicationServices;
#endif

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ModelPathUtils" /> class.
/// </summary>
[PublicAPI]
public static class ModelPathUtilsExtensions
{
    /// <param name="modelPath">The source model path.</param>
    extension(ModelPath modelPath)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ModelPathUtils.ConvertModelPathToUserVisiblePath(Autodesk.Revit.DB.ModelPath)" />
        [Pure]
        public string ConvertToUserVisiblePath()
        {
            return ModelPathUtils.ConvertModelPathToUserVisiblePath(modelPath);
        }
    }
#if REVIT2021_OR_GREATER
    /// <param name="modelGuid">The GUID of the Revit cloud model.</param>
    extension(Guid modelGuid)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ModelPathUtils.ConvertCloudGUIDsToCloudPath(System.String,System.Guid,System.Guid)"/>
        [Pure]
        public ModelPath ConvertToCloudPath(Guid projectGuid, string region)
        {
            return ModelPathUtils.ConvertCloudGUIDsToCloudPath(region, projectGuid, modelGuid);
        }
    }
#endif
#if REVIT2026_OR_GREATER
    /// <param name="application">The source application.</param>
    extension(Application application)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ModelPathUtils.GetAllCloudRegions"/>
        [Pure]
        public IList<string> GetAllCloudRegions()
        {
            return ModelPathUtils.GetAllCloudRegions();
        }
    }
#endif
}
