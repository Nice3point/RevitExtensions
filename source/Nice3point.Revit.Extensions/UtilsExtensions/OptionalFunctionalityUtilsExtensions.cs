using Autodesk.Revit.ApplicationServices;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.OptionalFunctionalityUtils"/> class.
/// </summary>
[PublicAPI]
public static class OptionalFunctionalityUtilsExtensions
{
    extension(Application _)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsDGNExportAvailable"/>
        public bool IsDgnExportAvailable => OptionalFunctionalityUtils.IsDGNExportAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsDGNImportLinkAvailable"/>
        public bool IsDgnImportLinkAvailable => OptionalFunctionalityUtils.IsDGNImportLinkAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsDWFExportAvailable"/>
        public bool IsDwfExportAvailable => OptionalFunctionalityUtils.IsDWFExportAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsDWGExportAvailable"/>
        public bool IsDwgExportAvailable => OptionalFunctionalityUtils.IsDWGExportAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsDWGImportLinkAvailable"/>
        public bool IsDwgImportLinkAvailable => OptionalFunctionalityUtils.IsDWGImportLinkAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsDXFExportAvailable"/>
        public bool IsDxfExportAvailable => OptionalFunctionalityUtils.IsDXFExportAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsFBXExportAvailable"/>
        public bool IsFbxExportAvailable => OptionalFunctionalityUtils.IsFBXExportAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsGraphicsAvailable"/>
        public bool IsGraphicsAvailable => OptionalFunctionalityUtils.IsGraphicsAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsIFCAvailable"/>
        public bool IsIfcAvailable => OptionalFunctionalityUtils.IsIFCAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsNavisworksExporterAvailable"/>
        public bool IsNavisworksExporterAvailable => OptionalFunctionalityUtils.IsNavisworksExporterAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsSATImportLinkAvailable"/>
        public bool IsSatImportLinkAvailable => OptionalFunctionalityUtils.IsSATImportLinkAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsShapeImporterAvailable"/>
        public bool IsShapeImporterAvailable => OptionalFunctionalityUtils.IsShapeImporterAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsSKPImportLinkAvailable"/>
        public bool IsSkpImportLinkAvailable => OptionalFunctionalityUtils.IsSKPImportLinkAvailable();
#if REVIT2022_OR_GREATER

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.Is3DMImportLinkAvailable"/>
        public bool Is3DmImportLinkAvailable => OptionalFunctionalityUtils.Is3DMImportLinkAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsAXMImportLinkAvailable"/>
#if REVIT2027
        [Obsolete("This method is deprecated in Revit 2027 and may be removed in a later version of Revit.")]
#endif
        public bool IsAxmImportLinkAvailable => OptionalFunctionalityUtils.IsAXMImportLinkAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsOBJImportLinkAvailable"/>
        public bool IsObjImportLinkAvailable => OptionalFunctionalityUtils.IsOBJImportLinkAvailable();

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsSTLImportLinkAvailable"/>
        public bool IsStlImportLinkAvailable => OptionalFunctionalityUtils.IsSTLImportLinkAvailable();
#endif
#if REVIT2024_OR_GREATER

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsSTEPImportLinkAvailable"/>
        public bool IsStepImportLinkAvailable => OptionalFunctionalityUtils.IsSTEPImportLinkAvailable();
#endif
#if REVIT2026_OR_GREATER

        /// <inheritdoc cref="Autodesk.Revit.DB.OptionalFunctionalityUtils.IsMaterialLibraryAvailable"/>
        public bool IsMaterialLibraryAvailable => OptionalFunctionalityUtils.IsMaterialLibraryAvailable();
#endif
    }
}