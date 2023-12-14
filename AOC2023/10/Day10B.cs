namespace AOC2023._10;

public class Day10B : Day
{
    protected override object Run()
    {
        var colors = new Dictionary<char, ConsoleColor>
        {
            {'.', ConsoleColor.DarkGreen},
            {'S', ConsoleColor.DarkRed},
            {'|', ConsoleColor.DarkRed},
            {'7', ConsoleColor.DarkRed},
            {'F', ConsoleColor.DarkRed},
            {'-', ConsoleColor.DarkRed},
            {'J', ConsoleColor.DarkRed},
            {'L', ConsoleColor.DarkRed},
            {'X', ConsoleColor.DarkBlue}
        };
        var input = GetInputAsCharMatrix();
        var lengthY = input.GetLength(0);
        var lengthX = input.GetLength(1);
        
        var nodes = new List<Node>();
        for (var y = 0; y < lengthY; y++)
        {
            for (var x = 0; x < lengthX; x++)
            {
                var node = new Node
                {
                    Value = input[y, x],
                    X = x,
                    Y = y
                };
                nodes.Add(node);
            }
        }

        var start = nodes.First(x => x.Value == 'S');

        var bdf = Helper.BreadthFirst(start, (child) => GetNeighbors(input, child)).ToList();
        var path = new List<Node>();
        for (var i = 0; i < bdf.Count; i += 2)
        {
            path.Add(bdf[i]);
        }
        
        for (var i = bdf.Count - 1; i > 0; i -= 2)
        {
            path.Add(bdf[i]);
        }
        
        path.Add(start);
        
        // Not part of maze will get . to simplify logic below
        for (var y = 0; y < lengthY; y++)
        {
            for (var x = 0; x < lengthX; x++)
            {
                if (!path.Any(p => p.Y == y && p.X == x))
                {
                    input[y, x] = '.';
                }
            }
        }
        
        // Follow path and add values to left and right of path
        var firstStep = path[1];
        var direction = Direction.North;
        if (start.X + 1 == firstStep.X)
        {
            direction = Direction.East;
        }
        else if (start.X - 1 == firstStep.X)
        {
            direction = Direction.West;
        }
        else if (start.Y + 1 == firstStep.Y)
        {
            direction = Direction.South;
        }

        var previousDirection = direction;
        
        foreach (var p in path)
        {
            var y = p.Y;
            var x = p.X;

            var pos = input[y, x];

            direction = previousDirection switch
            {
                Direction.North when pos == '7' => Direction.West,
                Direction.North when pos == 'F' => Direction.East,
                Direction.East when pos == '7' => Direction.South,
                Direction.East when pos == 'J' => Direction.North,
                Direction.South when pos == 'L' => Direction.East,
                Direction.South when pos == 'J' => Direction.West,
                Direction.West when pos == 'L' => Direction.North,
                Direction.West when pos == 'F' => Direction.South,
                _ => previousDirection
            };

            if (pos == '7' && previousDirection == Direction.East && y - 1 >= 0 && input[y - 1, x] == '.')
            {
                input[y - 1, x] = 'X';
            }
            else if (pos == 'J' && previousDirection == Direction.South && x + 1 < lengthX && input[y, x + 1] == '.')
            {
                input[y, x + 1] = 'X';
            }
            else if (pos == 'L' && previousDirection == Direction.West && y + 1 < lengthY && input[y + 1, x] == '.')
            {
                input[y + 1, x] = 'X';
            }
            else if (pos == 'F' && previousDirection == Direction.North && x - 1 >= 0 && input[y, x - 1] == '.')
            {
                input[y, x - 1] = 'X';
            }
            
            if (direction == Direction.North && x - 1 >= 0 && input[y, x - 1] == '.')
            {
                input[y, x - 1] = 'X';
            }

            if (direction == Direction.East && y - 1 >= 0 && input[y - 1, x] == '.')
            {
                input[y - 1, x] = 'X';
            }
            
            if (direction == Direction.South && x + 1 < lengthX && input[y, x + 1] == '.')
            {
                input[y, x + 1] = 'X';
            }
            
            if (direction == Direction.West && y + 1 < lengthY && input[y + 1, x] == '.')
            {
                input[y + 1, x] = 'X';
            }

            previousDirection = direction;
        }
        
        // Det är att det saknas på S, 7, L och J som gör att jag får fel värde på min input
        Helper.Print(input, colors);
        
        // Expand X
        for (var y = 0; y < lengthY; y++)
        {
            for (var x = 0; x < lengthX; x++)
            {
                if (input[y, x] == 'X')
                {
                    input[y, x] = '.'; // To get around escape mechanics in ExpandX
                    ExpandX(input, y, x);
                }
            }
        }
        
        Helper.Print(input, colors);

        var dotCount = 0;
        var crossCount = 0;
        for (var y = 0; y < lengthY; y++)
        {
            for (var x = 0; x < lengthX; x++)
            {
                if (input[y, x] == '.')
                {
                    dotCount++;
                }
                else if (input[y, x] == 'X')
                {
                    crossCount++;
                }
            }
        }
        
        Console.WriteLine($"dot: {dotCount} cross: {crossCount}");
        
        // If on outer boarder then it should be outside
        for (var y = 0; y < lengthY; y++)
        {
            if (input[y, 0] == '.' || input[y, lengthX - 1] == '.')
            {
                return "Cross is inside";
            }
        }

        for (var x = 0; x < lengthX; x++)
        {
            if (input[0, x] == '.' || input[lengthY - 1, x] == '.')
            {
                return "Cross is inside";
            }
        }
        
        return "Dot is inside";
    }

