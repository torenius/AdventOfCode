using AOC.Common;

namespace AOC2024._24;

public class Day24A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var wires = new List<Wire>();
        var staticWires = true;
        foreach (var row in input)
        {
            if (row == "")
            {
                staticWires = false;
                continue;
            }

            if (staticWires)
            {
                var sg = row.Split(": ");
                var wire = new Wire(sg[0])
                {
                    Input = new StaticValue(sg[1] == "1")
                };
                
                wires.Add(wire);
            }
            else
            {
                var g = row.Split(" ");
                var left = GetOrCreate(g[0]);
                var right = GetOrCreate(g[2]);
                var output = GetOrCreate(g[4]);
                Gate gate = g[1] switch
                {
                    "AND" => new AndGate(),
                    "OR" => new OrGate(),
                    "XOR" => new XorGate(),
                    _ => throw new ArgumentOutOfRangeException()
                };

                gate.InputA = left;
                gate.InputB = right;
                output.Input = gate;
            }
        }

        var binary = "";
        var zWires = wires.Where(w => w.Label.StartsWith('z')).OrderByDescending(w => w.Label).ToList();
        foreach (var wire in zWires)
        {
            binary += wire.GetOutputValue() ? "1" : "0";
        }
        
        var result = Convert.ToInt64(binary, 2);
        Console.WriteLine($"{binary} -> {result}");

        return result;

        Wire GetOrCreate(string label)
        {
            var wire = wires.FirstOrDefault(w => w.Label == label);
            if (wire is null)
            {
                wire = new Wire(label);
                wires.Add(wire);
            }

            return wire;
        }
    }
    
    private class Wire(string label)
    {
        public string Label { get; set; } = label;
        public bool? Output { get; set; }
        public Gate Input { get; set; }

        public bool GetOutputValue()
        {
            if (Output.HasValue) return Output.Value;

            Output = Input.GetOutputValue();

            return Output.Value;
        }
    }

    private abstract class Gate
    {
        public Wire InputA { get; set; }
        public Wire InputB { get; set; }

        public abstract bool GetOutputValue();
    }
    
    private class StaticValue(bool output) : Gate
    {
        private bool Output { get; init; } = output;

        public override bool GetOutputValue() => Output;
    }

    private class AndGate : Gate
    {
        public override bool GetOutputValue() => InputA.GetOutputValue() && InputB.GetOutputValue();
    }
    
    private class OrGate : Gate
    {
        public override bool GetOutputValue() => InputA.GetOutputValue() || InputB.GetOutputValue();
    }
    
    private class XorGate : Gate
    {
        public override bool GetOutputValue() => InputA.GetOutputValue() ^ InputB.GetOutputValue();
    }
}