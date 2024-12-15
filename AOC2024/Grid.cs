using System.Collections;
using System.Text;

namespace AOC2024;

public class Grid<T> : IEnumerable<GridData<T>> where T : notnull
{
    private readonly GridData<T>[,] _gridData;

    public Grid(T[,] matrix)
    {
        MaxY = matrix.GetLength(0);
        MaxX = matrix.GetLength(1);
        
        _gridData = new GridData<T>[MaxY, MaxX];
        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX; x++)
            {
                _gridData[y, x] = new GridData<T>(y, x, matrix[y, x]);
            }
        }
    }

    public int MaxY { get; private set; }
    public int MaxX { get; private set; }
    
    public T this[int y, int x]
    {
        get => _gridData[y, x].Value;
        set => _gridData[y, x].Value = value;
    }
    
    public GridData<T> GetGridData(int y, int x) => _gridData[y, x];

    public bool IsValid(int y, int x) =>
        y >= 0 && y < MaxY && x >= 0 && x < MaxX;
    
    public IEnumerable<GridData<T>> GetOrthogonalData(GridData<T> gridData) => GetOrthogonalData(gridData.Y, gridData.X);
    public IEnumerable<GridData<T>> GetOrthogonalData(int y, int x)
    {
        if (IsValid(y - 1, x)) yield return GetGridData(y - 1, x);
        
        if (IsValid(y, x + 1)) yield return GetGridData(y, x + 1);

        if (IsValid(y + 1, x)) yield return GetGridData(y + 1, x);

        if (IsValid(y, x - 1)) yield return GetGridData(y, x - 1);
    }

    public IEnumerator<GridData<T>> GetEnumerator()
    {
        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX; x++)
            {
                yield return _gridData[y, x];
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    public void Print()
    {
        var sb = new StringBuilder();
        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX; x++)
            {
                sb.Append(_gridData[y, x].Value);
            }
            sb.AppendLine();
        }
        
        Console.WriteLine(sb.ToString());
    }
    
    public void Print(Dictionary<T, ConsoleColor> colorMapping)
    {
        var defaultColor = Console.ForegroundColor;
        
        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX; x++)
            {
                Console.ForegroundColor = colorMapping.GetValueOrDefault(_gridData[y, x].Value, defaultColor);
                Console.Write(_gridData[y, x].Value);
            }

            Console.WriteLine();
        }
        
        Console.WriteLine();
        Console.ForegroundColor = defaultColor;
    }
}

public class GridData<T>(int y, int x, T value) : IEquatable<GridData<T>>
{
    public int Y { get; init; } = y;
    public int X { get; init; } = x;
    public T Value { get; set; } = value;

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