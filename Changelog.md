# Release 2025.0.1-preview.4.1

This update focuses on increased utility class coverage, new extensions for global parameter management, for ForgeTypeId handling, advanced geometry extensions, and many-many more.

## New Extensions

**Element Association Extensions**

- **IsAnalyticalElement:** Determines whether an element is an analytical element.
- **IsPhysicalElement:** Checks if an element is a physical one.

**Element Worksharing Extensions**

- **GetCheckoutStatus:** Retrieves the ownership status of an element, with an optional parameter to return the owner's name.
- **GetWorksharingTooltipInfo:** Provides worksharing information about an element for in-canvas tooltips.
- **GetModelUpdatesStatus:** Gets the status of an element in the central model.

**ElementId Extensions**

- **ToElements:** Retrieves a collection of Elements associated with the specified ElementIds.
- **ToElements<T>:** Retrieves a collection of Elements associated with the specified ElementIds as the specified type T.
- **ToOrderedElements:** Retrieves the Elements associated with the specified ElementIds in their original order.
- **ToOrderedElements<T>:** Retrieves the Elements associated with the specified ElementIds and casts them to the specified type T in their original order.

**ElementId Transform Extensions**

- **CanMirrorElements:** Verifies whether a set of elements can be mirrored.
- **MirrorElements:** Mirrors elements across a plane, with support for mirrored copies.
- **MoveElements:** Moves elements according to a specified transformation.
- **RotateElements:** Rotates elements around a given axis by a specified angle.
- **CopyElements:** Copies elements between views or within the same document, with options for translation and transformation.

**Document Extensions**

- **GetProfileSymbols:** Returns profile family symbols in the document.
- **RelinquishOwnership:** Allows relinquishment of ownership based on specified options.

**Document Managers Extensions**

- **GetTemporaryGraphicsManager:** Fetches a reference to the document’s temporary graphics manager.
- **GetAnalyticalToPhysicalAssociationManager:** Retrieves the manager for analytical-to-physical element associations.
- **GetLightGroupManager:** Creates or retrieves the light group manager for the document.

**Geometry Extensions**

- **Contains:** Determines if a point or another bounding box is contained within the bounding box, with strict mode available for more precise containment checks.
- **Overlaps:** Checks whether two bounding boxes overlap.
- **ComputeCentroid:** Calculates the geometric center of a bounding box.
- **ComputeVertices:** Returns the coordinates of the eight vertices of a bounding box.
- **ComputeVolume:** Computes the volume enclosed by the bounding box.
- **ComputeSurfaceArea:** Calculates the total surface area of the bounding box.

**Parameters Extensions**

- **IsBuiltInParameter:** Verifies if a parameter is a built-in parameter.

**Document Global Parameters Extensions**

- **FindGlobalParameter:** Locates a global parameter by name in the document.
- **GetAllGlobalParameters:** Returns all global parameters in the document.
- **GetGlobalParametersOrdered:** Retrieves ordered global parameters.
- **SortGlobalParameters:** Sorts global parameters in the specified order.
- **MoveUpOrder:** Moves a global parameter up in the order.
- **MoveDownOrder:** Moves a global parameter down in the order.
- **IsUniqueGlobalParameterName:** Checks if a global parameter name is unique.
- **IsValidGlobalParameter:** Validates if an ElementId is a global parameter.
- **AreGlobalParametersAllowed:** Checks if global parameters are permitted in the document.

**ForgeTypeId Extensions**

- **IsSpec:** Validates if a ForgeTypeId identifies a spec.
- **IsBuiltInGroup:** Determines whether a ForgeTypeId identifies a built-in parameter group.
- **IsBuiltInParameter:** Validates if a ForgeTypeId identifies a built-in parameter.
- **IsSymbol:** Checks if a ForgeTypeId identifies a symbol.
- **IsUnit:** Verifies if a ForgeTypeId identifies a unit.
- **IsValidDataType:** Determines if a ForgeTypeId identifies a valid parameter data type.
- **IsValidUnit:** Validates if a unit is valid for a measurable spec.
- **IsMeasurableSpec:** Checks if a ForgeTypeId represents a measurable spec.
- **GetBuiltInParameter:** Retrieves a BuiltInParameter from a ForgeTypeId.
- **GetParameterTypeId:** Fetches the ForgeTypeId corresponding to a BuiltInParameter.
- **GetDiscipline:** Gets the discipline for a measurable spec.
- **GetValidUnits:** Retrieves all valid units for a measurable spec.
- **GetTypeCatalogStringForSpec:** Fetches the type catalog string for a measurable spec.
- **GetTypeCatalogStringForUnit:** Retrieves the type catalog string for a unit.
- **DownloadCompanyName:** Downloads the owning company name for a parameter and records it in the document.
- **DownloadParameterOptions:** Fetches settings related to a parameter from the Parameters Service.
- **DownloadParameter:** Creates a shared parameter element in a document based on a downloaded parameter definition.

