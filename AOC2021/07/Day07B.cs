using System;
using System.Linq;

namespace AOC2021._07
{
    public class Day07B : Day
    {
        public override void Run()
        {
            var input = GetInputIntArray();

            var s = input.Sum();
            var average = s / (input.Length * 1.0);
            var c = Math.Ceiling(average);
            var f = Math.Floor(average);

            var cCost = GetCost((int)c, input);
            var fCost = GetCost((int)f, input);

            Console.WriteLine($"average = {average} sum = {Math.Min(cCost, fCost)}");
        }

        private int GetCost(int average, int[] input)
        {
            var sum = 0;
            foreach (var i in input)
            {
                var cost = 1;
                var x = i;
                while (x != average)
                {
                    if (x < average)
                    {
                        x++;
                    }
                    else
                    {
                        x--;
                    }

                    sum += cost;
                    cost++;
                }
            }

            return sum;
        }
    }
}
