﻿using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringBoolean
    {
        public static TheoryData<bool> BoolData => new()
        {
            { true },
            { false }
        };

        [Theory]
        [MemberData(nameof(BoolData))]
        public void BooleanImplicit(bool @bool)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = @bool;
            }

            Assert.Equal(@bool, value.As<bool>());
            Assert.Equal(typeof(bool), value.Type);

            bool? source = @bool;
            using (MemoryWatch.Create)
            {
                value = source;
            }
            Assert.Equal(source, value.As<bool?>());
            Assert.Equal(typeof(bool), value.Type);
        }

        [Theory]
        [MemberData(nameof(BoolData))]
        public void BooleanCreate(bool @bool)
        {
            Value value;
            using (MemoryWatch.Create)
            {
                value = Value.Create(@bool);
            }

            Assert.Equal(@bool, value.As<bool>());
            Assert.Equal(typeof(bool), value.Type);

            bool? source = @bool;

            using (MemoryWatch.Create)
            {
                value = Value.Create(source);
            }

            Assert.Equal(source, value.As<bool?>());
            Assert.Equal(typeof(bool), value.Type);
        }

        [Theory]
        [MemberData(nameof(BoolData))]
        public void BooleanInOut(bool @bool)
        {
            Value value;
            bool success;
            bool result;

            using (MemoryWatch.Create)
            {
                value = new(@bool);
                success = value.TryGetValue(out result);
            }

            Assert.True(success);
            Assert.Equal(@bool, result);

            Assert.Equal(@bool, value.As<bool>());
            Assert.Equal(@bool, (bool)value);
        }

        [Theory]
        [MemberData(nameof(BoolData))]
        public void NullableBooleanInBooleanOut(bool? @bool)
        {
            bool? source = @bool;
            Value value;
            bool success;
            bool result;

            using (MemoryWatch.Create)
            {
                value = new(source);
                success = value.TryGetValue(out result);
            }

            Assert.True(success);
            Assert.Equal(@bool, result);

            Assert.Equal(@bool, value.As<bool>());

            Assert.Equal(@bool, (bool)value);
        }

        [Theory]
        [MemberData(nameof(BoolData))]
        public void BooleanInNullableBooleanOut(bool @bool)
        {
            bool source = @bool;
            Value value = new(source);
            bool success = value.TryGetValue(out bool? result);
            Assert.True(success);
            Assert.Equal(@bool, result);

            Assert.Equal(@bool, (bool?)value);
        }

        [Theory]
        [MemberData(nameof(BoolData))]
        public void BoxedBoolean(bool @bool)
        {
            bool i = @bool;
            object o = i;
            Value value = new(o);

            Assert.Equal(typeof(bool), value.Type);
            Assert.True(value.TryGetValue(out bool result));
            Assert.Equal(@bool, result);
            Assert.True(value.TryGetValue(out bool? nullableResult));
            Assert.Equal(@bool, nullableResult!.Value);


            bool? n = @bool;
            o = n;
            value = new(o);

            Assert.Equal(typeof(bool), value.Type);
            Assert.True(value.TryGetValue(out result));
            Assert.Equal(@bool, result);
            Assert.True(value.TryGetValue(out nullableResult));
            Assert.Equal(@bool, nullableResult!.Value);
        }

        [Fact]
        public void NullBoolean()
        {
            bool? source = null;
            Value value;

            using (MemoryWatch.Create)
            {
                value = source;
            }

            Assert.Null(value.Type);
            Assert.Equal(source, value.As<bool?>());
            Assert.False(value.As<bool?>().HasValue);
        }
    }
}
