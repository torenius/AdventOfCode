using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021
{
    public abstract class Day
    {
        protected Day()
        {
            Console.WriteLine(GetType().Name);    
        }
        
        private string GetBasePath()
        {
            const string basePath = @"C:\project\AdventOfCode\";
            var folder = GetType().Namespace.Replace("._", "\\");

            return Path.Combine(basePath, folder);
        }

        protected string GetInputAsString(string filename = "input.txt")
        {
            return File.ReadAllText(Path.Combine(GetBasePath(), filename));
        }

        protected string[] GetInput(string filename = "input.txt")
        {
            return File.ReadAllLines(Path.Combine(GetBasePath(), filename));
        }

        protected int[] GetInputAsInt(string filename = "input.txt")
        {
            return File.ReadLines(Path.Combine(GetBasePath(), filename)).Select(int.Parse).ToArray();
        }

        protected int[] GetInputIntArray(string filename = "input.txt", string separator = ",")
        {
            var input = GetInput(filename);
            return input[0].Split(separator).Select(int.Parse).ToArray();
        }

        protected int[,] GetInputAsIntMatrix(string filename = "input.txt")
        {
            var input = GetInput(filename);
            var matrix = new int[input.Length, input[0].Length];
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    matrix[y, x] = (int)char.GetNumericValue(input[y][x]);
                }
            }

            return matrix;
        }

        public abstract void Run();

    }
}
