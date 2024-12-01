namespace AOC2024._01;

public class Day01B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var listA = new List<int>();
        var listB = new List<int>();

        foreach (var item in input)
        {
            var s = item.Split("   ");
            listA.Add(int.Parse(s[0]));
            listB.Add(int.Parse(s[1]));
        }

        var sum = 0;
        foreach (var number in listA)
        {
            var appearCount = listB.Count(x => x == number);
            sum += number * appearCount;   
        }
        
        return sum;
    }
}