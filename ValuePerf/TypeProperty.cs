using System;
using BenchmarkDotNet.Attributes;
using ValuePrototype;

namespace ValuePerf
{
    public class TypeProperty
    {
        private static Value s_int = new(42);
        private static Value s_object = new(new object());
        private static Value s_null = new((object?)null);
        private static Value s_byteArray = new(new byte[10]);
        private static Value s_byteSegment = new(new ArraySegment<byte>(new byte[10], 1, 3));

        [Benchmark(Baseline = true)]
        public Type? TypeOfInt()
        {
            return s_int.Type;
        }

        [Benchmark]
        public Type? TypeOfObject()
        {
            return s_object.Type;
        }

        [Benchmark]
        public Type? TypeOfNull()
        {
            return s_null.Type;
        }

        [Benchmark]
        public Type? TypeOfArray()
        {
            return s_byteArray.Type;
        }

        [Benchmark]
        public Type? TypeOfArraySegment()
        {
            return s_byteSegment.Type;
        }
    }
}
