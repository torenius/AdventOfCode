using AOC.Common;

namespace AOC2016._03;

public class Day03A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var triangleCount = 0;
        foreach (var row in input)
        {
            var data = row.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var a = data[0].ToInt();
            var b = data[1].ToInt();
            var c = data[2].ToInt();
            
            if (a + b > c && a + c > b && b + c > a)
            {
                triangleCount++;
            }
        }

        return triangleCount;
    }
}