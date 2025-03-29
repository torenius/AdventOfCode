using AOC.Common;

namespace AOC2024._21;

public class Day21B : Day
{
    private DirectionalKeyPad _directionalKeyPad;
    protected override object Run()
    {
        _directionalKeyPad = new DirectionalKeyPad(25);
        
        long sum = 0;
        var input = GetInputAsStringArray();
        
        foreach (var code in input)
        {
            var min = GetMinCommands(code);
            Console.WriteLine($"{code}: {min} {min}");
            sum += min * code[..3].ToInt();
        }

        return sum;
    }

    private long GetMinCommands(string code)
    {
        var keyPad = new NumericKeyPad();
        
        var first = ApplyKeypads(keyPad.GetOptions(code[0]));
        var second = ApplyKeypads(keyPad.GetOptions(code[1]));
        var third = ApplyKeypads(keyPad.GetOptions(code[2]));
        var fourth = ApplyKeypads(keyPad.GetOptions(code[3]));
        
        return first + second + third + fourth;
    }

    private long ApplyKeypads(List<string> options)
    {
        var minValue = long.MaxValue;
        foreach (var option in options)
        {
            _directionalKeyPad.Fill(option);
            var min = _directionalKeyPad.GetMinLength2(option);
            if (min < minValue)
            {
                minValue = min;
            }
        }
        
        return minValue;
    }

    private class NumericKeyPad
    {
        public NumericKeyPad()
        {
            var keypad = new char[4, 3];
            keypad[0, 0] = '7'; keypad[0, 1] = '8'; keypad[0, 2] = '9';
            keypad[1, 0] = '4'; keypad[1, 1] = '5'; keypad[1, 2] = '6';
            keypad[2, 0] = '1'; keypad[2, 1] = '2'; keypad[2, 2] = '3';
            keypad[3, 0] = '#'; keypad[3, 1] = '0'; keypad[3, 2] = 'A';

            Grid = new Grid<char>(keypad);
        }
        
        private Grid<char> Grid { get; set; }

        private char CurrentPosition { get; set; } = 'A';

        public List<string> GetOptions(char to)
        {
            var result = GetOptions(CurrentPosition, to);
            CurrentPosition = to;
            return result;
        }
        private List<string> GetOptions(char from, char to) =>
            ShortestPaths(from, to, Grid, (neighbour) => GetNeighbours(neighbour, Grid));
    }

    private class DirectionalKeyPad
    {
        public DirectionalKeyPad(int depth)
        {
            Depth = depth;
            PreFill();
        }

        private int Depth { get; set; }

        private Dictionary<string, long> MinLengths { get; set; } = [];
        private Dictionary<string, long> MinLengths2 { get; set; } = [];
        
        public long GetMinLength2(string path)
        {
            if (MinLengths2.TryGetValue(path + Depth, out var result)) return result;

            GetMinLength2(path, Depth);
            
            return MinLengths2[path + Depth];
        }
        
        private long GetMinLength2(string path, int currentDepth)
        {
            if (MinLengths2.TryGetValue(path + currentDepth, out var result)) return result;
            
            var command = Commands.First(c => c.Input == path);
            var minLength = long.MaxValue;
            foreach (var paths in command.Output)
            {
                long sum = 0;
                foreach (var p in paths)
                {
                    sum += GetMinLength2(p, currentDepth - 1);
                }

                if (sum < minLength)
                {
                    minLength = sum;
                }
            }
            
            MinLengths2.Add(path + currentDepth, minLength);
            
            return minLength;
        }
        

        private List<Command> Commands { get; set; } = [];
        public void Fill(string input)
        {
            if (Commands.Any(c => c.Input == input)) return;
            
            var current = 'A';
            
            var temp = new List<List<string>>();
            for(var i = 0; i < input.Length; i++)
            {
                var options = GetOptions(current, input[i]);
                temp.Add(options);
                current = input[i];
            }

            var command = GetCommand(input, temp);
            Commands.Add(command);
            
            MinLengths2.Add(input + "0", input.Length);
        }

        private void PreFill()
        {
            foreach (var possibleCommands in _directionalPaths.Values.SelectMany(v => v).Distinct())
            {
                Fill(possibleCommands);
            }
            foreach (var command in Commands.SelectMany(c => c.Output).SelectMany(o => o).Distinct())
            {
                var length = GetMinLength2(command);
            }
        }
        
        private List<string> GetOptions(char from, char to) => _directionalPaths[(from, to)];
        
