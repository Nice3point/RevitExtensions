<p align="center">
    <picture>
        <source media="(prefers-color-scheme: dark)" width="750" srcset="https://github.com/Nice3point/RevitExtensions/assets/20504884/d605eb83-74a7-4a47-9db8-cb0daced374e">
        <img alt="RevitLookup" width="750" src="https://github.com/Nice3point/RevitExtensions/assets/20504884/a1772d7d-38d4-4a9b-9985-1d83b8cbea8d">
    </picture>
</p>

## Improve your experience with Revit API

[![Nuget](https://img.shields.io/nuget/vpre/Nice3point.Revit.Extensions?style=for-the-badge)](https://www.nuget.org/packages/Nice3point.Revit.Extensions)
[![Downloads](https://img.shields.io/nuget/dt/Nice3point.Revit.Extensions?style=for-the-badge)](https://www.nuget.org/packages/Nice3point.Revit.Extensions)
[![Last Commit](https://img.shields.io/github/last-commit/Nice3point/RevitExtensions/develop?style=for-the-badge)](https://github.com/Nice3point/RevitExtensions/commits/develop)

Extensions bring a fresh, intuitive way to interact with the Revit API. By adding extension methods, they make your code more readable, maintainable, and concise.

Forget about complex utility methods — extensions provide a fluent syntax that lets you focus on what matters:

```csharp
new ElementId(123469)
    .ToElement<Door>()
    .Mirror()
    .FindParameter("Height")
    .AsDouble()
    .ToMillimeters()
    .Round()
```

Seamless integration with modern .NET features like `Nullable` and `Generics` gives you greater flexibility and control over your code.

## Installation

You can install Extensions as a [nuget package](https://www.nuget.org/packages/Nice3point.Revit.Extensions).

Packages are compiled for a specific version of Revit, to support different versions of libraries in one project, use RevitVersion property.

```text
<PackageReference Include="Nice3point.Revit.Extensions" Version="$(RevitVersion).*"/>
```

Package included by default in [Revit Templates](https://github.com/Nice3point/RevitTemplates).

## Table of contents

<!-- TOC -->
  * [Element extensions](#element-extensions)
    * [Element transform extensions](#element-transform-extensions)
    * [Element association extensions](#element-association-extensions)
    * [Element worksharing extensions](#element-worksharing-extensions)
    * [Element schema extensions](#element-schema-extensions)
  * [ElementId extensions](#elementid-extensions)
    * [ElementId transform extensions](#elementid-transform-extensions)
  * [Application extensions](#application-extensions)
    * [Ribbon Extensions](#ribbon-extensions)
    * [ContextMenu Extensions](#contextmenu-extensions)
  * [Document extensions](#document-extensions)
    * [Document managers extensions](#document-managers-extensions)
  * [Geometry extensions](#geometry-extensions)
    * [Element geometry extensions](#element-geometry-extensions)
  * [Parameters extensions](#parameters-extensions)
    * [Document global parameters extensions](#document-global-parameters-extensions)
  * [FilteredElementCollector extensions](#filteredelementcollector-extensions)
  * [ForgeTypeId extensions](#forgetypeid-extensions)
  * [Unit Extensions](#unit-extensions)
  * [Label Extensions](#label-extensions)
  * [Color extensions](#color-extensions)
  * [Family extensions](#family-extensions)
  * [HostObject extensions](#hostobject-extensions)
  * [Plumbing extensions](#plumbing-extensions)
  * [Solid extensions](#solid-extensions)
    * [Element solid cut extensions](#element-solid-cut-extensions)
  * [View extensions](#view-extensions)
    * [View managers extensions](#view-managers-extensions)
  * [Imperial Extensions](#imperial-extensions)
  * [System Extensions](#system-extensions)
<!-- TOC -->

## Element extensions

**FindParameter** extension finds a parameter in the instance or symbol by identifier.
For instances that do not have such a parameter, this method will find and return it at the element type.
This method combines all API methods for getting a parameter into one, such as `get_Parameter`, `LookupParameter`, `GetParameter`.

```csharp
var parameter = element.FindParameter(ParameterTypeId.AllModelUrl);
var parameter = element.FindParameter(BuiltInParameter.ALL_MODEL_URL);
var parameter = element.FindParameter("URL");
```

### Element transform extensions

**Copy** extension copies an element and places the copy at a location indicated by a given transformation.

```csharp
element.Copy(1, 1, 0);
element.Copy(new XYZ(1, 1, 0));
```

**Mirror** extension creates a mirrored copy of an element about a given plane.

```csharp
element.Mirror(plane);
```

**Move** extension moves the element by the specified vector.

```csharp
element.Move(1, 1, 0);
element.Move(new XYZ(1, 1, 0));
```

**Rotate** extension rotates an element about the given axis and angle.

```csharp
element.Rotate(axis, angle);
```

**CanBeMirrored** extension determines whether element can be mirrored.

```csharp
var canRotate = element.CanBeMirrored();
```

**CanBeMirrored** extension determines whether element can be mirrored.

```csharp
var canRotate = element.CanBeMirrored();
```

### Element association extensions
 
**IsAnalyticalElement** extension returns true if the element is an analytical element.

```csharp
var isAnalytical = element.IsAnalyticalElement();
```

**IsPhysicalElement** extension returns true if the element is a physical element.

```csharp
var isPhysical = element.IsPhysicalElement();
```

### Element worksharing extensions
 
**GetCheckoutStatus** extension gets the ownership status of an element.

```csharp
var status = element.GetCheckoutStatus();
```

**GetCheckoutStatus** extension gets the ownership status and outputs the owner of an element.

```csharp
var status = element.GetCheckoutStatus(out var owner);
```

**GetCheckoutStatus** extension gets worksharing information about an element to display in an in-canvas tooltip.

```csharp
var info = element.GetWorksharingTooltipInfo();
```

**GetModelUpdatesStatus** extension gets the status of a single element in the central model.

```csharp
var status = element.GetModelUpdatesStatus();
```

### Element schema extensions

**SaveEntity** extension stores data in the element. Existing data is overwritten.

```csharp
document.ProjectInformation.SaveEntity(schema, "data", "schemaField");
door.SaveEntity(schema, "white", "doorColorField");
```

**LoadEntity** extension retrieves the value stored in the schema from the element.

```csharp
var data = document.ProjectInformation.LoadEntity<string>(schema, "schemaField");
var color = door.LoadEntity<string>(schema, "doorColorField");
```

## ElementId extensions

**ToElement** extension retrieves the element associated with the specified ElementId.

```csharp
Element element = wallId.ToElement(document);
Wall wall = wallId.ToElement<Wall>(document);
```

**ToElements** extension retrieves a collection of elements associated with the specified ElementIds.

```csharp
IList<Element> element = wallIds.ToElements(document);
IList<Wall> element = wallIds.ToElements<Wall>(document);
```

To improve the database access performance, it is not guaranteed that the elements will be retrieved in the original order,
if you need the same order, use the `ToOrderedElements` extension.

**ToOrderedElements** extension retrieves the elements associated with the specified ElementIds in their original order.

```csharp
IList<Element> element = wallIds.ToOrderedElements(document);
IList<Wall> element = wallIds.ToOrderedElements<Wall>(document);
```

The elements will be retrieved in the same order as the original ElementIds collection.

**AreEquals** extension checks if an ID matches BuiltInСategory or BuiltInParameter.

```csharp
categoryId.AreEquals(BuiltInCategory.OST_Walls);
parameterId.AreEquals(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);
```

### ElementId transform extensions

**CanMirrorElements** extension determines whether elements can be mirrored.

```csharp
var canMirror = elementIds.CanMirrorElements(document);
```

**MirrorElements** extension mirrors a set of elements about a given plane.

```csharp
var elements = elementIds.MirrorElements(document, plane, mirrorCopies: true);
```

**MoveElements** extension moves a set of elements by a given transformation.

```csharp
elementIds.MoveElements(document, new XYZ(1, 1, 1));
```

**RotateElements** extension rotates a set of elements about the given axis and angle.

```csharp
elementIds.RotateElements(document, axis, angle: 3.14);
```

**CopyElements** extension copies a set of elements from source view to destination view.

```csharp
var copy = elementIds.CopyElements(source, destination);
var copy = elementIds.CopyElements(source, destination, transform, options);
```

**CopyElements** extension copies a set of elements and places the copies at a location indicated by a given translation.

```csharp
var copy = elementIds.CopyElements(document, new XYZ(1, 1, 1));
```

## Application extensions
### Ribbon Extensions

[Revit API Ribbon controls Guidelines](https://help.autodesk.com/view/RVT/2025/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Ribbon_Panels_and_Controls_html)

**CreatePanel** extension creates or retrieves an existing panel in the "Add-ins" tab of the Revit ribbon.

If a panel with the specified name already exists within the tab, it will return that panel; otherwise, a new one will be created.

Adding a panel also supports built-in tabs. To add a panel to the built-in Revit tab, specify the panel **ID** or **Name** as the `tabName` parameter

```csharp
var panel = application.CreatePanel("Panel name");
var panel = application.CreatePanel("Panel name", "Tab name");
```

![regularControls](https://github.com/user-attachments/assets/c5d202e0-0c16-4c84-b183-b09582676b05)

**RemovePanel** extension removes RibbonPanel from the Revit ribbon.

```csharp
var textBox = panel.RemovePanel();
```

**AddPushButton** extension adds a PushButton to the ribbon.

```csharp
var button = panel.AddPushButton<Command>("Button text");
var button = pullDownButton.AddPushButton<Command>("Button text");
```

**AddPullDownButton** extension adds a PullDownButton to the ribbon.

```csharp
var button = panel.AddPullDownButton("Button text");
```

**AddSplitButton** extension adds a SplitButton to the ribbon.

```csharp
var button = panel.AddSplitButton("Button text");
```

**AddRadioButtonGroup** extension adds a RadioButtonGroup to the ribbon.

```csharp
var radioGroup = panel.AddRadioButtonGroup();
```

**AddComboBox** extension adds a ComboBox to the ribbon.

```csharp
var comboBox = panel.AddComboBox();
```

**AddTextBox** extension adds a TextBox to the ribbon.

```csharp
var textBox = panel.AddTextBox();
```

**SetBackground** extension sets the panel background color.

```csharp
panel.SetBackground("Red");
panel.SetBackground("#FF0000");
panel.SetBackground("#AAFF0000");
panel.SetBackground(Colors.Red);
panel.SetBackground(Brushes.Red);
panel.SetBackground(new LinearGradientBrush(
[
    new GradientStop(Colors.White, 0),
    new GradientStop(Colors.Red, 1)
], 45));
```

**SetTitleBarBackground** extension sets the panel title bar background color.

```csharp
panel.SetTitleBarBackground("Blue");
panel.SetTitleBarBackground("#0000FF");
panel.SetTitleBarBackground("#AA0000FF");
panel.SetTitleBarBackground(Colors.Blue);
panel.SetTitleBarBackground(Brushes.Blue);
panel.SetTitleBarBackground(new LinearGradientBrush(
[
    new GradientStop(Colors.White, 0),
    new GradientStop(Colors.Blue, 1)
], 45));
```

**SetSlideOutPanelBackground** extension sets the slide-out panel background color for the target panel.

```csharp
panel.SetSlideOutBackground("Green");
panel.SetSlideOutBackground("#00FF00");
panel.SetSlideOutBackground("#AA00FF00");
panel.SetSlideOutBackground(Colors.Green);
panel.SetSlideOutBackground(Brushes.Green);
panel.SetSlideOutBackground(new LinearGradientBrush(
[
    new GradientStop(Colors.White, 0),
    new GradientStop(Colors.Green, 1)
], 45));
```

**AddStackPanel** extension adds a vertical stack panel to the Ribbon panel.

```csharp
var stackPanel = panel.AddStackPanel();
```

By default, the StackPanel accommodates one to three elements vertically. 
If the added items exceed the maximum threshold, they will be automatically added to a new column.

These 5 items will create 2 vertical panels, one will contain 3 items and the other 2 items:

```csharp
var stackPanel = panel.AddStackPanel();
stackPanel.AddPushButton<StartupCommand>("Execute");
stackPanel.AddPullDownButton("Execute");
stackPanel.AddSplitButton("Execute");
stackPanel.AddLabel("Items:");
stackPanel.AddComboBox();
stackPanel.AddTextBox();
```

![verticalStack](https://github.com/user-attachments/assets/3cef1e86-89a3-4f9c-8a06-b7661c6f428f)

**SetImage** extension adds an image to the RibbonButton.

```csharp
button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16.png");
button.SetImage("https://example.com/RibbonIcon16.png");
button.SetImage("C:/Pictures/RibbonIcon16.png");
```

**SetLargeImage** extension adds a large image to the RibbonButton.

```csharp
button.SetLargeImage("/RevitAddIn;component/Resources/Icons/RibbonIcon32.png");
button.SetLargeImage("https://example.com/RibbonIcon32.png");
button.SetLargeImage("C:/Pictures/RibbonIcon32.png");
```

Starting with Revit 2024 **SetImage** and **SetLargeImage** extensions support Light and Dark UI themes.

When the provided image name contains "light" or "dark" (case-insensitive), the extensions automatically modify the URI to match the current UI theme. 
For example:

```csharp
button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16-Light.png");
button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16_light.png");

// in the Dark Revit theme will be converted to:
button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16-Dark.png");
button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16_dark.png");
```

**SetToolTip** extension sets the tooltip text for the RibbonItem.

```csharp
button.SetToolTip("Tooltip);
```

**SetLongDescription** extension sets the extended tooltip description for the RibbonItem.

```csharp
button.SetLongDescription("Description);
```

**SetAvailabilityController** extension specifies the class that decides the availability of PushButton.

```csharp
pushButton.SetAvailabilityController<CommandController>();
```

**AddShortcuts** extension adds keyboard shortcuts to the PushButton.

```csharp
pushButton.AddShortcuts("RE");
pushButton.AddShortcuts("RE#NP");
pushButton.AddShortcuts("RE", "NP");
pushButton.AddShortcuts(["RE", "NP"]);
pushButton.AddShortcuts(new List<string>() {"RE", "NP"});
```

The method design is intended to add only the default shortcut assignment, and does not override the user's settings if they decide to change it.

### ContextMenu Extensions

**ConfigureContextMenu** extension registers an action used to configure a Context menu.

```csharp
application.ConfigureContextMenu(menu =>
{
    menu.AddMenuItem<Command>("Menu title");
    menu.AddMenuItem<Command>("Menu title")
        .SetAvailabilityController<Controller>()
        .SetToolTip("Description");
});
```

You can also specify your own context menu title. By default, Revit uses the Application name

```csharp
application.ConfigureContextMenu("Title", menu =>
{
    menu.AddMenuItem<Command>("Menu title");
});
```

**AddMenuItem** extension adds a menu item to the Context Menu.

```csharp
menu.AddMenuItem<Command>("Menu title");
```

**AddSeparator** extension adds a separator to the Context Menu.

```csharp
menu.AddSeparator();
```

**AddSubMenu** extension adds a sub menu to the Context Menu.

```csharp
var subMenu = new ContextMenu();
subMenu.AddMenuItem<Command>("Menu title");
subMenu.AddMenuItem<Command>("Menu title");

menu.AddSubMenu("Sub menu title", subMenu);
```

**SetAvailabilityController** extension specifies the class type that decides the availability of menu item.

```csharp
menuItem.SetAvailabilityController<Controller>()
```

## Document extensions

**GetProfileSymbols** extension gets the profile Family Symbols of the document.

```csharp
var symbols = document.GetProfileSymbols(ProfileFamilyUsage.Any, oneCurveLoopOnly: true);
```

**RelinquishOwnership** extension gets the profile Family Symbols of the document.

```csharp
var items = document.RelinquishOwnership(relinquishOptions, transactOptions);
```

### Document managers extensions

**GetTemporaryGraphicsManager** extension gets a TemporaryGraphicsManager reference of the document.

```csharp
var manager = document.GetTemporaryGraphicsManager();
```

**GetAnalyticalToPhysicalAssociationManager** extension gets a AnalyticalToPhysicalAssociationManager reference of the document.

```csharp
var manager = document.GetAnalyticalToPhysicalAssociationManager();
```

**GetLightGroupManager** extension creates a light group manager object from the given document.

```csharp
var manager = document.GetLightGroupManager();
```

## Geometry extensions

**Distance** extension returns distance between two lines. The lines are considered endless.

```csharp
var line1 = Line.CreateBound(new XYZ(0,0,1), new XYZ(1,1,1));
var line2 = Line.CreateBound(new XYZ(1,2,2), new XYZ(1,2,2));
var distance = line1.Distance(line2);
```

**Contains** extension determines whether the specified point is contained within this BoundingBox.

```csharp
var point = new XYZ(1,1,1);
var isContains = boundingBox.Contains(point);
```

**Contains** extension determines whether the specified point is contained within this BoundingBox.
Set strict mode if the point needs to be fully on the inside of the source. 
A point coinciding with the box border will be considered outside.

```csharp
var point = new XYZ(1,1,1);
var isContains = boundingBox.Contains(point, strict:true);
```

**Contains** extension determines whether one BoundingBoxXYZ contains another BoundingBoxXYZ.

```csharp
var boundingBox2 = new BoundingBoxXYZ();
var isContains = boundingBox1.Contains(boundingBox2);
```

**Contains** extension determines whether one BoundingBoxXYZ contains another BoundingBoxXYZ.
Set strict mode if the box needs to be fully on the inside of the source.
Coincident boxes will be considered outside.

```csharp
var boundingBox2 = new BoundingBoxXYZ();
var isContains = boundingBox1.Contains(boundingBox2, strict:true);
```

**Overlaps** extension determines whether this BoundingBox overlaps with another BoundingBox.

```csharp
var boundingBox2 = new BoundingBoxXYZ();
var isContains = boundingBox1.Overlaps(boundingBox2);
```

**ComputeCentroid** extension computes the geometric center point of the bounding box.

```csharp
var center = boundingBox.ComputeCentroid();
```

**ComputeVertices** extension retrieves the coordinates of the eight vertices that define the bounding box.

```csharp
var vertices = boundingBox.ComputeVertices();
```

**ComputeVolume** extension calculates the volume enclosed by the bounding box.

```csharp
var volume = boundingBox.ComputeVolume();
```

**ComputeSurfaceArea** extension calculates the total surface area of the bounding box.

```csharp
var area = boundingBox.ComputeSurfaceArea();
```

**SetCoordinateX** extension creates an instance of a curve with a new X coordinate.

```csharp
var newLine = line.SetCoordinateX(1);
var newArc = arc.SetCoordinateX(1);
```

**SetCoordinateY** extension creates an instance of a curve with a new Y coordinate.

```csharp
var newLine = line.SetCoordinateY(1);
var newArc = arc.SetCoordinateY(1);
```

**SetCoordinateZ** extension creates an instance of a curve with a new coordinate.

```csharp
var newLine = line.SetCoordinateZ(1);
var newArc = arc.SetCoordinateZ(1);
```

### Element geometry extensions

**JoinGeometry** extension creates clean joins between two elements that share a common face.

```csharp
element1.JoinGeometry(element2);
```

**UnjoinGeometry** extension removes a join between two elements.

```csharp
element1.UnjoinGeometry(element2);
```

**AreElementsJoined** extension determines whether two elements are joined.

```csharp
var areJoined = element1.AreElementsJoined(element2);
```

**GetJoinedElements** extension returns all elements joined to given element.

```csharp
var elements = element1.GetJoinedElements();
```

**SwitchJoinOrder** extension reverses the order in which two elements are joined.

```csharp
element1.SwitchJoinOrder();
```

**IsCuttingElementInJoin** extension determines whether the first of two joined elements is cutting the second element.

```csharp
var isCutting = element1.IsCuttingElementInJoin(element2);
```

## Parameters extensions

**AsBool** extension provides access to the boolean value within the parameter.

```csharp
bool value = element.FindParameter("IsClosed").AsBool();
```

**AsColor** extension provides access to the Color within the parameter.

```csharp
Color value = element.FindParameter("Door color").AsColor();
```

**AsElement** extension provides access to the Element within the parameter.

```csharp
Element value = element.FindParameter("Door material").AsElement();
Material value = element.FindParameter("Door material").AsElement<Material>();
```

**Set** extension sets the parameter to a new value.

```csharp
parameter.Set(true);
parameter.Set(new Color(66, 69, 96);
```

**IsBuiltInParameter** extension checks whether a Parameter identifies a built-in parameter.

```csharp
var isBuiltIn = parameter.IsBuiltInParameter();
```

### Document global parameters extensions

**FindGlobalParameter** extension finds whether a global parameter with the given name exists in the input document.

```csharp
var parameter = document.FindGlobalParameter(name);
```

**GetAllGlobalParameters** extension returns all global parameters available in the given document.

```csharp
var parameters = document.GetAllGlobalParameters();
```

**GetGlobalParametersOrdered** extension returns all global parameters in an ordered array.

```csharp
var parameters = document.GetGlobalParametersOrdered();
```

**SortGlobalParameters** extension sorts global parameters in the desired order.

```csharp
document.SortGlobalParameters(ParametersOrder.Ascending);
```

**MoveGlobalParameterUpOrder** extension moves given global parameter Up in the current order.

```csharp
var isMoved = globalParameter.MoveUpOrder();
```

**MoveGlobalParameterDownOrder** extension moves given global parameter Down in the current order.

```csharp
var isMoved = globalParameter.MoveDownOrder();
```

**IsUniqueGlobalParameterName** extension tests whether a name is unique among existing global parameters of a given document.

```csharp
var isUnique = document.IsUniqueGlobalParameterName(name);
```

**IsValidGlobalParameter** extension tests whether an ElementId is of a global parameter in the given document.

```csharp
var isValid = document.IsValidGlobalParameter(parameterId);
```

**AreGlobalParametersAllowed** extension tests whether global parameters are allowed in the given document.

```csharp
var isAllowed = document.AreGlobalParametersAllowed();
```


## FilteredElementCollector extensions

This set of extensions encapsulates all the work of searching for elements in the Revit database.

**GetElements** a generic method which constructs a new FilteredElementCollector that will search and filter the set of elements in a document.
Filter criteria are not applied to the method.

```csharp
var elements = document.GetElements().WhereElementIsViewIndependent().ToElements();
var elements = document.GetElements(elementIds).WhereElementIsViewIndependent.ToElements();
var elements = document.GetElements(viewId).ToElements();
```

The remaining methods contain a ready implementation of the collector, with filters applied:

```csharp
var elements = document.GetInstances();
var elements = document.GetInstances(new ElementParameterFilter());
var elements = document.GetInstances([elementParameterFilter, logicalFilter]);

var elements = document.GetInstances(BuiltInCategory.OST_Walls);
var elements = document.GetInstances(BuiltInCategory.OST_Walls, new ElementParameterFilter());
var elements = document.GetInstances(BuiltInCategory.OST_Walls, [elementParameterFilter, logicalFilter]);    

var elements = document.EnumerateInstances();
var elements = document.EnumerateInstances(new ElementParameterFilter());
var elements = document.EnumerateInstances([elementParameterFilter, logicalFilter]);

var elements = document.EnumerateInstances(BuiltInCategory.OST_Walls);
var elements = document.EnumerateInstances(BuiltInCategory.OST_Walls, new ElementParameterFilter());
var elements = document.EnumerateInstances(BuiltInCategory.OST_Walls, [elementParameterFilter, logicalFilter]);   

var elements = document.EnumerateInstances<Wall>();
var elements = document.EnumerateInstances<Wall>(new ElementParameterFilter());
var elements = document.EnumerateInstances<Wall>(new [elementParameterFilter, logicalFilter]);

var elements = document.EnumerateInstances<Wall>(BuiltInCategory.OST_Walls);
var elements = document.EnumerateInstances<Wall>(BuiltInCategory.OST_Walls, new ElementParameterFilter());
var elements = document.EnumerateInstances<Wall>(BuiltInCategory.OST_Walls, [elementParameterFilter, logicalFilter]);   
```

The same overloads exist for InstanceIds, Type, TypeIds:

```csharp
var types = document.GetTypes();
var types = document.GetTypeIds();
var types = document.GetInstanceIds();
var types = document.EnumerateTypes();
var types = document.EnumerateTypeIds();
var types = document.EnumerateInstanceIds();
```

**Remarks**: `Get` methods are faster than `Enumerate` due to RevitApi internal optimizations.
However, enumeration allows for more flexibility in finding elements.

Don't try to call `GetInstances().Select().Tolist()` instead of `EnumerateInstances().Select().Tolist()`, you will degrade performance.

## ForgeTypeId extensions

**IsSpec** extension Checks whether a ForgeTypeId identifies a spec.

```csharp
var isSpec = forgeId.IsSpec();
```

**IsBuiltInGroup** extension checks whether a ForgeTypeId identifies a built-in parameter group.

```csharp
var isGroup = forgeId.IsBuiltInGroup();
```

**IsBuiltInParameter** extension checks whether a ForgeTypeId identifies a built-in parameter.

```csharp
var isBuiltInParameter = forgeId.IsBuiltInParameter();
```

**IsSymbol** extension checks whether a ForgeTypeId identifies a symbol.

```csharp
var isSymbol = symbolTypeId.IsSymbol();
```

**IsUnit** extension checks whether a ForgeTypeId identifies a unit.

```csharp
var isUnit = unitTypeId.IsUnit();
```

**IsValidDataType** extension returns true if the given ForgeTypeId identifies a valid parameter data type.

```csharp
var isValid = forgeId.IsValidDataType();
```

**IsValidUnit** extension checks whether a unit is valid for a given measurable spec.

```csharp
var isValid = specTypeId.IsValidUnit(unitTypeId);
```

**IsMeasurableSpec** extension checks whether a ForgeTypeId identifies a spec associated with units of measurement.

```csharp
var isMeasurable = specTypeId.IsMeasurableSpec(unitTypeId);
```

**GetBuiltInParameter** extension gets the BuiltInParameter value corresponding to built-in parameter identified by the given ForgeTypeId.

```csharp
var builtInParameter = forgeId.GetBuiltInParameter();
```

**GetParameterTypeId** extension gets the ForgeTypeId identifying the built-in parameter corresponding to the given BuiltInParameter value.

```csharp
var forgeId = builtInParameter.GetParameterTypeId();
```

**GetDiscipline** extension gets the discipline for a given measurable spec.

```csharp
var disciplineId = specTypeId.GetDiscipline();
```

**GetValidUnits** extension gets the identifiers of all valid units for a given measurable spec.

```csharp
var unitIds = specTypeId.GetValidUnits();
```

**GetTypeCatalogStringForSpec** extension gets the string used in type catalogs to identify a given measurable spec.

```csharp
var catalog = specTypeId.GetTypeCatalogStringForSpec();
```

**GetTypeCatalogStringForUnit** extension gets the string used in type catalogs to identify a given unit.

```csharp
var catalog = unitTypeId.GetTypeCatalogStringForUnit();
```

**DownloadCompanyName** extension downloads the name of the given parameter's owning account and records it in the given document.
If the owning account's name is already recorded in the given document, this method returns the name without downloading it again.

```csharp
var name = forgeId.DownloadCompanyName(document);
```

**DownloadParameterOptions** extension retrieves settings associated with the given parameter from the Parameters Service.

```csharp
var options = forgeId.DownloadParameterOptions(document);
```

**DownloadParameter** extension creates a shared parameter element in the given document according to a parameter definition downloaded from the Parameters Service.

```csharp
var sharedParameter = forgeId.DownloadParameter(document, options);
```

## Unit Extensions

**FromMillimeters** extension converts millimeters to internal Revit number format (feet).

```csharp
var value = 69d.FromMillimeters(); // 0.226
```

**ToMillimeters** extension converts a Revit internal format value (feet) to millimeters.

```csharp
var value = 69d.ToMillimeters(); // 21031
```

**FromMeters** extension converts meters to internal Revit number format (feet).

```csharp
var value = 69d.FromMeters(); // 226.377
```

**ToMeters** extension converts a Revit internal format value (feet) to meters.

```csharp
var value = 69d.ToMeters(); // 21.031
```

**FromInches** extension converts inches to internal Revit number format (feet).

```csharp
var value = 69d.FromInches(); // 5.750
```

**ToInches** extension converts a Revit internal format value (feet) to inches.

```csharp
var value = 69d.ToInches(); // 827.999
```

**FromDegrees** extension converts degrees to internal Revit number format (radians).

```csharp
var value = 69d.FromDegrees(); // 1.204
```

**ToDegrees** extension converts a Revit internal format value (radians) to degrees.

```csharp
var value = 69d.ToDegrees(); // 3953
```

**FromUnit(UnitTypeId)** extension converts the specified unit type to internal Revit number format.

```csharp
var value = 69d.FromUnit(UnitTypeId.Celsius); // 342.15
```

**ToUnit(UnitTypeId)** extension converts a Revit internal format value to the specified unit type.

```csharp
var value = 69d.ToUnit(UnitTypeId.Celsius); // -204.15
```

**FormatUnit** extension formats a number with units into a string.

```csharp
var value = document.GetUnits().FormatUnit(SpecTypeId.Length, 69, false); // 21031
var value = document.GetUnits().FormatUnit(SpecTypeId.Length, 69, false, new FormatValueOptions {AppendUnitSymbol = true}); // 21031 mm
```

**TryParse** extension parses a formatted string into a number with units if possible.

```csharp
var isParsec = document.GetUnits().TryParse(SpecTypeId.Length, "21031 mm", out var value); // 69
```

## Label Extensions

**ToLabel** extension convert Enum to user-visible name.

```csharp
var label = BuiltInCategory.OST_Walls.ToLabel(); // "Walls"
var label = BuiltInParameter.WALL_TOP_OFFSET.ToLabel(); // "Top Offset"
var label = BuiltInParameter.WALL_TOP_OFFSET.ToLabel(LanguageType.Russian); // "Смещение сверху"
var label = BuiltInParameterGroup.PG_LENGTH.ToLabel(); // "Length"
var label = DisplayUnitType.DUT_KILOWATTS.ToLabel(); // "Kilowatts"
```

**ToLabel** extension convert ForgeTypeId to user-visible name.

```csharp
var label = ParameterType.Length.ToLabel(); // "Length"
var label = DisciplineTypeId.Hvac.ToLabel(); // "HVAC"
var label = GroupTypeId.Geometry.ToLabel(); // "Dimensions"
var label = ParameterTypeId.DoorCost.ToLabel(); // "Cost"
var label = SpecTypeId.SheetLength.ToLabel(); // "Sheet Length"
var label = SymbolTypeId.Hour.ToLabel(); // "h"
var label = UnitTypeId.Hertz.ToLabel(); // "Hertz"
```

**ToDisciplineLabel** extension convert ForgeTypeId to user-visible name a discipline.

```csharp
var label = DisciplineTypeId.Hvac.ToDisciplineLabel(); // "HVAC"
```

**ToGroupLabel** extension converts ForgeTypeId to user-visible name for a built-in parameter group.

```csharp
var label = GroupTypeId.Geometry.ToGroupLabel(); // "Dimensions"
```

**ToParameterLabel** extension converts ForgeTypeId to user-visible name for a built-in parameter.

```csharp
var label = ParameterTypeId.DoorCost.ToParameterLabel(); // "Cost"
```

**ToSpecLabel** extension converts ForgeTypeId to user-visible name for a spec.

```csharp
var label = SpecTypeId.SheetLength.ToSpecLabel(); // "Sheet Length"
```

**ToSymbolLabel** extension convert ForgeTypeId to user-visible name for a symbol.

```csharp
var label = SymbolTypeId.Hour.ToSymbolLabel(); // "h"
```

**ToUnitLabel** extension converts ForgeTypeId to user-visible name for a unit.

```csharp
var label = UnitTypeId.Hertz.ToUnitLabel(); // "Hertz"
```

## Color extensions

**ToHex** extension returns a hexadecimal representation of a color.

```csharp
var hex = color.ToHex();
```

**ToHexInteger** extension returns a hexadecimal integer representation of a color.

```csharp
var hexInteger = color.ToHexInteger();
```

**ToRgb** extension returns an RGB representation of a color.

```csharp
var rgb = color.ToRgb();
```

**ToHsl** extension returns a HSL representation of a color.

```csharp
var hsl = color.ToHsl();
```

**ToHsv** extension returns a HSV representation of a color.

```csharp
var hsv = color.ToHsv();
```

**ToCmyk** extension returns a CMYK representation of a color.

```csharp
var cmyk = color.ToCmyk();
```

**ToHsb** extension returns a HSB representation of a color.

```csharp
var hsb = color.ToHsb();
```

**ToHsi** extension returns a HSI representation of a color.

```csharp
var hsi = color.ToHsi();
```

**ToHwb** extension returns a HWB representation of a color.

```csharp
var hwb = color.ToHwb();
```

**ToNCol** extension returns a NCol representation of a color.

```csharp
var ncol = color.ToNCol();
```

**ToCielab** extension returns a Cielab representation of a color.

```csharp
var cielab = color.ToCielab();
```

**ToCieXyz** extension returns a CieXyz representation of a color.

```csharp
var xyz = color.ToCieXyz();
```

**ToFloat** extension returns a Float representation of a color.

```csharp
var float = color.ToFloat();
```

**ToDecimal** extension returns a Decimal representation of a color.

```csharp
var decimal = color.ToDecimal();
```

## Family extensions

**CanConvertToFaceHostBased** extension indicates whether the family can be converted to face host based.

```csharp
var canConvert = family.CanConvertToFaceHostBased();
```

**ConvertToFaceHostBased** extension converts a family to be face host based.

```csharp
family.ConvertToFaceHostBased();
```

## HostObject extensions

**GetBottomFaces** extension returns the bottom faces for the host object.

```csharp
floor.GetBottomFaces();
```

**GetTopFaces** extension returns the top faces for the host object.

```csharp
floor.GetTopFaces();
```

**GetSideFaces** extension returns the major side faces for the host object.

```csharp
wall.GetSideFaces(ShellLayerType.Interior);
```

## Plumbing extensions

**ConnectPipePlaceholdersAtElbow** extension connects placeholders that looks like elbow connection.

```csharp
var isConnected = connector1.ConnectPipePlaceholdersAtElbow(connector2);
```

**ConnectPipePlaceholdersAtTee** extension connects three placeholders that looks like Tee connection.

```csharp
var isConnected = connector1.ConnectPipePlaceholdersAtTee(connector2, connector3);
```

**ConnectPipePlaceholdersAtCross** extension connects placeholders that looks like Cross connection.

```csharp
var isConnected = connector1.ConnectPipePlaceholdersAtCross(connector2, connector3, connector4);
```

**PlaceCapOnOpenEnds** extension places caps on the open connectors of the pipe curve.

```csharp
pipe.PlaceCapOnOpenEnds();
pipe.PlaceCapOnOpenEnds(typeId);
```

**HasOpenConnector** extension checks if there is open piping connector for the given pipe curve.

```csharp
var hasOpenConnector = pipe.HasOpenConnector();
```

**BreakCurve** extension breaks the pipe curve into two parts at the given position.

```csharp
var pipeCurve = pipe.BreakCurve(new XYZ(1, 1, 1));
```

## Solid extensions

**Clone** extension creates a new Solid, which is a copy of the input Solid.

```csharp
var clone = solid.Clone();
```

**CreateTransformed** extension creates a new Solid which is the transformation of the input Solid.

```csharp
var transformed = solid.CreateTransformed(Transform.CreateRotationAtPoint());
var transformed = solid.CreateTransformed(Transform.CreateReflection());
```

**SplitVolumes** extension splits a solid geometry into several solids.

```csharp
var solids = solid.SplitVolumes();
```

**IsValidForTessellation** extension tests if the input solid or shell is valid for tessellation.

```csharp
var isValid = solid.IsValidForTessellation();
```

**TessellateSolidOrShell** extension facets (i.e., triangulates) a solid or an open shell.

```csharp
var tesselation = solid.TessellateSolidOrShell();
```

**FindAllEdgeEndPointsAtVertex** extension finds all EdgeEndPoints at a vertex identified by the input EdgeEndPoint.

```csharp
var point = edgeEndPoint.FindAllEdgeEndPointsAtVertex();
```

### Element solid cut extensions

**GetCuttingSolids** extension gets all the solids which cut the input element.

```csharp
var solids = element.GetCuttingSolids();
```

**GetSolidsBeingCut** extension gets all the solids which are cut by the input element.

```csharp
var solids = element.GetSolidsBeingCut();
```

**IsAllowedForSolidCut** extension validates that the element is eligible for a solid-solid cut.

```csharp
var isAllowed = element.IsAllowedForSolidCut();
```

**IsElementFromAppropriateContext** extension validates that the element is from an appropriate document.

```csharp
var fromContext = element.IsElementFromAppropriateContext();
```

**CanElementCutElement** extension verifies if the cutting element can add a solid cut to the target element.

```csharp
var canCut = element1.CanElementCutElement(element2, out var reason);
```

**CutExistsBetweenElements** extension checks that if there is a solid-solid cut between two elements.

```csharp
var isCutExists = element1.CutExistsBetweenElements(element2, out var isFirstCuts);
```

**AddCutBetweenSolids** extension adds a solid-solid cut for the two elements.

```csharp
element1.AddCutBetweenSolids(element2);
```

**RemoveCutBetweenSolids** extension removes the solid-solid cut between the two elements if it exists.

```csharp
element1.RemoveCutBetweenSolids(element2);
```

**SplitFacesOfCuttingSolid** extension removes the solid-solid cut between the two elements if it exists.

```csharp
element1.SplitFacesOfCuttingSolid(element2);
```

## View extensions

**GetTransformFromViewToView** extension returns a transformation that is applied to elements when copying from one view to another view.

```csharp
var transform = view1.GetTransformFromViewToView(view2);
```

### View managers extensions

**CreateSpatialFieldManager** extension creates SpatialField for the given view.

```csharp
var manager = view.CreateSpatialFieldManager(numberOfMeasurements: 69);
```

**GetSpatialFieldManager** extension retrieves SpatialField manager for the given view.

```csharp
var manager = view.GetSpatialFieldManager();
```

## Imperial Extensions

**ToFraction** extension converts a double value representing a measurement in feet to its string representation in the Imperial system, expressed as feet, inches, and fractional inches.

```csharp
var imperial = 0.0123.ToFraction(); // 1/8"
var imperial = 12.011.ToFraction(); // 12'-1/8"
var imperial = 25.222.ToFraction(); // 25'-2 1/8"
var imperial = 0.0123.ToFraction(8); // 1/8"
var imperial = 12.006.ToFraction(16); // 12'-1/16"
var imperial = 25.222.ToFraction(32); // 25'-2 21/32"
```

**FromFraction** extension converts a string representation of a measurement in the Imperial system (feet and inches) to a double value.

```csharp
var value = "17/64\"".FromFraction(); // 0.092
var value = "1'1.75".FromFraction(); // 1.145
var value = "-69'-69\"".FromFraction(); // -74.75
var value = "2'-1 15/64\"".FromFraction(); // 2.102
```

**TryFromFraction** extension converts the textual representation of the Imperial system number to number.

```csharp
var converted = "1'".TryFromFraction(out var value); // true
var converted = "69\"".TryFromFraction(out var value); // true
var converted = "-2'-1 15/64\"".TryFromFraction(out var value); // true
var converted = "value".TryFromFraction(out var value); // false
```

## System Extensions

**Cast<T>** extension casts an object to the specified type.

```csharp
var width = element.Cast<Wall>().Width;
var location = element.Location.Cast<LocationCurve>().Curve;
var faces = element.Cast<HostObject>().GetSideFaces();
```

**Round** extension rounds the value to the specified precision or 1e-9 precision specified in Revit Api.

```csharp
var rounded = 6.56170000000000000000000001.Round(); // 6.5617
var rounded = 6.56170000000000000000000001.Round(0); // 7
```

**IsAlmostEqual** extension compares two numbers within specified precision or 1e-9 precision specified in Revit Api.

```csharp
var isRqual = 6.56170000000000000000000001.IsAlmostEqual(6.5617); // true
var isEqual = 6.56170000000000000000000001.IsAlmostEqual(6.6, 1e-1); // true
```

**IsNullOrEmpty** extension indicates whether the specified string is null or an empty string.

```csharp
var isEmpty = "".IsNullOrEmpty(); // true
var isEmpty = null.IsNullOrEmpty(); // true
```

**IsNullOrWhiteSpace** extension indicates whether a specified string is null, empty, or consists only of white-space characters.

```csharp
var isEmpty = " ".IsNullOrWhiteSpace(); // true
var isEmpty = null.IsNullOrWhiteSpace(); // true
```

**AppendPath** extension combines paths.

```csharp
var path = "C:/Folder".AppendPath("AddIn"); // C:/Folder/AddIn
var path = "C:/Folder".AppendPath("AddIn", "file.txt"); // C:/Folder/AddIn/file.txt
```

**Contains** indicating whether a specified substring occurs within this string with `StringComparison` support.

Available only for .NET Framework builds. .NET Core has a built-in support for this method.

```csharp
var isContains = "Revit extensions".Contains("Revit", StringComparison.OrdinalIgnoreCase); // true
var isContains = "Revit extensions".Contains("revit", StringComparison.OrdinalIgnoreCase); // true
var isContains = "Revit extensions".Contains("REVIT", StringComparison.OrdinalIgnoreCase); // true
var isContains = "Revit extensions".Contains("invalid", StringComparison.OrdinalIgnoreCase); // false
```

**Show** extension opens a window and returns without waiting for the newly opened window to close.
Sets the owner of a child window. Applicable for modeless windows to be attached to Revit.

```csharp
new RevitAddinView.Show(uiApplication.MainWindowHandle)
```