namespace ValueTest;

public class StoringNull
{
    [Fact]
    public void GetIntFromStoredNull()
    {
        ValueCompact nullValue = new((object?)null);
        Assert.Throws<InvalidCastException>(() => _ = nullValue.As<int>());

        Value nullFastValue = new((object?)null);
        Assert.Throws<InvalidCastException>(() => _ = nullFastValue.As<int>());

        bool success = nullFastValue.TryGetValue(out int result);
        Assert.False(success);

        Assert.Equal(default(int), result);
    }
}
