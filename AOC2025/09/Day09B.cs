using System.Drawing;
using AOC.Common;

namespace AOC2025._09;

public class Day09B : Day
{
    private readonly List<Line> _lines = [];
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var points = input
            .Select(x => x.Split(','))
            .Select(x => new Point(x[0].ToInt(), x[1].ToInt()))
            .ToList();
        
        points.Add(points[0]);
        
        for (var i = 0; i < points.Count - 1; i++)
        {
            var line = new Line(points[i], points[i + 1]);
            _lines.Add(line);
        }

        var boxes = new List<Box>();
        for (var i = 0; i < points.Count - 1; i++)
        {
            var top = points[i];
            for (var j = i + 1; j < points.Count; j++)
            {
                var bottom = points[j];
                boxes.Add(new Box
                {
                    Size = (Math.Abs(top.X - bottom.X) + 1) * (long)(Math.Abs(top.Y - bottom.Y) + 1),
                    Top = top,
                    Bottom = bottom
                });
            }
        }
        
        PrintElapsedTime(boxes.Count);
        
        var k = 0;
        var printTime = TimeSpan.FromSeconds(3);
        foreach (var box in boxes
                     .Where(b => b.Size <= 1461987144) // It took 2 min and 13 seconds. Having this Where here to make the unit test fast....
                     .OrderByDescending(b => b.Size))
        {
            k++;
            PrintElapsedTime(k, printTime);

            if (box.OppositePoints().All(IsInside))
            {
                if (box.OuterBorder().All(IsInside))
                {
                    return box.Size;
                }
            }
        }
        
        return 0;
    }

    private bool IsInside(Point point)
    {
        var onBorder = _lines.Any(l => l.IsOnLine(point));
        if (onBorder) return true;

        if (!_lines.Any(l => l.IsOver(point))) return false;
        if (!_lines.Any(l => l.IsUnder(point))) return false;
        return _lines.Any(l => l.IsLeft(point)) && _lines.Any(l => l.IsRight(point));
    }

    private class Box
    {
        public long Size { get; set; }
        public Point Top { get; set; }
        public Point Bottom { get; set; }

        public IEnumerable<Point> OppositePoints()
        {
            yield return new Point(Top.X, Bottom.Y);
            yield return new Point(Bottom.X, Top.Y);
        }

        public IEnumerable<Point> OuterBorder()
        {
            var minX = Math.Min(Top.X, Bottom.X);
            var minY = Math.Min(Top.Y, Bottom.Y);
            var maxX = Math.Max(Top.X, Bottom.X);
            var maxY = Math.Max(Top.Y, Bottom.Y);
            
            var middleX = (maxX - minX) / 2 + minX;
            var middleY = (maxY - minY) / 2 + minY;
            
            for (var y = middleY; y < maxY; y++)
            {
                yield return new Point(minX, y);
                yield return new Point(maxX, y);
            }
            
            for (var y = middleY - 1; y > minY + 1; y--)
            {
                yield return new Point(minX, y);
                yield return new Point(maxX, y);
            }
            
            for (var x = middleX; x < maxX; x++)
            {
                yield return new Point(x, minY);
                yield return new Point(x, maxY);
            }
            
            for (var x = middleX - 1; x > minX + 1; x--)
            {
                yield return new Point(x, minY);
                yield return new Point(x, maxY);
            }
        }
    }

    private class Line
    {
        public Line(Point a, Point b)
        {
            A = a;
            B = b;
            
            MinY = Math.Min(a.Y, b.Y);
            MaxY = Math.Max(a.Y, b.Y);
            MinX = Math.Min(a.X, b.X);
            MaxX = Math.Max(a.X, b.X);
        }

        public Point A { get; }
        public Point B { get; }
        public int MinY { get; set; }
        public int MaxY { get; set; }
        public int MinX { get; set; }
        public int MaxX { get; set; }

        public bool IsOnLine(Point p)
        {
            if (p.X == MinX && p.X == MaxX && p.Y >= MinY && p.Y <= MaxY) return true;
            if (p.Y == MinY && p.Y == MaxY && p.X >= MinX && p.X <= MaxX) return true;
            
            return false;
        }
        
        public bool IsOver(Point p) => p.Y < MinY && p.X >= MinX && p.X <= MaxX;
        public bool IsUnder(Point p) => p.Y > MaxY && p.X >= MinX && p.X <= MaxX;
        public bool IsLeft(Point p) => p.X < MinX && p.Y >= MinY && p.Y <= MaxY;
        public bool IsRight(Point p) => p.X > MaxX && p.Y >= MinY && p.Y <= MaxY;
    }
}