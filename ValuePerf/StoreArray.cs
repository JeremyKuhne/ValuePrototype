namespace ValuePerf;

public class StoreArray
{
    private static readonly byte[] s_byteArray = new byte[10];
    private static readonly ArraySegment<byte> s_byteSegment = new(s_byteArray);
    private static readonly ArraySegment<byte> s_emptyByteSegment = new(s_byteArray, 0, 0);

    [Benchmark(Baseline = true)]
    public byte[] InOutByteArray()
    {
        Value value = new(s_byteArray);
        value.TryGetValue(out byte[] result);
        return result;
    }

    [Benchmark]
    public ArraySegment<byte> InOutByteSegment()
    {
        Value value = new(s_byteSegment);
        value.TryGetValue(out ArraySegment<byte> result);
        return result;
    }

    [Benchmark]
    public ArraySegment<byte> InOutEmptyByteSegment()
    {
        Value value = new(s_emptyByteSegment);
        value.TryGetValue(out ArraySegment<byte> result);
        return result;
    }
}
