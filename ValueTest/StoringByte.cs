using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringByte
    {
        [Fact]
        public void ByteImplicit()
        {
            Value value = (byte)42;
            Assert.Equal(42, value.As<byte>());
            Assert.Equal(typeof(byte), value.Type);

            byte? source = 42;
            value = source;
            Assert.Equal(source, value.As<byte?>());
            Assert.Equal(typeof(byte?), value.Type);
        }

        [Theory]
        [InlineData((byte)42)]
        [InlineData(byte.MinValue)]
        [InlineData(byte.MaxValue)]
        public void ByteInOut(byte @byte)
        {
            Value value = new(@byte);
            bool success = value.TryGetValue(out byte result);
            Assert.True(success);
            Assert.Equal(@byte, result);

            Assert.Equal(@byte, value.As<byte>());
            Assert.Equal(@byte, (byte)value);
        }

        [Theory]
        [InlineData((byte)42)]
        [InlineData(byte.MinValue)]
        [InlineData(byte.MaxValue)]
        public void NullableByteInByteOut(byte? @byte)
        {
            byte? source = @byte;
            Value value = new(source);

            bool success = value.TryGetValue(out byte result);
            Assert.True(success);
            Assert.Equal(@byte, result);

            Assert.Equal(@byte, value.As<byte>());

            Assert.Equal(@byte, (byte)value);
        }

        [Theory]
        [InlineData((byte)42)]
        [InlineData(byte.MinValue)]
        [InlineData(byte.MaxValue)]
        public void ByteInNullableByteOut(byte @byte)
        {
            byte source = @byte;
            Value value = new(source);
            bool success = value.TryGetValue(out byte? result);
            Assert.True(success);
            Assert.Equal(@byte, result);

            Assert.Equal(@byte, (byte?)value);
        }

        [Fact]
        public void NullByte()
        {
            byte? source = null;
            Value value = source;
            Assert.Equal(typeof(byte?), value.Type);
            Assert.Equal(source, value.As<byte?>());
            Assert.False(value.As<byte?>().HasValue);
        }
    }
}
