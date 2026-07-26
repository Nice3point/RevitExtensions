using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using Nice3point.BenchmarkDotNet.Revit;
using Nice3point.Revit.Extensions.Benchmarks.Benchmarks;

var configuration = ManualConfig.Create(DefaultConfig.Instance)
    .AddJob(Job.Default.WithCurrentConfiguration())
    .AddDiagnoser(MemoryDiagnoser.Default)
    .AddExporter(MarkdownExporter.GitHub);

// BenchmarkRunner.Run<EnumerationBenchmark>(configuration);
BenchmarkRunner.Run<FilteredElementCollectorBenchmark>(configuration);
BenchmarkRunner.Run<RebarCrankTypeUtilsBenchmark>(configuration);
BenchmarkRunner.Run<RebarSpliceTypeUtilsBenchmark>(configuration);
BenchmarkRunner.Run<ToCategoryBenchmark>(configuration);
BenchmarkRunner.Run<ToParameterBenchmark>(configuration);
BenchmarkRunner.Run<UnsafeAccessorsBenchmark>(configuration);