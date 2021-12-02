using System;
using System.IO;

namespace AOC2021._01
{
    public class Task02
    {
        private string[] _input;
    
        public Task02()
        {
            _input = File.ReadAllLines(@"C:\project\AdventOfCode\AOC2021\01\input.txt");
        }

        public void Run()
        {
            var count = 0;
            for (var i = 0; i < _input.Length - 3; i++)
            {
                var a = int.Parse(_input[i]) + int.Parse(_input[i + 1]) + int.Parse(_input[i + 2]);
                var b = int.Parse(_input[i + 1]) + int.Parse(_input[i + 2]) + int.Parse(_input[i + 3]);

                if (a < b)
                {
                    count++;
                }
            }
        
            Console.WriteLine(count);
        }
    }
}
