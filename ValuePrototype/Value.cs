using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace ValuePrototype
{
    public readonly partial struct Value
    {
        private readonly Union _union;
        private readonly object? _object;

        public Value(object? value)
        {
            _object = value is Type type ? new TypeBox(type) : value;
            _union = default;
        }

        public Type? Type
        {
            get
            {
                if (_object is null)
                {
                    return null;
                }

                var type = _object as Type;
                if (type != null)
                {
                    return type;
                }

                if (_object is TypeFlag typeFlag)
                {
                    return typeFlag.Type;
                }

                type = _object.GetType();

                if (type == typeof(byte[]))
                {
                    return typeof(string);
                }

                if (type == typeof(TypeBox))
                {
                    return typeof(Type);
                }

                return type;
            }
        }

        private static void ThrowInvalidCast() => throw new InvalidCastException();
        private static void ThrowNotImplemented() => throw new NotImplementedException();

        #region Byte
        public Value(byte value)
        {
            this = default;
            _object = TypeFlags.Byte;
            _union.Byte = value;
        }

        public Value(byte? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Byte;
                _union.Byte = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(byte value) => new(value);
        public static explicit operator byte(Value value) => value.As<byte>();
        public static implicit operator Value(byte? value) => new(value);
        public static explicit operator byte?(Value value) => value.As<byte?>();
        #endregion

        #region SByte
        public Value(sbyte value)
        {
            this = default;
            _object = TypeFlags.SByte;
            _union.SByte = value;
        }

        public Value(sbyte? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.SByte;
                _union.SByte = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(sbyte value) => new(value);
        public static explicit operator sbyte(Value value) => value.As<sbyte>();
        public static implicit operator Value(sbyte? value) => new(value);
        public static explicit operator sbyte?(Value value) => value.As<sbyte?>();
        #endregion

        #region Boolean
        public Value(bool value)
        {
            this = default;
            _object = TypeFlags.Boolean;
            _union.Boolean = value;
        }

        public Value(bool? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Boolean;
                _union.Boolean = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(bool value) => new(value);
        public static explicit operator bool(Value value) => value.As<bool>();
        public static implicit operator Value(bool? value) => new(value);
        public static explicit operator bool?(Value value) => value.As<bool?>();
        #endregion

        #region Char
        public Value(char value)
        {
            this = default;
            _object = TypeFlags.Char;
            _union.Char = value;
        }

        public Value(char? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Char;
                _union.Char = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(char value) => new(value);
        public static explicit operator char(Value value) => value.As<char>();
        public static implicit operator Value(char? value) => new(value);
        public static explicit operator char?(Value value) => value.As<char?>();
        #endregion

        #region Int16
        public Value(short value)
        {
            this = default;
            _object = TypeFlags.Int16;
            _union.Int16 = value;
        }

        public Value(short? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Int16;
                _union.Int16 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(short value) => new(value);
        public static explicit operator short(Value value) => value.As<short>();
        public static implicit operator Value(short? value) => new(value);
        public static explicit operator short?(Value value) => value.As<short?>();
        #endregion

        #region Int32
        public Value(int value)
        {
            this = default;
            _object = TypeFlags.Int32;
            _union.Int32 = value;
        }

        public Value(int? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Int32;
                _union.Int32 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(int value) => new(value);
        public static explicit operator int(Value value) => value.As<int>();
        public static implicit operator Value(int? value) => new(value);
        public static explicit operator int?(Value value) => value.As<int?>();
        #endregion

        #region Int64
        public Value(long value)
        {
            this = default;
            _object = TypeFlags.Int64;
            _union.Int64 = value;
        }

        public Value(long? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Int64;
                _union.Int64 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(long value) => new(value);
        public static explicit operator long(Value value) => value.As<long>();
        public static implicit operator Value(long? value) => new(value);
        public static explicit operator long?(Value value) => value.As<long?>();
        #endregion

        #region UInt16
        public Value(ushort value)
        {
            this = default;
            _object = TypeFlags.UInt16;
            _union.UInt16 = value;
        }

        public Value(ushort? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.UInt16;
                _union.UInt16 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(ushort value) => new(value);
        public static explicit operator ushort(Value value) => value.As<ushort>();
        public static implicit operator Value(ushort? value) => new(value);
        public static explicit operator ushort?(Value value) => value.As<ushort?>();
        #endregion

        #region UInt32
        public Value(uint value)
        {
            this = default;
            _object = TypeFlags.UInt32;
            _union.UInt32 = value;
        }

        public Value(uint? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.UInt32;
                _union.UInt32 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(uint value) => new(value);
        public static explicit operator uint(Value value) => value.As<uint>();
        public static implicit operator Value(uint? value) => new(value);
        public static explicit operator uint?(Value value) => value.As<uint?>();
        #endregion

        #region UInt64
        public Value(ulong value)
        {
            this = default;
            _object = TypeFlags.UInt64;
            _union.UInt64 = value;
        }

        public Value(ulong? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.UInt64;
                _union.UInt64 = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(ulong value) => new(value);
        public static explicit operator ulong(Value value) => value.As<ulong>();
        public static implicit operator Value(ulong? value) => new(value);
        public static explicit operator ulong?(Value value) => value.As<ulong?>();
        #endregion

        #region Single
        public Value(float value)
        {
            this = default;
            _object = TypeFlags.Single;
            _union.Single = value;
        }

        public Value(float? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Single;
                _union.Single = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(float value) => new(value);
        public static explicit operator float(Value value) => value.As<float>();
        public static implicit operator Value(float? value) => new(value);
        public static explicit operator float?(Value value) => value.As<float?>();
        #endregion

        #region Double
        public Value(double value)
        {
            this = default;
            _object = TypeFlags.Double;
            _union.Double = value;
        }

        public Value(double? value)
        {
            this = default;
            if (value.HasValue)
            {
                _object = TypeFlags.Double;
                _union.Double = value.Value;
            }
            else
            {
                _object = null;
            }
        }

        public static implicit operator Value(double value) => new(value);
        public static explicit operator double(Value value) => value.As<double>();
        public static implicit operator Value(double? value) => new(value);
        public static explicit operator double?(Value value) => value.As<double?>();
        #endregion

        #region DateTimeOffset
        public Value(DateTimeOffset value)
        {
            this = default;
            if (value.Offset.Ticks == 0)
            {
                // This is a UTC time
                _union.Ticks = value.Ticks;
                _object = TypeFlags.DateTimeOffset;
            }
            else
            {
                _object = value;
            }
        }

        public Value(DateTimeOffset? value)
        {
            this = default;
            if (value.HasValue && value.Value.Offset.Ticks == 0)
            {
                _object = TypeFlags.DateTimeOffset;
                _union.Ticks = value.Value.Ticks;
            }
            else
            {
                _object = value;
            }
        }

        public static implicit operator Value(DateTimeOffset value) => new(value);
        public static explicit operator DateTimeOffset(Value value) => value.As<DateTimeOffset>();
        public static implicit operator Value(DateTimeOffset? value) => new(value);
        public static explicit operator DateTimeOffset?(Value value) => value.As<DateTimeOffset?>();
        #endregion

        #region DateTime
        public Value(DateTime value)
        {
            this = default;
            if (value.Kind == DateTimeKind.Utc)
            {
                _union.Ticks = value.Ticks;
                _object = TypeFlags.DateTime;
            }
            else
            {
                _object = value;
            }
        }

        public Value(DateTime? value)
        {
            this = default;
            if (value.HasValue && value.Value.Kind == DateTimeKind.Utc)
            {
                _object = TypeFlags.DateTime;
                _union.Ticks = value.Value.Ticks;
            }
            else
            {
                _object = value;
            }
        }

        public static implicit operator Value(DateTime value) => new(value);
        public static explicit operator DateTime(Value value) => value.As<DateTime>();
        public static implicit operator Value(DateTime? value) => new(value);
        public static explicit operator DateTime?(Value value) => value.As<DateTime?>();
        #endregion

        #region Decimal
        public static implicit operator Value(decimal value) => new(value);
        public static explicit operator decimal(Value value) => value.As<decimal>();
        public static implicit operator Value(decimal? value) => value.HasValue ? new(value.Value) : new(value);
        public static explicit operator decimal?(Value value) => value.As<decimal?>();
        #endregion

        #region T
        public static Value Create<T>(T value)
        {
            // Explicit cast for types we don't box
            if (typeof(T) == typeof(bool)) return new(Unsafe.As<T, bool>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(byte)) return new(Unsafe.As<T, byte>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(sbyte)) return new(Unsafe.As<T, sbyte>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(char)) return new(Unsafe.As<T, char>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(short)) return new(Unsafe.As<T, short>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(int)) return new(Unsafe.As<T, int>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(long)) return new(Unsafe.As<T, long>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(ushort)) return new(Unsafe.As<T, ushort>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(uint)) return new(Unsafe.As<T, uint>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(ulong)) return new(Unsafe.As<T, ulong>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(float)) return new(Unsafe.As<T, float>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(double)) return new(Unsafe.As<T, double>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(DateTime)) return new(Unsafe.As<T, DateTime>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(DateTimeOffset)) return new(Unsafe.As<T, DateTimeOffset>(ref Unsafe.AsRef(value)));

            if (typeof(T) == typeof(bool?)) return new(Unsafe.As<T, bool?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(byte?)) return new(Unsafe.As<T, byte?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(sbyte?)) return new(Unsafe.As<T, sbyte?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(char?)) return new(Unsafe.As<T, char?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(short?)) return new(Unsafe.As<T, short?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(int?)) return new(Unsafe.As<T, int?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(long?)) return new(Unsafe.As<T, long?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(ushort?)) return new(Unsafe.As<T, ushort?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(uint?)) return new(Unsafe.As<T, uint?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(ulong?)) return new(Unsafe.As<T, ulong?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(float?)) return new(Unsafe.As<T, float?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(double?)) return new(Unsafe.As<T, double?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(DateTime?)) return new(Unsafe.As<T, DateTime?>(ref Unsafe.AsRef(value)));
            if (typeof(T) == typeof(DateTimeOffset?)) return new(Unsafe.As<T, DateTimeOffset?>(ref Unsafe.AsRef(value)));

            return new Value(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe bool TryGetValue<T>(out T value)
        {
            bool success;

            // Checking the type gets all of the non-relevant compares elided by the JIT
            if (_object is not null && ((typeof(T) == typeof(bool) && _object == TypeFlags.Boolean)
                || (typeof(T) == typeof(byte) && _object == TypeFlags.Byte)
                || (typeof(T) == typeof(char) && _object == TypeFlags.Char)
                || (typeof(T) == typeof(double) && _object == TypeFlags.Double)
                || (typeof(T) == typeof(short) && _object == TypeFlags.Int16)
                || (typeof(T) == typeof(int) && _object == TypeFlags.Int32)
                || (typeof(T) == typeof(long) && _object == TypeFlags.Int64)
                || (typeof(T) == typeof(sbyte) && _object == TypeFlags.SByte)
                || (typeof(T) == typeof(float) && _object == TypeFlags.Single)
                || (typeof(T) == typeof(ushort) && _object == TypeFlags.UInt16)
                || (typeof(T) == typeof(uint) && _object == TypeFlags.UInt32)
                || (typeof(T) == typeof(ulong) && _object == TypeFlags.UInt64)))
            {
                value = CastTo<T>();
                success = true;
            }
            else if (typeof(T) == typeof(DateTime) && _object == TypeFlags.DateTime)
            {
                value = Unsafe.As<DateTime, T>(ref Unsafe.AsRef(new DateTime(_union.Ticks, DateTimeKind.Utc)));
                success = true;
            }
            else if (typeof(T) == typeof(DateTimeOffset) && _object == TypeFlags.DateTimeOffset)
            {
                value = Unsafe.As<DateTimeOffset, T>(ref Unsafe.AsRef(new DateTimeOffset(_union.Ticks, TimeSpan.Zero)));
                success = true;
            }
            else
            {
                success = TryGetSlow(out value);
            }

            return success;
        }

        private bool TryGetSlow<T>(out T value)
        {
            if (_object is null)
            {
                // A null is stored, it can only be assigned to a reference type or nullable.
                value = default!;
                return !typeof(T).IsValueType || Nullable.GetUnderlyingType(typeof(T)) is not null;
            }

            Type objectType = _object.GetType();

            if (objectType == typeof(T) || typeof(T).IsAssignableFrom(objectType))
            {
                // Same, or assignable.
                value = (T)_object;
                return true;
            }

            if (Nullable.GetUnderlyingType(typeof(T)) is Type nullableType)
            {
                // Requested a nullable, see if we have a the underlying type.

                // TODO: Is there any way to do this with one cast for all types?
                if (nullableType == typeof(int) && _object == TypeFlags.Int32)
                {
                    value = Unsafe.As<int?, T>(ref Unsafe.AsRef((int?)_union.Int32));
                    return true;
                }

                if (nullableType == typeof(long) && _object == TypeFlags.Int64)
                {
                    value = Unsafe.As<long?, T>(ref Unsafe.AsRef((long?)_union.Int64));
                    return true;
                }

                if (nullableType == typeof(bool) && _object == TypeFlags.Boolean)
                {
                    value = Unsafe.As<bool?, T>(ref Unsafe.AsRef((bool?)_union.Boolean));
                    return true;
                }

                if (nullableType == typeof(float) && _object == TypeFlags.Single)
                {
                    value = Unsafe.As<float?, T>(ref Unsafe.AsRef((float?)_union.Single));
                    return true;
                }

                if (nullableType == typeof(double) && _object == TypeFlags.Double)
                {
                    value = Unsafe.As<double?, T>(ref Unsafe.AsRef((double?)_union.Double));
                    return true;
                }

                if (nullableType == typeof(uint) && _object == TypeFlags.UInt32)
                {
                    value = Unsafe.As<uint?, T>(ref Unsafe.AsRef((uint?)_union.UInt32));
                    return true;
                }

                if (nullableType == typeof(ulong) && _object == TypeFlags.UInt64)
                {
                    value = Unsafe.As<ulong?, T>(ref Unsafe.AsRef((ulong?)_union.UInt64));
                    return true;
                }

                if (nullableType == typeof(char) && _object == TypeFlags.Char)
                {
                    value = Unsafe.As<char?, T>(ref Unsafe.AsRef((char?)_union.Char));
                    return true;
                }

                if (nullableType == typeof(short) && _object == TypeFlags.Int16)
                {
                    value = Unsafe.As<short?, T>(ref Unsafe.AsRef((short?)_union.Int16));
                    return true;
                }

                if (nullableType == typeof(ushort) && _object == TypeFlags.UInt16)
                {
                    value = Unsafe.As<ushort?, T>(ref Unsafe.AsRef((ushort?)_union.UInt16));
                    return true;
                }

                if (nullableType == typeof(byte) && _object == TypeFlags.Byte)
                {
                    value = Unsafe.As<byte?, T>(ref Unsafe.AsRef((byte?)_union.Byte));
                    return true;
                }

                if (nullableType == typeof(sbyte) && _object == TypeFlags.SByte)
                {
                    value = Unsafe.As<sbyte?, T>(ref Unsafe.AsRef((sbyte?)_union.SByte));
                    return true;
                }

                if (nullableType == typeof(DateTime) && _object == TypeFlags.DateTime)
                {
                    value = Unsafe.As<DateTime?, T>(ref Unsafe.AsRef((DateTime?)new DateTime(_union.Ticks, DateTimeKind.Utc)));
                    return true;
                }

                if (nullableType == typeof(DateTimeOffset) && _object == TypeFlags.DateTimeOffset)
                {
                    value = Unsafe.As<DateTimeOffset?, T>(ref Unsafe.AsRef((DateTimeOffset?)new DateTimeOffset(_union.Ticks, TimeSpan.Zero)));
                    return true;
                }
            }

            if (typeof(T) == typeof(Type) && _object is TypeBox box)
            {
                // The value was actually a Type object.
                value = (T)(object)box.Value;
                return true;
            }

            value = default!;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T As<T>()
        {
            if (!TryGetValue<T>(out T value))
            {
                ThrowInvalidCast();
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T CastTo<T>()
        {
            Debug.Assert(typeof(T).IsPrimitive);
            T value = Unsafe.As<Union, T>(ref Unsafe.AsRef(_union));
            return value;
        }
#endregion
    }
}

