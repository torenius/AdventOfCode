namespace AOC2022._16;

public class Day16A : Day
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
        
        return "" + GetMaxPressure(pathCost, "AA", new List<string>(), 30);
    }

    private static long GetMaxPressure(List<ShortestPathCost> costs, string name, List<string> visited, int minutesLeft)
    {
        long maxPressure = 0;
        foreach (var cost in costs.Where(c => c.FromName == name && c.Cost < minutesLeft && !visited.Contains(c.ToName)))
        {
            var v = visited.ToList();
            v.Add(cost.ToName);
            var temp = GetMaxPressure(costs, cost.ToName, v, minutesLeft - cost.Cost) + cost.PressureReleased(minutesLeft);
            if (temp > maxPressure)
            {
                maxPressure = temp;
            }
        }

        return maxPressure;
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