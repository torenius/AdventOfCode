using System;

namespace AOC2021._09
{
    public class Day09A : Day
    {
        private int[,] _matrix;
        public override void Run()
        {
            _matrix = GetInputAsIntMatrix();

            var sum = 0;
            for (var y = 0; y < _matrix.GetLength(0); y++)
            {
                for (var x = 0; x < _matrix.GetLength(1); x++)
                {
                    if (IsLowest(y, x))
                    {
                        sum += _matrix[y, x] + 1;
                    }
                }
            }
            
            Console.WriteLine($"sum = {sum}");
        }

        private bool IsLowest(int y, int x)
        {
            if (y - 1 >= 0)
            {
                if (_matrix[y, x] >= _matrix[y - 1, x])
                {
                    return false;
                }
            }

            if (y + 1 < _matrix.GetLength(0))
            {
                if (_matrix[y, x] >= _matrix[y + 1, x])
                {
                    return false;
                }
            }

            if (x - 1 >= 0)
            {
                if (_matrix[y, x] >= _matrix[y, x - 1])
                {
                    return false;
                }
            }
            
            if (x + 1 < _matrix.GetLength(1))
            {
                if (_matrix[y, x] >= _matrix[y, x + 1])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
