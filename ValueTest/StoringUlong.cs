using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringULong
    {
        public static TheoryData<ulong> ULongData => new()
        {
            { 42 },
            { ulong.MaxValue },
            { ulong.MinValue }
        };

        [Theory]
        [MemberData(nameof(ULongData))]
        public void ULongImplicit(ulong @ulong)
        {
            Value value = @ulong;
            Assert.Equal(@ulong, value.As<ulong>());
            Assert.Equal(typeof(ulong), value.Type);

            ulong? source = @ulong;
            value = source;
            Assert.Equal(source, value.As<ulong?>());
            Assert.Equal(typeof(ulong), value.Type);
        }

        [Theory]
        [MemberData(nameof(ULongData))]
        public void ULongCreate(ulong @ulong)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = Value.Create(@ulong);
            }

            Assert.Equal(@ulong, value.As<ulong>());
            Assert.Equal(typeof(ulong), value.Type);

            ulong? source = @ulong;

            using (MemoryWatch.Create)
            {
                value = Value.Create(source);
            }

            Assert.Equal(source, value.As<ulong?>());
            Assert.Equal(typeof(ulong), value.Type);
        }

        [Theory]
        [MemberData(nameof(ULongData))]
        public void ULongInOut(ulong @ulong)
        {
            Value value = new(@ulong);
            bool success = value.TryGetValue(out ulong result);
            Assert.True(success);
            Assert.Equal(@ulong, result);

            Assert.Equal(@ulong, value.As<ulong>());
            Assert.Equal(@ulong, (ulong)value);
        }

        [Theory]
        [MemberData(nameof(ULongData))]
        public void NullableULongInULongOut(ulong? @ulong)
        {
            ulong? source = @ulong;
            Value value = new(source);

            bool success = value.TryGetValue(out ulong result);
            Assert.True(success);
            Assert.Equal(@ulong, result);

            Assert.Equal(@ulong, value.As<ulong>());

            Assert.Equal(@ulong, (ulong)value);
        }

        [Theory]
        [MemberData(nameof(ULongData))]
        public void ULongInNullableULongOut(ulong @ulong)
        {
            ulong source = @ulong;
            Value value = new(source);
            bool success = value.TryGetValue(out ulong? result);
            Assert.True(success);
            Assert.Equal(@ulong, result);

            Assert.Equal(@ulong, (ulong?)value);
        }

        [Theory]
        [MemberData(nameof(ULongData))]
        public void BoxedULong(ulong @ulong)
        {
            ulong i = @ulong;
            object o = i;
            Value value = new(o);

            Assert.Equal(typeof(ulong), value.Type);
            Assert.True(value.TryGetValue(out ulong result));
            Assert.Equal(@ulong, result);
            Assert.True(value.TryGetValue(out ulong? nullableResult));
            Assert.Equal(@ulong, nullableResult!.Value);


            ulong? n = @ulong;
            o = n;
            value = new(o);

            Assert.Equal(typeof(ulong), value.Type);
            Assert.True(value.TryGetValue(out result));
            Assert.Equal(@ulong, result);
            Assert.True(value.TryGetValue(out nullableResult));
            Assert.Equal(@ulong, nullableResult!.Value);
        }

        [Fact]
        public void NullULong()
        {
            ulong? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<ulong?>());
            Assert.False(value.As<ulong?>().HasValue);
        }

        [Theory]
        [MemberData(nameof(ULongData))]
        public void OutAsObject(ulong @ulong)
        {
            Value value = new(@ulong);
            object o = value.As<object>();
            Assert.Equal(typeof(ulong), o.GetType());
            Assert.Equal(@ulong, (ulong)o);

            ulong? n = @ulong;
            value = new(n);
            o = value.As<object>();
            Assert.Equal(typeof(ulong), o.GetType());
            Assert.Equal(@ulong, (ulong)o);
        }
    }
}
