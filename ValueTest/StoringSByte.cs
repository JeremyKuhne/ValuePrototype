using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringSByte
    {
        [Fact]
        public void SByteImplicit()
        {
            Value value = (sbyte)42;
            Assert.Equal(42, value.As<sbyte>());
            Assert.Equal(typeof(sbyte), value.Type);

            sbyte? source = 42;
            value = source;
            Assert.Equal(source, value.As<sbyte?>());
            Assert.Equal(typeof(sbyte?), value.Type);
        }

        [Theory]
        [InlineData((sbyte)42)]
        [InlineData(sbyte.MinValue)]
        [InlineData(sbyte.MaxValue)]
        public void SByteInOut(sbyte @sbyte)
        {
            Value value = new(@sbyte);
            bool success = value.TryGetValue(out sbyte result);
            Assert.True(success);
            Assert.Equal(@sbyte, result);

            Assert.Equal(@sbyte, value.As<sbyte>());
            Assert.Equal(@sbyte, (sbyte)value);
        }

        [Theory]
        [InlineData((sbyte)42)]
        [InlineData(sbyte.MinValue)]
        [InlineData(sbyte.MaxValue)]
        public void NullableSByteInSByteOut(sbyte? @sbyte)
        {
            sbyte? source = @sbyte;
            Value value = new(source);

            bool success = value.TryGetValue(out sbyte result);
            Assert.True(success);
            Assert.Equal(@sbyte, result);

            Assert.Equal(@sbyte, value.As<sbyte>());

            Assert.Equal(@sbyte, (sbyte)value);
        }

        [Theory]
        [InlineData((sbyte)42)]
        [InlineData(sbyte.MinValue)]
        [InlineData(sbyte.MaxValue)]
        public void SByteInNullableSByteOut(sbyte @sbyte)
        {
            sbyte source = @sbyte;
            Value value = new(source);
            bool success = value.TryGetValue(out sbyte? result);
            Assert.True(success);
            Assert.Equal(@sbyte, result);

            Assert.Equal(@sbyte, (sbyte?)value);
        }

        [Fact]
        public void NullSByte()
        {
            sbyte? source = null;
            Value value = source;
            Assert.Equal(typeof(sbyte?), value.Type);
            Assert.Equal(source, value.As<sbyte?>());
            Assert.False(value.As<sbyte?>().HasValue);
        }
    }
}
