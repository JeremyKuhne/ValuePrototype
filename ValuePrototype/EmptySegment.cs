using System;

namespace ValuePrototype
{
    public readonly partial struct Value
    {
        private class EmptySegment : TypeFlag
        {
            public object Array { get; } 

            public EmptySegment(object array, Type type) : base(type)
            {
                Array = array;
            }
        }
    }
}
