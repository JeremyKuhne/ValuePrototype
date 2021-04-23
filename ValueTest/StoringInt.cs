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
    }
}
