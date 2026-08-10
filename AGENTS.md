# Nice3point.Revit.Extensions

Nice3point.Revit.Extensions is a public NuGet library of fluent extension methods over the Revit API.

## Non-negotiables

* Wrap the Revit API directly: call the underlying API and return its result. Add ergonomics, never new behavior, and never a reimplementation.
* When a member's body is a direct call into one Revit API member, document it with `<inheritdoc cref="Fully.Qualified.Member(ParamTypes)"/>` instead of a hand-written `<summary>`. Add a supplemental `<example>` or `<remarks>` alongside `<inheritdoc/>` only when the extension needs context the wrapped member's docs don't cover.
* Author each member as an extension method with a full method body.
* A method that would return `void` returns its source object if it makes sense and is consistent with the method's name..
* Never break the public surface. Deprecate a renamed member with `[Obsolete]` with a JetBrains `CodeTemplate` auto-conversion and keep the member functional; the obsolete member calls the original Revit API and never recurses into the new extension. And never change a signature or return type.
* Every extension compiles under every supported Revit configuration.
* Test only custom logic; skip Revit-API wrappers and all UI extension.
* Confirm an unfamiliar Revit, .NET, or dependency API before use through official docs or `gh` (`gh api`, `gh search code`).
* A public-surface change updates `Readme.md`, `Changelog.md`, and the XML docs in the same commit.

## Repository map

* `source/Nice3point.Revit.Extensions/` — the extensions library, packed as a NuGet package. It exposes fluent, chainable extension methods over Revit API types, utilities for users.
* `tests/Nice3point.Revit.Extensions.Tests/` — the test project that tests the library.
* `tests/Nice3point.Revit.Extensions.Benchmarks/` — BenchmarkDotNet measurements for performance-sensitive extensions.
* `build/` — the ModularPipelines build; the supported Revit versions live in `build/appsettings.json`.
* Root — `Directory.Build.props`, `Directory.Packages.props`, `global.json`, `Readme.md`, `Changelog.md`, CI workflows.

## Build and verify

* Build: `dotnet build -c Release.R##`, where the `R##` suffix is the Revit year (`R27` targets Revit 2027).
* Test: `dotnet test -c Release.R##`; required a matching licensed Revit installation.
