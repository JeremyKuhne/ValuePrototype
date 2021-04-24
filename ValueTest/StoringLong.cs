using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringLong
    {
        [Fact]
        public void LongImplicit()
        {
            ValueCompactFast value = 42L;
            Assert.Equal(42L, value.As<long>());
            Assert.Equal(typeof(long), value.Type);

            long? source = 42;
            value = source;
            Assert.Equal(source, value.As<long?>());
            Assert.Equal(typeof(long?), value.Type);
        }

        [Theory]
        [InlineData(42)]
        [InlineData(long.MaxValue)]
        public void LongInOut(long @long)
        {
            ValueCompactFast value = new(@long);
            bool success = value.TryGetValue(out long result);
            Assert.True(success);
            Assert.Equal(@long, result);

            Assert.Equal(@long, value.As<long>());
            Assert.Equal(@long, (long)value);
        }

        [Theory]
        [InlineData(42)]
        [InlineData(long.MaxValue)]
        public void NullableLongInLongOut(long? @long)
        {
            long? source = @long;
            ValueCompactFast value = new(source);

            bool success = value.TryGetValue(out long result);
            Assert.True(success);
            Assert.Equal(@long, result);

            Assert.Equal(@long, value.As<long>());

            Assert.Equal(@long, (long)value);
        }

        [Theory]
        [InlineData(42)]
        [InlineData(long.MaxValue)]
        public void LongInNullableLongOut(long @long)
        {
            long source = @long;
            ValueCompactFast value = new(source);
            bool success = value.TryGetValue(out long? result);
            Assert.True(success);
            Assert.Equal(@long, result);

            Assert.Equal(@long, (long?)value);
        }

        [Fact]
        public void NullLong()
        {
            long? source = null;
            ValueCompactFast value = source;
            Assert.Equal(typeof(long?), value.Type);
            Assert.Equal(source, value.As<long?>());
            Assert.False(value.As<long?>().HasValue);
        }
    }
}
