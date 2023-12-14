namespace AOC2023._14;

public class Day14A : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();

        MoveBoulders(input);
        
        //Console.WriteLine(input.ToText());

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

    private static void MoveBoulders(char[,] platform)
    {
        for (var y = 1; y < platform.GetLength(0); y++)
        {
            for (var x = 0; x < platform.GetLength(1); x++)
            {
                if (platform[y, x] == 'O')
                {
                    platform[y, x] = '.';
                    MoveBoulder(platform, y, x);
                }
            }
        }
    }

    private static void MoveBoulder(char[,] platform, int y, int x)
    {
        while (y > 0 && platform[y - 1, x] == '.')
        {
            y--;
        }

        platform[y, x] = 'O';
    }
}
