

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.BooleanOperationsUtils"/> class.
/// </summary>
[PublicAPI]
public static class BooleanOperationsUtilsExtensions
{
    /// <param name="solid">The source solid.</param>
    extension(Solid solid)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.BooleanOperationsUtils.CutWithHalfSpace(Autodesk.Revit.DB.Solid,Autodesk.Revit.DB.Plane)"/>
        [Pure]
        public Solid CutWithHalfSpace(Plane plane)
        {
            return BooleanOperationsUtils.CutWithHalfSpace(solid, plane);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BooleanOperationsUtils.CutWithHalfSpaceModifyingOriginalSolid(Autodesk.Revit.DB.Solid,Autodesk.Revit.DB.Plane)"/>
        public void CutWithHalfSpaceModifyingOriginalSolid(Plane plane)
        {
            BooleanOperationsUtils.CutWithHalfSpaceModifyingOriginalSolid(solid, plane);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BooleanOperationsUtils.ExecuteBooleanOperation(Autodesk.Revit.DB.Solid,Autodesk.Revit.DB.Solid,Autodesk.Revit.DB.BooleanOperationsType)"/>
        [Pure]
        public Solid ExecuteBooleanOperation(Solid other, BooleanOperationsType booleanType)
        {
            return BooleanOperationsUtils.ExecuteBooleanOperation(solid, other, booleanType);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.BooleanOperationsUtils.ExecuteBooleanOperationModifyingOriginalSolid(Autodesk.Revit.DB.Solid,Autodesk.Revit.DB.Solid,Autodesk.Revit.DB.BooleanOperationsType)"/>
        public void ExecuteBooleanOperationModifyingOriginalSolid(Solid other, BooleanOperationsType booleanType)
        {
            BooleanOperationsUtils.ExecuteBooleanOperationModifyingOriginalSolid(solid, other, booleanType);
        }
    }
}