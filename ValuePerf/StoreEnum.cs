﻿namespace ValuePerf;

[DisassemblyDiagnoser]
public class StoreEnum
{
    [Benchmark(Baseline = true)]
    public DayOfWeek TryOut()
    {
        Value value = Value.Create(DayOfWeek.Monday);
        value.TryGetValue(out DayOfWeek result);
        return result;
    }
}
