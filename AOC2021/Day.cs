using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021
{
    public abstract class Day
    {
        private string GetBasePath()
        {
            const string basePath = @"C:\project\AdventOfCode\";
            var folder = GetType().Namespace.Replace("._", "\\");

            return Path.Combine(basePath, folder);
        }
        
        public string[] GetInput(string filename = "input.txt")
        {
            return File.ReadAllLines(Path.Combine(GetBasePath(), filename));
        }

        public int[] GetInputAsInt(string filename = "input.txt")
        {
            return File.ReadLines(Path.Combine(GetBasePath(), filename)).Select(int.Parse).ToArray();
        }

        public abstract void Run();

    }
}
