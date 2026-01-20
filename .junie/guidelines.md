# RevitExtensions Guidelines

## 1. Project Structure

### 1.1. Solution Organization

* **`/source`**: Core library project.
    * `Nice3point.Revit.Extensions`: Extension methods for Revit API types.
* **`/tests`**: Testing projects.
    * `Nice3point.Revit.Extensions.Tests`: Unit tests executed in Revit context using Nice3point.TUnit.Revit.
* **Root Level**:
    * Configuration files: `Directory.Build.props`, `Directory.Packages.props`
    * Documentation: `Readme.md`, `Changelog.md`
    * CI/CD: `.github/workflows`

## 2. Architecture Principles

### 2.1. Core Design Goals

* **Fluent API:** Enable method chaining for better readability.
* **Type Safety:** Utilize generics and nullable reference types.
* **Performance:** Minimize allocations, use optimized Revit API patterns.
* **Analyzers:** Use JetBrains.Annotations for static code analysis.
* **Backward Compatibility:** Never break existing public APIs.

## 3. Strict C# Production Style

### 3.1. General Principles

* **C# 14 Extension Syntax:** Use `extension(Type type) { }` blocks instead of static methods with `this`.
* **Method Chaining:** All `void` methods should return the source object for fluent API.
* **Pure Functions:** Mark read-only operations with `[Pure]` attribute.
* **Explicit over Implicit:** Code should be self-explanatory.

### 3.2. Naming Conventions

* **Clarity is King:** Names must be descriptive.
* **Revit API Patterns:** Follow Revit API naming conventions.
    * Passive voice for operations on object: `CanBeDeleted`, `CanBeMirrored`, `CanBeConvertedToFaceHostBased`
    * Active voice for object performing action: `CanElementCutElement`
* **No Abbreviations:**
    * ❌ `elem`, `doc`, `param`
    * ✅ `element`, `document`, `parameter`
* **ElementId Overloads:** Methods accepting `Document document` parameter should have descriptive names when context is ambiguous:
    * ✅ `elementId.MoveGlobalParameterUpOrder(document)`
    * ❌ `elementId.MoveUpOrder(document)` - too generic

### 3.3. Extension Method Structure

* **File-Scoped Namespaces:** Always use `namespace Nice3point.Revit.Extensions;` with the comment `// ReSharper disable once CheckNamespace`
* **PublicAPI Attribute:** Mark all extension classes with `[PublicAPI]`.
* **Extension Blocks:** Group related extensions using `extension(Type type) { }`, `extension<T>(Type<T> type) { }` or `extension(Type) { }` syntax.
* **XML Documentation:** Document all public methods with `<summary>` block(it should be copied from Revit API documentation for Revit API wrappers).

### 3.4. Method Chaining Pattern

All mutation methods should return the source object istead of void type:

```csharp
extension(Element element)
{
    public Element JoinGeometry(Element secondElement)
    {
        JoinGeometryUtils.JoinGeometry(element.Document, element, secondElement);
        return element; // Enable chaining
    }
}
```

### 3.5. ElementId Overload Pattern

For methods using `element.Document` and `element.Id`, provide ElementId overloads:

```csharp
extension(Element element)
{
    public bool CanBeDeleted()
    {
        return DocumentValidation.CanDeleteElement(element.Document, element.Id);
    }
}

extension(ElementId elementId)
{
    public bool CanBeDeleted(Document document)
    {
        return DocumentValidation.CanDeleteElement(document, elementId);
    }
}
```

### 3.6. Error Handling

* **Revit Exceptions:** Document Revit API exceptions in XML comments.
* **No Swallowing:** Let Revit exceptions propagate to caller.
* **Validation:** Validate inputs for custom logic only.

### 3.7. Compilation Directives

* **Revit Version Support:** Use `#if REVIT2024_OR_GREATER` for version-specific APIs.
* **Consistent Patterns:** Apply directives consistently across related methods.

## 4. Backward Compatibility

### 4.1. Obsolete Attribute Pattern

**NEVER** delete existing public APIs. Mark them as obsolete instead:

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

### 4.2. Obsolete Guidelines

* **Message:** Clear explanation with replacement method name.
* **CodeTemplate:** Provide JetBrains ReSharper auto-conversion pattern.
* **Implementation:** Obsolete method must call **original Revit API**, not the new method (avoid recursion).
* **No EditorBrowsable:** Do NOT add `[EditorBrowsable(EditorBrowsableState.Never)]` for Element extension obsolete methods.

### 4.3. Breaking Changes

* **Method Signature:** Never change existing method signatures (only if renaming is required).
* **Return Type:** Never change return types (except `void` → source object for chaining).
* **Parameters:** Add optional parameters only at the end.
* **Renaming:** Use Obsolete pattern, keep old method functional.

