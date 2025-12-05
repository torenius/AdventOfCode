using AOC.Common;

namespace AOC2025._05;

public class Day05A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var ranges = new List<Range>();
        var ingredients = new List<long>();
        var isRanges = true;
        foreach (var row in input)
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                isRanges = false;
                continue;
            }

            if (isRanges)
            {
                ranges.Add(new Range(row));
            }
            else
            {
                ingredients.Add(row.ToLong());
            }
        }

        var fresh = 0;
        foreach (var ingredient in ingredients)
        {
            foreach (var range in ranges)
            {
                if (range.IsInRange(ingredient))
                {
                    fresh++;
                    break;
                }
            }
        }
        
        return fresh;
    }

    private class Range
    {
        public Range(string input)
        {
            var range = input.Split('-').ToLongArray();
            From = range[0];
            To = range[1];
        }

        public long From { get; set; }
        public long To { get; set; }

        public bool IsInRange(long value) => value >= From && value <= To;
    }
}
