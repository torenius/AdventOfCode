namespace AOC2022._03;

public class Day03B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        long score = 0;

        for (var i = 0; i < input.Length - 2; i += 3)
        {
            var first = input[i];
            var second = input[i + 1];
            var third = input[i + 2];

            var firstAndSecondCommon = AppearInBoth(first, second);
            var common = AppearInBoth(new string(firstAndSecondCommon.ToArray()), third);
            score += GetScore(common);
        }

        return score.ToString();
    }

    private static long GetScore(IEnumerable<char> inBoth)
    {
        var score = 0;
        foreach (var c in inBoth)
        {
            if (char.IsLower(c))
            {
                score += (int)c - 96;
            }
            else
            {
                score += (int)c - 38;
            }
        }

        return score;
    }

    private static IEnumerable<char> AppearInBoth(string first, string second)
    {
        var inBoth = new List<char>();
        for (var i = 0; i < first.Length; i++)
        {
            for (var k = 0; k < second.Length; k++)
            {
                if (first[i] == second[k])
                {
                    inBoth.Add(first[i]);
                }
            }
        }

        return inBoth.Distinct().ToArray();
    }
}
