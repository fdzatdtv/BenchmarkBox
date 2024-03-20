using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BenchmarkBox
{
    [SimpleJob(RuntimeMoniker.Mono)]
    public class StructInterfaceBenchmark
    {
        private List<DateIntervalV2> _structIntervals;
        private List<IDateInterval> _intervals;

        [Params(100, 500)]
        //[Params(10_000)]
        public int N;
        //[GlobalSetup]
        //public void GlobalSetup()
        //{
        //    _structIntervals = new List<DateIntervalV1>(N);
        //    _intervals = new List<IDateInterval>(N);
        //}

        [Benchmark(Baseline = true)]
        public void InsertAbstract()
        {
            _intervals = new List<IDateInterval>(N);
            for (int i = 0; i < N; i++)
            {
                DateTime l = new DateTime(2020,i%12+1, 1);
                DateTime r = new DateTime(2020, i%12+1, 15);
                _intervals.Add(new DateIntervalV2(l, r));
            }
        }

        [Benchmark]
        public void InsertConcrete()
        {
            _structIntervals = new List<DateIntervalV2>(N);
            for (int i = 0; i < N; i++)
            {
                DateTime l = new DateTime(2020,i%12+1, 1);
                DateTime r = new DateTime(2020, i%12+1, 15);
                _structIntervals.Add(new DateIntervalV2(l, r));
            }
        }
    }

    public struct DateIntervalV1 : IEquatable<DateIntervalV1>
    {
        public DateTime Left { get; }
        public DateTime Right { get; }

        public DateIntervalV1(DateTime left, DateTime right)
        {
            Left = left;
            Right = right;
        }

        public bool Equals(DateIntervalV1 other)
        {
            return Left.Equals(other.Left) && Right.Equals(other.Right);
        }

        public override bool Equals(object obj)
        {
            return obj is DateIntervalV1 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Left.GetHashCode() * 397) ^ Right.GetHashCode();
            }
        }
    }

    public interface IDateInterval
    {
        DateTime Left { get; }
        DateTime Right { get; }
    }

    public struct DateIntervalV2 : IEquatable<DateIntervalV2>, IDateInterval
    {
        public DateTime Left { get; }
        public DateTime Right { get; }

        public DateIntervalV2(DateTime left, DateTime right)
        {
            Left = left;
            Right = right;
        }

        public bool Equals(DateIntervalV2 other)
        {
            return Left.Equals(other.Left) && Right.Equals(other.Right);
        }

        public override bool Equals(object obj)
        {
            return obj is DateIntervalV2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Left.GetHashCode() * 397) ^ Right.GetHashCode();
            }
        }
    }
}
