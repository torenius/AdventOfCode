namespace AOC2022._06;

public class Day06B : Day
{
    protected override string Run()
    {
        var input = GetInputAsString();

        for (var i = 0;  i < input.Length - 13; i++)
        {
            var span = input[i..(i + 14)];

            if (!ContainDuplicate(span))
            {
                return (i + 14).ToString();
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
