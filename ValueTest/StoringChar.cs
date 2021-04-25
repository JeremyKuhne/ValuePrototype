using ValuePrototype;
using Xunit;

namespace ValueTest
{
    public class StoringChar
    {
        [Fact]
        public void CharImplicit()
        {
            Value value = '!';
            Assert.Equal('!', value.As<char>());
            Assert.Equal(typeof(char), value.Type);

            char? source = '!';
            value = source;
            Assert.Equal(source, value.As<char?>());
            Assert.Equal(typeof(char?), value.Type);
        }

        [Theory]
        [InlineData('!')]
        [InlineData(char.MinValue)]
        [InlineData(char.MaxValue)]
        public void CharInOut(char @char)
        {
            Value value = new(@char);
            bool success = value.TryGetValue(out char result);
            Assert.True(success);
            Assert.Equal(@char, result);

            Assert.Equal(@char, value.As<char>());
            Assert.Equal(@char, (char)value);
        }

        [Theory]
        [InlineData('!')]
        [InlineData(char.MinValue)]
        [InlineData(char.MaxValue)]
        public void NullableCharInCharOut(char? @char)
        {
            char? source = @char;
            Value value = new(source);

            bool success = value.TryGetValue(out char result);
            Assert.True(success);
            Assert.Equal(@char, result);

            Assert.Equal(@char, value.As<char>());

            Assert.Equal(@char, (char)value);
        }

        [Theory]
        [InlineData('!')]
        [InlineData(char.MinValue)]
        [InlineData(char.MaxValue)]
        public void CharInNullableCharOut(char @char)
        {
            char source = @char;
            Value value = new(source);
            bool success = value.TryGetValue(out char? result);
            Assert.True(success);
            Assert.Equal(@char, result);

            Assert.Equal(@char, (char?)value);
        }

        [Fact]
        public void NullChar()
        {
            char? source = null;
            Value value = source;
            Assert.Equal(typeof(char?), value.Type);
            Assert.Equal(source, value.As<char?>());
            Assert.False(value.As<char?>().HasValue);
        }
    }
}
