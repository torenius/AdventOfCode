using AOC.Common;

namespace AOC2025._06;

public class Day06B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var problems = new List<MathProblem>();
        var lastRow = input[^1];
        var length = 0;
        for (var i = lastRow.Length - 1; i >= 0; i--)
        {
            length++;
            if (lastRow[i] == '*' || lastRow[i] == '+')
            {
                problems.Add(new MathProblem
                {
                    Operation = lastRow[i],
                    Offset = i,
                    Length = length
                });
                length = 0;
                i--;
            }
        }

        var numbers = input[..^1];
        foreach (var problem in problems)
        {
            problem.ExtractNumbers(numbers);
        }
        
        return problems.Sum(p => p.Solution());
    }

    private class MathProblem
    {
        public char Operation { get; set; }
        public int Offset { get; set; }
        public int Length { get; set; }

        public List<int> Numbers { get; set; } = [];

        public void ExtractNumbers(string[] numbers)
        {
            for (var i = Length - 1; i >= 0; i--)
            {
                var number = "";
                foreach (var row in numbers)
                {
                    number += row[i + Offset];
                }
                Numbers.Add(int.Parse(number));
            }
        }

        public long Solution()
        {
            if (Operation == '+')
            {
                return Numbers.Sum();
            }

            long result = Numbers[0];
            for (var i = 1; i < Length; i++)
            {
                result *= Numbers[i];
            }
            
            return result;
        }
    }
}