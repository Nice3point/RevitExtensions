

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.AdaptiveComponentFamilyUtils"/> class.
/// </summary>
[PublicAPI]
public static class AdaptiveComponentFamilyUtilsExtensions
{
    /// <param name="family">The source family.</param>
    extension(Family family)
    {
        /// <summary>Verifies if the Family is an Adaptive Component Family.</summary>
        /// <returns>True if the Family is an Adaptive Component Family.</returns>
        public bool IsAdaptiveComponentFamily => AdaptiveComponentFamilyUtils.IsAdaptiveComponentFamily(family);

        /// <summary>Gets number of Adaptive Point Elements in Adaptive Component Family.</summary>
        /// <returns>Number of Adaptive Point Element References in Adaptive Component Family.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Family family is not an Adaptive Component Family.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        [Pure]
        public int GetNumberOfAdaptivePoints()
        {
            return AdaptiveComponentFamilyUtils.GetNumberOfAdaptivePoints(family);
        }

        /// <summary>Gets number of Placement Point Elements in Adaptive Component Family.</summary>
        /// <returns>
        ///    Number of Adaptive Placement Point Element References in Adaptive Component Family.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Family family is not an Adaptive Component Family.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        [Pure]
        public int GetNumberOfAdaptivePlacementPoints()
        {
            return AdaptiveComponentFamilyUtils.GetNumberOfPlacementPoints(family);
        }

        /// <summary>Gets number of Shape Handle Point Elements in Adaptive Component Family.</summary>
        /// <returns>
        ///    Number of Adaptive Shape Handle Point Element References in the Adaptive Component Family.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Family family is not an Adaptive Component Family.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        [Pure]
        public int GetNumberOfAdaptiveShapeHandlePoints()
        {
            return AdaptiveComponentFamilyUtils.GetNumberOfShapeHandlePoints(family);
        }
    }

    /// <param name="referencePoint">The source reference point.</param>
    extension(ReferencePoint referencePoint)
    {
        /// <summary>Verifies if the Reference Point is an Adaptive Placement Point.</summary>
        /// <returns>True if the Point is an Adaptive Placement Point.</returns>
        public bool IsAdaptivePlacementPoint => AdaptiveComponentFamilyUtils.IsAdaptivePlacementPoint(referencePoint.Document, referencePoint.Id);

        /// <summary>Verifies if the Reference Point is an Adaptive Point.</summary>
        /// <returns>
        ///    True if the Point is an Adaptive Point (Placement Point or Shape Handle Point).
        /// </returns>
        public bool IsAdaptivePoint => AdaptiveComponentFamilyUtils.IsAdaptivePoint(referencePoint.Document, referencePoint.Id);

        /// <summary>Verifies if the Reference Point is an Adaptive Shape Handle Point.</summary>
        /// <returns>True if the Point is an Adaptive Shape Handle Point.</returns>
        public bool IsAdaptiveShapeHandlePoint => AdaptiveComponentFamilyUtils.IsAdaptiveShapeHandlePoint(referencePoint.Document, referencePoint.Id);

        /// <summary>Gets Placement number of an Adaptive Placement Point.</summary>
        /// <returns>Placement number of the Adaptive Placement Point.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Element corresponding to ElementId refPointId does not belong to an Adaptive Family.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public int GetAdaptivePlacementNumber()
        {
            return AdaptiveComponentFamilyUtils.GetPlacementNumber(referencePoint.Document, referencePoint.Id);
        }

        /// <summary>Gets constrain type of an Adaptive Shape Handle Point.</summary>
        /// <returns>Constraint type of the Adaptive Shape Handle Point.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Element corresponding to ElementId refPointId does not belong to an Adaptive Family.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public AdaptivePointConstraintType GetAdaptivePointConstraintType()
        {
            return AdaptiveComponentFamilyUtils.GetPointConstraintType(referencePoint.Document, referencePoint.Id);
        }

        /// <summary>Gets orientation type of an Adaptive Placement Point.</summary>
        /// <returns>Orientation type of Adaptive Placement Point.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Element corresponding to ElementId refPointId does not belong to an Adaptive Family.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public AdaptivePointOrientationType GetAdaptivePointOrientationType()
        {
            return AdaptiveComponentFamilyUtils.GetPointOrientationType(referencePoint.Document, referencePoint.Id);
        }

        /// <summary>
        ///    Makes Reference Point an Adaptive Point or makes an Adaptive Point a Reference Point.
        /// </summary>
        /// <param name="type">The Adaptive Point Type</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Element corresponding to ElementId refPointId does not belong to an Adaptive Family.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        public void MakeAdaptivePoint(AdaptivePointType type)
        {
            AdaptiveComponentFamilyUtils.MakeAdaptivePoint(referencePoint.Document, referencePoint.Id, type);
        }

        /// <summary>Sets Placement Number of an Adaptive Placement Point.</summary>
        /// <param name="placementNumber">Placement number of the Adaptive Placement Point.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Element corresponding to ElementId refPointId does not belong to an Adaptive Family.
        ///    -or-
        ///    The number placementNumber is out of range.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        public void SetAdaptivePlacementNumber(int placementNumber)
        {
            AdaptiveComponentFamilyUtils.SetPlacementNumber(referencePoint.Document, referencePoint.Id, placementNumber);
        }

        /// <summary>Sets constrain type of an Adaptive Shape Handle Point.</summary>
        /// <param name="constraintType">Constraint type of the Adaptive Shape Handle Point.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Element corresponding to ElementId refPointId does not belong to an Adaptive Family.
        ///    -or-
        ///    The ElementId refPointId does not correspond to a Shape Handle Point.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.InvalidOperationException">
        ///    This operation failed.
        /// </exception>
        public void SetAdaptivePointConstraintType(AdaptivePointConstraintType constraintType)
        {
            AdaptiveComponentFamilyUtils.SetPointConstraintType(referencePoint.Document, referencePoint.Id, constraintType);
        }

        /// <summary>Sets orientation type of an Adaptive Placement Point.</summary>
        /// <param name="orientationType">Orientation type of the Adaptive Placement Point.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The Element corresponding to ElementId refPointId does not belong to an Adaptive Family.
        ///    -or-
        ///    The ElementId refPointId does not correspond to an Adaptive Placement Point.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    A value passed for an enumeration argument is not a member of that enumeration
        /// </exception>
        public void SetAdaptivePointOrientationType(AdaptivePointOrientationType orientationType)
        {
            AdaptiveComponentFamilyUtils.SetPointOrientationType(referencePoint.Document, referencePoint.Id, orientationType);
        }
    }
}