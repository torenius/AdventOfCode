using AOC.Common;

namespace AOC2024._02;

public class Day02A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        return input.Count(IsSafe);
    }

    private static bool IsSafe(string input)
    {
        var values = input.Split(" ").ToIntArray();
        if (values[0] == values[1]) return false;
        
        var isDescending = values[0] - values[1] > 0;

        for (var i = 0; i < values.Length - 1; i++)
        {
            var diff = values[i] - values[i + 1];
            if (isDescending && diff <= 0) return false;
            if (!isDescending && diff >= 0) return false;
            if (Math.Abs(diff) is not (1 or 2 or 3)) return false;
        }
        
        return true;
    }
}
