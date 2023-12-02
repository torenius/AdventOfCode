using System.Text;

namespace AOC2015._10;

public class Day10A : Day
{
    protected override string Run()
    {
        var input = "1321131112";

        for (var i = 0; i < 40; i++)
        {
            input = LookAndSay(input);
            //Console.WriteLine($"{i}: {input}");    
        }
        
        return input.Length.ToString();
    }

    private string LookAndSay(string input)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            var count = 1;
            var c = input[i];
            for (var k = i + 1; k < input.Length; k++)
            {
                if (input[k] != c)
                {
                    break;
                }

                count++;
            }

            sb.Append(count).Append(c);
            i += count - 1;
        }

        return sb.ToString();
    }
}