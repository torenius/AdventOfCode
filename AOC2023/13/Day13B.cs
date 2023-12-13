namespace AOC2023._13;

public class Day13B : Day
{
    protected override object Run()
    {
        var input = GetInputAsString();
        var maps = input.Split("\n\n");
        var sum = 0;
        foreach (var map in maps)
        {
            var m = MapToMatrix(map);
            
            var oldVer = FindVerticalReflection(m).FirstOrDefault();
            var oldHor = FindHorizonReflection(m).FirstOrDefault();
            
            for (var y = 0; y < m.GetLength(0); y++)
            {
                for (var x = 0; x < m.GetLength(1); x++)
                {
                    var temp = m[y, x];
                    if (m[y, x] == '#')
                    {
                        m[y, x] = '.';
                    }
                    else
                    {
                        m[y, x] = '#';
                    }
                    
                    var ver = FindVerticalReflection(m);
                    var hor = FindHorizonReflection(m);
                    
                    var verResult = ver.FirstOrDefault(v => v != oldVer);
                    var horResult = hor.FirstOrDefault(h => h != oldHor);

                    if (verResult != 0 || horResult != 0)
                    {
                        sum += verResult;
                        sum += horResult * 100;
                        
                        // Break Loop
                        y = m.GetLength(0);
                        x = m.GetLength(1);
                    }
                    else
                    {
                        m[y, x] = temp;
                    }
                }
            }
        }
        
        return sum;
    }

    private static char[,] MapToMatrix(string map)
    {
        var m = map.Split("\n", StringSplitOptions.RemoveEmptyEntries);
        var matrix = new char[m.Length, m[0].Length];
        for (var y = 0; y < m.Length; y++)
        {
            for (var x = 0; x < m[y].Length; x++)
            {
                matrix[y, x] = m[y][x];
            }
        }

        return matrix;
    }

    private static List<int> FindVerticalReflection(char[,] map)
    {
        var length = map.GetLength(1);
        var result = new List<int>();
        for (var i = 0; i < length - 1; i++)
        {
            if (IsVerticalMatch(map, i, i + 1))
            {
                result.Add(i + 1); // +1 Since zero index
            }
        }
        
        return result;
    }

    private static bool IsVerticalMatch(char[,] map, int left, int right)
    {
        var lengthY = map.GetLength(0);
        var lengthX = map.GetLength(1);
        while (true)
        {
            // Reached left or right, nothing left to reflect
            if (left < 0 || right == lengthX)
            {
                return true;
            }

            for (var i = 0; i < lengthY; i++)
            {
                if (map[i, left] != map[i, right])
                {
                    return false;
                }
            }

            left--;
            right++;
        }
    }


    private static List<int> FindHorizonReflection(char[,] map)
    {
        var length = map.GetLength(0);
        var result = new List<int>();
        for (var i = 0; i < length - 1; i++)
        {
            if (IsHorizonMatch(map, i, i + 1))
            {
                result.Add(i + 1); // +1 Since zero index
            }
        }

        return result;
    }

    private static bool IsHorizonMatch(char[,] map, int upIndex, int downIndex)
    {
        var lengthY = map.GetLength(0);
        var lengthX = map.GetLength(1);
        while (true)
        {
            
            // Reached top or bottom, nothing left to reflect
            if (upIndex < 0 || downIndex == lengthY)
            {
                return true;
            }

            for (var i = 0; i < lengthX; i++)
            {
                if (map[upIndex, i] != map[downIndex, i])
                {
                    return false;
                }
            }

            upIndex--;
            downIndex++;
        }
    }
}