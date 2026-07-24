#if REVIT2026_OR_GREATER
using Autodesk.Revit.ApplicationServices;
#endif

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ModelPathUtils"/> class.
/// </summary>
[PublicAPI]
public static class ModelPathUtilsExtensions
{
    /// <param name="modelPath">The source model path.</param>
    extension(ModelPath modelPath)
    {
        /// <summary>Gets a string version of the path of a given ModelPath.</summary>
        /// <returns>The path in string form</returns>
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
        /// <summary>Converts a pair of cloud project and model GUIDs to a valid cloud path.</summary>
        /// <param name="region">
        ///    The region of the BIM 360 Docs or Autodesk Docs account and project which contains this model.
        ///    Please see the reference values, like <see cref="P:Autodesk.Revit.DB.ModelPathUtils.CloudRegionUS" /> and <see cref="P:Autodesk.Revit.DB.ModelPathUtils.CloudRegionEMEA" />,
        ///    and the new regions from release note.
        /// </param>
        /// <param name="projectGuid">The GUID of the cloud project which contains the model.</param>
        /// <returns>The cloud model path.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.CentralModelException">
        ///    The cloud project is missing.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerCommunicationException">
        ///    The central server could not be reached.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerUnauthenticatedUserException">
        ///    You must sign in to Autodesk 360 in order to complete action.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.RevitServerUnauthorizedException">
        ///    You are unauthorized to access this resource.
        /// </exception>
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
        /// <summary>
        ///    Gets all available regions that are supported by the cloud service. It returns an empty list when something gets wrong. Check the journal to see the error message for further information.
        /// </summary>
        /// <remarks>
        ///    The list may change dynamically when Revit Cloud Model are declared to support more regions.
        /// </remarks>
        /// <returns>The list of regions.</returns>
        [Pure]
        public IList<string> GetAllCloudRegions()
        {
            return ModelPathUtils.GetAllCloudRegions();
        }
    }
#endif
}