using System.Text;
using AOC.Common;

namespace AOC2016._06;

public class Day06B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var common = new Dictionary<int, Dictionary<char, int>>();
        foreach (var row in input)
        {
            for (var i = 0; i < row.Length; i++)
            {
                if (!common.ContainsKey(i))
                {
                    common.Add(i, new Dictionary<char, int>());
                }

                if (!common[i].TryAdd(row[i], 1))
                {
                    common[i][row[i]] += 1;
                }
            }
        }

        var sb = new StringBuilder();
        foreach (var kvp in common.OrderBy(x => x.Key))
        {
            sb.Append(kvp.Value.MinBy(x => x.Value).Key);
        }

        return sb.ToString();
    }
}