using System.Drawing;

namespace AOC2023._18;

public class Day18A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var instructions = input.Select(x => new DigInstruction(x)).ToList();
        var holes = new List<Point>();

        var current = new Point(0, 0);
        holes.Add(current);
        foreach (var instruction in instructions)
        {
            var points = instruction.Direction switch
            {
                "U" => GetPointsOnLine(current, 0, -1, instruction.Steps),
                "D" => GetPointsOnLine(current, 0, 1, instruction.Steps),
                "L" => GetPointsOnLine(current, -1, 0, instruction.Steps),
                "R" => GetPointsOnLine(current, 1, 0, instruction.Steps),
            };
            current = points.Last();
            holes.AddRange(points);
        }
        
        //Helper.Print(holes);

        var matrix = holes.ToCharArray();
        var inside = FindPointInside(matrix);
        Expand(matrix, inside.Y, inside.X);
        
        //Console.WriteLine(matrix.ToText());
        
        var sum = 0;
        for (var y = 0; y < matrix.GetLength(0); y++)
        {
            for (var x = 0; x < matrix.GetLength(1); x++)
            {
                if (matrix[y, x] == '#')
                {
                    sum++;
                }
            }
        }
        
        return sum;
    }

    private static List<Point> GetPointsOnLine(Point point, int xDiff, int yDiff, int steps)
    {
        var result = new List<Point>();
        for (var i = 1; i <= steps; i++)
        {
            result.Add(point with { X = point.X + xDiff * i, Y = point.Y + yDiff * i});
        }

        return result;
    }

    private static void Expand(char[,] input, int y, int x)
    {
        var start = new Point(x, y);
        foreach (var point in Helper.BreadthFirst(start, (point) => GetChildren(input, point)))
        {
            input[point.Y, point.X] = '#';
        }
        
    }

    private static IEnumerable<Point> GetChildren(char[,] input, Point point)
    {
        if (point.Y - 1 >= 0 && input[point.Y - 1, point.X] == '.')
        {
            yield return point with {Y = point.Y - 1};
        }
        
        if (point.Y + 1 < input.GetLength(0) && input[point.Y + 1, point.X] == '.')
        {
            yield return point with {Y = point.Y + 1};
        }
        
        if (point.X - 1 >= 0 && input[point.Y, point.X - 1] == '.')
        {
            yield return point with {X = point.X - 1};
        }
        
        if (point.X + 1 < input.GetLength(1) && input[point.Y, point.X + 1] == '.')
        {
            yield return point with {X = point.X + 1};
        }
    }
    

    private static (int Y, int X) FindPointInside(char[,] input)
    {
        for (var y = 0; y < input.GetLength(0); y++)
        {
            for (var x = 0; x < input.GetLength(1); x++)
            {
                if (input[y, x] == '.')
                {
                    var count = 0;
                    for (var z = x - 1; z >= 0; z--)
                    {
                        if (input[y, z] == '#')
                        {
                            count++;
                        }
                    }

                    if (count % 2 == 1)
                    {
                        count = 0;
                        for (var z = y - 1; z >= 0; z--)
                        {
                            if (input[z, x] == '#')
                            {
                                count++;
                            }
                        }

                        if (count % 2 == 1)
                        {
                            return (y, x);
                        }
                    }
                }
            }
        }

        return (-1, -1);
    }
    
    private class DigInstruction
    {
        public DigInstruction(string input)
        {
            var temp = input.Split(" ");
            Direction = temp[0];
            Steps = temp[1].ToInt();
            HexColor = temp[2];
        }

        public string Direction { get; set; }
        public int Steps { get; set; }
        public string HexColor { get; set; }
    }
}