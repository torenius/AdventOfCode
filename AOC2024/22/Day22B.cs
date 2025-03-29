using AOC.Common;

namespace AOC2024._22;

public class Day22B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        
        var monkeys = new List<Monkey>();
        foreach (var secretNumber in input)
        {
            var rand = new Randomizer(secretNumber.ToInt());
            var monkey = new Monkey(rand.CurrentValue);
            
            for (var i = 0; i < 2000; i++)
            {
                rand.Next();
                monkey.AddNumber(rand.CurrentValue);
            }
            monkeys.Add(monkey);
        }
        
        var sequences = Helper.GetPossibleCombinations(4, [-9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);
        
        // Took 8.5 min, but feels more readable
        long maxBananas = 0;
        foreach (var sequence in sequences)
        {
            PrintElapsedTime(string.Join(',', sequence), TimeSpan.FromSeconds(5));
            
            var current = monkeys.AsParallel().Sum(m => m.GetBananas(sequence));
        
            if (current > maxBananas)
            {
                maxBananas = current;
            }
        }
        
        return maxBananas;
        
        // Took just below 8 minutes
        // var locker = new Lock();
        // var result = new Dictionary<string, long>();
        // Parallel.ForEach(sequences, sequence =>
        // {
        //     var key = string.Join(',', sequence);
        //     PrintElapsedTime(key, TimeSpan.FromSeconds(5));
        //     var bananaCount = monkeys.Sum(monkey => monkey.GetBananas(sequence));
        //
        //     lock (locker)
        //     {
        //         result.Add(key, bananaCount);
        //     }
        // });
        //
        // var mostBanans = result.OrderByDescending(x => x.Value).First();
        // Console.WriteLine($"{mostBanans.Key} with {mostBanans.Value} bananas");
        //
        // return mostBanans.Value;
    }

    private class Monkey(long startNumber)
    {
        private int CurrentNumber { get; set; } = (int)(startNumber % 10);
        
        private List<(int, int)> Prices { get; set; } = [];
        
        public void AddNumber(long number)
        {
            var singleDigit = (int)(number % 10);
            Prices.Add((singleDigit, singleDigit - CurrentNumber));
            CurrentNumber = singleDigit;
        }

        public int GetBananas(int[] sequence)
        {
            for (var i = 0; i < Prices.Count - 3; i ++)
            {
                if (sequence[0] == Prices[i].Item2 &&
                    sequence[1] == Prices[i + 1].Item2 &&
                    sequence[2] == Prices[i + 2].Item2 &&
                    sequence[3] == Prices[i + 3].Item2)
                {
                    return Prices[i + 3].Item1;
                }
            }

            return 0;
        }
    }

    private class Randomizer(int seed)
    {
        public long CurrentValue { get; private set; } = seed;

        public long Next()
        {
            var mul = CurrentValue * 64;
            MixAndPrune(mul);
            
            var divide = CurrentValue / 32;
            MixAndPrune(divide);

            var mul2 = CurrentValue * 2048;
            MixAndPrune(mul2);

            return CurrentValue;
        }

        private void MixAndPrune(long number)
        {
            CurrentValue ^= number; // Mix
            CurrentValue %= 16777216; // Prune
        }
    }
}