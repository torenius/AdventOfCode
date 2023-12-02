namespace AOC2015._09;

public class Day09B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var data = new List<Route>();
        foreach (var row in input)
        {
            var route = new Route(row);
            data.Add(route);
            var opposite = new Route
            {
                A = route.B,
                B = route.A,
                Cost = route.Cost
            };
            data.Add(opposite);
        }

        var distinctTowns = data.Select(x => x.A).Distinct().ToList();
        var townCount = distinctTowns.Count;
        var maxCost = 0;
        foreach (var town in distinctTowns)
        {
            var (visitedAll, cost) = GetShortest(town, data, new List<string>(), townCount, town, 0);
            Console.WriteLine($"{town} {cost}");

            if (cost > maxCost)
            {
                maxCost = cost;
            }
        }
        
        return "" + maxCost;
    }

    private (bool visitedAll, int Cost) GetShortest(string town, List<Route> data, List<string> visited, int townCount, string path, int currentCost)
    {
        //Console.WriteLine(path + " = " + currentCost);
        var temp = visited.ToList();            
        temp.Add(town);

        var maxCost = 0;
        var possibleRoutes = data.Where(x => x.A == town && !visited.Contains(x.B)).ToList();

        if (possibleRoutes.Count == 0)
        {
            if (temp.Count == townCount)
            {
                return (true, currentCost);
            }

            return (false, 0);
        }
        
        foreach (var possibleRoute in possibleRoutes)
        {
            var (visitedAll, cost) = GetShortest(possibleRoute.B, data, temp, townCount, path + " -> " + possibleRoute.B, currentCost + possibleRoute.Cost);

            if (visitedAll)
            {
                if (maxCost < cost)
                {
                    maxCost = cost;
                }
            }
        }

        return (true, maxCost);
    }

    private class Route
    {
        public Route()
        {
        }
        public Route(string input)
        {
            var temp = input.Split(" to ");
            A = temp[0];
            temp = temp[1].Split(" = ");
            B = temp[0];
            Cost = temp[1].ToInt();
        }
        
        public string A { get; set; }
        public string B { get; set; }
        public int Cost { get; set; }
    }
}