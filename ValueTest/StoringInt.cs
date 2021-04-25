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

            Value valueFast = new(42);
            bool success = valueFast.TryGetValue(out result);
            Assert.True(success);
            Assert.Equal(42, result);

            Assert.Equal(42, (int)valueFast);
        }

        [Fact]
        public void NullableIntInIntOut()
        {
            int? source = 42;
            ValueCompact value = new(source);
            int result = value.As<int>();
            Assert.Equal(42, result);

            Value valueFast = new(source);
            bool success = valueFast.TryGetValue(out result);
            Assert.True(success);
            Assert.Equal(42, result);

            Assert.Equal(42, (int)valueFast);
        }


        [Fact]
        public void IntInNullableIntOut()
        {
            int source = 42;
            Value valueFast = new(source);
            bool success = valueFast.TryGetValue(out int? result);
            Assert.True(success);
            Assert.Equal(42, result);

            Assert.Equal(42, (int?)valueFast);
        }

        [Fact]
        public void TypeAssertions()
        {
            byte b = default;
            short s = default;
            ushort us = default;
            int i = default;
            uint ui = default;
            long l = default;
            ulong ul = default;

            float f = default;
            double d = default;

            s = b;
            us = b;
            i = b;
            ui = b;
            l = b;
            ul = b;
            f = b;
            d = b;
        }
    }
}
