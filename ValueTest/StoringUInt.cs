﻿using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringUInt
    {
        public static TheoryData<uint> UIntData => new()
        {
            { 42 },
            { uint.MaxValue },
            { uint.MinValue }
        };

        [Theory]
        [MemberData(nameof(UIntData))]
        public void UIntImplicit(uint @uint)
        {
            Value value = @uint;
            Assert.Equal(@uint, value.As<uint>());
            Assert.Equal(typeof(uint), value.Type);

            uint? source = @uint;
            value = source;
            Assert.Equal(source, value.As<uint?>());
            Assert.Equal(typeof(uint), value.Type);
        }

        [Theory]
        [MemberData(nameof(UIntData))]
        public void UIntCreate(uint @uint)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = Value.Create(@uint);
            }

            Assert.Equal(@uint, value.As<uint>());
            Assert.Equal(typeof(uint), value.Type);

            uint? source = @uint;

            using (MemoryWatch.Create)
            {
                value = Value.Create(source);
            }

            Assert.Equal(source, value.As<uint?>());
            Assert.Equal(typeof(uint), value.Type);
        }

        [Theory]
        [MemberData(nameof(UIntData))]
        public void UIntInOut(uint @uint)
        {
            Value value = new(@uint);
            bool success = value.TryGetValue(out uint result);
            Assert.True(success);
            Assert.Equal(@uint, result);

            Assert.Equal(@uint, value.As<uint>());
            Assert.Equal(@uint, (uint)value);
        }

        [Theory]
        [MemberData(nameof(UIntData))]
        public void NullableUIntInUIntOut(uint? @uint)
        {
            uint? source = @uint;
            Value value = new(source);

            bool success = value.TryGetValue(out uint result);
            Assert.True(success);
            Assert.Equal(@uint, result);

            Assert.Equal(@uint, value.As<uint>());

            Assert.Equal(@uint, (uint)value);
        }

        [Theory]
        [MemberData(nameof(UIntData))]
        public void UIntInNullableUIntOut(uint @uint)
        {
            uint source = @uint;
            Value value = new(source);
            bool success = value.TryGetValue(out uint? result);
            Assert.True(success);
            Assert.Equal(@uint, result);

            Assert.Equal(@uint, (uint?)value);
        }

        [Theory]
        [MemberData(nameof(UIntData))]
        public void BoxedUInt(uint @uint)
        {
            uint i = @uint;
            object o = i;
            Value value = new(o);

            Assert.Equal(typeof(uint), value.Type);
            Assert.True(value.TryGetValue(out uint result));
            Assert.Equal(@uint, result);
            Assert.True(value.TryGetValue(out uint? nullableResult));
            Assert.Equal(@uint, nullableResult!.Value);


            uint? n = @uint;
            o = n;
            value = new(o);

            Assert.Equal(typeof(uint), value.Type);
            Assert.True(value.TryGetValue(out result));
            Assert.Equal(@uint, result);
            Assert.True(value.TryGetValue(out nullableResult));
            Assert.Equal(@uint, nullableResult!.Value);
        }

        [Fact]
        public void NullUInt()
        {
            uint? source = null;
            Value value = source;
            Assert.Null(value.Type);
            Assert.Equal(source, value.As<uint?>());
            Assert.False(value.As<uint?>().HasValue);
        }
    }
}
