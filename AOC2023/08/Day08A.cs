namespace AOC2023._08;

public class Day08A : Day
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

        var count = 0;
        var position = "AAA";
        do
        {
            count++;
            var location = map[position];
            var next = direction.NextStep();
            if (next == 'L')
            {
                position = location.Left;
            }
            else
            {
                position = location.Right;
            }

        } while (position != "ZZZ");
        
        return count;
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