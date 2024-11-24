namespace AOC2016._04;

public class Day04A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var rooms = input.Select(x => new Room(x)).ToList();
        return rooms.Where(x => x.IsValid).Sum(x => x.Id);
    }

    public class Room
    {
        public Room(ReadOnlySpan<char> input)
        {
            Occurrence = new Dictionary<char, int>();

            var lastDash = input.LastIndexOf('-');
            var name = input[..lastDash];
            Name = name.ToString();

            foreach (var c in name)
            {
                if (c == '-') continue;
                
                if (!Occurrence.TryAdd(c, 1))
                {
                    Occurrence[c]++;
                }
            }

            var bracket = input.IndexOf('[');
            Id = input[(lastDash + 1)..bracket].ToInt();
            
            Type = input[(bracket + 1)..^1].ToArray();

            IsValid = true;
            var index = 0;
            foreach (var kvp in Occurrence.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                if (index == Type.Length) break;
                
                if (Type[index] != kvp.Key)
                {
                    IsValid = false;
                    break;
                }
                index++;
            }
        }

        public string Name { get; private set; }
        public int Id { get; set; }
        public Dictionary<char, int> Occurrence { get; set; }
        public char[] Type { get; set; }
        public bool IsValid { get; set; }
        
    }
}