using System.Drawing;

namespace AOC2022._14;

public class Day14A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var cave = new Dictionary<Point, char>();
        cave.Add(new Point(500, 0), '+');

        foreach (var lines in input)
        {
            var points = lines.Split(" -> ").Select(GetPoint).ToArray();
            for (var i = 0; i < points.Length - 1; i++)
            {
                var a = points[i];
                var b = points[i + 1];

                var fromX = Math.Min(a.X, b.X);
                var toX = Math.Max(a.X, b.X);

                var fromY = Math.Min(a.Y, b.Y);
                var toY = Math.Max(a.Y, b.Y);

                for (var x = fromX; x <= toX; x++)
                {
                    for (var y = fromY; y <= toY; y++)
                    {
                        cave.TryAdd(new Point(x, y), '#');
                    }
                }
            }
        }
        
        var cavePoints = cave.Keys;
        var minX = cavePoints.Min(p => p.X) - 2;
        var maxX = cavePoints.Max(p => p.X) + 2;
        var minY = cavePoints.Min(p => p.Y) - 2;
        var maxY = cavePoints.Max(p => p.Y) + 2;

        var current = new Point(500, 1);
        var sandCount = 0;

        while (current.X >= minX && current.Y <= maxY)
        {
            // Check One Down
            if (cave.TryGetValue(new Point(current.X, current.Y + 1), out _))
            {
                // Something existed, try left
                if (cave.TryGetValue(new Point(current.X - 1, current.Y + 1), out _))
                {
                    // Something existed, try right
                    if (cave.TryGetValue(new Point(current.X + 1, current.Y + 1), out _))
                    {
                        // Sand rest
                        sandCount++;
                        cave.Add(current, 'o');
                        //Print(cave, minX, maxX, minY, maxY);
                        current = new Point(500, 1);
                    }
                    else
                    {
                        current.X++;
                        current.Y++;
                    }
                }
                else
                {
                    current.X--;
                    current.Y++;
                }
            }
            else
            {
                current.Y++;
            }
        }
        
        return "" + sandCount;
    }

    private static Point GetPoint(string input)
    {
        var p = input.Split(",");

        return new Point(p[0].ToInt(), p[1].ToInt());
    }

    private static void Print(Dictionary<Point, char> cave, int fromX, int toX, int fromY, int toY)
    {
        for (var y = fromY; y <= toY; y++)
        {
            for (var x = fromX; x <= toX; x++)
            {
                if (!cave.TryGetValue(new Point(x, y), out var c))
                {
                    c = '.';
                }
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }
}