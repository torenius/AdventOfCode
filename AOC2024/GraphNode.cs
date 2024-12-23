namespace AOC2024;

public class GraphNode<T>(T value) where T : IEquatable<T>
{
    public T Value { get; init; } = value;
    public List<GraphNode<T>> Neighbors { get; init; } = [];
    public List<int> Costs { get; init; } = [];

    public override string? ToString() => Value.ToString();
}