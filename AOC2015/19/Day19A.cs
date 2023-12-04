using System.Text.RegularExpressions;

namespace AOC2015._19;

public class Day19A : Day
{
    private Dictionary<string, Regex> _reg;
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var replacements = new List<Replacement>();
        foreach (var row in input)
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                break;
            }
            
            replacements.Add(new Replacement(row));
        }
        var molecule = input[^1];

        _reg = replacements.Select(x => x.From).Distinct()
            .ToDictionary(k => k, v => new Regex($".*?(?<mol>{v}).*?", RegexOptions.Compiled));

        var molecules = new List<string>();
        foreach (var replacement in replacements)
        {
            var reg = _reg[replacement.From];
            foreach (Match match in reg.Matches(molecule))
            {
                var m = match.Groups["mol"];
                //Console.WriteLine($"{replacement.From} => {replacement.To} = {m.Index} {m.Length}");
                
                molecules.Add(Splice(molecule, m.Index, m.Length, replacement.To));
            }
        }

        var distinctMoleculesAfterOneReplacement = molecules.Distinct().Count();

        return "" + distinctMoleculesAfterOneReplacement;
    }

    private static string Splice(string original, int start, int length, string replacement) =>
        string.Concat(original[..start], replacement, original[(start + length)..]);

    private class Replacement
    {
        public Replacement(string input)
        {
            var temp = input.Split(" => ");
            From = temp[0];
            To = temp[1];
        }

        public string From { get; set; }
        public string To { get; set; }
    }
}