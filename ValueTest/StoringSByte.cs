using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringSByte
    {
        public static TheoryData<sbyte> SByteData => new()
        {
            { 42 },
            { sbyte.MaxValue },
            { sbyte.MinValue }
        };

        [Theory]
        [MemberData(nameof(SByteData))]
        public void SByteImplicit(sbyte @sbyte)
        {
            Value value = @sbyte;
            Assert.Equal(@sbyte, value.As<sbyte>());
            Assert.Equal(typeof(sbyte), value.Type);

            sbyte? source = @sbyte;
            value = source;
            Assert.Equal(source, value.As<sbyte?>());
            Assert.Equal(typeof(sbyte), value.Type);
        }

        [Theory]
        [MemberData(nameof(SByteData))]
        public void SByteCreate(sbyte @sbyte)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = Value.Create(@sbyte);
            }

            Assert.Equal(@sbyte, value.As<sbyte>());
            Assert.Equal(typeof(sbyte), value.Type);

            sbyte? source = @sbyte;

            using (MemoryWatch.Create)
            {
                value = Value.Create(source);
            }

            Assert.Equal(source, value.As<sbyte?>());
            Assert.Equal(typeof(sbyte), value.Type);
        }

        [Theory]
        [MemberData(nameof(SByteData))]
        public void SByteInOut(sbyte @sbyte)
        {
            Value value = new(@sbyte);
            bool success = value.TryGetValue(out sbyte result);
            Assert.True(success);
            Assert.Equal(@sbyte, result);

            Assert.Equal(@sbyte, value.As<sbyte>());
            Assert.Equal(@sbyte, (sbyte)value);
        }

        [Theory]
        [MemberData(nameof(SByteData))]
        public void NullableSByteInSByteOut(sbyte? @sbyte)
        {
            sbyte? source = @sbyte;
            Value value = new(source);

            bool success = value.TryGetValue(out sbyte result);
            Assert.True(success);
            Assert.Equal(@sbyte, result);

            Assert.Equal(@sbyte, value.As<sbyte>());

            Assert.Equal(@sbyte, (sbyte)value);
        }

        [Theory]
        [MemberData(nameof(SByteData))]
        public void SByteInNullableSByteOut(sbyte @sbyte)
        {
            sbyte source = @sbyte;
            Value value = new(source);
            bool success = value.TryGetValue(out sbyte? result);
            Assert.True(success);
            Assert.Equal(@sbyte, result);

            Assert.Equal(@sbyte, (sbyte?)value);
        }

        [Fact]
        public void NullSByte()
        {
            sbyte? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<sbyte?>());
            Assert.False(value.As<sbyte?>().HasValue);
        }
    }
}
