namespace AOC2023._06;

public class Day06B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var time = string.Join("", input[0][5..].Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToLong();
        var distance = string.Join("", input[1][9..].Split(" ", StringSplitOptions.RemoveEmptyEntries)).ToLong();
        
        return "" + CheckWaysToWin(time, distance).Count;
    }

    private static List<long> CheckWaysToWin(long raceDuration, long distanceToBeat)
    {
        var result = new List<long>();
        for (var i = 1; i < raceDuration; i++)
        {
            var distance = (raceDuration - i) * i;
            if (distance > distanceToBeat)
            {
                result.Add(distance);
            }
        }

        return result;
    }
}
