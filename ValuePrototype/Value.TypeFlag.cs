using System;

namespace ValuePrototype
{
    public readonly partial struct Value
    {
        private class TypeFlag
        {
            public TypeFlag(Type value) => Type = value;
            public Type Type { get; }
        }
    }
}
