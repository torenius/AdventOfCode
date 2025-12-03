using AOC.Common;

namespace AOC2025._03;

public class Day03B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        long sum = 0;
        foreach (var row in input)
        {
            var joltage = HighestVoltage(row, 12);
            Console.WriteLine($"{row} : {joltage}");
            sum += joltage;
        }
        
        return sum;
    }

    private static long HighestVoltage(string input, int numberLength)
    {
        var numbers = "";
        var from = 0;
        for (var i = 1; i <= numberLength; i++)
        {
            var to = input.Length - numberLength + i;
            var (maxIndex, maxNumber) = HighestValue(input, from, to);
            from = maxIndex + 1;
            numbers += maxNumber;
        }
        
        return numbers.ToLong();
    }

    private static (int maxIndex, int maxNumber) HighestValue(string input, int from, int to)
    {
        var max = input[from].ToInt();
        var maxI = from;
        for (var i = from; i < to; i++)
        {
            var number = input[i].ToInt();
            if (number > max)
            {
                max = number;
                maxI = i;
            }
        }

        return (maxI, max);
    }
}