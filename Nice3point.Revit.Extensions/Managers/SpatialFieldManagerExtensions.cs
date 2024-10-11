using Autodesk.Revit.DB.Analysis;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Analysis.SpatialFieldManager"/> class.
/// </summary>
public static class SpatialFieldManagerExtensions
{
    /// <summary>Factory method - creates manager object for the given view</summary>
    /// <param name="view">View for which manager object is created or retrieved</param>
    /// <param name="numberOfMeasurements">
    ///    Total number of measurements in the calculated results.
    ///    This number defines the length of value arrays in ValueAtPoint objects
    /// </param>
    /// <returns>Manager object for the view passed in the argument</returns>
    /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
    ///    numberOfMeasurements is less than one
    /// </exception>
    /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
    ///    View is not allowed to display analysis results or a manager object for this view already exists
    /// </exception>
    public static SpatialFieldManager CreateSpatialFieldManager(this View view, int numberOfMeasurements)
    {
        return SpatialFieldManager.CreateSpatialFieldManager(view, numberOfMeasurements);
    }
    
    /// <summary>Retrieves manager object for the given view or returns NULL</summary>
    /// <param name="view">View for which manager object is retrieved</param>
    /// <returns>Manager object for the view passed in the argument</returns>
    [Pure]
    public static SpatialFieldManager? GetSpatialFieldManager(this View view)
    {
        return SpatialFieldManager.GetSpatialFieldManager(view);
    }
}