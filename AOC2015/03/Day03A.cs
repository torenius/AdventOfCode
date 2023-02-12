using System.Drawing;

namespace AOC2015._03;

public class Day03A : Day
{
    protected override string Run()
    {
        var input = GetInputAsString();

        var houses = new HashSet<Point>();
        var location = new Point(0, 0);
        houses.Add(location);

        foreach (var l in input)
        {
            switch (l)
            {
                case '^':
                    location.Y--;
                    break;
                case 'v':
                    location.Y++;
                    break;
                case '>':
                    location.X++;
                    break;
                case '<':
                    location.X--;
                    break;
            }

            if (!houses.Contains(location))
            {
                houses.Add(location);
            }
        }
        
        return "" + houses.Count;
    }
}