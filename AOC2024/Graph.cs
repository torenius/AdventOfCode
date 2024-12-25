using System.Collections;

namespace AOC2024;

public class Graph<T> : IEnumerable<GraphNode<T>> 
    where T : IEquatable<T>
{
    public Graph()
    {
        Nodes = [];
    }
    public Graph(List<GraphNode<T>> nodes)
    {
        Nodes = nodes;
    }
    
    public List<GraphNode<T>> Nodes { get; private set; }

    public GraphNode<T> AddNode(T value)
    {
        var node = new GraphNode<T>(value);
        Nodes.Add(node);
        return node;
    }

    public GraphNode<T> AddOrGetIfExists(T value)
    {
        var node = Nodes.FirstOrDefault(n => n.Value.Equals(value));
        if (node is null)
        {
            node = new GraphNode<T>(value);
            Nodes.Add(node);
        }

        return node;
    }
    
    public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost = 0)
    {
        from.Neighbors.Add(to);
        from.Costs.Add(cost);
    }

    public void AddUndirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost = 0)
    {
        from.Neighbors.Add(to);
        from.Costs.Add(cost);

        to.Neighbors.Add(from);
        to.Costs.Add(cost);
    }
    
    public GraphNode<T> this[int i] => Nodes[i];

    public int Count => Nodes.Count;
    public IEnumerator<GraphNode<T>> GetEnumerator() => Nodes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}