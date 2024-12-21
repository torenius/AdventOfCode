namespace AOC2024;

//  https://andrewlock.net/implementing-dijkstras-algorithm-for-finding-the-shortest-path-between-two-nodes-using-priorityqueue-in-dotnet-9/
public class Dijkstra
{
    public static List<(T Node, int Cost)> GetShortestPath<T>(T start, T end,
        IEnumerable<T> nodes, Func<T, IEnumerable<(T Neighbour, int Cost)>> neighbourWithCost) where T : class, IEquatable<T> 
    {
        var costs = nodes
            .Select(node => (node, Details: (Previous: (T?) null, Cost: int.MaxValue)))
            .ToDictionary(x => x.node, x => x.Details);
        
        costs[start] = (null, 0);
        
        var queue = new PriorityQueue<T, int>();
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Equals(end))
            {
                return BuildPath();
            }
            
            var currentCost = costs[current].Cost;
            foreach (var path in neighbourWithCost(current))
            {
                var cost = costs[path.Neighbour].Cost;
                var newCost = currentCost + path.Cost;

                if (newCost < cost)
                {
                    costs[path.Neighbour] = (current, newCost);
                    
                    queue.Remove(path.Neighbour, out _, out _);
                    queue.Enqueue(path.Neighbour, newCost);
                }
            }
        }

        return [];

        List<(T, int)> BuildPath()
        {
            var path = new List<(T, int)>();
            var previous = end;

            while (previous is not null)
            {
                var current = previous;
                (previous, var cost) = costs[current];
                path.Add((current, cost));
            }
            
            return path;
        }
    }
}