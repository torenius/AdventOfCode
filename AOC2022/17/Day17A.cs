using System.Drawing;

namespace AOC2022._17;

public class Day17A : Day
{
    private char[] Gas;
    private int GasIndex = -1;
    
    protected override string Run()
    {
        var input = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
        input = GetInputAsString().Trim('\n');

        Gas = input.ToCharArray();
        
        var cave = new bool[9000, 7];

        var rocks = new List<Shape>();
        
        // Add 2020
        for (var i = 0; i < 404; i++)
        {
            rocks.Add(new Minus());
            rocks.Add(new Plus());
            rocks.Add(new J());
            rocks.Add(new I());
            rocks.Add(new Square());
        }
        // Add the last 2
        rocks.Add(new Minus());
        rocks.Add(new Plus());

        var minY = cave.GetLength(0);

        foreach (var rock in rocks)
        {
            rock.BottomLeftCoordinate = new Point(2, minY - 4);

            do
            {
                //Print(cave, rock.TopY, rock);
                var gas = GetNextGas();
                if (gas == '>')
                {
                    rock.MoveRight(cave);
                }
                else
                {
                    rock.MoveLeft(cave);
                }
                //Print(cave, rock.TopY, rock);
            }while(!rock.MoveDown(cave));
            
            
            rock.Stopped(cave);
            minY = Math.Min(minY, rock.TopY);
            //Print(cave, rock.TopY);
        }
        
        //Print(cave, minY);

        return "" + (cave.GetLength(0) - minY);
    }

    private static void Print(bool[,] cave, int from, Shape? rock = null)
    {
        var footprint = rock != null ? rock.GetFootprint() : new List<(int y, int x)>();
        for (var y = from; y < cave.GetLength(0); y++)
        {
            var row = ("" + (cave.GetLength(0) - y)).PadLeft(4, '0') + " ";
            for (var x = 0; x < cave.GetLength(1); x++)
            {
                if (footprint.Contains((y, x)))
                {
                    row += "¤";
                }
                else
                {
                    row += cave[y, x] ? "#" : ".";    
                }
            }
            Console.WriteLine(row);
        }
        Console.WriteLine();
    }

    private char GetNextGas()
    {
        if (GasIndex + 1 == Gas.Length)
        {
            GasIndex = -1;
        }

        GasIndex++;

        return Gas[GasIndex];
    }

    private abstract class Shape
    {
        public Point BottomLeftCoordinate { get; set; }
        public abstract int TopY { get; }

        public List<(int Y, int X)> GetFootprint()
        {
            return GetFootprint(BottomLeftCoordinate.Y, BottomLeftCoordinate.X);
        }
        
        public abstract List<(int Y, int X)> GetFootprint(int y, int x);

        public void MoveRight(bool[,] cave)
        {
            var y = BottomLeftCoordinate.Y;
            var x = BottomLeftCoordinate.X + 1;

            if (DoRockCollide(cave, y, x)) return;

            BottomLeftCoordinate = new Point(x, y);
        }
        
        public void MoveLeft(bool[,] cave)
        {
            var y = BottomLeftCoordinate.Y;
            var x = BottomLeftCoordinate.X - 1;
            
            if (DoRockCollide(cave, y, x)) return;

            BottomLeftCoordinate = new Point(x, y);
        }
        
        public bool MoveDown(bool[,] cave)
        {
            var y = BottomLeftCoordinate.Y + 1;
            var x = BottomLeftCoordinate.X;

            if (y >= cave.GetLength(0)) return true;

            if (DoRockCollide(cave, y, x)) return true;

            BottomLeftCoordinate = new Point(x, y);

            return false;
        }

        private bool DoRockCollide(bool[,] cave, int y, int x)
        {
            var caveWidth = cave.GetLength(1);
            foreach (var footprint in GetFootprint(y, x))
            {
                if (footprint.X >= caveWidth || footprint.X < 0)
                {
                    return true;
                }
            
                if (cave[footprint.Y, footprint.X])
                {
                    return true;
                }
            }

            return false;
        }

        public void Stopped(bool[,] cave)
        {
            foreach (var footprint in GetFootprint())
            {
                cave[footprint.Y, footprint.X] = true;
            }
        }
    }

    private class Minus : Shape
    {
        public override int TopY => BottomLeftCoordinate.Y;
        public override List<(int Y, int X)> GetFootprint(int y, int x)
        {
            return new List<(int Y, int X)>
            {
                /*
                 * ####
                 */
                {(y, x)},
                {(y, x + 1)},
                {(y, x + 2)},
                {(y, x + 3)}
            };
        }
    }

    private class Plus : Shape
    {
        public override int TopY => BottomLeftCoordinate.Y - 2;
        
        public override List<(int Y, int X)> GetFootprint(int y, int x)
        {
            return new List<(int Y, int X)>
            {
                /*
                 * .#.
                 * ###
                 * .#.
                 */
                {(y - 2, x + 1)},
                {(y - 1, x)},
                {(y - 1, x + 1)},
                {(y - 1, x + 2)},
                {(y, x + 1)}
            };
        }
    }

    private class J : Shape
    {
        public override int TopY => BottomLeftCoordinate.Y - 2;
        public override List<(int Y, int X)> GetFootprint(int y, int x)
        {
            return new List<(int Y, int X)>
            {
                /*
                 * ..#
                 * ..#
                 * ###
                 */
                {(y, x)},
                {(y, x + 1)},
                {(y, x + 2)},
                {(y - 1, x + 2)},
                {(y - 2, x + 2)}
            };
        }
    }
    
    private class I : Shape
    {
        public override int TopY => BottomLeftCoordinate.Y - 3;
        
        public override List<(int Y, int X)> GetFootprint(int y, int x)
        {
            return new List<(int Y, int X)>
            {
                /*
                 * #
                 * #
                 * #
                 * #
                 */
                {(y, x)},
                {(y - 1, x)},
                {(y - 2, x)},
                {(y - 3, x)}
            };
        }
    }

    private class Square : Shape
    {
        public override int TopY => BottomLeftCoordinate.Y - 1;
        
        public override List<(int Y, int X)> GetFootprint(int y, int x)
        {
            return new List<(int Y, int X)>
            {
                /*
                 * ##
                 * ##
                 */
                {(y, x)},
                {(y, x + 1)},
                {(y - 1, x)},
                {(y - 1, x + 1)}
            };
        }
    }
}