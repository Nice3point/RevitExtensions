

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
        /// <summary>
        ///    Checks that the server referenced by the given ExternalResourceReference supports
        ///    AssemblyCodeData.
        /// </summary>
        /// <returns>
        ///    True if the ExternalResourceReference refers to a server that supports AssemblyCodeData. False otherwise.
        /// </returns>
        public bool ServerSupportsAssemblyCodeData => ExternalResourceServerUtils.ServerSupportsAssemblyCodeData(externalResourceReference);

        /// <summary>
        ///    Checks that the server referenced by the given ExternalResourceReference supports
        ///    CAD links.
        /// </summary>
        /// <returns>
        ///    True if the ExternalResourceReference refers to a server that supports CAD links. False otherwise.
        /// </returns>
        public bool ServerSupportsCadLinks => ExternalResourceServerUtils.ServerSupportsCADLinks(externalResourceReference);

        /// <summary>
        ///    Checks that the server referenced by the given ExternalResourceReference supports
        ///    IFC links.
        /// </summary>
        /// <returns>
        ///    True if the ExternalResourceReference refers to a server that supports IFC links. False otherwise.
        /// </returns>
        public bool ServerSupportsIfcLinks => ExternalResourceServerUtils.ServerSupportsIFCLinks(externalResourceReference);

        /// <summary>
        ///    Checks that the server referenced by the given ExternalResourceReference supports
        ///    KeynoteTable data.
        /// </summary>
        /// <returns>
        ///    True if the ExternalResourceReference refers to a server that supports keynotes.  False otherwise.
        /// </returns>
        public bool ServerSupportsKeynotes => ExternalResourceServerUtils.ServerSupportsKeynotes(externalResourceReference);

        /// <summary>
        ///    Checks that the server referenced by the given ExternalResourceReference supports
        ///    Revit links.
        /// </summary>
        /// <returns>
        ///    True if the ExternalResourceReference refers to a server that supports Revit links. False otherwise.
        /// </returns>
        public bool ServerSupportsRevitLinks => ExternalResourceServerUtils.ServerSupportsRevitLinks(externalResourceReference);
    }

    extension(ExternalResourceReference)
    {
        /// <summary>
        ///    Checks whether the name is a valid short name for the external resource server.
        /// </summary>
        /// <remarks>
        ///    A valid short name should match the restrictions documented in <see cref="M:Autodesk.Revit.DB.IExternalResourceServer.GetShortName" />.
        /// </remarks>
        /// <param name="serverId">The id of the external resource server.</param>
        /// <param name="serverName">The short name of the external resource server.</param>
        /// <returns>True if the name is a valid short name, false otherwise.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static bool IsValidShortName(Guid serverId, string serverName)
        {
            return ExternalResourceServerUtils.IsValidShortName(serverId, serverName);
        }
    }
}