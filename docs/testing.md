# Testing

Tests cover the library's custom logic, not the Revit API itself. Every change ships with tests. They run inside a real Revit process against real Revit objects, never against mocks.

## What to Test

* **Custom logic only.** Test an extension that adds behavior beyond a direct API call, such as a containment check, a distance calculation, or an ordering. Skip a thin pass-through wrapper that only forwards to a Revit API call.
* **Edge cases.** Null inputs, empty collections, and boundary values.
* **No UI tests.** Skip Ribbon, context menu, and `UIApplication` extensions.

## Framework

* **TUnit on the Microsoft.Testing.Platform**, with Nice3point.TUnit.Revit for Revit API access. Assertions use the TUnit API: `await Assert.That(actual).IsNotNull()`.
* Tests live in the unit-test project under `/tests`.
* The assembly applies `[assembly: TestExecutor<RevitThreadExecutor>]` (`TestsConfiguration.cs`), so tests run on the Revit thread. An individual Revit-thread hook uses `[HookExecutor<RevitThreadExecutor>]`.

## Conventions

* A fixture is a `public sealed class` that inherits one of the shared Revit bases.
* A test method is named `<Method>_<Condition>_<Expected>`, marked `[Test]`, and returns `async Task`.
* Split each test body into `// Arrange`, `// Act`, and `// Assert` blocks.

```csharp
public sealed class GeometryExtensionsTests : RevitApiTest
{
    [Test]
    public async Task Distance_ParallelLines_ReturnsCorrectDistance()
    {
        // Arrange
        var first = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(10, 0, 0));
        var second = Line.CreateBound(new XYZ(0, 5, 0), new XYZ(10, 5, 0));

        // Act
        var distance = first.Distance(second);

        // Assert
        await Assert.That(distance).IsEqualTo(5).Within(1e-9);
    }
}
```

## Sample-Driven Context

A fixture inherits a shared sample base under the test project's `Abstractions` folder when it needs a loaded document:

* `RevitApiTest` runs against a fresh in-process Revit API context.
* `RevitModelSampleTest` opens `.rvt` sample models.
* `RevitFamilySampleTest` opens `.rfa` sample families.
* `RevitApiReportTest` drives report-style API coverage.

Sample files resolve from the installed Revit `Samples` folder via `RevitEnvironment.MajorVersion`. Each document is copied to a temp path, opened under a failure-suppression scope, and closed and deleted in the matching teardown hook. When the `Samples` folder is missing, the sample arrays are empty, so guard a test to skip cleanly rather than fail.

```csharp
public sealed class MyExtensionTests : RevitModelSampleTest
{
    [Test]
    public async Task MyExtension_ValidModel_ReturnsExpectedResult()
    {
        foreach (var document in ModelDocuments.Values)
        {
            // Arrange
            // Act
            var result = document.MyExtension();

            // Assert
            await Assert.That(result).IsNotNull();
        }
    }
}
```

## Version Coverage

* Tests build per Revit configuration (`Debug.RNN`). Prefer the latest supported debug configuration unless the change is version-specific.
* When changing version-specific behavior, run or document coverage for each affected `Debug.RNN` configuration the project declares.

## Build and Test

TUnit runs on the Microsoft.Testing.Platform, so `dotnet test` runs the suite directly. Pass the target Revit configuration, for example `dotnet test -c Debug.R27`.
