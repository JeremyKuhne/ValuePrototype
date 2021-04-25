using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringDouble
    {
        [Fact]
        public void DoubleImplicit()
        {
            Value value = 42.0d;
            Assert.Equal(42.0d, value.As<double>());
            Assert.Equal(typeof(double), value.Type);

            double? source = 42.0d;
            value = source;
            Assert.Equal(source, value.As<double?>());
            Assert.Equal(typeof(double), value.Type);
        }

        [Theory]
        [InlineData(42d)]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        [InlineData(double.NaN)]
        public void DoubleInOut(double @double)
        {
            Value value = new(@double);
            bool success = value.TryGetValue(out double result);
            Assert.True(success);
            Assert.Equal(@double, result);

            Assert.Equal(@double, value.As<double>());
            Assert.Equal(@double, (double)value);
        }

        [Theory]
        [InlineData(42d)]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        [InlineData(double.NaN)]
        public void NullableDoubleInDoubleOut(double? @double)
        {
            double? source = @double;
            Value value = new(source);

            bool success = value.TryGetValue(out double result);
            Assert.True(success);
            Assert.Equal(@double, result);

            Assert.Equal(@double, value.As<double>());

            Assert.Equal(@double, (double)value);
        }

        [Theory]
        [InlineData(42d)]
        [InlineData(double.MaxValue)]
        [InlineData(double.MinValue)]
        [InlineData(double.NaN)]
        public void DoubleInNullableDoubleOut(double @double)
        {
            double source = @double;
            Value value = new(source);
            bool success = value.TryGetValue(out double? result);
            Assert.True(success);
            Assert.Equal(@double, result);

            Assert.Equal(@double, (double)value);
        }

        [Fact]
        public void NullDouble()
        {
            double? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<double?>());
            Assert.False(value.As<double?>().HasValue);
        }
    }
}
