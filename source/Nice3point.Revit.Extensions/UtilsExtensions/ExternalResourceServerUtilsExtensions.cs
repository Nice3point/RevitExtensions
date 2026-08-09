

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExternalResourceServerUtils"/> class.
/// </summary>
[PublicAPI]
public static class ExternalResourceServerUtilsExtensions
{
    /// <param name="externalResourceReference">The source external resource reference.</param>
    extension(ExternalResourceReference externalResourceReference)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalResourceServerUtils.ServerSupportsAssemblyCodeData(Autodesk.Revit.DB.ExternalResourceReference)"/>
        public bool ServerSupportsAssemblyCodeData => ExternalResourceServerUtils.ServerSupportsAssemblyCodeData(externalResourceReference);

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalResourceServerUtils.ServerSupportsCADLinks(Autodesk.Revit.DB.ExternalResourceReference)"/>
        public bool ServerSupportsCadLinks => ExternalResourceServerUtils.ServerSupportsCADLinks(externalResourceReference);

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalResourceServerUtils.ServerSupportsIFCLinks(Autodesk.Revit.DB.ExternalResourceReference)"/>
        public bool ServerSupportsIfcLinks => ExternalResourceServerUtils.ServerSupportsIFCLinks(externalResourceReference);

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalResourceServerUtils.ServerSupportsKeynotes(Autodesk.Revit.DB.ExternalResourceReference)"/>
        public bool ServerSupportsKeynotes => ExternalResourceServerUtils.ServerSupportsKeynotes(externalResourceReference);

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalResourceServerUtils.ServerSupportsRevitLinks(Autodesk.Revit.DB.ExternalResourceReference)"/>
        public bool ServerSupportsRevitLinks => ExternalResourceServerUtils.ServerSupportsRevitLinks(externalResourceReference);
    }

    extension(ExternalResourceReference)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExternalResourceServerUtils.IsValidShortName(System.Guid,System.String)"/>
        [Pure]
        public static bool IsValidShortName(Guid serverId, string serverName)
        {
            return ExternalResourceServerUtils.IsValidShortName(serverId, serverName);
        }
    }
}