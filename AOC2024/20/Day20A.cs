namespace AOC2024._20;

public class Day20A : Day
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

        var wallsToRemove = new HashSet<GridData<char>>();
        foreach (var gridData in grid.Where(gd =>
                     gd.Y != 0 && gd.X != 0 && gd.Y != grid.MaxY - 1 && gd.X != grid.MaxX - 1 && gd.Value == '#'))
        {
            if (grid.GetOrthogonalData(gridData).Count(gd => gd.Value != '#') >= 2) wallsToRemove.Add(gridData);
        }
        
        //grid.Print(wallsToRemove);
        
        var cheats = new List<int>();
        foreach (var wall in wallsToRemove)
        {
            var lowSteps = int.MaxValue;
            var highSteps = 0;
            foreach (var spotToCheck in grid.GetOrthogonalData(wall).Where(gd => gd.Value != '#'))
            {
                var spot = nrOfSteps.First(x => x.Item1.Y == spotToCheck.Y && x.Item1.X == spotToCheck.X);
                if (spot.Item2 < lowSteps) lowSteps = spot.Item2;
                if (spot.Item2 > highSteps) highSteps = spot.Item2;
            }
            var newPathStepCount = highSteps - lowSteps - 2;
            cheats.Add(maxSteps - newPathStepCount);
        }

        var result = cheats.GroupBy(c => c).Select(c => new
        {
            Picoseconds = c.Key,
            Occurances = c.Count(),
            Save = maxSteps - c.Key
        }).ToList();

        foreach (var cheat in result.OrderByDescending(c => c.Picoseconds))
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
}
