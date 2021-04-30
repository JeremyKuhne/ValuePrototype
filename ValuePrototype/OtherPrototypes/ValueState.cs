using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ValuePrototype
{
    /// <summary>
    /// <see cref="ValueState"/> is a wrapper that avoids boxing common value types.
    /// </summary>
    public readonly struct ValueState
    {
        private readonly Union _union;
        private readonly object? _object;
        private readonly State _state;

        /// <summary>
        /// Get the value as an object if the value is stored as an object.
        /// </summary>
        /// <param name="value">The value, if an object, or null.</param>
        /// <returns>True if the value is actually an object.</returns>
        public bool TryGetValue(out object? value)
        {
            bool isObject = _state == State.Object;
            value = isObject ? _object : null;
            return isObject;
        }

        /// <summary>
        /// Get the value as the requested type <typeparamref name="T"/> if actually stored as that type.
        /// </summary>
        /// <param name="value">The value if stored as (T), or default.</param>
        /// <returns>True if the <see cref="ValueState"/> is of the requested type.</returns>
        public unsafe bool TryGetValue<T>(out T value) where T : unmanaged
        {
            value = default;
            bool success = false;

            // Checking the type gets all of the non-relevant compares elided by the JIT
            if ((typeof(T) == typeof(bool) && _state == State.Boolean)
                || (typeof(T) == typeof(byte) && _state == State.Byte)
                || (typeof(T) == typeof(char) && _state == State.Char)
                || (typeof(T) == typeof(DateTime) && _state == State.DateTime)
                || (typeof(T) == typeof(DateTimeOffset) && _state == State.DateTimeOffset)
                || (typeof(T) == typeof(decimal) && _state == State.Decimal)
                || (typeof(T) == typeof(double) && _state == State.Double)
                || (typeof(T) == typeof(Guid) && _state == State.Guid)
                || (typeof(T) == typeof(short) && _state == State.Int16)
                || (typeof(T) == typeof(int) && _state == State.Int32)
                || (typeof(T) == typeof(long) && _state == State.Int64)
                || (typeof(T) == typeof(sbyte) && _state == State.SByte)
                || (typeof(T) == typeof(float) && _state == State.Single)
                || (typeof(T) == typeof(TimeSpan) && _state == State.TimeSpan)
                || (typeof(T) == typeof(ushort) && _state == State.UInt16)
                || (typeof(T) == typeof(uint) && _state == State.UInt32)
                || (typeof(T) == typeof(ulong) && _state == State.UInt64))
            {
                value = CastTo<T>();
                success = true;
            }

            return success;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T CastTo<T>() where T : unmanaged
        {
            T value = Unsafe.As<Union, T>(ref Unsafe.AsRef(_union));
            return value;
        }

        // We have explicit constructors for each of the supported types for performance
        // and to restrict Variant to "safe" types. Allowing any struct that would fit
        // into the Union would expose users to issues where bad struct state could cause
        // hard failures like buffer overruns etc.

        // Setting this = default is a bit simpler than setting _object and _union to
        // default and generates less assembly / faster construction.

        public ValueState(bool value)
        {
            this = default;
            _union.Boolean = value;
            _state = State.Boolean;
        }

        public ValueState(byte value)
        {
            this = default;
            _union.Byte = value;
            _state = State.Byte;
        }

        public ValueState(sbyte value)
        {
            this = default;
            _union.SByte = value;
            _state = State.SByte;
        }

        public ValueState(short value)
        {
            this = default;
            _union.Int16 = value;
            _state = State.Int16;
        }

        public ValueState(ushort value)
        {
            this = default;
            _union.UInt16 = value;
            _state = State.UInt16;
        }

        public ValueState(int value)
        {
            this = default;
            _union.Int32 = value;
            _state = State.Int32;
        }

        public ValueState(uint value)
        {
            this = default;
            _union.UInt32 = value;
            _state = State.UInt32;
        }

        public ValueState(long value)
        {
            this = default;
            _union.Int64 = value;
            _state = State.Int64;
        }

        public ValueState(ulong value)
        {
            this = default;
            _union.UInt64 = value;
            _state = State.UInt64;
        }

        public ValueState(float value)
        {
            this = default;
            _union.Single = value;
            _state = State.Single;
        }

        public ValueState(double value)
        {
            this = default;
            _union.Double = value;
            _state = State.Double;
        }

        public ValueState(decimal value)
        {
            this = default;
            _union.Decimal = value;
            _state = State.Decimal;
        }

        public ValueState(DateTime value)
        {
            this = default;
            _union.DateTime = value;
            _state = State.DateTime;
        }

        public ValueState(DateTimeOffset value)
        {
            this = default;
            _union.DateTimeOffset = value;
            _state = State.DateTimeOffset;
        }

        public ValueState(Guid value)
        {
            this = default;
            _union.Guid = value;
            _state = State.Guid;
        }

        public ValueState(object value)
        {
            this = default;
            _object = value;
            _state = State.Object;
        }

        // The Variant struct gets laid out as follows on x64:
        //
        // | 4 bytes | 4 bytes |               16 bytes                |      8 bytes      |
        // |---------|---------|---------------------------------------|-------------------|
        // |  Type   |  Unused |                Union                  |       Object      |
        //
        // Layout of the struct is automatic and cannot be modified via [StructLayout].
        // Alignment requirements force Variant to be a multiple of 8 bytes. We could
        // shrink from 32 to 24 bytes by either dropping the 16 byte types (DateTimeOffset,
        // Decimal, and Guid) or stashing the flags in the Union and leveraging flag objects
        // for the types that exceed 8 bytes. (DateTimeOffset might fit in 12, need to test.)
        //
        // We could theoretically do sneaky things with unused bits in the object pointer, much
        // like ATOMs in Window handles (lowest 64K values). Presumably that isn't doable
        // without runtime support though (putting "bad" values in an object pointer)?
        //
        // We could also allow storing arbitrary "unmanaged" values that would fit into 16 bytes.
        // In that case we could store the typeof(T) in the _object field. That probably is only
        // particularly useful for something like enums. I think we can avoid boxing, would need
        // to expose a static entry point for formatting on System.Enum. Something like:
        //
        //  public static string Format(Type enumType, ulong value)
        //  {
        //      RuntimeType rtType = enumType as RuntimeType;
        //      if (rtType == null)
        //          throw new ArgumentException(SR.Arg_MustBeType, nameof(enumType));
        //
        //      if (!enumType.IsEnum)
        //          throw new ArgumentException(SR.Arg_MustBeEnum, nameof(enumType));
        //
        //      return Enum.InternalFormat(rtType, ulong) ?? ulong.ToString();
        //  }
        //
        // That is the minbar- as the string values are cached it would be a positive. We can
        // obviously do even better if we expose a TryFormat that takes an input span. There
        // is a little bit more to that, but nothing serious.

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        private struct Union
        {
            [FieldOffset(0)] public byte Byte;
            [FieldOffset(0)] public sbyte SByte;
            [FieldOffset(0)] public char Char;
            [FieldOffset(0)] public bool Boolean;
            [FieldOffset(0)] public short Int16;
            [FieldOffset(0)] public ushort UInt16;
            [FieldOffset(0)] public int Int32;
            [FieldOffset(0)] public uint UInt32;
            [FieldOffset(0)] public long Int64;
            [FieldOffset(0)] public ulong UInt64;
            [FieldOffset(0)] public float Single;                   // 4 bytes
            [FieldOffset(0)] public double Double;                  // 8 bytes
            [FieldOffset(0)] public DateTime DateTime;              // 8 bytes  (ulong)
            [FieldOffset(0)] public DateTimeOffset DateTimeOffset;  // 16 bytes (DateTime & short)
            [FieldOffset(0)] public decimal Decimal;                // 16 bytes (4 ints)
            [FieldOffset(0)] public Guid Guid;                      // 16 bytes (int, 2 shorts, 8 bytes)
        }

        /// <summary>
        /// Get the value as an object, boxing if necessary.
        /// </summary>
        public object? Box()
        {
            return _state switch
            {
                State.Boolean => CastTo<bool>(),
                State.Byte => CastTo<byte>(),
                State.Char => CastTo<char>(),
                State.DateTime => CastTo<DateTime>(),
                State.DateTimeOffset => CastTo<DateTimeOffset>(),
                State.Decimal => CastTo<decimal>(),
                State.Double => CastTo<double>(),
                State.Guid => CastTo<Guid>(),
                State.Int16 => CastTo<short>(),
                State.Int32 => CastTo<int>(),
                State.Int64 => CastTo<long>(),
                State.Object => _object,
                State.SByte => CastTo<sbyte>(),
                State.Single => CastTo<float>(),
                State.TimeSpan => CastTo<TimeSpan>(),
                State.UInt16 => CastTo<ushort>(),
                State.UInt32 => CastTo<uint>(),
                State.UInt64 => CastTo<ulong>(),
                _ => throw new InvalidOperationException(),
            };
        }

        // Idea is that you can cast to whatever supported type you want if you're explicit.
        // Worst case is you get default or nonsense values.

        public static explicit operator bool(in ValueState variant) => variant.CastTo<bool>();
        public static explicit operator byte(in ValueState variant) => variant.CastTo<byte>();
        public static explicit operator char(in ValueState variant) => variant.CastTo<char>();
        public static explicit operator DateTime(in ValueState variant) => variant.CastTo<DateTime>();
        public static explicit operator DateTimeOffset(in ValueState variant) => variant.CastTo<DateTimeOffset>();
        public static explicit operator decimal(in ValueState variant) => variant.CastTo<decimal>();
        public static explicit operator double(in ValueState variant) => variant.CastTo<double>();
        public static explicit operator Guid(in ValueState variant) => variant.CastTo<Guid>();
        public static explicit operator short(in ValueState variant) => variant.CastTo<short>();
        public static explicit operator int(in ValueState variant) => variant.CastTo<int>();
        public static explicit operator long(in ValueState variant) => variant.CastTo<long>();
        public static explicit operator sbyte(in ValueState variant) => variant.CastTo<sbyte>();
        public static explicit operator float(in ValueState variant) => variant.CastTo<float>();
        public static explicit operator TimeSpan(in ValueState variant) => variant.CastTo<TimeSpan>();
        public static explicit operator ushort(in ValueState variant) => variant.CastTo<ushort>();
        public static explicit operator uint(in ValueState variant) => variant.CastTo<uint>();
        public static explicit operator ulong(in ValueState variant) => variant.CastTo<ulong>();

        public static implicit operator ValueState(bool value) => new(value);
        public static implicit operator ValueState(byte value) => new(value);
        public static implicit operator ValueState(char value) => new(value);
        public static implicit operator ValueState(DateTime value) => new(value);
        public static implicit operator ValueState(DateTimeOffset value) => new(value);
        public static implicit operator ValueState(decimal value) => new(value);
        public static implicit operator ValueState(double value) => new(value);
        public static implicit operator ValueState(Guid value) => new(value);
        public static implicit operator ValueState(short value) => new(value);
        public static implicit operator ValueState(int value) => new(value);
        public static implicit operator ValueState(long value) => new(value);
        public static implicit operator ValueState(sbyte value) => new(value);
        public static implicit operator ValueState(float value) => new(value);
        public static implicit operator ValueState(TimeSpan value) => new(value);
        public static implicit operator ValueState(ushort value) => new(value);
        public static implicit operator ValueState(uint value) => new(value);
        public static implicit operator ValueState(ulong value) => new(value);

        // Common object types
        public static implicit operator ValueState(string value) => new(value);

        public static ValueState Create(in ValueState variant) => variant;
        //public static Variant2 Create(in Value first, in Value second) => new Variant2(in first, in second);
        //public static Variant3 Create(in Value first, in Value second, in Value third) => new Variant3(in first, in second, in third);

        public enum State
        {
            Object,
            Byte,
            SByte,
            Char,
            Boolean,
            Int16,
            UInt16,
            Int32,
            UInt32,
            Int64,
            UInt64,
            DateTime,
            DateTimeOffset,
            TimeSpan,
            Single,
            Double,
            Decimal,
            Guid

            // TODO:
            //
            // We can support arbitrary enums, see comments near the Union definition.
            // 
            // We should also support Memory<char>. This would require access to the internals
            // so that we can save the object and the offset/index. (Memory<char> is object/int/int).
            //
            // Supporting Span<char> would require making Variant a ref struct and access to
            // internals at the very least. It isn't clear if we could simply stick a ByReference<char>
            // in here or if there would be additional need for runtime changes.
            //
            // A significant drawback of making Variant a ref struct is that you would no longer be
            // able to create Variant[] or Span<Variant>.
        }
    }
}