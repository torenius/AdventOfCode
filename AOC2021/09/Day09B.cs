using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021._09
{
    public class Day09B : Day
    {
        private int[,] _matrix;
        private List<Basin> _basins;
        
        public override void Run()
        {
            _matrix = GetInputAsIntMatrix();

            _basins = new List<Basin>();
            for (var y = 0; y < _matrix.GetLength(0); y++)
            {
                for (var x = 0; x < _matrix.GetLength(1); x++)
                {
                    if (_matrix[y, x] == 9) continue;
                    
                    var b = new Basin();
                    _basins.Add(b);
                    AddBasin(b, y, x);
                }
            }

            var t = _basins.OrderByDescending(o => o.CordinationList.Count).Take(3);
            var sum = 1;
            foreach (var s in t)
            {
                sum *= s.CordinationList.Count;
            }
            Console.WriteLine($"sum = {sum}");
        }

        private void AddBasin(Basin b, int y, int x)
        {
            if (_matrix[y, x] == 9)
            {
                return;
            }

            if (_basins.Any(a => a.CordinationList.Contains((y, x))))
            {
                return;
            }
            
            b.CordinationList.Add((y, x));
            
            if (y - 1 >= 0)
            {
                AddBasin(b, y - 1, x);
            }

            if (y + 1 < _matrix.GetLength(0))
            {
                AddBasin(b, y + 1, x);
            }

            if (x - 1 >= 0)
            {
                AddBasin(b, y, x - 1);
            }
            
            if (x + 1 < _matrix.GetLength(1))
            {
                AddBasin(b, y, x + 1);
            }
        }
    }

    public class Basin
    {
        public Basin()
        {
            CordinationList = new List<(int y, int x)>();
        }
        public List<(int y, int x)> CordinationList { get; set; }
    }
}
