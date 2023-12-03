namespace AOC2015._18;

public class Day18B : Day
{
    protected override string Run()
    {
        const int steps = 100;
        var input = GetInputAsCharMatrix();
        TurnOnCorner(input);
        // Helper.Print(input);
        // Console.WriteLine();
        
        var step = input;
        for (var i = 1; i <= steps; i++)
        {
            step = OnStep(step);
            TurnOnCorner(step);
            // Console.WriteLine($"Step {i}");
            // Helper.Print(step);
            // Console.WriteLine();
        }
        
        return "" + OnCount(step);
    }

    private static void TurnOnCorner(char[,] input)
    {
        var yLength = input.GetLength(0) - 1;
        var xLength = input.GetLength(1) - 1;
        input[0, 0] = '#';
        input[0, xLength] = '#';
        input[yLength, 0] = '#';
        input[yLength, xLength] = '#';
        
    }

    private static int OnCount(char[,] input)
    {
        var yLength = input.GetLength(0);
        var xLength = input.GetLength(1);
        var onCount = 0;
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                if (input[y, x] == '#')
                {
                    onCount++;
                }
            }
        }

        return onCount;
    }

    private static char[,] OnStep(char[,] input)
    {
        var afterStep = new char[input.GetLength(0), input.GetLength(1)];
        var yLength = input.GetLength(0);
        var xLength = input.GetLength(1);
        for (var y = 0; y < yLength; y++)
        {
            for (var x = 0; x < xLength; x++)
            {
                var onCount = 0;
                foreach (var cord in Helper.GetAdjacentCoordinates(y, x))
                {
                    if (cord.Y >= 0 && cord.Y < yLength &&
                        cord.X >= 0 && cord.X < xLength &&
                        input[cord.Y, cord.X] == '#')
                    {
                        onCount++;
                    }
                }
                
                var isOn = input[y, x] == '#';
                afterStep[y, x] = isOn switch
                {
                    true when onCount != 2 && onCount != 3 => '.',
                    false when onCount == 3 => '#',
                    _ => input[y, x]
                };
            }
        }

        return afterStep;
    }
}