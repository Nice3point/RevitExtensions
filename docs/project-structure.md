# Project Structure

Nice3point.Revit.Extensions is a single-purpose library of fluent extension methods over the Revit API.
The solution separates the shipped library from its tests and benchmarks.
Keep each piece of code in the project that owns its runtime responsibility.

## Solution Groups

* **`/source`**: the shipped NuGet package.
    * The extensions project holds every public extension method over the Revit API. It is the only package-producing project.
* **`/tests`**: the verification projects.
    * The unit-test project runs custom logic inside a real Revit process with TUnit on the Revit thread.
    * The benchmark project measures extension performance with BenchmarkDotNet.
* **`/build`**: the ModularPipelines build that compiles, packages, and publishes.
* **Root**: build and package configuration, the README and CHANGELOG, the agent guidelines, and the CI workflows.

## Source Layout

Extension classes are grouped by the Revit API surface they wrap, so members surface in IntelliSense on the type they extend.
One file holds the extensions for a single Revit type or utility class, named `<Type>Extensions.cs`.

* The root of the extensions project holds extensions over core Revit API types such as elements, identifiers, documents, parameters, categories, colors, and geometry.
* A dedicated folder holds fluent wrappers over the Revit static `*Utils` classes, one file per utility class.
* A dedicated folder holds fluent wrappers over the Revit `*Manager` types.
* A dedicated folder holds Ribbon and UI Framework helpers, which are UI-only and not unit-tested.
* A non-public folder holds implementation detail such as reflection accessors, format helpers, and ribbon infrastructure. It never appears in the public surface.

## Change Placement

* An extension over a core Revit type goes in the matching `<Type>Extensions.cs` at the project root, or a new file when none exists.
* A wrapper over a Revit `*Utils` class goes in the utilities folder, one file per utility class.
* A wrapper over a Revit `*Manager` type goes in the managers folder.
* A Ribbon or UI Framework helper goes in the UI Framework folder.
* A private implementation detail, reflection accessor, or format helper goes in the internal folder and stays non-public.
* Unit coverage for custom logic goes in the unit-test project. A performance measurement goes in the benchmark project.
