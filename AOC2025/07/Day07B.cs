using AOC.Common;

namespace AOC2025._07;

public class Day07B : Day
{
    private Grid<char> _grid = new(0,0, '.');
    private readonly Dictionary<GridData<char>, long> _paths = [];
    
    protected override object Run()
    {
        _grid = GetInputAsCharGrid();

        var start = _grid.First(g => g.Value == 'S');
        start.Value = '|';
        
        for (var i = 1; i < _grid.MaxY; i++)
        {
            foreach (var beam in _grid.Where(g => g.Y == i && g.Value == '.'))
            {
                if (_grid[beam.Y - 1, beam.X] == '|')
                {
                    _grid[beam.Y, beam.X] = '|';
                }
            }
            
            foreach (var splitter in _grid.Where(g => g.Y == i && g.Value == '^'))
            {
                if (_grid[splitter.Y - 1, splitter.X] == '|')
                {
                    _grid[splitter.Y, splitter.X - 1] = '|';
                    _grid[splitter.Y, splitter.X + 1] = '|';
                }
            }
        }

        //grid.Print();
        
        return CountPaths(start.Y, start.X);
    }

    private long CountPaths(int y, int x)
    {
        while (true)
        {
            if (y >= _grid.MaxY) return 1;

            var cell = _grid.GetGridData(y, x);
            if (cell.Value == '^')
            {
                if (_paths.TryGetValue(cell, out var path))
                {
                    return path;
                }

                var left = CountPaths(y, x - 1);
                var right = CountPaths(y, x + 1);
                _paths[cell] = left + right;
                return left + right;
            }

            y++;
        }
    }
}
