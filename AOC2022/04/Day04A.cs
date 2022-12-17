namespace AOC2022._04;

public class Day04A : Day
{
    protected override string Run()
    {
        var input = GetInputRowAsStringArray();

        var fullContainingPairs = 0;
        foreach (var row in input)
        {
            if (IsFullyContained(row[0], row[1]))
            {
                fullContainingPairs++;
            }
        }

        return fullContainingPairs.ToString();
    }

    private static bool IsFullyContained(string first, string second)
    {
        var firstElf = GetRange(first);
        var secondElf = GetRange(second);

        return firstElf.from >= secondElf.from && firstElf.to <= secondElf.to ||
               secondElf.from >= firstElf.from && secondElf.to <= firstElf.to;
    }

    private static (int from, int to) GetRange(string elf)
    {
        var range = elf.Split("-");

        return (int.Parse(range[0]), int.Parse(range[1]));
    }
}
