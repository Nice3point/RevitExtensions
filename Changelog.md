# Release 2025.0.0-preview.3.0

Added new extensions to help you add context menu items without creating additional classes or specifying type names. Revit 2025 and higher

The **ConfigureContextMenu()** method registers an action used to configure a Context menu.

```c#
application.ConfigureContextMenu(menu =>
{
    menu.AddMenuItem<Command>("Menu title");
    menu.AddMenuItem<Command>("Menu title")
        .SetAvailabilityController<Controller>()
        .SetToolTip("Description");
});
```

You can also specify your own context menu title. By default, Revit uses the Application name

```c#
application.ConfigureContextMenu("Title", menu =>
{
    menu.AddMenuItem<Command>("Menu title");
});
```

The **AddMenuItem()** method adds a menu item to the Context Menu.

```c#
menu.AddMenuItem<Command>("Menu title");
```

The **AddSeparator()** method adds a separator to the Context Menu.

```c#
menu.AddSeparator();
```

The **AddSubMenu()** method adds a sub menu to the Context Menu.

```c#
var subMenu = new ContextMenu();
subMenu.AddMenuItem<Command>("Menu title");
subMenu.AddMenuItem<Command>("Menu title");

menu.AddSubMenu("Sub menu title", subMenu);
```

The **SetAvailabilityController()** method specifies the class type that decides the availability of menu item.

```c#
menuItem.SetAvailabilityController<Controller>()
```

# Release 2025.0.0-preview.2.0

- New package icon
- New `FindParameter()` overloads with GUID and Definition. This method combines all API methods for getting a parameter, such as `get_Parameter`, `LookupParameter`, `GetParameter`. It also searches for a parameter in the element type if there is no such parameter in the element
- `GetParameter()` method is obsolete. Use `FindParameter()` instead

# Release 2025.0.0-preview.1.0

- Revit 2025 support

# Release 2024.0.0

Revit 2024 support

# Release 2023.1.9

ElementExtensions:

- Method chain support

ElementIdExtensions:

- Fixed cast operations. Directly cast used by default (safe cast early)

SchemaExtensions:

- SaveEntity now return bool if the entity save was successful

SystemExtensions:

- AppendPath overload with params

# Release 2023.1.8

UnitExtensions:

- New FromUnit extension
- New ToUnit extension

# Release 2023.1.7

RibbonExtensions:

- New SetAvailabilityController() extension

ParameterExtensions:

- New Set(bool) extension
- New Set(color) extension

Nuget symbol server support: https://symbols.nuget.org/download/symbols

# Release 2023.1.6

CollectorExtensions:

- New GetElements(ElementId viewId) extension
- New GetElements(ICollection<ElementId> elementIds) extension
- New overloads for instances with ViewId

# Release 2023.1.5

New Parameter extensions

Fixed GetParameter extensions for cases where the parameter value was not initialized

# Release 2023.1.4

New Application extensions
New Collector extensions

Updated TargetFramework. It now matches the framework that Revit is running on

# Release 2023.1.3

New Schema extensions

# Release 2023.1.2

Geometry extensions

- New line.SetCoordinateX extension
- New line.SetCoordinateY extension
- New line.SetCoordinateZ extension
- New arc.SetCoordinateX extension
- New arc.SetCoordinateY extension
- New arc.SetCoordinateZ extension

# Release 2023.1.1

New JoinGeometryUtils extensions

# Release 2023.1.0

- Revit 2023 support
- Update attributes

# Release 2022.0.5

Unit extensions

- New FormatUnit extensions

Solid Extensions

- New Clone extension
- New CreateTransformed extension
- New SplitVolumes extension
- New IsValidForTessellation extension
- New TessellateSolidOrShell extension
- New FindAllEdgeEndPointsAtVertex extension

Ribbon Extensions

- New AddPushButton<TCommand> extensions
- Uri changed to UriKind.RelativeOrAbsolute

# Release 2022.0.4

Element extensions

- New GetParameter(ForgeTypeId) extension
- New Cast<T>() extension
- Removed CanBeNull attribute for ToElement<T> extension

Host Extensions

- New GetBottomFaces() extension
- New GetTopFaces() extension
- New GetSideFaces() extension

# Release 2022.0.3

Element extensions

- New Copy extension
- New Mirror extension
- New Move extension
- New Rotate extension
- New CanBeMirrored extension

Label Extensions

- New ToLabel(BuiltInParameter) extension
- New ToLabel(BuiltInParameterGroup) extension
- New ToLabel(BuiltInCategory) extension
- New ToLabel(DisplayUnitType) extension
- New ToLabel(ParameterType) extension
- New ToDisciplineLabel(ForgeTypeId) extension
- New ToGroupLabel(ForgeTypeId) extension
- New ToSpecLabel(ForgeTypeId) extension
- New ToSymbolLabel(ForgeTypeId) extension
- New ToUnitLabel(ForgeTypeId) extension

# Release 2022.0.2

Ribbon extensions

- New AddSplitButton extension
- New AddRadioButtonGroup extension
- New AddComboBox extension
- New AddTextBox extension

Unit extensions

- New FromMeters extension
- New ToMeters extension
- New FromDegrees extension
- New ToDegrees extension

ElementId extensions

- New AreEquals(BuiltInParameter) extension

# Release 2022.0.1

- Connected JetBrains annotations

# Release 2022.0.0

- Initial release