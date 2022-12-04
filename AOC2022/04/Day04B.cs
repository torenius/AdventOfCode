namespace AOC2022._04;

public class Day04B : Day
{
    public override long Run()
    {
        var input = GetInputRowAsStringArray();

        var overlappingPairs = 0;
        foreach (var row in input)
        {
            if (IsOverlapping(row[0], row[1]))
            {
                overlappingPairs++;
            }
        }

        return overlappingPairs;
    }

    private static bool IsOverlapping(string first, string second)
    {
        var firstElf = GetRange(first);
        var secondElf = GetRange(second);

        return firstElf.from <= secondElf.to && firstElf.to >= secondElf.from;
    }

    private static (int from, int to) GetRange(string elf)
    {
        var range = elf.Split("-");

        return (int.Parse(range[0]), int.Parse(range[1]));
    }
}