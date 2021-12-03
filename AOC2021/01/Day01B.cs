using System;
using System.IO;

namespace AOC2021._01
{
    public class Day01B : Day
    {
        public override void Run()
        {
            var input = GetInputAsInt();
            var count = 0;
            for (var i = 0; i < input.Length - 3; i++)
            {
                var a = input[i] + input[i + 1] + input[i + 2];
                var b = input[i + 1] + input[i + 2] + input[i + 3];

                if (a < b)
                {
                    count++;
                }
            }
        
            Console.WriteLine(count);
        }
    }
}
