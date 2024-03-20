namespace BenchmarkBox
{
    public readonly struct FairlyLargeStruct
    {
        private readonly long l1, l2, l3, l4;
        public int N { get; }
        public FairlyLargeStruct(int n) : this() => N = n;
    }
}