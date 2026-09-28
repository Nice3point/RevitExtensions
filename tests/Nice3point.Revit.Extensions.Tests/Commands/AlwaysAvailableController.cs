using Autodesk.Revit.UI;

namespace Nice3point.Revit.Extensions.Tests.Commands;

[UsedImplicitly]
public sealed class AlwaysAvailableController : IExternalCommandAvailability
{
    public bool IsCommandAvailable(UIApplication applicationData, CategorySet selectedCategories)
    {
        return true;
    }
}
