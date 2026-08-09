#if REVIT2024_OR_GREATER
using JetBrains.Annotations;
using Document = Autodesk.Revit.Creation.Document;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.MEPSupportUtils"/> class.
/// </summary>
[PublicAPI]
public static class MepSupportUtilsExtensions
{
    extension(Document _)
    {
        /// <inheritdoc cref="Autodesk.Revit.DB.MEPSupportUtils.CreateDuctworkStiffener(Autodesk.Revit.DB.Document,Autodesk.Revit.DB.ElementId,Autodesk.Revit.DB.ElementId,System.Double)"/>
        public FamilyInstance NewDuctworkStiffener(FamilySymbol familySymbol, Element host, double distanceFromHostEnd)
        {
            return MEPSupportUtils.CreateDuctworkStiffener(familySymbol.Document, familySymbol.Id, host.Id, distanceFromHostEnd);
        }
    }
}
#endif