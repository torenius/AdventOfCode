using AOC.Common;

namespace AOC2024._10;

public class Day10B : Day
{
    private int MaxX { get; set; }
    private int MaxY { get; set; }
    private int[,] Map { get; set; }
    private List<Node> Nodes { get; set; }
    
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        MaxX = input.GetLength(0);
        MaxY = input.GetLength(1);

        Nodes = [];
        Map = new int[MaxY, MaxX];
        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX; x++)
            {
                if (input[y, x] == '.')
                {
                    Map[y, x] = -1;
                    Nodes.Add(new Node{X = x, Y = y, Value = -1});
                }
                else
                {
                    Map[y, x] = input[y, x].ToInt();
                    Nodes.Add(new Node{X = x, Y = y, Value = Map[y, x]});
                }
            }
        }

        var sum = 0;
        foreach (var node in Nodes.Where(n => n.Value == 0))
        {
            var test = DepthFirstSearch(node, [], 0);
            sum += test.Count;
        }

        return sum;
    }

    private List<Dictionary<Node, int>> DepthFirstSearch(Node node, Dictionary<Node, int> visited, int depth)
    {
        if (!visited.TryAdd(node, depth)) return [];
        
        if (node.Value == 9) return [visited];

        var paths = new List<Dictionary<Node, int>>();
        foreach (var neighbour in GetNeighbour(node).Where(n => !visited.ContainsKey(n)))
        {
            var visit = visited.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var result = DepthFirstSearch(neighbour, visit, depth + 1);
            paths.AddRange(result);
        }

        return paths;
    }

    private IEnumerable<Node> GetNeighbour(Node nodeToCheck)
    {
        // Right
        if (IsValid(nodeToCheck.X + 1, nodeToCheck.Y) && Map[nodeToCheck.Y, nodeToCheck.X + 1] == nodeToCheck.Value + 1)
        {
            var r = Nodes.First(n => n.X == nodeToCheck.X + 1 && n.Y == nodeToCheck.Y);
            yield return r;
        }
        
        // Left
        if (IsValid(nodeToCheck.X - 1, nodeToCheck.Y) && Map[nodeToCheck.Y, nodeToCheck.X - 1] == nodeToCheck.Value + 1)
        {
            var l = Nodes.First(n => n.X == nodeToCheck.X - 1 && n.Y == nodeToCheck.Y);
            yield return l;
        }
        
        // Down
        if (IsValid(nodeToCheck.X, nodeToCheck.Y + 1) && Map[nodeToCheck.Y + 1, nodeToCheck.X] == nodeToCheck.Value + 1)
        {
            var d = Nodes.First(n => n.X == nodeToCheck.X && n.Y == nodeToCheck.Y + 1);
            yield return d;
        }
        
        // Up
        if (IsValid(nodeToCheck.X, nodeToCheck.Y - 1) && Map[nodeToCheck.Y - 1, nodeToCheck.X] == nodeToCheck.Value + 1)
        {
            var u = Nodes.First(n => n.X == nodeToCheck.X && n.Y == nodeToCheck.Y - 1);
            yield return u;
        }
    }

    private bool IsValid(int x, int y) =>
        x >= 0 && y >= 0 && x < MaxX && y < MaxY;
    
    private class Node : IEquatable<Node>
    {
        public int X { get; init; }
        public int Y { get; init; }
        public int Value { get; init; }

        public bool Equals(Node? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Node)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}