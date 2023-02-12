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

    public static int Min(int a, int b)
    {
        return Math.Min(a, b);
    }

    public static int Min(int a, int b, int c)
    {
        return Min(Min(a, b), c);
    }

    public static int Max(int a, int b)
    {
        return Math.Max(a, b);
    }

    public static int Max(int a, int b, int c)
    {
        return Max(Max(a, b), c);
    }
}