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
- [Imperial Extensions](#ImperialExtensions)
- [Double Extensions](#DoubleExtensions)
- [String Extensions](#StringExtensions)

### <a id="ElementExtensions">Element Extensions</a>

The **Cast<T>()** method Cast the element to the specified type.

```c#
Wall wall = element.Cast<Wall>();
Floor floor = element.Cast<Floor>();
HostObject hostObject = element.Cast<HostObject>();
```

The **GetParameter()** method allow you to get a parameter from an element, regardless of whether the parameter is in an instance or a type.

```c#
element.GetParameter(ParameterTypeId.AllModelUrl, includeType);
element.GetParameter(BuiltInParameter.ALL_MODEL_URL);
element.GetParameter("URL");
```

The **Copy()** method allow you copy an element to a new location.

```c#
element.Copy(1, 1, 0);
element.Copy(new XYZ(1, 1, 0));
```

The **Mirror()** method allow you mirror an element.

```c#
element.Mirror(plane);
```

The **Move()** method allow you move an element to a new location.

```c#
element.Move(1, 1, 0);
element.Move(new XYZ(1, 1, 0));
```

The **Rotate()** method allow you to rotate an element.

```c#
element.Rotate(axis, angle);
```

The **CanBeMirrored()** method determines whether element can be mirrored.

```c#
element.CanBeMirrored();
```

### <a id="ElementIdExtensions">ElementId Extensions</a>

The **ToElement()** method allow you to get an element from the Id for a specified document and convert to a type if necessary.

```c#
Element element = elementId.ToElement(document);
Wall wall = elementId.ToElement<Wall>(document);
```

The **AreEquals()** method allow you to check if an ID matches BuiltInСategory or BuiltInParameter.

```c#
categoryId.AreEquals(BuiltInCategory.OST_Walls);
parameterId.AreEquals(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);
```

### <a id="GeometryExtensions">Geometry Extensions</a>

The **Distance()** method allows you to get the distance between two lines. The lines are considered endless.

```c#
var line1 = Line.CreateBound(new XYZ(0,0,1), new XYZ(1,1,1));
var line2 = Line.CreateBound(new XYZ(1,2,2), new XYZ(1,2,2));
var distance = line1.Distance(line2);
```

### <a id="RibbonExtensions">Ribbon Extensions</a>

The **CreatePanel()** method allow you to create a new panel in the default AddIn tab or the specified tab. If the panel exists on the ribbon, the method will return it.

```c#
application.CreatePanel("Panel name");
application.CreatePanel("Panel name", "Tab name");
```

The **AddPushButton()** method adds a PushButton to the ribbon.

```c#
panel.AddPushButton(typeof(Command), "Button text");
```

The **AddPullDownButton()** method adds a PullDownButton to the ribbon. Also added a method for adding a PushButton to this button.

```c#
panel.AddPullDownButton("Button name", "Button text");

panel.AddPullDownButton("Button name", "Button text")
    .AddPushButton(typeof(Command), "Button text");
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
```

```c#
button.SetImage("Resources.RibbonIcon");
```

The **SetLargeImage()** method adds a large image to the RibbonButton.

```c#
button.SetLargeImage("/RevitAddIn;component/Resources/Icons/RibbonIcon32.png");
```

```c#
button.SetLargeImage("Resources.RibbonIcon");
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
string(" ").IsNullOrEmpty() => true
string(null).IsNullOrEmpty() => true
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