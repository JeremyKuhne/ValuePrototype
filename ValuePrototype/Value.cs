#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast

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
            Debug.Assert(value is null || value.GetType() != typeof(Type));
            _object = value;
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
                _object = typeof(byte?);
                _union.Byte = value.Value;
            }
            else
            {
                _object = TypeFlags.NullByte;
                _union = default;
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
                _object = typeof(sbyte?);
                _union.SByte = value.Value;
            }
            else
            {
                _object = TypeFlags.NullSByte;
                _union = default;
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
                _object = typeof(bool?);
                _union.Boolean = value.Value;
            }
            else
            {
                _object = TypeFlags.NullBoolean;
                _union = default;
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
                _object = typeof(char?);
                _union.Char = value.Value;
            }
            else
            {
                _object = TypeFlags.NullChar;
                _union = default;
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
                _object = typeof(short?);
                _union.Int32 = value.Value;
            }
            else
            {
                _object = TypeFlags.NullInt16;
                _union = default;
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
                _object = typeof(int?);
                _union.Int32 = value.Value;
            }
            else
            {
                _object = TypeFlags.NullInt32;
                _union = default;
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
                _object = typeof(long?);
                _union.Int64 = value.Value;
            }
            else
            {
                _object = TypeFlags.NullInt64;
                _union = default;
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
                _object = typeof(ushort?);
                _union.UInt16 = value.Value;
            }
            else
            {
                _object = TypeFlags.NullUInt16;
                _union = default;
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
                _object = typeof(uint?);
                _union.UInt32 = value.Value;
            }
            else
            {
                _object = TypeFlags.NullUInt32;
                _union = default;
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
                _object = typeof(ulong?);
                _union.UInt64 = value.Value;
            }
            else
            {
                _object = TypeFlags.NullUInt64;
                _union = default;
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
                _object = typeof(float?);
                _union.Single = value.Value;
            }
            else
            {
                _object = TypeFlags.NullSingle;
                _union = default;
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
                _object = typeof(double?);
                _union.Double = value.Value;
            }
            else
            {
                _object = TypeFlags.NullDouble;
                _union = default;
            }
        }

        public static implicit operator Value(double value) => new(value);
        public static explicit operator double(Value value) => value.As<double>();
        public static implicit operator Value(double? value) => new(value);
        public static explicit operator double?(Value value) => value.As<double?>();
        #endregion

        #region String
        public Value(string value)
        {
            _object = value;
            _union = default;
        }

        public static implicit operator Value(string value) => new(value);

        public Value(byte[] utf8, int index, int count)
        {
            this = default;
            _object = utf8;
            _union.Segment = (index, count);
        }

        public static explicit operator string(Value value)
        {
            var str = value._object as string;
            if (str != null) return str;

            var utf8 = value._object as byte[];
            if (utf8 != null)
            {
                var segment = value._union.Segment;
                var decoded = Encoding.UTF8.GetString(utf8, segment.Index, segment.Count);
                return decoded;
            }

            throw new InvalidCastException();
        }
        #endregion

        #region DateTimeOffset
        public Value(DateTimeOffset value)
        {
            this = default;
            if (value.Offset.Ticks == 0)
            {
                _union.Ticks = value.Ticks;
                _object = typeof(DateTimeOffset);
            }
            else
            {
                _object = value;
            }
        }

        public static implicit operator Value(DateTimeOffset value) => new(value);

        public static explicit operator DateTimeOffset(Value value)
        {
            if (value._object?.Equals(typeof(DateTimeOffset)) == true)
            {
                return new DateTimeOffset(value._union.Ticks, TimeSpan.Zero);
            }

            if (value._object is DateTimeOffset dto)
            {
                return dto;
            }

            throw new InvalidCastException();
        }
        #endregion

        #region DateTime
        public Value(DateTime value)
        {
            this = default;
            if (value.Kind == DateTimeKind.Utc)
            {
                _union.Ticks = value.Ticks;
                _object = typeof(DateTime);
            }
            else
            {
                _object = value;
            }
        }

        public static implicit operator Value(DateTime value) => new(value);

        public static explicit operator DateTime(Value value)
        {
            if (value._object?.Equals(typeof(DateTime)) == true)
            {
                return new DateTime(value._union.Ticks, DateTimeKind.Utc);
            }

            if (value._object is DateTime dto)
            {
                return dto;
            }

            throw new InvalidCastException();
        }
        #endregion

        #region Decimal
        public Value(decimal value)
        {
            _object = value;
            _union = default;
        }

        public static implicit operator Value(decimal value) => new(value);
        public static explicit operator decimal(Value value) => value.As<decimal>();
        #endregion

        #region T
        public static Value Create<T>(T value)
        {
            var type = value as Type;
            if (type != null)
            {
                return new Value(new TypeBox(type));
            }

            if (value is int i32) return new Value(i32);
            if (value is double d) return new Value(d);

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
                || (typeof(T) == typeof(decimal) && _object == TypeFlags.Decimal)
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
                // A null is stored, it can only be assigned to a reference type.
                value = default!;
                return !typeof(T).IsValueType;
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
                // Requested a nullable, see if we have a the underlying type or null token.

                // TODO: Is there any way to do this with one cast for all types?
                if (nullableType == typeof(int))
                {
                    if (_object == TypeFlags.Int32 || _object == typeof(int?))
                    {
                        value = Unsafe.As<int?, T>(ref Unsafe.AsRef((int?)_union.Int32));
                        return true;
                    }
                    else if (_object == TypeFlags.NullInt32)
                    {
                        value = Unsafe.As<int?, T>(ref Unsafe.AsRef((int?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(long))
                {
                    if (_object == TypeFlags.Int64 || _object == typeof(long?))
                    {
                        value = Unsafe.As<long?, T>(ref Unsafe.AsRef((long?)_union.Int64));
                        return true;
                    }
                    else if (_object == TypeFlags.NullInt64)
                    {
                        value = Unsafe.As<long?, T>(ref Unsafe.AsRef((long?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(bool))
                {
                    if (_object == TypeFlags.Boolean || _object == typeof(bool?))
                    {
                        value = Unsafe.As<bool?, T>(ref Unsafe.AsRef((bool?)_union.Boolean));
                        return true;
                    }
                    else if (_object == TypeFlags.NullBoolean)
                    {
                        value = Unsafe.As<bool?, T>(ref Unsafe.AsRef((bool?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(float))
                {
                    if (_object == TypeFlags.Single || _object == typeof(float?))
                    {
                        value = Unsafe.As<float?, T>(ref Unsafe.AsRef((float?)_union.Single));
                        return true;
                    }
                    else if (_object == TypeFlags.NullSingle)
                    {
                        value = Unsafe.As<float?, T>(ref Unsafe.AsRef((float?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(double))
                {
                    if (_object == TypeFlags.Double || _object == typeof(double?))
                    {
                        value = Unsafe.As<double?, T>(ref Unsafe.AsRef((double?)_union.Double));
                        return true;
                    }
                    else if (_object == TypeFlags.NullDouble)
                    {
                        value = Unsafe.As<double?, T>(ref Unsafe.AsRef((double?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(uint))
                {
                    if (_object == TypeFlags.UInt32 || _object == typeof(uint?))
                    {
                        value = Unsafe.As<uint?, T>(ref Unsafe.AsRef((uint?)_union.UInt32));
                        return true;
                    }
                    else if (_object == TypeFlags.NullUInt32)
                    {
                        value = Unsafe.As<uint?, T>(ref Unsafe.AsRef((uint?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(ulong))
                {
                    if (_object == TypeFlags.UInt64 || _object == typeof(ulong?))
                    {
                        value = Unsafe.As<ulong?, T>(ref Unsafe.AsRef((ulong?)_union.UInt64));
                        return true;
                    }
                    else if (_object == TypeFlags.NullUInt64)
                    {
                        value = Unsafe.As<ulong?, T>(ref Unsafe.AsRef((ulong?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(char))
                {
                    if (_object == TypeFlags.Char || _object == typeof(char?))
                    {
                        value = Unsafe.As<char?, T>(ref Unsafe.AsRef((char?)_union.Char));
                        return true;
                    }
                    else if (_object == TypeFlags.NullChar)
                    {
                        value = Unsafe.As<char?, T>(ref Unsafe.AsRef((char?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(short))
                {
                    if (_object == TypeFlags.Int16 || _object == typeof(short?))
                    {
                        value = Unsafe.As<short?, T>(ref Unsafe.AsRef((short?)_union.Int16));
                        return true;
                    }
                    else if (_object == TypeFlags.NullInt16)
                    {
                        value = Unsafe.As<short?, T>(ref Unsafe.AsRef((short?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(ushort))
                {
                    if (_object == TypeFlags.UInt16 || _object == typeof(ushort?))
                    {
                        value = Unsafe.As<ushort?, T>(ref Unsafe.AsRef((ushort?)_union.UInt16));
                        return true;
                    }
                    else if (_object == TypeFlags.NullUInt16)
                    {
                        value = Unsafe.As<ushort?, T>(ref Unsafe.AsRef((ushort?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(byte))
                {
                    if (_object == TypeFlags.Byte || _object == typeof(byte?))
                    {
                        value = Unsafe.As<byte?, T>(ref Unsafe.AsRef((byte?)_union.Byte));
                        return true;
                    }
                    else if (_object == TypeFlags.NullByte)
                    {
                        value = Unsafe.As<byte?, T>(ref Unsafe.AsRef((byte?)null));
                        return true;
                    }
                }

                if (nullableType == typeof(sbyte))
                {
                    if (_object == TypeFlags.SByte || _object == typeof(sbyte?))
                    {
                        value = Unsafe.As<sbyte?, T>(ref Unsafe.AsRef((sbyte?)_union.SByte));
                        return true;
                    }
                    else if (_object == TypeFlags.NullSByte)
                    {
                        value = Unsafe.As<sbyte?, T>(ref Unsafe.AsRef((sbyte?)null));
                        return true;
                    }
                }

            }

            if ((typeof(T) == typeof(bool) && _object == typeof(bool?))
                || (typeof(T) == typeof(byte) && _object == typeof(byte?))
                || (typeof(T) == typeof(char) && _object == typeof(char?))
                //|| (typeof(T) == typeof(decimal) && _object == typeof(decimal?))
                || (typeof(T) == typeof(double) && _object == typeof(double?))
                || (typeof(T) == typeof(short) && _object == typeof(short?))
                || (typeof(T) == typeof(int) && _object == typeof(int?))
                || (typeof(T) == typeof(long) && _object == typeof(long?))
                || (typeof(T) == typeof(sbyte) && _object == typeof(sbyte?))
                || (typeof(T) == typeof(float) && _object == typeof(float?))
                || (typeof(T) == typeof(ushort) && _object == typeof(ushort?))
                || (typeof(T) == typeof(uint) && _object == typeof(uint?))
                || (typeof(T) == typeof(ulong) && _object == typeof(ulong?)))
            {
                // We have a nullable with the requested underlying type.
                value = CastTo<T>();
                return true;
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

#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
