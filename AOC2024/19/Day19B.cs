namespace AOC2024._19;

public class Day19B : Day
{
    private string[] _towels;
    private readonly Dictionary<string, long> _valid = [];
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

        long count = 0;
        foreach (var pattern in patterns)
        {
            var result = IsPossible(pattern, []);
            count += result.Count;
            PrintElapsedTime(pattern + " " + result.Count);
        }
        
        return count;
    }

    private (bool IsPossible, long Count) IsPossible(string pattern, List<string> towels)
    {
        if (pattern.Length == 0) return (true, 1);
        if (_valid.TryGetValue(pattern, out var value)) return (true, value);
        if (_invalid.Contains(pattern)) return (false, 0);

        var result = false;
        long count = 0;
        foreach (var towel in _towels)
        {
            if (pattern.StartsWith(towel))
            {
                var test = pattern[towel.Length..];
                var newList = towels.ToList();
                newList.Add(towel);
                
                var internalResult = IsPossible(test, newList);
                if (internalResult.IsPossible)
                {
                    result = true;
                    count += internalResult.Count;

                    if (!_valid.TryAdd(pattern, internalResult.Count))
                    {
                        _valid[pattern] += internalResult.Count;
                    }
                }
                else
                {
                    _invalid.Add(test);
                }
            }
        }

        return (result, count);
    }
}
