namespace AOC2022;

public static class Extensions
{
    public static string[] SplitEveryN(this string input, int n)
    {
        var result = new List<string>();
        for (var i = 0; i < input.Length; i += n)
        {
            if (i + n < input.Length)
            {
                result.Add(input[i..(i + n)]);
            }
            else
            {
                result.Add(input[i..]);
            }
        }

        return result.ToArray();
    }

    public static int ToInt(this string input)
    {
        return int.Parse(input);
    }
    
    public static long ToLong(this string input)
    {
        return long.Parse(input);
    }
}
