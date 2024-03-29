﻿using System;
using System.Collections.Generic;

namespace AOC2021._03
{
    public class Day03B : Day
    {
        public override void Run()
        {
            var input = GetInput();

            var oxygen = Convert.ToInt32(GetHalf(input, true)[0], 2);
            var CO2 = Convert.ToInt32(GetHalf(input, false)[0], 2);
            
            Console.WriteLine($"Oxygen: {oxygen} * CO2: {CO2} = {oxygen * CO2}");
        }

        private string[] GetHalf(string[] input, bool keepCommon)
        {
            var pos = 0;
            while (true)
            {
                if (input.Length == 1)
                {
                    return input;
                }
                
                var one = new List<string>();
                var zero = new List<string>();
                foreach (var s in input)
                {
                    if (s[pos] == '1')
                    {
                        one.Add(s);
                    }
                    else
                    {
                        zero.Add(s);
                    }
                }

                if (keepCommon)
                {
                    input = one.Count >= zero.Count ? one.ToArray() : zero.ToArray();
                }
                else
                {
                    input = one.Count >= zero.Count ? zero.ToArray() : one.ToArray();
                }

                pos++;
            }
        }
    }
}
