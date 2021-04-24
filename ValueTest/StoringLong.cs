using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringLong
    {
        [Theory]
        [InlineData(42)]
        [InlineData(long.MaxValue)]
        public void LongInOut(long @long)
        {
            ValueCompactFast valueFast = new(@long);
            bool success = valueFast.TryGetValue(out long result);
            Assert.True(success);
            Assert.Equal(@long, result);

            Assert.Equal(@long, valueFast.As<long>());
            Assert.Equal(@long, (long)valueFast);
        }

        [Theory]
        [InlineData(42)]
        [InlineData(long.MaxValue)]
        public void NullableLongInLongOut(long? @long)
        {
            long? source = @long;
            ValueCompactFast valueFast = new(source);

            bool success = valueFast.TryGetValue(out long result);
            Assert.True(success);
            Assert.Equal(@long, result);

            Assert.Equal(@long, valueFast.As<long>());

            Assert.Equal(@long, (long)valueFast);
        }

        [Theory]
        [InlineData(42)]
        [InlineData(long.MaxValue)]
        public void LongInNullableLongOut(long @long)
        {
            long source = @long;
            ValueCompactFast valueFast = new(source);
            bool success = valueFast.TryGetValue(out long? result);
            Assert.True(success);
            Assert.Equal(@long, result);

            Assert.Equal(@long, (long?)valueFast);
        }
    }
}
