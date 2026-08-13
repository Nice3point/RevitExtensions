#if REVIT2022_OR_GREATER


// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.ExternallyTaggedGeometryValidation"/> class.
/// </summary>
[PublicAPI]
public static class ExternallyTaggedGeometryValidationExtensions
{
    /// <param name="geometry">The source geometry object.</param>
    extension(GeometryObject geometry)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.ExternallyTaggedGeometryValidation.IsNonSolid(Autodesk.Revit.DB.GeometryObject)"/>
        public bool IsNonSolid => ExternallyTaggedGeometryValidation.IsNonSolid(geometry);

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternallyTaggedGeometryValidation.IsSolid(Autodesk.Revit.DB.GeometryObject)"/>
        public bool IsSolid => ExternallyTaggedGeometryValidation.IsSolid(geometry);
#if REVIT2024_OR_GREATER

        /// <inheritdoc cref="Autodesk.Revit.DB.ExternallyTaggedGeometryValidation.LacksSubnodes(Autodesk.Revit.DB.GeometryObject)"/>
        public bool LacksSubnodes => ExternallyTaggedGeometryValidation.LacksSubnodes(geometry);
#endif
    }
}
#endif
