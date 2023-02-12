namespace AOC2015._05;

public class Day05A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        Test("ugknbfddgicrmopn");
        Test("aaa");
        Test("jchzalrnumimnmhp");
        Test("haegwjzuvuyypxyu");
        Test("dvszwmarrgswjxmb");

        var areNice = input.Count(IsNice);

        return "" + areNice;
    }

    private static void Test(string input)
    {
        Console.WriteLine(input + ": " + IsNice(input));
    }

    private static bool IsNice(string input)
    {
        var vowelCount = 0;
        var hasDoubleLetter = false;

        for (var i = 0; i < input.Length - 1; i++)
        {
            var a = input[i];
            var b = input[i + 1];

            if (BadStrings.Contains((a, b)))
            {
                return false;
            }

            if (a == b)
            {
                hasDoubleLetter = true;
            }

            if (Vowels.Contains(a))
            {
                vowelCount++;
            }
        }
        
        if (Vowels.Contains(input[^1]))
        {
            vowelCount++;
        }
        
        return vowelCount >= 3 && hasDoubleLetter;
    }
    
    private static readonly HashSet<char> Vowels = new HashSet<char>
    {
        'a', 'e', 'i', 'o', 'u'
    };
    
    private static readonly HashSet<(char a, char b)> BadStrings = new HashSet<(char a, char b)>
    {
        ('a', 'b'),
        ('c', 'd'),
        ('p', 'q'),
        ('x', 'y'),
    };
}