# Benchmarks

The library runs on Revit hot paths, so an implementation choice that affects allocations or throughput is measured, not guessed. Benchmarks live in the benchmark project under `/tests` and run with BenchmarkDotNet on the Revit thread, through Nice3point.BenchmarkDotNet.Revit.

## When to Benchmark

* Add a benchmark only when an implementation has more than one viable approach and the choice between them is not obvious. The benchmark exists to justify the chosen approach with numbers.
* Do not benchmark a single obvious implementation. A benchmark with one candidate measures nothing.
* When the shipped path is already settled, no benchmark is needed.

## Strategy Candidates

A benchmark compares self-contained candidates, each a full inline implementation of one approach to the same operation.

* Inline every candidate inside the benchmark. A candidate does not call the library's shipped extension, so a later refactor of the library cannot silently change what the benchmark measures.
* Mark the candidate that mirrors the shipped code `[Benchmark(Baseline = true)]`, so the report ranks the alternatives against the approach in use.
* Gate a candidate that depends on a runtime or Revit version behind the matching `#if` directive, so the comparison stays valid per configuration.

```csharp
public class ToCategoryBenchmark : RevitDocumentBenchmark
{
    [Benchmark]
    public Category ReflectionPinned() { /* one full inline approach */ }

    [Benchmark(Baseline = true)]
    public Category CachedPinned() { /* the approach the library ships */ }
}
```

## End-to-End Benchmarks

A benchmark that calls the library's public API directly is the exception, reserved for measuring an entire shipped path end to end rather than comparing strategy candidates. A document-backed benchmark inherits the shared Revit fixture under the benchmark project's `Abstractions` folder, which opens a project document for the run and closes it in cleanup.

## Version Coverage

* A benchmark builds per Revit configuration (`Debug.RNN`), the same as the tests.
* When measuring version-specific behavior, run the relevant configuration and keep the candidates gated to match.
