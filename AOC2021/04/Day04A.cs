using System;

namespace AOC2021._04
{
    public class Day04A : Day
    {
        public override void Run()
        {
            var bingo = new Bingo(GetInput());

            foreach (var n in bingo.Numbers)
            {
                foreach (var b in bingo.Boards)
                {
                    if (b.MarkNumber(n))
                    {
                        var sum = b.GetSumOfUnmarked();
                        Console.WriteLine($"Number: {n}");
                        Console.WriteLine($"Sum: {sum}");
                        Console.WriteLine($"n * sum = : {n * sum}");
                        return;
                    }
                }
            }
        }
    }
}
