using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AOC2021._20
{
    public class Day20B : Day
    {
        public override void Run()
        {
            var input = GetInput();
            var enhance = input[0].ToCharArray();
            var space = new Dictionary<(int x, int y), char>();
            for (var y = 2; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    space.Add((x, y - 2), input[y][x]);
                }
            }
            
            //Print(space);
            Console.WriteLine();

            for (var loop = 0; loop < 50; loop++)
            {
                var maxX = space.Max(m => m.Key.x);
                var maxy = space.Max(m => m.Key.y);

                var minX = space.Min(m => m.Key.x);
                var minY = space.Min(m => m.Key.y);

                var spaceEnhanced = new Dictionary<(int x, int y), char>();
                for (var y = minY - 1; y <= maxy + 1; y++)
                {
                    for (var x = minX - 1; x <= maxX + 1; x++)
                    {
                        var s = Enhancement(x, y, space, loop % 2 == 0 ? '.' : '#');
                        s = s.Replace('.', '0');
                        s = s.Replace('#', '1');
                        var i = Convert.ToInt32(s, 2);

                        var c = enhance[i];
                        spaceEnhanced.Add((x, y), c);
                    }
                }

                //Print(spaceEnhanced);
                space = spaceEnhanced;
            }

            var litCount = space.Where(w => w.Value == '#').Count();
            Console.WriteLine($"LitCount = {litCount}");
        }

        private void Print(IReadOnlyDictionary<(int x, int y), char> space)
        {
            var maxX = space.Max(m => m.Key.x);
            var maxy = space.Max(m => m.Key.y);
            
            var minX = space.Min(m => m.Key.x);
            var minY = space.Min(m => m.Key.y);
            
            for (var y = minY; y <= maxy; y++)
            {
                for (var x = minX; x <= maxX; x++)
                {
                    Console.Write(space[(x, y)]);
                }
                Console.WriteLine();
            }
        }

        private string Enhancement(int x, int y, IReadOnlyDictionary<(int x, int y), char> space, char lit)
        {
            var location = new (int x, int y)[]
            {
                (x - 1, y - 1),
                (x, y - 1),
                (x + 1, y - 1),
                (x - 1, y),
                (x, y),
                (x + 1, y),
                (x - 1, y + 1),
                (x, y + 1),
                (x + 1, y + 1)
            };

            var sb = new StringBuilder();

            foreach (var l in location)
            {
                if (space.TryGetValue(l, out var c))
                {
                    sb.Append(c);
                }
                else
                {
                    sb.Append(lit);
                }
            }

            return sb.ToString();
        }
    }
}