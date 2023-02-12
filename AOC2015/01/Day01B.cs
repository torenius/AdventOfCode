namespace AOC2015._01;

public class Day01B : Day
{
    protected override string Run()
    {
        var input = GetInputAsString();

        var floor = 0;

        for(var i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                floor++;
            }
            else
            {
                floor--;
            }

            if (floor == -1)
            {
                return "" + (i + 1);
            }
        }
        
        return "" + floor;
    }
}