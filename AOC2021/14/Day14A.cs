using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC2021._14
{
    public class Day14A : Day
    {
        public override void Run()
        {
            var input = GetInputAsString();
            input = input.Replace("\r\n", "\n");
            var s = input.Split("\n\n");

            var pair = new Dictionary<string, string>();
            foreach (var p in s[1].Split("\n", StringSplitOptions.RemoveEmptyEntries))
            {
                var c = p.Split(" -> ");
                pair.Add(c[0], c[1]);
            }

            var polymer = s[0];
            for (var i = 0; i < 10; i++)
            {
                polymer = ProcesString(polymer, pair);
                //Console.WriteLine(polymer);
            }

            var result = GetCommon(polymer);
            var max = result.Values.Max();
            var min = result.Values.Min();
            
            Console.WriteLine($"{max} - {min} = {max - min}");
        }

        private Dictionary<char, int> GetCommon(string s)
        {
            var common = new Dictionary<char, int>();
            foreach (var c in s)
            {
                if (common.ContainsKey(c))
                {
                    common[c]++;
                }
                else
                {
                    common.Add(c, 1);
                }
            }

            return common;
        }

        private string ProcesString(string s, Dictionary<string, string> template)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < s.Length - 1; i++)
            {
                sb.Append(s[i]);
                if (template.TryGetValue(s[i].ToString() + s[i + 1], out var t))
                {
                    sb.Append(t);
                }
            }

            sb.Append(s[^1]);

            return sb.ToString();
        }
    }
}
