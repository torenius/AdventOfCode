using AOC.Common;

namespace AOC2025._07;

public class Day07A : Day
{
    protected override object Run()
    {
        var grid = GetInputAsCharGrid();

        var start = grid.First(g => g.Value == 'S');
        start.Value = '|';
        
        for (var i = 1; i < grid.MaxY; i++)
        {
            foreach (var beam in grid.Where(g => g.Y == i && g.Value == '.'))
            {
                if (grid[beam.Y - 1, beam.X] == '|')
                {
                    grid[beam.Y, beam.X] = '|';
                }
            }
            
            foreach (var splitter in grid.Where(g => g.Y == i && g.Value == '^'))
            {
                if (grid[splitter.Y - 1, splitter.X] == '|')
                {
                    grid[splitter.Y, splitter.X - 1] = '|';
                    grid[splitter.Y, splitter.X + 1] = '|';
                }
            }
        }

        //grid.Print();

        var splits = grid.Where(g => g.Value == '^').Count(g => grid[g.Y - 1, g.X] == '|');

        return splits;
    }
}