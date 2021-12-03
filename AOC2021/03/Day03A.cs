using System;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;

namespace AOC2021._03
{
    public class Day03A : Day
    {
        public override void Run()
        {
            var input = GetInput();
            var numberOfRows = new int[input[0].Length];
            foreach (var s in input)
            {
               var c = s.ToCharArray();
               for (int i = 0; i < c.Length; i++)
               {
                   numberOfRows[i] += c[i] == '1' ? 1 : 0;
               }
            }

            var gamma = "";
            var epsilon = "";

            for (int i = 0; i < numberOfRows.Length; i++)
            {
                if (numberOfRows[i] > input.Length / 2)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }
            }

            int g = Convert.ToInt32(gamma, 2);
            var e = Convert.ToInt32(epsilon, 2);
            
            Console.WriteLine($"gamma: {g} * epsilon: {e} = {g * e}");
        }
    }
}
