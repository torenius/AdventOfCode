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
            
            path.Reverse();
            return path;
        }
    }
    
    public static List<(GraphNode<T> Node, int Cost)> GetShortestPath<T>(GraphNode<T> start, GraphNode<T> end,
        IEnumerable<GraphNode<T>> nodes) where T : class, IEquatable<T>
    {
        var costs = nodes
            .Select(node => (node, Details: (Previous: (GraphNode<T>?) null, Cost: int.MaxValue)))
            .ToDictionary(x => x.node, x => x.Details);
        
        costs[start] = (null, 0);
        
        var queue = new PriorityQueue<GraphNode<T>, int>();
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Equals(end))
            {
                return BuildPath();
            }
            
            var currentCost = costs[current].Cost;
            for (var i = 0; i < current.Neighbors.Count; i++)
            {
                var cost = costs[current.Neighbors[i]].Cost;
                var newCost = currentCost + current.Costs[i];

                if (newCost < cost)
                {
                    costs[current.Neighbors[i]] = (current, newCost);
                    
                    queue.Remove(current.Neighbors[i], out _, out _);
                    queue.Enqueue(current.Neighbors[i], newCost);
                }
            }
        }

        return [];

        List<(GraphNode<T>, int)> BuildPath()
        {
            var path = new List<(GraphNode<T>, int)>();
            var previous = end;

            while (previous is not null)
            {
                var current = previous;
                (previous, var cost) = costs[current];
                path.Add((current, cost));
            }
            
            path.Reverse();
            return path;
        }
    }
    
    public static List<List<(GraphNode<T> Node, int Cost)>> GetShortestPaths<T>(GraphNode<T> start, GraphNode<T> end,
        IEnumerable<GraphNode<T>> nodes) where T : class, IEquatable<T>
    {
        var costs = nodes
            .Select(node => (node, Details: (Previous: new List<GraphNode<T>>(), Cost: int.MaxValue)))
            .ToDictionary(x => x.node, x => x.Details);
        
        costs[start] = ([], 0);
        
        var queue = new PriorityQueue<GraphNode<T>, int>();
        queue.Enqueue(start, 0);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Equals(end))
            {
                return BuildShortestPaths(costs, [], end);
            }
            
            var currentCost = costs[current].Cost;
            for (var i = 0; i < current.Neighbors.Count; i++)
            {
                var cost = costs[current.Neighbors[i]].Cost;
                var newCost = currentCost + current.Costs[i];

                if (newCost < cost)
                {
                    costs[current.Neighbors[i]] = ([current], newCost);
                    
                    queue.Remove(current.Neighbors[i], out _, out _);
                    queue.Enqueue(current.Neighbors[i], newCost);
                }
                else if (newCost == cost)
                {
                    var (previous, _) = costs[current.Neighbors[i]];
                    previous.Add(current);
                }
            }
        }

        return [];
    }
    
    private static List<List<(GraphNode<T> Node, int Cost)>> BuildShortestPaths<T>(Dictionary<GraphNode<T>, (List<GraphNode<T>> Previous, int Cost)> path, List<(GraphNode<T>, int)> result, GraphNode<T> end)
        where T : class, IEquatable<T>
    {
        var current = end;
        while (true)
        {
            var parents = path[current];
            if (parents.Previous.Count == 0)
            {
                // Found start
                result.Add((current, 0));
                return [result];
            }
            
            if (parents.Previous.Count(c => !result.Any(r => r.Item1.Equals(c))) == 1)
            {
                var prev = parents.Previous.First(c => !result.Any(r => r.Item1.Equals(c)));
                result.Add((current, parents.Cost));
                current = prev;
            }
            else
            {
                var results = new List<List<(GraphNode<T>, int)>>();
                foreach (var previous in parents.Previous.Where(c => !result.Any(r => r.Item1.Equals(c))))
                {
                    var res = result.ToList();
                    res.Add((current, parents.Cost));
                    results.AddRange(BuildShortestPaths(path, res, previous));
                }

                return results;
            }
        }
    }
}
