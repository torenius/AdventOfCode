using System;
using System.Collections.Generic;

namespace AOC2021._11
{
    public class Day11B : Day
    {
        private int[,] _matrix;
        private Dictionary<(int y, int x), Octopus> _octopuses;

        public override void Run()
        {
            _matrix = GetInputAsIntMatrix();
            _octopuses = new Dictionary<(int y, int x), Octopus>();
            
            for (var y = 0; y < _matrix.GetLength(0); y++)
            {
                for (var x = 0; x < _matrix.GetLength(1); x++)
                {
                    _octopuses.Add((y, x), new Octopus(y, x));
                }
            }

            var count = 0;
            while (!AllIsZero())
            {
                Increment();
                HandleFlash();
                Reset();
                count++;
            }
            
            Console.WriteLine($"count = {count}");
        }

        private bool AllIsZero()
        {
            for (var y = 0; y < _matrix.GetLength(0); y++)
            {
                for (var x = 0; x < _matrix.GetLength(1); x++)
                {
                    if (_matrix[y, x] != 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void Increment()
        {
            for (var y = 0; y < _matrix.GetLength(0); y++)
            {
                for (var x = 0; x < _matrix.GetLength(1); x++)
                {
                    _matrix[y, x]++;
                }
            }
        }

        private void Reset()
        {
            for (var y = 0; y < _matrix.GetLength(0); y++)
            {
                for (var x = 0; x < _matrix.GetLength(1); x++)
                {
                    if (_matrix[y, x] > 9)
                    {
                        _matrix[y, x] = 0;    
                    }
                    _octopuses[(y, x)].Flashed = false;
                }
            }
        }

        private void HandleFlash()
        {
            foreach (var kvp in _octopuses)
            {
                Flash(kvp.Value, false);
            }
        }

        private void Flash(Octopus oct, bool flased)
        {
            if(oct.Flashed) return;
            
            var x = oct.x;
            var y = oct.y;

            if (flased)
            {
                _matrix[y, x]++;
            }
            
            if(_matrix[y, x] < 10) return;
            
            oct.Flashed = true;
            oct.FlashCount++;

            
            var possibleLocation = new[]
            {
                (y - 1, x - 1),
                (y - 1, x),
                (y - 1, x + 1),
                (y, x - 1),
                (y, x + 1),
                (y + 1, x - 1),
                (y + 1, x),
                (y + 1, x + 1)
            };

            foreach (var p in possibleLocation)
            {
                if (_octopuses.TryGetValue(p, out var o))
                {
                    Flash(o, true);
                }
            }
        }
    }
}