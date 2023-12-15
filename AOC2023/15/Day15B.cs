namespace AOC2023._15;

public class Day15B : Day
{
    protected override object Run()
    {
        var input = GetInputAsString().TrimEnd('\n').Split(",");

        var boxes = new List<Lens>[256];
        for (var i = 0; i < boxes.Length; i++)
        {
            boxes[i] = new List<Lens>();
        }
        
        foreach (var s in input)
        {
            var dashIndex = s.IndexOf('-');
            var equalIndex = s.IndexOf('=');
            var split = Math.Max(dashIndex, equalIndex);
            
            var label = s[..split];
            var hash = Hash(label);
            var operation = s[split..];

            if (operation == "-")
            {
                boxes[hash].RemoveAll(x => x.Label == label);
            }
            else
            {
                operation = operation[1..];
                var lens = boxes[hash].FirstOrDefault(x => x.Label == label);
                if (lens is not null)
                {
                    lens.FocalLength = operation.ToInt();
                }
                else
                {
                    boxes[hash].Add(new Lens
                    {
                        Label = label,
                        FocalLength = operation.ToInt() 
                    });
                }
            }
            
            Console.WriteLine($"s:{s} hash:{Hash(s[..split])} lens:{s[..split]}"); 
        }

        var sum = 0;
        for (var i = 0; i < boxes.Length; i++)
        {
            for (var k = 0; k < boxes[i].Count; k++)
            {
                sum += (i + 1) * (k + 1) * boxes[i][k].FocalLength;
            }
        }
        
        return sum;
    }

    private static int Hash(string input)
    {
        var value = 0;
        foreach (var c in input.ToCharArray())
        {
            value += c;
            value *= 17;
            value %= 256;
        }

        return value;
    }

    private class Lens
    {
        public string Label { get; set; }
        public int FocalLength { get; set; }
    }
}
