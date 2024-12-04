using System.Drawing;

namespace AOC2024._04;

public class Day04A : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        var maxX = input.GetLength(0);
        var maxY = input.GetLength(1);

        var sum = 0;
        for (var y = 0; y < maxY; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                sum += GetXmasCount(input, maxY, maxX, y, x);
            }
        }

        return sum;
    }

    private int GetXmasCount(char[,] matrix, int maxY, int maxX, int y, int x)
    {
        if (matrix[y, x] != 'X') return 0;
        
        var sum = 0;
        //NW
        var pointM = new Point(x - 1, y - 1);
        var pointA = pointM with { X = x - 2, Y = y - 2};
        var pointS = pointA with { X = x - 3, Y = y - 3};
        sum += GetXmasCount(matrix, maxY, maxX, pointM, pointA, pointS);
        
        // N
        pointM = new Point(x, y - 1);
        pointA = pointM with {X = x, Y = y - 2};
        pointS = pointA with {X = x, Y = y - 3};
        sum += GetXmasCount(matrix, maxY, maxX, pointM, pointA, pointS);
        
        // NE
        pointM = new Point(x + 1, y - 1);
        pointA = pointM with {X = x + 2, Y = y - 2};
        pointS = pointA with {X = x + 3, Y = y - 3};
        sum += GetXmasCount(matrix, maxY, maxX, pointM, pointA, pointS);
        
        // E
        pointM = new Point(x + 1, y);
        pointA = pointM with {X = x + 2, Y = y};
        pointS = pointA with {X = x + 3, Y = y};
        sum += GetXmasCount(matrix, maxY, maxX, pointM, pointA, pointS);
        
        // SE
        pointM = new Point(x + 1, y + 1);
        pointA = pointM with {X = x + 2, Y = y + 2};
        pointS = pointA with {X = x + 3, Y = y + 3};
        sum += GetXmasCount(matrix, maxY, maxX, pointM, pointA, pointS);
        
        // S
        pointM = new Point(x, y + 1);
        pointA = pointM with {X = x, Y = y + 2};
        pointS = pointA with {X = x, Y = y + 3};
        sum += GetXmasCount(matrix, maxY, maxX, pointM, pointA, pointS);
        
        // SW
        pointM = new Point(x - 1, y + 1);
        pointA = pointM with {X = x - 2, Y = y + 2};
        pointS = pointA with {X = x - 3, Y = y + 3};
        sum += GetXmasCount(matrix, maxY, maxX, pointM, pointA, pointS);
        
        // W
        pointM = new Point(x - 1, y);
        pointA = pointM with {X = x - 2, Y = y};
        pointS = pointA with {X = x - 3, Y = y};
        sum += GetXmasCount(matrix, maxY, maxX, pointM, pointA, pointS);
        
        return sum;
    }

    private static int GetXmasCount(char[,] matrix, int maxY, int maxX, Point m, Point a, Point s)
    {
        if (!IsWithin(maxY, maxX, m) || matrix[m.Y, m.X] != 'M') return 0;
        if (!IsWithin(maxY, maxX, a) || matrix[a.Y, a.X] != 'A') return 0;
        if (!IsWithin(maxY, maxX, s) || matrix[s.Y, s.X] != 'S') return 0;
        
        return 1;
    }

    private static bool IsWithin(int maxY, int maxX, Point pointToCheck) =>
        pointToCheck.Y >= 0 && pointToCheck.Y < maxY && pointToCheck.X >= 0 && pointToCheck.X < maxX;
}
