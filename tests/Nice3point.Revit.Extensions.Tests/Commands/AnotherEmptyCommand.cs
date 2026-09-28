using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;

namespace Nice3point.Revit.Extensions.Tests.Commands;

[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public sealed class AnotherEmptyCommand : ExternalCommand
{
    public override void Execute()
    {
    }
}
