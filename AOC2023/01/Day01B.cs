namespace AOC2023._01;

public class Day01B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        var dic = new Dictionary<string, string>
        {
            {"1", "1"},
            {"2", "2"},
            {"3", "3"},
            {"4", "4"},
            {"5", "5"},
            {"6", "6"},
            {"7", "7"},
            {"8", "8"},
            {"9", "9"},
            {"one", "1"},
            {"two", "2"},
            {"three", "3"},
            {"four", "4"},
            {"five", "5"},
            {"six", "6"},
            {"seven", "7"},
            {"eight", "8"},
            {"nine", "9"}
        };
        
        var sum = 0;
        foreach (var row in input)
        {
            var result = new List<(int, string)>();
            foreach (var kvp in dic)
            {
                result.AddRange(GetIndexes(row, kvp.Key, kvp.Value));
            }

            var rowAsNumbers = "";
            foreach (var kvp in result.OrderBy(x => x.Item1))
            {
                rowAsNumbers += kvp.Item2;
            }
            
            var numbers = new List<char>();
            foreach (var c in rowAsNumbers)
            {
                if (char.IsNumber(c))
                {
                    numbers.Add(c);
                }
            }

            var number = (numbers[0] + "" + numbers[^1]).ToInt();
            Console.WriteLine($"Row: {row}, rowAsNumbers: {rowAsNumbers}, Number: {number}");
            sum += number;
        }

        return sum.ToString();
    }

    private List<(int index, string value)> GetIndexes(string row, string key, string value)
    {
        var result = new List<(int, string)>();
        var index = 0;
        while (true)
        {
            index = row.IndexOf(key, index);
            if (index == -1)
            {
                return result;
            }
            
            result.Add((index, value));
            index++;
        }
    }
}
