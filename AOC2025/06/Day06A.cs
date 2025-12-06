using AOC.Common;

namespace AOC2025._06;

public class Day06A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        
        var numbers = new List<int[]>();
        for (var i = 0; i < input.Length - 1; i++)
        {
            var row = input[i].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToIntArray();
            numbers.Add(row);
        }
        
        var operations = input[^1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        long totalSum = 0;
        for (var i = 0; i < numbers[0].Length; i++)
        {
            long columnSum = numbers[0][i];
            for (var j = 1; j < numbers.Count; j++)
            {
                if (operations[i] == "*")
                {
                    columnSum *= numbers[j][i];
                }
                else
                {
                    columnSum += numbers[j][i];
                }
            }

            totalSum += columnSum;
        }
        
        return totalSum;
    }
}
