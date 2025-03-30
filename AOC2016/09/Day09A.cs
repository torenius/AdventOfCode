using System.Text;
using AOC.Common;

namespace AOC2016._09;

public class Day09A : Day
{
    protected override object Run()
    {
        var input = GetInputAsString().Trim().Trim(Environment.NewLine.ToCharArray());
        // input = "ADVENT";
        // input = "A(1x5)BC";
        // input = "(3x3)XYZ";
        // input = "A(2x2)BCD(2x2)EFG";
        // input = "(6x1)(1x3)A";
        // input = "X(8x2)(3x3)ABCY";
        // input = "X(4x2)ABCD(3x2)XYZ";

        var sb = new StringBuilder();
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

                while (repeat > 0)
                {
                    sb.Append(input[i..(i + take)]);
                    repeat--;
                }
                
                i += take;
                if (i < input.Length && input[i] == '(')
                {
                    i--;
                    continue;
                }
            }

            if (i < input.Length)
            {
                sb.Append(input[i]);    
            }
        }

        
        var result = sb.ToString();
        Console.WriteLine(result);
        return result.Length;
    }
}