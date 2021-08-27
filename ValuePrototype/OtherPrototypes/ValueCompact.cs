#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast

namespace ValuePrototype;

public readonly struct ValueCompact
{
    private static readonly object NullInt32 = new();

    private readonly long _i64;
    private readonly object? _obj;

    public ValueCompact(object? obj)
    {
        Debug.Assert(obj is null || obj.GetType() != typeof(Type));
        _obj = obj;
        _i64 = 0;
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

            type = _obj.GetType();

            if (type == typeof(byte[])) return typeof(string);
            if (type == typeof(object))
            {
                if (_obj == NullInt32) return typeof(int?);
            }
            if (type == typeof(TypeBox)) return typeof(Type);

            return type;
        }
    }

    #region Int32
    public ValueCompact(int value)
    {
        _obj = typeof(int);
        _i64 = value;
    }

    public static implicit operator ValueCompact(int value)
    {
        return new ValueCompact(value);
    }

    public static explicit operator int(ValueCompact variant)
    {
        if (variant._obj is null || !variant._obj.Equals(typeof(int))) throw new InvalidCastException();
        return (int)variant._i64;
    }

    #endregion

    #region Nullable<Int32>
    public ValueCompact(int? value)
    {
        if (value.HasValue)
        {
            _obj = typeof(int?);
            _i64 = value.Value;
        }
        else
        {
            _obj = NullInt32;
            _i64 = default;
        }
    }

    public static implicit operator ValueCompact(int? value)
    {
        return new ValueCompact(value);
    }

    public static explicit operator int?(ValueCompact variant)
    {
        if (variant._obj == NullInt32) return null;
        if (variant._obj is not null && variant._obj.Equals(typeof(int?))) return (int)variant._i64;
        throw new InvalidCastException();
    }

    #endregion

    #region Double
    public ValueCompact(double value)
    {
        _obj = typeof(double);
        _i64 = Unsafe.As<double, long>(ref value);
    }

    public static implicit operator ValueCompact(double value)
    {
        return new ValueCompact(value);
    }

    public static explicit operator double(ValueCompact variant)
    {
        if (variant._obj is null || !variant._obj.Equals(typeof(double))) throw new InvalidCastException();
        var representation = variant._i64;
        return Unsafe.As<long, double>(ref representation);
    }
    #endregion

    #region String
    public ValueCompact(string value)
    {
        _obj = value;
        _i64 = default;
    }

    public static implicit operator ValueCompact(string value)
    {
        return new ValueCompact(value);
    }

    public ValueCompact(byte[] utf8, int index, int count)
    {
        _obj = utf8;
        _i64 = index << 32 | count;
    }

    public static explicit operator string(ValueCompact variant)
    {
        var str = variant._obj as string;
        if (str != null) return str;

        var utf8 = variant._obj as byte[];
        if (utf8 != null)
        {
            var decoded = Encoding.UTF8.GetString(utf8, (int)(variant._i64 << 32), (int)variant._i64);
            return decoded;
        }

        throw new InvalidCastException();
    }
    #endregion

    #region DateTimeOffset
    public ValueCompact(DateTimeOffset value)
    {
        if (value.Offset.Ticks == 0)
        {
            _i64 = value.Ticks;
            _obj = typeof(DateTimeOffset);
        }
        else
        {
            _obj = value;
            _i64 = 0;
        }
    }

    public static implicit operator ValueCompact(DateTimeOffset value)
    {
        return new ValueCompact(value);
    }

    public static explicit operator DateTimeOffset(ValueCompact variant)
    {
        if (variant._obj?.Equals(typeof(DateTimeOffset)) == true)
        {
            return new DateTimeOffset(variant._i64, TimeSpan.Zero);
        }

        if (variant._obj is DateTimeOffset dto)
        {
            return dto;
        }

        throw new InvalidCastException();
    }
    #endregion

    #region DateTime
    public ValueCompact(DateTime value)
    {
        if (value.Kind == DateTimeKind.Utc)
        {
            _i64 = value.Ticks;
            _obj = typeof(DateTime);
        }
        else
        {
            _obj = value;
            _i64 = 0;
        }
    }

    public static implicit operator ValueCompact(DateTime value)
    {
        return new ValueCompact(value);
    }

    public static explicit operator DateTime(ValueCompact variant)
    {
        if (variant._obj?.Equals(typeof(DateTime)) == true)
        {
            return new DateTime(variant._i64, DateTimeKind.Utc);
        }

        if (variant._obj is DateTime dto)
        {
            return dto;
        }

        throw new InvalidCastException();
    }
    #endregion

    #region Decimal
    public ValueCompact(decimal value)
    {
        _obj = value;
        _i64 = default;
    }

    public static implicit operator ValueCompact(decimal value)
    {
        return new ValueCompact(value);
    }

    public static explicit operator decimal(ValueCompact variant)
    {
        if (variant._obj is decimal value)
        {
            return value;
        }

        throw new InvalidCastException();
    }
    #endregion

    #region T
    public static ValueCompact Create<T>(T value)
    {
        var type = value as Type;
        if (type != null)
        {
            return new ValueCompact(new TypeBox(type));
        }

        if (value is int i32) return new ValueCompact(i32);
        if (value is double d) return new ValueCompact(d);

        return new ValueCompact(value);
    }

    public T? As<T>()
    {
        // if value is stored in _i64 field
        if (_obj == typeof(T))
        {
            if (_obj.Equals(typeof(int)))
            {
                return CastTo<T>();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        // if value is stored in _obj field
        if (_obj?.GetType() == typeof(T)) return (T)_obj;

        // if value is stored in a "box"
        if (typeof(T) == typeof(Type) && _obj is TypeBox box)
        {
            return (T)(object)box.Value;
        }

        if (_obj is null && !typeof(T).IsValueType)
        {
            return default;
        }

        if (typeof(T) == typeof(int) && _obj == typeof(int?))
        {
            return CastTo<T>();
        }

        throw new InvalidCastException();
    }
    #endregion

    private class TypeBox
    {
        private readonly Type _value;

        public TypeBox(Type value) => _value = value;

        public Type Value => _value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private T CastTo<T>()
    {
        Debug.Assert(typeof(T).IsPrimitive);
        T value = Unsafe.As<long, T>(ref Unsafe.AsRef(_i64));
        return value;
    }
}

#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
