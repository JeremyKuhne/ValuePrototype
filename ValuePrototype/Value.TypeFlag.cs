using System;
using System.Runtime.CompilerServices;

namespace ValuePrototype
{
    public readonly partial struct Value
    {
        private abstract class TypeFlag
        {
            public abstract Type Type
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get;
            }

            public abstract object ToObject(in Value value);
        }

        private abstract class TypeFlag<T> : TypeFlag
        {
            public sealed override Type Type
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => typeof(T);
            }

            public override object ToObject(in Value value) => To(value)!;
            public abstract T To(in Value value);
        }
    }
}
