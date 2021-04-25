using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringUInt
    {
        [Fact]
        public void UIntImplicit()
        {
            Value value = 42u;
            Assert.Equal(42u, value.As<uint>());
            Assert.Equal(typeof(uint), value.Type);

            uint? source = 42;
            value = source;
            Assert.Equal(source, value.As<uint?>());
            Assert.Equal(typeof(uint?), value.Type);
        }

        [Theory]
        [InlineData(42u)]
        [InlineData(uint.MinValue)]
        [InlineData(uint.MaxValue)]
        public void UIntInOut(uint @uint)
        {
            Value value = new(@uint);
            bool success = value.TryGetValue(out uint result);
            Assert.True(success);
            Assert.Equal(@uint, result);

            Assert.Equal(@uint, value.As<uint>());
            Assert.Equal(@uint, (uint)value);
        }

        [Theory]
        [InlineData(42u)]
        [InlineData(uint.MinValue)]
        [InlineData(uint.MaxValue)]
        public void NullableUIntInUIntOut(uint? @uint)
        {
            uint? source = @uint;
            Value value = new(source);

            bool success = value.TryGetValue(out uint result);
            Assert.True(success);
            Assert.Equal(@uint, result);

            Assert.Equal(@uint, value.As<uint>());

            Assert.Equal(@uint, (uint)value);
        }

        [Theory]
        [InlineData(42u)]
        [InlineData(uint.MinValue)]
        [InlineData(uint.MaxValue)]
        public void UIntInNullableUIntOut(uint @uint)
        {
            uint source = @uint;
            Value value = new(source);
            bool success = value.TryGetValue(out uint? result);
            Assert.True(success);
            Assert.Equal(@uint, result);

            Assert.Equal(@uint, (uint?)value);
        }

        [Fact]
        public void NullUInt()
        {
            uint? source = null;
            Value value = source;
            Assert.Equal(typeof(uint?), value.Type);
            Assert.Equal(source, value.As<uint?>());
            Assert.False(value.As<uint?>().HasValue);
        }
    }
}
