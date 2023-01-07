using System.Text;

namespace AOC2022._22;

public class Day22A : Day
{
    protected override string Run()
    {
        var input = GetInputAsString().Split("\n\n");
        var map = input[0].Split("\n");
        var instructions = new List<string>();

        var instruction = "";
        for (var i = 0; i < input[1].Length; i++)
        {
            if (!char.IsDigit(input[1][i]))
            {
                instructions.Add(instruction);
                instruction = "";
                instructions.Add("" + input[1][i]);
            }
            else
            {
                instruction += input[1][i];
            }
        }
        instructions.Remove("\n");
        
        // Boundaries
        var minYValues = new Dictionary<int, (int Min, int Max)>();
        var minXValues = new Dictionary<int, (int Min, int Max)>();
        var maxYWidth = 0;
        for (var y = 0; y < map.Length; y++)
        {
            if (map[y].Length > maxYWidth)
            {
                maxYWidth = map[y].Length;
            }
            
            var minX = int.MaxValue;
            var maxX = 0;
            for (var x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] != ' ')
                {
                    if (x < minX)
                    {
                        minX = x;
                    }

                    if (x > maxX)
                    {
                        maxX = x;
                    }
                }
            }
            minYValues.Add(y, (minX, maxX));
        }

        for(var x = 0; x < maxYWidth; x++)
        {
            var minY = int.MaxValue;
            var maxY = 0;
            for (var y = 0; y < map.Length; y++)
            {
                if (map[y].Length <= x)
                {
                    continue;
                }

                if (map[y][x] != ' ')
                {
                    if (y < minY)
                    {
                        minY = y;
                    }

                    if (y > maxY)
                    {
                        maxY = y;
                    }
                }
            }
            minXValues.Add(x, (minY, maxY));
        }

        var xPos = minYValues[0].Min;
        var yPos = 0;
        var facing = FacingEnum.Right;

        var printAbleMap = new char[map.Length, maxYWidth];
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[y].Length; x++)
            {
                printAbleMap[y, x] = map[y][x];
            }
        }

        // Start walking
        foreach (var ins in instructions)
        {
            if (int.TryParse(ins, out var steps))
            {
                (int Min, int Max) boundary;
                switch (facing)
                {
                    case FacingEnum.Right:
                        boundary = minYValues[yPos];
                        for (var s = 0; s < steps; s++)
                        {
                            var xToCheck = xPos + 1;
                            if (xPos + 1 > boundary.Max)
                            {
                                xToCheck = boundary.Min;
                            }

                            if (map[yPos][xToCheck] == '#')
                            {
                                break;
                            }

                            xPos = xToCheck;
                            printAbleMap[yPos, xPos] = '>';
                            //Print(printAbleMap);
                        }
                        break;
                    case FacingEnum.Left:
                        boundary = minYValues[yPos];
                        for (var s = 0; s < steps; s++)
                        {
                            var xToCheck = xPos - 1;
                            if (xPos - 1 < boundary.Min)
                            {
                                xToCheck = boundary.Max;
                            }

                            if (map[yPos][xToCheck] == '#')
                            {
                                break;
                            }

                            xPos = xToCheck;
                            printAbleMap[yPos, xPos] = '<';
                            //Print(printAbleMap);
                        }
                        break;
                    case FacingEnum.Down:
                        boundary = minXValues[xPos];
                        for (var s = 0; s < steps; s++)
                        {
                            var yToCheck = yPos + 1;
                            if (yToCheck > boundary.Max)
                            {
                                yToCheck = boundary.Min;
                            }

                            if (map[yToCheck][xPos] == '#')
                            {
                                break;
                            }

                            yPos = yToCheck;
                            printAbleMap[yPos, xPos] = 'v';
                            //Print(printAbleMap);
                        }
                        break;
                    case FacingEnum.Up:
                        boundary = minXValues[xPos];
                        for (var s = 0; s < steps; s++)
                        {
                            var yToCheck = yPos - 1;
                            if (yToCheck < boundary.Min)
                            {
                                yToCheck = boundary.Max;
                            }

                            if (map[yToCheck][xPos] == '#')
                            {
                                break;
                            }

                            yPos = yToCheck;
                            printAbleMap[yPos, xPos] = '^';
                            //Print(printAbleMap);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if(ins == "L")
            {
                facing = facing switch
                {
                    FacingEnum.Right => FacingEnum.Up,
                    FacingEnum.Left => FacingEnum.Down,
                    FacingEnum.Down => FacingEnum.Right,
                    FacingEnum.Up => FacingEnum.Left,
                    _ => throw new ArgumentOutOfRangeException()
                };
                //Print(printAbleMap);
            }
            else
            {
                facing = facing switch
                {
                    FacingEnum.Right => FacingEnum.Down,
                    FacingEnum.Left => FacingEnum.Up,
                    FacingEnum.Down => FacingEnum.Left,
                    FacingEnum.Up => FacingEnum.Right,
                    _ => throw new ArgumentOutOfRangeException()
                };
                //Print(printAbleMap);
            }
        }
        
        Console.WriteLine($"Y = {yPos}");
        Console.WriteLine($"X = {xPos}");

        var result = 1000 * (yPos + 1) + 4 * (xPos + 1);
        result += facing switch
        {
            FacingEnum.Right => 0,
            FacingEnum.Down => 1,
            FacingEnum.Left => 2,
            FacingEnum.Up => 3,
            _ => throw new ArgumentOutOfRangeException()
        };

        return "" + result;
    }

    private enum FacingEnum
    {
        Right,
        Left,
        Down,
        Up
    }

    private static void Print(char[,] map)
    {
        var sb = new StringBuilder();
        for (var y = 0; y < map.GetLength(0); y++)
        {
            for (var x = 0; x < map.GetLength(1); x++)
            {
                sb.Append(map[y, x]);
            }

            sb.AppendLine();
        }
        
        Console.WriteLine(sb.ToString());
    }
}