**Color Extensions**

- **ToHex:** Converts a color to its hexadecimal representation.
- **ToHexInteger:** Returns the hexadecimal integer representation of a color.
- **ToRgb:** Provides the RGB representation of a color.
- **ToHsl:** Converts a color to its HSL representation.
- **ToHsv:** Converts a color to its HSV representation.
- **ToCmyk:** Retrieves the CMYK representation of a color.
- **ToHsb:** Converts a color to HSB.
- **ToHsi:** Converts a color to HSI format.
- **ToHwb:** Provides the HWB representation of a color.
- **ToNCol:** Converts a color to NCol format.
- **ToCielab:** Retrieves the Cielab representation of a color.
- **ToCieXyz:** Converts a color to CieXyz format.
- **ToFloat:** Returns the float representation of a color.
- **ToDecimal:** Returns the decimal representation of a color.

**Family Extensions**

- **CanConvertToFaceHostBased:** Indicates whether a family can be converted to a face-hosted version.
- **ConvertToFaceHostBased:** Converts a family to be face-host based.

**Plumbing Extensions**

- **ConnectPipePlaceholdersAtElbow:** Connects pipe placeholders that form an elbow connection.
- **ConnectPipePlaceholdersAtTee:** Connects pipe placeholders to form a Tee connection.
- **ConnectPipePlaceholdersAtCross:** Connects pipe placeholders to form a Cross connection.
- **PlaceCapOnOpenEnds:** Places caps on open pipe connectors.
- **HasOpenConnector:** Checks if a pipe curve has an open piping connector.
- **BreakCurve:** Splits a pipe curve at a specified point.

**Element Solid Cut Extensions**

- **GetCuttingSolids:** Retrieves solids that cut a given element.
- **GetSolidsBeingCut:** Returns solids that are cut by the given element.
- **IsAllowedForSolidCut:** Verifies if the element can be involved in a solid-solid cut.
- **IsElementFromAppropriateContext:** Checks if the element belongs to a suitable context for solid cuts.
- **CanElementCutElement:** Determines whether a cutting element can create a solid cut on a target element.
- **CutExistsBetweenElements:** Checks if there is an existing solid-solid cut between two elements.
- **AddCutBetweenSolids:** Adds a solid-solid cut between two elements.
- **RemoveCutBetweenSolids:** Removes an existing solid cut between two elements.
- **SplitFacesOfCuttingSolid:** Splits the faces of a cutting solid element.

**View Extensions**

- **GetTransformFromViewToView:** Returns a transformation to copy elements between two views.

**View Managers Extensions**

- **CreateSpatialFieldManager:** Creates a SpatialField manager for a given view.
- **GetSpatialFieldManager:** Retrieves the SpatialField manager for a specific view.

**Ribbon Extensions**

- **AddStackPanel:** Adds a vertical stack panel to the specified Ribbon panel.
- **AddPushButton:** Adds a PushButton to the vertical stack panel.
- **AddPullDownButton:** Adds a PullDownButton to the vertical stack panel.
- **AddSplitButton:** Adds a SplitButton to the vertical stack panel.
- **AddComboBox:** Adds a ComboBox to the vertical stack panel.
- **AddTextBox:** Adds a TextBox to the vertical stack panel.
- **AddLabel:** Adds a text label to the vertical stack panel.
- **RemovePanel:** Removes RibbonPanel from the Revit ribbon.
- **SetToolTip:** Sets the tooltip text for the PushButton.
- **SetLongDescription:** Sets the extended tooltip description for the RibbonItem.
- **CreatePanel:** Now works with internal panels. Panel can be added to the Modify, View, Manage and other tabs.
- **SetImage** support for Light and Dark UI themes.
- **SetLargeImage** support for Light and Dark UI themes.

![verticalStack](https://github.com/user-attachments/assets/3cef1e86-89a3-4f9c-8a06-b7661c6f428f)

## Breaking changes

- **ToFraction:** Now uses "8" precision, instead of "32".
- **ToFraction:** Now suppress "0" for inches part.
- **SetAvailabilityController:** Now returns `PushButton` instead of `RibbonButton`.

**Readme** has been updated, you can find a detailed description and code samples in it.

# Release 2025.0.0

- Revit 2025 support
- New `FindParameter()` overloads with GUID and Definition. This method combines all API methods for getting a parameter, such as `get_Parameter`, `LookupParameter`, `GetParameter`. It also searches for a parameter in the element type if there is no such parameter in the element
- `GetParameter()` method is obsolete. Use `FindParameter()` instead
- New extensions to help you add context menu items without creating additional classes or specifying type names. Revit 2025 and higher

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