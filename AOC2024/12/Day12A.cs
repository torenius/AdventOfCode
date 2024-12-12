namespace AOC2024._12;

public class Day12A : Day
{
    protected override object Run()
    {
        var grid = GetInputAsCharGrid();
        var visited = new bool[grid.MaxY, grid.MaxX];
        
        var groups = new Dictionary<GridData<char>, (int area, int perimeter)>();

        for (var y = 0; y < grid.MaxY; y++)
        {
            for (var x = 0; x < grid.MaxX; x++)
            {
                if (!visited[y, x])
                {
                    var p = grid.GetGridData(y, x);
                    var plots = Helper.BreadthFirst(p, (neighbour) => grid.GetOrthogonalData(neighbour).Where(gd => gd.Value == p.Value)).ToList();

                    var perimeter = 0;
                    foreach (var plot in plots)
                    {
                        perimeter += 4 - grid.GetOrthogonalData(plot).Count(gd => gd.Value == plot.Value);
                        visited[plot.Y, plot.X] = true;
                    }
                    
                    groups.Add(p, (plots.Count, perimeter));
                }
            }
        }
        
        return groups.Values.Sum(x => x.area * x.perimeter);
    }
}