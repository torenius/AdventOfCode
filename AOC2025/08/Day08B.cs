using AOC.Common;

namespace AOC2025._08;

public class Day08B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var coordinates = input
            .Select(row => row.Split(','))
            .Select(split => new XyzCoordinate(split[0].ToInt(), split[1].ToInt(), split[2].ToInt()))
            .ToList();

        var connections = new List<DistanceToBox>();
        for (var i = 0; i < coordinates.Count - 1; i++)
        {
            var left = coordinates[i];
            for (var j = i + 1; j < coordinates.Count; j++)
            {
                var right = coordinates[j];
                connections.Add(new DistanceToBox
                {
                    Distance = left.CalculateEuclideanDistance(right),
                    Left =  left,
                    Right =  right
                });
            }
        }

        var sorted = connections.OrderBy(x => x.Distance).ToList();
        var buckets = new Dictionary<int, List<XyzCoordinate>>();
        var maxBucketNumber = 0;
        DistanceToBox? last = null;
        foreach (var shortest in sorted)
        {
            var left = buckets.FirstOrDefault(b => b.Value.Contains(shortest.Left));
            var right = buckets.FirstOrDefault(b => b.Value.Contains(shortest.Right));

            if (left.Key == 0 && right.Key == 0)
            {
                maxBucketNumber++;
                buckets.Add(maxBucketNumber, [shortest.Left, shortest.Right]);
            }
            else if (left.Key == 0 && right.Key != 0)
            {
                right.Value.Add(shortest.Left);
            }
            else if (left.Key != 0 && right.Key == 0)
            {
                left.Value.Add(shortest.Right);
            }
            else if (left.Key != right.Key)
            {
                right.Value.AddRange(left.Value);
                buckets.Remove(left.Key);
            }

            if (buckets.Count == 1 && buckets.First().Value.Count == coordinates.Count)
            {
                last = shortest;
                break;
            }
        }

        return (long)last!.Left.X * (long)last.Right.X;
    }

    private class DistanceToBox
    {
        public long Distance { get; set; }
        public XyzCoordinate Left { get; set; }
        public XyzCoordinate Right { get; set; }
    }
}