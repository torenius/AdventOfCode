using System.Drawing;

namespace AOC2015._03;

public class Day03B : Day
{
    protected override string Run()
    {
        var input = GetInputAsString();

        var houses = new HashSet<Point>();
        var santa = new Point(0, 0);
        houses.Add(santa);
        
        var robotSanta = new Point(0, 0);

        for (var i = 0; i < input.Length; i++)
        {
            if (i % 2 == 0)
            {
                Move(ref santa, input[i]);
                if (!houses.Contains(santa))
                {
                    houses.Add(santa);
                }
            }
            else
            {
                Move(ref robotSanta, input[i]);
                if (!houses.Contains(robotSanta))
                {
                    houses.Add(robotSanta);
                }
            }
        }

        return "" + houses.Count;
    }

    private static void Move(ref Point location, char direction)
    {
        switch (direction)
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
    }
}