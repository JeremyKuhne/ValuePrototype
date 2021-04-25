using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringBoolean
    {
        [Fact]
        public void BooleanImplicit()
        {
            Value value = true;
            Assert.True(value.As<bool>());
            Assert.Equal(typeof(bool), value.Type);

            bool? source = true;
            value = source;
            Assert.Equal(source, value.As<bool?>());
            Assert.Equal(typeof(bool), value.Type);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void BooleanInOut(bool @bool)
        {
            Value value = new(@bool);
            bool success = value.TryGetValue(out bool result);
            Assert.True(success);
            Assert.Equal(@bool, result);

            Assert.Equal(@bool, value.As<bool>());
            Assert.Equal(@bool, (bool)value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void NullableBooleanInBooleanOut(bool? @bool)
        {
            bool? source = @bool;
            Value value = new(source);

            bool success = value.TryGetValue(out bool result);
            Assert.True(success);
            Assert.Equal(@bool, result);

            Assert.Equal(@bool, value.As<bool>());

            Assert.Equal(@bool, (bool)value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void BooleanInNullableBooleanOut(bool @bool)
        {
            bool source = @bool;
            Value value = new(source);
            bool success = value.TryGetValue(out bool? result);
            Assert.True(success);
            Assert.Equal(@bool, result);

            Assert.Equal(@bool, (bool?)value);
        }

        [Fact]
        public void NullBoolean()
        {
            bool? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<bool?>());
            Assert.False(value.As<bool?>().HasValue);
        }
    }
}
