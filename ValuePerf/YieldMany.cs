using BenchmarkDotNet.Attributes;
using System.Collections.Generic;
using ValuePrototype;

namespace ValuePerf
{
    [MemoryDiagnoser]
    public class YieldMany
    {
        private const int Iterations = 1_000_000;

        [Benchmark(Baseline = true)]
        public int SumObject()
        {
            int sum = 0;
            foreach (object o in EnumerateObjects()) sum += (int)o;
            return sum;
        }

        [Benchmark]
        public int SumValue()
        {
            int sum = 0;
            foreach (Value v in EnumerateValues()) sum += v.As<int>();
            return sum;
        }

        private static IEnumerable<object> EnumerateObjects()
        {
            for (int i = 0; i < Iterations; i++) yield return (object)1;
        }

        private static IEnumerable<Value> EnumerateValues()
        {
            for (int i = 0; i < Iterations; i++) yield return new Value((int)1);
        }
    }
}
