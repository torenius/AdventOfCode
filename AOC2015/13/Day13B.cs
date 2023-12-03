namespace AOC2015._13;

public class Day13B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var data = input.Select(x => new Data(x)).ToList();

        var people = data.Select(x => x.Name).Distinct().ToList();
        foreach (var person in people)
        {
            data.Add(new Data
            {
                Name = "Me",
                Happiness = 0,
                SittingNextTo = person
            });
            
            data.Add(new Data
            {
                Name = person,
                Happiness = 0,
                SittingNextTo = "Me"
            });
        }
        people.Add("Me");
        
        var combinations = Helper.GetAllCombinations(people);
        var mostHappiness = 0;
        foreach (var combo in combinations)
        {
            var first = combo[0];
            var last = combo[^1];
            var sum = Happiness(data, first, last);
            for (var i = 0; i < combo.Count - 1; i++)
            {
                sum += Happiness(data, combo[i], combo[i + 1]);
            }
            //Console.WriteLine($"{string.Join(",", combo)} {sum}");

            if (sum > mostHappiness)
            {
                mostHappiness = sum;
            }
        }

        return "" + mostHappiness;
    }

    private int Happiness(List<Data> data, string personA, string personB)
    {
        var scoreA = data.First(x => x.Name == personA && x.SittingNextTo == personB);
        var scoreB = data.First(x => x.Name == personB && x.SittingNextTo == personA);
        return scoreA.Happiness + scoreB.Happiness;
    }

    private class Data
    {
        public Data()
        {
            
        }
        public Data(string input)
        {
            var temp = input.Split(" ");
            Name = temp[0];
            Happiness = temp[3].ToInt();
            if (temp[2] == "lose")
            {
                Happiness = -1 * Happiness;
            }

            SittingNextTo = temp[10].TrimEnd('.');
        }

        public string Name { get; set; }
        public int Happiness { get; set; }
        public string SittingNextTo { get; set; }
    }
}