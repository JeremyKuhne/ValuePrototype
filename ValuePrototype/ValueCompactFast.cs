﻿#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace ValuePrototype
{
    public readonly partial struct ValueCompactFast
    {
        private static readonly TypeFlag Int32 = new(typeof(int));
        private static readonly TypeFlag NullInt32 = new(typeof(int?));
        private static readonly TypeFlag Int64 = new(typeof(long));
        private static readonly TypeFlag NullInt64 = new(typeof(long?));

        private readonly Union _union;
        private readonly object? _obj;

        public ValueCompactFast(object? obj)
        {
            Debug.Assert(obj is null || obj.GetType() != typeof(Type));
            _obj = obj;
            _union = default;
        }

        public Type? Type
        {
            get
            {
                if (_obj is null)
                {
                    return null;
                }

                var type = _obj as Type;
                if (type != null) return type;

                if (_obj is TypeFlag typeFlag)
                {
                    return typeFlag.Type;
                }

                type = _obj.GetType();

                if (type == typeof(byte[])) return typeof(string);
                if (type == typeof(object))
                {
                    if (_obj == NullInt32) return typeof(int?);
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

        #region Int32
        public ValueCompactFast(int value)
        {
            this = default;
            _obj = Int32;
            _union.Int32 = value;
        }

        public static implicit operator ValueCompactFast(int value) => new(value);
        public static explicit operator int(ValueCompactFast variant) => variant.As<int>();
        #endregion

        #region Nullable<Int32>
        public ValueCompactFast(int? value)
        {
            this = default;
            if (value.HasValue)
            {
                _obj = typeof(int?);
                _union.Int32 = value.Value;
            }
            else
            {
                _obj = NullInt32;
                _union = default;
            }
        }

        public static implicit operator ValueCompactFast(int? value) => new(value);
        public static explicit operator int?(ValueCompactFast variant) => variant.As<int?>();
        #endregion

        #region Int64
        public ValueCompactFast(long value)
        {
            this = default;
            _obj = Int64;
            _union.Int64 = value;
        }

        public static implicit operator ValueCompactFast(long value) => new(value);
        public static explicit operator long(ValueCompactFast variant) => variant.As<long>();
        #endregion

        #region Nullable<Int64>
        public ValueCompactFast(long? value)
        {
            this = default;
            if (value.HasValue)
            {
                _obj = typeof(long?);
                _union.Int64 = value.Value;
            }
            else
            {
                _obj = NullInt64;
                _union = default;
            }
        }

        public static implicit operator ValueCompactFast(long? value) => new(value);
        public static explicit operator long?(ValueCompactFast variant) => variant.As<long?>();
        #endregion

        #region Double
        public ValueCompactFast(double value)
        {
            this = default;
            _obj = typeof(double);
            _union.Double = value;
        }

        public static implicit operator ValueCompactFast(double value)
        {
            return new ValueCompactFast(value);
        }

        public static explicit operator double(ValueCompactFast variant)
        {
            if (variant._obj is null || !variant._obj.Equals(typeof(double))) ThrowInvalidCast();
            var representation = variant._union;
            return representation.Double;
        }
        #endregion

        #region String
        public ValueCompactFast(string value)
        {
            _obj = value;
            _union = default;
        }

        public static implicit operator ValueCompactFast(string value)
        {
            return new ValueCompactFast(value);
        }

        public ValueCompactFast(byte[] utf8, int index, int count)
        {
            this = default;
            _obj = utf8;
            _union.Segment = (index, count);
        }

        public static explicit operator string(ValueCompactFast variant)
        {
            var str = variant._obj as string;
            if (str != null) return str;

            var utf8 = variant._obj as byte[];
            if (utf8 != null)
            {
                var segment = variant._union.Segment;
                var decoded = Encoding.UTF8.GetString(utf8, segment.Index, segment.Count);
                return decoded;
            }

            throw new InvalidCastException();
        }
        #endregion

        #region DateTimeOffset
        public ValueCompactFast(DateTimeOffset value)
        {
            this = default;
            if (value.Offset.Ticks == 0)
            {
                _union.Ticks = value.Ticks;
                _obj = typeof(DateTimeOffset);
            }
            else
            {
                _obj = value;
            }
        }

        public static implicit operator ValueCompactFast(DateTimeOffset value)
        {
            return new ValueCompactFast(value);
        }

        public static explicit operator DateTimeOffset(ValueCompactFast variant)
        {
            if (variant._obj?.Equals(typeof(DateTimeOffset)) == true)
            {
                return new DateTimeOffset(variant._union.Ticks, TimeSpan.Zero);
            }

            if (variant._obj is DateTimeOffset dto)
            {
                return dto;
            }

            throw new InvalidCastException();
        }
        #endregion

        #region DateTime
        public ValueCompactFast(DateTime value)
        {
            this = default;
            if (value.Kind == DateTimeKind.Utc)
            {
                _union.Ticks = value.Ticks;
                _obj = typeof(DateTime);
            }
            else
            {
                _obj = value;
            }
        }

        public static implicit operator ValueCompactFast(DateTime value)
        {
            return new ValueCompactFast(value);
        }

        public static explicit operator DateTime(ValueCompactFast variant)
        {
            if (variant._obj?.Equals(typeof(DateTime)) == true)
            {
                return new DateTime(variant._union.Ticks, DateTimeKind.Utc);
            }

            if (variant._obj is DateTime dto)
            {
                return dto;
            }

            throw new InvalidCastException();
        }
        #endregion

        #region Decimal
        public ValueCompactFast(decimal value)
        {
            _obj = value;
            _union = default;
        }

        public static implicit operator ValueCompactFast(decimal value)
        {
            return new ValueCompactFast(value);
        }

        public static explicit operator decimal(ValueCompactFast variant)
        {
            if (variant._obj is decimal value)
            {
                return value;
            }

            throw new InvalidCastException();
        }
        #endregion

        #region T
        public static ValueCompactFast Create<T>(T value)
        {
            var type = value as Type;
            if (type != null)
            {
                return new ValueCompactFast(new TypeBox(type));
            }

            if (value is int i32) return new ValueCompactFast(i32);
            if (value is double d) return new ValueCompactFast(d);

            return new ValueCompactFast(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe bool TryGetValue<T>(out T value)
        {
            bool success;

            // Checking the type gets all of the non-relevant compares elided by the JIT
            if (_obj is not null && ((typeof(T) == typeof(bool) && _obj == typeof(bool))
                || (typeof(T) == typeof(byte) && _obj == typeof(byte))
                || (typeof(T) == typeof(char) && _obj == typeof(char))
                || (typeof(T) == typeof(decimal) && _obj == typeof(decimal))
                || (typeof(T) == typeof(double) && _obj == typeof(double))
                || (typeof(T) == typeof(short) && _obj == typeof(short))
                || (typeof(T) == typeof(int) && _obj == Int32)
                || (typeof(T) == typeof(long) && _obj == Int64)
                || (typeof(T) == typeof(sbyte) && _obj == typeof(sbyte))
                || (typeof(T) == typeof(float) && _obj == typeof(float))
                || (typeof(T) == typeof(ushort) && _obj == typeof(ushort))
                || (typeof(T) == typeof(uint) && _obj == typeof(uint))
                || (typeof(T) == typeof(ulong) && _obj == typeof(ulong))))
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
            if (_obj is null)
            {
                // A null is stored, it can only be assigned to a reference type.
                value = default!;
                return !typeof(T).IsValueType;
            }

            Type objectType = _obj.GetType();

            if (objectType == typeof(T) || typeof(T).IsAssignableFrom(objectType))
            {
                // Same, or assignable.
                value = (T)_obj;
                return true;
            }

            if (Nullable.GetUnderlyingType(typeof(T)) is Type nullableType)
            {
                // Requested a nullable, see if we have a the underlying type.

                // TODO: Is there any way to do this with one cast for all types?
                if (nullableType == typeof(int) && _obj == Int32)
                {
                    value = Unsafe.As<int?, T>(ref Unsafe.AsRef((int?)_union.Int32));
                    return true;
                }

                if (nullableType == typeof(long) && _obj == Int64)
                {
                    value = Unsafe.As<long?, T>(ref Unsafe.AsRef((long?)_union.Int64));
                    return true;
                }
            }

            if ((typeof(T) == typeof(bool) && _obj == typeof(bool?))
                || (typeof(T) == typeof(byte) && _obj == typeof(byte?))
                || (typeof(T) == typeof(char) && _obj == typeof(char?))
                || (typeof(T) == typeof(decimal) && _obj == typeof(decimal?))
                || (typeof(T) == typeof(double) && _obj == typeof(double?))
                || (typeof(T) == typeof(short) && _obj == typeof(short?))
                || (typeof(T) == typeof(int) && _obj == typeof(int?))
                || (typeof(T) == typeof(long) && _obj == typeof(long?))
                || (typeof(T) == typeof(sbyte) && _obj == typeof(sbyte?))
                || (typeof(T) == typeof(float) && _obj == typeof(float?))
                || (typeof(T) == typeof(ushort) && _obj == typeof(ushort?))
                || (typeof(T) == typeof(uint) && _obj == typeof(uint?))
                || (typeof(T) == typeof(ulong) && _obj == typeof(ulong?)))
            {
                // Value is nullable
                value = CastTo<T>();
                return true;
            }


            if (typeof(T) == typeof(Type) && _obj is TypeBox box)
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
