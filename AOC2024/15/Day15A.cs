namespace AOC2024._15;

public class Day15A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var maze = new List<string>();
        var commands = new List<char>();
        var isMazePart = true;
        foreach (var item in input)
        {
            if (item == "")
            {
                isMazePart = false;
                continue;
            }

            if (isMazePart)
            {
                maze.Add(item);
            }
            else
            {
                commands.AddRange(item.ToCharArray());
            }
        }
        
        var gridMaze = new char[maze.Count + 1, maze.Count + 1];
        for(var y = 0; y < maze.Count; y++)
        {
            for (var x = 0; x < maze.Count; x++)
            {
                gridMaze[y, x] = maze[y][x];
            }
        }
        
        var grid = new Grid<char>(gridMaze);
        var color = new Dictionary<char, ConsoleColor>
        {
            { '@', ConsoleColor.Red },
            { 'O', ConsoleColor.Blue}
        };
        foreach (var command in commands)
        {
            //grid.Print(color);
            MoveRobot(grid, command);
        }
        grid.Print(color);

        return grid.Where(g => g.Value == 'O').Sum(g => g.Y * 100 + g.X);
    }

    private static void MoveRobot(Grid<char> grid, char direction)
    {
        var robot = grid.First(g => g.Value == '@');
        var dirX = 0;
        var dirY = 0;
        switch (direction)
        {
            case '^':
                dirY = -1;
                break;
            case 'v':
                dirY = 1;
                break;
            case '>':
                dirX = 1;
                break;
            case '<':
                dirX = -1;
                break;
        }

        var move = grid.GetGridData(robot.Y + dirY, robot.X + dirX);

        if (move.Value == '#')
        {
            return;
        }
        
        if (move.Value == '.')
        {
            move.Value = '@';
            robot.Value = '.';
            return;
        }

        var freeSpotY = move.Y;
        var freeSpotX = move.X;

        do
        {
            freeSpotY += dirY;
            freeSpotX += dirX;
        }while(grid[freeSpotY, freeSpotX] == 'O');
        
        
        if (grid[freeSpotY, freeSpotX] == '.')
        {
            robot.Value = '.';
            move.Value = '@';
            grid[freeSpotY, freeSpotX] = 'O';
        }
    }
}