using AOC.Common;

namespace AOC2025._02;

public class Day02A : Day
{
    protected override object Run()
    {
        var inputs = GetInputAsString().Split(',');
        var ranges = inputs.Select(x => new IdRange(x)).ToList();
        
        return ranges.SelectMany(r => r.InvalidValues).Sum();
    }

    private class IdRange
    {
        public IdRange(string input)
        {
            var range = input.Split('-');
            From = range[0].ToLong();
            To = range[1].ToLong();
            
            InvalidValues = [];
            for (var i = From; i <= To; i++)
            {
                var value = i.ToString();
                if (value.Length % 2 == 1) continue;

                var middle = value.Length / 2;
                
                var a = value[..middle].ToLong();
                var b = value[middle..].ToLong();

                if (a == b)
                {
                    InvalidValues.Add(i);
                }
            }
        }

        public long From { get; set; }
        public long To { get; set; }
        public List<long> InvalidValues { get; set; }
    }
}