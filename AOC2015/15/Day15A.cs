namespace AOC2015._15;

public class Day15A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var ingredients = input.Select(x => new Ingredient(x)).ToList();

        var maxScore = 0;
        var permutations = MakePermutations(ingredients.Count);
        var perm100 = permutations.Where(x => x.Sum() == 100).ToList();
        foreach (var perm in perm100)
        {
            var capacity = 0;
            var durability = 0;
            var flavour = 0;
            var texture = 0;
            for (var i = 0; i < perm.Length; i++)
            {
                var ing = ingredients[i];
                var p = perm[i];
                capacity += p * ing.Capacity; 
                durability += p * ing.Durability; 
                flavour += p * ing.Flavor; 
                texture += p * ing.Texture;
                //Console.Write($"{ing.Name} {p} ");
            }

            capacity = capacity < 0 ? 0 : capacity;
            durability = durability < 0 ? 0 : durability;
            flavour = flavour < 0 ? 0 : flavour;
            texture = texture < 0 ? 0 : texture;

            var score = capacity * durability * flavour * texture;
            //Console.WriteLine(score);

            if (score > maxScore)
            {
                maxScore = score;
            }
        }
        
        return "" + maxScore;
    }

    private List<int[]> MakePermutations(int arrayLength)
    {
        var start = new List<int[]>();
        for (var i = 0; i <= 100; i++)
        {
            var n = new int[1];
            n[0] = i;
            start.Add(n);
        }

        var numbersToAdd = Enumerable.Range(0, 101).ToArray();
        var current = MakePermutations(start, numbersToAdd);
        for (var i = 2; i < arrayLength; i++)
        {
            current = MakePermutations(current, numbersToAdd);
        }

        return current;
    }

    private List<int[]> MakePermutations(List<int[]> current, int[] numbersToAdd)
    {
        var result = new List<int[]>(current.Count * numbersToAdd.Length);
        foreach (var c in current)
        {
            foreach (var number in numbersToAdd)
            {
                var newList = new int[c.Length + 1];
                var sum = number;
                for (var i = 0; i < c.Length; i++)
                {
                    newList[i] = c[i];
                    sum += c[i];
                }

                if (sum <= 100)
                {
                    newList[^1] = number;
                    result.Add(newList);
                }
            }
        }

        return result;
    }

    private class Ingredient
    {
        public Ingredient(string input)
        {
            var temp = input.Split(": ");
            Name = temp[0];
            temp = temp[1].Split(", ");
            Capacity = temp[0].Split(" ")[1].ToInt();
            Durability = temp[1].Split(" ")[1].ToInt();
            Flavor = temp[2].Split(" ")[1].ToInt();
            Texture = temp[3].Split(" ")[1].ToInt();
            Calories = temp[4].Split(" ")[1].ToInt();
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Durability { get; set; }
        public int Flavor { get; set; }
        public int Texture { get; set; }
        public int Calories { get; set; }
    }
}