﻿using System.Drawing;

namespace AOC2022;

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

    public static int ToInt(this string input)
    {
        return int.Parse(input);
    }
    
    public static long ToLong(this string input)
    {
        return long.Parse(input);
    }

    public static int LCM(this IEnumerable<int> numbers)
    {
        return Helper.LCM(numbers);
    }

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
}
