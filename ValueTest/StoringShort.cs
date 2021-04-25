using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringShort
    {
        [Fact]
        public void ShortImplicit()
        {
            Value value = (short)42;
            Assert.Equal(42, value.As<short>());
            Assert.Equal(typeof(short), value.Type);

            short? source = 42;
            value = source;
            Assert.Equal(source, value.As<short?>());
            Assert.Equal(typeof(short), value.Type);
        }

        [Theory]
        [InlineData((short)42)]
        [InlineData(short.MinValue)]
        [InlineData(short.MaxValue)]
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
        [InlineData((short)42)]
        [InlineData(short.MinValue)]
        [InlineData(short.MaxValue)]
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
        [InlineData((short)42)]
        [InlineData(short.MinValue)]
        [InlineData(short.MaxValue)]
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
