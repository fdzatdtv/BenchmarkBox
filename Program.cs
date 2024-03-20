using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Running;

namespace BenchmarkBox
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<FinalizerBenchmark>();
            // BenchmarkRunner.Run<BenchmarkReadonlyStruct>();
            // BenchmarkRunner.Run<HashSetBenchmark>();
            // BenchmarkRunner.Run<BenchmarkCleanTree>();
            // BenchmarkRunner.Run<ThrowHelperBenchmark>();
            // BenchmarkRunner.Run<MappingBenchmark>();
            // BenchmarkRunner.Run<MappingBenchmark2>();
            // BenchmarkRunner.Run<StructInterfaceBenchmark>();
            // BenchmarkRunner.Run<PolymorphismBenchmark>();
            // BenchmarkRunner.Run<MethodCallBenchmark>();
        }
    }
}
