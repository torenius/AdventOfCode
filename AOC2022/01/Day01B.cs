namespace AOC2022._01;

public class Day01B : Day
{
    public override string Run()
    {
        var input = GetInputAsStringArray();

        var list = new List<long>();
        var currentCount = 0;

        foreach (var calorie in input)
        {
            if (calorie == "")
            {
                list.Add(currentCount);

                currentCount = 0;
            }
            else
            {
                currentCount += int.Parse(calorie);
            }
        }
        
        list.Add(currentCount);

        return list.OrderByDescending(x => x).Take(3).Sum().ToString();
    }
}
