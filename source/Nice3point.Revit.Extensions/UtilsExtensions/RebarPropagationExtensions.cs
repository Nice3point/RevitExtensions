#if REVIT2023_OR_GREATER
using Autodesk.Revit.DB.Structure;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions.Structure;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.Structure.RebarPropagation"/> class.
/// </summary>
[PublicAPI]
public static class RebarPropagationExtensions
{
    /// <param name="sourceRebars">The source rebars.</param>
    extension(IList<Rebar> sourceRebars)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarPropagation.AlignByFace(Autodesk.Revit.DB.Document,System.Collections.Generic.IList{Autodesk.Revit.DB.Structure.Rebar},Autodesk.Revit.DB.Reference,Autodesk.Revit.DB.Reference)"/>
        public ISet<ElementId> AlignByFace(Document document, Reference sourceFaceReference, Reference destinationFaceReference)
        {
            return RebarPropagation.AlignByFace(document, sourceRebars, sourceFaceReference, destinationFaceReference);
        }

        /// <inheritdoc cref="Autodesk.Revit.DB.Structure.RebarPropagation.AlignByHost(Autodesk.Revit.DB.Document,System.Collections.Generic.IList{Autodesk.Revit.DB.Structure.Rebar},Autodesk.Revit.DB.Element)"/>
        public ISet<ElementId> AlignByHost(Document document, Element destinationHost)
        {
            return RebarPropagation.AlignByHost(document, sourceRebars, destinationHost);
        }
    }
}
#endif
