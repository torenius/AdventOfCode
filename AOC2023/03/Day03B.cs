namespace AOC2023._03;

public class Day03B : Day
{
    protected override string Run()
    {
        var input = GetInputAsCharMatrix();
        var parts = new List<Part>();
        for (var y = 0; y < input.GetLength(0); y++)
        {
            for (var x = 0; x < input.GetLength(1); x++)
            {
                if (input[y, x] != '.' && !char.IsNumber(input[y, x]))
                {
                    parts.Add(GetPart(input, y, x));
                }
            }
        }

        var gears = parts.Where(x => x.Symbol == '*' && x.Numbers.Count == 2).ToList();
        var sum = 0;
        foreach (var gear in gears)
        {
            sum += gear.Numbers[0].Value * gear.Numbers[1].Value;
        }

        return "" + sum;
    }

    private static Part GetPart(char[,] input, int symbolY, int symbolX)
    {
        var lengthY = input.GetLength(0);
        var lengthX = input.GetLength(1);
        var part = new Part
        {
            Symbol = input[symbolY, symbolX]
        };
        foreach (var cord in GetAdjacentCoordinates(symbolY, symbolX))
        {
            if (cord.y >= 0 && cord.y < lengthY && cord.x >= 0 && cord.x < lengthX)
            {
                if (char.IsNumber(input[cord.y, cord.x]))
                {
                    part.Numbers.Add(GetNumber(input, cord.y, cord.x));
                }
            }
        }

        part.Numbers = part.Numbers.Distinct().ToList();

        return part;
    }

    private class Part
    {
        public char Symbol { get; set; }
        public List<Number> Numbers { get; set; } = new();
    }

    private static List<(int y, int x)> GetAdjacentCoordinates(int y, int x) => new()
    {
        {(y - 1, x - 1)}, {(y - 1, x)}, {(y - 1, x + 1)},
        {(y - 0, x - 1)}, {(y - 0, x)}, {(y - 0, x + 1)},
        {(y + 1, x - 1)}, {(y + 1, x)}, {(y + 1, x + 1)}
    };

    private static Number GetNumber(char[,] input, int partOfNumberY, int partOfNumberX)
    {
        var minX = partOfNumberX;
        var maxX = partOfNumberX;
        
        // Check Left
        while (--partOfNumberX >= 0 && char.IsNumber(input[partOfNumberY, partOfNumberX]))
        {
            minX = partOfNumberX;
        }
        
        // Check Right
        var length = input.GetLength(1);
        while (++partOfNumberX < length && char.IsNumber(input[partOfNumberY, partOfNumberX]))
        {
            maxX = partOfNumberX;
        }

        var temp = "";
        for (var x = minX; x <= maxX; x++)
        {
            temp += input[partOfNumberY, x];
        }
        
        return new Number
        {
            StartY = partOfNumberY,
            StartX = minX,
            Value = temp.ToInt()
        };
    }

    private class Number : IEquatable<Number>
    {
        public int StartY { get; set; }
        public int StartX { get; set; }
        public int Value { get; set; }

        public bool Equals(Number? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return StartY == other.StartY && StartX == other.StartX && Value == other.Value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Number) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartY, StartX, Value);
        }
    }
}