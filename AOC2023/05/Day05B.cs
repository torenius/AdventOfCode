namespace AOC2023._05;

public class Day05B : Day
{
    protected override string Run()
    {
        //var input = GetInputAsString("example.txt");
        var input = GetInputAsString();
        var temp = input.Split("\n\n");
        var seedData = temp[0][7..].Split(" ").Select(x => x.ToLong()).ToList();
        var seeds = new List<SeedGroup>();
        for (var i = 0; i < seedData.Count; i += 2)
        {
            seeds.Add(new SeedGroup(seedData[i], seedData[i+1]));
        }

        var maps = new Dictionary<string, List<Map>>();
        for (var i = 1; i < temp.Length; i++)
        {
            var mapTemp = temp[i].Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var mapList = new List<Map>();
            for (var k = 1; k < mapTemp.Length; k++)
            {
                mapList.Add(new Map(mapTemp[k], mapTemp[0]));
            }
            maps.Add(mapTemp[0], mapList);
        }
        
        foreach (var map in maps)
        {
            var tempSeeds = new List<SeedGroup>();
            foreach (var seed in seeds)
            {
                var newGroups = CreateNewSeedGroups(seed, map.Value);
                tempSeeds.AddRange(newGroups);
            }

            seeds = tempSeeds.ToList();
        }

        return "" + seeds.Min(x => x.From);
    }

    private class SeedGroup
    {
        public SeedGroup()
        {
            
        }
        public SeedGroup(long from, long length)
        {
            From = from;
            To = from + length - 1;
            OriginalSeed = from;
        }

        public long OriginalSeed { get; set; }
        public long From { get; set; }
        public long To { get; set; }
        public string MatchedOnMap { get; set; } = "";
    }

    private static List<SeedGroup> CreateNewSeedGroups(SeedGroup seedGroup, List<Map> maps)
    {
        var seedGroups = new List<SeedGroup> {seedGroup};
        var result = new List<SeedGroup>();
        var temp = new List<SeedGroup>();
        for(var i = 0; i < maps.Count; i++)
        {
            foreach (var group in seedGroups)
            {
                if (group.MatchedOnMap == maps[i].MapName)
                {
                    temp.Add(group);
                }
                else
                {
                    var g = CreateNewSeedGroups(group, maps[i]);
                    temp.AddRange(g);
                }
            }

            seedGroups = temp.ToList();
            temp = new List<SeedGroup>();
            
            if (i + 1 == maps.Count)
            {
                result = seedGroups;
            }
        }
        
        return result;
    }

    private static List<SeedGroup> CreateNewSeedGroups(SeedGroup seedGroup, Map map)
    {
        var result = new List<SeedGroup>();
        // Seed group that's lower than map
        if (seedGroup.From < map.SourceStart)
        {
            result.Add(new SeedGroup
            {
                OriginalSeed = seedGroup.OriginalSeed,
                From = seedGroup.From,
                To = Math.Min(seedGroup.To, map.SourceStart - 1)
            });
        }
        
        // Seed group that's inside map
        if (map.SourceEnd >= seedGroup.From && map.SourceStart <= seedGroup.To)
        {
            result.Add(new SeedGroup
            {
                OriginalSeed = seedGroup.OriginalSeed,
                From = Math.Max(seedGroup.From, map.SourceStart) + map.Diff,
                To = Math.Min(seedGroup.To, map.SourceEnd) + map.Diff,
                MatchedOnMap = map.MapName
            });
        }
        
        // Seed group that's higher than map
        if (seedGroup.To > map.SourceEnd)
        {
            result.Add(new SeedGroup
            {
                OriginalSeed = seedGroup.OriginalSeed,
                From = Math.Max(seedGroup.From, map.SourceEnd + 1),
                To = seedGroup.To
            });
        }

        return result;
    }

    private class Map
    {
        public Map(string input, string mapName)
        {
            var temp = input.Split(" ");
            DestinationStart = temp[0].ToLong();
            SourceStart = temp[1].ToLong();
            SourceEnd = SourceStart + temp[2].ToLong();
            Diff = DestinationStart - SourceStart;
            MapName = mapName;
        }

        public string MapName { get; set; }
        public long DestinationStart { get; set; }
        public long SourceStart { get; set; }
        public long SourceEnd { get; set; }
        public long Diff { get; set; }
    }
}
