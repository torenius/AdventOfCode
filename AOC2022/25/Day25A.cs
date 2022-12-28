namespace AOC2022._25;

public class Day25A : Day
{
    private Dictionary<long, string> _conversions = new ();
    protected override string Run()
    {
        _conversions.Add(1, "1");
        _conversions.Add(2, "2");
        _conversions.Add(3, "1=");
        _conversions.Add(4, "1-");
        _conversions.Add(5, "10");
        _conversions.Add(6, "11");
        _conversions.Add(7, "12");
        _conversions.Add(8, "2=");
        _conversions.Add(9, "2-");
        _conversions.Add(10, "20");
        _conversions.Add(15, "1=0");
        _conversions.Add(20, "1-0");
        _conversions.Add(2022, "1=11-2");
        _conversions.Add(12345, "1-0---0");
        _conversions.Add(314159265, "1121-1110-1=0");
        
        var test = GetDecimal("102");
        Console.WriteLine(test);
        test = GetDecimal("1=-");
        Console.WriteLine(test);

        TestSnafuPlus();
        
        var input = GetInputAsStringArray();
        
        var sum = input[0];
        for (var i = 1; i < input.Length; i++)
        {
            sum = SnafuPlus(sum, input[i]);
        }

        return sum;

        // long sum = 0;
        // foreach (var i in input)
        // {
        //     var decimalValue = GetDecimal(i);
        //     _conversions.TryAdd(decimalValue, i);
        //     
        //     sum += decimalValue;
        // }
        //
        // var test = GetDecimal("1122--");
        //
        // return GetSnafu(sum);
    }

    private string GetSnafu(long input)
    {
        var max = HighestPossibleValue(input);
        var snafu = max.Value;
        var valueLeft = input - max.Key;
        while (valueLeft != 0)
        {
            max = HighestPossibleValue(valueLeft);
            snafu = SnafuPlus(snafu, max.Value);
            valueLeft -= max.Key;
            _conversions.TryAdd(input - valueLeft, snafu);
        }

        return snafu;
    }

    private KeyValuePair<long, string> HighestPossibleValue(long input)
    {
        return _conversions.Where(c => c.Key <= input).MaxBy(c => c.Key);
    }

    private static string SnafuPlus(string a, string b)
    {
        // Make same length and add space for carry
        var maxLength = Math.Max(a.Length, b.Length);
        a = a.PadLeft(maxLength + 1, '0');
        b = b.PadLeft(maxLength + 1, '0');

        var sum = "";
        var carry = '0';
        for (var i = a.Length - 1; i >= 0; i--)
        {
            if (a[i] == '=' && b[i] == '=' && carry == '-')
            {
                
            }
            var temp = SnafuPlus(a[i], b[i], carry);
            sum = temp.Value + sum;
            carry = temp.Carry;
        }

        return sum.TrimStart('0');
    }

    private static (char Value, char Carry) SnafuPlus(char a, char b, char carry = '0')
    {
        var aDecimal = GetDecimalValue(a);
        var bDecimal = GetDecimalValue(b);
        var carryDecimal = GetDecimalValue(carry);

        var sum = aDecimal + bDecimal + carryDecimal;

        switch (sum)
        {
            case 5:
                return ('0', '1');
            case 4:
                return ('-', '1');
            case 3:
                return ('=', '1');
            case 2:
                return ('2', '0');
            case 1:
                return ('1', '0');
            case 0:
                return ('0', '0');
            case -1:
                return ('-', '0');
            case -2:
                return ('=', '0');
            case -3:
                return ('2', '-');
            case -4:
                return ('1', '-');
            case -5:
                return ('0', '-');
        }

        throw new NotSupportedException($"Could not sum {a} + {b} + {carry}");
    }
    
    private static void TestSnafuPlus()
    {
        var tests = new List<(string Expected, string A, string B)>
        {
            ("12", "1=", "1-"), // 12 = 3 + 4
            ("1=0", "10", "20"), // 15 = 5 + 10
            ("1=0", "12", "2="), // 15 = 7 + 8
            ("11", "1=", "1="), // 6 = 3 + 3
            ("2=", "1-", "1-"), // 8 = 4 + 4
            ("21", "1-", "12"), // 11 = 4 + 7
            ("2-", "1-", "10"), // 9 = 4 + 5
            ("1=-", "12", "12"), // 14 = 7 + 7
            ("22", "11", "11"), // 12 = 6 + 6
            ("102", "1==", "1=-"), // 27 = 13 + 14
        };

        foreach (var test in tests)
        {
            var result = SnafuPlus(test.A, test.B);
            Console.WriteLine($"{test.A} plus {test.B} Equal {result} Expected: {test.Expected} IsEqual: {result == test.Expected}");
        }
    }

    private static long GetDecimal(string input)
    {
        long sum = 0;
        var pos = 1;
        for (var i = input.Length - 1; i >= 0; i--)
        {
            var snafuValue = input[i];
            var decimalValue = GetDecimalValue(snafuValue) * pos;

            sum += decimalValue;

            pos *= 5;
        }
        
        return sum;
    }

    private static long GetDecimalValue(char snafuValue)
    {
        return snafuValue switch
        {
            '2' => 2,
            '1' => 1,
            '0' => 0,
            '-' => -1,
            '=' => -2,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}