using AOC.Common;

namespace AOC2025._04;

public class Day04A : Day
{
    protected override object Run()
    {
        var grid = GetInputAsCharGrid();

        var canLift = 0;
        foreach (var gd in grid.Where(x => x.Value == '@'))
        {
            var surroundingRolls = grid.GetSurroundingData(gd).Count(x => x.Value == '@');
            if (surroundingRolls < 4)
            {
                canLift++;
            }
        }
        
        return canLift;
    }
}