using System.Drawing;

namespace AOC2022._12;

public class Day12A : Day
{
    public override string Run()
    {
        var input = GetInputAsCharMatrix();
        var start = FindAndReplace(input, 'S', 'a');
        var end = FindAndReplace(input, 'E', 'z');

        var map = ConvertToIntArray(input);

        var path = ShortestPath(map, start, end);

        return "" + (path.Count - 1);
    }

    private static List<Point> ShortestPath(int[,] map, Point start, Point end)
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

        var path = new List<Point>();
        var cur = end;
        while (!cur.Equals(start))
        {
            path.Add(cur);
            cur = previous[cur];
        }
        
        path.Add(start);
        path.Reverse();

        return path;
    }

    private static List<Point> GetNeighbors(int[,] map, Point current)
    {
        var neighbors = new List<Point>();

        // Right
        if (current.X + 1 < map.GetLength(1) && map[current.Y, current.X + 1] <= map[current.Y, current.X] + 1)
        {
            neighbors.Add(current with {X = current.X + 1});
        }
        
        // Left
        if (current.X - 1 >= 0 && map[current.Y, current.X - 1] <= map[current.Y, current.X] + 1)
        {
            neighbors.Add(current with {X = current.X - 1});
        }
        
        // Down
        if (current.Y + 1 < map.GetLength(0) && map[current.Y + 1, current.X] <= map[current.Y, current.X] + 1)
        {
            neighbors.Add(current with {Y = current.Y + 1});
        }
        
        // Up
        if (current.Y - 1 >= 0 && map[current.Y - 1, current.X] <= map[current.Y, current.X] + 1)
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
