# Package Management

The solution uses centralized NuGet package management. All versions live in `Directory.Packages.props` (`ManagePackageVersionsCentrally=true`). Renovate (`renovate.json`) bumps versions automatically, so manual version edits are rare.

## Rules

* Define every package version in `Directory.Packages.props`. Do not add `<Version>` to an individual `PackageReference`.
* Keep a Revit-version-specific package conditional on `$(RevitVersion)`. The Revit API packages float to `$(RevitVersion).*`, and the per-version packages such as the Toolkit, TUnit.Revit, and BenchmarkDotNet.Revit are pinned with a `$(RevitVersion)` condition.
* Keep a shared dependency version unconditional unless it truly varies by Revit version.
* Use `GlobalPackageReference` only for a solution-wide package such as the polyfill and the annotation sources.
* Revit API references in the library are `PrivateAssets="all"`, so they stay build-time only and never flow to consumers of the package.

## Add a Dependency

1. Add the package version to `Directory.Packages.props`.
2. Add a versionless `PackageReference` to the project that uses it.
3. Keep the scope narrow. The shipped library stays dependency-light, so prefer an existing package or a platform or Revit API before introducing a new dependency.
