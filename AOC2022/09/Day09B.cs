using System.Drawing;

namespace AOC2022._09;

public class Day09B : Day
{
    private Point[] Snake = new []
    {
        new Point(0, 0),
        new Point(0, 0),
        new Point(0, 0),
        new Point(0, 0),
        new Point(0, 0),
        new Point(0, 0),
        new Point(0, 0),
        new Point(0, 0),
        new Point(0, 0),
        new Point(0, 0)
    };
    
    private HashSet<Point> TailLocations = new HashSet<Point>();

    public override string Run()
    {
        var input = GetInputAsStringArray();

        TailLocations.Add(Snake[9]);

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
                case "R": Snake[0].X++;
                    break;
                case "L": Snake[0].X--;
                    break;
                case "U": Snake[0].Y++;
                    break;
                case "D": Snake[0].Y--;
                    break;
            }

            for (var k = 1; k <= 9; k++)
            {
                var possibleMoves = MoveBody(Snake[k].X, Snake[k].Y);
                var movement = possibleMoves[(Snake[k - 1].X, Snake[k - 1].Y)];
                Snake[k].X += movement.MoveTailX;
                Snake[k].Y += movement.MoveTailY;
            }

            TailLocations.Add(Snake[9]);
        }
    }

    private static Dictionary<(int HeadX, int HeadY), (int MoveTailX, int MoveTailY)> MoveBody(int x, int y)
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
            {(x + 2, y + 1), (1, 1)},
            
            {(x - 2, y + 2), (-1, 1)},
            {(x - 2, y - 2), (-1, -1)},
            {(x + 2, y + 2), (1, 1)},
            {(x + 2, y - 2), (1, -1)},
        };
    }
}
