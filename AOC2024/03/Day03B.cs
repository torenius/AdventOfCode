using System.Text.RegularExpressions;
using AOC.Common;

namespace AOC2024._03;

public partial class Day03B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var enabled = true;
        var sum = 0;
        foreach (var row in input)
        {
            var matches = Mul().Matches(row);
            foreach (Match match in matches)
            {
                if (match.Value == "don't()")
                {
                    enabled = false;
                }
                else if (match.Value == "do()")
                {
                    enabled = true;
                }
                else if (enabled)
                {
                    var a = match.Groups["a"].Value.ToInt();
                    var b = match.Groups["b"].Value.ToInt();
                    var c = a * b;
                    sum += c;
                }
            }
        }

        return sum;
    }

    [GeneratedRegex(@"(?<mul>mul\((?<a>[0-9]{1,3}),(?<b>[0-9]{1,3})\))|(?<do>do(n't)?\(\))")]
    private partial Regex Mul();
}
