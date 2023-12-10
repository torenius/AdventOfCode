namespace AOC2023._10;

public class Day10A : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        var nodes = new List<Node>();
        for (var y = 0; y < input.GetLength(0); y++)
        {
            for (var x = 0; x < input.GetLength(1); x++)
            {
                var node = new Node
                {
                    Value = input[y, x],
                    X = x,
                    Y = y
                };
                nodes.Add(node);
            }
        }

        var start = nodes.First(x => x.Value == 'S');
        var shortestPaths = Helper.ShortestPath(start, (child) => GetNeighbors(input, child));

        var max = 0;
        foreach (var node in nodes)
        {
            var path = shortestPaths(node).ToList();
            
            if (path.Count > max)
            {
                max = path.Count;
            }
        }
        
        return max;
    }

    private static List<Node> GetNeighbors(char[,] map, Node current)
    {
        var neighbors = new List<Node>();

        var canGoUp = current.Value is 'S' or '|' or 'L' or 'J';
        var canGoDown = current.Value is 'S' or '|' or '7' or 'F';
        var canGoLeft = current.Value is 'S' or '-' or 'J' or '7';
        var canGoRight = current.Value is 'S' or '-' or 'L' or 'F';
        
        
        // Up
        if (canGoUp && current.Y - 1 >= 0 && map[current.Y - 1, current.X] is '|' or '7' or 'F' or 'S')
        {
            neighbors.Add(new Node
            {
                Value = map[current.Y - 1, current.X],
                Y = current.Y - 1,
                X = current.X
            });
        }
        
        // Down
        if (canGoDown && current.Y + 1 < map.GetLength(0) && map[current.Y + 1, current.X] is '|' or 'L' or 'J' or 'S')
        {
            neighbors.Add(new Node
            {
                Value = map[current.Y + 1, current.X],
                Y = current.Y + 1,
                X = current.X
            });
        }
        
        // Left
        if (canGoLeft && current.X - 1 >= 0 && map[current.Y, current.X - 1] is '-' or 'L' or 'F' or 'S')
        {
            neighbors.Add(new Node
            {
                Value = map[current.Y, current.X - 1],
                Y = current.Y,
                X = current.X - 1
            });
        }
        
        // Right
        if (canGoRight && current.X + 1 < map.GetLength(1) && map[current.Y, current.X + 1] is '-' or 'J' or '7' or 'S')
        {
            neighbors.Add(new Node
            {
                Value = map[current.Y, current.X + 1],
                Y = current.Y,
                X = current.X + 1
            });
        }

        return neighbors;
    }
 
    
    private class Node : IEquatable<Node>
    {
        public char Value { get; init; }
        public int X { get; init; }
        public int Y { get; init; }

        public bool Equals(Node? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"Y: {Y} X: {X} V: {Value}";
        }
    }
}