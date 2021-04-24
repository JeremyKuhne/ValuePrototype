using System;
using BenchmarkDotNet.Attributes;
using ValuePrototype;

namespace ValuePerf
{
    public class StoreNullableLong
    {
        [Benchmark(Baseline = true)]
        public long? InOutNullableLong()
        {
            long? @long = 42;
            ValueCompactFast value = new(@long);
            value.TryGetValue(out long? result);
            return result;
        }

        [Benchmark]
        public long? InLongOutNullableLong()
        {
            long @long = 42;
            ValueCompactFast value = new(@long);
            value.TryGetValue(out long? result);
            return result;
        }

        [Benchmark]
        public long InNullableLongOutLong()
        {
            long? @long = 42;
            ValueCompactFast value = new(@long);
            value.TryGetValue(out long result);
            return result;
        }
    }
}
