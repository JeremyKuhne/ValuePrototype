namespace ValuePrototype
{
    public readonly partial struct ValueCompactFast
    {
        private static class TypeFlags
        {
            internal static readonly TypeFlag Boolean = new(typeof(bool));
            internal static readonly TypeFlag NullBoolean = new(typeof(bool?));
            internal static readonly TypeFlag Char = new(typeof(char));
            internal static readonly TypeFlag NullChar = new(typeof(char?));
            internal static readonly TypeFlag Byte = new(typeof(byte));
            internal static readonly TypeFlag NullByte = new(typeof(byte?));
            internal static readonly TypeFlag SByte = new(typeof(sbyte));
            internal static readonly TypeFlag NullSByte = new(typeof(sbyte?));
            internal static readonly TypeFlag Int16 = new(typeof(short));
            internal static readonly TypeFlag NullInt16 = new(typeof(short?));
            internal static readonly TypeFlag UInt16 = new(typeof(ushort));
            internal static readonly TypeFlag NullUInt16 = new(typeof(ushort?));
            internal static readonly TypeFlag Int32 = new(typeof(int));
            internal static readonly TypeFlag NullInt32 = new(typeof(int?));
            internal static readonly TypeFlag UInt32 = new(typeof(uint));
            internal static readonly TypeFlag NullUInt32 = new(typeof(uint?));
            internal static readonly TypeFlag Int64 = new(typeof(long));
            internal static readonly TypeFlag NullInt64 = new(typeof(long?));
            internal static readonly TypeFlag UInt64 = new(typeof(long));
            internal static readonly TypeFlag NullUInt64 = new(typeof(long?));
            internal static readonly TypeFlag Single = new(typeof(float));
            internal static readonly TypeFlag NullSingle = new(typeof(float?));
            internal static readonly TypeFlag Double = new(typeof(double));
            internal static readonly TypeFlag NullDouble = new(typeof(double?));
            internal static readonly TypeFlag Decimal = new(typeof(decimal));
            internal static readonly TypeFlag NullDecimal = new(typeof(decimal?));
        }
    }
}
