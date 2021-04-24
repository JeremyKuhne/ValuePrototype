#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast

using System;

namespace ValuePrototype
{
    public readonly partial struct ValueCompactFast
    {
        private class TypeFlag
        {
            public TypeFlag(Type value) => Type = value;
            public Type Type { get; }
        }
    }
}

#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
