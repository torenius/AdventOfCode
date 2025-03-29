using System.Drawing;
using AOC.Common;

namespace AOC2016._01;

public class Day01A : Day
{
    protected override object Run()
    {
        var input = GetInputAsString();
        var current = new Point(0, 0);
        var direction = Direction.North;
        foreach (var instruction in input.Split(", "))
        {
            var dir = instruction[0];
            direction = (direction, dir) switch
            {
                (Direction.North, 'R') => Direction.East,
                (Direction.North, 'L') => Direction.West,
                (Direction.South, 'R') => Direction.West,
                (Direction.South, 'L') => Direction.East,
                (Direction.East, 'R') => Direction.South,
                (Direction.East, 'L') => Direction.North,
                (Direction.West, 'R') => Direction.North,
                (Direction.West, 'L') => Direction.South,
                _ => direction
            };

            current = direction switch
            {
                Direction.North => current with { Y = current.Y - instruction[1..].ToInt() },
                Direction.South => current with { Y = current.Y + instruction[1..].ToInt() },
                Direction.East => current with { X = current.X + instruction[1..].ToInt() },
                Direction.West => current with { X = current.X - instruction[1..].ToInt() },
                _ => current
            };
        }

        return current.CalculateManhattanDistance(new Point(0, 0));
    }

    private enum Direction
    {
        North,
        South,
        East,
        West
    }
}