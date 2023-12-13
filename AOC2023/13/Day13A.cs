namespace AOC2023._13;

public class Day13A : Day
{
    protected override object Run()
    {
        var input = GetInputAsString();
        var maps = input.Split("\n\n");
        var sum = 0;
        foreach (var map in maps)
        {
            var m = map.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            
            var ver = FindVerticalReflection(m);
            sum += ver;

            var hor = FindHorizonReflection(m);
            sum += hor * 100;
        }
        
        return sum;
    }

    private static int FindVerticalReflection(string[] map)
    {
        var max = 0;
        for (var i = 0; i < map[0].Length - 1; i++)
        {
            if (IsVerticalMatch(map, i, i + 1))
            {
                max = i + 1; // +1 Since zero index
            }
        }
        
        return max;
    }

    private static bool IsVerticalMatch(string[] map, int left, int right)
    {
        while (true)
        {
            // Reached left or right, nothing left to reflect
            if (left < 0 || right == map[0].Length)
            {
                return true;
            }

            for (var i = 0; i < map.Length; i++)
            {
                if (map[i][left] != map[i][right])
                {
                    return false;
                }
            }

            left--;
            right++;
        }
    }


    private static int FindHorizonReflection(string[] map)
    {
        var max = 0;
        for (var i = 0; i < map.Length - 1; i++)
        {
            if (IsHorizonMatch(map, i, i + 1))
            {
                max = i + 1; // +1 Since zero index
            }
        }

        return max;
    }

    private static bool IsHorizonMatch(string[] map, int upIndex, int downIndex)
    {
        while (true)
        {
            // Reached top or bottom, nothing left to reflect
            if (upIndex < 0 || downIndex == map.Length)
            {
                return true;
            }

            if (!map[upIndex].Equals(map[downIndex]))
            {
                return false;
            }

            upIndex--;
            downIndex++;
        }
    }
}