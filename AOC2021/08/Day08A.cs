using System;
using System.Linq;

namespace AOC2021._08
{
    public class Day08A : Day
    {
        public override void Run()
        {
            var entryList = GetInput().Select(s => new Entry(s)).ToList();

            var sum = 0;
            foreach (var e in entryList)
            {
                foreach (var o in e.Output)
                {
                    if (o.Length is 2 or 3 or 4 or 7)
                    {
                        sum++;
                    }
                }
            }
            
            Console.WriteLine($"sum = {sum}");
        }
    }
}
