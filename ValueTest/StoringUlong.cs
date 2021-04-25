using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringULong
    {
        [Fact]
        public void ULongImplicit()
        {
            Value value = 42ul;
            Assert.Equal(42ul, value.As<ulong>());
            Assert.Equal(typeof(ulong), value.Type);

            ulong? source = 42;
            value = source;
            Assert.Equal(source, value.As<ulong?>());
            Assert.Equal(typeof(ulong?), value.Type);
        }

        [Theory]
        [InlineData(42ul)]
        [InlineData(ulong.MinValue)]
        [InlineData(ulong.MaxValue)]
        public void ULongInOut(ulong @ulong)
        {
            Value value = new(@ulong);
            bool success = value.TryGetValue(out ulong result);
            Assert.True(success);
            Assert.Equal(@ulong, result);

            Assert.Equal(@ulong, value.As<ulong>());
            Assert.Equal(@ulong, (ulong)value);
        }

        [Theory]
        [InlineData(42ul)]
        [InlineData(ulong.MinValue)]
        [InlineData(ulong.MaxValue)]
        public void NullableULongInULongOut(ulong? @ulong)
        {
            ulong? source = @ulong;
            Value value = new(source);

            bool success = value.TryGetValue(out ulong result);
            Assert.True(success);
            Assert.Equal(@ulong, result);

            Assert.Equal(@ulong, value.As<ulong>());

            Assert.Equal(@ulong, (ulong)value);
        }

        [Theory]
        [InlineData(42ul)]
        [InlineData(ulong.MinValue)]
        [InlineData(ulong.MaxValue)]
        public void ULongInNullableULongOut(ulong @ulong)
        {
            ulong source = @ulong;
            Value value = new(source);
            bool success = value.TryGetValue(out ulong? result);
            Assert.True(success);
            Assert.Equal(@ulong, result);

            Assert.Equal(@ulong, (ulong?)value);
        }

        [Fact]
        public void NullULong()
        {
            ulong? source = null;
            Value value = source;
            Assert.Equal(typeof(ulong?), value.Type);
            Assert.Equal(source, value.As<ulong?>());
            Assert.False(value.As<ulong?>().HasValue);
        }
    }
}
