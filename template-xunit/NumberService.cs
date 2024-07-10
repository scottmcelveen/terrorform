namespace template_xunit;

public class NumberService
{
    public static bool IsOdd(int value) {
        return value % 2 == 1;
    }

    public static bool IsEven(int value) {
        return value % 2 == 0;
    }

    public static int Multiply(int x, int y) {
        return x * y;
    }
}
