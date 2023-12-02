namespace AOC2015._11;

public class Day11B : Day
{
    protected override string Run()
    {
        var input = "vzbxkghb".ToCharArray();
        
        do
        {
            Increment(input);
        } while (!IsValid(input));
        
        do
        {
            Increment(input);
        } while (!IsValid(input));

        return new string(input);
    }

    private static char[] Increment(char[] input)
    {
        for (var i = 7; i >= 0; i--)
        {
            var c = input[i];
            if (c != 'z')
            {
                input[i] = (char)(Convert.ToUInt16(c) + 1);
                break;                
            }

            input[i] = 'a';
        }
        
        return input;
    }

    private static bool IsValid(char[] input)
    {
        var hasIncreasingOfThree = false;
        var pairCount = 0;
        var lastPairStart = ' ';
        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] is 'i' or 'o' or 'l')
            {
                return false;
            }

            if (i < input.Length - 1 && input[i] == input[i + 1] && input[i] != lastPairStart)
            {
                pairCount++;
                lastPairStart = input[i];
            }

            if (i < input.Length - 2 && !hasIncreasingOfThree)
            {
                var a = Convert.ToUInt16(input[i]);
                var b = Convert.ToUInt16(input[i + 1]);
                var c = Convert.ToUInt16(input[i + 2]);
                if (a + 1 == b && b + 1 == c)
                {
                    hasIncreasingOfThree = true;
                }
            }
        }
        
        return hasIncreasingOfThree && pairCount >= 2;
    }
}