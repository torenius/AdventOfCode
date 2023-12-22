using System.Drawing;

namespace AOC2023._18;

public class Day18B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var instructions = input.Select(x => new DigInstruction(x)).ToList();
        var polygon = new List<Point>();
        
        var current = new Point(0, 0);
        foreach (var instruction in instructions)
        {
            current = instruction.Direction switch
            {
                "U" => current with {Y = current.Y - instruction.Steps},
                "D" => current with {Y = current.Y + instruction.Steps},
                "L" => current with {X = current.X - instruction.Steps},
                "R" => current with {X = current.X + instruction.Steps},
            };
            polygon.Add(current);
        }

        return Area(polygon);
    }

    private static long Area(IReadOnlyList<Point> polygon)
    {
        var area = ShoelaceArea(polygon);
        var boundaryPoints = PerimeterLength(polygon);
        var interiorPoints = InteriorPoints(area, boundaryPoints);
        return interiorPoints + boundaryPoints;
    }

    private static long ShoelaceArea(IReadOnlyList<Point> polygon)
    {
        long area = 0;
        var length = polygon.Count;
        for (var i = 0; i < length; i++)
        {
            var next = (i + 1) % length;
            var previous = (i - 1 + length) % length;
            area += (long)polygon[i].X * (polygon[next].Y - polygon[previous].Y);
        }

        return Math.Abs(area) / 2;
    }

    // Boundary points
    private static long PerimeterLength(IReadOnlyList<Point> polygon)
    {
        long perimeter = 0;
        var length = polygon.Count;
        for (var i = 0; i < length; i++)
        {
            var next = (i + 1) % length;
            perimeter += (long)Math.Abs(polygon[next].X - polygon[i].X) + Math.Abs(polygon[next].Y - polygon[i].Y);
        }
        
        return perimeter;
    }

    private static long InteriorPoints(long area, long boundaryPoints)
    {
        return area - (boundaryPoints / 2) + 1;
    }

    private class DigInstruction
    {
        public DigInstruction(string input)
        {
            var temp = input.Split(" ");
            Direction = temp[0];
            Steps = temp[1].ToInt();
            
            // Part 2
            //return;
            var hexValue = temp[2].Trim('(', ')');
            var hexColor = hexValue[1..^1];
            Direction = hexValue[^1] switch
            {
                '0' => "R",
                '1' => "D",
                '2' => "L",
                '3' => "U"
            };
            Steps = Convert.ToInt32("0x" + hexColor, 16);
        }

        public string Direction { get; set; }
        public int Steps { get; set; }
    }
}