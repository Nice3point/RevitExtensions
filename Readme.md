<h3 align="center"><img src="https://user-images.githubusercontent.com/20504884/147851991-6da425cf-44ee-4517-86ad-a6d22253d197.png" width="500px"></h3>

# Improve your experience with Revit API now

<p align="center">
  <a href="https://www.nuget.org/packages/Nice3point.Revit.Extensions"><img src="https://img.shields.io/nuget/v/Nice3point.Revit.Extensions?style=for-the-badge"></a>
  <a href="https://www.nuget.org/packages/Nice3point.Revit.Extensions"><img src="https://img.shields.io/nuget/dt/Nice3point.Revit.Extensions?style=for-the-badge"></a>
  <a href="https://github.com/Nice3point/RevitExtensions/commits/develop"><img src="https://img.shields.io/github/last-commit/Nice3point/RevitExtensions/develop?style=for-the-badge"></a>
</p>

Extensions minimize the writing of repetitive code, add new methods not included in RevitApi, and also allow you to write chained methods without worrying about API versioning:

```c#
new ElementId(123469)
.ToElement(document)
.GetParameter(BuiltInParameter.DOOR_HEIGHT)
.AsDouble()
.ToMillimeters()
.Round()
```

Extensions include annotations to help ReShaper parse your code and signal when a method may return null or the value returned by the method is not used in your code.

## Installation

