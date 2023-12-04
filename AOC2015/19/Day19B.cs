using System.Text.RegularExpressions;

namespace AOC2015._19;

public class Day19B : Day
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
        var targetMolecule = input[^1];

        _reg = replacements.Select(x => x.To).Distinct()
            .ToDictionary(k => k, v => new Regex($".*?(?<mol>{v}).*?", RegexOptions.Compiled));

        var step = 0;
        var molecules = new HashSet<string> {targetMolecule};
        do
        {
            step++;
            molecules = GenerateMolecules(molecules, replacements);

            // Naive approach to always go for the shortest worked for my input, but might just been lucky
            molecules = molecules.OrderBy(x => x.Length).Take(1).ToHashSet();

        } while (!molecules.Contains("e"));

        return "" + step;
    }

    private HashSet<string> GenerateMolecules(HashSet<string> molecules, List<Replacement> replacements)
    {
        var result = new HashSet<string>();
        foreach (var molecule in molecules)
        {
            foreach (var replacement in replacements)
            {
                var reg = _reg[replacement.To];
                foreach (Match match in reg.Matches(molecule))
                {
                    var m = match.Groups["mol"];
                    result.Add(Splice(molecule, m.Index, m.Length, replacement.From));
                }
            }
        }

        return result;
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