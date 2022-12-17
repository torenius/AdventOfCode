using System.Drawing;

namespace AOC2022._15;

public class Day15A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        const int rowToCheck = 2000000;

        var pointsOnRow = new HashSet<Point>();
        var sensors = new HashSet<Point>();
        var beacons = new HashSet<Point>();

        foreach (var row in input)
        {
            var data = row.Split(":");
            var sensorData = data[0].Replace("Sensor at x=", "");
            var beaconData = data[1].Replace(" closest beacon is at x=", "");

            var sensor = GetPoint(sensorData);
            sensors.Add(sensor);
            
            var beacon = GetPoint(beaconData);
            beacons.Add(beacon);
            
            var sensorRange = sensor.CalculateManhattanDistance(beacon);

            var rowPoint = new Point(sensor.X, rowToCheck);
            var distanceToRow = sensor.CalculateManhattanDistance(rowPoint);
            if (distanceToRow <= sensorRange)
            {
                pointsOnRow.Add(rowPoint);
                var leftAndRight = sensorRange - distanceToRow;
                for (var i = 1; i <= leftAndRight; i++)
                {
                    pointsOnRow.Add(new Point(rowPoint.X - i, rowPoint.Y));
                    pointsOnRow.Add(new Point(rowPoint.X + i, rowPoint.Y));
                }
            }
        }

        var scannedPoints = pointsOnRow.Where(p => !sensors.Contains(p));
        scannedPoints = scannedPoints.Where(p => !beacons.Contains(p));

        return "" + scannedPoints.ToList().Count;
    }
    
    private static Point GetPoint(string input)
    {
        input = input.Replace(" y=", "");
        var point = input.Split(",");

        return new Point(point[0].ToInt(), point[1].ToInt());
    }
}