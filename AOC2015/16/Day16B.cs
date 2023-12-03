namespace AOC2015._16;

public class Day16B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var sues = input.Select(x => new Sue(x)).ToList();

        var tickerTape = new Dictionary<string, int>()
        {
            {"children", 3},
            {"cats", 7},
            {"samoyeds", 2},
            {"pomeranians", 3},
            {"akitas", 0},
            {"vizslas", 0},
            {"goldfish", 5},
            {"trees", 3},
            {"cars", 2},
            {"perfumes", 1},
        };

        var possibleSues = new List<Sue>();
        foreach (var sue in sues)
        {
            var missMatch = false;
            foreach (var thing in sue.Things)
            {
                var t = tickerTape[thing.Key];
                if (thing.Key is "cats" or "trees")
                {
                    if (t >= thing.Value)
                    {
                        missMatch = true;
                        break;
                    }
                }
                else if (thing.Key is "pomeranians" or "goldfish")
                {
                    if (t <= thing.Value)
                    {
                        missMatch = true;
                        break;
                    }
                }
                else if (t != thing.Value)
                {
                    missMatch = true;
                    break;
                }
            }

            if (!missMatch)
            {
                possibleSues.Add(sue);
            }
        }

        return "" + possibleSues.Single().Number;
    }

    private class Sue
    {
        public Sue(string input)
        {
            var temp = input.Split(": ");
            Number = temp[0][3..].ToInt();
         
            var firstColon = input.IndexOf(':');
            temp = input[(firstColon+2)..].Split(", ");
            foreach (var thing in temp)
            {
                var t = thing.Split(": ");
                Things.Add(t[0], t[1].ToInt());
            }
        }

        public int Number { get; set; }
        public Dictionary<string, int> Things { get; set; } = new();
    }
}