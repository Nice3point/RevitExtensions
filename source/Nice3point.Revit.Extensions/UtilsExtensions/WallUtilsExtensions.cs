

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.WallUtils"/> class.
/// </summary>
[PublicAPI]
public static class WallUtilsExtensions
{
    /// <param name="wall">The source wall.</param>
    extension(Wall wall)
    {
        /// <summary>Identifies if the indicated end of the wall allows joins or not.</summary>
        /// <param name="end">0 or 1 for the beginning or end of the wall's curve</param>
        /// <returns>true if it is allowed to join. false if it is disallowed.</returns>
        [Pure]
        public bool IsJoinAllowedAtEnd(int end)
        {
            return WallUtils.IsWallJoinAllowedAtEnd(wall, end);
        }

        /// <summary>
        ///    Allows the wall's end to join to other walls. If that end is near other walls it will become joined as a result.
        /// </summary>
        /// <remarks>
        ///    By default all walls are allowed to join at ends, so this function is only needed if this wall end is already disallowed to join.
        ///    If this wall is a stacked wall, all subwalls at this end will be allowed to join.
        /// </remarks>
        /// <param name="end">0 or 1 for the beginning or end of the wall's curve</param>
        public void AllowJoinAtEnd(int end)
        {
            WallUtils.AllowWallJoinAtEnd(wall, end);
        }

        /// <summary>Sets the wall's end not to join to other walls.</summary>
        /// <remarks>
        ///    If this wall is already joined at this end, it will become disconnected.
        ///    If this wall is a stacked wall, all subwalls at this end will be disallowed to join.
        /// </remarks>
        /// <param name="end">0 or 1 for the beginning or end of the wall's curve</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public void DisallowJoinAtEnd(int end)
        {
            WallUtils.DisallowWallJoinAtEnd(wall, end);
        }
    }
}