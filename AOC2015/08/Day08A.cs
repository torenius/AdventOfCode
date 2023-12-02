namespace AOC2015._08;

public class Day08A : Day
{
    protected override string Run()
    {
        var inputs = GetInputAsStringArray();

        var sumStringChar = 0;
        var sumMemoryChar = 0;
        foreach (var input in inputs)
        {
            var row = new Row(input);
            sumStringChar += row.StringLength;
            sumMemoryChar += row.MemoryLength;
        }
        
        return "" + (sumStringChar - sumMemoryChar);
    }

    private class Row
    {
        public Row(string input)
        {
            StringLength = input.Length;

            for (var i = 1; i < input.Length; i++)
            {
                var temp = input[i];
                if (input[i] == '"')
                {
                    continue;
                }
                else if (input[i] != '\\')
                {
                    MemoryLength++;
                    continue;
                }

                // intput[i] will be \
                
                if (input[i + 1] == '\\')
                {
                    MemoryLength++;
                    i += 1; // Jump past second \
                }
                else if (input[i + 1] == '"')
                {
                    MemoryLength++;
                    i += 1; // Jump past "
                }
                else if (input[i + 1] == 'x')
                {
                    MemoryLength++;
                    i += 3; // Jump past ascii
                }
            }
            
            Console.WriteLine($"{input} Length: {StringLength} Memory: {MemoryLength}");
        }

        public int StringLength { get; set; }
        public int MemoryLength { get; set; }
    }
}