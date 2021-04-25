using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringShort
    {
        public static TheoryData<short> ShortData => new()
        {
            { 42 },
            { short.MaxValue },
            { short.MinValue }
        };

        [Theory]
        [MemberData(nameof(ShortData))]
        public void ShortImplicit(short @short)
        {
            Value value = @short;
            Assert.Equal(@short, value.As<short>());
            Assert.Equal(typeof(short), value.Type);

            short? source = @short;
            value = source;
            Assert.Equal(source, value.As<short?>());
            Assert.Equal(typeof(short), value.Type);
        }

        [Theory]
        [MemberData(nameof(ShortData))]
        public void ShortCreate(short @short)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = Value.Create(@short);
            }

            Assert.Equal(@short, value.As<short>());
            Assert.Equal(typeof(short), value.Type);

            short? source = @short;

            using (MemoryWatch.Create)
            {
                value = Value.Create(source);
            }

            Assert.Equal(source, value.As<short?>());
            Assert.Equal(typeof(short), value.Type);
        }

        [Theory]
        [MemberData(nameof(ShortData))]
        public void ShortInOut(short @short)
        {
            Value value = new(@short);
            bool success = value.TryGetValue(out short result);
            Assert.True(success);
            Assert.Equal(@short, result);

            Assert.Equal(@short, value.As<short>());
            Assert.Equal(@short, (short)value);
        }

        [Theory]
        [MemberData(nameof(ShortData))]
        public void NullableShortInShortOut(short? @short)
        {
            short? source = @short;
            Value value = new(source);

            bool success = value.TryGetValue(out short result);
            Assert.True(success);
            Assert.Equal(@short, result);

            Assert.Equal(@short, value.As<short>());

            Assert.Equal(@short, (short)value);
        }

        [Theory]
        [MemberData(nameof(ShortData))]
        public void ShortInNullableShortOut(short @short)
        {
            short source = @short;
            Value value = new(source);
            bool success = value.TryGetValue(out short? result);
            Assert.True(success);
            Assert.Equal(@short, result);

            Assert.Equal(@short, (short?)value);
        }

        [Fact]
        public void NullShort()
        {
            short? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<short?>());
            Assert.False(value.As<short?>().HasValue);
        }
    }
}
