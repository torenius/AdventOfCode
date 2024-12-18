namespace AOC2024._18;

public class Day18A : Day
{
    protected override object Run()
    {
        // var input = GetInputAsStringArray("example.txt");
        // var matrix = new char[7, 7];
        
        var input = GetInputAsStringArray();
        var matrix = new char[71, 71];

        for (var y = 0; y < matrix.GetLength(0); y++)
        {
            for (var x = 0; x < matrix.GetLength(1); x++)
            {
                matrix[y, x] = '.';
            }
        }

        foreach (var row in input.Take(1024))
        {
            var split = row.Split(',');
            matrix[split[1].ToInt(), split[0].ToInt()] = '#';
        }
        
        var grid = new Grid<char>(matrix);
        //grid.Print();

        var start = grid.GetGridData(0, 0);
        var paths = Helper.ShortestPath(start,
            (neighbor) => grid.GetOrthogonalData(neighbor).Where(n => n.Value != '#'));
        
        var end = grid.GetGridData(grid.MaxY - 1, grid.MaxX - 1);
        var shortestPath = paths(end).ToList();
        // shortestPath.ForEach(sp => sp.Value = 'O');
        // grid.Print();
        
        return shortestPath.Count;
    }
}
