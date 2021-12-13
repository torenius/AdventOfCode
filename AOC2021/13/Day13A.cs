using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AOC2021._13
{
    public class Day13A : Day
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

            var command = s[1].Split("\n");

            List<Point> result = null;
            if (command[0].Contains("fold along y="))
            {
                var y = command[0].Replace("fold along y=", "");
                result = FoldY(int.Parse(y), pointList);
            }
            
            if (command[0].Contains("fold along x="))
            {
                var x = command[0].Replace("fold along x=", "");
                result = FoldX(int.Parse(x), pointList);
            }

            // result = FoldY(7, pointList);
            // result = FoldX(5, result);

            Console.WriteLine($"Count = {result.Count}");
        }

        private List<Point> FoldY(int y, List<Point> pointList)
        {
            var newPoints = pointList.Where(w => w.Y < y).ToList();
            foreach (var p in pointList.Where(w => w.Y > y))
            {
                // 10 - 7 = 3
                // 7 - 3 = 4
                // 7 - (10 - 7)
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
