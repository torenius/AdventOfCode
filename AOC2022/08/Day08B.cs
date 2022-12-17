namespace AOC2022._08;

public class Day08B : Day
{
    protected override string Run()
    {
        var input = GetInputAsIntMatrix();
        var viewDistance = new int[input.GetLength(0), input.GetLength(1)];

        for (var x = 0; x < input.GetLength(0); x++)
        {
            for (var y = 0; y < input.GetLength(1); y++)
            {
                CheckViewDistance(input, viewDistance, x, y);
            }
        }

        var max = 0;
        for (var x = 0; x < viewDistance.GetLength(0); x++)
        {
            for (var y = 0; y < viewDistance.GetLength(1); y++)
            {
                if (viewDistance[x, y] > max)
                {
                    max = viewDistance[x, y];
                }
            }
        }

        return "" + max;
    }

    private static void CheckViewDistance(int[,] input, int[,] viewDistance, int x, int y)
    {
        var maxX = input.GetLength(0);
        var maxY = input.GetLength(1);
        
        var treeSize = input[x, y];
        var totalDistance = 1;
        var currentDistance = 0;
        
        // Check up
        for (var i = x - 1; i > -1; i--)
        {
            if (input[i, y] <= treeSize)
            {
                currentDistance++;
            }
            
            if (input[i, y] >= treeSize)
            {
                break;
            }
        }
        
        totalDistance *= currentDistance;
        currentDistance = 0;
        
        // Check down
        for (var i = x + 1; i < maxY; i++)
        {
            if (input[i, y] <= treeSize)
            {
                currentDistance++;
            }
            
            if (input[i, y] >= treeSize)
            {
                break;
            }
        }
        
        totalDistance *= currentDistance;
        currentDistance = 0;
        
        // Check left
        for (var i = y - 1; i > -1; i--)
        {
            if (input[x, i] <= treeSize)
            {
                currentDistance++;
            }
            
            if (input[x, i] >= treeSize)
            {
                break;
            }
        }
        
        totalDistance *= currentDistance;
        currentDistance = 0;
        
        // Check right
        for (var i = y + 1; i < maxX; i++)
        {
            if (input[x, i] <= treeSize)
            {
                currentDistance++;
            }
            
            if (input[x, i] >= treeSize)
            {
                break;
            }
        }

        totalDistance *= currentDistance;
        viewDistance[x, y] = totalDistance;
    }
}
