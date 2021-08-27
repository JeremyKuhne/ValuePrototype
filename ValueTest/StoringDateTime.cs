namespace ValueTest;

public class StoringDateTime
{
    public static TheoryData<DateTime> DateTimeData => new()
    {
        { DateTime.Now },
        { DateTime.UtcNow },
        { DateTime.MaxValue },
        { DateTime.MinValue }
    };

    [Theory]
    [MemberData(nameof(DateTimeData))]
    public void DateTimeImplicit(DateTime @DateTime)
    {
        Value value = @DateTime;
        Assert.Equal(@DateTime, value.As<DateTime>());
        Assert.Equal(typeof(DateTime), value.Type);

        DateTime? source = @DateTime;
        value = source;
        Assert.Equal(source, value.As<DateTime?>());
        Assert.Equal(typeof(DateTime), value.Type);
    }

    [Theory]
    [MemberData(nameof(DateTimeData))]
    public void DateTimeInOut(DateTime @DateTime)
    {
        Value value = new(@DateTime);
        bool success = value.TryGetValue(out DateTime result);
        Assert.True(success);
        Assert.Equal(@DateTime, result);

        Assert.Equal(@DateTime, value.As<DateTime>());
        Assert.Equal(@DateTime, (DateTime)value);
    }

    [Theory]
    [MemberData(nameof(DateTimeData))]
    public void NullableDateTimeInDateTimeOut(DateTime? @DateTime)
    {
        DateTime? source = @DateTime;
        Value value = new(source);

        bool success = value.TryGetValue(out DateTime result);
        Assert.True(success);
        Assert.Equal(@DateTime, result);

        Assert.Equal(@DateTime, value.As<DateTime>());

        Assert.Equal(@DateTime, (DateTime)value);
    }

    [Theory]
    [MemberData(nameof(DateTimeData))]
    public void DateTimeInNullableDateTimeOut(DateTime @DateTime)
    {
        DateTime source = @DateTime;
        Value value = new(source);
        bool success = value.TryGetValue(out DateTime? result);
        Assert.True(success);
        Assert.Equal(@DateTime, result);

        Assert.Equal(@DateTime, (DateTime?)value);
    }

    [Fact]
    public void NullDateTime()
    {
        DateTime? source = null;
        Value value = source;
        Assert.Null(value.Type);
        Assert.Equal(source, value.As<DateTime?>());
        Assert.False(value.As<DateTime?>().HasValue);
    }

    [Theory]
    [MemberData(nameof(DateTimeData))]
    public void OutAsObject(DateTime @DateTime)
    {
        Value value = new(@DateTime);
        object o = value.As<object>();
        Assert.Equal(typeof(DateTime), o.GetType());
        Assert.Equal(@DateTime, (DateTime)o);

        DateTime? n = @DateTime;
        value = new(n);
        o = value.As<object>();
        Assert.Equal(typeof(DateTime), o.GetType());
        Assert.Equal(@DateTime, (DateTime)o);
    }
}
