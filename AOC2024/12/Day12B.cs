using AOC.Common;

namespace AOC2024._12;

public class Day12B : Day
{
    protected override object Run()
    {
        var grid = GetInputAsCharGrid();
        var visited = new bool[grid.MaxY, grid.MaxX];
        
        var groups = new Dictionary<GridData<char>, (int area, int nrOfSides)>();

        for (var y = 0; y < grid.MaxY; y++)
        {
            for (var x = 0; x < grid.MaxX; x++)
            {
                if (!visited[y, x])
                {
                    var p = grid.GetGridData(y, x);
                    var plots = Helper.BreadthFirst(p, (neighbour) => grid.GetOrthogonalData(neighbour).Where(gd => gd.Value == p.Value)).ToList();
                    
                    foreach (var plot in plots)
                    {
                        visited[plot.Y, plot.X] = true;
                    }
                    
                    groups.Add(p, (plots.Count, CountCorners(plots)));
                }
            }
        }
        
        return groups.Values.Sum(x => x.area * x.nrOfSides);
    }

    private static int CountCorners(List<GridData<char>> gridDatas)
    {
        if (gridDatas.Count <= 2) return 4;
        
        var count = 0;
        foreach (var gridData in gridDatas)
        {
            //  X-
            // |
            if (!gridDatas.Any(gd => gd.X == gridData.X && gd.Y - 1 == gridData.Y) &&
                !gridDatas.Any(gd => gd.X - 1 == gridData.X && gd.Y == gridData.Y))
            {
                count++;
            }
            
            if (!gridDatas.Any(gd => gd.X + 1 == gridData.X && gd.Y + 1 == gridData.Y) &&
                gridDatas.Any(gd => gd.X == gridData.X && gd.Y + 1 == gridData.Y) &&
                gridDatas.Any(gd => gd.X + 1 == gridData.X && gd.Y == gridData.Y))
            {
                count++;
            }
            
            //  -X
            //   |
            if (!gridDatas.Any(gd => gd.X == gridData.X && gd.Y - 1 == gridData.Y) &&
                !gridDatas.Any(gd => gd.X + 1 == gridData.X && gd.Y == gridData.Y))
            {
                count++;
            }

            if (!gridDatas.Any(gd => gd.X - 1 == gridData.X && gd.Y + 1 == gridData.Y) &&
                gridDatas.Any(gd => gd.X == gridData.X && gd.Y + 1 == gridData.Y) &&
                gridDatas.Any(gd => gd.X - 1 == gridData.X && gd.Y == gridData.Y))
            {
                count++;
            }
            
            // |
            // X-
            if (!gridDatas.Any(gd => gd.X == gridData.X && gd.Y + 1 == gridData.Y) &&
                !gridDatas.Any(gd => gd.X - 1 == gridData.X && gd.Y == gridData.Y))
            {
                count++;
            }

            if (!gridDatas.Any(gd => gd.X + 1 == gridData.X && gd.Y - 1 == gridData.Y) &&
                gridDatas.Any(gd => gd.X == gridData.X && gd.Y - 1 == gridData.Y) &&
                gridDatas.Any(gd => gd.X + 1 == gridData.X && gd.Y == gridData.Y))
            {
                count++;
            }
            
            //   |
            // -X
            if (!gridDatas.Any(gd => gd.X == gridData.X && gd.Y + 1 == gridData.Y) &&
                !gridDatas.Any(gd => gd.X + 1 == gridData.X && gd.Y == gridData.Y))
            {
                count++;
            }

            if (!gridDatas.Any(gd => gd.X - 1 == gridData.X && gd.Y - 1 == gridData.Y) &&
                gridDatas.Any(gd => gd.X == gridData.X && gd.Y - 1 == gridData.Y) &&
                gridDatas.Any(gd => gd.X - 1 == gridData.X && gd.Y == gridData.Y))
            {
                count++;
            }
        }
        
        return count;
    }
}