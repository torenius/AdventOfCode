using AOC.Common;

namespace AOC2025._03;

public class Day03A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var sum = 0;
        foreach (var row in input)
        {
            var maxA = row[0].ToInt();
            var maxI = 0;
            for (var i = 1; i < row.Length - 1; i++)
            {
                var number = row[i].ToInt();
                if (number > maxA)
                {
                    maxA = number;
                    maxI = i;
                }
            }
            
            var maxB = row[maxI + 1].ToInt();
            for (var i = maxI + 1; i < row.Length; i++)
            {
                var number = row[i].ToInt();
                if (number > maxB)
                {
                    maxB = number;
                }
            }

            var joltage = $"{maxA}{maxB}".ToInt();
            sum += joltage;
        }

        return sum;
    }
}