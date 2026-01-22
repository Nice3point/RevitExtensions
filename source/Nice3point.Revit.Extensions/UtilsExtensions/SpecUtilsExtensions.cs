using JetBrains.Annotations;

#if REVIT2022_OR_GREATER
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
        /// <summary>Checks whether a ForgeTypeId identifies a spec.</summary>
        /// <returns>True if the ForgeTypeId identifies a spec, false otherwise.</returns>
        public bool IsSpec => SpecUtils.IsSpec(typeId);

        /// <summary>
        ///    Returns true if the given ForgeTypeId identifies a valid parameter data type.
        /// </summary>
        /// <remarks>
        ///    A ForgeTypeId is acceptable as a parameter data type if it
        ///    identifies either a spec or a category. When a category
        ///    identifier is used as a parameter data type, it indicates a
        ///    Family Type parameter of that category.
        /// </remarks>
        /// <returns>
        ///    True if the ForgeTypeId identifies either a spec or a category, false otherwise.
        /// </returns>
        public bool IsValidDataType => SpecUtils.IsValidDataType(typeId);
    }
}
#endif