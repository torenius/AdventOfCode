using System.Collections.Generic;
using System.Linq;

namespace AOC2021._04
{
    public class Bingo
    {
        
        public Bingo(string[] input)
        {
            var numbers = input[0].Split(",");
            Numbers = numbers.Select(int.Parse).ToArray();

            Boards = new List<Board>();
            for (var i = 2; i < input.Length; i++)
            {
                var b = new Board();
                var k = 0;
                while (k < 5)
                {
                    b.AddRow(input[i]);
                    i++;
                    k++;
                }
                Boards.Add(b);
            }
        }

        public int[] Numbers { get; set; }
        public List<Board> Boards { get; set; }
    }
    
    public class Board
    {
        private int _index;
        public Board()
        {
            Position = new int[5, 5];
            Marked = new bool[5, 5];
            _index = 0;
        }
        
        public int[,] Position { get; set; }
        public bool[,] Marked { get; set; }
        public bool HaveBingo { get; set; }
        public void AddRow(string s)
        {
            s = s.Trim().Replace("  ", " ");
            var numbers = s.Split(" ");
            for (var i = 0; i < numbers.Length; i++)
            {
                Position[_index, i] = int.Parse(numbers[i]);
            }
            _index++;
        }

        public bool MarkNumber(int number)
        {
            for (var i = 0; i < Position.GetLength(0); i++)
            {
                for (var k = 0; k < Position.GetLength(1); k++)
                {
                    if (Position[i, k] == number)
                    {
                        Marked[i, k] = true;
                        return CheckIfBingo(i, k);
                    }
                }
            }

            return false;
        }

        private bool CheckIfBingo(int row, int column)
        {
            // Check if row is bingo
            for (var i = 0; i < Position.GetLength(0); i++)
            {
                if (!Marked[row, i])
                {
                    goto column;
                }
            }

            HaveBingo = true;
            return true;
            
            column:
            // Check if column is bingo
            for (var i = 0; i < Position.GetLength(1); i++)
            {
                if (!Marked[i, column])
                {
                    return false;
                }
            }

            HaveBingo = true;
            return true;
        }

        public int GetSumOfUnmarked()
        {
            var sum = 0;
            for (var i = 0; i < Position.GetLength(0); i++)
            {
                for (var k = 0; k < Position.GetLength(1); k++)
                {
                    if (!Marked[i, k])
                    {
                        sum += Position[i, k];
                    }
                }
            }

            return sum;
        }
    }
}
