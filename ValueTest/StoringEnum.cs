using System;
using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringEnum
    {
        [Fact]
        public void BasicFunctionality()
        {
            DayOfWeek day = DayOfWeek.Monday;
            // Hitting this the first time will allocate as it JITs
            Value value = Value.Create(day);

            MemoryWatch watch = MemoryWatch.Create;
            value = Value.Create(day);
            DayOfWeek outDay = value.As<DayOfWeek>();
            watch.Validate();

            Assert.Equal(day, outDay);
            Assert.Equal(typeof(DayOfWeek), value.Type);
        }
    }
}
