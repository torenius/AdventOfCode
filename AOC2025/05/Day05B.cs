using AOC.Common;

namespace AOC2025._05;

public class Day05B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var ranges = new List<Range>();

        foreach (var row in input)
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                break;
            }

            ranges.Add(new Range(row));
        }
        
        var overlapping = ranges;
        do
        {
            var overlappingTemp = Merge(overlapping);
            overlapping = overlappingTemp.Distinct().ToList();
        } while (OverlappExists(overlapping));
        
        return overlapping.Sum(r => r.IncludedIds());
    }

    private static bool OverlappExists(List<Range> ranges)
    {
        foreach (var range in ranges)
        {
            var overlapp = ranges.Any(r => !r.Equals(range) && r.IsOverlap(range));
            if (overlapp) return true;
        }

        return false;
    }

    private static List<Range> Merge(List<Range> ranges)
    {
        var overlapping = new List<Range>();
        
        do
        {
            var range = ranges.First();
            var overlapps = ranges.Where(r => !r.Equals(range) && r.IsOverlap(range)).ToList();
            if (overlapps.Count > 0)
            {
                var from = Math.Min(overlapps.Min(r => r.From), range.From);
                var to = Math.Max(overlapps.Max(r => r.To), range.To);
                overlapping.Add(new Range(from, to));
                ranges.RemoveAll(r => overlapps.Contains(r));
            }
            else
            {
                overlapping.Add(range);
                ranges.Remove(range);
            }
        }while(ranges.Count != 0);
        
        return overlapping;
    }

    private class Range : IEquatable<Range>
    {
        public Range(string input)
        {
            var range = input.Split('-').ToLongArray();
            From = range[0];
            To = range[1];
        }

        public Range(long from, long to)
        {
            From = from;
            To = to;
        }

        public long From { get; }
        public long To { get; }
        
        public bool IsOverlap(Range other)
        {
            return From <= other.To && To >= other.From;
        }

        public long IncludedIds() =>  To - From + 1;


        public bool Equals(Range? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return From == other.From && To == other.To;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Range) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To);
        }
    }
}
