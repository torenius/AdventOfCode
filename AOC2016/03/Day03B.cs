namespace AOC2016._03;

public class Day03B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var triangleCount = 0;
        for (var i = 0; i < input.Length; i += 3)
        {
            var row1 = input[i].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var row2 = input[i + 1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var row3 = input[i + 2].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            
            if(IsTriangle(row1[0], row2[0], row3[0])) triangleCount++;
            if(IsTriangle(row1[1], row2[1], row3[1])) triangleCount++;
            if(IsTriangle(row1[2], row2[2], row3[2])) triangleCount++;
        }

        return triangleCount;
    }

    private static bool IsTriangle(string sideA, string sideB, string sideC)
    {
        var a = sideA.ToInt();
        var b = sideB.ToInt();
        var c = sideC.ToInt();

        return a + b > c && a + c > b && b + c > a;
    }
}