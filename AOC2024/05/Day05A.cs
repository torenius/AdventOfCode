namespace AOC2024._05;

public class Day05A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var rules = new List<Rule>();
        var updates = new List<int[]>();

        var isRules = true;
        foreach (var row in input)
        {
            if (row == "")
            {
                isRules = false;
                continue;
            }

            if (isRules)
            {
                var rule = row.Split('|');
                rules.Add(new Rule{ A = rule[0].ToInt(), B = rule[1].ToInt() });    
            }
            else
            {
                updates.Add(row.Split(",").ToIntArray());
            }
        }

        var sum = 0;
        foreach (var update in updates)
        {
            if (IsInOrder(rules, update))
            {
                sum += update[update.Length / 2];
            }
        }

        return sum;
    }

    private static bool IsInOrder(List<Rule> rules, int[] input)
    {
        for (var i = 1; i < input.Length; i++)
        {
            var rulesToCheck = rules.Where(r => r.A == input[i]).ToList();
            if (rulesToCheck.Any(rule => input[..i].Contains(rule.B)))
            {
                return false;
            }
        }
        
        return true;
    }

    private class Rule
    {
        public int A { get; set; }
        public int B { get; set; }
    }
}
