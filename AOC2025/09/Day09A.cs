using System.Drawing;
using AOC.Common;

namespace AOC2025._09;

public class Day09A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var points = input
            .Select(x => x.Split(','))
            .Select(x => new Point(x[0].ToInt(), x[1].ToInt()))
            .ToList();

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
        
        return boxes.Max(b => b.Size);
    }

    private class Box
    {
        public long Size { get; set; }
        public Point Top { get; set; }
        public Point Bottom { get; set; }
    }
}