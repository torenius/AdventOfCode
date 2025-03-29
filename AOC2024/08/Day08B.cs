using System.Drawing;
using AOC.Common;

namespace AOC2024._08;

public class Day08B : Day
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
        foreach (var kvp in antennas)
        {
            antinodes.AddRange(GetAntinodes(kvp.Value, maxX, maxY));    
        }
        
        Helper.Print(input, antinodes);
        return antinodes.Distinct().Count(a => a.X >= 0 && a.Y >= 0 && a.X < maxX && a.Y < maxY);
    }

    private static List<Point> GetAntinodes(List<Point> points, int maxX, int maxY)
    {
        var antinodes = new List<Point>();
        if (points.Count >= 2)
        {
            antinodes.AddRange(points);
        }
        for (var i = 0; i < points.Count; i++)
        {
            for (var k = i + 1; k < points.Count; k++)
            {
                var pointI = points[i];
                var pointK = points[k];
                var deltaX = Math.Abs(pointI.X - pointK.X);
                var deltaY = Math.Abs(pointI.Y - pointK.Y);
                
                if (pointI.X > pointK.X && pointI.Y < pointK.Y)
                {
                    antinodes.AddRange(GetAntinodes(pointI.X, pointI.Y, deltaX, -deltaY, maxX, maxY));
                    antinodes.AddRange(GetAntinodes(pointK.X, pointK.Y, -deltaX, deltaY, maxX, maxY));
                }
                else if (pointI.X < pointK.X && pointI.Y > pointK.Y)
                {
                    antinodes.AddRange(GetAntinodes(pointI.X, pointI.Y, -deltaX, deltaY, maxX, maxY));
                    antinodes.AddRange(GetAntinodes(pointK.X, pointK.Y, deltaX, -deltaY, maxX, maxY));
                }
                else if (pointI.X < pointK.X && pointI.Y < pointK.Y)
                {
                    antinodes.AddRange(GetAntinodes(pointI.X, pointI.Y, -deltaX, -deltaY, maxX, maxY));
                    antinodes.AddRange(GetAntinodes(pointK.X, pointK.Y, deltaX, deltaY, maxX, maxY));
                }
                else if (pointI.X > pointK.X && pointI.Y > pointK.Y)
                {
                    antinodes.AddRange(GetAntinodes(pointI.X, pointI.Y, deltaX, deltaY, maxX, maxY));
                    antinodes.AddRange(GetAntinodes(pointK.X, pointK.Y, -deltaX, -deltaY, maxX, maxY));
                }
            }
        }

        return antinodes;
    }

    private static IEnumerable<Point> GetAntinodes(int x, int y, int deltaX, int deltaY, int maxX, int maxY)
    {
        var multiplier = 1;
        while (x + deltaX * multiplier >= 0 && x + deltaX * multiplier < maxX && y + deltaY * multiplier >= 0 &&
               y + deltaY * multiplier < maxY)
        {
            var p = new Point(x + deltaX * multiplier, y + deltaY * multiplier);
            yield return p;
            multiplier++;
        }
    }
}