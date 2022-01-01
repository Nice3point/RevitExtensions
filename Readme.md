<h3 align="center"><img src="https://user-images.githubusercontent.com/20504884/147851991-6da425cf-44ee-4517-86ad-a6d22253d197.png" width="500px"></h3>

# Improve your experience with Revit API now

<p align="center">
  <a href="https://www.nuget.org/packages/Nice3point.Revit.Extensions"><img src="https://img.shields.io/nuget/v/Nice3point.Revit.Extensions?style=for-the-badge"></a>
  <a href="https://www.nuget.org/packages/Nice3point.Revit.Extensions"><img src="https://img.shields.io/nuget/dt/Nice3point.Revit.Extensions?style=for-the-badge"></a>
  <a href="https://github.com/Nice3point/RevitExtensions/commits/main"><img src="https://img.shields.io/github/last-commit/Nice3point/RevitExtensions?style=for-the-badge"></a>
</p>

Extensions minimize the writing of repetitive code, add new methods not included in RevitApi, and also allow you to write chained methods without worrying about API versioning:

<pre><code class='language-cs'>new ElementId(123469)
.ToElement(document)
.GetParameter(BuiltInParameter.ALL_MODEL_URL)
.AsDouble()
.Round()
.ToMeters()
</code></pre>

Extensions include annotations to help ReShaper parse your code and signal when a method may return null or the value returned by the method is not used in your code.

## Installation

