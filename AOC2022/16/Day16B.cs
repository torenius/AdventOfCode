namespace AOC2022._16;

public class Day16B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var rooms = input.Select(i => new Room(i)).ToList();
        foreach (var room in rooms)
        {
            foreach (var leadsTo in room.LeadsToName)
            {
                room.LeadsTo.Add(rooms.First(r => r.Name == leadsTo));
            }
        }

        var pathCost = new List<ShortestPathCost>();
        var roomsToCheck = rooms.Where(r => r.FlowRate > 0 || r.Name == "AA").ToList();
        foreach (var start in roomsToCheck)
        {
            var shortestPath = ShortestPath(start);
            foreach (var finish in roomsToCheck.Where(r => r.Name != start.Name && r.Name != "AA"))
            {
                var steps = shortestPath(finish);
                var cost = new ShortestPathCost
                {
                    FromName = start.Name,
                    ToName = finish.Name,
                    Cost = steps.Count() + 1, // go there + open valve
                    FlowRate = finish.FlowRate
                };
                
                pathCost.Add(cost);
            }
        }

        var you = new Player
        {
            Name = "AA",
            MinutesLeft = 26
        };
        var elephant = new Player
        {
            Name = "AA",
            MinutesLeft = 26
        };
        
        return "" + GetMaxPressure(pathCost, you, elephant, new List<string>{"AA"});
    }

    private class Player
    {
        public string Name { get; set; }
        public int MinutesLeft { get; set; }
    }

    private static long GetMaxPressure(List<ShortestPathCost> costs, Player? you, Player? elephant, List<string> visited)
    {
        long maxPressure = 0;
        foreach (var paths in GetPossiblePaths(costs, you, elephant, visited))
        {
            long temp = 0;
            Player? y = null;
            Player? e = null;

            if (paths.You is not null)
            {
                visited.Add(paths.You.ToName);
                temp += paths.You.PressureReleased(you.MinutesLeft);
                y = new Player
                {
                    Name = paths.You.ToName,
                    MinutesLeft = you.MinutesLeft - paths.You.Cost
                };
            }

            if (paths.Elephant is not null)
            {
                visited.Add(paths.Elephant.ToName);
                temp += paths.Elephant.PressureReleased(elephant.MinutesLeft);
                e = new Player
                {
                    Name = paths.Elephant.ToName,
                    MinutesLeft = elephant.MinutesLeft - paths.Elephant.Cost
                };
            }
            
            temp += GetMaxPressure(costs, y, e, visited);
            
            if (paths.You is not null)
            {
                visited.Remove(paths.You.ToName);
            }

            if (paths.Elephant is not null)
            {
                visited.Remove(paths.Elephant.ToName);
            }

            if (temp > maxPressure)
            {
                maxPressure = temp;
            }
        }

        return maxPressure;
    }

    private static List<(ShortestPathCost? You, ShortestPathCost? Elephant)> GetPossiblePaths(
        List<ShortestPathCost> costs, Player? you, Player? elephant, List<string> visited)
    {
        var paths = new List<(ShortestPathCost? You, ShortestPathCost? Elephant)>();

        var youPaths = new List<ShortestPathCost>();
        if (you is not null)
        {
            youPaths = costs.Where(c =>
                c.FromName == you.Name && c.Cost < you.MinutesLeft && !visited.Contains(c.ToName)).ToList();
        }
        
        var elephantPaths = new List<ShortestPathCost>();
        if (elephant is not null)
        {
            elephantPaths = costs.Where(c =>
                c.FromName == elephant.Name && c.Cost < elephant.MinutesLeft && !visited.Contains(c.ToName)).ToList();
        }

        if (youPaths.Count == 0 && elephantPaths.Count != 0)
        {
            foreach (var elephantCost in elephantPaths)
            {
                paths.Add((null, elephantCost));
            }

            return paths;
        }
        
        if (youPaths.Count != 0 && elephantPaths.Count == 0)
        {
            foreach (var youCost in youPaths)
            {
                paths.Add((youCost, null));
            }

            return paths;
        }
        
        foreach (var youCost in youPaths)
        {
            foreach (var elephantCost in elephantPaths)
            {
                if (youCost.ToName != elephantCost.ToName)
                {
                    paths.Add((youCost, elephantCost));
                }
            }
        }

        return paths;
    }

    private static Func<Room, IEnumerable<Room>> ShortestPath(Room start)
    {
        var previous = new Dictionary<Room, Room>();
        var queue = new Queue<Room>();
        queue.Enqueue(start);

        while (queue.Any())
        {
            var current = queue.Dequeue();
            foreach (var neighbor in current.LeadsTo.Where(r => !previous.ContainsKey(r)))
            {
                previous.Add(neighbor, current);
                queue.Enqueue(neighbor);
            }
        }

        IEnumerable<Room> Path(Room finish)
        {
            var path = new List<Room>();

            var current = finish;
            while (!current.Name.Equals(start.Name))
            {
                path.Add(current);

                if (previous.TryGetValue(current, out var value))
                {
                    current = value;
                }
                else
                {
                    return new List<Room>();
                }
            }

            return path;
        };

        return Path;
    }

    private class ShortestPathCost
    {
        public string FromName { get; set; }
        public string ToName { get; set; }
        public int Cost { get; set; }
        public int FlowRate { get; set; }
        public long PressureReleased(int currentMinutes) => (currentMinutes - Cost) * FlowRate;
    }

    private class Room
    {
        public Room(string input)
        {
            var row = input.Split("; ");
            var valvePart = row[0].Split(" has flow rate=");
            Name = valvePart[0].Replace("Valve ", "");
            FlowRate = valvePart[1].ToInt();

            var roomPart = row[1];
            roomPart = roomPart.Replace("tunnels lead to valves ", "");
            roomPart = roomPart.Replace("tunnel leads to valve ", "");
            LeadsToName = roomPart.Split(", ").ToList();

            LeadsTo = new List<Room>();
        }

        public string Name { get; set; }
        public int FlowRate { get; set; }
        public List<string> LeadsToName { get; set; }
        public List<Room> LeadsTo { get; set; }

    }
}