﻿using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringFloat
    {
        public static TheoryData<float>FloatData => new()
        {
            { 42f },
            { float.MaxValue },
            { float.MinValue },
            { float.NaN },
            { float.NegativeInfinity },
            { float.PositiveInfinity }
        };

        [Theory]
        [MemberData(nameof(FloatData))]
        public void FloatImplicit(float @float)
        {
            Value value = @float;
            Assert.Equal(@float, value.As<float>());
            Assert.Equal(typeof(float), value.Type);

            float? source = @float;
            value = source;
            Assert.Equal(source, value.As<float?>());
            Assert.Equal(typeof(float), value.Type);
        }

        [Theory]
        [MemberData(nameof(FloatData))]
        public void FloatCreate(float @float)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = Value.Create(@float);
            }

            Assert.Equal(@float, value.As<float>());
            Assert.Equal(typeof(float), value.Type);

            float? source = @float;

            using (MemoryWatch.Create)
            {
                value = Value.Create(source);
            }

            Assert.Equal(source, value.As<float?>());
            Assert.Equal(typeof(float), value.Type);
        }

        [Theory]
        [MemberData(nameof(FloatData))]
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
        [MemberData(nameof(FloatData))]
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
        [MemberData(nameof(FloatData))]
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
