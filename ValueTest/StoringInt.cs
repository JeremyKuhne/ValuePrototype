using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringInt
    {
        [Fact]
        public void IntInOut()
        {
            ValueCompact value = new(42);
            int result = value.As<int>();
            Assert.Equal(42, result);

            ValueCompactFast valueFast = new(42);
            bool success = valueFast.TryGetValue(out result);
            Assert.True(success);
            Assert.Equal(42, result);
        }

        [Fact]
        public void NullableIntInIntOut()
        {
            int? source = 42;
            ValueCompact value = new(source);
            int result = value.As<int>();
            Assert.Equal(42, result);

            ValueCompactFast valueFast = new(source);
            bool success = valueFast.TryGetValue(out result);
            Assert.True(success);
            Assert.Equal(42, result);
        }


        [Fact]
        public void IntInNullableIntOut()
        {
            int source = 42;
            ValueCompactFast valueFast = new(source);
            bool success = valueFast.TryGetValue(out int? result);
            Assert.True(success);
            Assert.Equal((int?)42, result);
        }
    }
}
