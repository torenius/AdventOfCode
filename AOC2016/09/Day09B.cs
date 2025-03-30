using System.Text;
using AOC.Common;

namespace AOC2016._09;

public class Day09B : Day
{
    protected override object Run()
    {
        var input = GetInputAsString().Trim().Trim(Environment.NewLine.ToCharArray());
        return Decompress(input);
    }

    private static long Decompress(string input)
    {
        long sum = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                i++;
                var takeChar = "";
                while (input[i] != 'x')
                {
                    takeChar += input[i];
                    i++;
                }

                i++;
                var numberOfRepeats = "";
                while (input[i] != ')')
                {
                    numberOfRepeats += input[i];
                    i++;
                }

                i++;
                var take = takeChar.ToInt();
                var repeat = numberOfRepeats.ToInt();

                var inner = Decompress(input[i..(i + take)]);
                sum += inner * repeat;
                
                i += take;
                if (i < input.Length && input[i] == '(')
                {
                    i--;
                    continue;
                }
            }

            if (i < input.Length)
            {
                sum++;
            }
        }
        
        return sum;
    }
}