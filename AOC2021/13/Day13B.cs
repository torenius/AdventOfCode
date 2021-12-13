using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AOC2021._13
{
    public class Day13B : Day
    {
        public override void Run()
        {
            var input = GetInputAsString();
            input = input.Replace("\r\n", "\n");
            var s = input.Split("\n\n");

            var pointList = new List<Point>();
            foreach (var cord in s[0].Split("\n"))
            {
                var c = cord.Split(",");
                var p = new Point(int.Parse(c[0]), int.Parse(c[1]));
                pointList.Add(p);
            }

            var command = s[1].Split("\n", StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var c in command)
            {
                if (c.Contains("fold along y="))
                {
                    var y = c.Replace("fold along y=", "");
                    pointList = FoldY(int.Parse(y), pointList);
                }
            
                if (c.Contains("fold along x="))
                {
                    var x = c.Replace("fold along x=", "");
                    pointList = FoldX(int.Parse(x), pointList);
                }
            }

            Console.WriteLine($"Count = {pointList.Count}");

            var maxX = pointList.Max(m => m.X);
            var maxY = pointList.Max(m => m.Y);

            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    if (pointList.Any(a => a.X == x && a.Y == y))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        private List<Point> FoldY(int y, List<Point> pointList)
        {
            var newPoints = pointList.Where(w => w.Y < y).ToList();
            foreach (var p in pointList.Where(w => w.Y > y))
            {
                var n = new Point(p.X, y - (p.Y - y));
                newPoints.Add(n);
            }
            
            return newPoints.Distinct().ToList();
        }
        
        private List<Point> FoldX(int x, List<Point> pointList)
        {
            var newPoints = pointList.Where(w => w.X < x).ToList();
            foreach (var p in pointList.Where(w => w.X > x))
            {
                var n = new Point(x - (p.X - x), p.Y);
                newPoints.Add(n);
            }
            
            return newPoints.Distinct().ToList();
        }
    }
}
