namespace AOC2023;

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
    
    public static long GCD(long a, long b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static int LCM(int a, int b) => (a / GCD(a, b)) * b;
    public static long LCM(long a, long b) => (a / GCD(a, b)) * b;

    public static int LCM(IEnumerable<int> numbers)
    {
        return numbers.Aggregate((sum, n) => LCM(sum, n));
    }
    public static long LCM(IEnumerable<long> numbers)
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
    
    public static Func<T, IEnumerable<T>> ShortestPath<T>(T start, Func<T, IEnumerable<T>> children) where T : IEquatable<T>
    {
        var previous = new Dictionary<T, T> {{start, start}};

        var queue = new Queue<T>();
        queue.Enqueue(start);

        while (queue.Any())
        {
            var current = queue.Dequeue();
            foreach (var neighbor in children(current).Where(x => !previous.ContainsKey(x)))
            {
                previous.Add(neighbor, current);
                queue.Enqueue(neighbor);
            }
        }
        
        return Path;

        IEnumerable<T> Path(T end)
        {
            var path = new List<T>();

            var current = end;
            while (!current.Equals(start))
            {
                path.Add(current);

                if (previous.TryGetValue(current, out var value))
                {
                    current = value;
                }
                else
                {
                    return new List<T>();
                }
            }

            return path;
        }
    }
}
