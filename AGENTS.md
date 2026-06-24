# Nice3point.Revit.Extensions Agent Instructions

Nice3point.Revit.Extensions is a public NuGet library of fluent extension methods over the Revit API. The shipped project wraps Revit API types, utilities, and managers behind a type-safe, chainable surface that adds ergonomics, never new behavior.

## Non-Negotiables

* **Wrap the Revit API, never reimplement it.** An extension calls the underlying Revit API directly and adds only ergonomics. See [Architecture](./docs/architecture.md).
* **Use C# extension blocks.** Author every new extension as an `extension(Type) { }` member. A mutation method returns the source object so calls chain.
* **Provide the `ElementId` sibling.** When a method works through `element.Document` and `element.Id`, add a sibling `ElementId` overload that takes an explicit `Document`, with a name that stays unambiguous on `ElementId`.
* **Never break an existing public API.** Deprecate with `[Obsolete]` plus `CodeTemplate` and keep the old method functional. See [Backward Compatibility](./docs/backward-compatibility.md).
* **Attributes carry the contract.** Mark public extension classes `[PublicAPI]` and read-only methods `[Pure]`.
* **Every extension compiles under every supported configuration.** Gate version-specific Revit APIs with `#if REVIT2024_OR_GREATER`-style directives. See [Revit Best Practices](./docs/revit-best-practices.md).
* **Performance matters.** The library runs on Revit hot paths. Stay allocation-conscious and follow optimized API access patterns.
* **Verify unfamiliar APIs.** When unsure of a Revit or .NET API's behavior or signature, confirm it before use. Search the web for the official docs. To read a referenced library's source, query GitHub with `gh` (`gh api`, `gh search code`). If `gh` is unavailable, search the web or ask. Never inspect compiled DLLs or XML extracted from NuGet packages.
* **Tests ship with every change.** Cover custom logic only. See [Testing](./docs/testing.md) and [Benchmarks](./docs/benchmarks.md).
* **Keep docs in sync.** A public-surface change updates `README.md`, `CHANGELOG.md`, and the XML docs in the same commit. See [Documentation](./docs/documentation.md).

## Build

The build is a ModularPipelines project. Run `dotnet run -c Release` from the `build` directory to compile.

## Specialized Docs

Read the matching file before related work.

* [Project Structure](./docs/project-structure.md). Solution layout, the source grouping by Revit type, and change placement.
* [Architecture](./docs/architecture.md). Design goals, the extension model, chained calls, and the `ElementId` overload pattern.
* [Code Style](./docs/code-style.md). C# conventions, naming, attributes, the extension-block pattern, and error handling.
* [Backward Compatibility](./docs/backward-compatibility.md). The Obsolete plus CodeTemplate pattern and breaking-change rules.
* [Revit Best Practices](./docs/revit-best-practices.md). Revit API usage, the version matrix, threading, and performance.
* [Testing](./docs/testing.md). Unit tests with TUnit on the Revit thread.
* [Benchmarks](./docs/benchmarks.md). When and how to benchmark with BenchmarkDotNet.
* [Documentation](./docs/documentation.md). XML docs, the README catalog, and the CHANGELOG.
* [Package Management](./docs/package-management.md). Centralized versions, Revit-version packages, and dependencies.
