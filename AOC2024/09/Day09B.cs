using AOC.Common;

namespace AOC2024._09;

public class Day09B : Day
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
                    blocks.Add(-1);
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

        for (var b = blocks.Count - 1; b > 0; b--)
        {
            if (blocks[b] == -1) continue;
            var bFrom = b;
            while (bFrom > 0 && blocks[bFrom - 1] == blocks[b])
            {
                bFrom--;
            }
            
            if (bFrom == 0) break;
            
            var blockSize = b - bFrom + 1;
            
            for (var f = 0; f <= bFrom - blockSize; f++)
            {
                if (blocks[f] == -1 && blocks[f + blockSize - 1] == -1 && blocks[f..(f + blockSize)].All(x => x == -1))
                {
                    for (var s = 0; s < blockSize; s++)
                    {
                        blocks[f + s] = blocks[b];
                        blocks[bFrom + s] = -1;
                    }
                    
                    // for (var k = 0; k < blocks.Count; k++)
                    // {
                    //     if (blocks[k] == -1)
                    //     {
                    //         Console.Write('.');
                    //     }
                    //     else
                    //     {
                    //         Console.Write(blocks[k]);
                    //     }
                    // }
                    // Console.WriteLine();
                    break;
                }
            }
            
            b = bFrom;
        }
        

        var multiplier = 0;
        long sum = 0;
        for (var b = 0; b < blocks.Count; b++)
        {
            var block = blocks[b];
            if (block != -1)
            {
                sum += blocks[b] * multiplier;   
            }
            multiplier++;
        }
        
        return sum;
    }
}