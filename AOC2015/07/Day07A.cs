namespace AOC2015._07;

public class Day07A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var wires = input.Select(x => new Wire(x)).ToList();
        // Looks like input sometimes have a 1 as the input to AND gates
        wires.Add(new Wire("1 -> 1"));
        foreach (var wire in wires)
        {
            var (gate, wiresName) = GateFactory.GetGate(wire.InputString);
            wire.Input = gate;
            gate.Inputs = wires.Where(x => wiresName.Contains(x.Label)).ToArray();
        }

        // foreach (var wire in wires)
        // {
        //     Console.WriteLine($"{wire.Label}: {wire.GetOutputValue()}");
        // }
        
        var wireA = wires.First(x => x.Label == "a");
        var output = wireA.Input.OutputValue();

        return "" + output;
    }

    private class Wire
    {
        public Wire(string input)
        {
            var temp = input.Split(" -> ");
            InputString = temp[0];
            Label = temp[1];
        }

        public string InputString { get; set; }
        public string Label { get; set; }
        public int? Output { get; set; }
        public Gate Input { get; set; }

        public int GetOutputValue()
        {
            if (Output.HasValue) return Output.Value;

            Console.WriteLine($"{InputString} -> {Label} {Input.GetType().Name} {string.Join(" ", Input.Inputs.Select(x => x.Label))}");
            Output = Input.OutputValue();

            return Output.Value;
        }
    }

    private static class GateFactory
    {
        public static (Gate Gate, string[] WireNames) GetGate(string input)
        {
            if (input.Contains("AND"))
            {
                var temp = input.Split(" AND ");
                return (new AndGate(), temp);
            }
            else if (input.Contains("OR"))
            {
                var temp = input.Split(" OR ");
                return (new OrGate(), temp);
            }
            else if (input.Contains("LSHIFT"))
            {
                var temp = input.Split(" LSHIFT ");
                return (new LShiftGate(temp[1].ToInt()), new []{temp[0]});
            }
            else if (input.Contains("RSHIFT"))
            {
                var temp = input.Split(" RSHIFT ");
                return (new RShiftGate(temp[1].ToInt()), new []{temp[0]});
            }
            else if (input.Contains("NOT"))
            {
                var temp = input.Split("NOT ");
                return (new NotGate(), new []{temp[1]});
            }
            else if (int.TryParse(input, out var result))
            {
                return (new SpecificValue(result), Array.Empty<string>());    
            }

            return (new WireToWire(), new []{input});
        }
    }

    private abstract class Gate
    {
        public Wire[] Inputs { get; set; } = Array.Empty<Wire>();
        public bool HaveAllInputSignals => Inputs.All(x => x.Output.HasValue);

        public abstract int OutputValue();
    }

    private class SpecificValue : Gate
    {
        private readonly int _value;

        public SpecificValue(int value)
        {
            _value = value;
        }
        
        public override int OutputValue() => _value;
    }

    private class WireToWire : Gate
    {
        public override int OutputValue() => Inputs[0].GetOutputValue();
    }

    private class AndGate : Gate
    {
        public override int OutputValue() => Inputs[0].GetOutputValue() & Inputs[1].GetOutputValue();
    }

    private class OrGate : Gate
    {
        public override int OutputValue() => Inputs[0].GetOutputValue() | Inputs[1].GetOutputValue();
    }

    private class LShiftGate : Gate
    {
        private readonly int _shiftNumber;

        public LShiftGate(int shiftNumber)
        {
            _shiftNumber = shiftNumber;
        }
        public override int OutputValue() => Inputs[0].GetOutputValue() << _shiftNumber;
    }
    
    private class RShiftGate : Gate
    {
        private readonly int _shiftNumber;

        public RShiftGate(int shiftNumber)
        {
            _shiftNumber = shiftNumber;
        }
        public override int OutputValue() => Inputs[0].GetOutputValue() >> _shiftNumber;
    }

    private class NotGate : Gate
    {
        public override int OutputValue() => 65535^Inputs[0].GetOutputValue();
    }
}