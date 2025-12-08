namespace AOC.Common;

public struct XyzCoordinate : IEquatable<XyzCoordinate>
{
    public XyzCoordinate(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    
    public int X { get; set; }

    public int Y { get; set; }

    public int Z { get; set; }

    public static bool operator ==(XyzCoordinate left, XyzCoordinate right) =>
        left.X == right.X && left.Y == right.Y && left.Z == right.Z;
    
    public static bool operator !=(XyzCoordinate left, XyzCoordinate right) => !(left == right);

    public bool Equals(XyzCoordinate other) => this == other;

    public override bool Equals(object? obj)
    {
        return obj is XyzCoordinate other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
    
    public readonly override string ToString() => $"{{X={X},Y={Y},Z={Z}}})";
    
    /// <summary>
    /// Imagine drawing a right triangle. Where the hypotenuse goes from coordinate A to coordinate B
    /// https://en.wikipedia.org/wiki/Euclidean_distance
    /// </summary>
    /// <returns>Length of the hypotenuse</returns>
    public int CalculateEuclideanDistance(XyzCoordinate to)
    {
        long dx = X - to.X;
        long dy = Y - to.Y;
        long dz = Z - to.Z;
        return (int)Math.Sqrt(dx * dx + dy * dy + dz * dz);
    }
}