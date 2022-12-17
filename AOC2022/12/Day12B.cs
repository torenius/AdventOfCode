using System.Drawing;

namespace AOC2022._12;

public class Day12B : Day
{
    protected override string Run()
    {
        var input = GetInputAsCharMatrix();
        FindAndReplace(input, 'S', 'a');
        var end = FindAndReplace(input, 'E', 'z');

        var map = ConvertToIntArray(input);

        var minPath = int.MaxValue;
        var shortestPaths = ShortestPath(map, end);

        foreach (var start in FindAllA(map))
        {
            var path = shortestPaths(start).ToList();
            if (path.Any())
            {
                if (path.Count - 1 < minPath)
                {
                    minPath = path.Count - 1;
                    Console.WriteLine(string.Join(", ", path));
                }
            }
        }

        return "" + minPath;
    }

    private static List<Point> FindAllA(int[,] map)
    {
        var possibleStarts = new List<Point>();
        for (var y = 0; y < map.GetLength(0); y++)
        {
            for (var x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 'a')
                {
                    possibleStarts.Add(new Point(x, y));
                }
            }
        }

        return possibleStarts;
    }

    private static Func<Point, IEnumerable<Point>> ShortestPath(int[,] map, Point start)
    {
        var previous = new Dictionary<Point, Point>();

        var queue = new Queue<Point>();
        queue.Enqueue(start);

        while (queue.Any())
        {
            var current = queue.Dequeue();
            foreach (var neighbor in GetNeighbors(map, current).Where(neighbor => !previous.ContainsKey(neighbor)))
            {
                previous.Add(neighbor, current);
                queue.Enqueue(neighbor);
            }
        }

        Func<Point, IEnumerable<Point>> shortestPath = v => {
            var path = new List<Point>();

            var current = v;
            while (!current.Equals(start)) {
                path.Add(current);

                if (previous.TryGetValue(current, out var value))
                {
                    current = value;
                }
                else
                {
                    return new List<Point>();
                }
            };

            path.Add(start);
            path.Reverse();

            return path;
        };

        return shortestPath;
    }

    private static List<Point> GetNeighbors(int[,] map, Point current)
    {
        var neighbors = new List<Point>();

        // Right
        if (current.X + 1 < map.GetLength(1) && map[current.Y, current.X + 1] >= map[current.Y, current.X] - 1)
        {
            neighbors.Add(current with {X = current.X + 1});
        }
        
        // Left
        if (current.X - 1 >= 0 && map[current.Y, current.X - 1] >= map[current.Y, current.X] - 1)
        {
            neighbors.Add(current with {X = current.X - 1});
        }
        
        // Down
        if (current.Y + 1 < map.GetLength(0) && map[current.Y + 1, current.X] >= map[current.Y, current.X] - 1)
        {
            neighbors.Add(current with {Y = current.Y + 1});
        }
        
        // Up
        if (current.Y - 1 >= 0 && map[current.Y - 1, current.X] >= map[current.Y, current.X] - 1)
        {
            neighbors.Add(current with {Y = current.Y - 1});
        }

        return neighbors;
    }
    

    private static Point FindAndReplace(char[,] input, char find, char replace)
    {
        for (var y = 0; y < input.GetLength(0); y++)
        {
            for (var x = 0; x < input.GetLength(1); x++)
            {
                if (input[y, x] == find)
                {
                    input[y, x] = replace;
                    return new Point(x, y);
                }
            }
        }

        throw new Exception();
    }

    private static int[,] ConvertToIntArray(char[,] input)
    {
        var result = new int[input.GetLength(0), input.GetLength(1)];
        for (var y = 0; y < input.GetLength(0); y++)
        {
            for (var x = 0; x < input.GetLength(1); x++)
            {
                result[y, x] = input[y, x];
            }
        }

        return result;
    }
}
