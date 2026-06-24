# Backward Compatibility

This is a public library with downstream consumers. A breaking change breaks other people's builds. **Never delete or change an existing public API.** Deprecate it instead.

## Obsolete Pattern

To rename or replace a method, mark the old one `[Obsolete]` and keep it functional. Provide a JetBrains `CodeTemplate` so ReSharper and Rider can auto-convert call sites.

```csharp
[Obsolete("Use CanBeMirrored() instead")]
[CodeTemplate(
    searchTemplate: "$expr$.CanMirrorElement()",
    Message = "CanMirrorElement is obsolete, use CanBeMirrored instead",
    ReplaceTemplate: "$expr$.CanBeMirrored()",
    ReplaceMessage = "Replace with CanBeMirrored()")]
public bool CanMirrorElement()
{
    return ElementTransformUtils.CanMirrorElement(element.Document, element.Id);
}
```

## Obsolete Guidelines

* **Message.** A clear explanation that names the replacement method.
* **CodeTemplate.** Provide the ReSharper auto-conversion pattern (`searchTemplate` to `ReplaceTemplate`) so call sites migrate automatically.
* **Implementation.** The obsolete method calls the original Revit API, not the new method. This avoids recursion and keeps it working independently.
* **No `EditorBrowsable`.** Do not add `[EditorBrowsable(EditorBrowsableState.Never)]` to an obsolete `Element` extension method.

## Breaking Changes

* **Method signatures.** Never change an existing signature. Add a new method instead.
* **Return types.** Never change a return type, except the allowed `void` to source-object change that enables chaining.
* **Parameters.** Add a new parameter only as optional, and only at the end of the list.
* **Renaming.** Use the Obsolete pattern above and keep the old method functional indefinitely.

Document every change, additions, deprecations, and behavior changes alike, in the [CHANGELOG](./documentation.md).
