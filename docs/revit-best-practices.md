# Revit Best Practices

The library is consumed inside the Revit process, often on hot paths. An extension respects Revit API constraints and stays allocation-conscious.

## Wrap the Revit API

* An extension wraps the Revit API and does not reimplement it. It calls the underlying API directly.
* Do not hide an expensive API call behind a harmless-looking name. The cost of a call stays discoverable from its name.
* Read-only operations are `[Pure]`. Let Revit exceptions propagate and document them. See [Code Style](./code-style.md).

## Threading & the API Context

* The Revit API may only be modified from the Revit API context. An extension that mutates a model assumes the caller already holds an open transaction in that context.
* An extension that opens a transaction keeps its scope minimal and never leaves one open across a return.

## Revit Versions

The active version comes from the `$(RevitVersion)` build property. The project (SDK `Nice3point.Revit.Sdk`) declares the full `Debug.RNN`/`Release.RNN` configuration list across the supported Revit versions.

* Use conditional compilation (`#if REVIT2024_OR_GREATER`, and similar) only where the Revit API genuinely differs between versions.
* Keep shared behavior version-neutral wherever possible.
* Apply directives consistently across related members so a type's surface stays coherent per version.
* Every extension compiles under every declared `Debug.RNN`/`Release.RNN` configuration.
* Version-specific package versions belong in `Directory.Packages.props`. See [Package Management](./package-management.md).

## Performance

* **Avoid LINQ on hot paths.** Use a traditional loop where allocations or iterator overhead matter.
* **Pre-size a collection** when the count is known.
* **Use `StringBuilder`** for non-trivial string concatenation.
* **Prefer a batch Revit API** over a per-element call.
* Validate a performance-sensitive change with a benchmark. See [Benchmarks](./benchmarks.md).

## Internal Helpers

* Reflection-based accessors, color and format utilities, and ribbon infrastructure live in the internal folder and stay non-public.
* Do not expose an internal type through the public surface.
