using System;
using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringDateTimeOffset
    {
        public static TheoryData<DateTimeOffset> DateTimeOffsetData => new()
        {
            { DateTimeOffset.Now },
            { DateTimeOffset.UtcNow },
            { DateTimeOffset.MaxValue },
            { DateTimeOffset.MinValue }
        };

        [Theory]
        [MemberData(nameof(DateTimeOffsetData))]
        public void DateTimeOffsetImplicit(DateTimeOffset @DateTimeOffset)
        {
            Value value = @DateTimeOffset;
            Assert.Equal(@DateTimeOffset, value.As<DateTimeOffset>());
            Assert.Equal(typeof(DateTimeOffset), value.Type);

            DateTimeOffset? source = @DateTimeOffset;
            value = source;
            Assert.Equal(source, value.As<DateTimeOffset?>());
            Assert.Equal(typeof(DateTimeOffset), value.Type);
        }

        [Theory]
        [MemberData(nameof(DateTimeOffsetData))]
        public void DateTimeOffsetInOut(DateTimeOffset @DateTimeOffset)
        {
            Value value = new(@DateTimeOffset);
            bool success = value.TryGetValue(out DateTimeOffset result);
            Assert.True(success);
            Assert.Equal(@DateTimeOffset, result);

            Assert.Equal(@DateTimeOffset, value.As<DateTimeOffset>());
            Assert.Equal(@DateTimeOffset, (DateTimeOffset)value);
        }

        [Theory]
        [MemberData(nameof(DateTimeOffsetData))]
        public void NullableDateTimeOffsetInDateTimeOffsetOut(DateTimeOffset? @DateTimeOffset)
        {
            DateTimeOffset? source = @DateTimeOffset;
            Value value = new(source);

            bool success = value.TryGetValue(out DateTimeOffset result);
            Assert.True(success);
            Assert.Equal(@DateTimeOffset, result);

            Assert.Equal(@DateTimeOffset, value.As<DateTimeOffset>());

            Assert.Equal(@DateTimeOffset, (DateTimeOffset)value);
        }

        [Theory]
        [MemberData(nameof(DateTimeOffsetData))]
        public void DateTimeOffsetInNullableDateTimeOffsetOut(DateTimeOffset @DateTimeOffset)
        {
            DateTimeOffset source = @DateTimeOffset;
            Value value = new(source);
            bool success = value.TryGetValue(out DateTimeOffset? result);
            Assert.True(success);
            Assert.Equal(@DateTimeOffset, result);

            Assert.Equal(@DateTimeOffset, (DateTimeOffset?)value);
        }

        [Fact]
        public void NullDateTimeOffset()
        {
            DateTimeOffset? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<DateTimeOffset?>());
            Assert.False(value.As<DateTimeOffset?>().HasValue);
        }

        [Theory]
        [MemberData(nameof(DateTimeOffsetData))]
        public void OutAsObject(DateTimeOffset @DateTimeOffset)
        {
            Value value = new(@DateTimeOffset);
            object o = value.As<object>();
            Assert.Equal(typeof(DateTimeOffset), o.GetType());
            Assert.Equal(@DateTimeOffset, (DateTimeOffset)o);

            DateTimeOffset? n = @DateTimeOffset;
            value = new(n);
            o = value.As<object>();
            Assert.Equal(typeof(DateTimeOffset), o.GetType());
            Assert.Equal(@DateTimeOffset, (DateTimeOffset)o);
        }
    }
}
