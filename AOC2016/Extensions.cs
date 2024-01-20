using System.Drawing;
using System.Text;

namespace AOC2016;

public static class Extensions
{
    public static string[] SplitEveryN(this string input, int n)
    {
        var result = new List<string>();
        for (var i = 0; i < input.Length; i += n)
        {
            if (i + n < input.Length)
            {
                result.Add(input[i..(i + n)]);
            }
            else
            {
                result.Add(input[i..]);
            }
        }

        return result.ToArray();
    }

    public static int ToInt(this string input) => int.Parse(input);
    public static long ToLong(this string input) => long.Parse(input);

    public static int LCM(this IEnumerable<int> numbers) => Helper.LCM(numbers);
    public static long LCM(this IEnumerable<long> numbers) => Helper.LCM(numbers);

    public static int CalculateManhattanDistance(this Point from, Point to)
    {
        var dx = Math.Abs(from.X - to.X);
        var dy = Math.Abs(from.Y - to.Y);
        return dx + dy;
    }
    
    public static int CalculateEuclideanDistance(this Point from, Point to)
    {
        var dx = Math.Abs(from.X - to.X);
        var dy = Math.Abs(from.Y - to.Y);
        return (int)Math.Sqrt(dx * dx + dy * dy);
    }
    
    public static int CalculateChebyshevDistance(this Point from, Point to)
    {
        var dx = Math.Abs(from.X - to.X);
        var dy = Math.Abs(from.Y - to.Y);
        return (dx + dy) - Math.Min(dx, dy);
    }
    
    public static string ToText(this char[,] matrix)
    {
        var yLength = matrix.GetLength(0);
        var xLength = matrix.GetLength(1);
        var sb = new StringBuilder(yLength * xLength + yLength * 2); // Size + NewLine chars
        
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                sb.Append(matrix[y,x]);
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
    
    public static char[,] ToCharArray(this List<Point> points)
    {
        var minY = points.Min(e => e.Y);
        var maxY = points.Max(e => e.Y);
        var minX = points.Min(e => e.X);
        var maxX = points.Max(e => e.X);

        var yOffset = 0 - minY;
        var xOffset = 0 - minX;
        var yDiff = (maxY - minY) + 1;
        var xDiff = (maxX - minX) + 1;
        var result = new char[yDiff, xDiff];
        
        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                if (points.Any(p => p.Y == y && p.X == x))
                {
                    result[y + yOffset, x + xOffset] = '#';
                }
                else
                {
                    result[y + yOffset, x + xOffset] = '.';
                }
            }
        }

        return result;
    }

    public static IEnumerable<(int Y, int X)> GetOrthogonalCoordinates(this Point p) => GetOrthogonalCoordinates(p.Y, p.X);
    
    public static IEnumerable<(int Y, int X)> GetOrthogonalCoordinates(int y, int x)
    {
        yield return (y - 1, x);
        yield return (y + 1, x);
        yield return (y, x - 1);
        yield return (y, x + 1);
    }
}
