# Documentation

These rules govern every piece of prose the package ships: XML doc comments, `README.md`, and `CHANGELOG.md`. Each format adds its own rules on top of the shared set.

A public-surface change updates the README, the CHANGELOG, and the affected XML docs in the same commit. Documentation that lags the code is a defect.

## Shared Prose Rules

* **State what, not how.** Describe observable behavior and contract, never the implementation. A summary survives an implementation rewrite unchanged.
* **Plain technical English.** No corporate jargon, no marketing tone.
* **No filler.** Omit obvious statements. State only what a reader cannot infer from the signature.
* **Third-person present indicative.** Write "Retrieves the element", not "Retrieving the element". No `-ing` verb form for what a member does.
* **One sentence per line.** Break at sentence boundaries, never at a fixed character width.
* **No dashes or semicolons.** Use separate sentences or commas.

## XML Doc Comments

* Document every public member with a `<summary>` that states what it does.
* **`<summary>` describes the member, not its parameters.** Parameters belong in `<param>`, the return value in `<returns>`, and thrown exceptions in `<exception>`. Do not restate the signature in prose.
* For a wrapper over the Revit API, mirror the corresponding Revit API summary and document the Revit `<exception>`s the member can throw.
* Reference another type or member with `<see cref="..."/>` so renames stay tracked.

## README

The README is an API catalog organized by Revit type. Each top-level section covers one type or area, with subsections for the families of operations on it.

* Every new extension has a copy-pasteable usage example with C# syntax highlighting, under the section for the type it extends.
* An `ElementId` overload, a new overload, or any other variant belongs with its primary method, in the existing section. Do not open a new section for a variant.
* Add a Table of Contents entry when a new section appears.

## CHANGELOG

The CHANGELOG is versioned by release. Add the change to the current preview or release version section, grouped by the same Revit type categories the README uses.

* List a new method by its call-site signature under its type category, with the `ElementId` sibling alongside it.
* Categorize every change, not only the major ones: new extensions, breaking changes, improvements, and bug fixes.
* Provide a migration example at the end of the section for any breaking change or deprecation. See [Backward Compatibility](./backward-compatibility.md) for the deprecation pattern.
