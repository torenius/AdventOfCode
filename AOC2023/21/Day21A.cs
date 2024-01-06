using System.Drawing;

namespace AOC2023._21;

public class Day21A : Day
{
    private const int MaxSteps = 64;
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        
        // We need to keep track if we visited the spot on an even amount of steps or not
        var start = new GardenPlot
        {
            Location = FindStart(input)
        };
        var plots = Helper.BreadthFirst(start, plot => GetPaths(input, plot)).ToList();
        
        return plots.Count(p => p.FirstVisitedAfterSteps <= MaxSteps && p.IsEven);
    }

    private static List<GardenPlot> GetPaths(char[,] input, GardenPlot from)
    {
        var result = new List<GardenPlot>();
        if (from.FirstVisitedAfterSteps >= MaxSteps)
        {
            return result;
        }
        
        var lengthY = input.GetLength(0);
        var lengthX = input.GetLength(1);
        foreach (var point in from.Location.GetOrthogonalCoordinates())
        {
            if (point.Y >= 0 && point.Y < lengthY &&
                point.X >= 0 && point.X < lengthX &&
                input[point.Y, point.X] == '.')
            {
                result.Add(new GardenPlot
                {
                    Location = new Point(point.X, point.Y),
                    FirstVisitedAfterSteps = from.FirstVisitedAfterSteps + 1
                });
            }
        }

        return result;
    }

    private class GardenPlot : IEquatable<GardenPlot>
    {
        public Point Location { get; set; }
        public bool IsEven => FirstVisitedAfterSteps % 2 == 0;
        public int FirstVisitedAfterSteps { get; set; }

        public bool Equals(GardenPlot? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Location.Equals(other.Location) && IsEven == other.IsEven;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((GardenPlot) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Location, IsEven);
        }
    }

    private static Point FindStart(char[,] input)
    {
        for (var y = 0; y < input.GetLength(0); y++)
        {
            for (var x = 0; x < input.GetLength(1); x++)
            {
                if (input[y, x] == 'S')
                {
                    return new Point(x, y);
                }
            }
        }

        return new Point(-1, -1);
    }
}