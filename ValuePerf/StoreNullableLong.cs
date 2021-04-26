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
            Value value = new(@long);
            value.TryGetValue(out long? result);
            return result;
        }

        [Benchmark]
        public long? InLongOutNullableLong()
        {
            long @long = 42;
            Value value = new(@long);
            value.TryGetValue(out long? result);
            return result;
        }

        [Benchmark]
        public long InNullableLongOutLong()
        {
            long? @long = 42;
            Value value = new(@long);
            value.TryGetValue(out long result);
            return result;
        }

        [Benchmark]
        public long? CastInOutNullableLong()
        {
            long? @long = 42;
            Value value = @long;
            return (long?)value;
        }

        [Benchmark]
        public long? CastInLongOutNullableLong()
        {
            long @long = 42;
            Value value = @long;
            return (long?)value;
        }

        [Benchmark]
        public long CastInNullableLongOutLong()
        {
            long? @long = 42;
            Value value = @long;
            return (long)value;
        }
    }
}
