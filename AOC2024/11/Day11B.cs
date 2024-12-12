namespace AOC2024._11;

public class Day11B : Day
{
    protected override object Run()
    {
        var input = GetInputAsString().TrimEnd(Environment.NewLine.ToCharArray()).Split(" ").ToLongArray();

        var result = new Dictionary<long, List<long>>();
        // Blink 25
        foreach (var value in input)
        {
            result.Add(value, Blink(value));
        }
        
        // Blink 50
        var valuesToCheck = result.Values.SelectMany(v => v).Distinct().Where(v => !result.ContainsKey(v)).ToList();
        foreach (var value in valuesToCheck)
        {
            result.Add(value, Blink(value));
        }
        
        // Blink 75
        valuesToCheck = result.Values.SelectMany(v => v).Distinct().Where(v => !result.ContainsKey(v)).ToList();
        foreach (var value in valuesToCheck)
        {
            result.Add(value, Blink(value));
        }

        long stoneCount = 0;
        foreach (var value in input)
        {
            // 25 Blink
            foreach (var secondBatch in result[value])
            {
                // 50 Blink
                foreach (var thirdBatch in result[secondBatch])
                {
                    // 75 Blink
                    stoneCount += result[thirdBatch].Count;
                }
            }
        }
        
        return stoneCount;
    }

    private static List<long> Blink(long value)
    {
        var result = new List<long> {value};
        
        var blinkCount = 0;
        while (blinkCount < 25)
        {
            var beforeBlink = result.ToList();
            result.Clear();
            for (var i = 0; i < beforeBlink.Count; i++)
            {
                if (beforeBlink[i] == 0)
                {
                    result.Add(1);
                    continue;
                }
                
                var s = beforeBlink[i].ToString();
                if (s.Length % 2 == 0)
                {
                    result.Add(s[..(s.Length / 2)].ToLong());
                    result.Add(s[(s.Length / 2)..].ToLong());
                    continue;
                }
                
                result.Add(beforeBlink[i] * 2024);
            }

            blinkCount++;
        }

        return result;
    }
}
