using System;

namespace ValuePrototype
{
    public readonly partial struct Value
    {
        private static class TypeFlags
        {
            internal static readonly TypeFlag Boolean = new(typeof(bool));
            internal static readonly TypeFlag Char = new(typeof(char));
            internal static readonly TypeFlag Byte = new(typeof(byte));
            internal static readonly TypeFlag SByte = new(typeof(sbyte));
            internal static readonly TypeFlag Int16 = new(typeof(short));
            internal static readonly TypeFlag UInt16 = new(typeof(ushort));
            internal static readonly TypeFlag Int32 = new(typeof(int));
            internal static readonly TypeFlag UInt32 = new(typeof(uint));
            internal static readonly TypeFlag Int64 = new(typeof(long));
            internal static readonly TypeFlag UInt64 = new(typeof(ulong));
            internal static readonly TypeFlag Single = new(typeof(float));
            internal static readonly TypeFlag Double = new(typeof(double));
            internal static readonly TypeFlag DateTime = new(typeof(DateTime));
            internal static readonly TypeFlag DateTimeOffset = new(typeof(DateTimeOffset));
            internal static readonly TypeFlag PackedDateTimeOffset = new(typeof(DateTimeOffset));
        }
    }
}
