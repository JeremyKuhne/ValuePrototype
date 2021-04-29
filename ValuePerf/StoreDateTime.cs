using System;
using BenchmarkDotNet.Attributes;
using ValuePrototype;

namespace ValuePerf
{
    public class StoreDateTime
    {
        private static DateTime s_now = DateTime.Now;
        private static DateTimeOffset s_offsetUtc = DateTime.UtcNow;
        private static DateTimeOffset s_offset = DateTime.Now;

        [Benchmark(Baseline = true)]
        public DateTime InOutDateTime()
        {
            Value value = new(s_now);
            value.TryGetValue(out DateTime result);
            return result;
        }

        [Benchmark]
        public DateTimeOffset InOutDateTimeOffsetUTC()
        {
            Value value = new(s_offsetUtc);
            value.TryGetValue(out DateTimeOffset result);
            return result;
        }

        [Benchmark]
        public DateTimeOffset InOutDateTimeOffset()
        {
            Value value = new(s_offset);
            value.TryGetValue(out DateTimeOffset result);
            return result;
        }
    }
}
