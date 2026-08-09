using Autodesk.Revit.DB.Analysis;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Analysis;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Analysis.SpatialFieldManager"/> class.
/// </summary>
[PublicAPI]
public static class SpatialFieldManagerExtensions
{
    /// <param name="view">The source view.</param>
    extension(View view)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Analysis.SpatialFieldManager.CreateSpatialFieldManager(Autodesk.Revit.DB.View,System.Int32)"/>
        public SpatialFieldManager CreateSpatialFieldManager(int numberOfMeasurements)
        {
            return SpatialFieldManager.CreateSpatialFieldManager(view, numberOfMeasurements);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Analysis.SpatialFieldManager.GetSpatialFieldManager(Autodesk.Revit.DB.View)"/>
        [Pure]
        public SpatialFieldManager? GetSpatialFieldManager()
        {
            return SpatialFieldManager.GetSpatialFieldManager(view);
        }
    }
}