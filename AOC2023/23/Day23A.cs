using System.Drawing;

namespace AOC2023._23;

public class Day23A : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        var start = new Point(1, 0);
        var end = new Point(input.GetLength(1) - 2, input.GetLength(0) - 1);

        var paths = GetPaths(input, start, end, new List<Point>());
        // foreach (var path in paths)
        // {
        //     Helper.Print(input, path);
        // }

        return paths.Max(x => x.Count) - 1; // -1 since it contains start point
    }

    private static List<List<Point>> GetPaths(char[,] map, Point start, Point end, List<Point> path)
    {
        var result = new List<List<Point>>();
        var neighbors = new List<Point>{ start };
        do
        {
            var current = neighbors[0];
            path.Add(current);
            
            if (current == end)
            {
                result.Add(path);
                return result;
            }
            
            neighbors = GetNeighbors(map, current).Where(x => !path.Contains(x)).ToList();
        } while (neighbors.Count == 1);

        if (neighbors.Count == 0)
        {
            return result;
        }

        foreach (var neighbor in neighbors)
        {
            result.AddRange(GetPaths(map, neighbor, end, path.ToList()));
        }

        return result;
    }
    
    private static List<Point> GetNeighbors(char[,] map, Point current)
    {
        var result = new List<Point>();
        switch (map[current.Y, current.X])
        {
            case '^':
                result.Add(current with {Y = current.Y - 1});
                return result;
            case 'v':
                result.Add(current with {Y = current.Y + 1});
                return result;
            case '<':
                result.Add(current with {X = current.X - 1});
                return result;
            case '>':
                result.Add(current with {X = current.X + 1});
                return result;
        }
        
        if (current.Y - 1 >= 0)
        {
            var up = map[current.Y - 1, current.X];
            if (up is '.' or '^')
            {
                result.Add(current with {Y = current.Y - 1});
            }
        }

        var down = map[current.Y + 1, current.X];
        if (down is '.' or 'v')
        {
            result.Add(current with {Y = current.Y + 1});
        }

        var left = map[current.Y, current.X - 1];
        if (left is '.' or '<')
        {
            result.Add(current with {X = current.X - 1});
        }
        
        var right = map[current.Y, current.X + 1];
        if (right is '.' or '>')
        {
            result.Add(current with {X = current.X + 1});
        }

        return result;
    }
}