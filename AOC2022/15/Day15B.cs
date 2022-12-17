using System.Drawing;

namespace AOC2022._15;

public class Day15B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        var lines = new Dictionary<int, List<Line>>();

        foreach (var row in input)
        {
            Console.WriteLine(row);
            var data = row.Split(":");
            var sensorData = data[0].Replace("Sensor at x=", "");
            var beaconData = data[1].Replace(" closest beacon is at x=", "");

            var sensor = GetPoint(sensorData);
            var beacon = GetPoint(beaconData);

            var sensorRange = sensor.CalculateManhattanDistance(beacon);
            
            var minY = sensor.Y - sensorRange;
            minY = minY < 0 ? 0 : minY;
            
            var maxY = sensor.Y + sensorRange;
            maxY = maxY > 4000000 ? 4000000 : maxY;

            for (var y = minY; y <= maxY; y++)
            {
                var distance = sensor.CalculateManhattanDistance(new Point(sensor.X, y));
                var leftAndRight = sensorRange - distance;
                
                var line = new Line
                {
                    Y = y,
                    MinX = sensor.X - leftAndRight,
                    MaxX = sensor.X + leftAndRight
                };

                if (lines.ContainsKey(y))
                {
                    lines[y].Add(line);
                }
                else
                {
                    lines.Add(y, new List<Line> { line });
                }
            }
        }
        
        PrintElapsedTime();
        foreach (var kvp in lines)
        {
            var row = CombineLines(kvp.Value);
            if (row.Count == 2)
            {
                var left = row.OrderBy(r => r.MaxX).First();
                var x = left.MaxX + 1;
                
                Console.WriteLine($"x={x}, y={kvp.Key}");

                var result = (ulong)x * 4000000 + (ulong)kvp.Key;

                return "" + result;
            }
        }

        return "";
    }

    private static Point GetPoint(string input)
    {
        input = input.Replace(" y=", "");
        var point = input.Split(",");

        return new Point(point[0].ToInt(), point[1].ToInt());
    }

    private static List<Line> CombineLines(List<Line> lines)
    {
        while (true)
        {
            if (lines.Count == 1)
            {
                return lines;
            }
            
            var newList = new List<Line>();
            var line = lines[0];

            if (lines.Count == 2)
            {
                if (line.IsOverlapping(lines[1]))
                {
                    line = line.Combine(lines[1]);
                }
                else
                {
                    newList.Add(lines[1]);
                }

                newList.Add(line);

                return newList;
            }

            for (var i = 1; i < lines.Count; i++)
            {
                if (line.IsOverlapping(lines[i]))
                {
                    line = line.Combine(lines[i]);
                }
                else
                {
                    newList.Add(lines[i]);
                }
            }

            newList.Add(line);

            lines = newList;
        }
    }

    private class Line
    {
        public int Y { get; set; }
        public int MinX { get; set; }
        public int MaxX { get; set; }

        public bool IsOverlapping(Line other)
        {
            if (other.Y != Y)
            {
                return false;
            }

            return MinX <= other.MaxX && MaxX >= other.MinX;
        }

        public Line Combine(Line other)
        {
            var line = new Line();
            line.Y = Y;
            line.MinX = Math.Min(MinX, other.MinX);
            line.MaxX = Math.Max(MaxX, other.MaxX);

            return line;
        }
    }
}