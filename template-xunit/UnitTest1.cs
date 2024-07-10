namespace template_xunit;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Assert.Equal("test", "test");
    }

    [Theory]
    [InlineData(3)]
    [InlineData(5)]
    public void NumberServiceIsOddTheory(int value)
    {
        Assert.True(NumberService.IsOdd(value));
    }
}