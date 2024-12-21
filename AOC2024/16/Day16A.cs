namespace AOC2024._16;

public class Day16A : Day
{
    private readonly List<Node> _nodes = [];
    protected override object Run()
    {
        var grid = GetInputAsCharGrid();
        foreach (var gridData in grid.Where(gd => gd.Value != '#'))
        {
            _nodes.Add(new Node { Y = gridData.Y, X = gridData.X, Direction = '>' });
            _nodes.Add(new Node { Y = gridData.Y, X = gridData.X, Direction = 'v' });
            _nodes.Add(new Node { Y = gridData.Y, X = gridData.X, Direction = '<' });
            _nodes.Add(new Node { Y = gridData.Y, X = gridData.X, Direction = '^' });
        }
        
        var gridStart = grid.First(gd => gd.Value == 'S');
        var gridEnd = grid.First(gd => gd.Value == 'E');

        var start = _nodes.First(n => n.Y == gridStart.Y && n.X == gridStart.X && n.Direction == '>');

        PrintElapsedTime("init");
        var cost = new List<int>();
        Parallel.ForEach(_nodes.Where(n => n.Y == gridEnd.Y && n.X == gridEnd.X), end =>
        {
            var path = Dijkstra.GetShortestPath(start, end, _nodes, GetNeighbourWithCost);
            var pathCost = path.Max(p => p.Cost);
            PrintElapsedTime($"Direction: {end.Direction} Cost: {pathCost}");
            cost.Add(pathCost);
        });

        return cost.Min();
    }

    private IEnumerable<(Node, int)> GetNeighbourWithCost(Node currentNode)
    {
        if (currentNode.Direction == '>')
        {
            var right = _nodes.FirstOrDefault(
                n => n.X == currentNode.X + 1 && n.Y == currentNode.Y && n.Direction == '>');
            if (right is not null)
            {
                yield return (right, 1);
            }
        }
        else if (currentNode.Direction == 'v')
        {
            var down = _nodes.FirstOrDefault(n =>
                n.X == currentNode.X && n.Y == currentNode.Y + 1 && n.Direction == 'v');
            if (down is not null)
            {
                yield return (down, 1);
            }
        }
        else if (currentNode.Direction == '<')
        {
            var left = _nodes.FirstOrDefault(n =>
                n.X == currentNode.X - 1 && n.Y == currentNode.Y && n.Direction == '<');
            if (left is not null)
            {
                yield return (left, 1);
            }
        }
        else
        {
            var up = _nodes.FirstOrDefault(n => n.X == currentNode.X && n.Y == currentNode.Y - 1 && n.Direction == '^');
            if (up is not null)
            {
                yield return (up, 1);
            }
        }

        if (currentNode.Direction != '>')
        {
            var right = _nodes.First(n => n.X == currentNode.X && n.Y == currentNode.Y && n.Direction == '>');
            yield return (right, Cost(currentNode.Direction, '>'));
        }
        
        if (currentNode.Direction != 'v')
        {
            var down = _nodes.First(n => n.X == currentNode.X && n.Y == currentNode.Y && n.Direction == 'v');
            yield return (down, Cost(currentNode.Direction, 'v'));
        }
        
        if (currentNode.Direction != '<')
        {
            var left = _nodes.First(n => n.X == currentNode.X && n.Y == currentNode.Y && n.Direction == '<');
            yield return (left, Cost(currentNode.Direction, '<'));
        }
        
        if (currentNode.Direction != '^')
        {
            var up = _nodes.First(n => n.X == currentNode.X && n.Y == currentNode.Y && n.Direction == '^');
            yield return (up, Cost(currentNode.Direction, '^'));
        }
    }

    private static int Cost(char from, char to) => from switch 
    {
        '>' when to is '^' or 'v' => 1000,
        'v' when to is '<' or '>' => 1000,
        '<' when to is '^' or 'v' => 1000,
        '^' when to is '<' or '>' => 1000,
        _ => 2000
    };

    private class Node : IEquatable<Node>
    {
        public int Y { get; init; }
        public int X { get; init; }
        public char Direction { get; init; }

        public bool Equals(Node? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Y == other.Y && X == other.X && Direction == other.Direction;
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
            return HashCode.Combine(Y, X, Direction);
        }
    }
}