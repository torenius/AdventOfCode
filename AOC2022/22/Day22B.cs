using System.Text;
using System.Drawing;

namespace AOC2022._22;

public class Day22B : Day
{
    protected override string Run()
    {
        var fileName = "example.txt";
        fileName = "input.txt";
        //fileName = "test.txt";
        var input = GetInputAsString(fileName).Split("\n\n");
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
        
        var printAbleMap = new char[map.Length, 200];
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[y].Length; x++)
            {
                printAbleMap[y, x] = map[y][x];
            }
        }

        var sides = new List<Side>();
        Player player;
        if (fileName == "example.txt")
        {
            var side1 = new Side(1, map, 0, 8, 4);
            var side2 = new Side(2, map, 4, 0, 4);
            var side3 = new Side(3, map, 4, 4, 4);
            var side4 = new Side(4, map, 4, 8, 4);
            var side5 = new Side(5, map, 8, 8, 4);
            var side6 = new Side(6, map, 8, 12, 4);

            player = new Player(side1);
            
            // Side 1
            side1.UpSide = side2;
            side1.UpNewFacing = FacingEnum.Down;
            side1.MoveUp.Add(Player.SizeMinusY);

            side1.DownSide = side4;
            side1.DownNewFacing = FacingEnum.Down;
            side1.MoveDown.Add(Player.SizeMinusY);

            side1.RightSide = side3;
            side1.RightNewFacing = FacingEnum.Down;
            // 2, 0 -> 0, 2
            side1.MoveRight.Add(Player.Swap);

            side1.LeftSide = side6;
            side1.LeftNewFacing = FacingEnum.Left;
            // 2, 3 -> 1, 3
            side1.MoveLeft.Add(Player.SizeMinusY);
            
            // Side 2
            side2.UpSide = side1;
            side2.UpNewFacing = FacingEnum.Down;
            // 0, 2 -> 0, 2
            //side2.MoveUp.Add(player.SizeMinusY);

            side2.DownSide = side5;
            side2.DownNewFacing = FacingEnum.Up;
            // 3, 3 -> 0, 3
            side2.MoveDown.Add(Player.SizeMinusY);

            side2.RightSide = side3;
            side2.RightNewFacing = FacingEnum.Right;
            // 0, 3 -> 0, 0
            side2.MoveRight.Add(Player.SizeMinusX);

            side2.LeftSide = side6;
            side2.LeftNewFacing = FacingEnum.Up;
            // 0, 0 -> 3, 3
            // 0, 3 -> 3, 0
            side2.MoveLeft.Add(Player.Swap);

            // Side 3
            side3.UpSide = side1;
            side3.UpNewFacing = FacingEnum.Right;
            // 0, 2 -> 2, 0
            side3.MoveUp.Add(Player.Swap);

            side3.DownSide = side5;
            side3.DownNewFacing = FacingEnum.Right;
            // 3, 3 -> 0, 0
            side3.MoveDown.Add(Player.Swap);

            side3.RightSide = side4;
            side3.RightNewFacing = FacingEnum.Right;
            // 0, 3 -> 0, 0
            side3.MoveRight.Add(Player.SizeMinusX);

            side3.LeftSide = side2;
            side3.LeftNewFacing = FacingEnum.Left;
            // 0, 0 -> 0, 3
            // 1, 0 -> 1, 3
            side3.MoveLeft.Add(Player.MaxX);
            
            // Side 4
            side4.UpSide = side1;
            side4.UpNewFacing = FacingEnum.Up;
            // 0, 2 -> 3, 2
            side4.MoveUp.Add(Player.MaxY);

            side4.DownSide = side5;
            side4.DownNewFacing = FacingEnum.Down;
            // 3, 2 -> 0, 2
            side4.MoveDown.Add(Player.MinY);

            side4.RightSide = side6;
            side4.RightNewFacing = FacingEnum.Down;
            // 0, 3 -> 0, 3 
            // 1, 3 -> 0, 2
            // 2, 3 -> 0, 1
            // 3, 3 -> 0, 0
            side4.MoveRight.Add(Player.MinX);
            side4.MoveRight.Add(Player.SizeMinusY);
            side4.MoveRight.Add(Player.Swap);

            side4.LeftSide = side3;
            side4.LeftNewFacing = FacingEnum.Left;
            // 2,0 -> 2,3
            side4.MoveLeft.Add(Player.MaxX);
            
            // Side 5
            side5.UpSide = side4;
            side5.UpNewFacing = FacingEnum.Up;
            // 0, 2 -> 3, 2
            side5.MoveUp.Add(Player.MaxY);

            side5.DownSide = side2;
            side5.DownNewFacing = FacingEnum.Up;
            // 3, 0 -> 3, 3
            // 3, 1 -> 3, 2
            // 3, 2 -> 3, 1
            // 3, 3 -> 3, 0
            side5.MoveDown.Add(Player.SizeMinusX);

            side5.RightSide = side6;
            side5.RightNewFacing = FacingEnum.Right;
            // 0, 3 -> 0, 0
            // 2, 3 -> 2, 0
            side5.MoveRight.Add(Player.MinX);

            side5.LeftSide = side3;
            side5.LeftNewFacing = FacingEnum.Up;
            // 0,0 -> 3,3
            // 1,0 -> 3,2
            // 2,0 -> 3,1
            // 3,0 -> 3,0
            side5.MoveLeft.Add(Player.SizeMinusX);
            side5.MoveLeft.Add(Player.MaxX);
            side5.MoveLeft.Add(Player.Swap);
            
            // Side 6
            side6.UpSide = side4;
            side6.UpNewFacing = FacingEnum.Left;
            // 0, 0 -> 3, 3
            // 0, 1 -> 2, 3
            // 0, 2 -> 1, 3
            // 0, 3 -> 0, 3
            side6.MoveUp.Add(Player.SizeMinusY);
            side6.MoveUp.Add(Player.MaxX);
            side6.MoveUp.Add(Player.Swap);

            side6.DownSide = side2;
            side6.DownNewFacing = FacingEnum.Right;
            // 3, 0 -> 0, 3
            // 3, 1 -> 0, 2
            // 3, 2 -> 0, 1
            // 3, 3 -> 0, 0
            side6.MoveDown.Add(Player.MinY);
            side6.MoveDown.Add(Player.SizeMinusX);
            side6.MoveDown.Add(Player.Swap);

            side6.RightSide = side1;
            side6.RightNewFacing = FacingEnum.Left;
            // 0,3 -> 3,3
            // 1,3 -> 2,3
            // 2,3 -> 1,3
            // 3,3 -> 0,3
            side6.MoveRight.Add(Player.SizeMinusY);

            side6.LeftSide = side5;
            side6.LeftNewFacing = FacingEnum.Left;
            // 0,0 -> 0,3
            side6.MoveLeft.Add(Player.MaxX);
        }
        else
        {
            /*   1 2
             *   3
             * 4 5
             * 6
             */
            var side1 = new Side(1, map, 0, 50, 50);
            var side2 = new Side(2, map, 0, 100, 50);
            var side3 = new Side(3, map, 50, 50, 50);
            var side4 = new Side(4, map, 100, 0, 50);
            var side5 = new Side(5, map, 100, 50, 50);
            var side6 = new Side(6, map, 150, 0, 50);

            player = new Player(side1);
            
            /*   6
             * 4 1 2
             *   3
             */
            side1.UpSide = side6;
            side1.UpNewFacing = FacingEnum.Right;
            // 0,0 -> 0,0
            // 0,1 -> 1,0
            // 0,2 -> 2,0
            // 0,3 -> 3,0
            side1.MoveUp.Add(Player.Swap);

            side1.DownSide = side3;
            side1.DownNewFacing = FacingEnum.Down;
            side1.MoveDown.Add(Player.MinY);

            side1.RightSide = side2;
            side1.RightNewFacing = FacingEnum.Right;
            side1.MoveRight.Add(Player.MinX);

            side1.LeftSide = side4;
            // 0,0 -> 3,0
            // 1,0 -> 2,0
            // 2,0 -> 1,0
            // 3,0 -> 0,0
            side1.LeftNewFacing = FacingEnum.Right;
            side1.MoveLeft.Add(Player.SizeMinusY);
            
            /*   6
             * 1 2 5
             *   3
             */
            side2.UpSide = side6;
            side2.UpNewFacing = FacingEnum.Up;
            // 0,0 -> 3,0
            // 0,1 -> 3,1
            // 0,2 -> 3,2
            // 0,3 -> 3,3
            side2.MoveUp.Add(Player.MaxY);

            side2.DownSide = side3;
            side2.DownNewFacing = FacingEnum.Left;
            // 3,0 -> 0,3
            // 3,1 -> 1,3
            // 3,2 -> 2,3
            // 3,3 -> 3,3
            side2.MoveDown.Add(Player.Swap);

            side2.RightSide = side5;
            side2.RightNewFacing = FacingEnum.Left;
            // 0,3 -> 3,3
            // 1,3 -> 2,3
            // 2,3 -> 1,3
            // 3,3 -> 0,3
            side2.MoveRight.Add(Player.SizeMinusY);

            side2.LeftSide = side1;
            side2.LeftNewFacing = FacingEnum.Left;
            side2.MoveLeft.Add(Player.MaxX);

            /*   1
             * 4 3 2
             *   5
             */
            side3.UpSide = side1;
            side3.UpNewFacing = FacingEnum.Up;
            side3.MoveUp.Add(Player.MaxY);

            side3.DownSide = side5;
            side3.DownNewFacing = FacingEnum.Down;
            side3.MoveDown.Add(Player.MinY);

            side3.RightSide = side2;
            side3.RightNewFacing = FacingEnum.Up;
            // 0,3 -> 3,0
            // 1,3 -> 3,1
            // 2,3 -> 3,2
            // 3,3 -> 3,3
            side3.MoveRight.Add(Player.Swap);

            side3.LeftSide = side4;
            side3.LeftNewFacing = FacingEnum.Down;
            // 0,0 -> 0,0
            // 1,0 -> 0,1
            // 2,0 -> 0,2
            // 3,0 -> 0,3
            side3.MoveLeft.Add(Player.Swap);
            
            /*   3
             * 1 4 5
             *   6
             */
            side4.UpSide = side3;
            side4.UpNewFacing = FacingEnum.Right;
            // 0,0 -> 0,0
            // 0,1 -> 1,0
            // 0,2 -> 2,0
            // 0,3 -> 3,0
            side4.MoveUp.Add(Player.Swap);

            side4.DownSide = side6;
            side4.DownNewFacing = FacingEnum.Down;
            side4.MoveDown.Add(Player.MinY);

            side4.RightSide = side5;
            side4.RightNewFacing = FacingEnum.Right;
            side4.MoveRight.Add(Player.MinX);

            side4.LeftSide = side1;
            side4.LeftNewFacing = FacingEnum.Right;
            // 0,0 -> 3,0
            // 1,0 -> 2,0
            // 2,0 -> 1,0
            // 3,0 -> 0,0
            side4.MoveLeft.Add(Player.SizeMinusY);

            /*   3
             * 4 5 2
             *   6
             */
            side5.UpSide = side3;
            side5.UpNewFacing = FacingEnum.Up;
            side5.MoveUp.Add(Player.MaxY);

            side5.DownSide = side6;
            side5.DownNewFacing = FacingEnum.Left;
            side5.MoveDown.Add(Player.Swap);
            side5.MoveDown.Add(Player.MaxX);

            side5.RightSide = side2;
            side5.RightNewFacing = FacingEnum.Left;
            // 0,3 -> 3,3
            // 1,3 -> 2,3
            // 2,3 -> 1,3
            // 3,3 -> 0,3
            side5.MoveRight.Add(Player.SizeMinusY);

            side5.LeftSide = side4;
            side5.LeftNewFacing = FacingEnum.Left;
            side5.MoveLeft.Add(Player.MaxX);
            
            /*   4
             * 1 6 5
             *   2
             */
            side6.UpSide = side4;
            side6.UpNewFacing = FacingEnum.Up;
            side6.MoveUp.Add(Player.MaxY);

            side6.DownSide = side2;
            side6.DownNewFacing = FacingEnum.Down;
            // 3, 0 -> 0, 0
            // 3, 1 -> 0, 1
            // 3, 2 -> 0, 2
            // 3, 3 -> 0, 3
            side6.MoveDown.Add(Player.MinY);

            side6.RightSide = side5;
            side6.RightNewFacing = FacingEnum.Up;
            // 0,3 -> 3,0
            // 1,3 -> 3,1
            // 2,3 -> 3,2
            // 3,3 -> 3,3
            side6.MoveRight.Add(Player.Swap);

            side6.LeftSide = side1;
            side6.LeftNewFacing = FacingEnum.Down;
            // 0,0 -> 0,0
            // 1,0 -> 0,1
            // 2,0 -> 0,2
            // 3,0 -> 0,3
            side6.MoveLeft.Add(Player.Swap);
        }

        // Start walking
        foreach (var ins in instructions)
        {
            //Print(printAbleMap, player);
            if (int.TryParse(ins, out var steps))
            {
                for (var step = 0; step < steps; step++)
                {
                    //Print(printAbleMap, player);
                    switch (player.Facing)
                    {
                        case FacingEnum.Right:
                            if (!player.TryMove(player.Y, player.X + 1))
                            {
                                step = steps;
                            }
                            break;
                        case FacingEnum.Left:
                            if (!player.TryMove(player.Y, player.X - 1))
                            {
                                step = steps;
                            }
                            break;
                        case FacingEnum.Down:
                            if (!player.TryMove(player.Y + 1, player.X))
                            {
                                step = steps;
                            }
                            break;
                        case FacingEnum.Up:
                            if (!player.TryMove(player.Y - 1, player.X))
                            {
                                step = steps;
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            else if(ins == "L")
            {
                player.Facing = player.Facing switch
                {
                    FacingEnum.Right => FacingEnum.Up,
                    FacingEnum.Left => FacingEnum.Down,
                    FacingEnum.Down => FacingEnum.Right,
                    FacingEnum.Up => FacingEnum.Left,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            else
            {
                player.Facing = player.Facing switch
                {
                    FacingEnum.Right => FacingEnum.Down,
                    FacingEnum.Left => FacingEnum.Up,
                    FacingEnum.Down => FacingEnum.Left,
                    FacingEnum.Up => FacingEnum.Right,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
        
        // PrintElapsedTime();
        Print(printAbleMap, player);
        Console.WriteLine($"Y = {player.YPos}");
        Console.WriteLine($"X = {player.XPos}");

        var result = 1000 * (player.YPos + 1) + 4 * (player.XPos + 1);
        result += player.Facing switch
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

    private static void Print(char[,] map, Player player)
    {
        //var sb = new StringBuilder();
        for (var y = 0; y < map.GetLength(0); y++)
        {
            for (var x = 0; x < map.GetLength(1); x++)
            {
                var visit = player.Visit.FirstOrDefault(v => v.Item1.Y == y && v.Item1.X == x);
                if (!visit.Item1.IsEmpty)
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(visit.Item2 switch
                    {
                        FacingEnum.Right => '>',
                        FacingEnum.Down => 'v',
                        FacingEnum.Left => '<',
                        FacingEnum.Up => '^',
                        _ => throw new ArgumentOutOfRangeException()
                    });
                    Console.ForegroundColor = color;
                }
                else
                {
                    //sb.Append(map[y, x]);
                    Console.Write(map[y, x]);
                }
            }

            //sb.AppendLine();
            Console.WriteLine();
        }
        
        //Console.WriteLine(sb.ToString());
    }

    private class Player
    {
        private readonly int _boundary;
        public Player(Side side)
        {
            Facing = FacingEnum.Right;
            Y = 0;
            X = 0;
            Side = side;
            _boundary = side.SideMap.GetLength(0) - 1;
            Visit.Add((side.OriginalCoordinates[Y, X], Facing));
        }
        public int Y { get; set; }
        public int X { get; set; }
        public Side Side { get; set; }
        public FacingEnum Facing { get; set; }
        public List<(Point, FacingEnum)> Visit { get; set; } = new();

        public int YPos => Side.OriginalCoordinates[Y, X].Y;
        public int XPos => Side.OriginalCoordinates[Y, X].X;

        public bool TryMove(int y, int x)
        {
            var side = Side;
            var facing = Facing;
            // Move down
            if (y > _boundary)
            {
                Console.WriteLine($"From: {y},{x} on side {side.Number} facing {facing.ToString()}");
                var coord = NewCoordinates(side.MoveDown);
                y = coord.y;
                x = coord.x;
                facing = side.DownNewFacing;
                side = side.DownSide;
                Console.WriteLine($"To: {y},{x} on side {side.Number} facing {facing.ToString()}");
            }
            // Move up
            else if (y < 0)
            {
                Console.WriteLine($"From: {y},{x} on side {side.Number} facing {facing.ToString()}");
                var coord = NewCoordinates(side.MoveUp);
                y = coord.y;
                x = coord.x;
                facing = side.UpNewFacing;
                side = side.UpSide;
                Console.WriteLine($"To: {y},{x} on side {side.Number} facing {facing.ToString()}");
            }
            // Move right
            else if (x > _boundary)
            {
                Console.WriteLine($"From: {y},{x} on side {side.Number} facing {facing.ToString()}");
                var coord = NewCoordinates(side.MoveRight);
                y = coord.y;
                x = coord.x;
                facing = side.RightNewFacing;
                side = side.RightSide;
                Console.WriteLine($"To: {y},{x} on side {side.Number} facing {facing.ToString()}");
            }
            // Move left
            else if (x < 0)
            {
                Console.WriteLine($"From: {y},{x} on side {side.Number} facing {facing.ToString()}");
                var coord = NewCoordinates(side.MoveLeft);
                y = coord.y;
                x = coord.x;
                facing = side.LeftNewFacing;
                side = side.LeftSide;
                Console.WriteLine($"To: {y},{x} on side {side.Number} facing {facing.ToString()}");
            }

            if (side.SideMap[y, x] == '#')
            {
                return false;
            }
            
            Visit.Add((side.OriginalCoordinates[y, x], facing));
            X = x;
            Y = y;
            Side = side;
            Facing = facing;
            return true;
        }

        private (int x, int y) NewCoordinates(List<Func<int, (int X, int Y), (int X, int Y)>> changeCoordinates)
        {
            var x = X;
            var y = Y;
            foreach (var change in changeCoordinates)
            {
                var newCoords = change.Invoke(_boundary, (x, y));
                x = newCoords.X;
                y = newCoords.Y;
            }

            return (x, y);
        } 

        public static (int X, int Y) Swap(int size, (int X, int Y) old)
        {
            return (old.Y, old.X);
        }
        
        public static (int X, int Y) SizeMinusX(int size, (int X, int Y) old)
        {
            return (size - old.X, old.Y);
        }

        public static (int X, int Y) SizeMinusY(int size, (int X, int Y) old)
        {
            return (old.X, size - old.Y);
        }
        
        public static (int X, int Y) MaxX(int size, (int X, int Y) old)
        {
            return (size, old.Y);
        }
        
        public static (int X, int Y) MaxY(int size, (int X, int Y) old)
        {
            return (old.X, size);
        }
        
        public static (int X, int Y) MinX(int size, (int X, int Y) old)
        {
            return (0, old.Y);
        }
        
        public static (int X, int Y) MinY(int size, (int X, int Y) old)
        {
            return (old.X, 0);
        }
    }

    private class Side
    {
        public Side(int number, string[] map, int topLeftY, int topLeftX, int size)
        {
            Number = number;
            SideMap = new char[size, size];
            OriginalCoordinates = new Point[size, size];
            
            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    SideMap[y, x] = map[y + topLeftY][x + topLeftX];
                    OriginalCoordinates[y, x] = new Point(x + topLeftX, y + topLeftY);
                }
            }
        }
        
        public int Number { get; set; }
        public char[,] SideMap { get; set; }
        public Point[,] OriginalCoordinates { get; set; }

        public Side UpSide { get; set; }
        public Side DownSide { get; set; }
        public Side LeftSide { get; set; }
        public Side RightSide { get; set; }
        
        public FacingEnum UpNewFacing { get; set; }
        public FacingEnum DownNewFacing { get; set; }
        public FacingEnum LeftNewFacing { get; set; }
        public FacingEnum RightNewFacing { get; set; }

        public List<Func<int, (int X, int Y), (int X, int Y)>> MoveUp { get; set; } = new();
        public List<Func<int, (int X, int Y), (int X, int Y)>> MoveDown { get; set; } = new();
        public List<Func<int, (int X, int Y), (int X, int Y)>> MoveLeft { get; set; } = new();
        public List<Func<int, (int X, int Y), (int X, int Y)>> MoveRight { get; set; } = new();

        // public void RotateMapRight(int numberOfTimes)
        // {
        //     for (var i = 0; i < numberOfTimes; i++)
        //     {
        //         RotateMapRight();
        //     }
        // }
        // public void RotateMapRight()
        // {
        //     var size = SideMap.GetLength(0);
        //     var newSideMap = new char[size, size];
        //     var newOriginalCoordinates = new Point[size, size];
        //     
        //     for (var y = 0; y < size; y++)
        //     {
        //         for (var x = 0; x < size; x++)
        //         {
        //             newSideMap[size - 1 - x, y] = SideMap[y, x];
        //             newOriginalCoordinates[size - 1 - x, y] = OriginalCoordinates[y, x];
        //         }
        //     }
        //
        //     SideMap = newSideMap;
        //     OriginalCoordinates = newOriginalCoordinates;
        // }

        public void Print()
        {
            var size = SideMap.GetLength(0);
            var sb = new StringBuilder();
            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    sb.Append(SideMap[y, x]);
                }
                sb.AppendLine();
            }
            
            Console.WriteLine(sb.ToString());
        }
    }
}