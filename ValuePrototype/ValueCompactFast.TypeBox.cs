#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast

using System;

namespace ValuePrototype
{
    public readonly partial struct ValueCompactFast
    {
        #endregion

        private class TypeBox
        {
            public TypeBox(Type value) => Value = value;
            public Type Value { get; }
        }
    }
}

#pragma warning restore CS0252 // Possible unintended reference comparison; left hand side needs cast
