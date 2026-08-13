// ReSharper disable once CheckNamespace

namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.WallUtils" /> class.
/// </summary>
[PublicAPI]
public static class WallUtilsExtensions
{
    /// <param name="wall">The source wall.</param>
    extension(Wall wall)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.WallUtils.IsWallJoinAllowedAtEnd(Autodesk.Revit.DB.Wall,System.Int32)" />
        [Pure]
        public bool IsJoinAllowedAtEnd(int end)
        {
            return WallUtils.IsWallJoinAllowedAtEnd(wall, end);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WallUtils.AllowWallJoinAtEnd(Autodesk.Revit.DB.Wall,System.Int32)" />
        public void AllowJoinAtEnd(int end)
        {
            WallUtils.AllowWallJoinAtEnd(wall, end);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.WallUtils.DisallowWallJoinAtEnd(Autodesk.Revit.DB.Wall,System.Int32)" />
        public void DisallowJoinAtEnd(int end)
        {
            WallUtils.DisallowWallJoinAtEnd(wall, end);
        }
    }
}
