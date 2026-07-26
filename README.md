<p align="center">
    <picture>
        <source media="(prefers-color-scheme: dark)" width="750" srcset="https://github.com/Nice3point/RevitExtensions/assets/20504884/d605eb83-74a7-4a47-9db8-cb0daced374e">
        <img alt="Nice3point.Revit.Extensions" width="750" src="https://github.com/Nice3point/RevitExtensions/assets/20504884/a1772d7d-38d4-4a9b-9985-1d83b8cbea8d">
    </picture>
</p>

## Improve your experience with Revit API

[![Nuget](https://img.shields.io/nuget/vpre/Nice3point.Revit.Extensions?style=for-the-badge)](https://www.nuget.org/packages/Nice3point.Revit.Extensions)
[![Downloads](https://img.shields.io/nuget/dt/Nice3point.Revit.Extensions?style=for-the-badge)](https://www.nuget.org/packages/Nice3point.Revit.Extensions)
[![Last Commit](https://img.shields.io/github/last-commit/Nice3point/RevitExtensions/develop?style=for-the-badge)](https://github.com/Nice3point/RevitExtensions/commits/develop)

Extensions make working with the Revit API much easier and more intuitive.
They add helpful methods that make your code cleaner, easier to understand, and simpler to maintain. Generics, nullable, everything here.

Instead of writing complex utility functions, you can use these extensions to write code in a natural, fluent way that focuses on what you actually want to do:

```csharp
new ElementId(123469)
    .ToElement<Wall>()
    .FindParameter(ParameterTypeId.WallBaseOffset)
    .AsDouble()
    .ToMillimeters()
    .Round();
```

## Installation

You can install the Extensions as a [NuGet package](https://www.nuget.org/packages/Nice3point.Revit.Extensions).

The packages are compiled for specific versions of Revit. To support different versions of libraries in one project, use the `RevitVersion` property:

```xml
<PackageReference Include="Nice3point.Revit.Extensions" Version="$(RevitVersion).*"/>
```

Package included by default in [Revit Templates](https://github.com/Nice3point/RevitTemplates).

## Table of contents

<!-- TOC -->
* [Element](#element)
    * [Transformation](#transformation)
    * [Joins and cuts](#joins-and-cuts)
    * [Extensible storage](#extensible-storage)
* [Application extensions](#application-extensions)
* [UIApplication extensions](#uiapplication-extensions)
    * [Ribbon](#ribbon)
    * [Context menu](#context-menu)
* [Document](#document)
    * [Global parameters](#global-parameters)
    * [Managers and services](#managers-and-services)
* [Parameters](#parameters)
    * [BuiltInParameter](#builtinparameter)
    * [Filtering](#filtering)
* [Category](#category)
    * [BuiltInCategory](#builtincategory)
* [Geometry](#geometry)
    * [Bounding box](#bounding-box)
    * [Curve](#curve)
    * [Solid](#solid)
    * [Tessellation](#tessellation)
* [View](#view)
    * [SSE point](#sse-point)
    * [Spatial field](#spatial-field)
* [Unit](#unit)
    * [Formatting](#formatting)
* [ForgeTypeId](#forgetypeid)
* [Label](#label)
* [Color](#color)
* [FilteredElementCollector](#filteredelementcollector)
* [Families and modeling](#families-and-modeling)
    * [Family](#family)
    * [FamilySymbol](#familysymbol)
    * [FamilyInstance](#familyinstance)
    * [Form](#form)
    * [HostObject](#hostobject)
    * [Wall](#wall)
    * [Adaptive components](#adaptive-components)
    * [Annotation](#annotation)
    * [Detail](#detail)
    * [Parts](#parts)
    * [Assembly](#assembly)
    * [Mass](#mass)
* [Disciplines](#disciplines)
    * [MEP](#mep)
        * [Pipe](#pipe)
        * [Duct](#duct)
        * [Connector](#connector)
        * [Fabrication](#fabrication)
    * [Structure](#structure)
        * [Rebar](#rebar)
        * [Structural framing](#structural-framing)
        * [Structural sections](#structural-sections)
    * [Analytical](#analytical)
* [Model access and interoperability](#model-access-and-interoperability)
    * [ModelPath](#modelpath)
    * [Worksharing](#worksharing)
    * [Coordination model](#coordination-model)
    * [Export](#export)
    * [External files](#external-files)
    * [External resources](#external-resources)
    * [Point clouds](#point-clouds)
* [DirectContext3D](#directcontext3d)
* [System](#system)
<!-- TOC -->

## Element

**ToElement** retrieves the element associated with the specified ElementId.

```csharp
Element element = wallId.ToElement(document);
Wall wall = wallId.ToElement<Wall>(document);
```

**ToElements** retrieves a collection of elements associated with the specified ElementIds.

```csharp
IList<Element> elements = wallIds.ToElements(document);
IList<Wall> walls = wallIds.ToElements<Wall>(document);
```

To improve database access performance, it is not guaranteed that the elements will be retrieved in the original order.
If you need the same order, use the `ToOrderedElements` extension.

**ToOrderedElements** retrieves the elements associated with the specified ElementIds in their original order.

```csharp
IList<Element> elements = wallIds.ToOrderedElements(document);
IList<Wall> walls = wallIds.ToOrderedElements<Wall>(document);
```

**CanBeDeleted** indicates whether an element can be deleted.

```csharp
var canDelete = element.CanBeDeleted;
var canDelete = elementId.CanBeDeleted(document);
```

**FindParameter** finds a parameter in the instance or symbol by identifier.
For instances that do not have such a parameter, this method will find and return it at the element type.
This method combines all API methods for getting a parameter into one, such as `get_Parameter`, `LookupParameter`, `GetParameter`.

```csharp
var parameter = element.FindParameter(ParameterTypeId.AllModelUrl);
var parameter = element.FindParameter(BuiltInParameter.ALL_MODEL_URL);
var parameter = element.FindParameter(guid);
var parameter = element.FindParameter("URL");
```

### Transformation

**Copy** copies an element and places the copy at a location indicated by a given transformation.

```csharp
element.Copy(1, 1, 0);
element.Copy(new XYZ(1, 1, 0));
elementId.Copy(document, 1, 1, 0);
elementId.Copy(document, new XYZ(1, 1, 0));
```

**Mirror** creates a mirrored copy of an element about a given plane.

```csharp
element.Mirror(plane);
elementId.Mirror(document, plane);
```

**Move** moves the element by the specified vector.

```csharp
element.Move(1, 1, 0);
element.Move(new XYZ(1, 1, 0));
elementId.Move(document, 1, 1, 0);
elementId.Move(document, new XYZ(1, 1, 0));
```

**Rotate** rotates an element about the given axis and angle.

```csharp
element.Rotate(axis, angle);
elementId.Rotate(document, axis, angle);
```

**CanBeMirrored** determines whether an element or a set of elements can be mirrored.

```csharp
var canMirror = element.CanBeMirrored;
var canMirror = elementId.CanBeMirrored(document);
var canMirror = elementIds.CanBeMirrored(document);
```

**MirrorElements** mirrors a set of elements about a given plane.

```csharp
var elements = elementIds.MirrorElements(document, plane, mirrorCopies: true);
```

**MoveElements** moves a set of elements by a given transformation.

```csharp
elementIds.MoveElements(document, new XYZ(1, 1, 1));
```

**RotateElements** rotates a set of elements about the given axis and angle.

```csharp
elementIds.RotateElements(document, axis, angle: 3.14);
```

**CopyElements** copies elements within a document, between views, or between documents.

```csharp
var viewCopy = elementIds.CopyElements(sourceView, destinationView);
var viewCopy = elementIds.CopyElements(sourceView, destinationView, transform, options);

var translatedCopy = elementIds.CopyElements(document, new XYZ(1, 1, 1));

var documentCopy = elementIds.CopyElements(sourceDocument, destinationDocument);
var documentCopy = elementIds.CopyElements(sourceDocument, destinationDocument, transform, options);
```

### Joins and cuts

**JoinGeometry** creates clean joins between two elements that share a common face.

```csharp
element1.JoinGeometry(element2);
```

**UnjoinGeometry** removes a join between two elements.

```csharp
element1.UnjoinGeometry(element2);
```

**AreElementsJoined** determines whether two elements are joined.

```csharp
var areJoined = element1.AreElementsJoined(element2);
```

**GetJoinedElements** returns all elements joined to a given element.

```csharp
var elements = element1.GetJoinedElements();
```

**SwitchJoinOrder** reverses the order in which two elements are joined.

```csharp
element1.SwitchJoinOrder(element2);
```

**IsCuttingElementInJoin** determines whether the first of two joined elements is cutting the second.

```csharp
var isCutting = element1.IsCuttingElementInJoin(element2);
```

**CanBeCutWithVoid** indicates if the element can be cut by an instance with unattached voids.

```csharp
var canBeCut = element.CanBeCutWithVoid;
```

**GetCuttingVoidInstances** returns ids of the instances with unattached voids cutting the element.

```csharp
var ids = element.GetCuttingVoidInstances();
```

**AddInstanceVoidCut** adds a cut to an element using the unattached voids inside a cutting instance.

```csharp
element.AddInstanceVoidCut(cuttingInstance);
```

**RemoveInstanceVoidCut** removes a cut applied to the element by a cutting instance.

```csharp
element.RemoveInstanceVoidCut(cuttingInstance);
```

**IsInstanceVoidCutExists** checks whether the instance is cutting the element.

```csharp
var exists = element.IsInstanceVoidCutExists(cuttingInstance);
```

### Extensible storage

**SaveEntity** stores data in the element. Existing data is overwritten.

```csharp
document.ProjectInformation.SaveEntity(schema, "data", "schemaField");
door.SaveEntity(schema, "white", "doorColorField");
```

**LoadEntity** retrieves the value stored in the schema from the element.

```csharp
var data = document.ProjectInformation.LoadEntity<string>(schema, "schemaField");
var color = door.LoadEntity<string>(schema, "doorColorField");
```

## Application extensions

**AsControlledApplication** creates a `ControlledApplication` from the current `Application` instance.
This allows using APIs that require a `ControlledApplication` outside of the `IExternalDBApplication.OnStartup` context.

```csharp
var controlledApplication = application.AsControlledApplication();
```

**IsDgnExportAvailable** and other optional functionality extensions expose whether optional Revit modules are installed.

```csharp
var available = application.IsDgnExportAvailable;
var available = application.IsDgnImportLinkAvailable;
var available = application.IsDwfExportAvailable;
var available = application.IsDwgExportAvailable;
var available = application.IsDwgImportLinkAvailable;
var available = application.IsDxfExportAvailable;
var available = application.IsFbxExportAvailable;
var available = application.IsGraphicsAvailable;
var available = application.IsIfcAvailable;
var available = application.IsNavisworksExporterAvailable;
var available = application.IsSatImportLinkAvailable;
var available = application.IsShapeImporterAvailable;
var available = application.IsSkpImportLinkAvailable;
var available = application.Is3DmImportLinkAvailable;
var available = application.IsAxmImportLinkAvailable;
var available = application.IsObjImportLinkAvailable;
var available = application.IsStlImportLinkAvailable;
var available = application.IsStepImportLinkAvailable;
var available = application.IsMaterialLibraryAvailable;
```

**GetAllCloudRegions** gets all available regions supported by the cloud service.

```csharp
var regions = application.GetAllCloudRegions();
```

## UIApplication extensions

**AsControlledApplication** creates a `UIControlledApplication` from the current `UIApplication` instance.
This allows using APIs that require a `UIControlledApplication`, such as ribbon customization, outside of the `IExternalApplication.OnStartup` context.

```csharp
var controlledApplication = uiApplication.AsControlledApplication();
```

### Ribbon

[Revit API Ribbon controls Guidelines](https://help.autodesk.com/view/RVT/2025/ENU/?guid=Revit_API_Revit_API_Developers_Guide_Introduction_Add_In_Integration_Ribbon_Panels_and_Controls_html)

**CreatePanel** creates or retrieves an existing panel in the "Add-ins" tab of the Revit ribbon.

If a panel with the specified name already exists within the tab, it will return that panel; otherwise, a new one will be created.

Adding a panel also supports built-in tabs. To add a panel to the built-in Revit tab, specify the panel **ID** or **Name** as the `tabName` parameter.

```csharp
var panel = application.CreatePanel("Panel name");
var panel = application.CreatePanel("Panel name", "Tab name");
```

![regularControls](https://github.com/user-attachments/assets/c5d202e0-0c16-4c84-b183-b09582676b05)

**RemovePanel** removes a RibbonPanel from the Revit ribbon.

```csharp
panel.RemovePanel();
```

**AddPushButton** adds a PushButton to the ribbon.

```csharp
var button = panel.AddPushButton<Command>("Button text");
var button = pullDownButton.AddPushButton<Command>("Button text");
```

**AddPullDownButton** adds a PullDownButton to the ribbon.

```csharp
var button = panel.AddPullDownButton("Button text");
```

**AddSplitButton** adds a SplitButton to the ribbon.

```csharp
var button = panel.AddSplitButton("Button text");
```

**AddRadioButtonGroup** adds a RadioButtonGroup to the ribbon.

```csharp
var radioGroup = panel.AddRadioButtonGroup();
```

**AddComboBox** adds a ComboBox to the ribbon.

```csharp
var comboBox = panel.AddComboBox();
```

**AddTextBox** adds a TextBox to the ribbon.

```csharp
var textBox = panel.AddTextBox();
```

**SetImage** adds an image to the RibbonButton.

```csharp
button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16.png");
button.SetImage("https://example.com/RibbonIcon16.png");
button.SetImage("C:/Pictures/RibbonIcon16.png");
```

**SetLargeImage** adds a large image to the RibbonButton.

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

**SetToolTip** sets the tooltip text for the RibbonItem.

```csharp
button.SetToolTip("Tooltip");
```

**SetLongDescription** sets the extended tooltip description for the RibbonItem.

```csharp
button.SetLongDescription("Description");
```

**SetAvailabilityController** specifies the class that decides the availability of a PushButton.

```csharp
pushButton.SetAvailabilityController<CommandController>();
```

**AddShortcuts** adds keyboard shortcuts to the PushButton.

```csharp
pushButton.AddShortcuts("RE");
pushButton.AddShortcuts("RE#NP");
pushButton.AddShortcuts("RE", "NP");
pushButton.AddShortcuts(["RE", "NP"]);
pushButton.AddShortcuts(new List<string>() {"RE", "NP"});
```

The method is intended to add only the default shortcut assignment, and does not override the user's settings if they decide to change it.

**TryAddShortcuts** attempts to add keyboard shortcuts to the PushButton.
Shortcuts are added only if they do not conflict with existing commands.

```csharp
var isAdded = pushButton.TryAddShortcuts("RE");
var isAdded = pushButton.TryAddShortcuts("RE#NP");
var isAdded = pushButton.TryAddShortcuts("RE", "NP");
var isAdded = pushButton.TryAddShortcuts(["RE", "NP"]);
var isAdded = pushButton.TryAddShortcuts(new List<string>() {"RE", "NP"});
```

**AddStackPanel** adds a vertical stack panel to the Ribbon panel.

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

**SetBackground** sets the panel background color.

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

**SetTitleBarBackground** sets the panel title bar background color.

```csharp
panel.SetTitleBarBackground("Blue");
panel.SetTitleBarBackground("#0000FF");
panel.SetTitleBarBackground(Colors.Blue);
panel.SetTitleBarBackground(Brushes.Blue);
```

**SetSlideOutPanelBackground** sets the slide-out panel background color.

```csharp
panel.SetSlideOutPanelBackground("Green");
panel.SetSlideOutPanelBackground("#00FF00");
panel.SetSlideOutPanelBackground(Colors.Green);
panel.SetSlideOutPanelBackground(Brushes.Green);
```

### Context menu

**ConfigureContextMenu** registers an action used to configure a Context menu.

```csharp
application.ConfigureContextMenu(menu =>
{
    menu.AddMenuItem<Command>("Menu title");
    menu.AddMenuItem<Command>("Menu title")
        .SetAvailabilityController<Controller>()
        .SetToolTip("Description");
});
```

You can also specify your own context menu title. By default, Revit uses the Application name.

```csharp
application.ConfigureContextMenu("Title", menu =>
{
    menu.AddMenuItem<Command>("Menu title");
});
```

**AddMenuItem** adds a menu item to the Context Menu.

```csharp
menu.AddMenuItem<Command>("Menu title");
```

**AddSeparator** adds a separator to the Context Menu.

```csharp
menu.AddSeparator();
```

**AddSubMenu** adds a sub menu to the Context Menu.

```csharp
var subMenu = new ContextMenu();
subMenu.AddMenuItem<Command>("Menu title");
subMenu.AddMenuItem<Command>("Menu title");

menu.AddSubMenu("Sub menu title", subMenu);
```

**SetAvailabilityController** specifies the class type that decides the availability of a menu item.

```csharp
menuItem.SetAvailabilityController<Controller>();
```

## Document

**Version** gets the DocumentVersion that corresponds to a document.

```csharp
var version = document.Version;
```

**IsValidVersionGuid** checks whether the GUID is valid for the given document.

```csharp
var isValid = document.IsValidVersionGuid(versionGuid);
```

**CheckAllFamilies** checks that all families loaded in the host document have their content documents.

```csharp
var isValid = document.CheckAllFamilies(out var corruptFamilyIds);
```

**CheckAllFamiliesSlow** checks integrity of content documents of all families loaded in the host document. This check is slow as it involves traversal of all content documents.

```csharp
var isValid = document.CheckAllFamiliesSlow(out var corruptFamilyIds);
```

### Global parameters

**FindGlobalParameter** finds a global parameter with the given name in the document.

```csharp
var parameter = document.FindGlobalParameter(name);
```

**GetAllGlobalParameters** returns all global parameters available in the given document.

```csharp
var parameters = document.GetAllGlobalParameters();
```

**GetGlobalParametersOrdered** returns all global parameters in an ordered array.

```csharp
var parameters = document.GetGlobalParametersOrdered();
```

**SortGlobalParameters** sorts global parameters in the desired order.

```csharp
document.SortGlobalParameters(ParametersOrder.Ascending);
```

**MoveUpOrder** moves the given global parameter Up in the current order.

```csharp
var isMoved = globalParameter.MoveUpOrder();
var isMoved = globalParameterId.MoveGlobalParameterUpOrder(document);
```

**MoveDownOrder** moves the given global parameter Down in the current order.

```csharp
var isMoved = globalParameter.MoveDownOrder();
var isMoved = globalParameterId.MoveGlobalParameterDownOrder(document);
```

**IsUniqueGlobalParameterName** tests whether a name is unique among existing global parameters of a given document.

```csharp
var isUnique = document.IsUniqueGlobalParameterName(name);
```

**IsValidGlobalParameter** tests whether an ElementId is of a global parameter in the given document.

```csharp
var isValid = parameterId.IsValidGlobalParameter(document);
```

**AreGlobalParametersAllowed** tests whether global parameters are allowed in the given document.

```csharp
var isAllowed = document.AreGlobalParametersAllowed;
```

### Managers and services

**GetTemporaryGraphicsManager** gets a TemporaryGraphicsManager reference of the document.

```csharp
var manager = document.GetTemporaryGraphicsManager();
```

**GetAnalyticalToPhysicalAssociationManager** gets an AnalyticalToPhysicalAssociationManager reference of the document.

```csharp
var manager = document.GetAnalyticalToPhysicalAssociationManager();
```

**GetLightGroupManager** creates a light group manager object from the given document.

```csharp
var manager = document.GetLightGroupManager();
```

## Parameters

**AsBool** provides access to the boolean value within the parameter.

```csharp
bool value = element.FindParameter("IsClosed").AsBool();
```

**AsColor** provides access to the Color within the parameter.

```csharp
Color value = element.FindParameter("Door color").AsColor();
```

**AsElement** provides access to the Element within the parameter.

```csharp
Element value = element.FindParameter("Door material").AsElement();
Material value = element.FindParameter("Door material").AsElement<Material>();
```

**Set** sets the parameter to a new value.

```csharp
parameter.Set(true);
parameter.Set(new Color(66, 69, 96));
```

**SearchExternalDefinition** searches a DefinitionFile for the ExternalDefinition corresponding to a parameter in a document.

```csharp
var definition = definitionFile.SearchExternalDefinition(document, parameterId);
var definition = definitionFile.SearchExternalDefinition(parameter);
```

### BuiltInParameter

**IsBuiltInParameter** checks whether a ForgeTypeId identifies a built-in parameter.

```csharp
var isBuiltIn = forgeId.IsBuiltInParameter;
var isBuiltIn = parameter.IsBuiltInParameter;
```

**ToParameter** converts a BuiltInParameter into a Revit Parameter object.

```csharp
var parameter = BuiltInParameter.WALL_TOP_OFFSET.ToParameter(document);
```

**ToElementId** creates an ElementId from a BuiltInParameter value.

```csharp
var elementId = BuiltInParameter.WALL_TOP_OFFSET.ToElementId();
```

**IsParameter** checks if an ElementId matches a BuiltInParameter.

```csharp
var isParameter = parameterId.IsParameter(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);
```

### Filtering

**GetAllFilterableCategories** returns the set of categories that may be used in a ParameterFilterElement.

```csharp
var categories = ParameterFilterElement.GetAllFilterableCategories();
```

**GetFilterableParametersInCommon** returns the filterable parameters common to the given categories.

```csharp
var parameters = ParameterFilterElement.GetFilterableParametersInCommon(document, categories);
```

**IsParameterApplicable** determines whether the element supports the given parameter.

```csharp
var applicable = element.IsParameterApplicable(parameterId);
var applicable = element.IsParameterApplicable(parameter);
```

**RemoveUnfilterableCategories** removes from the given set the categories that are not filterable.

```csharp
var filtered = categories.RemoveUnfilterableCategories();
```

**GetInapplicableParameters** returns the parameters that are not among the set of filterable parameters common to the given categories.

```csharp
var invalid = ParameterFilterElement.GetInapplicableParameters(document, categories, parameters);
```

## Category

### BuiltInCategory

**ToCategory** converts a BuiltInCategory into a Revit Category object.

```csharp
var category = BuiltInCategory.OST_Walls.ToCategory(document);
```

**ToElementId** creates an ElementId from a BuiltInCategory value.

```csharp
var elementId = BuiltInCategory.OST_Walls.ToElementId();
```

**IsCategory** checks if an ElementId matches a BuiltInCategory.

```csharp
var isWalls = categoryId.IsCategory(BuiltInCategory.OST_Walls);
```

## Geometry

**Distance** returns the distance between two lines. The lines are considered endless.

```csharp
var line1 = Line.CreateBound(new XYZ(0, 0, 1), new XYZ(1, 1, 1));
var line2 = Line.CreateBound(new XYZ(1, 2, 2), new XYZ(1, 2, 2));
var distance = line1.Distance(line2);
```

### Bounding box

**ComputeCentroid** computes the geometric center point of the bounding box.

```csharp
var center = boundingBox.ComputeCentroid();
```

**ComputeVertices** retrieves the coordinates of the eight vertices that define the bounding box.

```csharp
var vertices = boundingBox.ComputeVertices();
```

**ComputeVolume** calculates the volume enclosed by the bounding box.

```csharp
var volume = boundingBox.ComputeVolume();
```

**ComputeSurfaceArea** calculates the total surface area of the bounding box.

```csharp
var area = boundingBox.ComputeSurfaceArea();
```

**Contains** determines whether a bounding box contains a point or another bounding box.

```csharp
var containsPoint = boundingBox.Contains(new XYZ(1, 1, 1));
var containsPoint = boundingBox.Contains(new XYZ(1, 1, 1), strict: true);

var containsBox = boundingBox1.Contains(boundingBox2);
var containsBox = boundingBox1.Contains(boundingBox2, strict: true);
```

**Overlaps** determines whether this BoundingBox overlaps with another BoundingBox.

```csharp
var overlaps = boundingBox1.Overlaps(boundingBox2);
```

### Curve

**SetCoordinateX** creates an instance of a curve with a new X coordinate.

```csharp
var newLine = line.SetCoordinateX(1);
var newArc = arc.SetCoordinateX(1);
```

**SetCoordinateY** creates an instance of a curve with a new Y coordinate.

```csharp
var newLine = line.SetCoordinateY(1);
var newArc = arc.SetCoordinateY(1);
```

**SetCoordinateZ** creates an instance of a curve with a new Z coordinate.

```csharp
var newLine = line.SetCoordinateZ(1);
var newArc = arc.SetCoordinateZ(1);
```

**SetX** creates an instance of a point with a new X coordinate.

```csharp
var newPoint = point.SetX(1);
```

**SetY** creates an instance of a point with a new Y coordinate.

```csharp
var newPoint = point.SetY(1);
```

**SetZ** creates an instance of a point with a new Z coordinate.

```csharp
var newPoint = point.SetZ(1);
```

**GetHostFace** gets the host face to which the CurveElement is added.

```csharp
var reference = curveElement.GetHostFace();
```

**GetProjectionType** gets the projection type of the CurveElement.

```csharp
var type = curveElement.GetProjectionType();
```

**SetProjectionType** sets the projection type of the CurveElement.

```csharp
curveElement.SetProjectionType(CurveProjectionType.FromTopDown);
```

**GetSketchOnSurface** gets the relationship between the CurveElement and face.

```csharp
var sketchOnSurface = curveElement.GetSketchOnSurface();
```

**SetSketchOnSurface** sets the relationship between the CurveElement and face.

```csharp
curveElement.SetSketchOnSurface(sketchOnSurface: true);
```

**CreateArcThroughPoints** creates an arc through the given reference points.

```csharp
var arc = CurveElement.CreateArcThroughPoints(document, startPoint, endPoint, interiorPoint);
```

**AddCurvesToFaceRegion** adds CurveElements to one or more FaceRegions.

```csharp
CurveElement.AddCurvesToFaceRegion(document, curveElementIds);
```

**CreateRectangle** creates a rectangle on a face or sketchplane for two given diagonal points.

```csharp
CurveElement.CreateRectangle(document, startPoint, endPoint, projectionType, boundaryReferenceLines, boundaryCurvesFollowSurface, out createdCurvesIds, out createdCornersIds);
```

**ValidateForFaceRegions** validates that the input CurveElements can define FaceRegions.

```csharp
var isValid = CurveElement.ValidateForFaceRegions(document, curveElemIds);
```

**IsValidHorizontalBoundary** identifies whether the given curve loops compose a valid horizontal boundary.

```csharp
var isValid = CurveLoop.IsValidHorizontalBoundary(curveLoops);
```

**IsValidBoundaryOnSketchPlane** indicates if the given curve loops compose a valid boundary on the sketch plane.

```csharp
var isValid = CurveLoop.IsValidBoundaryOnSketchPlane(sketchPlane, curveLoops);
```

**IsValidBoundaryOnView** indicates if the given curve loops compose a valid boundary on the view's detail sketch plane.

```csharp
var isValid = CurveLoop.IsValidBoundaryOnView(document, viewId, curveLoops);
```

**GetFaceRegions** gets the FaceRegions in the existing face.

```csharp
var regions = reference.GetFaceRegions(document);
```

### Solid

**IsNonSolid** makes sure that the input geometry object is not a Solid.

```csharp
var isNonSolid = geometry.IsNonSolid;
```

**IsSolid** makes sure that the input geometry object is a Solid.

```csharp
var isSolid = geometry.IsSolid;
```

**Clone** creates a new Solid which is a copy of the input Solid.

```csharp
var clone = solid.Clone();
```

**CreateTransformed** creates a new Solid which is the transformation of the input Solid.

```csharp
var transformed = solid.CreateTransformed(Transform.CreateRotationAtPoint());
```

**CreateBlendGeometry** creates a solid by blending two closed curve loops.

```csharp
var solid = Solid.CreateBlendGeometry(firstLoop, secondLoop);
var solid = Solid.CreateBlendGeometry(firstLoop, secondLoop, vertexPairs);
```

**CreateExtrusionGeometry** creates a solid by linearly extruding one or more closed coplanar curve loops.

```csharp
var solid = Solid.CreateExtrusionGeometry(profileLoops, extrusionDirection, extrusionDistance);
```

**CreateRevolvedGeometry** creates a solid of revolution by revolving a set of closed curve loops around an axis.

```csharp
var solid = Solid.CreateRevolvedGeometry(coordinateFrame, profileLoops, startAngle, endAngle);
```

**CreateSweptGeometry** creates a solid by sweeping one or more closed coplanar curve loops along a path.

```csharp
var solid = Solid.CreateSweptGeometry(sweepPath, pathAttachmentCurveIndex, pathAttachmentParameter, profileLoops);
```

**CreateSweptBlendGeometry** creates a solid by simultaneously sweeping and blending two or more closed planar curve loops along a single curve.

```csharp
var solid = Solid.CreateSweptBlendGeometry(pathCurve, pathParameters, profileLoops, vertexPairs);
```

**CreateFixedReferenceSweptGeometry** creates a solid by sweeping curve loops along a path while keeping the profile plane oriented to a fixed direction.

```csharp
var solid = Solid.CreateFixedReferenceSweptGeometry(sweepPath, pathAttachmentCurveIndex, pathAttachmentParameter, profileLoops, fixedReferenceDirection);
```

**CreateLoftGeometry** creates a solid or open shell geometry by lofting between a sequence of curve loops.

```csharp
var solid = Solid.CreateLoftGeometry(profileLoops, solidOptions);
```

**ComputeIsGeometricallyClosed** computes whether the Solid is geometrically closed to within Revit's tolerances.

```csharp
var closed = solid.ComputeIsGeometricallyClosed(profileLoops, solidOptions);
```

**ComputeIsTopologicallyClosed** computes whether the Solid is is topologically closed.

```csharp
var closed = solid.ComputeIsTopologicallyClosed(profileLoops, solidOptions);
```

**SplitVolumes** splits a solid geometry into several separate solids.

```csharp
var solids = solid.SplitVolumes();
```

**CutWithHalfSpace** creates a new Solid which is the intersection of the input Solid with the half-space on the positive side of the given Plane.

```csharp
var result = solid.CutWithHalfSpace(plane);
```

**CutWithHalfSpaceModifyingOriginalSolid** modifies the input Solid preserving only the volume on the positive side of the given Plane.

```csharp
solid.CutWithHalfSpaceModifyingOriginalSolid(plane);
```

**ExecuteBooleanOperation** performs a boolean geometric operation between two solids and returns a new solid.

```csharp
var result = solid.ExecuteBooleanOperation(other, BooleanOperationsType.Union);
var result = solid.ExecuteBooleanOperation(other, BooleanOperationsType.Intersect);
var result = solid.ExecuteBooleanOperation(other, BooleanOperationsType.Difference);
```

**ExecuteBooleanOperationModifyingOriginalSolid** performs a boolean operation modifying the original solid.

```csharp
solid.ExecuteBooleanOperationModifyingOriginalSolid(other, BooleanOperationsType.Union);
```

**GetCuttingSolids** gets all the solids which cut the input element.

```csharp
var solids = element.GetCuttingSolids();
```

**GetSolidsBeingCut** gets all the solids which are cut by the input element.

```csharp
var solids = element.GetSolidsBeingCut();
```

**IsAllowedForSolidCut** validates that the element is eligible for a solid-solid cut.

```csharp
var isAllowed = element.IsAllowedForSolidCut;
```

**IsElementFromAppropriateContext** validates that the element is from an appropriate document.

```csharp
var fromContext = element.IsElementFromAppropriateContext;
```

**CanElementCutElement** verifies if the cutting element can add a solid cut to the target element.

```csharp
var canCut = element1.CanElementCutElement(element2, out var reason);
```

**CutExistsBetweenElements** checks if there is a solid-solid cut between two elements.

```csharp
var exists = element1.CutExistsBetweenElements(element2, out var firstCutsSecond);
```

**AddCutBetweenSolids** adds a solid-solid cut for the two elements.

```csharp
element1.AddCutBetweenSolids(element2);
element1.AddCutBetweenSolids(element2, splitFacesOfCuttingSolid: true);
```

**RemoveCutBetweenSolids** removes the solid-solid cut between the two elements.

```csharp
element1.RemoveCutBetweenSolids(element2);
```

**SplitFacesOfCuttingSolid** causes the faces of the cutting element to be split or unsplit.

```csharp
element1.SplitFacesOfCuttingSolid(element2, split: true);
```

### Tessellation

**IsValidForTessellation** tests if the input solid or shell is valid for tessellation.

```csharp
var isValid = solid.IsValidForTessellation;
```

**TessellateSolidOrShell** facets (triangulates) a solid or an open shell.

```csharp
var tessellation = solid.TessellateSolidOrShell(tessellationControls);
```

**ConvertTrianglesToQuads** replaces pairs of adjacent, coplanar triangles by quadrilaterals.

```csharp
var facets = triangulation.ConvertTrianglesToQuads();
```

**FindAllEdgeEndPointsAtVertex** finds all EdgeEndPoints at a vertex identified by the input EdgeEndPoint.

```csharp
var points = edgeEndPoint.FindAllEdgeEndPointsAtVertex();
```

## View

**GetTransformFromViewToView** returns a transformation that is applied to elements when copying from one view to another view.

```csharp
var transform = view1.GetTransformFromViewToView(view2);
```

**GetReferencedViewId** gets the id of the view referenced by a reference view.

```csharp
var viewId = element.GetReferencedViewId();
var viewId = elementId.GetReferencedViewId(document);
```

**ChangeReferencedView** changes a reference view to refer to a different View.

```csharp
element.ChangeReferencedView(desiredViewId);
elementId.ChangeReferencedView(document, desiredViewId);
```

### SSE point

**GetSsePointVisibility** gets the SSE point visibility for the given category.

```csharp
var isVisible = category.GetSsePointVisibility(document);
```

**SetSsePointVisibility** sets the SSE point visibility for the given category.

```csharp
category.SetSsePointVisibility(document, isVisible: true);
```

### Spatial field

**CreateSpatialFieldManager** creates a SpatialFieldManager for the given view.

```csharp
var manager = view.CreateSpatialFieldManager(numberOfMeasurements: 69);
```

**GetSpatialFieldManager** retrieves the SpatialFieldManager for the given view.

```csharp
var manager = view.GetSpatialFieldManager();
```

## Unit

**FromMillimeters** converts millimeters to internal Revit format (feet).

```csharp
var value = 69d.FromMillimeters(); // 0.226
```

**ToMillimeters** converts a Revit internal format value (feet) to millimeters.

```csharp
var value = 69d.ToMillimeters(); // 21031
```

**FromMeters** converts meters to internal Revit format (feet).

```csharp
var value = 69d.FromMeters(); // 226.377
```

**ToMeters** converts a Revit internal format value (feet) to meters.

```csharp
var value = 69d.ToMeters(); // 21.031
```

**FromInches** converts inches to internal Revit format (feet).

```csharp
var value = 69d.FromInches(); // 5.750
```

**ToInches** converts a Revit internal format value (feet) to inches.

```csharp
var value = 69d.ToInches(); // 827.999
```

**FromDegrees** converts degrees to internal Revit format (radians).

```csharp
var value = 69d.FromDegrees(); // 1.204
```

**ToDegrees** converts a Revit internal format value (radians) to degrees.

```csharp
var value = 69d.ToDegrees(); // 3953
```

**FromUnit** converts the specified unit type to internal Revit format.

```csharp
var value = 69d.FromUnit(UnitTypeId.Celsius); // 342.15
```

**ToUnit** converts a Revit internal format value to the specified unit type.

```csharp
var value = 69d.ToUnit(UnitTypeId.Celsius); // -204.15
```

### Formatting

**Format** formats a number with units into a string.

```csharp
var value = document.GetUnits().Format(SpecTypeId.Length, 69, forEditing: false); // 21031
var value = document.GetUnits().Format(SpecTypeId.Length, 69, forEditing: false, new FormatValueOptions {AppendUnitSymbol = true}); // 21031 mm
```

**TryParse** parses a formatted string into a number with units if possible.

```csharp
var isParsed = document.GetUnits().TryParse(SpecTypeId.Length, "21031 mm", out var value); // 69
```

## ForgeTypeId

**IsBuiltInGroup** checks whether a ForgeTypeId identifies a built-in parameter group.

```csharp
var isGroup = forgeId.IsBuiltInGroup;
```

**IsSpec** checks whether a ForgeTypeId identifies a spec.

```csharp
var isSpec = forgeId.IsSpec;
```

**IsValidDataType** returns true if the given ForgeTypeId identifies a valid parameter data type.

```csharp
var isValid = forgeId.IsValidDataType;
```

**IsSymbol** checks whether a ForgeTypeId identifies a symbol.

```csharp
var isSymbol = symbolTypeId.IsSymbol;
```

**IsUnit** checks whether a ForgeTypeId identifies a unit.

```csharp
var isUnit = unitTypeId.IsUnit;
```

**IsMeasurableSpec** checks whether a ForgeTypeId identifies a spec associated with units of measurement.

```csharp
var isMeasurable = specTypeId.IsMeasurableSpec;
```

**IsValidUnit** checks whether a unit is valid for a given measurable spec.

```csharp
var isValid = specTypeId.IsValidUnit(unitTypeId);
```

**GetBuiltInParameter** gets the BuiltInParameter value corresponding to a built-in parameter identified by the given ForgeTypeId.

```csharp
var builtInParameter = forgeId.GetBuiltInParameter();
```

**GetBuiltInParameterGroupTypeId** gets the parameter group identifier corresponding to the given built-in parameter identifier.

```csharp
var builtInParameterGroup = forgeId.GetBuiltInParameterGroupTypeId();
```

**GetAllBuiltInParameters** gets the identifiers of all built-in parameters.

```csharp
var ids = ForgeTypeId.GetAllBuiltInParameters();
```

**GetAllBuiltInGroups** gets the identifiers of all built-in parameter groups.

```csharp
var ids = ForgeTypeId.GetAllBuiltInGroups();
```

**GetAllSpecs** gets the identifiers of all specs.

```csharp
var ids = ForgeTypeId.GetAllSpecs();
```

**GetAllMeasurableSpecs** gets the identifiers of all available measurable specs.

```csharp
var ids = ForgeTypeId.GetAllMeasurableSpecs();
```

**GetAllDisciplines** gets the identifiers of all available disciplines.

```csharp
var ids = ForgeTypeId.GetAllDisciplines();
```

**GetAllUnits** gets the identifiers of all available units.

```csharp
var ids = ForgeTypeId.GetAllUnits();
```

**GetParameterTypeId** gets the ForgeTypeId identifying the built-in parameter corresponding to the given BuiltInParameter value.

```csharp
var forgeId = builtInParameter.GetParameterTypeId();
```

**GetDiscipline** gets the discipline for a given measurable spec.

```csharp
var disciplineId = specTypeId.GetDiscipline();
```

**GetValidUnits** gets the identifiers of all valid units for a given measurable spec.

```csharp
var unitIds = specTypeId.GetValidUnits();
```

**GetTypeCatalogStringForSpec** gets the string used in type catalogs to identify a given measurable spec.

```csharp
var catalog = specTypeId.GetTypeCatalogStringForSpec();
```

**GetTypeCatalogStringForUnit** gets the string used in type catalogs to identify a given unit.

```csharp
var catalog = unitTypeId.GetTypeCatalogStringForUnit();
```

**DownloadCompanyName** downloads the name of the given parameter's owning account and records it in the given document.

```csharp
var name = forgeId.DownloadCompanyName(document);
var name = forgeId.DownloadCompanyName(document, region);
```

**DownloadParameterOptions** retrieves settings associated with the given parameter from the Parameters Service.

```csharp
var options = forgeId.DownloadParameterOptions();
var options = forgeId.DownloadParameterOptions(region);
```

**DownloadParameter** creates a shared parameter element in the given document according to a parameter definition downloaded from the Parameters Service.

```csharp
var sharedParameter = forgeId.DownloadParameter(document, options);
var sharedParameter = forgeId.DownloadParameter(document, options, region);
```

## Label

**ToLabel** converts enums and type identifiers to user-visible names.

```csharp
var categoryLabel = BuiltInCategory.OST_Walls.ToLabel(); // "Walls"
var parameterLabel = BuiltInParameter.WALL_TOP_OFFSET.ToLabel(); // "Top Offset"
var parameterLabelRu = BuiltInParameter.WALL_TOP_OFFSET.ToLabel(LanguageType.Russian); // "Смещение сверху"
var groupLabel = BuiltInParameterGroup.PG_LENGTH.ToLabel(); // "Length"
var displayUnitLabel = DisplayUnitType.DUT_KILOWATTS.ToLabel(); // "Kilowatts"

var parameterTypeLabel = ParameterType.Length.ToLabel(); // "Length"
var disciplineLabel = DisciplineTypeId.Hvac.ToLabel(); // "HVAC"
var groupTypeLabel = GroupTypeId.Geometry.ToLabel(); // "Dimensions"
var parameterTypeIdLabel = ParameterTypeId.DoorCost.ToLabel(); // "Cost"
var specTypeLabel = SpecTypeId.SheetLength.ToLabel(); // "Sheet Length"
var symbolTypeLabel = SymbolTypeId.Hour.ToLabel(); // "h"
var unitTypeLabel = UnitTypeId.Hertz.ToLabel(); // "Hertz"

var failureLabel = FailureSeverity.DocumentCorruption.ToLabel(); // "Document Corruption"
var sectionShapeLabel = StructuralSectionShape.RectangleParameterized.ToLabel() // "Rectangle Parameterized"
```

**ToDisciplineLabel** converts ForgeTypeId to user-visible name for a discipline.

```csharp
var label = DisciplineTypeId.Hvac.ToDisciplineLabel(); // "HVAC"
```

**ToGroupLabel** converts ForgeTypeId to user-visible name for a built-in parameter group.

```csharp
var label = GroupTypeId.Geometry.ToGroupLabel(); // "Dimensions"
```

**ToParameterLabel** converts ForgeTypeId to user-visible name for a built-in parameter.

```csharp
var label = ParameterTypeId.DoorCost.ToParameterLabel(); // "Cost"
```

**ToSpecLabel** converts ForgeTypeId to user-visible name for a spec.

```csharp
var label = SpecTypeId.SheetLength.ToSpecLabel(); // "Sheet Length"
```

**ToSymbolLabel** converts ForgeTypeId to user-visible name for a symbol.

```csharp
var label = SymbolTypeId.Hour.ToSymbolLabel(); // "h"
```

**ToUnitLabel** converts ForgeTypeId to user-visible name for a unit.

```csharp
var label = UnitTypeId.Hertz.ToUnitLabel(); // "Hertz"
```

## Color

**ToHex** returns a hexadecimal representation of a color.

```csharp
var hex = color.ToHex();
```

**ToHexInteger** returns a hexadecimal integer representation of a color.

```csharp
var hexInteger = color.ToHexInteger();
```

**ToRgb** returns an RGB representation of a color.

```csharp
var rgb = color.ToRgb();
```

**ToHsl** returns a HSL representation of a color.

```csharp
var hsl = color.ToHsl();
```

**ToHsv** returns a HSV representation of a color.

```csharp
var hsv = color.ToHsv();
```

**ToCmyk** returns a CMYK representation of a color.

```csharp
var cmyk = color.ToCmyk();
```

**ToHsb** returns a HSB representation of a color.

```csharp
var hsb = color.ToHsb();
```

**ToHsi** returns a HSI representation of a color.

```csharp
var hsi = color.ToHsi();
```

**ToHwb** returns a HWB representation of a color.

```csharp
var hwb = color.ToHwb();
```

**ToNCol** returns a NCol representation of a color.

```csharp
var ncol = color.ToNCol();
```

**ToCielab** returns a CIE LAB representation of a color.

```csharp
var cielab = color.ToCielab();
```

**ToCieXyz** returns a CIE XYZ representation of a color.

```csharp
var xyz = color.ToCieXyz();
```

**ToFloat** returns a float representation of a color.

```csharp
var floatColor = color.ToFloat();
```

**ToDecimal** returns a decimal representation of a color.

```csharp
var decimalColor = color.ToDecimal();
```

## FilteredElementCollector

**CollectElements** creates a new FilteredElementCollector for the document.

```csharp
var collector = document.CollectElements();
var collector = document.CollectElements(viewId);
var collector = document.CollectElements(view);
var collector = document.CollectElements(elementIds);
```

**OfClass** applies an `ElementClassFilter` to the collector with generic type support.

```csharp
var walls = document.CollectElements()
    .OfClass<Wall>()
    .ToElements();
```

**OfClasses** applies an `ElementMulticlassFilter` to the collector to match elements whose class matches any of the given types.

```csharp
var elements = document.CollectElements()
    .OfClasses(typeof(Wall), typeof(Floor))
    .ToElements();
```

**ExcludingClass** applies an inverted `ElementClassFilter` to match all elements that are not of the given type.

```csharp
var elements = document.CollectElements()
    .ExcludingClass<Wall>()
    .ToElements();
```

**ExcludingClasses** applies an inverted `ElementMulticlassFilter` to match all elements whose class does not match any of the given types.

```csharp
var elements = document.CollectElements()
    .ExcludingClasses(typeof(Wall), typeof(Grid))
    .ToElements();
```

**OfCategories** applies an `ElementMulticategoryFilter` to the collector to match elements belonging to any of the given categories.

```csharp
var elements = document.CollectElements()
    .OfCategories(BuiltInCategory.OST_Walls, BuiltInCategory.OST_Floors)
    .ToElements();
```

**ExcludingCategory** applies an inverted `ElementCategoryFilter` to match all elements not belonging to the given category.

```csharp
var elements = document.CollectElements()
    .ExcludingCategory(BuiltInCategory.OST_Walls)
    .ToElements();
```

**ExcludingCategories** applies an inverted `ElementMulticategoryFilter` to match all elements not belonging to any of the given categories.

```csharp
var elements = document.CollectElements()
    .ExcludingCategories(BuiltInCategory.OST_Walls, BuiltInCategory.OST_Grids)
    .ToElements();
```

**OfElements** applies an `ElementIdSetFilter` to match only elements with the given ids.

```csharp
var elements = document.CollectElements()
    .OfElements(ids)
    .ToElements();
```

**OfCurveElementType** applies a `CurveElementFilter` to match curve elements of the given type.

```csharp
var modelCurves = document.CollectElements()
    .OfCurveElementType(CurveElementType.ModelCurve)
    .ToElements();
```

**ExcludingCurveElementType** applies an inverted `CurveElementFilter` to match all curve elements not of the given type.

```csharp
var elements = document.CollectElements()
    .ExcludingCurveElementType(CurveElementType.DetailCurve)
    .ToElements();
```

**OfStructuralType** applies an `ElementStructuralTypeFilter` to match elements of the given structural type.

```csharp
var nonStructural = document.CollectElements()
    .OfStructuralType(StructuralType.NonStructural)
    .ToElements();
```

**ExcludingStructuralType** applies an inverted `ElementStructuralTypeFilter` to match all elements not of the given structural type.

```csharp
var elements = document.CollectElements()
    .ExcludingStructuralType(StructuralType.NonStructural)
    .ToElements();
```

**Types** applies an `ElementIsElementTypeFilter`. Only element types will pass the collector.

```csharp
var types = document.CollectElements()
    .Types()
    .ToElements();
```

**Instances** applies an inverted `ElementIsElementTypeFilter`. Only element instances will pass the collector.

```csharp
var instances = document.CollectElements()
    .Instances()
    .ToElements();
```

**Rooms** applies a `RoomFilter` to match room elements.

```csharp
var rooms = document.CollectElements()
    .Rooms()
    .ToElements();
```

**RoomTags** applies a `RoomTagFilter` to match room tag elements.

```csharp
var roomTags = document.CollectElements()
    .RoomTags()
    .ToElements();
```

**Areas** applies an `AreaFilter` to match area elements.

```csharp
var areas = document.CollectElements()
    .Areas()
    .ToElements();
```

**AreaTags** applies an `AreaTagFilter` to match area tag elements.

```csharp
var areaTags = document.CollectElements()
    .AreaTags()
    .ToElements();
```

**Spaces** applies a `SpaceFilter` to match space elements.

```csharp
var spaces = document.CollectElements()
    .Spaces()
    .ToElements();
```

**SpaceTags** applies a `SpaceTagFilter` to match space tag elements.

```csharp
var spaceTags = document.CollectElements()
    .SpaceTags()
    .ToElements();
```

**FamilySymbols** applies a `FamilySymbolFilter` to match all family symbols belonging to the given family.

```csharp
var symbols = document.CollectElements()
    .FamilySymbols(family)
    .ToElements();
```

**FamilyInstances** applies a `FamilyInstanceFilter` to match family instances of the given family symbol.

```csharp
var instances = document.CollectElements()
    .FamilyInstances(symbol)
    .ToElements();
```

**IsCurveDriven** applies an `ElementIsCurveDrivenFilter` to match elements that are curve-driven.

```csharp
var curveDriven = document.CollectElements()
    .IsCurveDriven()
    .ToElements();
```

**IsViewIndependent** applies an `ElementIsCurveDrivenFilter` to match elements that are view-independent.

```csharp
var viewIndependent = document.CollectElements()
    .IsViewIndependent()
    .ToElements();
```

**IsPrimaryDesignOptionMember** applies a `PrimaryDesignOptionMemberFilter` to match elements contained in any primary design option of any design option set.

```csharp
var primary = document.CollectElements()
    .IsPrimaryDesignOptionMember()
    .ToElements();
```

**IsNotPrimaryDesignOptionMember** applies an inverted `PrimaryDesignOptionMemberFilter` to match all elements not contained in any primary design option.

```csharp
var nonPrimary = document.CollectElements()
    .IsNotPrimaryDesignOptionMember()
    .ToElements();
```

**OwnedByView** applies an `ElementOwnerViewFilter` to match elements owned by the given view.

```csharp
var viewElements = document.CollectElements(viewId)
    .OwnedByView(view)
    .ToElements();
```

**NotOwnedByView** applies an inverted `ElementOwnerViewFilter` to match all elements not owned by the given view.

```csharp
var nonViewElements = document.CollectElements()
    .NotOwnedByView(view)
    .ToElements();
```

**VisibleInView** applies a `VisibleInViewFilter` to match elements visible in the given view.

```csharp
var visible = document.CollectElements(view.Id)
    .VisibleInView(view)
    .ToElements();
```

**NotVisibleInView** applies an inverted `VisibleInViewFilter` to match all elements not visible in the given view.

```csharp
var notVisible = document.CollectElements(view.Id)
    .NotVisibleInView(view)
    .ToElements();
```

**SelectableInView** applies a `SelectableInViewFilter` to match elements that are selectable in the given view.

```csharp
var selectable = document.CollectElements(viewId)
    .SelectableInView(view)
    .ToElements();
```

**NotSelectableInView** applies an inverted `SelectableInViewFilter` to match all elements that are not selectable in the given view.

```csharp 
var notSelectable = document.CollectElements(viewId)
    .NotSelectableInView(view)
    .ToElements();
```

**OnLevel** applies an `ElementLevelFilter` to match elements associated with the given level.

```csharp
var onGround = document.CollectElements()
    .OnLevel(level)
    .ToElements();
```

**NotOnLevel** applies an inverted `ElementLevelFilter` to match all elements not associated with the given level.

```csharp
var notOnGround = document.CollectElements()
    .NotOnLevel(level)
    .ToElements();
```

**InDesignOption** applies an `ElementDesignOptionFilter` to match elements contained within the given design option.

```csharp
var elements = document.CollectElements()
    .InDesignOption(designOption)
    .ToElements();
```

**NotInDesignOption** applies an inverted `ElementDesignOptionFilter` to match all elements not contained within the given design option.

```csharp
var elements = document.CollectElements()
    .NotInDesignOption(designOption)
    .ToElements();
```

**InWorkset** applies an `ElementWorksetFilter` to match elements in the given workset.

```csharp
var elements = document.CollectElements()
    .InWorkset(worksetId)
    .ToElements();
```

**NotInWorkset** applies an inverted `ElementWorksetFilter` to match all elements not in the given workset.

```csharp
var elements = document.CollectElements()
    .NotInWorkset(worksetId)
    .ToElements();
```

**WithStructuralUsage** applies a `StructuralInstanceUsageFilter` to match structural family instances with the given structural usage.

```csharp
var columns = document.CollectElements()
    .WithStructuralUsage(StructuralInstanceUsage.Column)
    .ToElements();
```

**WithoutStructuralUsage** applies an inverted `StructuralInstanceUsageFilter` to match all family instances not of the given structural usage.

```csharp
var nonColumns = document.CollectElements()
    .WithoutStructuralUsage(StructuralInstanceUsage.Column)
    .ToElements();
```

**WithStructuralWallUsage** applies a `StructuralWallUsageFilter` to match walls with the given structural wall usage.

```csharp
var bearingWalls = document.CollectElements()
    .WithStructuralWallUsage(StructuralWallUsage.Bearing)
    .ToElements();
```

**WithoutStructuralWallUsage** applies an inverted `StructuralWallUsageFilter` to match all walls not of the given structural wall usage.

```csharp
var nonBearing = document.CollectElements()
    .WithoutStructuralWallUsage(StructuralWallUsage.Bearing)
    .ToElements();
```

**WithStructuralMaterial** applies a `StructuralMaterialTypeFilter` to match family instances that have the given structural material type.

```csharp
var steelElements = document.CollectElements()
    .WithStructuralMaterial(StructuralMaterialType.Steel)
    .ToElements();
```

**WithoutStructuralMaterial** applies an inverted `StructuralMaterialTypeFilter` to match all family instances not of the given structural material type.

```csharp
var nonSteel = document.CollectElements()
    .WithoutStructuralMaterial(StructuralMaterialType.Steel)
    .ToElements();
```

**WithFamilyStructuralMaterial** applies a `FamilyStructuralMaterialTypeFilter` to match families that have the given structural material type.

```csharp
var concreteFamilies = document.CollectElements()
    .WithFamilyStructuralMaterial(StructuralMaterialType.Concrete)
    .ToElements();
```

**WithoutFamilyStructuralMaterial** applies an inverted `FamilyStructuralMaterialTypeFilter` to match all families not of the given structural material type.

```csharp
var nonConcrete = document.CollectElements()
    .WithoutFamilyStructuralMaterial(StructuralMaterialType.Concrete)
    .ToElements();
```

**WithPhaseStatus** applies an `ElementPhaseStatusFilter` to match elements that have any of the given phase statuses on the given phase.

```csharp
var newElements = document.CollectElements()
    .WithPhaseStatus(phase, ElementOnPhaseStatus.New)
    .ToElements();
```

**WithoutPhaseStatus** applies an inverted `ElementPhaseStatusFilter` to match all elements that do not have any of the given phase statuses on the given phase.

```csharp
var notNew = document.CollectElements()
    .WithoutPhaseStatus(phase, ElementOnPhaseStatus.New)
    .ToElements();
```

**WithExtensibleStorage** applies an `ExtensibleStorageFilter` to match elements that have extensible storage data for the given schema.

```csharp
var elements = document.CollectElements()
    .WithExtensibleStorage(schemaGuid)
    .ToElements();
```

**HasSharedParameter** applies a `SharedParameterApplicableRule` filter to match elements that support a shared parameter with the given name.

```csharp
var elements = document.CollectElements()
    .HasSharedParameter("SharedParameter")
    .ToElements();
```

**IntersectingBoundingBox** applies a `BoundingBoxIntersectsFilter` to match elements whose bounding box intersects the given outline.

```csharp
var elements = document.CollectElements()
    .IntersectingBoundingBox(outline)
    .ToElements();
```

**NotIntersectingBoundingBox** applies an inverted `BoundingBoxIntersectsFilter` to match all elements whose bounding box does not intersect the given outline.

```csharp
var elements = document.CollectElements()
    .NotIntersectingBoundingBox(outline)
    .ToElements();
```

**InsideBoundingBox** applies a `BoundingBoxIsInsideFilter` to match elements whose bounding box is fully contained by the given outline.

```csharp
var elements = document.CollectElements()
    .InsideBoundingBox(outline)
    .ToElements();
```

**NotInsideBoundingBox** applies an inverted `BoundingBoxIsInsideFilter` to match all elements whose bounding box is not fully contained by the given outline.

```csharp
var elements = document.CollectElements()
    .NotInsideBoundingBox(outline)
    .ToElements();
```

**ContainingPoint** applies a `BoundingBoxContainsPointFilter` to match elements whose bounding box contains the given point.

```csharp
var elements = document.CollectElements()
    .ContainingPoint(point)
    .ToElements();
```

**NotContainingPoint** applies an inverted `BoundingBoxContainsPointFilter` to match all elements whose bounding box does not contain the given point.

```csharp
var elements = document.CollectElements()
    .NotContainingPoint(point)
    .ToElements();
```

**IntersectingElement** applies an `ElementIntersectsElementFilter` to match elements whose geometry intersects the given element.

```csharp
var intersecting = document.CollectElements()
    .IntersectingElement(wall)
    .ToElements();
```

**NotIntersectingElement** applies an inverted `ElementIntersectsElementFilter` to match all elements whose geometry does not intersect the given element.

```csharp
var notIntersecting = document.CollectElements()
    .NotIntersectingElement(wall)
    .ToElements();
```

**IntersectingSolid** applies an `ElementIntersectsSolidFilter` to match elements whose geometry intersects the given solid.

```csharp
var intersecting = document.CollectElements()
    .IntersectingSolid(solid)
    .ToElements();
```

**NotIntersectingSolid** applies an inverted `ElementIntersectsSolidFilter` to match all elements whose geometry does not intersect the given solid.

```csharp
var notIntersecting = document.CollectElements()
    .NotIntersectingSolid(solid)
    .ToElements();
```

**WhereParameter** begins a fluent parameter filter expression for the given built-in parameter or parameter id.
It applies `ElementParameterFilter` with `ParameterFilterRuleFactory` rules — Revit's native filtering engine that evaluates parameters at the database level before element expansion, unlike LINQ post-filtering which
requires loading every element into memory first.

```csharp
var wallsOnLevel = document.CollectElements()
    .WhereParameter(BuiltInParameter.WALL_BASE_CONSTRAINT).Equals(levelId)
    .ToElements();

var tallWalls = document.CollectElements()
    .WhereParameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).IsGreaterThan(3.0, 1e-6)
    .ToElements();

var genericTypes = document.CollectElements()
    .WhereParameter(BuiltInParameter.ALL_MODEL_TYPE_NAME).Contains("Generic")
    .ToElements();

// Supports all Rulus provided by Revit API:
//.Equals(string | int | double | ElementId)
//.NotEquals(string | int | double | ElementId)
//.IsGreaterThan(string | int | double | ElementId)
//.IsGreaterThanOrEqualTo(string | int | double | ElementId)
//.IsLessThan(string | int | double | ElementId)
//.IsLessThanOrEqualTo(string | int | double | ElementId)
//.Contains(string)
//.NotContains(string)
//.StartsWith(string)
//.NotStartsWith(string)
//.EndsWith(string)
//.NotEndsWith(string)
//.HasValue()
//.HasNoValue()
//.IsAssociatedWithGlobalParameter(ElementId)
//.IsNotAssociatedWithGlobalParameter(ElementId)
```

**First** and **FirstOrDefault** return the first element in the collector. These use Revit native fast implementation instead of LINQ.

```csharp
var first = document.CollectElements()
    .Types()
    .First();

var firstOrNull = document.CollectElements()
    .Types()
    .FirstOrDefault();
```

**Count** returns the number of elements in the collector using Revit's native `GetElementCount()`. This use Revit native fast implementation instead of LINQ

```csharp
var count = document.CollectElements()
    .OfClass<Wall>()
    .Count();
```

**Any** returns `true` if the collector contains at least one element. These use Revit native fast implementation instead of LINQ.

```csharp
var hasWalls = document.CollectElements()
    .OfClass<Wall>()
    .Any();
```

## Families and modeling

### Family

**CanBeConvertedToFaceHostBased** indicates whether the family can be converted to face host based.

```csharp
var canConvert = family.CanBeConvertedToFaceHostBased;
var canConvert = elementId.CanBeConvertedToFaceHostBased(document);
```

**ConvertToFaceHostBased** converts a family to be face host based.

```csharp
family.ConvertToFaceHostBased();
elementId.ConvertToFaceHostBased(document);
```

**CheckIntegrity** checks that the loaded family has its content document.

```csharp
var isValid = family.CheckIntegrity();
var isValid = elementId.CheckFamilyIntegrity(document);
```

### FamilySymbol

**IsAdaptiveFamilySymbol** verifies if a FamilySymbol is a valid Adaptive Family Symbol.

```csharp
var isAdaptive = familySymbol.IsAdaptiveFamilySymbol;
```

**GetProfileSymbols** gets the profile Family Symbols of the document.

```csharp
var symbols = FamilySymbol.GetProfileSymbols(document, ProfileFamilyUsage.Any, oneCurveLoopOnly: true);
```

### FamilyInstance

**IsVoidInstanceCuttingElement** indicates if the family instance with unattached voids can cut other elements.

```csharp
var isCutting = familyInstance.IsVoidInstanceCuttingElement;
```

**GetElementsBeingCut** returns ids of the elements being cut by the instance.

```csharp
var ids = familyInstance.GetElementsBeingCut();
```

### Form

**CanBeDissolved** validates that input contains one or more form elements that can be dissolved.

```csharp
var canDissolve = Form.CanBeDissolved(document, elements);
```

**DissolveForms** dissolves a collection of form elements into their defining elements.

```csharp
var curveIds = Form.DissolveForms(document, elements);
var curveIds = Form.DissolveForms(document, elements, out var profileOriginPointSet);
```

### HostObject

**GetBottomFaces** returns the bottom faces for the host object.

```csharp
var faces = floor.GetBottomFaces();
```

**GetTopFaces** returns the top faces for the host object.

```csharp
var faces = floor.GetTopFaces();
```

**GetSideFaces** returns the major side faces for the host object.

```csharp
var faces = wall.GetSideFaces(ShellLayerType.Interior);
```

### Wall

**IsJoinAllowedAtEnd** identifies if the indicated end of the wall allows joins.

```csharp
var allowed = wall.IsJoinAllowedAtEnd(end: 0);
```

**AllowJoinAtEnd** allows the wall's end to join to other walls.

```csharp
wall.AllowJoinAtEnd(end: 0);
```

**DisallowJoinAtEnd** sets the wall's end not to join to other walls.

```csharp
wall.DisallowJoinAtEnd(end: 0);
```

### Adaptive components

**IsAdaptiveComponentFamily** verifies if the Family is an Adaptive Component Family.

```csharp
var isAdaptive = family.IsAdaptiveComponentFamily;
```

**IsAdaptiveInstanceFlipped** gets the value of the flip parameter on the adaptive instance.

```csharp
var isFlipped = familyInstance.IsAdaptiveInstanceFlipped;
```

**SetAdaptiveInstanceFlipped** sets the value of the flip parameter on the adaptive instance.

```csharp
familyInstance.SetAdaptiveInstanceFlipped(flip: true);
```

**MoveAdaptiveComponentInstance** moves an Adaptive Component Instance by the specified transformation.

```csharp
familyInstance.MoveAdaptiveComponentInstance(transform, unHost: false);
```

**GetNumberOfAdaptivePoints** gets the number of Adaptive Point Elements in an Adaptive Component Family.

```csharp
var count = family.GetNumberOfAdaptivePoints();
```

**GetNumberOfAdaptivePlacementPoints** gets the number of Placement Point Elements in an Adaptive Component Family.

```csharp
var count = family.GetNumberOfAdaptivePlacementPoints();
```

**GetNumberOfAdaptiveShapeHandlePoints** gets the number of Shape Handle Point Elements in an Adaptive Component Family.

```csharp
var count = family.GetNumberOfAdaptiveShapeHandlePoints();
```

**IsAdaptivePlacementPoint** verifies if the Reference Point is an Adaptive Placement Point.

```csharp
var isPlacementPoint = referencePoint.IsAdaptivePlacementPoint;
```

**IsAdaptivePoint** verifies if the Reference Point is an Adaptive Point.

```csharp
var isAdaptivePoint = referencePoint.IsAdaptivePoint;
```

**IsAdaptiveShapeHandlePoint** verifies if the Reference Point is an Adaptive Shape Handle Point.

```csharp
var isShapeHandle = referencePoint.IsAdaptiveShapeHandlePoint;
```

**MakeAdaptivePoint** makes a Reference Point an Adaptive Point or makes an Adaptive Point a Reference Point.

```csharp
referencePoint.MakeAdaptivePoint(AdaptivePointType.Placement);
```

**GetAdaptivePlacementNumber** gets the placement number of an Adaptive Placement Point.

```csharp
var number = referencePoint.GetAdaptivePlacementNumber();
```

**GetAdaptivePointConstraintType** gets the constraint type of an Adaptive Shape Handle Point.

```csharp
var type = referencePoint.GetAdaptivePointConstraintType();
```

**GetAdaptivePointOrientationType** gets the orientation type of an Adaptive Placement Point.

```csharp
var type = referencePoint.GetAdaptivePointOrientationType();
```

**SetAdaptivePlacementNumber** sets the placement number of an Adaptive Placement Point.

```csharp
referencePoint.SetAdaptivePlacementNumber(placementNumber);
```

**SetAdaptivePointConstraintType** sets the constraint type of an Adaptive Shape Handle Point.

```csharp
referencePoint.SetAdaptivePointConstraintType(constraintType);
```

**SetAdaptivePointOrientationType** sets the orientation type of an Adaptive Placement Point.

```csharp
referencePoint.SetAdaptivePointOrientationType(orientationType);
```

**HasAdaptiveFamilySymbol** verifies if a FamilyInstance has an Adaptive Family Symbol.

```csharp
var hasAdaptive = familyInstance.HasAdaptiveFamilySymbol;
```

**IsAdaptiveComponentInstance** verifies if a FamilyInstance is an Adaptive Component Instance.

```csharp
var isAdaptive = familyInstance.IsAdaptiveComponentInstance;
```

**GetAdaptivePlacementPointElementRefIds** gets the Placement Adaptive Point Element Ref ids to which the instance geometry adapts.

```csharp
var ids = familyInstance.GetAdaptivePlacementPointElementRefIds();
```

**GetAdaptivePointElementRefIds** gets all Adaptive Point Element Ref ids to which the instance geometry adapts.

```csharp
var ids = familyInstance.GetAdaptivePointElementRefIds();
```

**GetAdaptiveShapeHandlePointElementRefIds** gets Shape Handle Adaptive Point Element Ref ids.

```csharp
var ids = familyInstance.GetAdaptiveShapeHandlePointElementRefIds();
```

**CreateAdaptiveComponentInstance** creates a FamilyInstance of an Adaptive Component Family.

```csharp
var instance = familySymbol.CreateAdaptiveComponentInstance();
```

### Annotation

**IsMultiAlignSupported** returns true if the element can be aligned to other similar elements.

```csharp
var supports = element.IsMultiAlignSupported;
```

**GetAnnotationOutlineWithoutLeaders** gets the four corners of the alignable element in model space without its leaders.

```csharp
var corners = element.GetAnnotationOutlineWithoutLeaders();
```

**MoveWithAnchoredLeaders** moves the element while keeping the leader end points anchored.

```csharp
element.MoveWithAnchoredLeaders(moveVector);
```

### Detail

**IsDetailElement** indicates if the element participates in detail draw ordering in the view.

```csharp
var isDetail = element.IsDetailElement(view);
var isDetail = elementId.IsDetailElement(document, view);
var areDetails = elementIds.AreDetailElements(document, view);
```

**BringForward** moves the given detail instance one step closer to the front.

```csharp
element.BringForward(view);
elementId.BringForward(document, view);
elementIds.BringForward(document, view);
```

**BringToFront** places the given detail instance in front of all other detail instances.

```csharp
element.BringToFront(view);
elementId.BringToFront(document, view);
elementIds.BringToFront(document, view);
```

**SendBackward** moves the given detail instance one step closer to the back.

```csharp
element.SendBackward(view);
elementId.SendBackward(document, view);
elementIds.SendBackward(document, view);
```

**SendToBack** places the given detail instance behind all other detail instances.

```csharp
element.SendToBack(view);
elementId.SendToBack(document, view);
elementIds.SendToBack(document, view);
```

**GetDrawOrderForDetails** returns the given detail elements sorted according to the current draw order.

```csharp
var sorted = view.GetDrawOrderForDetails(detailIds);
```

### Parts

**HasAssociatedParts** checks if an element has associated parts.

```csharp
var hasParts = element.HasAssociatedParts;
var hasParts = elementId.HasAssociatedParts(document);
var hasParts = linkElementId.HasAssociatedParts(document);
```

**GetAssociatedParts** returns all Parts that are associated with the given element.

```csharp
var parts = element.GetAssociatedParts(includePartsWithAssociatedParts: true, includeAllChildren: true);
var parts = elementId.GetAssociatedParts(document, includePartsWithAssociatedParts: true, includeAllChildren: true);
```

**GetAssociatedPartMaker** gets associated PartMaker for an element.

```csharp
var maker = element.GetAssociatedPartMaker();
var maker = elementId.GetAssociatedPartMaker(document);
```

**CreateParts** creates a new set of parts out of the original elements.

```csharp
elementIds.CreateParts(document);
hostOrLinkElements.CreateParts(document);
```

**DivideParts** creates divided parts out of parts.

```csharp
var maker = elementIds.DivideParts(document, intersectingReferenceIds, curveArray, sketchPlaneId);
```

**CreateMergedPart** creates a single merged part from the specified Parts.

```csharp
var maker = elementIds.CreateMergedPart(document);
```

**IsMergedPart** checks if the Part is the result of a merge.

```csharp
var isMerged = part.IsMergedPart;
```

**GetSplittingCurves** identifies the curves that were used to create the part.

```csharp
var curves = part.GetSplittingCurves();
var curves = elementId.GetSplittingCurves(document);
```

**GetSplittingElements** identifies the elements that were used to create the part.

```csharp
var elements = part.GetSplittingElements();
var elements = elementId.GetSplittingElements(document);
```

**AreElementsValidForCreateParts** identifies if the given elements can be used to create parts.

```csharp
var valid = elementIds.AreElementsValidForCreateParts(document);
```

**IsValidForCreateParts** identifies if the given element can be used to create parts.

```csharp
var valid = linkElementId.IsValidForCreateParts(document);
```

**ArePartsValidForDivide** identifies if the provided members are valid for dividing parts.

```csharp
var valid = elementIds.ArePartsValidForDivide(document);
```

**ArePartsValidForMerge** identifies whether Part elements may be merged.

```csharp
var valid = elementIds.ArePartsValidForMerge(document);
```

**FindMergeableClusters** segregates a set of elements into subsets which are valid for merge.

```csharp
var clusters = elementIds.FindMergeableClusters(document);
```

**GetChainLengthToOriginal** calculates the length of the longest chain of divisions/merges to reach the original non-Part element.

```csharp
var length = part.GetChainLengthToOriginal();
```

**GetMergedParts** retrieves the element ids of the source elements of a merged part.

```csharp
var ids = part.GetMergedParts();
```

**GetPartMakerMethodToDivideVolumeFw** obtains the object allowing access to the divided volume properties of the PartMaker.

```csharp
var method = partMaker.GetPartMakerMethodToDivideVolumeFw();
```

### Assembly

**AcquireViews** transfers the assembly views owned by a source assembly instance to a target sibling assembly instance.

```csharp
sourceAssembly.AcquireViews(targetAssembly);
```

**Create3DOrthographic** creates a new orthographic 3D assembly view for the assembly instance.

```csharp
var view = assemblyInstance.Create3DOrthographic();
var view = assemblyInstance.Create3DOrthographic(viewTemplateId, isAssigned: true);
```

**CreateDetailSection** creates a new detail section assembly view for the assembly instance.

```csharp
var view = assemblyInstance.CreateDetailSection(AssemblyDetailViewOrientation.ElevationBottom);
var view = assemblyInstance.CreateDetailSection(AssemblyDetailViewOrientation.ElevationBottom, viewTemplateId, isAssigned: true);
```

**CreateMaterialTakeoff** creates a new material takeoff multicategory schedule assembly view for the assembly instance.

```csharp
var schedule = assemblyInstance.CreateMaterialTakeoff();
var schedule = assemblyInstance.CreateMaterialTakeoff(viewTemplateId, isAssigned: true);
```

**CreatePartList** creates a new part list multicategory schedule assembly view for the assembly instance.

```csharp
var schedule = assemblyInstance.CreatePartList();
var schedule = assemblyInstance.CreatePartList(viewTemplateId, isAssigned: true);
```

**CreateSheet** creates a new sheet assembly view for the assembly instance.

```csharp
var sheet = assemblyInstance.CreateSheet(titleBlockId);
```

**CreateSingleCategorySchedule** creates a new single-category schedule assembly view for the assembly instance.

```csharp
var schedule = assemblyInstance.CreateSingleCategorySchedule(scheduleCategoryId);
var schedule = assemblyInstance.CreateSingleCategorySchedule(scheduleCategoryId, viewTemplateId, isAssigned: true);
```

### Mass

**IsMassFamilyInstance** checks if the element is a mass family instance.

```csharp
var isMass = massInstance.IsMassFamilyInstance;
var isMass = massInstanceId.IsMassFamilyInstance(document);
```

**GetMassGrossFloorArea** gets the total occupiable floor area represented by a mass instance.

```csharp
var area = massInstance.GetMassGrossFloorArea();
var area = massInstanceId.GetMassGrossFloorArea(document);
```

**GetMassGrossSurfaceArea** gets the total exterior building surface area represented by a mass instance.

```csharp
var area = massInstance.GetMassGrossSurfaceArea();
var area = massInstanceId.GetMassGrossSurfaceArea(document);
```

**GetMassGrossVolume** gets the total building volume represented by a mass instance.

```csharp
var volume = massInstance.GetMassGrossVolume();
var volume = massInstanceId.GetMassGrossVolume(document);
```

**GetMassLevelDataIds** gets the ElementIds of the MassLevelDatas associated with a mass instance.

```csharp
var ids = massInstance.GetMassLevelDataIds();
var ids = massInstanceId.GetMassLevelDataIds(document);
```

**GetMassJoinedElementIds** gets the ElementIds of elements that are joined to a mass instance.

```csharp
var ids = massInstance.GetMassJoinedElementIds();
var ids = massInstanceId.GetMassJoinedElementIds(document);
```

**GetMassLevelIds** gets the ElementIds of the Levels associated with a mass instance.

```csharp
var ids = massInstance.GetMassLevelIds();
var ids = massInstanceId.GetMassLevelIds(document);
```

**AddMassLevelData** creates a MassLevelData to associate a Level with a mass instance.

```csharp
var id = massInstance.AddMassLevelData(levelId);
var id = massInstanceId.AddMassLevelData(document, levelId);
```

**RemoveMassLevelData** deletes the MassLevelData that associates a Level with a mass instance.

```csharp
massInstance.RemoveMassLevelData(levelId);
massInstanceId.RemoveMassLevelData(document, levelId);
```

## Disciplines

### MEP

#### Pipe

**ConnectPipePlaceholdersAtElbow** connects placeholders that look like an elbow connection.

```csharp
var isConnected = connector1.ConnectPipePlaceholdersAtElbow(connector2);
```

**ConnectPipePlaceholdersAtTee** connects three placeholders that look like a Tee connection.

```csharp
var isConnected = connector1.ConnectPipePlaceholdersAtTee(connector2, connector3);
```

**ConnectPipePlaceholdersAtCross** connects placeholders that look like a Cross connection.

```csharp
var isConnected = connector1.ConnectPipePlaceholdersAtCross(connector2, connector3, connector4);
```

**ConvertPipePlaceholders** converts a collection of pipe placeholder elements into pipe elements.

```csharp
var ids = placeholderIds.ConvertPipePlaceholders(document);
```

**PlaceCapOnOpenEnds** places caps on the open connectors of the pipe curve.

```csharp
pipe.PlaceCapOnOpenEnds();
pipe.PlaceCapOnOpenEnds(typeId);
```

**HasOpenConnector** checks if there is an open piping connector for the given pipe curve.

```csharp
var hasOpen = pipe.HasOpenConnector;
```

**BreakCurve** breaks the pipe curve into two parts at the given position.

```csharp
var newPipeId = pipe.BreakCurve(new XYZ(1, 1, 1));
```

#### Duct

**BreakCurve** breaks the duct curve into two parts at the given position.

```csharp
var newDuctId = duct.BreakCurve(new XYZ(1, 1, 1));
```

**ConnectAirTerminal** connects an air terminal to a duct directly.

```csharp
var isConnected = duct.ConnectAirTerminal(airTerminalId);
```

**ConvertDuctPlaceholders** converts a collection of duct placeholder elements into duct elements.

```csharp
var ids = placeholderIds.ConvertDuctPlaceholders(document);
```

**NewDuctworkStiffener** creates a family based stiffener on the specified fabrication ductwork.

```csharp
var stiffener = document.NewDuctworkStiffener(familySymbol, host, distanceFromHostEnd);
```

#### Connector

**ConnectDuctPlaceholdersAtElbow** connects a pair of placeholders at an Elbow connection.

```csharp
var isConnected = connector.ConnectDuctPlaceholdersAtElbow(other);
```

**ConnectDuctPlaceholdersAtTee** connects a trio of placeholders at a Tee connection.

```csharp
var isConnected = connector.ConnectDuctPlaceholdersAtTee(connector2, connector3);
```

**ConnectDuctPlaceholdersAtCross** connects a group of placeholders at a Cross connection.

```csharp
var isConnected = connector.ConnectDuctPlaceholdersAtCross(connector2, connector3, connector4);
```

**ValidateFabricationConnectivity** checks if two connectors are valid to connect directly without couplings.

```csharp
var isValid = connector.ValidateFabricationConnectivity(other);
```

#### Fabrication

**ExportToPcf** exports a list of fabrication parts into PCF format.

```csharp
document.ExportToPcf(filename, ids);
```

### Structure

#### Rebar

**CanBeSpliced** verifies if the rebar can be spliced with the provided line or geometry.

```csharp
var error = rebar.CanBeSpliced(spliceOptions, line, linePlaneNormal);
var error = rebar.CanBeSpliced(spliceOptions, line, viewId);
var error = rebar.CanBeSpliced(spliceOptions, spliceGeometry);
```

**Splice** splices a rebar with a line or a list of RebarSpliceGeometry.

```csharp
var ids = rebar.Splice(spliceOptions, line, linePlaneNormal);
var ids = rebar.Splice(spliceOptions, line, viewId);
var ids = rebar.Splice(spliceOptions, spliceGeometries);

var ids = elementId.SpliceRebar(document, spliceOptions, line, linePlaneNormal);
var ids = elementId.SpliceRebar(document, spliceOptions, line, viewId);
var ids = elementId.SpliceRebar(document, spliceOptions, spliceGeometries);
```

**UnifyRebarsIntoOne** unifies two rebars by removing the splice between them.

```csharp
var newId = rebar.UnifyRebarsIntoOne(secondRebarId);
var newId = elementId.UnifyRebarsIntoOne(document, secondRebarId);
```

**GetSpliceChain** returns all the rebars that are part of a splice chain with the input rebar.

```csharp
var chain = rebar.GetSpliceChain();
```

**GetLapDirectionForSpliceGeometryAndPosition** computes the lap direction given a RebarSpliceGeometry and a RebarSplicePosition.

```csharp
var direction = rebar.GetLapDirectionForSpliceGeometryAndPosition(spliceGeometry, splicePosition);
```

**GetSpliceGeometries** computes a list of RebarSpliceGeometry which respects the rules.

```csharp
var result = rebar.GetSpliceGeometries(spliceOptions, spliceRules);
var result = elementId.GetRebarSpliceGeometries(document, spliceOptions, spliceRules);
```

**AlignByFace** copies source rebars aligned to the destination face.

```csharp
var ids = sourceRebars.AlignByFace(document, sourceFaceReference, destinationFaceReference);
```

**AlignByHost** copies source rebars aligned in the same way as the source host is aligned to the destination host.

```csharp
var ids = sourceRebars.AlignByHost(document, destinationHost);
```

**GetAllParameters** lists all shape parameters used by all existing RebarShapes in the document.

```csharp
var ids = rebarShape.GetAllParameters();
```

**IsValidRebarShapeParameter** checks that an ExternalDefinition may be used as a Rebar Shape parameter.

```csharp
var isValid = externalDefinition.IsValidRebarShapeParameter;
```

**GetRebarShapeParameterElementId** retrieves the ElementId corresponding to an external rebar shape parameter in the document.

```csharp
var id = externalDefinition.GetRebarShapeParameterElementId(document);
```

**GetOrCreateRebarShapeParameterElementId** retrieves or creates the ElementId corresponding to an external rebar shape parameter in the document.

```csharp
var id = externalDefinition.GetOrCreateRebarShapeParameterElementId(document);
```

**NewRebarSpliceType** creates a Rebar Splice Type element.

```csharp
var spliceType = document.NewRebarSpliceType(typeName);
```

**NewRebarCrankType** creates a Rebar Crank Type element.

```csharp
var crankType = document.NewRebarCrankType(typeName);
```

**GetRebarSpliceLapLengthMultiplier** gets the lap length multiplier value of the Rebar Splice Type.

```csharp
var value = element.GetRebarSpliceLapLengthMultiplier();
var value = elementId.GetRebarSpliceLapLengthMultiplier(document);
```

**GetRebarSpliceShiftOption** identifies the way bars are shifted in the splice relation.

```csharp
var option = element.GetRebarSpliceShiftOption();
var option = elementId.GetRebarSpliceShiftOption(document);
```

**GetRebarSpliceStaggerLengthMultiplier** gets the stagger length multiplier value of the Rebar Splice Type.

```csharp
var value = element.GetRebarSpliceStaggerLengthMultiplier();
var value = elementId.GetRebarSpliceStaggerLengthMultiplier(document);
```

**SetRebarSpliceLapLengthMultiplier** sets the lap length multiplier value of the Rebar Splice Type.

```csharp
element.SetRebarSpliceLapLengthMultiplier(lapLengthMultiplier);
elementId.SetRebarSpliceLapLengthMultiplier(document, lapLengthMultiplier);
```

**SetRebarSpliceShiftOption** sets the way bars are shifted in the splice relation.

```csharp
element.SetRebarSpliceShiftOption(shiftOption);
elementId.SetRebarSpliceShiftOption(document, shiftOption);
```

**SetRebarSpliceStaggerLengthMultiplier** sets the stagger length multiplier value of the Rebar Splice Type.

```csharp
element.SetRebarSpliceStaggerLengthMultiplier(staggerLengthMultiplier);
elementId.SetRebarSpliceStaggerLengthMultiplier(document, staggerLengthMultiplier);
```

**GetRebarCrankLengthMultiplier** gets the crank length multiplier value of the Rebar Crank Type.

```csharp
var value = element.GetRebarCrankLengthMultiplier();
var value = elementId.GetRebarCrankLengthMultiplier(document);
```

**GetRebarCrankOffsetMultiplier** gets the crank offset multiplier value of the Rebar Crank Type.

```csharp
var value = element.GetRebarCrankOffsetMultiplier();
var value = elementId.GetRebarCrankOffsetMultiplier(document);
```

**GetRebarCrankRatio** gets the crank ratio value of the Rebar Crank Type.

```csharp
var value = element.GetRebarCrankRatio();
var value = elementId.GetRebarCrankRatio(document);
```

**SetRebarCrankLengthMultiplier** sets the crank length multiplier value of the Rebar Crank Type.

```csharp
element.SetRebarCrankLengthMultiplier(crankLengthMultiplier);
elementId.SetRebarCrankLengthMultiplier(document, crankLengthMultiplier);
```

**SetRebarCrankOffsetMultiplier** sets the crank offset multiplier value of the Rebar Crank Type.

```csharp
element.SetRebarCrankOffsetMultiplier(crankOffsetMultiplier);
elementId.SetRebarCrankOffsetMultiplier(document, crankOffsetMultiplier);
```

**SetRebarCrankRatio** sets the crank ratio value of the Rebar Crank Type.

```csharp
element.SetRebarCrankRatio(crankRatio);
elementId.SetRebarCrankRatio(document, crankRatio);
```

#### Structural framing

**CanFlipFramingEnds** determines if the ends of the given framing element can be flipped.

```csharp
var canFlip = familyInstance.CanFlipFramingEnds;
```

**FlipFramingEnds** flips the ends of the structural framing element.

```csharp
familyInstance.FlipFramingEnds();
```

**IsFramingJoinAllowedAtEnd** identifies if the indicated end is allowed to join to others.

```csharp
var allowed = familyInstance.IsFramingJoinAllowedAtEnd(end: 0);
```

**AllowFramingJoinAtEnd** sets the indicated end to be allowed to join.

```csharp
familyInstance.AllowFramingJoinAtEnd(end: 0);
```

**DisallowFramingJoinAtEnd** sets the indicated end to not be allowed to join.

```csharp
familyInstance.DisallowFramingJoinAtEnd(end: 0);
```

**GetFramingEndReference** returns a reference to the end of a framing element according to the setback settings.

```csharp
var reference = familyInstance.GetFramingEndReference(end: 0);
```

**IsFramingEndReferenceValid** determines if the given reference can be set for the given end of the framing element.

```csharp
var isValid = familyInstance.IsFramingEndReferenceValid(end: 0, pick);
```

**CanSetFramingEndReference** determines if a reference can be set for the given end of the framing element.

```csharp
var canSet = familyInstance.CanSetFramingEndReference(end: 0);
```

**SetFramingEndReference** sets the end reference of a framing element.

```csharp
familyInstance.SetFramingEndReference(end: 0, pick);
```

**RemoveFramingEndReference** resets the end reference of the structural framing element.

```csharp
familyInstance.RemoveFramingEndReference(end: 0);
```

#### Structural sections

**GetStructuralSection** returns the structural section from an element.

```csharp
var section = familyInstance.GetStructuralSection();
var section = elementId.GetStructuralSection(document);
```

**SetStructuralSection** sets the structural section in an element.

```csharp
var success = familySymbol.SetStructuralSection(structuralSection);
var success = elementId.SetStructuralSection(document, structuralSection);
```

**GetStructuralElementDefinitionData** returns structural element definition data.

```csharp
var code = familyInstance.GetStructuralElementDefinitionData(out var data);
var code = elementId.GetStructuralElementDefinitionData(document, out var data);
```

### Analytical

**IsAnalyticalElement** returns true if the element is an analytical element.

```csharp
var isAnalytical = element.IsAnalyticalElement;
var isAnalytical = elementId.IsAnalyticalElement(document);
```

**IsPhysicalElement** returns true if the element is a physical element.

```csharp
var isPhysical = element.IsPhysicalElement;
var isPhysical = elementId.IsPhysicalElement(document);
```

## Model access and interoperability

### ModelPath

**ConvertToUserVisiblePath** gets a string version of the path of a given ModelPath.

```csharp
var path = modelPath.ConvertToUserVisiblePath();
```

**ConvertToCloudPath** converts a pair of cloud project and model GUIDs to a valid cloud path.

```csharp
var cloudPath = modelGuid.ConvertToCloudPath(projectGuid, region);
```

### Worksharing

**CreateNewLocal** copies the central model into a new local file for the current user.

```csharp
var localPath = centralPath.CreateNewLocal(targetPath);
```

**GetUserWorksetInfo** gets information about user worksets in a workshared model file without fully opening the file.

```csharp
var info = modelPath.GetUserWorksetInfo();
```

**GetCheckoutStatus** gets the ownership status of an element and can also return the owner.

```csharp
var status = element.GetCheckoutStatus();
var status = elementId.GetCheckoutStatus(document);

var statusWithOwner = element.GetCheckoutStatus(out var owner);
var statusWithOwner = elementId.GetCheckoutStatus(document, out var owner);
```

**GetWorksharingTooltipInfo** gets worksharing information about an element to display in an in-canvas tooltip.

```csharp
var info = element.GetWorksharingTooltipInfo();
var info = elementId.GetWorksharingTooltipInfo(document);
```

**GetModelUpdatesStatus** gets the status of a single element in the central model.

```csharp
var status = element.GetModelUpdatesStatus();
var status = elementId.GetModelUpdatesStatus(document);
```

**RelinquishOwnership** relinquishes ownership of as many specified elements and worksets as possible.

```csharp
var items = document.RelinquishOwnership(relinquishOptions, transactOptions);
```

**CheckoutWorksets** obtains ownership for the current user of as many specified worksets as possible.

```csharp
var checkedOut = worksets.CheckoutWorksets(document);
var checkedOut = worksets.CheckoutWorksets(document, options);
```

**CheckoutElements** obtains ownership for the current user of as many specified elements as possible.

```csharp
var checkedOut = elementIds.CheckoutElements(document);
var checkedOut = elementIds.CheckoutElements(document, options);
```

### Coordination model

**IsCoordinationModelInstance** checks whether an element is a Coordination Model instance.

```csharp
var isInstance = element.IsCoordinationModelInstance;
```

**IsCoordinationModelType** checks whether an element is a Coordination Model type.

```csharp
var isType = element.IsCoordinationModelType;
```

**GetCoordinationModelVisibilityOverride** gets the visibility override for the provided Coordination Model instance or type.

```csharp
var isVisible = element.GetCoordinationModelVisibilityOverride(view);
```

**SetCoordinationModelVisibilityOverride** sets the visibility override for the provided Coordination Model instance or type.

```csharp
element.SetCoordinationModelVisibilityOverride(view, visible: true);
```

**LinkCoordinationModelFromLocalPath** creates a Coordination Model instance using the absolute path of a .nwc or .nwd file.

```csharp
var instance = document.LinkCoordinationModelFromLocalPath(filePath, linkOptions);
```

**Link3DViewFromAutodeskDocs** creates a Coordination Model instance based on the information provided by the specified Autodesk Docs data.

```csharp
var instance = document.Link3DViewFromAutodeskDocs(accountId, projectId, fileId, viewName, linkOptions);
```

**GetCoordinationModelTypeData** gets link data for the provided Coordination Model type.

```csharp
var data = elementType.GetCoordinationModelTypeData();
```

**ReloadCoordinationModel** reloads the provided Coordination Model type element.

```csharp
elementType.ReloadCoordinationModel();
```

**UnloadCoordinationModel** unloads the provided Coordination Model type element.

```csharp
elementType.UnloadCoordinationModel();
```

**GetAllCoordinationModelInstanceIds** gets all Coordination Model instance ids in the document.

```csharp
var ids = document.GetAllCoordinationModelInstanceIds();
```

**GetAllCoordinationModelTypeIds** gets all Coordination Model type ids in the document.

```csharp
var ids = document.GetAllCoordinationModelTypeIds();
```

**GetAllPropertiesForReferenceInsideCoordinationModel** gets all the properties for the provided Coordination Model instance reference.

```csharp
var props = element.GetAllPropertiesForReferenceInsideCoordinationModel(reference);
```

**GetCategoryForReferenceInsideCoordinationModel** returns the category name for the provided element reference inside the Coordination Model instance.

```csharp
var name = element.GetCategoryForReferenceInsideCoordinationModel(reference);
```

**GetVisibilityOverrideForReferenceInsideCoordinationModel** gets the visibility for the provided reference inside the Coordination Model.

```csharp
var isVisible = element.GetVisibilityOverrideForReferenceInsideCoordinationModel(view, reference);
```

**SetVisibilityOverrideForReferenceInsideCoordinationModel** sets the visibility override for the provided reference inside the Coordination Model instance.

```csharp
element.SetVisibilityOverrideForReferenceInsideCoordinationModel(view, reference, visible: true);
```

**GetCoordinationModelColorOverride** gets the color override value for the provided Coordination Model type.

```csharp
var color = elementType.GetCoordinationModelColorOverride(view);
```

**SetCoordinationModelColorOverride** sets the color override value for the provided Coordination Model type.

```csharp
elementType.SetCoordinationModelColorOverride(view, color);
```

**GetCoordinationModelTransparencyOverride** gets the transparency override value for the provided Coordination Model type.

```csharp
var transparency = elementType.GetCoordinationModelTransparencyOverride(view);
```

**SetCoordinationModelTransparencyOverride** sets the transparency override value for the provided Coordination Model type.

```csharp
elementType.SetCoordinationModelTransparencyOverride(view, transparency);
```

**ContainsCoordinationModelCategory** checks whether a provided string is an element category name in the provided Autodesk Docs Coordination Model type.

```csharp
var contains = elementType.ContainsCoordinationModelCategory(categoryName);
```

**GetCoordinationModelColorOverrideForCategory** returns the color override value for the provided element category name inside the Coordination Model type.

```csharp
var color = elementType.GetCoordinationModelColorOverrideForCategory(view, categoryName);
```

**SetCoordinationModelColorOverrideForCategory** sets the color override value for the provided element category name inside the Coordination Model type.

```csharp
elementType.SetCoordinationModelColorOverrideForCategory(view, categoryName, color);
```

**GetCoordinationModelVisibilityOverrideForCategory** gets the visibility override for the provided element category name in the Coordination Model type.

```csharp
var isVisible = elementType.GetCoordinationModelVisibilityOverrideForCategory(view, categoryName);
```

**SetCoordinationModelVisibilityOverrideForCategory** sets the visibility override for the provided element category name inside the Coordination Model type.

```csharp
elementType.SetCoordinationModelVisibilityOverrideForCategory(view, categoryName, visible: true);
```

**ReloadAutodeskDocsCoordinationModelFrom** reloads an Autodesk Docs Coordination Model type from the specified Autodesk Docs data.

```csharp
elementType.ReloadAutodeskDocsCoordinationModelFrom(accountId, projectId, fileId, viewName);
```

**ReloadLocalCoordinationModelFrom** reloads a local Coordination Model type from the specified absolute path.

```csharp
elementType.ReloadLocalCoordinationModelFrom(filePath);
```

### Export

**GetExportId** retrieves the GUID representing this element in DWF and IFC export.

```csharp
var guid = element.ExportId;
var guid = subelement.ExportId;
var guid = elementId.GetExportId(document);
```

**GbXmlId** retrieves the GUID representing this document in exported gbXML files.

```csharp
var guid = document.GbXmlId;
```

**GetNurbsSurfaceData** returns the necessary information to define a NURBS surface.

```csharp
var data = surface.GetNurbsSurfaceData();
```

### External files

**IsExternalFileReference** determines whether the given element represents an external file.

```csharp
var isExternal = element.IsExternalFileReference;
var isExternal = elementId.IsExternalFileReference(document);
```

**GetAllExternalFileReferences** gets the ids of all elements which are external file references.

```csharp
var ids = document.GetAllExternalFileReferences();
```

**GetExternalFileReference** gets the external file referencing data for the given element.

```csharp
var reference = element.GetExternalFileReference();
var reference = elementId.GetExternalFileReference(document);
```

### External resources

**GetServers** gets registered external resource servers which support the external resource type.

```csharp
var servers = resourceType.GetServers();
```

**ServerSupportsAssemblyCodeData** and similar extensions check whether the server supports a given resource type.

```csharp
var supports = externalResourceReference.ServerSupportsAssemblyCodeData;
var supports = externalResourceReference.ServerSupportsRevitLinks;
var supports = externalResourceReference.ServerSupportsCadLinks;
var supports = externalResourceReference.ServerSupportsIfcLinks;
var supports = externalResourceReference.ServerSupportsKeynotes;
```

**IsValidShortName** checks whether the name is a valid short name for the external resource server.

```csharp
var isValid = ExternalResourceReference.IsValidShortName(serverId, serverName);
```

**GetAllExternalResourceReferences** gets the ids of all elements which refer to external resources.

```csharp
var ids = document.GetAllExternalResourceReferences();
var ids = document.GetAllExternalResourceReferences(resourceType);
```

### Point clouds

**GetFilteredOutline** computes the outline of a part of a box that satisfies the given PointCloudFilter.

```csharp
var outline = filter.GetFilteredOutline(box);
```

## DirectContext3D

**IsADirectContext3DHandleCategory** checks whether the category is one of the categories used by DirectContext3D handle elements.

```csharp
var isHandle = category.IsADirectContext3DHandleCategory;
```

**GetDirectContext3DHandleInstances** returns all DirectContext3D handle instances of the given category in the document.

```csharp
var ids = category.GetDirectContext3DHandleInstances(document);
```

**GetDirectContext3DHandleTypes** returns all DirectContext3D handle types of the given category in the document.

```csharp
var ids = category.GetDirectContext3DHandleTypes(document);
```

**IsADirectContext3DHandleInstance** checks whether the element corresponds to a DirectContext3D handle instance.

```csharp
var isInstance = element.IsADirectContext3DHandleInstance;
```

**IsADirectContext3DHandleType** checks whether the element corresponds to a DirectContext3D handle type.

```csharp
var isType = element.IsADirectContext3DHandleType;
```

## System

**Cast\<T\>** casts an object to the specified type.

```csharp
var width = element.Cast<Wall>().Width;
var location = element.Location.Cast<LocationCurve>().Curve;
```

**Round** rounds the value to the specified precision or 1e-9 precision specified in Revit API.

```csharp
var rounded = 6.56170000000000000000000001.Round(); // 6.5617
var rounded = 6.56170000000000000000000001.Round(0); // 7
```

**IsAlmostEqual** compares two numbers within specified precision or 1e-9 precision specified in Revit API.

```csharp
var isEqual = 6.56170000000000000000000001.IsAlmostEqual(6.5617); // true
var isEqual = 6.56170000000000000000000001.IsAlmostEqual(6.6, 1e-1); // true
```

**IsNullOrEmpty** indicates whether the specified string is null or an empty string.

```csharp
var isEmpty = "".IsNullOrEmpty(); // true
var isEmpty = null.IsNullOrEmpty(); // true
```

**IsNullOrWhiteSpace** indicates whether a specified string is null, empty, or consists only of white-space characters.

```csharp
var isEmpty = " ".IsNullOrWhiteSpace(); // true
var isEmpty = null.IsNullOrWhiteSpace(); // true
```

**AppendPath** combines paths.

```csharp
var path = "C:/Folder".AppendPath("AddIn"); // C:/Folder/AddIn
```

**Show** opens a window and sets the owner of a child window. Applicable for modeless windows to be attached to Revit.

```csharp
new RevitAddinView().Show(uiApplication.MainWindowHandle);
```