You can install Extensions as a [nuget package](https://www.nuget.org/packages/Nice3point.Revit.Extensions).

Packages are compiled for a specific version of Revit, to support different versions of libraries in one project, use a floating version.

```msbuild
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
- [Imperial Extensions](#ImperialExtensions)
- [Double Extensions](#DoubleExtensions)
- [String Extensions](#StringExtensions)

### <a id="ElementExtensions">Element Extensions</a>

The **GetParameter()** methods allow you to get a parameter from an element, regardless of whether the parameter is an instance or a type.

```code
element.GetParameter(BuiltInParameter.ALL_MODEL_URL);
element.GetParameter("URL");
```

### <a id="ElementIdExtensions">ElementId Extensions</a>

The **ToElement()** methods allow you to get an element from the Id for a specified document and convert to a type if necessary.

```cs
Element element = elementId.ToElement(document);
Material material = elementId.ToElement<Material>(document);
```

The **AreEquals()** methods allow you to check if an ID matches BuiltInСategory or BuiltInParameter.

```cs
categoryId.AreEquals(BuiltInCategory.OST_Walls);
parameterId.AreEquals(BuiltInParameter.WALL_BOTTOM_IS_ATTACHED);
```

### <a id="GeometryExtensions">Geometry Extensions</a>

The **Distance()** method allows you to get the distance between two lines. The lines are considered endless. The method is not included in RevitApi.

```cs
var line1 = Line.CreateBound(new XYZ(0,0,1), new XYZ(1,1,1));
var line2 = Line.CreateBound(new XYZ(1,2,2), new XYZ(1,2,2));
var distance = line1.Distance(line2);
```

### <a id="RibbonExtensions">Ribbon Extensions</a>

The **AddPushButton()** method adds a PushButton to the ribbon. The code is significantly simplified compared to the original method.

```cs
panel.AddPushButton(typeof(Command), "Button text");
```

The **AddPullDownButton()** method adds a PullDownButton to the ribbon. Also added a method for adding a PushButton to this button.

```cs
panel.AddPullDownButton("Button name", "Button text");

panel.AddPullDownButton("Button name", "Button text")
    .AddPushButton(typeof(Command), "Button text")
```

The **AddSplitButton()** method adds a SplitButton to the ribbon.

```cs
panel.AddSplitButton("Button name", "Button text");
```

The **AddRadioButtonGroup()** method adds a RadioButtonGroup to the ribbon.

```cs
panel.AddRadioButtonGroup("Button name");
```

The **AddComboBox()** method adds a ComboBox to the ribbon.

```cs
panel.AddComboBox("Button name");
```

The **AddTextBox()** method adds a TextBox to the ribbon.

```cs
panel.AddTextBox("Button name");
```

The **SetImage()** method adds an image to the RibbonButton.

```cs
button.SetImage("/RevitAddIn;component/Resources/Icons/RibbonIcon16.png");
```

The **SetLargeImage()** method adds a large image to the RibbonButton.

```cs
button.SetLargeImage("/RevitAddIn;component/Resources/Icons/RibbonIcon32.png");
```

### <a id="UnitExtensions">Unit Extensions</a>

The **FromMillimeters()** method converts millimeters to internal Revit number format (feet).

```cs
69.FromMillimeters() => 0.226
```

The **ToMillimeters()** method converts a Revit internal format value (feet) to millimeters.

<pre><code class='language-cs'>69.ToMillimeters() => 21031
</code></pre>

The **FromMeters()** method converts meters to internal Revit number format (feet).

```cs
69.FromMeters() => 226.377
```

The **ToMeters()** method converts a Revit internal format value (feet) to meters.

```cs
69.ToMeters() => 21.031
```

The **FromInches()** method converts inches to internal Revit number format (feet).

```cs
69.FromInches() => 5.750
```

The **ToInches()** method converts a Revit internal format value (feet) to inches.

```cs
69.ToInches() => 827.999
```

The **FromDegrees()** method converts degrees to internal Revit number format (radians).

```cs
69.FromDegrees() => 1.204
```

The **ToDegrees()** method converts a Revit internal format value (radians) to degrees.

```cs
69.ToDegrees() => 3953
```

### <a id="ImperialExtensions">Imperial Extensions</a>

The **ToFraction()** method converts a number to Imperial fractional format

```cs
1.ToFraction() => 1'-0"
0.0123.ToFraction() => 0 5/32"
15.125.ToFraction() => 15'-1 1/2"
-25.222.ToFraction() => 25'-2 21/32"
-25.222.ToFraction(4) => 25'-2 3/4"
```

The **FromFraction()** method converts the textual representation of the Imperial system number to number

```cs
"".FromFraction() => 0
"1 17/64\"".FromFraction() => 0.105
"1'1.75".FromFraction() => 1.145
"-69'-69".FromFraction() => -74.75
```

### <a id="DoubleExtensions">Double Extensions</a>

The **Round()** method rounds the value to the specified precision or 1e-9 precision specified in Revit Api

```cs
6.56170000000000000000000001.Round() => 6.5617
6.56170000000000000000000001.Round(0) => 7
```

The **IsAlmostEqual()** method compares two numbers within specified precision or 1e-9 precision specified in Revit Api

```cs
6.56170000000000000000000001.IsAlmostEqual(6.5617) => true
6.56170000000000000000000001.IsAlmostEqual(6.6, 1e-1) => true
```

### <a id="StringExtensions">String Extensions</a>

The **IsNullOrEmpty()** method same as string.IsNullOrEmpty().

```cs
"".IsNullOrEmpty() => true
null.IsNullOrEmpty() => true
```

The **IsNullOrWhiteSpace()** method same as string.IsNullOrWhiteSpace().

```cs
" ".IsNullOrEmpty() => true
null.IsNullOrEmpty() => true
```

The **AppendPath()** method combines 2 paths.

```cs
"C:\Folder".AppendPath("AddIn").AppendPath("file.txt")) => "C:\Folder\AddIn\file.txt"
```

The **Contains()** indicating whether a specified substring occurs within this string with StringComparison support.

```cs
"some Test string".Contains("test", StringComparison.OrdinalIgnoreCase)) => true
"some test string".Contains("test", StringComparison.OrdinalIgnoreCase)) => true
"some TEST string".Contains("TeSt", StringComparison.OrdinalIgnoreCase)) => true
"some TEST string".Contains("invalid", StringComparison.OrdinalIgnoreCase)) => false
```