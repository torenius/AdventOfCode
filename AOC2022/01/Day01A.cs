namespace AOC2022._01;

public class Day01A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        var currentMax = 0;
        var currentCount = 0;

        foreach (var calorie in input)
        {
            if (calorie == "")
            {
                if (currentCount > currentMax)
                {
                    currentMax = currentCount;
                }

                currentCount = 0;
            }
            else
            {
                currentCount += int.Parse(calorie);
            }
        }
        
        if (currentCount > currentMax)
        {
            currentMax = currentCount;
        }

        return currentMax.ToString();
    }
}
