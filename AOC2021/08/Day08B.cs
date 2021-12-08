using System;
using System.Linq;

namespace AOC2021._08
{
    public class Day08B : Day
    {
        public override void Run()
        {
            var entryList = GetInput().Select(s => new Entry(s)).ToList();

            var sum = 0;
            foreach (var e in entryList)
            {
                sum += e.OutputValue();
            }
            
            Console.WriteLine($"sum = {sum}");
        }
    }
}
