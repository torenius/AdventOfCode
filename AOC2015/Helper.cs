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

    public static List<List<T>> GetAllCombinations<T>(IEnumerable<T> list) where T : struct
    {
        return GetAllCombinations(new List<T>(), list.ToArray());
    }
    
    public static List<List<string>> GetAllCombinations(IEnumerable<string> list)
    {
        return GetAllCombinations(new List<string>(), list.ToArray());
    }
    
    private static List<List<T>> GetAllCombinations<T>(List<T> current, T[] possibleNext)
    {
        if (possibleNext.Length == 0)
        {
            return new List<List<T>>
            {
                current
            };
        }

        var result = new List<List<T>>();
        for (var i = 0; i < possibleNext.Length; i++)
        {
            var first = possibleNext[i];
            var left = i == 0 ? Array.Empty<T>() : possibleNext[..i];
            var right = i + 1 == possibleNext.Length ? Array.Empty<T>() : possibleNext[(i+1)..];

            var newCurrent = current.ToList();
            newCurrent.Add(first);
            
            result.AddRange(GetAllCombinations(newCurrent, left.Concat(right).ToArray()));
        }

        return result;
    }
}