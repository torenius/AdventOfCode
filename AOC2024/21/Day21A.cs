namespace AOC2024._21;

public class Day21A : Day
{
    protected override object Run()
    {
        var sum = 0;
        var input = GetInputAsStringArray();
        foreach (var code in input)
        {
            var min = GetMinCommands(code);
            Console.WriteLine($"{code}: {min} {min.Length}");
            sum += min.Length * code[..3].ToInt();
        }

        return sum;
    }

    private string GetMinCommands(string code)
    {
        var keyPad = new NumericKeyPad();

        var first = GetDirectionalOptions(GetDirectionalOptions(keyPad.GetOptions(code[0])));
        var second = GetDirectionalOptions(GetDirectionalOptions(keyPad.GetOptions(code[1])));
        var third = GetDirectionalOptions(GetDirectionalOptions(keyPad.GetOptions(code[2])));
        var fourth = GetDirectionalOptions(GetDirectionalOptions(keyPad.GetOptions(code[3])));

        return first.MinBy(f => f.Length) + second.MinBy(l => l.Length) + third.MinBy(t => t.Length) + fourth.MinBy(f => f.Length);
    }

    private List<string> GetDirectionalOptions(List<string> options)
    {
        var result = new List<string>();
        foreach (var option in options)
        {
            var directionalKeyPad = new DirectionalKeyPad();
            foreach (var command in directionalKeyPad.GetOptions(option))
            {
                result.Add(command);
            }
        }
        
        return result;
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
        public DirectionalKeyPad()
        {
            var keyPad = new char[2, 3];
            keyPad[0, 0] = '#'; keyPad[0, 1] = '^'; keyPad[0, 2] = 'A';
            keyPad[1, 0] = '<'; keyPad[1, 1] = 'v'; keyPad[1, 2] = '>';

            Grid = new Grid<char>(keyPad);
        }
        
        private Grid<char> Grid { get; set; }
        
        public List<string> GetOptions(string to)
        {
            var current = 'A';
            
            var temp = new List<List<string>>();
            for(var i = 0; i < to.Length; i++)
            {
                var options = GetOptions(current, to[i]);
                temp.Add(options);
                current = to[i];
            }
            
            return AllCombinations(temp);
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
    
    private static List<string> AllCombinations(List<List<string>> input)
    {
        var temp = input[0];
        var restOfList = input[1..];
            
        var result = new List<string>();
        foreach (var t in temp)
        {
            if (restOfList.Count > 0)
            {
                foreach (var rest in AllCombinations(restOfList))
                {
                    result.Add(t + rest);
                }
            }
            else
            {
                result.Add(t);
            }
        }

        return result;
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