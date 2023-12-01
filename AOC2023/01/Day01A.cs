namespace AOC2023._01;

public class Day01A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var sum = 0;
        foreach (var row in input)
        {
            var numbers = new List<char>();
            foreach (var c in row)
            {
                if (char.IsNumber(c))
                {
                    numbers.Add(c);
                }
            }

            var number = (numbers[0] + "" + numbers[^1]).ToInt();
            sum += number;
        }

        return sum.ToString();
    }
}
