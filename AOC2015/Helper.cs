namespace AOC2015;

public static class Helper
{
    public static int GCD(int a, int b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static int LCM(int a, int b)
    {
        return (a / GCD(a, b)) * b;
    }

    public static int LCM(IEnumerable<int> numbers)
    {
        return numbers.Aggregate((sum, n) => LCM(sum, n));
    }
}