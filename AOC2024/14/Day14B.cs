using System.Text;
using System.Text.RegularExpressions;
using AOC.Common;

namespace AOC2024._14;

public partial class Day14B : Day
{
    protected override object Run()
    {
        var robots = GetInputAsStringArray().Select(r => new Robot(r)).ToList();
        const int maxX = 101;
        const int maxY = 103;

        for (var i = 1; i <= 100000; i++)
        {
            foreach (var robot in robots)
            {
                robot.X += robot.XSpeed;
                if (robot.X >= maxX)
                {
                    robot.X -= maxX;
                }
                else if (robot.X < 0)
                {
                    robot.X += maxX;
                }
                
                robot.Y += robot.YSpeed;
                if (robot.Y >= maxY)
                {
                    robot.Y -= maxY;
                }
                else if (robot.Y < 0)
                {
                    robot.Y += maxY;
                }
            }

            if (robots.GroupBy(r => r.Y).Any(g => g.Count() >= 20))
            {
                Thread.Sleep(1000);
                Console.WriteLine(i);
                Print(robots);
            }
        }
        
        return 8280;
    }
    
    private static void Print(List<Robot> points)
    {
        var minY = points.Min(e => e.Y);
        var maxY = points.Max(e => e.Y);
        var minX = points.Min(e => e.X);
        var maxX = points.Max(e => e.X);

        var sb = new StringBuilder();
        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                sb.Append(points.Any(e => e.X == x && e.Y == y) ? '#' : '.');
            }

            sb.AppendLine();
        }
        
        Console.WriteLine(sb.ToString());
    }

    private partial class Robot
    {
        public Robot(string input)
        {
            var match = ParseInput().Match(input);
            X = match.Groups["X"].Value.ToInt();
            Y = match.Groups["Y"].Value.ToInt();
            XSpeed = match.Groups["XSpeed"].Value.ToInt();
            YSpeed = match.Groups["YSpeed"].Value.ToInt();
        }
        
        public int X { get; set; }
        public int Y { get; set; }
        public int XSpeed { get; set; }
        public int YSpeed { get; set; }
        
        [GeneratedRegex(@"^p=(?<X>[-0-9]+),(?<Y>[-0-9]+) v=(?<XSpeed>[-0-9]+),(?<YSpeed>[-0-9]+)$")]
        private partial Regex ParseInput();
    }
}