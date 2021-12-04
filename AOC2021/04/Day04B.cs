using System;
using System.Linq;

namespace AOC2021._04
{
    public class Day04B : Day
    {
        public override void Run()
        {
            var bingo = new Bingo(GetInput());

            int sumOfLastWinner = 0;
            int lastWinningNumber = 0;

            foreach (var n in bingo.Numbers)
            {
                foreach (var b in bingo.Boards.Where(w=> !w.HaveBingo))
                {
                    if (b.MarkNumber(n))
                    {
                        sumOfLastWinner = b.GetSumOfUnmarked();;
                        lastWinningNumber = n;
                    }
                }
            }
            
            Console.WriteLine($"Number: {lastWinningNumber}");
            Console.WriteLine($"Sum: {sumOfLastWinner}");
            Console.WriteLine($"n * sum = : {lastWinningNumber * sumOfLastWinner}");
        }
    }
}
