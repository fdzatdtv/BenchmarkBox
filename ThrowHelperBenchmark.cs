using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BenchmarkBox
{
    [SimpleJob(RuntimeMoniker.Mono)]
    public class ThrowHelperBenchmark
    {
        [Params(1_000)] public int N;

        [Benchmark(Baseline = true)]
        public int NonInlineCall()
        {
            var counter = new Counter();
            for (int i = 0; i < N; i++)
                counter.AddNonInlined(i % 2);
            return counter.Count;
        }
        
        [Benchmark]
        public int InlineCall()
        {
            var counter = new Counter();
            for (int i = 0; i < N; i++)
                counter.AddPossiblyInlined(i % 2);
            return counter.Count;
        }

        private class Counter
        {
            private int _count;
            public int Count => _count;

            public void AddNonInlined(int i)
            {
                if (i < 0)
                    throw new ArgumentOutOfRangeException(nameof(i));
                _count += i;
            }

            // [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void AddPossiblyInlined(int i)
            {
                if (i < 0)
                    Guard.ThrowArgumentOutOfRange(nameof(i));
                _count += i;
            }
        }
    }

    public static class Guard
    {
        public static void ThrowArgumentOutOfRange(string parameterName)
        {
            throw new ArgumentOutOfRangeException(parameterName);
        }
    }
}