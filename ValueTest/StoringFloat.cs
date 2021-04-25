using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringFloat
    {
        [Fact]
        public void FloatImplicit()
        {
            Value value = 42.0f;
            Assert.Equal(42.0f, value.As<float>());
            Assert.Equal(typeof(float), value.Type);

            float? source = 42.0f;
            value = source;
            Assert.Equal(source, value.As<float?>());
            Assert.Equal(typeof(float), value.Type);
        }

        [Theory]
        [InlineData(42.0f)]
        [InlineData(float.MaxValue)]
        [InlineData(float.MinValue)]
        [InlineData(float.NaN)]
        public void FloatInOut(float @float)
        {
            Value value = new(@float);
            bool success = value.TryGetValue(out float result);
            Assert.True(success);
            Assert.Equal(@float, result);

            Assert.Equal(@float, value.As<float>());
            Assert.Equal(@float, (float)value);
        }

        [Theory]
        [InlineData(42.0f)]
        [InlineData(float.MaxValue)]
        [InlineData(float.MinValue)]
        [InlineData(float.NaN)]
        public void NullableFloatInFloatOut(float? @float)
        {
            float? source = @float;
            Value value = new(source);

            bool success = value.TryGetValue(out float result);
            Assert.True(success);
            Assert.Equal(@float, result);

            Assert.Equal(@float, value.As<float>());

            Assert.Equal(@float, (float)value);
        }

        [Theory]
        [InlineData(42.0f)]
        [InlineData(float.MaxValue)]
        [InlineData(float.MinValue)]
        [InlineData(float.NaN)]
        public void FloatInNullableFloatOut(float @float)
        {
            float source = @float;
            Value value = new(source);
            bool success = value.TryGetValue(out float? result);
            Assert.True(success);
            Assert.Equal(@float, result);

            Assert.Equal(@float, (float?)value);
        }

        [Fact]
        public void NullFloat()
        {
            float? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<float?>());
            Assert.False(value.As<float?>().HasValue);
        }
    }
}
