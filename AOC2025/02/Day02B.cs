using AOC.Common;

namespace AOC2025._02;

public class Day02B : Day
{
    protected override object Run()
    {
        var inputs = GetInputAsString().Split(',');
        var ranges = inputs.Select(x => new IdRange(x)).ToList();
        
        //Console.WriteLine(string.Join(Environment.NewLine, ranges.Select(r => r.ToString())));
        
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
                var middle = value.Length / 2;

                for (var k = 0; k < middle; k++)
                {
                    var sequence = value[..(k + 1)];
                    if (k + sequence.Length > value.Length) continue;

                    var isInvalid = true;
                    for (var m = k + 1; m < value.Length; m += sequence.Length)
                    {
                        if (m + sequence.Length > value.Length)
                        {
                            isInvalid = false;
                            break;
                        }
                        
                        var compareAgainst = value[m..(m + sequence.Length)];
                        if (sequence != compareAgainst)
                        {
                            isInvalid = false;
                            break;
                        }
                    }
                    
                    if (isInvalid)
                    {
                        InvalidValues.Add(i);
                        break;
                    }
                }
            }
        }

        public long From { get; set; }
        public long To { get; set; }
        public List<long> InvalidValues { get; set; }
        
        public override string ToString() => $"{From}-{To} InvalidValues: {string.Join(",", InvalidValues)}";
    }
}