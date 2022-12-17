namespace AOC2022._10;

public class Day10B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        var cycleCount = 0;
        var spriteCenterPosition = 1;
        foreach (var signal in input)
        {
            var command = signal.Split(" ");

            for (var i = 0; i < command.Length; i++)
            {
                cycleCount++;

                var drawX = (cycleCount - 1) % 40;

                if (drawX == spriteCenterPosition - 1 || drawX == spriteCenterPosition ||
                    drawX == spriteCenterPosition + 1)
                {
                    Console.Write("#");
                }
                else
                {
                    Console.Write(".");
                }

                if (cycleCount % 40 == 0)
                {
                    Console.WriteLine();
                }
            }

            if (command.Length == 2)
            {
                spriteCenterPosition += command[1].ToInt();
            }
        }

        return "";
    }
}