using System.Drawing;

namespace AOC2023._23;

public class Day23B : Day
{
    protected override object Run()
    {
        var input = GetInputAsCharMatrix();
        var start = new Point(1, 0);
        var end = new Point(input.GetLength(1) - 2, input.GetLength(0) - 1);

        var crossRoads = new List<Point> {start, end};
        for (var y = 1; y < input.GetLength(0) - 1; y++)
        {
            for (var x = 1; x < input.GetLength(1) - 1; x++)
            {
                if (input[y, x] != '#')
                {
                    var count = 0;
                    if (input[y - 1, x] != '#') count++;
                    if (input[y + 1, x] != '#') count++;
                    if(input[y, x - 1] != '#') count++;
                    if(input[y, x + 1] != '#') count++;
                    
                    if (count > 2)
                    {
                        crossRoads.Add(new Point(x, y));
                    }
                }
            }
        }

        GetPaths(input, start, start, end, new List<Point>(), crossRoads);
        PrintElapsedTime("KnownRoads: " + KnownRoads.Count);

        var graph = new Helper.DijkstraGraph<Point>();
        foreach (var road in KnownRoads)
        {
            graph.AddDirectedEdge(road.Start, road.End, 1 * (road.Path.Count - 1));
        }
        
        PrintElapsedTime("Create graph");

        var longestPath = FindLongestPath(start, end, graph, new List<Helper.DijkstraEdge<Point>>(), new HashSet<Point>());
        // foreach (var possiblePath in longestPath)
        // {
        //     Console.WriteLine(possiblePath.Sum(x => x.Cost));
        // }

        var longest = longestPath.MaxBy(x => x.Sum(y => y.Cost));
        return longest.Sum(x => x.Cost);
    }
    
    private List<List<Helper.DijkstraEdge<Point>>> FindLongestPath(Point start, Point end, Helper.DijkstraGraph<Point> graph, List<Helper.DijkstraEdge<Point>> path, HashSet<Point> visit)
    {
        var result = new List<List<Helper.DijkstraEdge<Point>>>();
        foreach (var edges in graph.Connections.Where(x => x.Key == start))
        {
            foreach (var edge in edges.Value)
            {
                if (visit.Contains(edge.Destination))
                {
                    continue;
                }

                var newVisit = visit.ToHashSet();
                newVisit.Add(edge.Destination);

                var newPath = path.ToList();
                newPath.Add(edge);

                if (edge.Destination == end)
                {
                    result.Add(newPath);
                }
                else
                {
                    result.AddRange(FindLongestPath(edge.Destination, end, graph, newPath, newVisit));   
                }
            }
        }

        return result;
    }
    
    
    private List<Road> KnownRoads = new();
    private void GetPaths(char[,] map, Point previous, Point start, Point end, List<Point> path, List<Point> crossRoads)
    {
        foreach (var startPoint in crossRoads.Where(x => x != start && x != end))
        {
            var neighbors = GetNeighbors(map, startPoint).ToList();
            foreach (var neighbor in neighbors)
            {
                path = new List<Point>();
                var n = new List<Point>{neighbor};
                previous = startPoint;
                do
                {
                    var current = n[0];
                    path.Add(current);
                    n = GetNeighbors(map, current)
                        .Where(x => x != previous)
                        .ToList();
                    previous = current;
                } while (!n.Any(crossRoads.Contains));
                
                path.Add(startPoint);
                path.Add(n.First(crossRoads.Contains));
                AddRoad(startPoint, n.First(crossRoads.Contains), path);
            }
        }
    }
    
    private void AddRoad(Point start, Point end, IEnumerable<Point> path)
    {
        if (!KnownRoads.Any(x => x.Start == start && x.End == end))
        {
            var p = path.ToList();
            KnownRoads.Add(new Road
            {
                Start = start,
                End = end,
                Path = p,
            });
            
            KnownRoads.Add(new Road
            {
                Start = end,
                End = start,
                Path = p,
            });
        }
    }

    private class Road
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public List<Point> Path { get; set; }

        public override string ToString()
        {
            return $"Start: {Start} End: {End} Cost: {Path.Count}";
        }
    }
    
    private static List<Point> GetNeighbors(char[,] map, Point current)
    {
        var result = new List<Point>();
        
        if (current.Y - 1 >= 0)
        {
            var up = map[current.Y - 1, current.X];
            if (up is not '#')
            {
                result.Add(current with {Y = current.Y - 1});
            }
        }

        var down = map[current.Y + 1, current.X];
        if (down is not '#')
        {
            result.Add(current with {Y = current.Y + 1});
        }

        var left = map[current.Y, current.X - 1];
        if (left is not '#')
        {
            result.Add(current with {X = current.X - 1});
        }
        
        var right = map[current.Y, current.X + 1];
        if (right is not '#')
        {
            result.Add(current with {X = current.X + 1});
        }

        return result;
    }
}