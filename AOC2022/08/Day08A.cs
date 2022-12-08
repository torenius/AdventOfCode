namespace AOC2022._08;

public class Day08A : Day
{
    public override string Run()
    {
        var input = GetInputAsIntMatrix();
        var visible = new bool[input.GetLength(0), input.GetLength(1)];

        for (var x = 0; x < input.GetLength(0); x++)
        {
            for (var y = 0; y < input.GetLength(1); y++)
            {
                CheckAllDirections(input, visible, x, y);
            }
        }

        var sum = 0;
        for (var x = 0; x < visible.GetLength(0); x++)
        {
            for (var y = 0; y < visible.GetLength(1); y++)
            {
                if (visible[x, y])
                {
                    sum++;
                }
            }
        }

        return "" + sum;
    }

    private static void CheckAllDirections(int[,] input, bool[,] visible, int x, int y)
    {
        var maxX = input.GetLength(0);
        var maxY = input.GetLength(1);
        
        var treeSize = input[x, y];
        
        // Check up
        for (var i = x - 1; i >= -1; i--)
        {
            if (i == -1)
            {
                visible[x, y] = true;
                return;
            }
            
            if (input[i, y] >= treeSize)
            {
                break;
            }
        }
        
        // Check down
        for (var i = x + 1; i <= maxY; i++)
        {
            if (i == maxY)
            {
                visible[x, y] = true;
                return;
            }
            
            if (input[i, y] >= treeSize)
            {
                break;
            }
        }
        
        // Check left
        for (var i = y - 1; i >= -1; i--)
        {
            if (i == -1)
            {
                visible[x, y] = true;
                return;
            }
            
            if (input[x, i] >= treeSize)
            {
                break;
            }
        }
        
        // Check right
        for (var i = y + 1; i <= maxX; i++)
        {
            if (i == maxX)
            {
                visible[x, y] = true;
                return;
            }
            
            if (input[x, i] >= treeSize)
            {
                break;
            }
        }
    }
}
