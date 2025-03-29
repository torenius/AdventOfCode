using AOC.Common;

namespace AOC2024._22;

public class Day22A : Day
{
    protected override object Run()
    {
        // Console.WriteLine(42^15);
        // Console.WriteLine(100000000%16777216);
        
        var input = GetInputAsStringArray();

        long sum = 0;
        foreach (var secretNumber in input)
        {
            var rand = new Randomizer(secretNumber.ToInt());
            for (var i = 0; i < 2000; i++)
            {
                rand.Next();
            }

            sum += rand.CurrentValue;
        }

        return sum;
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