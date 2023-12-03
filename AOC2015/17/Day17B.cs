namespace AOC2015._17;

public class Day17B : Day
{
    protected override string Run()
    {
        const int eggnog = 150;
        var input = GetInputAsIntArray();
        var result = new List<List<int>>();
        for (var i = 0; i < input.Length - 1; i++)
        {
            result.AddRange(Containers(input, i, new List<int>{input[i]}, eggnog));
        }

        var minContainerCount = result.Min(x => x.Count);
        var numberOfCombinations = result.Count(x => x.Count == minContainerCount);

        return "" + numberOfCombinations;
    }

    private List<List<int>> Containers(int[] input, int index, List<int> containers, int eggnog)
    {
        var result = new List<List<int>>();
        var containerSum = containers.Sum();
        for (var i = index + 1; i < input.Length; i++)
        {
            if (containerSum + input[i] == eggnog)
            {
                var temp = containers.ToList();
                temp.Add(input[i]);
                result.Add(temp);
            }
            else if (containerSum + input[i] < eggnog)
            {
                var temp = containers.ToList();
                temp.Add(input[i]);
                result.AddRange(Containers(input, i, temp, eggnog));
            }
        }

        return result;
    }
}