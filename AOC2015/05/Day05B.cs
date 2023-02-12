namespace AOC2015._05;

public class Day05B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        Test("qjhvhtzxzqqjkmpb");
        Test("xxyxx");
        Test("uurcxstgmygtbstg");
        Test("ieodomkazucvgmuy");

        var areNice = input.Count(IsNice);

        return "" + areNice;
    }

    private static void Test(string input)
    {
        Console.WriteLine(input + ": " + IsNice(input));
    }

    private static bool IsNice(string input)
    {
        var doRepeat = false;

        for (var i = 0; i < input.Length - 2; i++)
        {
            var a = input[i];
            var b = input[i + 2];
            
            if (a == b)
            {
                doRepeat = true;
            }
        }

        if (!doRepeat)
        {
            return false;
        }

        for (var i = 0; i < input.Length - 3; i++)
        {
            var a = input[i];
            var b = input[i + 1];
            for (var k = i + 2; k < input.Length - 1; k++)
            {
                var c = input[k];
                var d = input[k + 1];

                if (a == c && b == d)
                {
                    return true;
                }
            }
        }

        return false;
    }
}