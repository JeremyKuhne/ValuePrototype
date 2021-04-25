using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringUShort
    {
        [Fact]
        public void UShortImplicit()
        {
            Value value = (ushort)42;
            Assert.Equal(42, value.As<ushort>());
            Assert.Equal(typeof(ushort), value.Type);

            ushort? source = 42;
            value = source;
            Assert.Equal(source, value.As<ushort?>());
            Assert.Equal(typeof(ushort), value.Type);
        }

        [Theory]
        [InlineData((ushort)42)]
        [InlineData(ushort.MinValue)]
        [InlineData(ushort.MaxValue)]
        public void UShortInOut(ushort @ushort)
        {
            Value value = new(@ushort);
            bool success = value.TryGetValue(out ushort result);
            Assert.True(success);
            Assert.Equal(@ushort, result);

            Assert.Equal(@ushort, value.As<ushort>());
            Assert.Equal(@ushort, (ushort)value);
        }

        [Theory]
        [InlineData((ushort)42)]
        [InlineData(ushort.MinValue)]
        [InlineData(ushort.MaxValue)]
        public void NullableUShortInUShortOut(ushort? @ushort)
        {
            ushort? source = @ushort;
            Value value = new(source);

            bool success = value.TryGetValue(out ushort result);
            Assert.True(success);
            Assert.Equal(@ushort, result);

            Assert.Equal(@ushort, value.As<ushort>());

            Assert.Equal(@ushort, (ushort)value);
        }

        [Theory]
        [InlineData((ushort)42)]
        [InlineData(ushort.MinValue)]
        [InlineData(ushort.MaxValue)]
        public void UShortInNullableUShortOut(ushort @ushort)
        {
            ushort source = @ushort;
            Value value = new(source);
            bool success = value.TryGetValue(out ushort? result);
            Assert.True(success);
            Assert.Equal(@ushort, result);

            Assert.Equal(@ushort, (ushort?)value);
        }

        [Fact]
        public void NullUShort()
        {
            ushort? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<ushort?>());
            Assert.False(value.As<ushort?>().HasValue);
        }
    }
}
