using AOC.Common;

namespace AOC2025._04;

public class Day04B : Day
{
    protected override object Run()
    {
        var grid = GetInputAsCharGrid();

        var canLift = 0;
        var removedRolls = 0;
        do
        {
            removedRolls = RemovePapper(grid);
            canLift += removedRolls;

        } while (removedRolls != 0);
        
        return canLift;
    }

    private static int RemovePapper(Grid<char> grid)
    {
        var canLift = 0;
        foreach (var gd in grid.Where(x => x.Value == '@'))
        {
            var surroundingRolls = grid.GetSurroundingData(gd).Count(x => x.Value == '@');
            if (surroundingRolls < 4)
            {
                canLift++;
                gd.Value = '.';
            }
        }
        
        return canLift;
    }
}