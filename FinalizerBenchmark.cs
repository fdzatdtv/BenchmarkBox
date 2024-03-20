using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BenchmarkBox
{
    [SimpleJob(RuntimeMoniker.Mono)]
    public class FinalizerBenchmark
    {
        [Params(100, 1000, 10_000)] public int N;

        [Benchmark(Baseline = true)]
        public void WithFinalizer()
        {
            for (int i = 0; i < N; i++)
            {
                var a = new AWithF();
            }
        }

        [Benchmark]
        public void NoFinalizer()
        {
            for (int i = 0; i < N; i++)
            {
                var a = new A();
            }
        }

        private class A
        {
        }
        
        private class AWithF
        {
            ~AWithF()
            {
            }
        }
    }
}