You can install Extensions as a [nuget package](https://www.nuget.org/packages/Nice3point.Revit.Extensions).

Packages are compiled for a specific version of Revit, to support different versions of libraries in one project, use RevitVersion property.

```text
<PackageReference Include="Nice3point.Revit.Extensions" Version="$(RevitVersion).*"/>
```

Package included by default in [Revit Templates](https://github.com/Nice3point/RevitTemplates).

## Features

### Table of contents

- [Element Extensions](#ElementExtensions)
- [ElementId Extensions](#ElementIdExtensions)
- [Geometry Extensions](#GeometryExtensions)
- [Ribbon Extensions](#RibbonExtensions)
- [Unit Extensions](#UnitExtensions)
- [Host Extensions](#HostExtensions)
- [Label Extensions](#LabelExtensions)
- [Solid Extensions](#SolidExtensions)
- [Schema Extensions](#SchemaExtensions)
- [Application Extensions](#ApplicationExtensions)
- [Collector Extensions](#CollectorExtensions)
- [Imperial Extensions](#ImperialExtensions)
- [Double Extensions](#DoubleExtensions)
- [String Extensions](#StringExtensions)

### <a id="ElementExtensions">Element Extensions</a>

The **Cast<T>()** method cast the element to the specified type.

```c#
Wall wall = element.Cast<Wall>();
Floor floor = element.Cast<Floor>();
HostObject hostObject = element.Cast<HostObject>();
```

The **GetParameter()** method retrieves a parameter from an element, regardless of whether the parameter is in an instance or a type.

```c#
element.GetParameter(ParameterTypeId.AllModelUrl, includeType);
element.GetParameter(BuiltInParameter.ALL_MODEL_URL);
element.GetParameter("URL");
```

The **Copy()** method copies an element and places the copy at a location indicated by a given transformation.

```c#
element.Copy(1, 1, 0);
element.Copy(new XYZ(1, 1, 0));
```

The **Mirror()** method creates a mirrored copy of an element about a given plane.

```c#
element.Mirror(plane);
```

The **Move()** method moves the element by the specified vector.

```c#
element.Move(1, 1, 0);
element.Move(new XYZ(1, 1, 0));
```

The **Rotate()** method rotates an element about the given axis and angle.

```c#
element.Rotate(axis, angle);
```

The **CanBeMirrored()** method determines whether element can be mirrored.

```c#
element.CanBeMirrored();
```

### <a id="ElementIdExtensions">ElementId Extensions</a>

The **ToElement()** method returns the element from the Id for a specified document and convert to a type if necessary.

```c#
Element element = elementId.ToElement(document);
Wall wall = elementId.ToElement<Wall>(document);
```

The **AreEquals()** method checks if an ID matches BuiltInСategory or BuiltInParameter.

```c#
categoryId.AreEquals(BuiltInCategory.OST_Walls);
parameterId.AreEquals(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);
```

### <a id="GeometryExtensions">Geometry Extensions</a>

The **Distance()** method returns distance between two lines. The lines are considered endless.

```c#
var line1 = Line.CreateBound(new XYZ(0,0,1), new XYZ(1,1,1));
var line2 = Line.CreateBound(new XYZ(1,2,2), new XYZ(1,2,2));
var distance = line1.Distance(line2);
```

The **JoinGeometry()** method creates clean joins between two elements that share a common face.

```c#
element1.JoinGeometry(element2);
```

The **UnjoinGeometry()** method removes a join between two elements.

```c#
element1.UnjoinGeometry(element2);
```

The **AreElementsJoined()** method determines whether two elements are joined.

```c#
var isJoined = element1.AreElementsJoined(element2);
```

The **GetJoinedElements()** method returns all elements joined to given element.

```c#
var elements = element1.GetJoinedElements();
```

The **SwitchJoinOrder()** method reverses the order in which two elements are joined.

```c#
element1.SwitchJoinOrder();
```

The **IsCuttingElementInJoin()** method determines whether the first of two joined elements is cutting the second element.

```c#
var isCutting = element1.IsCuttingElementInJoin(element2);
```

The **SetCoordinateX(), SetCoordinateY(), SetCoordinateZ()** methods creates an instance of a curve with a new coordinate.

```c#
var newLine = line.SetCoordinateX(1);
var newLine = line.SetCoordinateY(1);
var newLine = line.SetCoordinateZ(1);
var newArc = arc.SetCoordinateX(1);
var newArc = arc.SetCoordinateY(1);
var newArc = arc.SetCoordinateZ(1);
```

### <a id="RibbonExtensions">Ribbon Extensions</a>

The **CreatePanel()** method create a new panel in the default AddIn tab or the specified tab. If the panel exists on the ribbon, the method will return it.

```c#
application.CreatePanel("Panel name");
application.CreatePanel("Panel name", "Tab name");
```

The **AddPushButton()** method adds a PushButton to the ribbon.

```c#
panel.AddPushButton(typeof(Command), "Button text");
panel.AddPushButton<Command>("Button text");
pullDownButton.AddPushButton(typeof(Command), "Button text");
pullDownButton.AddPushButton<Command>("Button text");
```

The **AddPullDownButton()** method adds a PullDownButton to the ribbon.

```c#
panel.AddPullDownButton("Button name", "Button text");
```

The **AddSplitButton()** method adds a SplitButton to the ribbon.

```c#
panel.AddSplitButton("Button name", "Button text");
```

The **AddRadioButtonGroup()** method adds a RadioButtonGroup to the ribbon.

```c#
panel.AddRadioButtonGroup("Button name");
```

The **AddComboBox()** method adds a ComboBox to the ribbon.

```c#
panel.AddComboBox("Button name");
```

The **AddTextBox()** method adds a TextBox to the ribbon.

```c#
panel.AddTextBox("Button name");
```

The **SetImage()** method adds an image to the RibbonButton.

```c#
button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16.png");
button.SetImage("http://example.com/RibbonIcon16.png");
button.SetImage("C:\Pictures\RibbonIcon16.png");
```

The **SetLargeImage()** method adds a large image to the RibbonButton.

```c#
button.SetLargeImage("/RevitAddIn;component/Resources/Icons/RibbonIcon32.png");
button.SetLargeImage("http://example.com/RibbonIcon32.png");
button.SetLargeImage("C:\Pictures\RibbonIcon32.png");
```

### <a id="UnitExtensions">Unit Extensions</a>

The **FromMillimeters()** method converts millimeters to internal Revit number format (feet).

```c#
double(69).FromMillimeters() => 0.226
```

The **ToMillimeters()** method converts a Revit internal format value (feet) to millimeters.

```c#
double(69).ToMillimeters() => 21031
```

The **FromMeters()** method converts meters to internal Revit number format (feet).

```c#
double(69).FromMeters() => 226.377
```

The **ToMeters()** method converts a Revit internal format value (feet) to meters.

```c#
double(69).ToMeters() => 21.031
```

The **FromInches()** method converts inches to internal Revit number format (feet).

```c#
double(69).FromInches() => 5.750
```

The **ToInches()** method converts a Revit internal format value (feet) to inches.

```c#
double(69).ToInches() => 827.999
```

The **FromDegrees()** method converts degrees to internal Revit number format (radians).

```c#
double(69).FromDegrees() => 1.204
```

The **ToDegrees()** method converts a Revit internal format value (radians) to degrees.

```c#
double(69).ToDegrees() => 3953
```

The **FormatUnit()** method formats a number with units into a string.

```c#
document.FormatUnit(SpecTypeId.Length, 69, false) => 21031
document.FormatUnit(SpecTypeId.Length, 69, false, new FormatValueOptions {AppendUnitSymbol = true}) => 21031 mm
```

### <a id="HostExtensions">Host Extensions</a>

The **GetBottomFaces()** method returns the bottom faces for the host object.

```c#
floor.Cast<HostObject>().GetBottomFaces();
```

The **GetTopFaces()** method returns the top faces for the host object.

```c#
floor.Cast<HostObject>().GetTopFaces();
```

The **GetSideFaces()** method returns the major side faces for the host object.

```c#
wall.Cast<HostObject>().GetSideFaces(ShellLayerType.Interior);
```

### <a id="LabelExtensions">Label Extensions</a>

The **ToLabel()** method convert Enum to user-visible name.

```c#
BuiltInCategory.OST_Walls.ToLabel() => "Walls"
BuiltInParameter.WALL_TOP_OFFSET.ToLabel() => "Top Offset"
BuiltInParameter.WALL_TOP_OFFSET.ToLabel(LanguageType.Russian) => "Смещение сверху"
BuiltInParameterGroup.PG_LENGTH.ToLabel() => "Length"
DisplayUnitType.DUT_KILOWATTS.ToLabel() => "Kilowatts"
ParameterType.Length.ToLabel() => "Length"

DisciplineTypeId.Hvac.ToLabel() => "HVAC"
GroupTypeId.Geometry.ToLabel() => "Dimensions"
ParameterTypeId.DoorCost.ToLabel() => "Cost"
SpecTypeId.SheetLength.ToLabel() => "Sheet Length"
SymbolTypeId.Hour.ToLabel() => "h"
UnitTypeId.Hertz.ToLabel() => "Hertz"
```

The **ToDisciplineLabel()** method convert ForgeTypeId to user-visible name a discipline.

```c#
DisciplineTypeId.Hvac.ToDisciplineLabel() => "HVAC"
```

The **ToGroupLabel()** method convert ForgeTypeId to user-visible name for a built-in parameter group.

```c#
GroupTypeId.Geometry.ToGroupLabel() => "Dimensions"
```

The **ToParameterLabel()** method convert ForgeTypeId to user-visible name for a built-in parameter.

```c#
ParameterTypeId.DoorCost.ToParameterLabel() => "Cost"
```

The **ToSpecLabel()** method convert ForgeTypeId to user-visible name for a spec.

```c#
SpecTypeId.SheetLength.ToSpecLabel() => "Sheet Length"
```

The **ToSymbolLabel()** method convert ForgeTypeId to user-visible name for a symbol.

```c#
SymbolTypeId.Hour.ToSymbolLabel() => "h"
```

The **ToUnitLabel()** method convert ForgeTypeId to user-visible name for a unit.

```c#
UnitTypeId.Hertz.ToUnitLabel() => "Hertz"
```

### <a id="SolidExtensions">Solid Extensions</a>

The **Clone()** method creates a new Solid which is a copy of the input Solid.

```c#
solid.Clone();
```

The **CreateTransformed()** method creates a new Solid which is the transformation of the input Solid.

```c#
solid.CreateTransformed(Transform.CreateRotationAtPoint());
solid.CreateTransformed(Transform.CreateReflection());
```

The **SplitVolumes()** method splits a solid geometry into several solids.

```c#
solid.SplitVolumes();
```

The **IsValidForTessellation()** method tests if the input solid or shell is valid for tessellation.

```c#
solid.IsValidForTessellation();
```

The **TessellateSolidOrShell()** method facets (i.e., triangulates) a solid or an open shell.

```c#
solid.TessellateSolidOrShell();
```

The **FindAllEdgeEndPointsAtVertex()** method find all EdgeEndPoints at a vertex identified by the input EdgeEndPoint.

```c#
edgeEndPoint.FindAllEdgeEndPointsAtVertex();
```

### <a id="SchemaExtensions">Schema Extensions</a>

The **SaveEntity()** method stores data in the element. Existing data is overwritten.

```c#
var schema = Schema.Lookup(guid);
document.ProjectInformation.SaveEntity(schema, "data", "schemaField");
door.SaveEntity(schema, "white", "doorColorField");
```

The **LoadEntity()** method retrieves the value stored in the schema from the element.

```c#
var schema = Schema.Lookup(guid);
var data = document.ProjectInformation.LoadEntity<string>(schema, "schemaField");
var color = door.LoadEntity<string>(schema, "doorColorField");
```

### <a id="ApplicationExtensions">Application Extensions</a>

The **Show()** method Opens a window and returns without waiting for the newly opened window to close.
Sets the owner of a child window. Applicable for modeless windows to be attached to Revit.

```c#
new RevitAddinView.Show(uiApplication.MainWindowHandle)
```

### <a id="CollectorExtensions">Collector Extensions</a>

This set of extensions encapsulates all the work of searching for elements in the Revit database.

The **GetElements()** a generic method which constructs a new FilteredElementCollector that will search and filter the set of elements in a document.
Filter criteria are not applied to the method.

```c#
document.GetElements().WhereElementIsViewIndependent().ToElements();
```

The remaining methods contain a ready implementation of the collector, with filters applied:

```c#
document.GetInstances();
document.GetInstances(new ElementParameterFilter());
document.GetInstances(new []{elementParameterFilter, logicalFilter});

document.GetInstances(BuiltInCategory.OST_Walls);
document.GetInstances(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.GetInstances(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});    

document.EnumerateInstances();
document.EnumerateInstances(new ElementParameterFilter());
document.EnumerateInstances(new []{elementParameterFilter, logicalFilter});

document.EnumerateInstances(BuiltInCategory.OST_Walls);
document.EnumerateInstances(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.EnumerateInstances(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});   

document.EnumerateInstances<Wall>();
document.EnumerateInstances<Wall>(new ElementParameterFilter());
document.EnumerateInstances<Wall>(new []{elementParameterFilter, logicalFilter});

document.EnumerateInstances<Wall>(BuiltInCategory.OST_Walls);
document.EnumerateInstances<Wall>(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.EnumerateInstances<Wall>(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});   

document.GetInstanceIds();
document.GetInstanceIds(new ElementParameterFilter());
document.GetInstanceIds(new []{elementParameterFilter, logicalFilter});

document.GetInstanceIds(BuiltInCategory.OST_Walls);
document.GetInstanceIds(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.GetInstanceIds(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});    

document.EnumerateInstanceIds();
document.EnumerateInstanceIds(new ElementParameterFilter());
document.EnumerateInstanceIds(new []{elementParameterFilter, logicalFilter});

document.EnumerateInstanceIds(BuiltInCategory.OST_Walls);
document.EnumerateInstanceIds(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.EnumerateInstanceIds(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});   

document.EnumerateInstanceIds<Wall>();
document.EnumerateInstanceIds<Wall>(new ElementParameterFilter());
document.EnumerateInstanceIds<Wall>(new []{elementParameterFilter, logicalFilter});

document.EnumerateInstanceIds<Wall>(BuiltInCategory.OST_Walls);
document.EnumerateInstanceIds<Wall>(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.EnumerateInstanceIds<Wall>(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});        
document.GetTypes();
document.GetTypes(new ElementParameterFilter());
document.GetTypes(new []{elementParameterFilter, logicalFilter});

document.GetTypes(BuiltInCategory.OST_Walls);
document.GetTypes(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.GetTypes(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});    

document.EnumerateTypes();
document.EnumerateTypes(new ElementParameterFilter());
document.EnumerateTypes(new []{elementParameterFilter, logicalFilter});

document.EnumerateTypes(BuiltInCategory.OST_Walls);
document.EnumerateTypes(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.EnumerateTypes(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});   

document.EnumerateTypes<WallType>();
document.EnumerateTypes<WallType>(new ElementParameterFilter());
document.EnumerateTypes<WallType>(new []{elementParameterFilter, logicalFilter});

document.EnumerateTypes<WallType>(BuiltInCategory.OST_Walls);
document.EnumerateTypes<WallType>(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.EnumerateTypes<WallType>(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});   

document.GetTypeIds();
document.GetTypeIds(new ElementParameterFilter());
document.GetTypeIds(new []{elementParameterFilter, logicalFilter});

document.GetTypeIds(BuiltInCategory.OST_Walls);
document.GetTypeIds(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.GetTypeIds(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});    

document.EnumerateTypeIds();
document.EnumerateTypeIds(new ElementParameterFilter());
document.EnumerateTypeIds(new []{elementParameterFilter, logicalFilter});

document.EnumerateTypeIds(BuiltInCategory.OST_Walls);
document.EnumerateTypeIds(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.EnumerateTypeIds(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});   

document.EnumerateTypeIds<WallType>();
document.EnumerateTypeIds<WallType>(new ElementParameterFilter());
document.EnumerateTypeIds<WallType>(new []{elementParameterFilter, logicalFilter});

document.EnumerateTypeIds<WallType>(BuiltInCategory.OST_Walls);
document.EnumerateTypeIds<WallType>(BuiltInCategory.OST_Walls, new ElementParameterFilter());
document.EnumerateTypeIds<WallType>(BuiltInCategory.OST_Walls, new []{elementParameterFilter, logicalFilter});
```

**Remarks**: Get methods are faster than Enumerate due to RevitApi internal optimizations. 
However, enumeration allows for more flexibility in finding elements.
Don't try to call ```GetInstances().Select().Tolist()``` instead of ```EnumerateInstances().Select().Tolist()```, you will degrade performance.

### <a id="ImperialExtensions">Imperial Extensions</a>

The **ToFraction()** method converts a number to Imperial fractional format.

```c#
int(1).ToFraction() => 1’-0〞
double(0.0123).ToFraction() => 0 5/32〞
double(15.125).ToFraction() => 15’-1 1/2〞
double(-25.222).ToFraction() => 25’-2 21/32〞
double(-25.222).ToFraction(4) => 25’-2 3/4〞
```

The **FromFraction()** method converts the textual representation of the Imperial system number to number.

```c#
string("").FromFraction() => double(0)
string(1 17/64〞).FromFraction() => double(0.105)
string(1’1.75).FromFraction() => double(1.145)
string(-69’-69〞).FromFraction() => double(-74.75)

string(-2’-1 15/64〞).FromFraction(out var value) => true
string("-").FromFraction(out var value) => true
string(".").FromFraction(out var value) => false
string("value").FromFraction(out var value) => false
string(null).FromFraction(out var value) => false
```

### <a id="DoubleExtensions">Double Extensions</a>

The **Round()** method rounds the value to the specified precision or 1e-9 precision specified in Revit Api.

```c#
double(6.56170000000000000000000001).Round() => 6.5617
double(6.56170000000000000000000001).Round(0) => 7
```

The **IsAlmostEqual()** method compares two numbers within specified precision or 1e-9 precision specified in Revit Api.

```c#
double(6.56170000000000000000000001).IsAlmostEqual(6.5617) => true
double(6.56170000000000000000000001).IsAlmostEqual(6.6, 1e-1) => true
```

### <a id="StringExtensions">String Extensions</a>

The **IsNullOrEmpty()** method same as string.IsNullOrEmpty().

```c#
string("").IsNullOrEmpty() => true
string(null).IsNullOrEmpty() => true
```

The **IsNullOrWhiteSpace()** method same as string.IsNullOrWhiteSpace().

```c#
string(" ").IsNullOrWhiteSpace() => true
string(null).IsNullOrWhiteSpace() => true
```

The **AppendPath()** method combines 2 paths.

```c#
"C:\Folder".AppendPath("AddIn").AppendPath("file.txt") => "C:\Folder\AddIn\file.txt"
```

The **Contains()** indicating whether a specified substring occurs within this string with StringComparison support.

```c#
"Revit extensions".Contains("Revit", StringComparison.OrdinalIgnoreCase) => true
"Revit extensions".Contains("revit", StringComparison.OrdinalIgnoreCase) => true
"Revit extensions".Contains("REVIT", StringComparison.OrdinalIgnoreCase) => true
"Revit extensions".Contains("invalid", StringComparison.OrdinalIgnoreCase) => false
```

## Technology Sponsors

Thanks to [JetBrains](https://jetbrains.com) for providing licenses for [Rider](https://jetbrains.com/rider) and [dotUltimate](https://www.jetbrains.com/dotnet/) tools, which both
make open-source development a real pleasure!