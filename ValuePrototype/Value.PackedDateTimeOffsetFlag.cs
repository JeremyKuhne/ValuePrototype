using System;

namespace ValuePrototype
{
    public readonly partial struct Value
    {
        private sealed class PackedDateTimeOffsetFlag : TypeFlag<DateTimeOffset>
        {
            public static PackedDateTimeOffsetFlag Instance { get; } = new();

            public override DateTimeOffset To(in Value value)
                => value._union.PackedDateTimeOffset.Extract();
        }
    }
}