        private readonly Dictionary<(char, char), List<string>> _directionalPaths = new()
        {
            { ('A', 'A'), ["A"] },
            { ('A', '^'), ["<A"] },
            { ('A', '>'), ["vA"] },
            { ('A', 'v'), ["<vA", "v<A"] },
            { ('A', '<'), ["v<<A", "<v<A"] },
        
            { ('^', '^'), ["A"] },
            { ('^', 'A'), [">A"] },
            { ('^', '>'), [">vA", "v>A"] },
            { ('^', 'v'), ["vA"] },
            { ('^', '<'), ["v<A"] },
        
            { ('>', '>'), ["A"] },
            { ('>', 'A'), ["^A"] },
            { ('>', '^'), ["<^A", "^<A"] },
            { ('>', 'v'), ["<A"] },
            { ('>', '<'), ["<<A"] },
        
            { ('v', 'v'), ["A"] },
            { ('v', 'A'), [">^A", "^>A"] },
            { ('v', '^'), ["^A"] },
            { ('v', '>'), [">A"] },
            { ('v', '<'), ["<A"] },
        
            { ('<', '<'), ["A"] },
            { ('<', 'A'), [">>^A", ">^>A"] },
            { ('<', '^'), [">^A"] },
            { ('<', 'v'), [">A"] },
            { ('<', '>'), [">>A"] },
        };
    }

    private class Command
    {
        public string Input { get; set; }
        public List<List<string>> Output { get; set; } = [];
    }

    private static Command GetCommand(string input, List<List<string>> possibleOutputs)
    {
        var command = new Command
        {
            Input = input
        };

        foreach (var first in possibleOutputs[0])
        {
            if (possibleOutputs.Count > 1)
            {
                foreach (var second in possibleOutputs[1])
                {
                    if (possibleOutputs.Count > 2)
                    {
                        foreach (var third in possibleOutputs[2])
                        {
                            if (possibleOutputs.Count > 3)
                            {
                                foreach (var fourth in possibleOutputs[3])
                                {
                                    if (possibleOutputs.Count > 4)
                                    {
                                        foreach (var fifth in possibleOutputs[4])
                                        {
                                            var output = new List<string> { first, second, third, fourth, fifth };
                                            command.Output.Add(output);
                                        }
                                    }
                                    else
                                    {
                                        var output = new List<string> { first, second, third, fourth };
                                        command.Output.Add(output);
                                    }
                                }
                            }
                            else
                            {
                                var output = new List<string> { first, second, third };
                                command.Output.Add(output);
                            }
                        }
                    }
                    else
                    {
                        var output = new List<string> { first, second };
                        command.Output.Add(output);
                    }
                }
            }
            else
            {
                var output = new List<string> { first };
                command.Output.Add(output);
            }
        }

        return command;
    }
    
    private static List<string> ShortestPaths(char fromKey, char toKey, Grid<char> grid, Func<GridData<char>, IEnumerable<(GridData<char> Neighbour, int Cost)>> neighbourWithCost){

        var start = grid.First(gd => gd.Value == fromKey);
        var end = grid.First(gd => gd.Value == toKey);

        var shortestPaths = Dijkstra.GetShortestPaths(start, end,
            grid.Where(gd => gd.Value != '#'), (x) => neighbourWithCost(x).Where(n => n.Neighbour.Value != '#'));

        var results = new List<string>();
        foreach (var shortestPath in shortestPaths)
        {
            
            var result = "";
            for (var i = shortestPath.Count - 1; i > 0; i--)
            {
                var from = shortestPath[i];
                var to = shortestPath[i - 1];
                if (from.Node.Y > to.Node.Y) result += '^';
                else if (from.Node.Y < to.Node.Y) result += 'v';
                else if (from.Node.X > to.Node.X) result += '<';
                else if (from.Node.X < to.Node.X) result += '>';
                else result += 'A'; // Same direction
            }

            result += 'A';
            results.Add(result);
        }
        
        return results;
    }
    
    private static IEnumerable<(GridData<char> Neighbour, int Cost)> GetNeighbours(GridData<char> gridData, Grid<char> grid)
    {
        var y = gridData.Y;
        var x = gridData.X;

        yield return (gridData, 1);
        if (grid.IsValid(y, x + 1)) yield return (grid.GetGridData(y, x + 1), 1);
        if (grid.IsValid(y, x - 1)) yield return (grid.GetGridData(y, x - 1), 1);
        if (grid.IsValid(y - 1, x)) yield return (grid.GetGridData(y - 1, x), 1);
        if (grid.IsValid(y + 1, x)) yield return (grid.GetGridData(y + 1, x), 1);
    }
}