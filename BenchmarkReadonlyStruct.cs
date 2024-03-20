using System.Linq;
using BenchmarkDotNet.Attributes;

namespace BenchmarkBox
{
    [SimpleJob(BenchmarkDotNet.Jobs.RuntimeMoniker.Mono)]
    public class BenchmarkReadonlyStruct
    {
        private FairlyLargeStruct _nonReadOnlyStruct = new FairlyLargeStruct(42);
        private readonly FairlyLargeStruct _readOnlyStruct = new FairlyLargeStruct(42);
        private readonly int[] _data = Enumerable.Range(1, 100_000).ToArray();

        [Benchmark(Baseline = true)]
        public int AggregateForReadOnlyField()
        {
            int result = 0;
            foreach (int n in _data)
                result += n + _readOnlyStruct.N;
            return result;
        }
        
        [Benchmark]
        public int AggregateForNonReadOnlyField()
        {
            int result = 0;
            foreach (int n in _data)
                result += n + _nonReadOnlyStruct.N;
            return result;
        }
    }
}