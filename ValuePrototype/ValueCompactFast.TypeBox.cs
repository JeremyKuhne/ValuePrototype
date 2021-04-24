using System;

namespace ValuePrototype
{
    public readonly partial struct ValueCompactFast
    {
        private class TypeBox
        {
            public TypeBox(Type value) => Value = value;
            public Type Value { get; }
        }
    }
}
