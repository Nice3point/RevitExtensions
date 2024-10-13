#if REVIT2025_OR_GREATER
using Autodesk.Revit.UI;

namespace Nice3point.Revit.Extensions.Abstraction;

internal sealed class ContextMenuCreator(Action<ContextMenu> handler) : IContextMenuCreator
{
    public void BuildContextMenu(ContextMenu menu) => handler.Invoke(menu);
}
#endif