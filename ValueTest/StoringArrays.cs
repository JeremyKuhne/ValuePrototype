using System;
using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringArrays
    {
        [Fact]
        public void ByteArray()
        {
            byte[] b = new byte[10];
            Value value;

            value = Value.Create(b);
            Assert.Equal(typeof(byte[]), value.Type);
            Assert.Same(b, value.As<byte[]>());
            Assert.Equal(b, (byte[])value.As<object>());

            Assert.Throws<InvalidCastException>(() => value.As<ArraySegment<byte>>());
        }

        [Fact]
        public void CharArray()
        {
            char[] b = new char[10];
            Value value;

            value = Value.Create(b);
            Assert.Equal(typeof(char[]), value.Type);
            Assert.Same(b, value.As<char[]>());
            Assert.Equal(b, (char[])value.As<object>());

            Assert.Throws<InvalidCastException>(() => value.As<ArraySegment<char>>());
        }

        [Fact]
        public void ByteSegment()
        {
            byte[] b = new byte[10];
            Value value;

            ArraySegment<byte> segment = new(b);
            value = Value.Create(segment);
            Assert.Equal(typeof(ArraySegment<byte>), value.Type);
            Assert.Equal(segment, value.As<ArraySegment<byte>>());
            Assert.Equal(segment, (ArraySegment<byte>)value.As<object>());
            Assert.Throws<InvalidCastException>(() => value.As<byte[]>());

            segment = new(b, 0, 0);
            value = Value.Create(segment);
            Assert.Equal(typeof(ArraySegment<byte>), value.Type);
            Assert.Equal(segment, value.As<ArraySegment<byte>>());
            Assert.Equal(segment, (ArraySegment<byte>)value.As<object>());
            Assert.Throws<InvalidCastException>(() => value.As<byte[]>());

            segment = new(b, 1, 1);
            value = Value.Create(segment);
            Assert.Equal(typeof(ArraySegment<byte>), value.Type);
            Assert.Equal(segment, value.As<ArraySegment<byte>>());
            Assert.Equal(segment, (ArraySegment<byte>)value.As<object>());
            Assert.Throws<InvalidCastException>(() => value.As<byte[]>());
        }

        [Fact]
        public void CharSegment()
        {
            char[] b = new char[10];
            Value value;

            ArraySegment<char> segment = new(b);
            value = Value.Create(segment);
            Assert.Equal(typeof(ArraySegment<char>), value.Type);
            Assert.Equal(segment, value.As<ArraySegment<char>>());
            Assert.Equal(segment, (ArraySegment<char>)value.As<object>());
            Assert.Throws<InvalidCastException>(() => value.As<char[]>());

            segment = new(b, 0, 0);
            value = Value.Create(segment);
            Assert.Equal(typeof(ArraySegment<char>), value.Type);
            Assert.Equal(segment, value.As<ArraySegment<char>>());
            Assert.Equal(segment, (ArraySegment<char>)value.As<object>());
            Assert.Throws<InvalidCastException>(() => value.As<char[]>());

            segment = new(b, 1, 1);
            value = Value.Create(segment);
            Assert.Equal(typeof(ArraySegment<char>), value.Type);
            Assert.Equal(segment, value.As<ArraySegment<char>>());
            Assert.Equal(segment, (ArraySegment<char>)value.As<object>());
            Assert.Throws<InvalidCastException>(() => value.As<char[]>());
        }
    }
}
