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
        /// <summary>Determines if the ends of the given framing element can be flipped.</summary>
        /// <returns>
        ///    True for non-concrete line, arc or ellipse framing element, false otherwise.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public bool CanFlipFramingEnds => StructuralFramingUtils.CanFlipEnds(familyInstance);

        /// <summary>
        ///    Identifies if the indicated end of the framing element is allowed to join to others.
        /// </summary>
        /// <param name="end">The index of the end (0 for the start, 1 for the end).</param>
        /// <returns>True if it is allowed to join. False if it is disallowed.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    end must be 0 or 1.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    The input familyInstance is not of a structural framing category.
        /// </exception>
        [Pure]
        public bool IsFramingJoinAllowedAtEnd(int end)
        {
            return StructuralFramingUtils.IsJoinAllowedAtEnd(familyInstance, end);
        }

        /// <summary>
        ///    Returns a reference to the end of a framing element according to the setback settings.
        /// </summary>
        /// <param name="end">The index of the end (0 for the start, 1 for the end).</param>
        /// <returns>The end reference.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    end must be 0 or 1.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    The input familyInstance is not of a structural framing category or is concrete or is not joined at given end and cannot have an end reference set.
        /// </exception>
        [Pure]
        public Reference GetFramingEndReference(int end)
        {
            return StructuralFramingUtils.GetEndReference(familyInstance, end);
        }

        /// <summary>
        ///    Determines if the given reference can be set for the given end of the framing element.
        /// </summary>
        /// <param name="end">The index of the end (0 for the start, 1 for the end).</param>
        /// <param name="pick">
        ///    The reference to be checked against the given end of the framing element.
        /// </param>
        /// <returns>
        ///    True if the given reference can be set for the given end of the framing element.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    end must be 0 or 1.
        /// </exception>
        [Pure]
        public bool IsFramingEndReferenceValid(int end, Reference pick)
        {
            return StructuralFramingUtils.IsEndReferenceValid(familyInstance, end, pick);
        }

        /// <summary>
        ///    Determines if a reference can be set for the given end of the framing element.
        /// </summary>
        /// <param name="end">The index of the end (0 for the start, 1 for the end).</param>
        /// <returns>True if reference can be set for the given end of the framing element.</returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    end must be 0 or 1.
        /// </exception>
        [Pure]
        public bool CanSetFramingEndReference(int end)
        {
            return StructuralFramingUtils.CanSetEndReference(familyInstance, end);
        }

        /// <summary>
        ///    Sets the indicated end of the framing element to be allowed to join to others.
        /// </summary>
        /// <remarks>
        ///    If that end is near other elements it will become joined as a result.
        ///    By default all framing elements are allowed to join at ends, so this function is only needed if this element end is already disallowed to join.
        /// </remarks>
        /// <param name="end">The index of the end (0 for the start, 1 for the end).</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    end must be 0 or 1.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    The input familyInstance is not of a structural framing category.
        /// </exception>
        public void AllowFramingJoinAtEnd(int end)
        {
            StructuralFramingUtils.AllowJoinAtEnd(familyInstance, end);
        }

        /// <summary>
        ///    Sets the indicated end of the framing element to not be allowed to join to others.
        /// </summary>
        /// <remarks>
        ///    If this framing element is already joined at this end, it will become disconnected.
        /// </remarks>
        /// <param name="end">The index of the end (0 for the start, 1 for the end).</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    end must be 0 or 1.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    The input familyInstance is not of a structural framing category.
        /// </exception>
        public void DisallowFramingJoinAtEnd(int end)
        {
            StructuralFramingUtils.DisallowJoinAtEnd(familyInstance, end);
        }

        /// <summary>Flips the ends of the structural framing element.</summary>
        /// <remarks>
        ///    Only ends of non-concrete structural framing element like beam and brace can be flipped and only in case if it is a line, arc or ellipse element.
        /// </remarks>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The input familyInstance is concrete or is not a line, arc or ellipse element.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    The input familyInstance is not of a structural framing category.
        /// </exception>
        public void FlipFramingEnds()
        {
            StructuralFramingUtils.FlipEnds(familyInstance);
        }

        /// <summary>Sets the end reference of a framing element.</summary>
        /// <remarks>The setback value will be changed as a result of the removal.</remarks>
        /// <param name="end">The index of the end (0 for the start, 1 for the end).</param>
        /// <param name="pick">The reference to set to the given end.</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    end must be 0 or 1.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    The input familyInstance is not of a structural framing category or is concrete or is not joined at given end and cannot have an end reference set.
        ///    -or-
        ///    The input pick cannot be set as the end reference for the given end of the structural framing element.
        /// </exception>
        public void SetFramingEndReference(int end, Reference pick)
        {
            StructuralFramingUtils.SetEndReference(familyInstance, end, pick);
        }

        /// <summary>Resets the end reference of the structural framing element.</summary>
        /// <remarks>The setback value will be changed as a result of the removal.</remarks>
        /// <param name="end">The index of the end (0 for the start, 1 for the end).</param>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentOutOfRangeException">
        ///    end must be 0 or 1.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentsInconsistentException">
        ///    The input familyInstance is not of a structural framing category or is concrete or is not joined at given end and cannot have an end reference set.
        /// </exception>
        public void RemoveFramingEndReference(int end)
        {
            StructuralFramingUtils.RemoveEndReference(familyInstance, end);
        }
    }
}