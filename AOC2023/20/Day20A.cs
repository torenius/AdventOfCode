namespace AOC2023._20;

public class Day20A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var modules = new Dictionary<string, Module>();
        modules.Add("button", new Button("buttons -> broadcaster"));
        
        foreach (var s in input)
        {
            if (s.StartsWith("broadcaster"))
            {
                var broadcaster = new Broadcaster(s);
                modules.Add(broadcaster.Name, broadcaster);
            }
            else if (s[0] == '%')
            {
                var flipFlop = new FlipFlop(s);
                modules.Add(flipFlop.Name, flipFlop);
            }
            else if (s[0] == '&')
            {
                var conjunction = new Conjunction(s);
                modules.Add(conjunction.Name, conjunction);
            }
        }

        foreach (var conjunction in modules.Where(x => x.Value.GetType() == typeof(Conjunction)).Select(x => x.Value).Cast<Conjunction>())
        {
            foreach (var inputModule in modules.Where(x => x.Value.OutputModules.Contains(conjunction.Name)).Select(x => x.Key))
            {
                conjunction.LastSignal.Add(inputModule, false);
            }
        }

        var checkForDummyModules = modules.Values.SelectMany(x => x.OutputModules).Distinct().ToList();
        foreach (var module in checkForDummyModules)
        {
            if (!modules.ContainsKey(module))
            {
                modules.Add(module, new Dummy(module));
            }
        }

        for (var i = 0; i < 1000; i++)
        {
            ButtonPress(modules, false);
        }

        long lowCount = 0;
        long highCount = 0;
        foreach (var module in modules.Values)
        {
            lowCount += module.LowPulseCount;
            highCount += module.HighPulseCount;
        }
        
        Console.WriteLine($"low: {lowCount} high: {highCount}");
        
        return lowCount * highCount;
    }

    private static void ButtonPress(IReadOnlyDictionary<string, Module> modules, bool print = false)
    {
        var queue = new Queue<Module>();
        var button = modules["button"];
        button.UpdateOutput(null, print);
        queue.Enqueue(button);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            foreach (var outputModule in current.OutputModules)
            {
                var output = modules[outputModule];
                if (output.UpdateOutput(current, print))
                {
                    queue.Enqueue(output);
                }
            }
        }
    }

    private abstract class Module
    {
        public Module(string input)
        {
            var temp = input.Split(" -> ");
            OutputModules = temp[1].Split(", ").ToList();
            Name = temp[0].TrimStart('%', '&');
        }
        public string Name { get; set; }
        
        public List<string> OutputModules { get; set; } = new();
        public int LowPulseCount { get; set; }
        public int HighPulseCount { get; set; }
        public abstract bool Output { get; set; }

        public abstract bool UpdateOutput(Module input, bool print = false);
    }

    private class Button : Module
    {
        public Button(string input) : base(input)
        {
        }
        public override bool Output { get; set; } = false;
        public override bool UpdateOutput(Module input, bool print = false)
        {
            LowPulseCount++;
            if (print)
            {
                Console.WriteLine("button -low-> broadcaster");
            }

            return true;
        }
    }

    private class Broadcaster : Module 
    {
        public Broadcaster(string input) : base(input)
        {
        }
        
        public override bool Output { get; set; } = false;

        public override bool UpdateOutput(Module input, bool print = false)
        {
            LowPulseCount += OutputModules.Count;

            if (print)
            {
                foreach (var module in OutputModules)
                {
                    Console.WriteLine($"Broadcaster -low-> {module}");
                }
            }

            return true;
        }
    }

    private class FlipFlop : Module
    {
        public FlipFlop(string input) : base(input)
        {
        }
        
        private bool IsOn { get; set; } = false;

        public override bool Output { get; set; }
        
        public override bool UpdateOutput(Module input, bool print = false)
        {
            if (input.Output)
            {
                return false;
            }

            if (IsOn)
            {
                IsOn = false;
                Output = false;
                LowPulseCount += OutputModules.Count;
                
                
            }
            else
            {
                IsOn = true;
                Output = true;
                HighPulseCount += OutputModules.Count;
            }

            if (print)
            {
                foreach (var module in OutputModules)
                {
                    Console.WriteLine($"{Name} -{(IsOn ? "high" : "low")}-> {module}");
                }
            }

            return true;
        }
    }

    private class Conjunction : Module
    {
        public Conjunction(string input) : base(input)
        {
        }

        public Dictionary<string, bool> LastSignal { get; set; } = new();
        public override bool Output { get; set; }
        public override bool UpdateOutput(Module input, bool print = false)
        {
            LastSignal[input.Name] = input.Output;

            if (LastSignal.Values.All(x => x))
            {
                Output = false;
                LowPulseCount += OutputModules.Count;
            }
            else
            {
                Output = true;
                HighPulseCount += OutputModules.Count;
            }

            if (print)
            {
                foreach (var module in OutputModules)
                {
                    Console.WriteLine($"{Name} -{(Output ? "high" : "low")}-> {module}");
                }
            }

            return true;
        }
    }

    private class Dummy : Module
    {
        public Dummy(string input) : base("dummy -> dummy")
        {
            Name = input;
            OutputModules = new List<string>();
        }

        public override bool Output { get; set; }
        public override bool UpdateOutput(Module input, bool print = false)
        {
            return true;
        }
    }
}