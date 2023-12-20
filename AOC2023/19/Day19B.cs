namespace AOC2023._19;

public class Day19B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var workflows = new Dictionary<string, Workflow>();
        foreach (var s in input)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                break;
            }

            var w = new Workflow(s);
            workflows.Add(w.Name, w);
        }

        var paths = GetHappyPaths(workflows);
        var intervals = paths.Select(GetInterval).ToList();

        long sum = 0;
        foreach (var interval in intervals)
        {
            Console.WriteLine(interval.Combinations);
            sum += interval.Combinations;
        }
        
        return sum;
    }

    private static Interval GetInterval(List<Rule> rules)
    {
        var interval = new Interval();
        foreach (var rule in rules)
        {
            switch (rule.PartType, rule.GreaterThan)
            {
                case ("x", true):
                    if (rule.ControlValue > interval.MinX)
                    {
                        interval.MinX = rule.ControlValue + 1;
                    }
                    break;
                case ("x", false):
                    if (rule.ControlValue < interval.MaxX)
                    {
                        interval.MaxX = rule.ControlValue - 1;
                    }
                    break;
                case ("m", true):
                    if (rule.ControlValue > interval.MinM)
                    {
                        interval.MinM = rule.ControlValue + 1;
                    }
                    break;
                case ("m", false):
                    if (rule.ControlValue < interval.MaxM)
                    {
                        interval.MaxM = rule.ControlValue - 1;
                    }
                    break;
                case ("a", true):
                    if (rule.ControlValue > interval.MinA)
                    {
                        interval.MinA = rule.ControlValue + 1;
                    }
                    break;
                case ("a", false):
                    if (rule.ControlValue < interval.MaxA)
                    {
                        interval.MaxA = rule.ControlValue - 1;
                    }
                    break;
                case ("s", true):
                    if (rule.ControlValue > interval.MinS)
                    {
                        interval.MinS = rule.ControlValue + 1;
                    }
                    break;
                case ("s", false):
                    if (rule.ControlValue < interval.MaxS)
                    {
                        interval.MaxS = rule.ControlValue - 1;
                    }
                    break;
            }
        }

        return interval;
    }

    private class Interval
    {
        public long MinX { get; set; } = 1;
        public long MaxX { get; set; } = 4000;
        public long MinM { get; set; } = 1;
        public long MaxM { get; set; } = 4000;
        public long MinA { get; set; } = 1;
        public long MaxA { get; set; } = 4000;
        public long MinS { get; set; } = 1;
        public long MaxS { get; set; } = 4000;

        public long Combinations =>
            (MaxX - MinX + 1) * (MaxM - MinM + 1) * (MaxA - MinA + 1) * (MaxS - MinS + 1);
    }

    private static List<List<Rule>> GetHappyPaths(Dictionary<string, Workflow> workflows)
    {
        var result = new List<List<Rule>>();
        
        var root = workflows["in"];
        
        var visit = new HashSet<string>();
        
        var stack = new Stack<(Workflow, List<Rule>)>();
        stack.Push((root, new List<Rule>()));

        while (stack.Count > 0)
        {
            var (current, gotHereByRule) = stack.Pop();

            if (!visit.Add(current.Name))
            {
                continue;
            }


            for (var i = 0; i < current.Rules.Count; i++)
            {
                var rule = current.Rules[i];
                if (!visit.Contains(rule.SendToWorkflow))
                {
                    if (rule.SendToWorkflow == "R")
                    {
                        continue;
                    }
                    
                    var newList = gotHereByRule.ToList();
                    for (var k = 0; k < i; k++)
                    {
                        newList.Add(current.Rules[k].Reverse);
                    }
                    
                    newList.Add(rule);
                    
                    if (rule.SendToWorkflow == "A")
                    {
                        result.Add(newList);   
                    }
                    else
                    {
                        stack.Push((workflows[rule.SendToWorkflow], newList));    
                    }
                }
            }
        }

        return result;
    }

    private class Workflow
    {
        public Workflow(string input)
        {
            var ruleIndex = input.IndexOf('{');
            Name = input[..ruleIndex];
            var rules = input[ruleIndex..].Trim('{', '}').Split(",");
            
            Rules = new List<Rule>();
            foreach (var rule in rules[..^1])
            {
                Rules.Add(new Rule(rule));
            }
            
            var defaultValue = rules[^1];
            Rules.Add(new Rule("x>0:" + defaultValue));
        }

        public string Name { get; set; }
        public List<Rule> Rules { get; set; }
    }

    private class Rule
    {
        public Rule()
        {
            
        }
        public Rule(string input)
        {
            var temp = input.Split(":");
            SendToWorkflow = temp[1];
            
            var lessThan = temp[0].IndexOf('<');
            var greaterThan = temp[0].IndexOf('>');
            var split = Math.Max(lessThan, greaterThan);

            PartType = temp[0][..split];
            GreaterThan = greaterThan > -1;
            ControlValue = temp[0][(split + 1)..].ToInt();

            var reverse = new Rule
            {
                PartType = PartType
            };

            if (GreaterThan)
            {
                reverse.GreaterThan = false;
                reverse.ControlValue =  ControlValue + 1;
            }
            else
            {
                reverse.GreaterThan = true;
                reverse.ControlValue = ControlValue - 1;
            }

            Reverse = reverse;
        }

        public Rule Reverse { get; set; }

        public string PartType { get; set; }
        public bool GreaterThan { get; set; }
        public int ControlValue { get; set; }
        public string SendToWorkflow { get; set; }
        
    }
}
