

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExternalResourceServiceUtils"/> class.
/// </summary>
[PublicAPI]
public static class ExternalResourceServiceUtilsExtensions
{
    /// <param name="resourceType">The source external resource type.</param>
    extension(ExternalResourceType resourceType)
    {
        /// <summary>
        ///    Gets registered external resource servers which support the external resource type.
        /// </summary>
        /// <returns>A list of matched external resource servers</returns>
        [Pure]
        public IList<IExternalResourceServer> GetServers()
        {
            return ExternalResourceServiceUtils.GetServersByType(resourceType);
        }
    }
}