    private static void ExpandX(char[,] input, int y, int x)
    {
        if (y < 0 || y >= input.GetLength(0)) return;
        if (x < 0 || x >= input.GetLength(1)) return;
        
        if (input[y, x] == '.')
        {
            input[y, x] = 'X';
            ExpandX(input, y - 1, x);
            ExpandX(input, y + 1, x);
            ExpandX(input, y, x - 1);
            ExpandX(input, y, x + 1);
        }
    }

    private static List<Node> GetNeighbors(char[,] map, Node current)
    {
        var neighbors = new List<Node>();

        var canGoUp = current.Value is 'S' or '|' or 'L' or 'J';
        var canGoDown = current.Value is 'S' or '|' or '7' or 'F';
        var canGoLeft = current.Value is 'S' or '-' or 'J' or '7';
        var canGoRight = current.Value is 'S' or '-' or 'L' or 'F';
        
        
        // Up
        if (canGoUp && current.Y - 1 >= 0 && map[current.Y - 1, current.X] is '|' or '7' or 'F' or 'S')
        {
            neighbors.Add(new Node
            {
                Value = map[current.Y - 1, current.X],
                Y = current.Y - 1,
                X = current.X
            });
        }
        
        // Down
        if (canGoDown && current.Y + 1 < map.GetLength(0) && map[current.Y + 1, current.X] is '|' or 'L' or 'J' or 'S')
        {
            neighbors.Add(new Node
            {
                Value = map[current.Y + 1, current.X],
                Y = current.Y + 1,
                X = current.X
            });
        }
        
        // Left
        if (canGoLeft && current.X - 1 >= 0 && map[current.Y, current.X - 1] is '-' or 'L' or 'F' or 'S')
        {
            neighbors.Add(new Node
            {
                Value = map[current.Y, current.X - 1],
                Y = current.Y,
                X = current.X - 1
            });
        }
        
        // Right
        if (canGoRight && current.X + 1 < map.GetLength(1) && map[current.Y, current.X + 1] is '-' or 'J' or '7' or 'S')
        {
            neighbors.Add(new Node
            {
                Value = map[current.Y, current.X + 1],
                Y = current.Y,
                X = current.X + 1
            });
        }

        return neighbors;
    }
 
    
    private class Node : IEquatable<Node>
    {
        public char Value { get; init; }
        public int X { get; init; }
        public int Y { get; init; }

        public bool Equals(Node? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return $"Y: {Y} X: {X} V: {Value}";
        }
    }

    private enum Direction
    {
        North,
        East,
        South,
        West
    }
}