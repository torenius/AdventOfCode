using System.Text.RegularExpressions;

namespace AOC2022._05;

public class Day05B : Day
{
    protected override string Run()
    {
        var input = GetInputAsString().Split("\n\n");
        var stackStart = input[0].Split('\n');
        var movements = input[1].Split('\n');

        var stackCount = stackStart[0].SplitEveryN(4).Length;
        var stacks = new Dictionary<int, Stack<string>>();
        for (var i = 0; i < stackCount; i++)
        {
            stacks.Add(i, new Stack<string>());
        }

        for (var i = stackStart.Length - 2; i >= 0; i--)
        {
            var row = stackStart[i].SplitEveryN(4);
            for (var k = 0; k < row.Length; k++)
            {
                if (!string.IsNullOrWhiteSpace(row[k]))
                {
                    stacks[k].Push(row[k].Trim());
                }
            }
        }

        // Move
        var tempStack = new Stack<string>();
        foreach (var command in GetCommands(movements))
        {
            for (var i = 0; i < command.MoveCount; i++)
            {
                var crate = stacks[command.From].Pop();
                tempStack.Push(crate);
            }
            
            for (var i = 0; i < command.MoveCount; i++)
            {
                var crate = tempStack.Pop();
                stacks[command.To].Push(crate);
            }
        }
        
        // Check
        var result = "";
        for (var i = 0; i < stackCount; i++)
        {
            var stackValue = stacks[i].Peek();
            result += stackValue.Trim(new []{'[', ']'});
        }

        return result;
    }

    private readonly Regex _movementRegex = new Regex("move ([0-9]+) from ([0-9]+) to ([0-9]+)");
    private IEnumerable<(int MoveCount, int From, int To)> GetCommands(IEnumerable<string> movements)
    {
        foreach (var movement in movements.Where(m => !string.IsNullOrWhiteSpace(m)))
        {
            var row = _movementRegex.Match(movement);

            yield return (row.Groups[1].Value.ToInt(), row.Groups[2].Value.ToInt() - 1, row.Groups[3].Value.ToInt() - 1);
        }
    }
}
