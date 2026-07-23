

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.FormUtils"/> class.
/// </summary>
[PublicAPI]
public static class FormUtilsExtensions
{
    extension(Form)
    {
        /// <summary>
        ///    Validates that input contains one or more form elements or geom combinations containing form elements.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="elements">A collection of elements.</param>
        /// <returns>
        ///    True if inputs contain one or more form elements.  Non-form element inputs are ignored.
        ///    False if none of the inputs are form elements or do not contain form elements.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        [Pure]
        public static bool CanBeDissolved(Document document, ICollection<ElementId> elements)
        {
            return FormUtils.CanBeDissolved(document, elements);
        }

        /// <summary>Dissolves a collection of form elements into their defining elements.</summary>
        /// <param name="document">The document</param>
        /// <param name="elements">
        ///    A collection of element IDs of Forms and GeomCombinations that contain Forms that will be dissolved.
        /// </param>
        /// <returns>
        ///    A collection of curve element ids from the profiles and paths of the dissolved forms.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The elements do not include Forms that can be dissolved.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public static ICollection<ElementId> DissolveForms(Document document, ICollection<ElementId> elements)
        {
            return FormUtils.DissolveForms(document, elements);
        }

        /// <summary>Dissolves a collection of form elements into their defining elements.</summary>
        /// <remarks>
        ///    Profile origin points define the workplane of form profiles and paths and their curves.
        ///    The profile origin point represents a coordinate system with an origin (reference point) which
        ///    can be manipulated to move the curves of a profile together as a unit after dissolve.
        ///    Profile origin points may themselves be constrained to other parts of the model or parts of the form,
        ///    based on how the form was created/constructed.  This is done through the reference point hosting
        ///    mechanism.
        /// </remarks>
        /// <param name="document">The document</param>
        /// <param name="elements">
        ///    A collection of element IDs of Forms and GeomCombinations that contain Forms that will be dissolved.
        /// </param>
        /// <param name="profileOriginPointSet">
        ///    A collection of the point element ids that represent the 'origin' of the profiles
        /// </param>
        /// <returns>
        ///    A collection of curve element ids from the profiles and paths of the dissolved forms.
        /// </returns>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentException">
        ///    The elements do not include Forms that can be dissolved.
        /// </exception>
        /// <exception cref="T:Autodesk.Revit.Exceptions.ArgumentNullException">
        ///    A non-optional argument was null
        /// </exception>
        public static ICollection<ElementId> DissolveForms(Document document, ICollection<ElementId> elements, out ICollection<ElementId> profileOriginPointSet)
        {
            return FormUtils.DissolveForms(document, elements, out profileOriginPointSet);
        }
    }
}