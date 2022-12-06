namespace AOC2022._06;

public class Day06A : Day
{
    public override string Run()
    {
        var input = GetInputAsString();

        for (var i = 0;  i < input.Length - 3; i++)
        {
            var span = input[i..(i + 4)];

            if (!ContainDuplicate(span))
            {
                return (i + 4).ToString();
            }
        }

        return "";
    }

    private static bool ContainDuplicate(string input)
    {
        for (var i = 0; i < input.Length - 1; i++)
        {
            for (var k = i + 1; k < input.Length; k++)
            {
                if (input[i] == input[k])
                {
                    return true;
                }
            }
        }

        return false;
    }
}
