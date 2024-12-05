namespace AOC2024._05;

public class Day05B : Day
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
            if (!IsInOrder(rules, update))
            {
                var ordered= Order(rules, update);
                sum += ordered[update.Length / 2];
            }
        }

        return sum;
    }

    private static int[] Order(List<Rule> rules, int[] input)
    {
        var list = input.ToList();
        list.Sort(CompareUpdate);
        return list.ToArray();
        
        int CompareUpdate(int a, int b)
        {
            var rulesA = rules.Where(r => r.A == a).ToList();
            if (rulesA.Any(r => r.B == b)) return -1;
            
            var rulesB = rules.Where(r => r.A == b).ToList();
            if (rulesB.Any(r => r.B == a)) return 1;
            
            return 0;
        }
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
