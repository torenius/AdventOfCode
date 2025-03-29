using AOC.Common;

namespace AOC2024._19;

public class Day19A : Day
{
    private string[] _towels;
    private readonly HashSet<string> _valid = [];
    private readonly HashSet<string> _invalid = [];
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        _towels = input[0].Split(", ");

        var patterns = new List<string>();
        for (var i = 2; i < input.Length; i++)
        {
            patterns.Add(input[i]);
        }

        var count = 0;
        foreach (var pattern in patterns)
        {
            PrintElapsedTime(pattern);
            if (IsPossible(pattern)) count++;
        }
        
        return count;
    }

    private bool IsPossible(string pattern)
    {
        if (pattern.Length == 0) return true;
        if (_valid.Contains(pattern)) return true;
        if (_invalid.Contains(pattern)) return false;

        var result = false;
        foreach (var towel in _towels)
        {
            if (pattern.StartsWith(towel))
            {
                var test = pattern[towel.Length..];
                var possible = IsPossible(test);
                if (possible)
                {
                    _valid.Add(test);
                    result = true;
                }
                else
                {
                    _invalid.Add(test);
                }
            }
        }

        return result;
    }
}
