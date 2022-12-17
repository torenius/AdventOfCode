namespace AOC2022._03;

public class Day03A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        long score = 0;
        foreach (var rucksack in input)
        {
            var first = rucksack[..(rucksack.Length / 2)];
            var second = rucksack[(rucksack.Length / 2)..];
            var inBoth = AppearInBoth(first, second);
            score += GetScore(inBoth);
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
