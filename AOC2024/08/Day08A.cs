using System.Drawing;

namespace AOC2024._08;

public class Day08A : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        var antennas = new Dictionary<char, List<Point>>();

        var maxY = input.GetLength(0);
        var maxX = input.GetLength(1);

        for (var y = 0; y < maxY; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                if (input[y, x] != '.' && input[y, x] != '#')
                {
                    if (!antennas.TryGetValue(input[y, x], out var value))
                    {
                        value = [];
                        antennas.Add(input[y, x], value);    
                    }

                    value.Add(new Point(x, y));
                }
            }
        }

        var antinodes = new List<Point>();
        for (var y = 0; y < maxY; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                var point = new Point(x, y);
                foreach (var kvp in antennas)
                {
                    if (IsAntinode(kvp.Value, point))
                    {
                        antinodes.Add(point);
                        break;
                    }
                }
            }
        }
        Helper.Print(input, antinodes);
        return antinodes.Count;
    }

    private static bool IsAntinode(List<Point> points, Point p)
    {
        for (var i = 0; i < points.Count; i++)
        {
            var firstX = points[i].X - p.X;
            var firstY = points[i].Y - p.Y;
            for (var k = i + 1; k < points.Count; k++)
            {
                var secondX = points[k].X - p.X;
                var secondY = points[k].Y - p.Y;
                if ((firstX * 2 == secondX && firstY * 2 == secondY) ||
                    (firstX == secondX * 2 && firstY == secondY * 2))
                {
                    return true;
                }
            }
        }
        
        return false;
    }
}