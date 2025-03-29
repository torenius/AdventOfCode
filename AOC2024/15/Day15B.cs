using AOC.Common;

namespace AOC2024._15;

public class Day15B : Day
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
                var wideInput = "";
                foreach (var c in item)
                {
                    wideInput += c switch
                    {
                        '#' => "##",
                        'O' => "[]",
                        '.' => "..",
                        _ => "@."
                    };
                }
                maze.Add(wideInput);
            }
            else
            {
                commands.AddRange(item.ToCharArray());
            }
        }
        
        var gridMaze = new char[maze.Count, maze[0].Length];
        for(var y = 0; y < gridMaze.GetLength(0); y++)
        {
            for (var x = 0; x < gridMaze.GetLength(1); x++)
            {
                gridMaze[y, x] = maze[y][x];
            }
        }
        
        var grid = new Grid<char>(gridMaze);
        var color = new Dictionary<char, ConsoleColor>
        {
            { '@', ConsoleColor.Red },
            { '[', ConsoleColor.Blue},
            { ']', ConsoleColor.Blue}
        };
        foreach (var command in commands)
        {
            //grid.Print(color);
            MoveRobot(grid, command);
            //Console.WriteLine(command);
        }
        grid.Print(color);
        
        return grid.Where(g => g.Value == '[').Sum(g => g.Y * 100 + g.X);
    }

    private static void MoveRobot(Grid<char> grid, char direction)
    {
        var robot = grid.Single(g => g.Value == '@');
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

        if (dirX != 0)
        {
            var freeSpotX = move.X;

            do
            {
                freeSpotX += dirX;
            }while(grid[move.Y, freeSpotX] is '[' or ']');

            if (grid[move.Y, freeSpotX] is '#') return;

            if (dirX == -1)
            {
                for (var x = freeSpotX; x < robot.X; x++)
                {
                    grid[move.Y, x] = grid[move.Y, x + 1];
                }    
            }
            else
            {
                for (var x = freeSpotX; x > robot.X; x--)
                {
                    grid[move.Y, x] = grid[move.Y, x - 1];
                }  
            }
            
            robot.Value = '.';

            return;
        }

        (List<GridData<char>> Moved, bool CanMove) test;
        var boxToMove = grid.GetGridData(robot.Y + dirY, robot.X);
        if (boxToMove.Value == '[')
        {
            var rightBox = grid.GetGridData(robot.Y + dirY, robot.X + 1);
            test = CanMove(grid, boxToMove, rightBox, dirY);
        }
        else
        {
            var leftBox = grid.GetGridData(robot.Y + dirY, robot.X - 1);
            test = CanMove(grid, leftBox, boxToMove, dirY);
        }

        if (!test.CanMove) return;

        if (dirY == -1)
        {
            foreach (var g in test.Moved.Distinct().OrderBy(m => m.Y))
            {
                grid[g.Y - 1, g.X] = g.Value;
                g.Value = '.';
            }
        }
        else
        {
            foreach (var g in test.Moved.Distinct().OrderByDescending(m => m.Y))
            {
                grid[g.Y + 1, g.X] = g.Value;
                g.Value = '.';
            }
        }
        
        robot.Value = '.';
        grid[robot.Y + dirY, robot.X] = '@';
    }

    private static (List<GridData<char>> Moved, bool CanMove) CanMove(Grid<char> grid, GridData<char> boxLeft, GridData<char> boxRight, int yDirection)
    {
        if (boxLeft.Value is '.' && boxRight.Value is '.') return ([], true);
        if (boxLeft.Value is ']' or '#' && boxRight.Value is '.') return ([], true);
        if (boxLeft.Value is '.' && boxRight.Value is '[' or '#') return ([], true);
        
        if (!grid.IsValid(boxLeft.Y + yDirection, boxLeft.X) ||
            !grid.IsValid(boxRight.Y + yDirection, boxRight.X))
        {
            return ([], false);
        }
        
        var moved = new List<GridData<char>>
        {
            boxLeft,
            boxRight
        };

        var nextLeft = grid.GetGridData(boxLeft.Y + yDirection, boxLeft.X);
        var nextRight = grid.GetGridData(boxRight.Y + yDirection, boxRight.X);
        
        if (nextLeft.Value == '#' || nextRight.Value == '#') return ([], false);
        if (nextLeft.Value == '.' && nextRight.Value == '.') return (moved, true);

        if (nextLeft.Value == '[' && nextRight.Value == ']')
        {
            var result = CanMove(grid, nextLeft, nextRight, yDirection);
            if (!result.CanMove) return ([], false);
            moved.AddRange(result.Moved);
            return (moved, result.CanMove);
        }
        
        var leftLeftBox = grid.GetGridData(nextLeft.Y, nextLeft.X - 1);
        var rightRightBox = grid.GetGridData(nextRight.Y, nextRight.X + 1);
        
        var resultLeft = CanMove(grid, leftLeftBox, nextLeft, yDirection);
        if (!resultLeft.CanMove) return ([], false);
        moved.AddRange(resultLeft.Moved);

        var resultRight = CanMove(grid, nextRight, rightRightBox, yDirection);
        if (!resultRight.CanMove) return ([], false);
        moved.AddRange(resultRight.Moved);

        return (moved, true);
    }
}