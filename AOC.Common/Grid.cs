using System.Collections;
using System.Text;

namespace AOC.Common;

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
    
    public IEnumerable<GridData<T>> GetDiagonalData(GridData<T> gridData) => GetDiagonalData(gridData.Y, gridData.X);
    public IEnumerable<GridData<T>> GetDiagonalData(int y, int x)
    {
        if (IsValid(y - 1, x - 1)) yield return GetGridData(y - 1, x - 1);
        
        if (IsValid(y - 1, x + 1)) yield return GetGridData(y - 1, x + 1);

        if (IsValid(y + 1, x + 1)) yield return GetGridData(y + 1, x + 1);

        if (IsValid(y + 1, x - 1)) yield return GetGridData(y + 1, x - 1);
    }

    public IEnumerable<GridData<T>> GetSurroundingData(GridData<T> gridData) => GetSurroundingData(gridData.Y, gridData.X);
    public IEnumerable<GridData<T>> GetSurroundingData(int y, int x)
    {
        foreach (var orthogonal in GetOrthogonalData(y, x))
        {
            yield return orthogonal;
        }

        foreach (var diagonal in GetDiagonalData(y, x))
        {
            yield return diagonal;
        }
    }

    public IEnumerable<GridData<T>> GetDiamondData(GridData<T> center, int size) => GetDiamondData(center.Y, center.X, size);
    public IEnumerable<GridData<T>> GetDiamondData(int centerY, int centerX, int size)
    {
        if (size < 0) return [];
        if (size == 0) return [GetGridData(centerY, centerX)];
        if (size == 1) return GetOrthogonalData(centerY, centerX);
        
        var result = new List<GridData<T>>();
        result.AddRange(GetLine(centerY, centerX, size));
        for (var y = size * -1; y < 0; y++)
        {
            result.AddRange(GetLine(centerY + y, centerX, size + y));
            result.AddRange(GetLine(centerY - y, centerX, size + y));
        }

        return result;
    }

    public IEnumerable<GridData<T>> GetLine(GridData<T> center, int padding) => GetLine(center.Y, center.X, padding);
    public IEnumerable<GridData<T>> GetLine(int centerY, int centerX, int padding)
    {
        for (var x = centerX - padding; x <= centerX + padding; x++)
        {
            if (IsValid(centerY, x)) yield return GetGridData(centerY, x);
        }
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
    
    public void Print(ICollection<GridData<T>> highlight, ConsoleColor colorMapping = ConsoleColor.Red)
    {
        var defaultColor = Console.ForegroundColor;
        
        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX; x++)
            {
                if (highlight.Contains(_gridData[y, x]))
                {
                    Console.ForegroundColor = colorMapping;
                }

                Console.Write(_gridData[y, x].Value);
                Console.ForegroundColor = defaultColor;
            }

            Console.WriteLine();
        }
        
        Console.WriteLine();
        Console.ForegroundColor = defaultColor;
    }
}
