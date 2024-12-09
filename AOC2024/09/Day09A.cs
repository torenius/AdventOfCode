namespace AOC2024._09;

public class Day09A : Day
{
    protected override object Run()
    {
        var input = GetInputAsString().TrimEnd(Environment.NewLine.ToCharArray());

        var diskMap = new List<int>();
        for (var mapIndex = 0; mapIndex < input.Length; mapIndex++)
        {
            diskMap.Add(input[mapIndex].ToInt());
        }

        var blocks = new List<int>();
        
        var currentMaxId = (input.Length - 1) / 2;
        var endIndex = input.Length - 1;
        
        var i = 0;
        var id = 0;
        while (i < input.Length && i < endIndex)
        {
            // Existing space
            if (i % 2 == 0)
            {
                for (var b = 0; b < diskMap[i]; b++)
                {
                    blocks.Add(id);
                }
                id++;
            }
            // Zero space
            else
            {
                for (var b = 0; b < diskMap[i]; b++)
                {
                    var endBlock = diskMap[endIndex];
                    while (endBlock == 0 && endIndex > i)
                    {
                        endIndex-=2;
                        endBlock = diskMap[endIndex];
                        currentMaxId--;
                    }

                    if (endIndex <= i) break;
                    
                    blocks.Add(currentMaxId);
                    diskMap[endIndex]--;
                }
            }

            i++;
        }

        if (diskMap[endIndex] != 0 && i == endIndex)
        {
            for (var b = 0; b < diskMap[endIndex]; b++)
            {
                blocks.Add(currentMaxId);
            }
        }

        var multiplier = 0;
        long sum = 0;
        for (var b = 0; b < blocks.Count; b++)
        {
            sum += blocks[b] * multiplier;
            multiplier++;
        }
        
        return sum;
    }
}