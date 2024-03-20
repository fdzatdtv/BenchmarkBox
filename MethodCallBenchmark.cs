using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BenchmarkBox
{
    [SimpleJob(RuntimeMoniker.Mono)]
    public class MethodCallBenchmark
    {
        private interface ICalculator
        {
            double Sum(double x, double y);
        }

        private class CalculatorA : ICalculator
        {
            double ICalculator.Sum(double x, double y) => x + y;
        }

        private class CalculatorB : ICalculator
        {
            public double Sum(double x, double y) => x + y;
        }

        private class CalculatorC : ICalculator
        {
            public double Sum(double x, double y) => x + y;
            double ICalculator.Sum(double x, double y) => Sum(x, y);
        }

        private static class CalculatorD
        {
            public static double Sum(double x, double y) => x + y;
        }

        [Params(1000, 10_000)] public int N;

        private ICalculator[] _homogenList;
        private ICalculator[] _inhomogenList;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _homogenList = new ICalculator[N];
            _inhomogenList = new ICalculator[N];

            for (int i = 0; i < N; i++)
                _homogenList[i] = new CalculatorA();
            for (int i = 0; i < N; i++)
            {
                ICalculator calc = null;
                switch (i % 3)
                {
                    case 0: calc = new CalculatorA();
                        break;
                    case 1: calc = new CalculatorB();
                        break;
                    case 2: calc = new CalculatorC();
                        break;
                }
                _inhomogenList[i] = calc;
            }
        }
        
        [Benchmark(Baseline = true)]
        public void InterfaceExplicit()
        {
            ICalculator calc = new CalculatorA();
            for (int i = 0; i < N; i++)
            {
                _ = calc.Sum(i * 0.5, i * 1.5);
            }
        }
        
        [Benchmark]
        public void MemberCall()
        {
            var calc = new CalculatorB();
            for (int i = 0; i < N; i++)
            {
                _ = calc.Sum(i * 0.5, i * 1.5);
            }
        }
        
        [Benchmark]
        public void ForwardedInterfaceExplicit()
        {
            ICalculator calc = new CalculatorB();
            for (int i = 0; i < N; i++)
            {
                _ = calc.Sum(i * 0.5, i * 1.5);
            }
        }
        
        [Benchmark]
        public void StaticCall()
        {
            for (int i = 0; i < N; i++)
            {
                _ = CalculatorD.Sum(i * 0.5, i * 1.5);
            }
        }
        
        // [Benchmark]
        public void LoopInhomogen()
        {
            for (int i = 0; i < N; i++)
            {
                _ = _inhomogenList[i].Sum(i * 0.5, i * 1.5);
            }
        }
        
        // [Benchmark]
        public void LoopHomogen()
        {
            for (int i = 0; i < N; i++)
            {
                _ = _homogenList[i].Sum(i * 0.5, i * 1.5);
            }
        }
    }
}