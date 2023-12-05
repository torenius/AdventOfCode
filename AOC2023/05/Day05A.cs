namespace AOC2023._05;

public class Day05A : Day
{
    protected override string Run()
    {
        var input = GetInputAsString();
        var temp = input.Split("\n\n");
        var seeds = temp[0][7..].Split(" ").Select(x => new Seed(x.ToLong())).ToList();

        var maps = new Dictionary<string, List<Map>>();
        for (var i = 1; i < temp.Length; i++)
        {
            var mapTemp = temp[i].Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var mapList = new List<Map>();
            for (var k = 1; k < mapTemp.Length; k++)
            {
                mapList.Add(new Map(mapTemp[k]));
            }
            maps.Add(mapTemp[0], mapList);
        }

        foreach (var map in maps)
        {
            foreach (var seed in seeds)
            {
                var cor = GetCorresponding(seed.LastNumber, map.Value);
                seed.LastNumber = cor;
                seed.Data.Add(map.Key, cor);
            }
        }

        var minLocation = seeds.Select(seed => seed.Data["humidity-to-location map:"]).Min();

        return "" + minLocation;
    }

    private static long GetCorresponding(long number, List<Map> maps)
    {
        foreach (var map in maps)
        {
            if (number >= map.SourceStart && number < map.SourceStart + map.RangeLength)
            {
                var diff = map.DestinationStart - map.SourceStart;
                return number + diff;
            }
        }
        return number;
    }

    private class Seed
    {
        public Seed(long number)
        {
            Number = number;
            LastNumber = number;
        }
        
        public long Number { get; set; }
        public long LastNumber { get; set; }
        public Dictionary<string, long> Data { get; set; } = new();
    }

    private class Map
    {
        public Map(string input)
        {
            var temp = input.Split(" ");
            DestinationStart = temp[0].ToLong();
            SourceStart = temp[1].ToLong();
            RangeLength = temp[2].ToLong();
        }

        public long DestinationStart { get; set; }
        public long SourceStart { get; set; }
        public long RangeLength { get; set; }
    }
}
