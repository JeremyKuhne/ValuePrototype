using System;
using BenchmarkDotNet.Attributes;
using ValuePrototype;

namespace ValuePerf
{
    [DisassemblyDiagnoser]
    public class StoreLong
    {
        [Benchmark]
        public long ValueAsPerf()
        {
            Value value = new(42L);
            return value.As<long>();
        }

        [Benchmark(Baseline = true)]
        public long ValueTryPerf()
        {
            Value value = new(42L);
            value.TryGetValue(out long result);
            return result;
        }

        [Benchmark]
        public long ValueCastOutPerf()
        {
            Value value = new(42L);
            return (long)value;
        }

        [Benchmark]
        public long ValueCastInPerf()
        {
            Value value = 42L;
            value.TryGetValue(out long result);
            return result;
        }
    }
}
