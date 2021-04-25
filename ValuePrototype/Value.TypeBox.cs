using System;

namespace ValuePrototype
{
    public readonly partial struct Value
    {
        private class TypeBox
        {
            public TypeBox(Type value) => Value = value;
            public Type Value { get; }
        }
    }
}
