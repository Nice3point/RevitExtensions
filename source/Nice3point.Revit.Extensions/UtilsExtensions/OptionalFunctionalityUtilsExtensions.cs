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
        /// <summary>
        ///    Checks whether the DGN Export functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    DGN Export requires presence of certain modules that are optional and may not be part of the installed Revit.
        /// </remarks>
        /// <returns>True if the DGN Export functionality is available in the installed Revit.</returns>
        public bool IsDgnExportAvailable => OptionalFunctionalityUtils.IsDGNExportAvailable();

        /// <summary>
        ///    Checks whether the DGN Import/Link functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    DGN Import/Link is optional functionality that does not have to be part of the Revit installation.
        /// </remarks>
        /// <returns>
        ///    True if the DGN Import/Link functionality is available in the installed Revit.
        /// </returns>
        public bool IsDgnImportLinkAvailable => OptionalFunctionalityUtils.IsDGNImportLinkAvailable();

        /// <summary>
        ///    Checks whether the DWF/DWFx Export functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    DWF/DWFx Export requires presence of certain modules that are optional and may not be part of the installed Revit.
        /// </remarks>
        /// <returns>
        ///    True if the DWF/DWFx Export functionality is available in the installed Revit.
        /// </returns>
        public bool IsDwfExportAvailable => OptionalFunctionalityUtils.IsDWFExportAvailable();

        /// <summary>
        ///    Checks whether the DWG Export functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    DWG Export requires presence of certain modules that are optional and may not be part of the installed Revit.
        /// </remarks>
        /// <returns>True if the DWG Export functionality is available in the installed Revit.</returns>
        public bool IsDwgExportAvailable => OptionalFunctionalityUtils.IsDWGExportAvailable();

        /// <summary>
        ///    Checks whether the DWG Import/Link functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    DWG Import/Link is optional functionality that does not have to be part of the Revit installation.
        /// </remarks>
        /// <returns>
        ///    True if the DWG Import/Link functionality is available in the installed Revit.
        /// </returns>
        public bool IsDwgImportLinkAvailable => OptionalFunctionalityUtils.IsDWGImportLinkAvailable();

        /// <summary>
        ///    Checks whether the DXF Export functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    DXF Export requires presence of certain modules that are optional and may not be part of the installed Revit.
        /// </remarks>
        /// <returns>True if the DXF Export functionality is available in the installed Revit.</returns>
        public bool IsDxfExportAvailable => OptionalFunctionalityUtils.IsDXFExportAvailable();

        /// <summary>
        ///    Checks whether the FBX Export functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    FBX Export requires presence of certain modules that are optional and may not be part of the installed Revit.
        /// </remarks>
        /// <returns>True if the FBX Export functionality is available in the installed Revit.</returns>
        public bool IsFbxExportAvailable => OptionalFunctionalityUtils.IsFBXExportAvailable();

        /// <summary>
        ///    Checks whether the graphics functionality is available to support display, print, and export functionality.
        /// </summary>
        /// <remarks>
        ///    Graphics functionality is optional functionality that does not have to be part of the Revit installation.
        /// </remarks>
        /// <returns>True if the Graphics functionality is available in the installed Revit.</returns>
        public bool IsGraphicsAvailable => OptionalFunctionalityUtils.IsGraphicsAvailable();

        /// <summary>Checks whether IFC functionality is available in the installed Revit.</summary>
        /// <remarks>
        ///    IFC Import/Export requires presence of certain modules that are optional and may not be part of the installed Revit.
        /// </remarks>
        /// <returns>True if the IFC functionality is available in the installed Revit.</returns>
        public bool IsIfcAvailable => OptionalFunctionalityUtils.IsIFCAvailable();

        /// <summary>Checks whether a Navisworks Exporter is available in the installed Revit.</summary>
        /// <remarks>
        ///    A Navisworks exporter is registered via an external add-in. If no add-in has registered an exporter, this will not be a part of the session.
        /// </remarks>
        /// <returns>True if a Navisworks Exporter is available in the installed Revit.</returns>
        public bool IsNavisworksExporterAvailable => OptionalFunctionalityUtils.IsNavisworksExporterAvailable();

        /// <summary>
        ///    Checks whether the SAT Import/Link functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    SAT Import/Link is optional functionality that does not have to be part of the Revit installation.
        /// </remarks>
        /// <returns>
        ///    True if the SAT Import/Link functionality is available in the installed Revit.
        /// </returns>
        public bool IsSatImportLinkAvailable => OptionalFunctionalityUtils.IsSATImportLinkAvailable();

        /// <summary>
        ///    Checks whether the ShapeImporter functionality and Material Library are available in the installed Revit.
        /// </summary>
        /// <returns>
        ///    True if the ShapeImporter functionality and Material Library are available in the installed Revit.
        /// </returns>
        public bool IsShapeImporterAvailable => OptionalFunctionalityUtils.IsShapeImporterAvailable();

        /// <summary>
        ///    Checks whether the SKP Import/Link functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    SKP Import/Link is optional functionality that does not have to be part of the Revit installation.
        /// </remarks>
        /// <returns>
        ///    True if the SKP Import/Link functionality is available in the installed Revit.
        /// </returns>
        public bool IsSkpImportLinkAvailable => OptionalFunctionalityUtils.IsSKPImportLinkAvailable();
