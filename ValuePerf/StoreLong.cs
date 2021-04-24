using System;
using BenchmarkDotNet.Attributes;
using ValuePrototype;

namespace ValuePerf
{
    [DisassemblyDiagnoser]
    public class StoreLong
    {
        [Benchmark]
        public long ValueCompactFastPerf()
        {
            ValueCompactFast value = new(42L);
            return value.As<long>();
        }

        [Benchmark(Baseline = true)]
        public long ValueCompactFastTryPerf()
        {
            ValueCompactFast value = new(42L);
            value.TryGetValue(out long result);
            return result;
        }

        [Benchmark]
        public long ValueCompactFastCastPerf()
        {
            ValueCompactFast value = new(42L);
            return (long)value;
        }
    }
}
