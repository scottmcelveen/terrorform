using Xunit;

namespace template_xunit.Tests
{
    public class NumberServiceTests
    {
        [Fact]
        public void Multiply_ReturnsCorrectResult()
        {
            // Arrange
            int x = 5;
            int y = 3;
            int expected = 15;

            // Act
            int actual = NumberService.Multiply(x, y);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}