#if REVIT2022_OR_GREATER

        /// <summary>
        ///    Checks whether the 3DM Import/Link functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    3DM Import/Link is optional functionality that does not have to be part of the Revit installation.
        /// </remarks>
        /// <returns>
        ///    True if the 3DM Import/Link functionality is available in the installed Revit.
        /// </returns>
        public bool Is3DmImportLinkAvailable => OptionalFunctionalityUtils.Is3DMImportLinkAvailable();

        /// <summary>
        ///    Checks whether the AXM Import/Link functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    AXM Import/Link is optional functionality that does not have to be part of the Revit installation.
        /// </remarks>
        /// <returns>
        ///    True if the AXM Import/Link functionality is available in the installed Revit.
        /// </returns>
#if REVIT2027
        [Obsolete("This method is deprecated in Revit 2027 and may be removed in a later version of Revit.")]
#endif
        public bool IsAxmImportLinkAvailable => OptionalFunctionalityUtils.IsAXMImportLinkAvailable();

        /// <summary>
        ///    Checks whether the OBJ Import/Link functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    OBJ Import/Link is optional functionality that does not have to be a part of the Revit installation.
        /// </remarks>
        /// <returns>
        ///    True if the OBJ Import/Link functionality is available in the installed Revit.
        /// </returns>
        public bool IsObjImportLinkAvailable => OptionalFunctionalityUtils.IsOBJImportLinkAvailable();

        /// <summary>
        ///    Checks whether the STL Import/Link functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    STL Import/Link is optional functionality that does not have to be a part of the Revit installation.
        /// </remarks>
        /// <returns>
        ///    True if the STL Import/Link functionality is available in the installed Revit.
        /// </returns>
        public bool IsStlImportLinkAvailable => OptionalFunctionalityUtils.IsSTLImportLinkAvailable();
#endif
#if REVIT2024_OR_GREATER

        /// <summary>
        ///    Checks whether the STEP Import/Link functionality is available in the installed Revit.
        /// </summary>
        /// <remarks>
        ///    STEP Import/Link is optional functionality that does not have to be a part of the Revit installation.
        /// </remarks>
        /// <returns>
        ///    True if the STEP Import/Link functionality is available in the installed Revit.
        /// </returns>
        public bool IsStepImportLinkAvailable => OptionalFunctionalityUtils.IsSTEPImportLinkAvailable();
#endif
#if REVIT2026_OR_GREATER

        /// <summary>Checks whether the Material Library is available in the installed Revit.</summary>
        /// <returns>True if the Material Library is available in the installed Revit.</returns>
        public bool IsMaterialLibraryAvailable => OptionalFunctionalityUtils.IsMaterialLibraryAvailable();
#endif
    }
}