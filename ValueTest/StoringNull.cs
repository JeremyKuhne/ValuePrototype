using System;
using Xunit;
using ValuePrototype;

namespace ValueTest
{
    public class StoringNull
    {
        [Fact]
        public void GetIntFromStoredNull()
        {
            ValueCompact nullValue = new((object)null);
            Assert.Throws<InvalidCastException>(() => _ = nullValue.As<int>());
        }
    }
}
