namespace AOC2023._16;

public class Day16B : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        var lengthY = input.GetLength(0);
        var lengthX = input.GetLength(1);

        var beamStart = new List<Beam>();
        for (var y = 0; y < lengthY; y++)
        {
            beamStart.Add(new Beam(y, 0, Direction.Right));
            beamStart.Add(new Beam(y, lengthX - 1, Direction.Left));
        }
        
        for (var x = 0; x < lengthX; x++)
        {
            beamStart.Add(new Beam(0, x, Direction.Down));
            beamStart.Add(new Beam(lengthY - 1, x, Direction.Up));
        }
        
        var max = 0;
        var maxBeam = new List<Beam>();
        foreach (var start in beamStart)
        {
            var energized = Helper.BreadthFirst(start, (child) => GetChild(child, input)).ToList();
            var distinctBeam = energized.Select(e => new {e.Y, e.X}).Distinct();
            var energizedCount = distinctBeam.Count(e => 
                e.Y >= 0 && e.Y < lengthY &&
                e.X >= 0 && e.X < lengthX);

            if (energizedCount > max)
            {
                max = energizedCount;
                maxBeam = energized;
            }
        }
        
        
        foreach (var beam in maxBeam)
        {
            if (beam.Y >= 0 && beam.Y < lengthY &&
                beam.X >= 0 && beam.X < lengthX)
            {
                input[beam.Y, beam.X] = '#';
            }
        }
        
        Helper.Print(input, new Dictionary<char, ConsoleColor>{{'#', ConsoleColor.DarkRed}});
        
        return max;
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

    private record Beam(int Y, int X, Direction Direction);

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}