namespace AOC2024;

public class GridData<T>(int y, int x, T value) : IEquatable<GridData<T>>
{
    public int Y { get; init; } = y;
    public int X { get; init; } = x;
    public T Value { get; set; } = value;
    
    public override string? ToString() => Value?.ToString();

    public bool Equals(GridData<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Y == other.Y && X == other.X;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((GridData<T>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Y, X);
    }
}

public static class GridDataExtensions
{
    public static int CalculateManhattanDistance<T>(this GridData<T> from, GridData<T> to)
    {
        var dx = Math.Abs(from.X - to.X);
        var dy = Math.Abs(from.Y - to.Y);
        return dx + dy;
    }
}
