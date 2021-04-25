using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringLong
    {
        public static TheoryData<long> LongData => new()
        {
            { 42 },
            { long.MaxValue },
            { long.MinValue }
        };

        [Theory]
        [MemberData(nameof(LongData))]
        public void LongImplicit(long @long)
        {
            Value value = @long;
            Assert.Equal(@long, value.As<long>());
            Assert.Equal(typeof(long), value.Type);

            long? source = @long;
            value = source;
            Assert.Equal(source, value.As<long>());
            Assert.Equal(typeof(long), value.Type);
        }

        [Theory]
        [MemberData(nameof(LongData))]
        public void LongCreate(long @long)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = Value.Create(@long);
            }

            Assert.Equal(@long, value.As<long>());
            Assert.Equal(typeof(long), value.Type);

            long? source = @long;

            using (MemoryWatch.Create)
            {
                value = Value.Create(source);
            }

            Assert.Equal(source, value.As<long?>());
            Assert.Equal(typeof(long), value.Type);
        }

        [Theory]
        [MemberData(nameof(LongData))]
        public void LongInOut(long @long)
        {
            Value value = new(@long);
            bool success = value.TryGetValue(out long result);
            Assert.True(success);
            Assert.Equal(@long, result);

            Assert.Equal(@long, value.As<long>());
            Assert.Equal(@long, (long)value);
        }

        [Theory]
        [MemberData(nameof(LongData))]
        public void NullableLongInLongOut(long? @long)
        {
            long? source = @long;
            Value value = new(source);

            bool success = value.TryGetValue(out long result);
            Assert.True(success);
            Assert.Equal(@long, result);

            Assert.Equal(@long, value.As<long>());

            Assert.Equal(@long, (long)value);
        }

        [Theory]
        [MemberData(nameof(LongData))]
        public void LongInNullableLongOut(long @long)
        {
            long source = @long;
            Value value = new(source);
            bool success = value.TryGetValue(out long? result);
            Assert.True(success);
            Assert.Equal(@long, result);

            Assert.Equal(@long, (long?)value);
        }

        [Fact]
        public void NullLong()
        {
            long? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<long?>());
            Assert.False(value.As<long?>().HasValue);
        }
    }
}
