using System.Drawing;

namespace AOC2024._16;

public class Day16B : Day
{
    protected override object Run()
    {
        var grid = GetInputAsCharGrid();
        var graph = new Graph<Node>();
        foreach (var gridData in grid.Where(gd => gd.Value != '#'))
        {
            if (grid.IsValid(gridData.Y, gridData.X + 1) && grid.GetGridData(gridData.Y, gridData.X + 1).Value != '#' ||
                grid.IsValid(gridData.Y, gridData.X - 1) && grid.GetGridData(gridData.Y, gridData.X - 1).Value != '#')
            {
                graph.AddNode(new Node { Y = gridData.Y, X = gridData.X, Direction = '>' });
                graph.AddNode(new Node { Y = gridData.Y, X = gridData.X, Direction = '<' });
            }
            
            if (grid.IsValid(gridData.Y + 1, gridData.X) && grid.GetGridData(gridData.Y + 1, gridData.X).Value != '#' ||
                grid.IsValid(gridData.Y - 1, gridData.X) && grid.GetGridData(gridData.Y - 1, gridData.X).Value != '#')
            {
                graph.AddNode(new Node { Y = gridData.Y, X = gridData.X, Direction = '^' });
                graph.AddNode(new Node { Y = gridData.Y, X = gridData.X, Direction = 'v' });
            }
        }

        foreach (var gridData in grid.Where(gd => gd.Value != '#'))
        {
            var nodes = graph.Nodes.Where(n => n.Value.Y == gridData.Y && n.Value.X == gridData.X).ToList();
            for (var i = 0; i < nodes.Count - 1; i++)
            {
                for (var j = i + 1; j < nodes.Count; j++)
                {
                    graph.AddUndirectedEdge(nodes[i], nodes[j], Cost(nodes[i].Value, nodes[j].Value));
                }
            }

            foreach (var gd in grid.GetOrthogonalData(gridData).Where(gd => gd.Value != '#'))
            {
                if (gridData.X + 1 == gd.X)
                {
                    var node = nodes.First(n => n.Value.Direction == '>');
                    var right = graph.Nodes.First(n => n.Value.Y == gridData.Y && n.Value.X == gridData.X + 1 && n.Value.Direction == '>');
                    graph.AddDirectedEdge(node, right, 1);
                }
                else if (gridData.X - 1 == gd.X)
                {
                    var node = nodes.First(n => n.Value.Direction == '<');
                    var left = graph.Nodes.First(n => n.Value.Y == gridData.Y && n.Value.X == gridData.X - 1 && n.Value.Direction == '<');
                    graph.AddDirectedEdge(node, left, 1);
                }
                else if (gridData.Y + 1 == gd.Y)
                {
                    var node = nodes.First(n => n.Value.Direction == 'v');
                    var down = graph.Nodes.First(n => n.Value.Y == gridData.Y + 1 && n.Value.X == gridData.X && n.Value.Direction == 'v');
                    graph.AddDirectedEdge(node, down, 1);
                }
                else if (gridData.Y - 1 == gd.Y)
                {
                    var node = nodes.First(n => n.Value.Direction == '^');
                    var up = graph.Nodes.First(n => n.Value.Y == gridData.Y - 1 && n.Value.X == gridData.X && n.Value.Direction == '^');
                    graph.AddDirectedEdge(node, up, 1);
                }
            }
        }
        
        var gridStart = grid.First(gd => gd.Value == 'S');
        var gridEnd = grid.First(gd => gd.Value == 'E');
        var start = graph.Nodes.First(n => n.Value.Y == gridStart.Y && n.Value.X == gridStart.X && n.Value.Direction == '>');

        PrintElapsedTime("init");
        
        var cost = new List<int>();
        foreach (var end in graph.Nodes.Where(n => n.Value.Y == gridEnd.Y && n.Value.X == gridEnd.X))
        {
            var path = Dijkstra.GetShortestPath(start, end, graph.Nodes);
            var pathCost = path.Max(p => p.Cost);
            PrintElapsedTime($"Direction: {end.Value.Direction} Cost: {pathCost}");
            cost.Add(pathCost);
        }

        var minCost = cost.Min();

        var shortestPaths = new List<List<(GraphNode<Node> Node, int Cost)>>();
        foreach (var end in graph.Nodes.Where(n => n.Value.Y == gridEnd.Y && n.Value.X == gridEnd.X))
        {
            Console.WriteLine(end);
            foreach (var path in Dijkstra.GetShortestPaths(start, end, graph.Nodes))
            {
                var pathCost = path.Max(p => p.Cost);
                if (pathCost == minCost)
                {
                    //Helper.Print(path.Select(r => new Point(r.Node.Value.X, r.Node.Value.Y)).ToList());
                    shortestPaths.Add(path);
                }
            }
        }

        var result = shortestPaths.SelectMany(sp => sp.Select(n => new
        {
            n.Node.Value.Y,
            n.Node.Value.X
        })).Distinct().ToList();

        // foreach (var path in result)
        // {
        //     grid[path.Y, path.X] = 'O';
        // }
        // grid.Print();
        
        return result.Count();
    }
    
    private static int Cost(Node from, Node to) => from.Direction switch 
    {
        '>' when to.Direction is '^' or 'v' => 1000,
        'v' when to.Direction is '<' or '>' => 1000,
        '<' when to.Direction is '^' or 'v' => 1000,
        '^' when to.Direction is '<' or '>' => 1000,
        _ => 2000
    };
    
    private class Node : IEquatable<Node>
    {
        public int Y { get; init; }
        public int X { get; init; }
        public char Direction { get; init; }

        public override string ToString() => $"{X}, {Y}, {Direction}";

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