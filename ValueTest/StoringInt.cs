using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringInt
    {
        [Fact]
        public void IntInOut()
        {
            ValueCompact value = new(42);
            int result = value.As<int>();
            Assert.Equal(42, result);
        }
    }
}