## 5. Documentation Requirements

### 5.1. Readme.md

* **Add Examples:** Every new extension category must have usage examples.
* **ElementId Examples:** When adding ElementId overloads, add examples in existing sections:
  ```csharp
  // Element extension
  element.Copy(new XYZ(1, 1, 0));

  // ElementId extension (add to same section)
  elementId.Copy(document, new XYZ(1, 1, 0));
  ```
* **No New Sections:** Don't create new sections for ElementId variants(if extension has the method for an element), add to existing sections.

### 5.2. Changelog.md

* **Version Sections:** Update the current preview/release version section.
* **Categories:**
    * **New Features:** New extension methods, ElementId overloads.
    * **Breaking Changes:** Renamed methods, changed behavior.
    * **Improvements:** Performance, refactoring.
    * **Bug Fixes:** Corrections to existing functionality.
* **Migration Examples:** Show at the end.
* **Complete Documentation:** Document ALL changes, not just major ones.

### 5.3. XML Documentation

* **Summary:** Describe what the method does.
* **Parameters:** Document each parameter with context.
* **Returns:** Describe return value meaning.
* **Exceptions:** Document all possible Revit API exceptions.

## 6. Testing Strategy

### 6.1. Test Scope

* **Custom Logic Only:** Test extensions with user-defined logic, NOT simple wrappers over Revit Utils.
* **Examples:**
    * ✅ Test: `BoundingBox.Contains()` - custom containment logic
    * ✅ Test: `Line.Distance()` - custom distance calculation
    * ✅ Test: `ToOrderedElements()` - custom ordering logic
    * ❌ Skip: `element.Copy()` - simple wrapper over `ElementTransformUtils.CopyElement`
    * ❌ Skip: `element.JoinGeometry()` - simple wrapper over `JoinGeometryUtils.JoinGeometry`

### 6.2. Test Framework

* **Framework:** TUnit with Nice3point.TUnit.Revit for Revit context execution.
* **Location:** `tests/Nice3point.Revit.Extensions.Tests`.
* **Execution:** Tests run inside Revit process using `[TestExecutor<RevitThreadExecutor>]`.

### 6.3. Test Data Pattern

Use `MethodDataSource` to test against all Revit sample files:

```csharp
private static readonly string SamplesPath = $@"C:\Program Files\Autodesk\Revit {Application.VersionNumber}\Samples";

[Before(Class)]
public static void ValidateSamples()
{
    if (!Directory.Exists(SamplesPath))
    {
        Skip.Test($"Samples folder not found at {SamplesPath}");
        return;
    }

    if (!Directory.EnumerateFiles(SamplesPath, "*.rfa").Any())
    {
        Skip.Test($"No .rfa files found in {SamplesPath}");
    }
}

public static IEnumerable<string> GetSampleFiles()
{
    if (!Directory.Exists(SamplesPath))
    {
        yield return string.Empty;
        yield break;
    }

    foreach (var file in Directory.EnumerateFiles(SamplesPath, "*.rfa")) yield return file;
}

[Test]
[TestExecutor<RevitThreadExecutor>]
[MethodDataSource(nameof(GetSampleFiles))]
public async Task MyExtension_ValidFile_ReturnsExpectedResult(string filePath)
{
    Document? document = null;

    try
    {
        document = Application.OpenDocumentFile(filePath);

        // Test logic here

        await Assert.That(result).IsNotNull();
    }
    finally
    {
        document?.Close(false);
    }
}
```

### 6.4. Test Coverage

* **Edge Cases:** Null inputs, empty collections, boundary values.
* **Revit API Constraints:** Test against actual Revit objects, not mocks.

### 6.5. UI Extensions

* **No Testing:** Skip UI-related extensions (Ribbon, ContextMenu, UIApplication).

## 7. Performance Guidelines

### 7.1. Revit API Optimization

* **Batch Operations:** Prefer batch APIs over individual calls.
* **Transactions:** Minimize transaction scope.

### 7.2. Memory Allocation

* **Avoid LINQ:** For hot paths, use traditional loops instead of LINQ.
* **Collection Sizing:** Pre-allocate collections when size is known.
* **String Operations:** Use `StringBuilder` for complex concatenations.

## 8. Package Management

* **Centralized:** All versions are defined in `Directory.Packages.props`.
* **Multi-targeting:** Support Revit 2019-2026 configurations (Debug.R19-R26, Release.R19-R26).
* **Dependencies:**
    * `JetBrains.Annotations` - Code analysis attributes
* **Conditional Compilation:** Use `#if REVIT2024_OR_GREATER` and similar for API changes.