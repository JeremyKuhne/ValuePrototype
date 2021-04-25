using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringInt
    {
        public static TheoryData<int> IntData => new()
        {
            { 42 },
            { int.MaxValue },
            { int.MinValue }
        };

        [Theory]
        [MemberData(nameof(IntData))]
        public void IntImplicit(int @int)
        {
            Value value = @int;
            Assert.Equal(@int, value.As<int>());
            Assert.Equal(typeof(int), value.Type);

            int? source = @int;
            value = source;
            Assert.Equal(source, value.As<int?>());
            Assert.Equal(typeof(int), value.Type);
        }

        [Theory]
        [MemberData(nameof(IntData))]
        public void IntCreate(int @int)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = Value.Create(@int);
            }

            Assert.Equal(@int, value.As<int>());
            Assert.Equal(typeof(int), value.Type);

            int? source = @int;

            using (MemoryWatch.Create)
            {
                value = Value.Create(source);
            }

            Assert.Equal(source, value.As<int?>());
            Assert.Equal(typeof(int), value.Type);
        }

        [Theory]
        [MemberData(nameof(IntData))]
        public void IntInOut(int @int)
        {
            Value value = new(@int);
            bool success = value.TryGetValue(out int result);
            Assert.True(success);
            Assert.Equal(@int, result);

            Assert.Equal(@int, value.As<int>());
            Assert.Equal(@int, (int)value);
        }

        [Theory]
        [MemberData(nameof(IntData))]
        public void NullableIntInIntOut(int? @int)
        {
            int? source = @int;
            Value value = new(source);

            bool success = value.TryGetValue(out int result);
            Assert.True(success);
            Assert.Equal(@int, result);

            Assert.Equal(@int, value.As<int>());

            Assert.Equal(@int, (int)value);
        }

        [Theory]
        [MemberData(nameof(IntData))]
        public void IntInNullableIntOut(int @int)
        {
            int source = @int;
            Value value = new(source);
            bool success = value.TryGetValue(out int? result);
            Assert.True(success);
            Assert.Equal(@int, result);

            Assert.Equal(@int, (int?)value);
        }

        [Fact]
        public void NullInt()
        {
            int? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<int?>());
            Assert.False(value.As<int?>().HasValue);
        }
    }
}
