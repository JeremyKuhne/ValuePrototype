using System;

namespace ValuePrototype
{
    public readonly partial struct Value
    {
        private class TypeBox : TypeFlag
        {
            public TypeBox(Type value) : base(typeof(Type))
            {
                Value = value;
            }

            public Type Value { get; }
        }
    }
}
