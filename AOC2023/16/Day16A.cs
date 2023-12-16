namespace AOC2023._16;

public class Day16A : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        var start = new Beam(0, 0, Direction.Right);
        
        var energized = Helper.BreadthFirst(start, (child) => GetChild(child, input)).ToList();
        foreach (var beam in energized)
        {
            if (beam.Y >= 0 && beam.Y < input.GetLength(0) &&
                beam.X >= 0 && beam.X < input.GetLength(1))
            {
                input[beam.Y, beam.X] = '#';
            }
        }
        
        Helper.Print(input, new Dictionary<char, ConsoleColor>{{'#', ConsoleColor.DarkRed}});
        
        return energized
            .Select(e => new { e.Y, e.X })
            .Distinct()
            .Count(e => 
                e.Y >= 0 && e.Y < input.GetLength(0) &&
                e.X >= 0 && e.X < input.GetLength(1));
    }

    private static List<Beam> GetChild(Beam beam, char[,] input)
    {
        if (beam.Y < 0 || beam.Y >= input.GetLength(0) ||
            beam.X < 0 || beam.X >= input.GetLength(1))
        {
            return Enumerable.Empty<Beam>().ToList();
        }

        var result = new List<Beam>();
        switch (input[beam.Y, beam.X])
        {
            case '.':
                switch (beam.Direction)
                {
                    case Direction.Up:
                        result.Add(beam with { Y = beam.Y - 1});
                        break;
                    case Direction.Down:
                        result.Add(beam with { Y = beam.Y + 1});
                        break;
                    case Direction.Left:
                        result.Add(beam with { X = beam.X - 1});
                        break;
                    case Direction.Right:
                        result.Add(beam with { X = beam.X + 1});
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            case '/':
                switch (beam.Direction)
                {
                    case Direction.Up:
                        result.Add(beam with { X = beam.X + 1, Direction = Direction.Right});
                        break;
                    case Direction.Down:
                        result.Add(beam with { X = beam.X - 1, Direction = Direction.Left});
                        break;
                    case Direction.Left:
                        result.Add(beam with { Y = beam.Y + 1, Direction = Direction.Down});
                        break;
                    case Direction.Right:
                        result.Add(beam with { Y = beam.Y - 1, Direction = Direction.Up});
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            case '\\':
                switch (beam.Direction)
                {
                    case Direction.Up:
                        result.Add(beam with { X = beam.X - 1, Direction = Direction.Left});
                        break;
                    case Direction.Down:
                        result.Add(beam with { X = beam.X + 1, Direction = Direction.Right});
                        break;
                    case Direction.Left:
                        result.Add(beam with { Y = beam.Y - 1, Direction = Direction.Up});
                        break;
                    case Direction.Right:
                        result.Add(beam with { Y = beam.Y + 1, Direction = Direction.Down});
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            case '-':
                switch (beam.Direction)
                {
                    case Direction.Up:
                    case Direction.Down:
                        result.Add(beam with { X = beam.X - 1, Direction = Direction.Left});
                        result.Add(beam with { X = beam.X + 1, Direction = Direction.Right});
                        break;
                    case Direction.Left:
                        result.Add(beam with { X = beam.X - 1});
                        break;
                    case Direction.Right:
                        result.Add(beam with { X = beam.X + 1});
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
            case '|':
                switch (beam.Direction)
                {
                    case Direction.Up:
                        result.Add(beam with { Y = beam.Y - 1});
                        break;
                    case Direction.Down:
                        result.Add(beam with { Y = beam.Y + 1});
                        break;
                    case Direction.Left:
                    case Direction.Right:
                        result.Add(beam with { Y = beam.Y - 1, Direction = Direction.Up});
                        result.Add(beam with { Y = beam.Y + 1, Direction = Direction.Down});
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                break;
        }
        
        return result;
    }

    private record Beam(int X, int Y, Direction Direction);

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}