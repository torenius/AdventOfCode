using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021._05
{
    public class Day05A : Day
    {
        private Dictionary<(int x, int y), int> d;
        public override void Run()
        {
            d = new Dictionary<(int x, int y), int>();

            foreach (var s in GetInput())
            {
                var a = s.Split(" -> ");
                var b = a[0].Split(",").Select(int.Parse).ToArray();
                var c = a[1].Split(",").Select(int.Parse).ToArray();

                if (b[0] != c[0] && b[1] != c[1]) continue;
                AddLine(b[0], b[1], c[0], c[1]);
            }

            var sum = 0;
            foreach (var x in d.Values)
            {
                if (x > 1)
                {
                    sum++;
                }
            }
            Console.WriteLine($"sum = {sum}");
        }

        private void AddLine(int xf, int yf, int xt, int yt)
        {
            var xi = xf <= xt ? 1 : -1;
            var yi = yf <= yt ? 1 : -1;

            for (var x = xf; x != xt + xi; x += xi)
            {
                for(var y = yf; y != yt + yi; y += yi)
                {
                    AddCordinate(x, y);
                }
            }
        }
        
        private void AddCordinate(int x, int y)
        {
            if (d.TryGetValue((x, y), out int value))
            {
                d[(x, y)]++;
            }
            else
            {
                d.Add((x,y), 1);
            }
        }
    }
}
