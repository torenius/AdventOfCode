namespace AOC2023._08;

public class Day08B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var direction = new Direction(input[0]);

        var map = new Dictionary<string, (string Left, string Right)>();
        for (var i = 2; i < input.Length; i++)
        {
            var temp = input[i].Split(" = ");
            var leftRight = temp[1].Replace("(", "").Replace(")", "").Split(", ");
            map.Add(temp[0], (leftRight[0], leftRight[1]));
        }

        var a = map.Where(x => x.Key[2] == 'A').ToList();
        var z = map.Where(x => x.Key[2] == 'Z').ToList();

        var paths = new List<Path>();
        foreach (var kvpA in a)
        {
            foreach (var kvpZ in z)
            {
                if (kvpA.Value.Left == kvpZ.Value.Right && kvpA.Value.Right == kvpZ.Value.Left)
                {
                    paths.Add(new Path
                    {
                        From = kvpA.Key,
                        To = kvpZ.Key
                    });
                }
            }
        }

        foreach (var path in paths)
        {
            path.Steps = NumberOfSteps(map, direction, path.From, path.To);
        }
        
        var steps = paths.Select(x => x.Steps).ToList();
        var lcm = steps.LCM();
        
        return lcm;
    }
    
    private static long NumberOfSteps(Dictionary<string, (string Left, string Right)> map, Direction direction, string start,
        string end)
    {
        direction.Index = 0;
        var count = 0;
        var position = start;
        do
        {
            count++;
            var location = map[position];
            var next = direction.NextStep();
            position = next == 'L' ? location.Left : location.Right;

        } while (position != end);
        
        return count;
    }

    private class Path
    {
        public string From { get; set; }
        public string To { get; set; }
        public long Steps { get; set; }
    }

    private class Direction
    {
        public Direction(string input)
        {
            Steps = input.ToCharArray();
        }

        public char[] Steps { get; set; }
        public int Index { get; set; }

        public char NextStep()
        {
            var step = Steps[Index];
            
            Index++;
            if (Index == Steps.Length)
            {
                Index = 0;
            }

            return step;
        }
    }
}