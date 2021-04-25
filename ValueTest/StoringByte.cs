using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringByte
    {
        public static TheoryData<byte> ByteData => new()
        {
            { 42 },
            { byte.MaxValue },
            { byte.MinValue }
        };

        [Theory]
        [MemberData(nameof(ByteData))]
        public void ByteImplicit(byte @byte)
        {
            Value value = @byte;
            Assert.Equal(@byte, value.As<byte>());
            Assert.Equal(typeof(byte), value.Type);

            byte? source = @byte;
            value = source;
            Assert.Equal(source, value.As<byte?>());
            Assert.Equal(typeof(byte), value.Type);
        }

        [Theory]
        [MemberData(nameof(ByteData))]
        public void ByteCreate(byte @byte)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = Value.Create(@byte);
            }

            Assert.Equal(@byte, value.As<byte>());
            Assert.Equal(typeof(byte), value.Type);

            byte? source = @byte;

            using (MemoryWatch.Create)
            {
                value = Value.Create(source);
            }

            Assert.Equal(source, value.As<byte?>());
            Assert.Equal(typeof(byte), value.Type);
        }

        [Theory]
        [MemberData(nameof(ByteData))]
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
        [MemberData(nameof(ByteData))]
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
        [MemberData(nameof(ByteData))]
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
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<byte?>());
            Assert.False(value.As<byte?>().HasValue);
        }
    }
}
