using System;
using BenchmarkDotNet.Attributes;
using ValuePrototype;

namespace ValuePerf
{
    public class StoreObject
    {
        private static A s_A = new();
        private static B s_B = new();

        [Benchmark(Baseline = true)]
        public A TryOut()
        {
            Value value = new(s_A);
            value.TryGetValue(out A result);
            return result;
        }

        [Benchmark]
        public A TryOutAssignable()
        {
            Value value = new(s_B);
            value.TryGetValue(out A result);
            return result;
        }

        public class A : I { }
        public class B : A, I { }
        public class C : B, I { }

        public interface I
        {
            string? ToString();
        }
    }
}
