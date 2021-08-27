namespace ValuePerf;

public class StoreInteger
{
    [Benchmark(Baseline = true)]
    public int ValueCompactPerf()
    {
        ValueCompact value = new(42);
        return value.As<int>();
    }

    [Benchmark]
    public int ValueCompactFastPerf()
    {
        Value value = new(42);
        return value.As<int>();
    }

    [Benchmark]
    public int ValueCompactFastTryPerf()
    {
        Value value = new(42);
        value.TryGetValue(out int result);
        return result;
    }

    [Benchmark]
    public int ValueStatePerf()
    {
        ValueState value = new(42);
        value.TryGetValue(out int result);
        return result;
    }
}
