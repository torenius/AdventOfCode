namespace AOC2015._12;

public class Day12A : Day
{
    protected override string Run()
    {
        var input = GetInputAsString();
        var sum = 0;
        var lastWasNumber = false;
        var temp = "";
        for(var i = 0; i < input.Length; i++)
        {
            var c = input[i];
            if (char.IsNumber(c))
            {
                temp += c;
                if (!lastWasNumber && input[i-1] == '-')
                {
                    temp = "-" + temp;
                }
                lastWasNumber = true;
            }
            else if (lastWasNumber)
            {
                sum += temp.ToInt();
                temp = "";
                lastWasNumber = false;
            }
        }
        
        return sum.ToString();
    }
}