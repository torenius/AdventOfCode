using System.Drawing;

namespace AOC2023._11;

public class Day11B : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        var points = new List<Point>();
        for (var y = 0; y < input.GetLength(0); y++)
        {
            for (var x = 0; x < input.GetLength(1); x++)
            {
                if (input[y, x] == '#')
                {
                    points.Add(new Point
                    {
                        X = x,
                        Y = y
                    });
                }
            }
        }
        
        // Expand Y
        for (var y = 0; y < input.GetLength(0); y++)
        {
            var allIsEmpty = true;
            for (var x = 0; x < input.GetLength(1); x++)
            {
                if (input[y, x] == '#')
                {
                    allIsEmpty = false;
                    break;
                }
            }

            if (allIsEmpty)
            {
                for(var i = 0; i < points.Count; i++)
                {
                    var p = points[i];
                    if (p.Y < y)
                    {
                        points[i] = p with {Y = p.Y - 1000000 + 1};
                    }
                }
            }
        }
        
        // Expand X
        for (var x = 0; x < input.GetLength(1); x++)
        {
            var allIsEmpty = true;
            for (var y = 0; y < input.GetLength(1); y++)
            {
                if (input[y, x] == '#')
                {
                    allIsEmpty = false;
                    break;
                }
            }

            if (allIsEmpty)
            {
                for(var i = 0; i < points.Count; i++)
                {
                    var p = points[i];
                    if (p.X < x)
                    {
                        points[i] = p with {X = p.X - 1000000 + 1};
                    }
                }
            }
        }
        
        long sum = 0;
        for (var i = 0; i < points.Count - 1; i++)
        {
            for (var k = i + 1; k < points.Count; k++)
            {
                var distance = points[i].CalculateManhattanDistance(points[k]);
                //Console.WriteLine($"{i} {k} {points[i]} -> {points[k]} = {distance}");
                sum += distance;
            }
        }

        return sum;
    }
}