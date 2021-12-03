using System;
using System.IO;

namespace AOC2021._01
{
    public class Day01A : Day
    {
        public override void Run()
        {
            var input = GetInputAsInt();
            var count = 0;
            for (var i = 1; i < input.Length; i++)
            {
                if (input[i - 1] < input[i])
                {
                    count++;
                }
            }
            
            Console.WriteLine(count);
        }
    }
}
