using System;

namespace AOC2021._07
{
    public class Day07A : Day
    {
        public override void Run()
        {
            var input = GetInputIntArray();
            Array.Sort(input);

            var median = input[input.Length / 2];
            if (input.Length / 2 != input.Length / 2.0)
            {
                median = (median + input[(input.Length / 2) + 1]) / 2;
            }

            var sum = 0;
            foreach (var i in input)
            {
                if (i < median)
                {
                    sum += median - i;
                }
                else
                {
                    sum += i - median;
                }
            }
            
            Console.WriteLine($"median = {median} sum = {sum}");
        }
    }
}
