using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System;
using System.Collections.Generic;

namespace BenchmarkBox
{
    [SimpleJob(RuntimeMoniker.Mono)]
    public class HashSetBenchmark
    {
        private HashSet<int> set;
        private const int times = 1_000_000;
        private readonly int[] keys;

        public HashSetBenchmark()
        {
            set = new HashSet<int>();
            keys = new int[times];
            for (int i = 0; i < times; i++)
            {
                keys[i] = i % 10000;
            }
        }

        [Benchmark]
        public int DuplicateKeyLookup()
        {
            int sum = 0;
            for (int i = 0; i < times; i++)
            {
                int key = keys[i];
                if (!set.Contains(key))
                {
                    set.Add(key);
                    sum++;
                }
            }
            return sum;
        }

        [Benchmark]
        public int SingleKeyLookup()
        {
            int sum = 0;
            for (int i = 0; i < times; i++)
            {
                int key = keys[i];
                if (set.Add(key))
                {
                    sum++;
                }
            }
            return sum;
        }
    }
}
