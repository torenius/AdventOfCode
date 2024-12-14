using System.Text.RegularExpressions;

namespace AOC2024._14;

public partial class Day14A : Day
{
    protected override object Run()
    {
        var robots = GetInputAsStringArray().Select(r => new Robot(r)).ToList();
        // const int maxX = 11;
        // const int maxY = 7;
        const int maxX = 101;
        const int maxY = 103;

        for (var i = 1; i <= 100; i++)
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
        }
        
        const int midX = maxX / 2;
        const int midY = maxY / 2;
        
        var q1 = robots.Count(r => r.X < midX && r.Y < midY);
        var q2 = robots.Count(r => r.X > midX && r.Y < midY);
        var q3 = robots.Count(r => r.X < midX && r.Y > midY);
        var q4 = robots.Count(r => r.X > midX && r.Y > midY);
        
        return q1 * q2 * q3 * q4;
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