#if REVIT2022_OR_GREATER
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.SpecUtils"/> class.
/// </summary>
[PublicAPI]
public static class SpecUtilsExtensions
{
    /// <param name="typeId">Unique identifier</param>
    extension(ForgeTypeId typeId)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.SpecUtils.IsSpec(Autodesk.Revit.DB.ForgeTypeId)"/>
        public bool IsSpec => SpecUtils.IsSpec(typeId);

        /// <inheritdoc cref="Autodesk.Revit.DB.SpecUtils.IsValidDataType(Autodesk.Revit.DB.ForgeTypeId)"/>
        public bool IsValidDataType => SpecUtils.IsValidDataType(typeId);

        /// <inheritdoc cref="Autodesk.Revit.DB.SpecUtils.GetAllSpecs"/>
        [Pure]
        public static IList<ForgeTypeId> GetAllSpecs()
        {
            return SpecUtils.GetAllSpecs();
        }
    }
}
#endif