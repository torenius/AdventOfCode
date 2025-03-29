using System.Security.Cryptography;
using System.Text;
using AOC.Common;

namespace AOC2016._05;

public class Day05A : Day
{
    protected override object Run()
    {
        var input = "ojvtpuvg";
        var index = 0;
        var output = "";
        var hexToLetter = new Dictionary<byte, char>
        {
            {0, '0'},
            {1, '1'},
            {2, '2'},
            {3, '3'},
            {4, '4'},
            {5, '5'},
            {6, '6'},
            {7, '7'},
            {8, '8'},
            {9, '9'},
            {10, 'a'},
            {11, 'b'},
            {12, 'c'},
            {13, 'd'},
            {14, 'e'},
            {15, 'f'},
        };

        while (output.Length != 8)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input + index);
            var test = MD5.HashData(inputBytes);
            if (test[0] == 0 && test[1] == 0 && test[2] <= 15)
            {
                output += hexToLetter[test[2]];
            }

            index++;
        }
        return output;
    }
}