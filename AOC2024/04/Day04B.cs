using System.Drawing;

namespace AOC2024._04;

public class Day04B : Day
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
        if (matrix[y, x] != 'A') return 0;
        
        var pointA = new Point(x - 1, y - 1);
        var pointB = new Point(x + 1, y - 1);
        var pointC = new Point(x - 1, y + 1);
        var pointD = new Point(x + 1, y + 1);
        
        return GetXmasCount(matrix, maxY, maxX, pointA, pointB, pointC, pointD);
    }

    private static int GetXmasCount(char[,] matrix, int maxY, int maxX, Point a, Point b, Point c, Point d)
    {
        if (!IsWithin(maxY, maxX, a) || !IsWithin(maxY, maxX, b) || !IsWithin(maxY, maxX, c) || !IsWithin(maxY, maxX, d)) return 0;
        
        /*
         * M M
         *  A
         * S S
         */
        if (matrix[a.Y, a.X] == 'M' && matrix[b.Y, b.X] == 'M' && matrix[c.Y, c.X] == 'S' && matrix[d.Y, d.X] == 'S') return 1;
        
        /*
         * M S
         *  A
         * M S
         */
        if (matrix[a.Y, a.X] == 'M' && matrix[b.Y, b.X] == 'S' && matrix[c.Y, c.X] == 'M' && matrix[d.Y, d.X] == 'S') return 1;
        
        /*
         * S S
         *  A
         * M M
         */
        if (matrix[a.Y, a.X] == 'S' && matrix[b.Y, b.X] == 'S' && matrix[c.Y, c.X] == 'M' && matrix[d.Y, d.X] == 'M') return 1;
        
        /*
         * S M
         *  A
         * S M
         */
        if (matrix[a.Y, a.X] == 'S' && matrix[b.Y, b.X] == 'M' && matrix[c.Y, c.X] == 'S' && matrix[d.Y, d.X] == 'M') return 1;
        
        return 0;
    }

    private static bool IsWithin(int maxY, int maxX, Point pointToCheck) =>
        pointToCheck.Y >= 0 && pointToCheck.Y < maxY && pointToCheck.X >= 0 && pointToCheck.X < maxX;
}
