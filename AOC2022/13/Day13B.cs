namespace AOC2022._13;

public class Day13B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray()
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToList();
        
        input.Add("[[2]]");
        input.Add("[[6]]");

        var packages = input.Select(i => new Package(i)).ToList();
        packages.Sort();

        var indexForTwo = 1;
        var indexForSix = 1;
        for (var i = 0; i < packages.Count; i++)
        {
            if (packages[i].Value == "[[2]]")
            {
                indexForTwo += i;
            }
            else if (packages[i].Value == "[[6]]")
            {
                indexForSix += i;
            }
        }
        
        Console.WriteLine($"{indexForTwo} * {indexForSix} = {indexForTwo * indexForSix}");

        return "" + indexForTwo * indexForSix;
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