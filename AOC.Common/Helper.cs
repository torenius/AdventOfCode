using System.Drawing;
using System.Text;

namespace AOC.Common;

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
    
    public static IEnumerable<T> BreadthFirst<T>(T start, Func<T, IEnumerable<T>> children) where T : IEquatable<T>
    {
        var previous = new HashSet<T> {start};

        var queue = new Queue<T>();
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            var current = queue.Dequeue();
            yield return current;
            foreach (var neighbor in children(current).Where(x => !previous.Contains(x)))
            {
                previous.Add(neighbor);
                queue.Enqueue(neighbor);
            }
        }
    }
    
    public static Func<T, IEnumerable<T>> ShortestPath<T>(T start, Func<T, IEnumerable<T>> children) where T : IEquatable<T>
    {
        var previous = new Dictionary<T, T> {{start, start}};

        var queue = new Queue<T>();
        queue.Enqueue(start);

        while (queue.Count != 0)
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
    
    public static (Dictionary<T, int> Costs, Dictionary<T, T> Parents, Func<T, IEnumerable<(T Node, int Cost)>> Path) Dijkstra<T>(Helper.DijkstraGraph<T> graph, T start) where T : IEquatable<T>
    {
        var queue = new Dictionary<T, int>();
        var costs = new Dictionary<T, int>();
        var parents = new Dictionary<T, T>();
        var alreadyDequeued = new HashSet<T>();
        
        // Populate queue
        foreach (var vertex in graph.Connections.Keys)
        {
            queue.Add(vertex, int.MaxValue);
        }
        
        // Set start to zero so that will be the first to dequeue
        queue[start] = 0;
        
        costs.Add(start, 0);
        parents.Add(start, start);

        while (queue.Count > 0)
        {
            var q = queue.OrderBy(x => x.Value).Select(x => new
            {
                Current = x.Key,
                Priority = x.Value
            }).First();

            queue.Remove(q.Current);

            alreadyDequeued.Add(q.Current);

            // Add or update cost
            if (!costs.TryAdd(q.Current, q.Priority))
            {
                costs[q.Current] = q.Priority;
            } ;
            
            foreach(var edge in graph.Connections[q.Current])
            {
                // For undirected graphs
                var destination = edge.Source.Equals(q.Current) ? edge.Destination : edge.Source;
                
                // If already dequeued, we already found a shorter path
                if (alreadyDequeued.Contains(destination))
                {
                    continue;
                }

                var cost = costs[q.Current] + edge.Cost;

                var currentCost = queue[destination];
                if (cost < currentCost)
                {
                    queue[destination] = cost;

                    if (!parents.TryAdd(destination, q.Current))
                    {
                        parents[destination] = q.Current;
                    }
                }
            }
        }

        return (costs, parents, Path);

        IEnumerable<(T Node, int Cost)> Path(T end)
        {
            try
            {
                var path = new List<(T, int)>();

                var current = end;
                while (!current.Equals(start))
                {
                    var cost = costs[current];
                    path.Add((current, cost));

                    if (parents.TryGetValue(current, out var value))
                    {
                        current = value;
                    }
                    else
                    {
                        return Enumerable.Empty<(T, int)>();
                    }
                }

                return path;
            }
            catch
            {
                return Enumerable.Empty<(T, int)>();
            }
        }
    }
    
    public class DijkstraGraph<T> where T : IEquatable<T>
    {
        public Dictionary<T, List<DijkstraEdge<T>>> Connections { get; private set; } = new();

        public void AddDirectedEdge(T source, T destination, int cost)
        {
            if (Connections.ContainsKey(source))
            {
                Connections[source].Add(new DijkstraEdge<T>(source, destination, cost));
            }
            else
            {
                Connections.Add(source, new List<DijkstraEdge<T>>{ new (source, destination, cost) });
            }

            if (!Connections.ContainsKey(destination))
            {
                Connections.Add(destination, new List<DijkstraEdge<T>>());
            }
        }

        public void AddUndirectedEdge(T source, T destination, int cost)
        {
            if (Connections.ContainsKey(source))
            {
                Connections[source].Add(new DijkstraEdge<T>(source, destination, cost));
            }
            else
            {
                Connections.Add(source, new List<DijkstraEdge<T>>{ new (source, destination, cost) });
            }
            
            if (Connections.ContainsKey(destination))
            {
                Connections[destination].Add(new DijkstraEdge<T>(destination, source, cost));
            }
            else
            {
                Connections.Add(destination, new List<DijkstraEdge<T>>{ new (destination, source, cost) });
            }
        }
    }
    public class DijkstraEdge<T>
    {
        public DijkstraEdge(T source, T destination, int cost)
        {
            Source = source;
            Destination = destination;
            Cost = cost;
        }

        public DijkstraEdge()
        {
        }

        public T Source { get; init; }
        public T Destination { get; init; }
        public int Cost { get; init; }
    }
    
    public static void Print(char[,] matrix, List<Point> points, ConsoleColor pointColor = ConsoleColor.DarkRed)
    {
        var defaultColor = Console.ForegroundColor;
        var yLength = matrix.GetLength(0);
        var xLength = matrix.GetLength(1);
        
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                Console.ForegroundColor = points.Any(p => p.X == x && p.Y == y) ? pointColor : defaultColor;
                Console.Write(matrix[y, x]);
            }

            Console.WriteLine();
        }
        
        Console.WriteLine();
        Console.ForegroundColor = defaultColor;
    }
    
    public static void Print(int[,] matrix, List<Point> points, ConsoleColor pointColor = ConsoleColor.DarkRed)
    {
        var defaultColor = Console.ForegroundColor;
        var yLength = matrix.GetLength(0);
        var xLength = matrix.GetLength(1);
        
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                Console.ForegroundColor = points.Any(p => p.X == x && p.Y == y) ? pointColor : defaultColor;
                Console.Write(matrix[y, x]);
            }

            Console.WriteLine();
        }
        
        Console.WriteLine();
        Console.ForegroundColor = defaultColor;
    }
    
    
    public static void Print(char[,] matrix, Dictionary<char, ConsoleColor> colorMapping)
    {
        var defaultColor = Console.ForegroundColor;
        var yLength = matrix.GetLength(0);
        var xLength = matrix.GetLength(1);
        
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                Console.ForegroundColor = colorMapping.GetValueOrDefault(matrix[y, x], defaultColor);
                Console.Write(matrix[y, x]);
            }

            Console.WriteLine();
        }
        
        Console.WriteLine();
        Console.ForegroundColor = defaultColor;
    }
    
    public static void Print(List<Point> points)
    {
        var minY = points.Min(e => e.Y);
        var maxY = points.Max(e => e.Y);
        var minX = points.Min(e => e.X);
        var maxX = points.Max(e => e.X);

        var sb = new StringBuilder();
        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                sb.Append(points.Any(e => e.X == x && e.Y == y) ? '#' : '.');
            }

            sb.AppendLine();
        }
        
        Console.WriteLine(sb.ToString());
    }

    public static IEnumerable<char[]> GetPossibleCombinations(int length, params char[] letters)
    {
        var queue = new Queue<char[]>();
        queue.Enqueue([]);

        while (queue.Peek().Length < length)
        {
            var current = queue.Dequeue();
            foreach (var letter in letters)
            {
                var next = current.Append(letter).ToArray();
                queue.Enqueue(next);

                if (next.Length == length)
                {
                    yield return next;
                }
            }
        }
    }
    
    public static IEnumerable<int[]> GetPossibleCombinations(int length, int[] numbers)
    {
        var queue = new Queue<int[]>();
        queue.Enqueue([]);

        while (queue.Peek().Length < length)
        {
            var current = queue.Dequeue();
            foreach (var letter in numbers)
            {
                var next = current.Append(letter).ToArray();
                queue.Enqueue(next);

                if (next.Length == length)
                {
                    yield return next;
                }
            }
        }
    }
}
