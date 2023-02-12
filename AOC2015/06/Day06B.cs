using System.Drawing;

namespace AOC2015._06;

public class Day06B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        var grid = new int[1000, 1000];

        foreach (var i in input)
        {
            var command = new Command(i);
            for (var y = command.From.Y; y <= command.To.Y; y++)
            {
                for (var x = command.From.X; x <= command.To.X; x++)
                {
                    var newValue = grid[y, x] + command.Increase;
                    if (newValue < 0)
                    {
                        newValue = 0;
                    }

                    grid[y, x] = newValue;
                }
            }
        }

        var result = 0;
        
        for (var y = 0; y < 1000; y++)
        {
            for (var x = 0; x < 1000; x++)
            {
                result += grid[y, x];
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
            Increase = -1;
            if (c[0] == "toggle")
            {
                Increase = 2;
                fromIndex = 1;
            }
            else if (c[1] == "on")
            {
                Increase = 1;
            }

            var from = c[fromIndex].Split(",");
            From = new Point(from[1].ToInt(), from[0].ToInt());

            var to = c[fromIndex + 2].Split(",");
            To = new Point(to[1].ToInt(), to[0].ToInt());
        }

        public int Increase { get; set; }
        public Point From { get; set; }
        public Point To { get; set; }
    }
}