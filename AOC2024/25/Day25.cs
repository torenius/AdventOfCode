namespace AOC2024._25;

public class Day25 : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var keyOrLocks = new List<List<string>>();
        var currentList = new List<string>();
        foreach (var row in input)
        {
            if (row == "")
            {
                keyOrLocks.Add(currentList);
                currentList = [];
                continue;
            }
            
            currentList.Add(row);
        }
        keyOrLocks.Add(currentList);

        var keys = new List<int[]>();
        var locks = new List<int[]>();
        foreach (var keyOrLock in keyOrLocks)
        {
            if (keyOrLock[0] == "#####")
            {
                keys.Add(Parse(keyOrLock));
            }
            else
            {
                locks.Add(Parse(keyOrLock));
            }
        }

        var matchCount = 0;
        foreach (var @lock in locks)
        {
            foreach (var key in keys)
            {
                if (@lock[0] + key[0] <= 5 && @lock[1] + key[1] <= 5 && @lock[2] + key[2] <= 5 &&
                    @lock[3] + key[3] <= 5 && @lock[4] + key[4] <= 5)
                {
                    matchCount++;
                }          
            }    
        }
        
        return matchCount;
    }

    private int[] Parse(List<string> input)
    {
        var keyOrLock = new int[5];
        foreach (var row in input)
        {
            if (row[0] == '#') keyOrLock[0]++;
            if (row[1] == '#') keyOrLock[1]++;
            if (row[2] == '#') keyOrLock[2]++;
            if (row[3] == '#') keyOrLock[3]++;
            if (row[4] == '#') keyOrLock[4]++;
        }
        
        keyOrLock[0]--;
        keyOrLock[1]--;
        keyOrLock[2]--;
        keyOrLock[3]--;
        keyOrLock[4]--;
        
        return keyOrLock;
    }
}