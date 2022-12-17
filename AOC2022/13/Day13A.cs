namespace AOC2022._13;

public class Day13A : Day
{
    protected override string Run()
    {
        var input = GetInputAsString().Split("\n\n");

        var result = new List<int>();
        for (var i = 1; i <= input.Length; i++)
        {
            var packages = input[i - 1].Split("\n");
            
            var left = new Package(packages[0]);
            var right = new Package(packages[1]);
            
            Console.WriteLine();
            Console.WriteLine("== Pair " + i + " ==");

            var compareResult = left.CompareTo(right);

            if (compareResult < 0)
            {
                result.Add(i);
                Console.WriteLine("True");
            }
            else
            {
                Console.WriteLine("False");
            }
        }

        Console.WriteLine();
        Console.WriteLine(string.Join(", ", result));
        return "" + result.Sum();
    }

    private static List<string> GetPackages(string input)
    {
        var result = new List<string>();
        input = input.Substring(1, input.Length - 2);

        var isBracket = false;
        var bracketCount = 0;
        var value = "";
        foreach (var c in input)
        {
            if (isBracket)
            {
                value += c;
                if (c == ']')
                {
                    bracketCount--;
                    if (bracketCount == 0)
                    {
                        isBracket = false;
                        result.Add(value);
                        value = "";
                    }
                }
                else if (c == '[')
                {
                    bracketCount++;
                }
            }
            else if (c == '[')
            {
                isBracket = true;
                bracketCount = 1;
                value = "[";
            }
            else if (c == ',')
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    result.Add(value);
                }
                value = "";
            }
            else
            {
                value += c;
            }
        }

        if (!string.IsNullOrWhiteSpace(value))
        {
            result.Add(value);
        }
        
        return result;
    }

    private class Package : IComparable<Package>
    {
        public Package(string value)
        {
            Value = value;

            if (int.TryParse(value, out var result))
            {
                PackageType = PackageTypeEnum.Integer;
                IntegerValue = result;
            }
            else
            {
                PackageType = PackageTypeEnum.List;
                foreach (var p in GetPackages(value))
                {
                    ListValues.Add(new Package(p));
                }
            }
        }
        public string Value { get; set; }
        public PackageTypeEnum PackageType { get; set; }
        public int IntegerValue { get; set; }
        public List<Package> ListValues { get; set; } = new();

        public enum PackageTypeEnum
        {
            Integer,
            List
        }

        public int CompareTo(Package other)
        {
            while (true)
            {
                // If both values are integers
                if (PackageType == PackageTypeEnum.Integer && other.PackageType == PackageTypeEnum.Integer)
                {
                    return IntegerValue.CompareTo(other.IntegerValue);
                }

                // If both values are lists
                if (PackageType == PackageTypeEnum.List && other.PackageType == PackageTypeEnum.List)
                {
                    var length = Math.Max(ListValues.Count, other.ListValues.Count);
                    for (var i = 0; i < length; i++)
                    {
                        // Left side ran out of items, so inputs are in the right order
                        if (i >= ListValues.Count)
                        {
                            return -1;
                        }

                        // Right side ran out of items, so inputs are not in the right order
                        if (i >= other.ListValues.Count)
                        {
                            return 1;
                        }

                        var result = ListValues[i].CompareTo(other.ListValues[i]);
                        if (result != 0)
                        {
                            return result;
                        }
                    }

                    return 0;
                }

                // If exactly one value is an integer
                if (PackageType == PackageTypeEnum.Integer)
                {
                    PackageType = PackageTypeEnum.List;
                    ListValues.Add(new Package(Value));
                }
                else if (other.PackageType == PackageTypeEnum.Integer)
                {
                    other.PackageType = PackageTypeEnum.List;
                    other.ListValues.Add(new Package(other.Value));
                }
            }
        }
    }
}