---
name: revit-extensions-authoring
description: >
  Add a fluent extension to Nice3point.Revit.Extensions with C# 14 extension(Type){} blocks that wrap the raw Revit API for readability and chaining.
  USE FOR: authoring a new extension method or property over a Revit type, utility, or manager; choosing the receiver type and adding the ElementId sibling overload; naming, attributes, namespace placement, and version gating for a new extension.
  DO NOT USE FOR: deprecating, renaming, or changing an existing public member without breaking consumers; writing the tests or benchmarks that exercise an extension.
license: MIT
---

# Authoring a Revit Extension

Nice3point.Revit.Extensions is a public NuGet library of fluent extension methods over the Revit API.
Each member wraps a raw Revit API call behind a type-safe, chainable, discoverable surface and adds ergonomics, never new behavior.
This skill covers adding one extension the way the existing surface is built; it depends on the Revit API packages (`Nice3point.Revit.Api.*`).

## When to use

- Adding a new extension method or property over a core Revit type, a `*Utils` static class, or a `*Manager`.
- Deciding the receiver type and whether an `ElementId` sibling overload is needed.
- Naming, attributes, XML docs, namespace placement, and `#if` version gating for a new member.

## When not to use

- Changing, renaming, or deprecating an existing public member — that must preserve the API contract; use revit-extensions-backward-compat.
- Writing coverage for the new member — custom logic is exercised by the TUnit test project, and API wrappers and UI extensions are not tested.

## Workflow

### Step 1: Place the file and declare the flat namespace

Group one Revit type's extensions per `<Type>Extensions.cs`.
Choose the folder by what the type is:

- core `Autodesk.Revit.DB` type (`Element`, `Document`, `Category`, `Parameter`) → project root.
- Revit `*Utils` static class → `UtilsExtensions/<UtilsClass>Extensions.cs`.
- Revit `*Manager` type → `ManagersExtensions/<Manager>Extensions.cs`.
- Ribbon / UI Framework helper → `UIFrameworkExtensions/`.
- non-public helper (reflection accessors, format utilities) → `Internal/`, never public.

Declare the flat file-scoped namespace `namespace Nice3point.Revit.Extensions;`, or `namespace Nice3point.Revit.Extensions.UI;` for types from RevitAPIUI.
Namespaces with nesting like `Autodesk.Revit.DB.Structure.StructuralSections` declare base on the same policy `Nice3point.Revit.Extensions.Structure.StructuralSections`.
Add `// ReSharper disable once CheckNamespace` above the namespace whenever the declared namespace does not match the file's folder — every subfolder file needs it; a root file already in the flat namespace does not.

```csharp
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

/// <summary>
///     Represent extension methods for the <see cref="Autodesk.Revit.DB.JoinGeometryUtils"/> class.
/// </summary>
[PublicAPI]
public static class JoinGeometryUtilsExtensions
```

Mark the public class `[PublicAPI]`.

### Step 2: Author members inside an extension(Type) block

Open one `extension(Type receiver)` block per receiver and put the receiver's XML doc on the block.
Wrap the Revit API directly — call the underlying API and return its result; never reimplement the operation.
Write full block bodies for methods; an expression-bodied property is fine for a read-only value.

```csharp
/// <param name="element">The source element.</param>
extension(Element element)
{
    /// <summary>Creates clean joins between two elements that share a common face</summary>
    public Element JoinGeometry(Element secondElement)
    {
        JoinGeometryUtils.JoinGeometry(element.Document, element, secondElement);
        return element;
    }

    /// <summary>Determines whether two elements are joined</summary>
    /// <returns>True if the two elements are joined</returns>
    [Pure]
    public bool AreElementsJoined(Element secondElement)
    {
        return JoinGeometryUtils.AreElementsJoined(element.Document, element, secondElement);
    }
}
```

A static member of the wrapped API becomes a `public static` member in the block:

```csharp
extension(FamilySymbol familySymbol)
{
    [Pure]
    public static ICollection<ElementId> GetProfileSymbols(Document document, ProfileFamilyUsage profileFamilyUsage, bool oneCurveLoopOnly)
    {
        return FamilyUtils.GetProfileSymbols(document, profileFamilyUsage, oneCurveLoopOnly);
    }
}
```

### Step 3: Return the source object from mutation methods

A method that mutates and would otherwise return `void` returns the receiver; calls chain.
Keep a meaningful return value when the API produces one (for example `Copy` returns the new `ElementId` collection).

