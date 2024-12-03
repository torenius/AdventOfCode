using System.Text.RegularExpressions;

namespace AOC2024._03;

public partial class Day03A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var sum = 0;
        foreach (var row in input)
        {
            var matches = Mul().Matches(row);
            foreach (Match match in matches)
            {
                var a = match.Groups["a"].Value.ToInt();
                var b = match.Groups["b"].Value.ToInt();
                var c = a * b;
                sum += c;
            }
        }

        return sum;
    }

    [GeneratedRegex(@"(?<mul>mul\((?<a>[0-9]{1,3}),(?<b>[0-9]{1,3})\))")]
    private partial Regex Mul();
}
