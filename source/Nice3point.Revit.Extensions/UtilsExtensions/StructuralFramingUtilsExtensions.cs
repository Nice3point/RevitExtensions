using Autodesk.Revit.DB.Structure;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils"/> class.
/// </summary>
[PublicAPI]
public static class StructuralFramingUtilsExtensions
{
    /// <param name="familyInstance">The source family instance, which must be of a structural framing category, non-concrete.</param>
    extension(FamilyInstance familyInstance)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils.CanFlipEnds(Autodesk.Revit.DB.FamilyInstance)"/>
        public bool CanFlipFramingEnds => StructuralFramingUtils.CanFlipEnds(familyInstance);

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils.IsJoinAllowedAtEnd(Autodesk.Revit.DB.FamilyInstance,System.Int32)"/>
        [Pure]
        public bool IsFramingJoinAllowedAtEnd(int end)
        {
            return StructuralFramingUtils.IsJoinAllowedAtEnd(familyInstance, end);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils.GetEndReference(Autodesk.Revit.DB.FamilyInstance,System.Int32)"/>
        [Pure]
        public Reference GetFramingEndReference(int end)
        {
            return StructuralFramingUtils.GetEndReference(familyInstance, end);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils.IsEndReferenceValid(Autodesk.Revit.DB.FamilyInstance,System.Int32,Autodesk.Revit.DB.Reference)"/>
        [Pure]
        public bool IsFramingEndReferenceValid(int end, Reference pick)
        {
            return StructuralFramingUtils.IsEndReferenceValid(familyInstance, end, pick);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils.CanSetEndReference(Autodesk.Revit.DB.FamilyInstance,System.Int32)"/>
        [Pure]
        public bool CanSetFramingEndReference(int end)
        {
            return StructuralFramingUtils.CanSetEndReference(familyInstance, end);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils.AllowJoinAtEnd(Autodesk.Revit.DB.FamilyInstance,System.Int32)"/>
        public void AllowFramingJoinAtEnd(int end)
        {
            StructuralFramingUtils.AllowJoinAtEnd(familyInstance, end);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils.DisallowJoinAtEnd(Autodesk.Revit.DB.FamilyInstance,System.Int32)"/>
        public void DisallowFramingJoinAtEnd(int end)
        {
            StructuralFramingUtils.DisallowJoinAtEnd(familyInstance, end);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils.FlipEnds(Autodesk.Revit.DB.FamilyInstance)"/>
        public void FlipFramingEnds()
        {
            StructuralFramingUtils.FlipEnds(familyInstance);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils.SetEndReference(Autodesk.Revit.DB.FamilyInstance,System.Int32,Autodesk.Revit.DB.Reference)"/>
        public void SetFramingEndReference(int end, Reference pick)
        {
            StructuralFramingUtils.SetEndReference(familyInstance, end, pick);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.StructuralFramingUtils.RemoveEndReference(Autodesk.Revit.DB.FamilyInstance,System.Int32)"/>
        public void RemoveFramingEndReference(int end)
        {
            StructuralFramingUtils.RemoveEndReference(familyInstance, end);
        }
    }
}