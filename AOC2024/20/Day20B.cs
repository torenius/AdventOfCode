using AOC.Common;

namespace AOC2024._20;

public class Day20B : Day
{
    protected override object Run()
    {
        //var grid = GetInputAsCharGrid("example.txt");
        var grid = GetInputAsCharGrid();

        var start = grid.First(gd => gd.Value == 'S');
        var end = grid.First(gd => gd.Value == 'E');

        var nrOfSteps = ShortestPath(start, end,
            (neighbour) => grid.GetOrthogonalData(neighbour).Where(gd => gd.Value != '#'));
        var maxSteps = nrOfSteps.Max(x => x.Item2);
        
        //grid.Print(grid.GetDiamondData(end, 2).ToList());

        var cheats = new HashSet<Cheat>();
        foreach (var from in grid.Where(gd => gd.Value is 'S' or '.'))
        {
            var stepFrom = nrOfSteps.First(x => x.Item1.Y == from.Y && x.Item1.X == from.X);
            foreach (var spotToCheck in grid.GetDiamondData(from, 20)
                         .Where(gd => gd.Value is '.' or 'E' && !(gd.Y == from.Y && gd.X == from.X) && (gd.Y != from.Y || gd.X != from.X)))
            {
                var cheat = new Cheat
                {
                    From = from,
                    To = spotToCheck,
                };
                if (cheats.Contains(cheat)) continue;
                
                var spot = nrOfSteps.First(x => x.Item1.Y == spotToCheck.Y && x.Item1.X == spotToCheck.X);
                var stepCount = from.CalculateManhattanDistance(spotToCheck); 
                
                var newPathStepCount = maxSteps - (stepFrom.Item2 - spot.Item2 - stepCount);
                if (newPathStepCount > 0 && newPathStepCount < maxSteps)
                {
                    cheat.Steps = newPathStepCount;
                    cheats.Add(cheat);
                }
            }
        }

        var result = cheats.GroupBy(c => c.Steps).Select(c => new
        {
            Picoseconds = c.Key,
            Occurances = c.Count(),
            Save = maxSteps - c.Key
        }).ToList();

        //foreach (var cheat in result.OrderByDescending(c => c.Picoseconds))
        foreach (var cheat in result.Where(c => c.Save >= 50).OrderByDescending(c => c.Picoseconds))
        {
            Console.WriteLine($"{cheat.Occurances} that saves at {cheat.Save}");
        }
        
        return result.Where(r => r.Save >= 100).Sum(r => r.Occurances);
    }
    
    public static List<(T, int)> ShortestPath<T>(T start, T end, Func<T, IEnumerable<T>> children) where T : IEquatable<T>
    {
        var previous = new Dictionary<T, T> {{start, start}};

        var queue = new Queue<T>();
        queue.Enqueue(start);

        while (queue.Count != 0)
        {
            var from = queue.Dequeue();
            foreach (var neighbor in children(from).Where(x => !previous.ContainsKey(x)))
            {
                previous.Add(neighbor, from);
                queue.Enqueue(neighbor);
            }
        }
        
        var path = new List<(T, int)>();
        var steps = 0;

        var current = end;
        while (!current.Equals(start))
        {
            path.Add((current, steps));

            if (previous.TryGetValue(current, out var value))
            {
                current = value;
                steps++;
            }
            else
            {
                return [];
            }
        }

        path.Add((start, steps));
        return path;
    }

    private class Cheat : IEquatable<Cheat>
    {
        public GridData<char> From { get; set; }
        public GridData<char> To { get; set; }
        public int Steps { get; set; }

        public bool Equals(Cheat? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return From.Equals(other.From) && To.Equals(other.To);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Cheat) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To);
        }
    }
}
