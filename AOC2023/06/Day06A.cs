namespace AOC2023._06;

public class Day06A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var time = input[0][5..].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToInt()).ToArray();
        var distance = input[1][9..].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToInt()).ToArray();
        var waysToWin = new Dictionary<int, List<int>>();
        for (var i = 0; i < time.Length; i++)
        {
            waysToWin.Add(i, CheckWaysToWin(time[i], distance[i]));
        }

        var result = 1;
        foreach (var kvp in waysToWin)
        {
            result *= kvp.Value.Count;
        }
        
        return "" + result;
    }

    private static List<int> CheckWaysToWin(int raceDuration, int distanceToBeat)
    {
        var result = new List<int>();
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
