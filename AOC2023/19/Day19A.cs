namespace AOC2023._19;

public class Day19A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var workflows = new Dictionary<string, Workflow>();
        var parts = new List<Part>();
        var workflow = true;
        foreach (var s in input)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                workflow = false;
                continue;
            }

            if (workflow)
            {
                var w = new Workflow(s);
                workflows.Add(w.Name, w);
            }
            else
            {
                var p = new Part(s);
                parts.Add(p);
            }
        }

        foreach (var part in parts)
        {
            do
            {
                var w = workflows[part.CurrentWorkflow];
                part.CurrentWorkflow = w.Proccess(part);

            } while (part.CurrentWorkflow != "A" && part.CurrentWorkflow != "R");
        }

        var sum = 0;
        foreach (var part in parts.Where(p => p.CurrentWorkflow == "A"))
        {
            sum += part.X + part.M + part.A + part.S;
        }
        
        return sum;
    }

    private class Workflow
    {
        public Workflow(string input)
        {
            var ruleIndex = input.IndexOf('{');
            Name = input[..ruleIndex];
            var rules = input[ruleIndex..].Trim('{', '}').Split(",");
            DefaultValue = rules[^1];
            
            Rules = new List<Rule>();
            foreach (var rule in rules[..^1])
            {
                Rules.Add(new Rule(rule));
            }
        }

        public string Name { get; set; }
        public List<Rule> Rules { get; set; }
        public string DefaultValue { get; set; }

        public string Proccess(Part part)
        {
            foreach (var rule in Rules)
            {
                if (rule.Satisfy(part))
                {
                    return rule.SendToWorkflow;
                }
            }
            
            return DefaultValue;
        }
    }

    private class Rule
    {
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
        }

        public string PartType { get; set; }
        public bool GreaterThan { get; set; }
        public int ControlValue { get; set; }
        public string SendToWorkflow { get; set; }

        public bool Satisfy(Part part) => PartType switch
        {
            "x" when GreaterThan && part.X > ControlValue => true,
            "x" when !GreaterThan && part.X < ControlValue => true,
            "m" when GreaterThan && part.M > ControlValue => true,
            "m" when !GreaterThan && part.M < ControlValue => true,
            "a" when GreaterThan && part.A > ControlValue => true,
            "a" when !GreaterThan && part.A < ControlValue => true,
            "s" when GreaterThan && part.S > ControlValue => true,
            "s" when !GreaterThan && part.S < ControlValue => true,
            _ => false
        };
    }

    private class Part
    {
        public Part(string input)
        {
            var temp = input.Trim('{', '}').Split(",");
            X = temp[0].Split("=")[1].ToInt();
            M = temp[1].Split("=")[1].ToInt();
            A = temp[2].Split("=")[1].ToInt();
            S = temp[3].Split("=")[1].ToInt();
            CurrentWorkflow = "in";
        }

        public int X { get; set; } // Extremely cool looking
        public int M { get; set; } // Musical (it makes a noise when you hit it)
        public int A { get; set; } // Aerodynamic
        public int S { get; set; } // Shiny
        public string CurrentWorkflow { get; set; }
    }
}
