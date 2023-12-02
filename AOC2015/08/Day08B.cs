using System.Net;

namespace AOC2015._08;

public class Day08B : Day
{
    protected override string Run()
    {
        var inputs = GetInputAsStringArray();

        var sumStringChar = 0;
        var sumEncodedChar = 0;
        foreach (var input in inputs)
        {
            var row = new Row(input);
            sumStringChar += row.StringLength;
            sumEncodedChar += row.EncodedLength;
        }
        
        return "" + (sumEncodedChar - sumStringChar);
    }

    private class Row
    {
        public Row(string input)
        {
            StringLength = input.Length;

            var newString = "" + '"';
            for (var i = 0; i < input.Length; i++)
            {
                var temp = input[i];
                newString += input[i] switch
                {
                    '"' => "\\" + '"',
                    '\\' => "\\" + "\\",
                    _ => input[i]
                };
            }

            newString += '"';

            Console.WriteLine($"{input} -> {newString}");
            EncodedLength = newString.Length;
        }

        public int StringLength { get; set; }
        public int EncodedLength { get; set; }
    }
}