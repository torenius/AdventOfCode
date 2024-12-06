namespace AOC2024._06;

public class Day06B : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();

        var guardX = -1;
        var guardY = -1;
        
        var maxX = input.GetLength(1);
        var maxY = input.GetLength(0);
        
        for (var y = 0; y < maxY; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                if (input[y, x] == '^')
                {
                    guardX = x;
                    guardY = y;
                    break;
                }
            }

            if (guardX != -1) break;
        }

        var loopCount = 0;
        for (var y = 0; y < maxY; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                if (input[y, x] == '.')
                {
                    // var map = input.Copy();
                    // map[y, x] = '#';
                    input[y, x] = '#';
                    if (IsLoop(input, new Guard {X = guardX, Y = guardY}))
                    {
                        loopCount++;
                    }
                    input[y, x] = '.';
                }
            }
        }

        return loopCount;
    }

    private static bool IsLoop(char[,] input, Guard guard)
    {
        var maxX = input.GetLength(1);
        var maxY = input.GetLength(0);

        var visit = new HashSet<(int X, int Y, char Direction)>();
        
        do
        {
            if (input[guard.Y, guard.X] == '#')
            {
                guard.Y -= guard.YDirection;
                guard.X -= guard.XDirection;
                guard.TurnRight();
                //input[guard.Y, guard.X] = '+';
            }
            else
            {
                if (!visit.Add((guard.X, guard.Y, guard.Direction)))
                {
                    // Loop
                    //Helper.Print(input, new Dictionary<char, ConsoleColor> { { '#', ConsoleColor.Red }, { 'X', ConsoleColor.Green }, {'+', ConsoleColor.Blue} });
                    return true;
                }
                
                //input[guard.Y, guard.X] = 'X';
            }

            guard.Walk();

        } while (IsValid(maxX, maxY, guard.X, guard.Y));

        return false;
    }

    private static bool IsValid(int maxX, int maxY, int x, int y) =>
        y >= 0 && y < maxY && x >= 0 && x < maxX;

    private class Guard
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int XDirection { get; set; }
        public int YDirection { get; set; } = -1;
        public char Direction { get; set; } = '^';

        public void Walk()
        {
            X += XDirection;
            Y += YDirection;
        }

        public void TurnRight()
        {
            switch (Direction)
            {
                case '^':
                    Direction = '>';
                    XDirection = 1;
                    YDirection = 0;
                    break;
                case '>':
                    Direction = 'v';
                    XDirection = 0;
                    YDirection = 1;
                    break;
                case 'v':
                    Direction = '<';
                    XDirection = -1;
                    YDirection = 0;
                    break;
                case '<':
                    Direction = '^';
                    XDirection = 0;
                    YDirection = -1;
                    break;
            }
        }
    }
}
