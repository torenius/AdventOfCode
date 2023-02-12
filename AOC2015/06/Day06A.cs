using System.Drawing;

namespace AOC2015._06;

public class Day06A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        var grid = new bool[1000, 1000];

        foreach (var i in input)
        {
            var command = new Command(i);
            for (var y = command.From.Y; y <= command.To.Y; y++)
            {
                for (var x = command.From.X; x <= command.To.X; x++)
                {
                    if (command.IsToggle)
                    {
                        grid[y, x] = !grid[y, x];
                    }
                    else
                    {
                        grid[y, x] = command.TurnOn;
                    }
                }
            }
        }

        var result = 0;
        
        for (var y = 0; y < 1000; y++)
        {
            for (var x = 0; x < 1000; x++)
            {
                if (grid[y, x])
                {
                    result++;
                }
            }
        }
        
        return "" + result;
    }

    private class Command
    {
        public Command(string input)
        {
            var c = input.Split(" ");

            var fromIndex = 2;
            if (c[0] == "toggle")
            {
                IsToggle = true;
                fromIndex = 1;
            }
            else if (c[1] == "on")
            {
                TurnOn = true;
            }

            var from = c[fromIndex].Split(",");
            From = new Point(from[1].ToInt(), from[0].ToInt());

            var to = c[fromIndex + 2].Split(",");
            To = new Point(to[1].ToInt(), to[0].ToInt());
        }

        public bool IsToggle { get; set; }
        public bool TurnOn { get; set; }
        public Point From { get; set; }
        public Point To { get; set; }
    }
}