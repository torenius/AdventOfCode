namespace AOC2015._01;

public class Day01A : Day
{
    protected override string Run()
    {
        var input = GetInputAsString();

        var floor = 0;

        foreach (var f in input)
        {
            if (f == '(')
            {
                floor++;
            }
            else
            {
                floor--;
            }
        }
        
        return "" + floor;
    }
}