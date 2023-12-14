namespace AOC2023._14;

public class Day14B : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();

        var cycles = new Dictionary<string, int>();

        for (var i = 1; i <= 1000000000; i++)
        {
            Cycle(input);
            // Console.WriteLine($"Cycle {i}");
            // Console.WriteLine(input.ToText());

            var text = input.ToText();
            if (cycles.TryGetValue(text, out var numberOfCycles))
            {
                var cyclesLeft = 1000000000 - i;
                var canAdd = cyclesLeft / (i - numberOfCycles);

                i += canAdd * (i - numberOfCycles);
            }
            else
            {
                cycles.Add(text, i);
            }
        }

        var load = 0;
        for (var y = 0; y < input.GetLength(0); y++)
        {
            var numberOfBoulders = 0;
            for (var x = 0; x < input.GetLength(1); x++)
            {
                if (input[y, x] == 'O')
                {
                    numberOfBoulders++;
                }
            }

            load += numberOfBoulders * (input.GetLength(0) - y);
        }

        return load;
    }

    private static void Cycle(char[,] platform)
    {
        //Console.WriteLine(platform.ToText());
        MoveBouldersNorth(platform);
        //Console.WriteLine(platform.ToText());
        MoveBouldersWest(platform);
        //Console.WriteLine(platform.ToText());
        MoveBouldersSouth(platform);
        //Console.WriteLine(platform.ToText());
        MoveBouldersEast(platform);
    }

    private static void MoveBouldersNorth(char[,] platform)
    {
        for (var y = 1; y < platform.GetLength(0); y++)
        {
            for (var x = 0; x < platform.GetLength(1); x++)
            {
                if (platform[y, x] == 'O')
                {
                    platform[y, x] = '.';
                    
                    var tempY = y;
                    while (tempY > 0 && platform[tempY - 1, x] == '.')
                    {
                        tempY--;
                    }

                    platform[tempY, x] = 'O';
                }
            }
        }
    }
    
    private static void MoveBouldersWest(char[,] platform)
    {
        for (var y = 0; y < platform.GetLength(0); y++)
        {
            for (var x = 1; x < platform.GetLength(1); x++)
            {
                if (platform[y, x] == 'O')
                {
                    platform[y, x] = '.';
                    
                    var tempX = x;
                    while (tempX > 0 && platform[y, tempX - 1] == '.')
                    {
                        tempX--;
                    }

                    platform[y, tempX] = 'O';
                }
            }
        }
    }
    
    private static void MoveBouldersSouth(char[,] platform)
    {
        var edge = platform.GetLength(0) - 1;
        for (var y = edge; y >= 0 ; y--)
        {
            for (var x = 0; x < platform.GetLength(1); x++)
            {
                if (platform[y, x] == 'O')
                {
                    platform[y, x] = '.';
                    
                    var tempY = y;
                    while (tempY < edge && platform[tempY + 1, x] == '.')
                    {
                        tempY++;
                    }

                    platform[tempY, x] = 'O';
                }
            }
        }
    }
    
    private static void MoveBouldersEast(char[,] platform)
    {
        var edge = platform.GetLength(1) - 1;
        for (var y = 0; y < platform.GetLength(0); y++)
        {
            for (var x = edge; x >= 0; x--)
            {
                if (platform[y, x] == 'O')
                {
                    platform[y, x] = '.';
                    
                    var tempX = x;
                    while (tempX < edge && platform[y, tempX + 1] == '.')
                    {
                        tempX++;
                    }

                    platform[y, tempX] = 'O';
                }
            }
        }
    }
}
