namespace AOC2023._12;

public class Day12A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var maps = input.Select(x => new SpringMap(x)).ToList();
        var sum = 0;
        for (var i = 0; i < maps.Count; i++)
        {
            var map = maps[i];
            LookForSprings(map, map.Springs, 0);
            sum += map.Matches.Count;
            PrintElapsedTime($"{i} {map.Matches.Count}");
        }

        foreach (var map in maps)
        {
            foreach (var match in map.Matches)
            {
                Console.WriteLine(match);
            }
        }

        return sum;
    }

    private bool LookForSprings(SpringMap map, char[] springs, int index)
    {
        if (!map.IsPossible(springs))
        {
            return false;
        }
        
        for (var i = index; i < springs.Length; i++)
        {
            if (springs[i] == '?')
            {
                springs[i] = '.';
                var couldBeWorking = LookForSprings(map, springs, i + 1);

                springs[i] = '#';
                var couldBeBroken = LookForSprings(map, springs, i + 1);
                
                springs[i] = '?';
                if (!couldBeWorking && !couldBeBroken)
                {
                    return false;
                }
            }
        }
        
        if (map.IsMatch(springs))
        {
            map.Matches.Add(new string(springs));
            return true;
        }
        
        return false;
    }
    
    private class SpringMap
    {
        public SpringMap(string input)
        {
            var temp = input.Split(" ");
            Springs = temp[0].ToCharArray();
            Damaged = temp[1].Split(",").Select(x => x.ToInt()).ToArray();
        }

        public char[] Springs { get; set; }
        public int[] Damaged { get; set; }
        public List<string> Matches { get; set; } = new();
        
        public bool IsPossible(IReadOnlyList<char> springs)
        {
            var index = -1;
            var isGroup = false;
            var groupCount = 0;
            for (var i = 0; i < springs.Count; i++)
            {
                if (springs[i] == '?')
                {
                    if (isGroup)
                    {
                        groupCount++;
                    }
                    else
                    {
                        return true; // Only ....??? so var. So Still possible
                    }
                    
                    if (groupCount >= Damaged[index])
                    {
                        return true;
                    }
                }
                else if (springs[i] == '#')
                {
                    groupCount++;
                    if (!isGroup)
                    {
                        isGroup = true;
                        index++;
                        if (index >= Damaged.Length)
                        {
                            return false;
                        }
                    }
                }
                else if (isGroup)
                {
                    if (Damaged[index] != groupCount)
                    {
                        return false;
                    }
                    
                    groupCount = 0;
                    isGroup = false;
                }
            }

            return true;
        }
        
        public bool IsMatch(IReadOnlyList<char> springs)
        {
            var index = -1;
            var isGroup = false;
            var groupCount = 0;
            for (var i = 0; i < springs.Count; i++)
            {
                if (springs[i] == '?')
                {
                    return false;
                }
                
                if (springs[i] == '.' && isGroup)
                {
                    if (index >= Damaged.Length || Damaged[index] != groupCount)
                    {
                        return false;
                    }
                    
                    groupCount = 0;
                    isGroup = false;
                }
                else if(springs[i] == '#')
                {
                    groupCount++;
                    if (!isGroup)
                    {
                        isGroup = true;
                        index++;
                        if (index >= Damaged.Length)
                        {
                            return false;
                        }
                    }
                }
            }
            
            if (index != Damaged.Length - 1 || (isGroup && Damaged[index] != groupCount))
            {
                return false;
            }

            return true;
        }
    }
}