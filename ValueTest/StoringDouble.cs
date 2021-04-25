using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringDouble
    {
        public static TheoryData<double> DoubleData => new()
        {
            { 42d },
            { double.MaxValue },
            { double.MinValue },
            { double.NaN },
            { double.NegativeInfinity },
            { double.PositiveInfinity }
        };

        [Theory]
        [MemberData(nameof(DoubleData))]
        public void DoubleImplicit(double @double)
        {
            Value value = @double;
            Assert.Equal(@double, value.As<double>());
            Assert.Equal(typeof(double), value.Type);

            double? source = @double;
            value = source;
            Assert.Equal(source, value.As<double?>());
            Assert.Equal(typeof(double), value.Type);
        }

        [Theory]
        [MemberData(nameof(DoubleData))]
        public void DoubleCreate(double @double)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = Value.Create(@double);
            }

            Assert.Equal(@double, value.As<double>());
            Assert.Equal(typeof(double), value.Type);

            double? source = @double;

            using (MemoryWatch.Create)
            {
                value = Value.Create(source);
            }

            Assert.Equal(source, value.As<double?>());
            Assert.Equal(typeof(double), value.Type);
        }

        [Theory]
        [MemberData(nameof(DoubleData))]
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
        [MemberData(nameof(DoubleData))]
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
        [MemberData(nameof(DoubleData))]
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
