namespace AOC2024;

public class GraphNode<T>(T value) : IEquatable<GraphNode<T>> 
    where T : IEquatable<T>
{
    public T Value { get; init; } = value;
    public List<GraphNode<T>> Neighbors { get; init; } = [];
    public List<int> Costs { get; init; } = [];

    public override string? ToString() => Value.ToString();

    public bool Equals(GraphNode<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((GraphNode<T>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Value);
    }
}