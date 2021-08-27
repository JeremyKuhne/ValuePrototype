namespace ValuePrototype;

public readonly partial struct Value
{
    [StructLayout(LayoutKind.Sequential)]
    private readonly struct NullableTemplate<T> where T : unmanaged
    {
        public readonly bool _hasValue;
        public readonly T _value;

        public NullableTemplate(T value)
        {
            _value = value;
            _hasValue = true;
        }
    }
}

