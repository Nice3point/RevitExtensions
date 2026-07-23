---
name: revit-extensions-backward-compat
description: >
  Deprecate or replace a public member of Nice3point.Revit.Extensions without breaking downstream consumers, using [Obsolete] with a JetBrains CodeTemplate auto-conversion pattern.
  USE FOR: renaming or superseding an existing extension while keeping the old member functional; retiring a member that Revit itself deprecated in a specific year; deciding what may change on the public surface and how to migrate callers automatically.
  DO NOT USE FOR: authoring a brand-new extension from scratch — use revit-extensions-authoring.
license: MIT
---

# Preserving Backward Compatibility in Revit Extensions

Nice3point.Revit.Extensions is a public NuGet library with downstream consumers; its public surface is a contract.
A deleted or altered member breaks other people's builds. Keep the old member forever, mark it `[Obsolete]`, and ship a JetBrains `CodeTemplate`; ReSharper and Rider then auto-migrate call sites to the replacement.
This skill covers deprecating, renaming, or superseding an existing member; it depends on JetBrains annotations (`JetBrains.Annotations`) for both `[Obsolete]`'s companion `CodeTemplate` and the surrounding attributes.

## When to use

- Renaming an extension or replacing it with a better-named member while keeping every existing call compiling.
- Retiring a member whose underlying Revit API was deprecated in a specific Revit year.
- Deciding whether a proposed change to a signature, return type, or parameter list is allowed at all.

## When not to use

- Adding a member that does not yet exist on the public surface — that is new-surface work; use revit-extensions-authoring.

## Workflow

### Step 1: Add the replacement beside the original, both wrapping the raw API

Keep the old member in place and add the new one in the same `extension(Type)` block.
Both members call the underlying Revit API directly; the new member never delegates to the old one, and the old one never delegates to the new one.
Write full method bodies; an expression-bodied property is fine for a read-only value.

```csharp
extension(Element element)
{
    /// <summary>Determines whether element can be mirrored</summary>
    /// <returns>True if the element can be mirrored</returns>
    public bool CanBeMirrored => ElementTransformUtils.CanMirrorElement(element.Document, element.Id);
}
```

### Step 2: Mark the original `[Obsolete]` with a message that names the replacement

The message states plainly what to use instead.
The obsolete member keeps calling the original Revit API; it stays correct and independent and must not recurse into the new extension.

```csharp
[Pure]
[Obsolete("Use CanBeMirrored() instead")]
public bool CanMirrorElement()
{
    return ElementTransformUtils.CanMirrorElement(element.Document, element.Id);
}
```

### Step 3: Attach the CodeTemplate auto-conversion pattern

Add `[CodeTemplate]` beside `[Obsolete]` so the IDE rewrites call sites automatically.
`searchTemplate` matches the old call, `ReplaceTemplate` produces the new call, and the two `Message`/`ReplaceMessage` strings surface in the inspection and the quick-fix.
Use `$expr$` for the receiver and match the replacement shape exactly — a property replacement drops the parentheses.

```csharp
[Pure]
[Obsolete("Use CanBeMirrored() instead")]
[CodeTemplate(
    searchTemplate: "$expr$.CanMirrorElement()",
    Message = "CanMirrorElement is obsolete, use CanBeMirrored instead",
    ReplaceTemplate = "$expr$.CanBeMirrored",
    ReplaceMessage = "Replace with CanBeMirrored")]
public bool CanMirrorElement()
{
    return ElementTransformUtils.CanMirrorElement(element.Document, element.Id);
}
```

When the member takes arguments, thread them through the template with named placeholders; the rewrite preserves them:

```csharp
[Pure]
[Obsolete("Use CanBeMirrored() instead")]
[CodeTemplate(
    searchTemplate: "$expr$.CanMirrorElements($document$)",
    Message = "CanMirrorElements is obsolete, use CanBeMirrored instead",
    ReplaceTemplate = "$expr$.CanBeMirrored($document$)",
    ReplaceMessage = "Replace with CanBeMirrored()")]
public bool CanMirrorElements(Document document)
{
    return ElementTransformUtils.CanMirrorElements(document, elements);
}
```

### Step 4: Gate the deprecation when Revit deprecates the API in a specific year

If the underlying Revit API is only obsolete from a given release, wrap just the `[Obsolete]` and `[CodeTemplate]` attributes in the matching `#if`; the member deprecates on the versions that warrant it and stays clean elsewhere.
When the replacement adds a parameter, add it as an optional argument at the end; use a `$placeholder$` in the template for the new argument.

```csharp
[Pure]
#if REVIT2027_OR_GREATER
[Obsolete("This method is deprecated in Revit 2027 and may be removed in a later version of Revit. We suggest you use the overload which accepts a region input instead.")]
[CodeTemplate(
    searchTemplate: "$expr$.DownloadParameterOptions()",
    Message = "DownloadParameterOptions() is obsolete, use overload with region parameter",
    ReplaceTemplate = "$expr$.DownloadParameterOptions($arg$)",
    ReplaceMessage = "Replace with DownloadParameterOptions(region)")]
#endif
public ParameterDownloadOptions DownloadParameterOptions()
{
    return ParameterUtils.DownloadParameterOptions(typeId);
}
```

### Step 5: Confirm nothing on the surface actually broke

Hold every change to the allowed set before you compile:

- Never change an existing signature or return type — the only allowed return-type change is a `void` mutation becoming its source object for chaining.
- Add new parameters only as optional, and only at the end of the list.
- Keep the old member functional indefinitely; deprecation is not deletion.

Then compile every configuration from the `build` directory and update the shipped docs in the same change.

```shell
dotnet run -c Release
```

Update `Readme.md`, add a categorized `Changelog.md` entry for the deprecation, and keep the XML docs on both the old and new members.

## Validation

- [ ] The original member still exists, still compiles, and still calls the original Revit API, not the new member.
- [ ] `[Obsolete]` carries a message that names the replacement; no `[EditorBrowsable(EditorBrowsableState.Never)]` sits on an obsolete `Element` extension.
- [ ] `[CodeTemplate]` supplies `searchTemplate`, `ReplaceTemplate`, `Message`, and `ReplaceMessage`, and the replacement shape matches the new member (property vs. method, argument placeholders preserved).
- [ ] A Revit-year-specific deprecation wraps only the attributes in the matching `#if`.
- [ ] No signature or return type changed; any new parameter is optional and last; `dotnet run -c Release` compiles every configuration and `Readme.md`/`Changelog.md`/XML docs are updated.

## Common Pitfalls

| Pitfall                                                                | Correct approach                                                                               |
|------------------------------------------------------------------------|------------------------------------------------------------------------------------------------|
| Deleting or renaming the old member outright                           | Keep it, mark it `[Obsolete]`, and add the replacement beside it.                              |
| The obsolete member calling the new extension                          | Call the original Revit API from the obsolete member; it never recurses and stays independent. |
| `[EditorBrowsable(Never)]` on an obsolete `Element` extension          | Omit it; let the member stay discoverable while `[Obsolete]` warns.                            |
| `[Obsolete]` without a `[CodeTemplate]`                                | Add the `searchTemplate` → `ReplaceTemplate` pattern; callers auto-migrate.                    |
| `ReplaceTemplate` keeping parentheses for a property replacement       | Match the replacement exactly — drop the `()` when the new member is a property.               |
| Changing a signature or return type, or inserting a required parameter | Add a new member or an optional trailing parameter instead.                                    |
| Deprecating unconditionally when Revit deprecates only in a later year | Wrap just the `[Obsolete]`/`[CodeTemplate]` attributes in the matching `#if`.                  |
