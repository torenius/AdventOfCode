namespace AOC2015._24;

public class Day24B : Day
{
    protected override string Run()
    {
        var input = GetInputAsIntArray().OrderByDescending(x => x).ToArray();

        var groupSize = input.Sum() / 4;

        var balanced = new List<Group>();
        for (var i = 0; i < input.Length - 1; i++)
        {
            var alternatives = input[i..];
            var g1Alternatives = FillGroup(alternatives, groupSize);
            
            // If we managed to make a group then we will have numbers left to make two other groups
            // The smallest G1 will be our competitor.
            if (g1Alternatives.Count != 0)
            {
                g1Alternatives.Sort();
                balanced.Add(g1Alternatives[0]);
            }
        }

        balanced.Sort();
        foreach (var group in balanced)
        {
            Console.WriteLine($"QE: {group.Qe()} {string.Join(" ", group.Values)}");
        }
        return "" + balanced[0].Qe();
    }
    
    private List<Group> FillGroup(int[] input, int groupSize)
    {
        var result = new List<Group>();
        for (var i = 0; i < input.Length - 1; i++)
        {
            var packing = new Group();
            packing.Values.Add(input[i]);
            result.AddRange(FillGroup(packing, input[(i+1)..], groupSize));
        }

        return result;
    }

    private List<Group> FillGroup(Group group, int[] input, int groupSize)
    {
        var result = new List<Group>();
        var sum = group.Values.Sum();
        if (sum > groupSize) return result;
        if (sum == groupSize)
        {
            result.Add(group);
            return result;
        }
        for (var i = 0; i < input.Length; i++)
        {
            if (sum + input[i] <= groupSize)
            {
                var g = new Group
                {
                    Values = group.Values.ToList()
                };
                g.Values.Add(input[i]);
                result.AddRange(FillGroup(g, input[(i+1)..], groupSize));
            }
        }

        return result;
    }

    private class Group : IComparable<Group>
    {
        public List<int> Values { get; set; } = new();
        
        public long Qe()
        {
            long sum = 1;
            foreach (var present in Values)
            {
                sum *= present;
            }

            return sum;
        }
        
        public int CompareTo(Group? other)
        {
            var test = Values.Count.CompareTo(other.Values.Count);
            return test == 0 ? Qe().CompareTo(other.Qe()) : test;
        }
    }
}