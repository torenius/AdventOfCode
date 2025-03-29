using AOC.Common;

namespace AOC2016._07;

public class Day07A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var sum = 0;
        for (var i = 0; i < input.Length; i++)
        {
            var insideBrackets = false;
            var foundTls = false;
            for (var k = 0; k < input[i].Length - 3; k++)
            {
                if (input[i][k] == '[')
                {
                    insideBrackets = true;
                }
                else if (input[i][k] == ']')
                {
                    insideBrackets = false;
                }
                else
                {
                    if (input[i][k] == input[i][k + 3]
                        && input[i][k + 1] == input[i][k + 2]
                        && input[i][k] != input[i][k + 1])
                    {
                        foundTls = true;
                        if (insideBrackets)
                        {
                            break;    
                        }
                    }
                }
            }

            if (!insideBrackets && foundTls)
            {
                sum++;
            }
        }
        
        return sum;
    }
}