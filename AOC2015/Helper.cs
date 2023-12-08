using System.Text;

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

    public static List<int> GetDivisors(int n)
    {
        var result = new List<int>();
        switch (n)
        {
            case <= 0:
                return result;
            case 1:
                result.Add(1);
                return result;
        }

        var sqrt = (int) Math.Sqrt(n);
        for (var i = 1; i <= sqrt; i++)
        {
            if (n % i == 0)
            {
                result.Add(i);
                if (i != n / i)
                {
                    result.Add(n / i);
                }
            }
        }
        
        result.Sort();
        return result;
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

    public static void Print(char[,] matrix)
    {
        var yLength = matrix.GetLength(0);
        var xLength = matrix.GetLength(1);
        var sb = new StringBuilder(yLength * xLength + yLength * 2); // Size + NewLine chars
        
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                sb.Append(matrix[y,x]);
            }

            sb.AppendLine();
        }

        Console.Write(sb.ToString());
    }
    
    public static List<(int Y, int X)> GetAdjacentCoordinates(int y, int x) => new()
    {
        {(y - 1, x - 1)}, {(y - 1, x)}, {(y - 1, x + 1)},
                {(y, x - 1)}, {(y, x + 1)},
        {(y + 1, x - 1)}, {(y + 1, x)}, {(y + 1, x + 1)}
    };
    
    public static List<(int Y, int X)> GetNineGridCoordinates(int y, int x) => new()
    {
        {(y - 1, x - 1)}, {(y - 1, x)}, {(y - 1, x + 1)},
            {(y, x - 1)}, {(y, x)}, {(y, x + 1)},
        {(y + 1, x - 1)}, {(y + 1, x)}, {(y + 1, x + 1)}
    };

    public static char[,] Copy(char[,] input)
    {
        var yLength = input.GetLength(0);
        var xLength = input.GetLength(1);
        var matrix = new char[yLength, xLength];
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                matrix[y, x] = input[y, x];
            }
        }

        return matrix;
    }
}