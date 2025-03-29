using AOC.Common;

namespace AOC2024._10;

public class Day10A : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        var maxY = input.GetLength(0);
        var maxX = input.GetLength(1);

        var nodes = new List<Node>();
        var map = new int[maxY, maxX];
        for (var y = 0; y < maxY; y++)
        {
            for (var x = 0; x < maxX; x++)
            {
                if (input[y, x] == '.')
                {
                    map[y, x] = -1;
                    nodes.Add(new Node{X = x, Y = y, Value = -1});
                }
                else
                {
                    map[y, x] = input[y, x].ToInt();
                    nodes.Add(new Node{X = x, Y = y, Value = map[y, x]});
                }
            }
        }

        var sum = 0;
        foreach (var node in nodes.Where(n => n.Value == 0))
        {
            var test = Helper.BreadthFirst(node, (nodeToCheck) => GetNeighbour(map, maxX, maxY, nodeToCheck, nodes)).ToList();
            sum += test.Count(t => t.Value == 9);
        }

        return sum;
    }

    private static IEnumerable<Node> GetNeighbour(int[,] map, int maxX, int maxY, Node nodeToCheck, List<Node> nodes)
    {
        // Right
        if (IsValid(map, nodeToCheck.X + 1, nodeToCheck.Y, maxX, maxY) && map[nodeToCheck.Y, nodeToCheck.X + 1] == nodeToCheck.Value + 1)
        {
            var r = nodes.First(n => n.X == nodeToCheck.X + 1 && n.Y == nodeToCheck.Y);
            yield return r;
        }
        
        // Left
        if (IsValid(map, nodeToCheck.X - 1, nodeToCheck.Y, maxX, maxY) && map[nodeToCheck.Y, nodeToCheck.X - 1] == nodeToCheck.Value + 1)
        {
            var l = nodes.First(n => n.X == nodeToCheck.X - 1 && n.Y == nodeToCheck.Y);
            yield return l;
        }
        
        // Down
        if (IsValid(map, nodeToCheck.X, nodeToCheck.Y + 1, maxX, maxY) && map[nodeToCheck.Y + 1, nodeToCheck.X] == nodeToCheck.Value + 1)
        {
            var d = nodes.First(n => n.X == nodeToCheck.X && n.Y == nodeToCheck.Y + 1);
            yield return d;
        }
        
        // Up
        if (IsValid(map, nodeToCheck.X, nodeToCheck.Y - 1, maxX, maxY) && map[nodeToCheck.Y - 1, nodeToCheck.X] == nodeToCheck.Value + 1)
        {
            var u = nodes.First(n => n.X == nodeToCheck.X && n.Y == nodeToCheck.Y - 1);
            yield return u;
        }
    }

    private static bool IsValid(int[,] map, int x, int y, int maxX, int maxY) =>
        x >= 0 && y >= 0 && x < maxX && y < maxY;
    
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