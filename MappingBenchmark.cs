using System;
using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace BenchmarkBox
{
    [SimpleJob(RuntimeMoniker.Mono)]
    public class MappingBenchmark
    {
        [Params(10,25,50,100)]
        public int Length;

        private Dictionary<string, int> _rowsById;
        private string[] _rowsByOrder;
        private string[] _searchKeys;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _rowsById = new Dictionary<string, int>(Length);
            _rowsByOrder = new string[Length];
            for (int row = 0; row < Length; row++)
            {
                var key = Guid.NewGuid().ToString();
                _rowsByOrder[row] = key;
                _rowsById[key] = row;
            }

            _searchKeys = _rowsByOrder.Take(Length / 2).ToArray();
        }

        [Benchmark(Baseline = true)]
        public int SearchInDictionary()
        {
            int found = 0;
            for (int index = 0; index < _searchKeys.Length; index++)
            {
                if (SearchInDictionary(_searchKeys[index]) != -1)
                    found++;
            }
            return found;
        }

        private int SearchInDictionary(string key)
        {
            if (key != null && _rowsById.TryGetValue(key, out int value))
                return value;
            return -1;
        }

        [Benchmark]
        public int SearchInArray()
        {
            int found = 0;
            for (int index = 0; index < _searchKeys.Length; index++)
            {
                if (SearchInArray(_searchKeys[index]) != -1)
                    found++;
            }
            return found;
        }
        
        private int SearchInArray(string key)
        {
            for (int index = 0; index < _rowsByOrder.Length; index++)
            {
                if (_rowsByOrder[index] == key)
                    return index;
            }
            return -1;
        }
    }
    
    [SimpleJob(RuntimeMoniker.Mono)]
    public class MappingBenchmark2
    {
        [Params(10,25,50)]
        public int Length;

        private Dictionary<int, int> _rowsById;
        private int[] _rowsByOrder;
        private int[] _searchKeys;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _rowsById = new Dictionary<int, int>(Length);
            _rowsByOrder = new int[Length];
            for (int row = 0; row < Length; row++)
            {
                var key = 1000 + row * 2;
                _rowsByOrder[row] = key;
                _rowsById[key] = row;
            }

            _searchKeys = _rowsByOrder.Take(Length / 2).ToArray();
        }

        [Benchmark(Baseline = true)]
        public int SearchInDictionary()
        {
            int found = 0;
            for (int index = 0; index < _searchKeys.Length; index++)
            {
                if (SearchInDictionary(_searchKeys[index]) != -1)
                    found++;
            }
            return found;
        }

        private int SearchInDictionary(int key)
        {
            if (_rowsById.TryGetValue(key, out int value))
                return value;
            return -1;
        }

        [Benchmark]
        public int SearchInArray()
        {
            int found = 0;
            for (int index = 0; index < _searchKeys.Length; index++)
            {
                if (SearchInArray(_searchKeys[index]) != -1)
                    found++;
            }
            return found;
        }
        
        private int SearchInArray(int key)
        {
            for (int index = 0; index < _rowsByOrder.Length; index++)
            {
                if (_rowsByOrder[index] == key)
                    return index;
            }
            return -1;
        }
    }
}