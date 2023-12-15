namespace AOC2023._15;

public class Day15A : Day
{
    protected override object Run()
    {
        var input = GetInputAsString().TrimEnd('\n').Split(",");

        var sum = 0;
        foreach (var s in input)
        {
            sum += Hash(s);
        }
        
        return sum;
    }

    private static int Hash(string input)
    {
        var value = 0;
        foreach (var c in input.ToCharArray())
        {
            value += c;
            value *= 17;
            value %= 256;
        }

        return value;
    }
}
