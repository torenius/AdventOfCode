namespace AOC2022._10;

public class Day10A : Day
{
    public override string Run()
    {
        var input = GetInputAsStringArray();

        var cycleCount = 0;
        var measurePoints = new List<int>();
        var signalStrength = 1;
        foreach (var signal in input)
        {
            var command = signal.Split(" ");

            for (var i = 0; i < command.Length; i++)
            {
                cycleCount++;
                if ((cycleCount - 20) % 40 == 0)
                {
                    measurePoints.Add(signalStrength * cycleCount);
                }
            }

            if (command.Length == 2)
            {
                signalStrength += command[1].ToInt();
            }
        }

        return "" + measurePoints.Sum();
    }
}