```csharp
extension(Element element)
{
    public Element Move(double deltaX = 0d, double deltaY = 0d, double deltaZ = 0d)
    {
        ElementTransformUtils.MoveElement(element.Document, element.Id, new XYZ(deltaX, deltaY, deltaZ));
        return element;
    }

    public Element Rotate(Line axis, double angle)
    {
        ElementTransformUtils.RotateElement(element.Document, element.Id, axis, angle);
        return element;
    }
}
```

### Step 4: Add the ElementId sibling overload

When the method reaches the API through `element.Document` and `element.Id`, add a sibling on `ElementId` that takes an explicit `Document`.
Keep the bare name when it stays unambiguous on `ElementId`; give it a descriptive, disambiguated name when the bare name would be too generic.

```csharp
extension(Element element)
{
    public bool CanBeDeleted => DocumentValidation.CanDeleteElement(element.Document, element.Id);
}

extension(ElementId elementId)
{
    [Pure]
    public bool CanBeDeleted(Document document)
    {
        return DocumentValidation.CanDeleteElement(document, elementId);
    }
}
```

A generic name gets qualified: `GlobalParameter.MoveUpOrder()` has the sibling `elementId.MoveGlobalParameterUpOrder(document)`, not `elementId.MoveUpOrder(document)`.

### Step 5: Attribute, name, and gate by version

- `[Pure]` on every read-only method or property; `[PublicAPI]` on the public class.
- Follow Revit's naming voice: passive for a test on an object (`CanBeDeleted`, `CanBeMirrored`), active when the object acts (`CanElementCutElement`).
- Document each member: `<summary>` (copied from the Revit API doc for a wrapper), `<param>`, `<returns>`, and every Revit `<exception>` the call can throw. Let Revit exceptions propagate.
- Gate a version-specific API with `#if REVIT2024_OR_GREATER`-style directives; the member then compiles under every declared `Debug.RNN`/`Release.RNN`. Keep the whole file coherent per version.

```csharp
extension(ElementId elementId)
{
    [Pure]
    public bool IsCategory(BuiltInCategory category)
    {
#if REVIT2024_OR_GREATER
        return elementId.Value == (long)category;
#else
        return elementId.IntegerValue == (int)category;
#endif
    }
}
```

### Step 6: Compile every configuration and update docs

Compile all Revit configurations from the `build` directory and update the shipped docs in the same change.

```shell
dotnet run -c Release
```

Update `Readme.md` (add the usage example to the type's existing section — no new section for an `ElementId` sibling), `Changelog.md` (categorized entry), and the XML docs.
Add coverage in the test project only when the member has custom logic beyond the wrapped call.

## Validation

- [ ] One `<Type>Extensions.cs` per Revit type, in the folder matching core/Utils/Manager/UI/Internal; `[PublicAPI]` on the class.
- [ ] Flat namespace declared, with `// ReSharper disable once CheckNamespace` on every subfolder and `.UI` file.
- [ ] Members live in `extension(Type)` blocks, call the Revit API directly, and use full method bodies.
- [ ] Mutation methods return the receiver; an `ElementId` sibling with an explicit `Document` exists wherever the method uses `element.Document`/`element.Id`.
- [ ] `[Pure]` on read-only members; XML `<summary>`/`<param>`/`<returns>`/`<exception>` present; version-specific APIs gated with `#if`.
- [ ] `dotnet run -c Release` compiles every configuration; `Readme.md`, `Changelog.md`, and XML docs updated.

## Common Pitfalls

| Pitfall                                                                | Correct approach                                                                     |
|------------------------------------------------------------------------|--------------------------------------------------------------------------------------|
| Reimplementing the operation                                           | Call the underlying Revit API directly and return its result.                        |
| A mutation method returning `void`                                     | Return the receiver so calls chain.                                                  |
| Method with `element.Document`/`element.Id` and no `ElementId` sibling | Add the sibling taking an explicit `Document`, disambiguating the name when needed.  |
| Expression-bodied method body                                          | Use a full block body for methods; expression-bodied properties are fine.            |
| Version-specific API used unconditionally                              | Gate it with `#if REVIT####_OR_GREATER` so every `Debug.RNN`/`Release.RNN` compiles. |
| Missing `// ReSharper disable once CheckNamespace` on a subfolder file | Add it whenever the namespace does not match the folder.                             |
