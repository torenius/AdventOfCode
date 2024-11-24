using System.Security.Cryptography;
using System.Text;

namespace AOC2016._05;

public class Day05B : Day
{
    protected override object Run()
    {
        var input = "ojvtpuvg";
        //input = "abc";
        var index = 0;
        var output = new char[8] { '-', '-', '-', '-', '-', '-', '-', '-' };

        var numbersFound = 0;
        while (numbersFound != 8)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input + index);
            var test = MD5.HashData(inputBytes);
            var value = BitConverter.ToString(test);
            if (value[..7] == "00-00-0")
            {
                var pos = (int)char.GetNumericValue(value[7]);
                if (pos is >= 0 and < 8 && output[pos] == '-')
                {
                    output[pos] = value[9];
                    numbersFound++;
                }
            }

            index++;
        }
        return new string(output).ToLower();
    }
}