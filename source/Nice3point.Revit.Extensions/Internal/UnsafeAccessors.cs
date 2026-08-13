#if NET8_0_OR_GREATER
using System.Runtime.CompilerServices;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Creation;
using Application = Autodesk.Revit.ApplicationServices.Application;
using Document = Autodesk.Revit.DB.Document;

namespace Nice3point.Revit.Extensions.Internal;

internal static class UnsafeAccessors
{
    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    public static extern ControlledApplication CreateControlledApplication(Application application);

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "getDocument")]
    public static extern Document GetDocument(ItemFactoryBase document);
}
#endif
