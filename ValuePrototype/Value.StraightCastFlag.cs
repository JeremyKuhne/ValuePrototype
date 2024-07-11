namespace ValuePrototype;

public readonly partial struct Value
{
    private sealed class StraightCastFlag<T> : TypeFlag<T>
    {
        public static StraightCastFlag<T> Instance { get; } = new();

        public override T To(in Value value)
            => Unsafe.As<Union, T>(ref Unsafe.AsRef(in value._union));
    }
}
