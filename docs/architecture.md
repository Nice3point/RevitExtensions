# Architecture & Design Principles

Nice3point.Revit.Extensions exists to make the Revit API pleasant to consume from C#.
Every public member is an extension method that wraps a Revit API call behind a fluent, type-safe, discoverable surface.

## Core Design Goals

* **Type safety.** Generics and nullable reference types push errors to compile time.
* **Discoverability.** Extensions group by the Revit type they extend, so they surface in IntelliSense on that type.
* **Fluency.** A mutation method returns the source object, so calls chain into a readable pipeline.
* **Performance.** The library runs on Revit hot paths, so it minimizes allocations and follows optimized API access patterns. See [Revit Best Practices](./revit-best-practices.md).
* **Backward compatibility.** An existing public API never breaks. See [Backward Compatibility](./backward-compatibility.md).

## Extension Method Model

Author every new extension as a C# `extension(Type) { }` member rather than a legacy static method with a `this` parameter.
Group members that extend the same type under a single block.
A generic block (`extension<T>(...)`) wraps a generic API, and a static-context block wraps a static API.

```csharp
extension(Element element)
{
    public Element JoinGeometry(Element secondElement)
    {
        JoinGeometryUtils.JoinGeometry(element.Document, element, secondElement);
        return element; // enables chaining
    }
}
```

## Chained Calls

A mutation method returns the source object, so calls chain.
A method that would naturally return `void` returns the object it operated on instead.

## ElementId Overload Pattern

When a method operates through `element.Document` and `element.Id`, provide a sibling overload on `ElementId` that takes an explicit `Document`.
Keep the name descriptive so the `ElementId` context never reads as ambiguous.

```csharp
extension(Element element)
{
    public bool CanBeDeleted() => DocumentValidation.CanDeleteElement(element.Document, element.Id);
}

extension(ElementId elementId)
{
    public bool CanBeDeleted(Document document) => DocumentValidation.CanDeleteElement(document, elementId);
}
```

A specific name keeps the `ElementId` overload clear, such as `elementId.MoveGlobalParameterUpOrder(document)` rather than `elementId.MoveUpOrder(document)`.

## Design Rules

* Wrap the Revit API, never reimplement it. A wrapper calls the underlying Revit API directly and adds only ergonomics.
* Mark read-only operations `[Pure]`.
* Keep the public surface in `[PublicAPI]`-marked classes and keep implementation detail in the internal folder.
* Do not leak internal helpers, such as reflection accessors and format utilities, into the public surface.
* Isolate version-specific Revit API differences behind compilation directives, not duplicated types. See [Revit Best Practices](./revit-best-practices.md).
