# Code Style

Production C# only. This is a public library, so its style is part of its contract.

## General Principles

* **SOLID and DRY.** One responsibility per type. Extract shared logic rather than duplicate it.
* **Explicit over implicit.** Code is self-explanatory. Avoid hidden behavior and unclear defaults.
* **Nullable safety.** Nullable reference types are enabled solution-wide. Treat every nullability warning as a defect.
* **Follow StyleCop conventions** for layout, member ordering, and spacing.

## Modern C#

`LangVersion` is `latest`. Reach for the newest feature that expresses the intent directly, and do not hand-roll what the language already provides.

* `extension(Type) { }` blocks for all new extensions, never legacy static methods with a `this` parameter.
* Primary constructors when a type captures state.
* Collection expressions for literals and spans.
* Pattern matching and switch expressions over branching chains.
* Range and index operators for slicing.
* Expression-bodied members for simple wrappers.
* File-scoped namespaces.

## Comments

Public members carry XML doc comments, covered in [Documentation](./documentation.md). Inside the code, comments are the exception.

* Names and structure carry the meaning. Default to no comment.
* Add one only when the reason cannot be read from the code and a reader could break it without that reason, such as a non-obvious invariant.
* A comment explains why, never what. Do not restate the code.

## Attributes

Decorate members with every JetBrains and .NET attribute that carries meaning, so analyzers, the debugger, and callers read the full contract.

* `[PublicAPI]` on every public extension class.
* `[Pure]` on a read-only method.
* `[CodeTemplate]` on a deprecated method so Rider can auto-convert call sites. See [Backward Compatibility](./backward-compatibility.md).

## Naming

* **Clarity first.** Names are descriptive and never abbreviated: `element` not `elem`, `document` not `doc`, `parameter` not `param`, `context` not `ctx`.
* **Follow Revit API naming conventions.** Passive voice for an operation tested on an object (`CanBeDeleted`, `CanBeMirrored`), active voice when the object performs the action (`CanElementCutElement`).
* **Disambiguate `ElementId` overloads.** When the bare name reads as too generic on `ElementId`, keep it specific: `elementId.MoveGlobalParameterUpOrder(document)`, not `elementId.MoveUpOrder(document)`.
* No single-letter variables except in a short loop or lambda.

## File and Class Structure

* **One type group per file**, named `<Type>Extensions.cs`.
* **File-scoped namespaces.** Use `namespace Nice3point.Revit.Extensions;`, or the UI sub-namespace for RevitAPIUI-related extensions. When a file lives in a subfolder but keeps the flatter namespace for discoverability, add `// ReSharper disable once CheckNamespace` above the declaration.
* **`[PublicAPI]`** on every public extension class.
* **`extension(Type) { }` blocks** group every member that extends the same type.

```csharp
// ReSharper disable once CheckNamespace
namespace Nice3point.Revit.Extensions;

[PublicAPI]
public static class ElementExtensions
{
    extension(Element element)
    {
        public Element JoinGeometry(Element secondElement)
        {
            JoinGeometryUtils.JoinGeometry(element.Document, element, secondElement);
            return element;
        }
    }
}
```

## Error Handling

* **Let Revit exceptions propagate.** Never swallow a Revit API exception. Document it with `<exception>`.
* **Validate custom logic only.** Validate inputs for behavior the library itself adds, not for a thin wrapper where Revit already validates.
* Prefer a semantic exception over a generic `Exception` for any custom logic.

## Compilation Directives

* `#if REVIT2024_OR_GREATER` and similar for version-specific Revit APIs.
* `#if NET` or `#if NET8_0_OR_GREATER` for runtime-specific features.
* Apply directives consistently across related members so a type's surface stays coherent per version. See [Revit Best Practices](./revit-best-practices.md).
