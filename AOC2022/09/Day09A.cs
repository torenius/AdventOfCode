using System.Drawing;

namespace AOC2022._09;

public class Day09A : Day
{
    private Point Head = new Point(0, 0);
    private Point Tail = new Point(0, 0);
    private HashSet<Point> TailLocations = new HashSet<Point>();

    public override string Run()
    {
        var input = GetInputAsStringArray();

        TailLocations.Add(Tail);

        foreach (var motion in input)
        {
            var command = motion.Split(" ");
            Move(command[0], command[1].ToInt());
        }
        
        return "" + TailLocations.Count;
    }

    private void Move(string direction, int steps)
    {
        for (var i = 0; i < steps; i++)
        {
            switch (direction)
            {
                case "R": Head.X++;
                    break;
                case "L": Head.X--;
                    break;
                case "U": Head.Y++;
                    break;
                case "D": Head.Y--;
                    break;
            }

            var headLocation = MoveTail(Tail.X, Tail.Y);
            var tailMovement = headLocation[(Head.X, Head.Y)];
            Tail.X += tailMovement.MoveTailX;
            Tail.Y += tailMovement.MoveTailY;
            TailLocations.Add(Tail);
        }
    }

    private static Dictionary<(int HeadX, int HeadY), (int MoveTailX, int MoveTailY)> MoveTail(int x, int y)
    {
        return new Dictionary<(int HeadX, int HeadY), (int MoveTailX, int MoveTailY)>
        {
            // To close to move
            {(x - 1, y - 1), (0, 0)},
            {(x, y - 1), (0, 0)},
            {(x + 1, y - 1), (0, 0)},
            {(x - 1, y), (0, 0)},
            {(x, y), (0, 0)},
            {(x + 1, y), (0, 0)},
            {(x - 1, y + 1), (0, 0)},
            {(x, y + 1), (0, 0)},
            {(x + 1, y + 1), (0, 0)},
            
            // Need to move
            {(x - 1, y - 2), (-1, -1)},
            {(x, y - 2), (0, -1)},
            {(x + 1, y - 2), (1, -1)},
            {(x - 2, y), (-1, 0)},
            {(x + 2, y), (1, 0)},
            {(x - 1, y + 2), (-1, 1)},
            {(x, y + 2), (0, 1)},
            {(x + 1, y + 2), (1, 1)},
            
            {(x - 2, y + 1), (-1, 1)},
            {(x - 2, y - 1), (-1, -1)},
            {(x + 2, y - 1), (1, -1)},
            {(x + 2, y + 1), (1, 1)}
        };
    }
}
