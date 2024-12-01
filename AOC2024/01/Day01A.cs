namespace AOC2024._01;

public class Day01A : Day
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
        
        listA.Sort();
        listB.Sort();

        var sum = 0;
        for (var i = 0; i < listA.Count; i++)
        {
            sum += Math.Abs(listA[i] - listB[i]);
        }
        
        return sum;
    }
}