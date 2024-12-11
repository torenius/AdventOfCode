namespace AOC2024._11;

public class Day11A : Day
{
    protected override object Run()
    {
        var input = GetInputAsString().TrimEnd(Environment.NewLine.ToCharArray()).Split(" ");
        var start = new Stone {Value = input[0].ToLong()};
        var previous = start;
        for (var i = 1; i < input.Length; i++)
        {
            var next = new Stone
            {
                Value = input[i].ToLong(),
                Previous = previous
            };
            previous.Next = next;
            previous = next;
        }

        for (var i = 1; i <= 25; i++)
        {
            Console.WriteLine(i);
            start = Blink(start);
        }

        return CountStone(start);
    }

    private static Stone Blink(Stone stone)
    {
        var start = stone;
        while (true)
        {
            if (stone.Value == 0)
            {
                stone.Value = 1;
            }
            else
            {
                var s = stone.Value.ToString();
                if (s.Length % 2 == 0)
                {
                    var l = s[..(s.Length / 2)].ToLong();
                    var r = s[(s.Length / 2)..].ToLong();
                    var leftStone = new Stone
                    {
                        Previous = stone.Previous,
                        Value = l,
                    };
                    var rightStone = new Stone
                    {
                        Previous = leftStone,
                        Next = stone.Next,
                        Value = r
                    };
                    leftStone.Next = rightStone;

                    if (stone.Previous is not null)
                    {
                        stone.Previous.Next = leftStone;
                    }
                    else
                    {
                        start = leftStone;
                    }

                    if (stone.Next is not null)
                    {
                        stone.Next.Previous = rightStone;
                    }
                }
                else
                {
                    stone.Value *= 2024;
                }
            }

            if (stone.Next is null)
            {
                return start;
            }
            
            stone = stone.Next;
        }
    }

    private static long CountStone(Stone stone)
    {
        var count = 0;
        while (true)
        {
            count++;
            stone = stone.Next;
            if (stone is null) break;
        }
        
        return count;
    }

    private class Stone
    {
        public Stone? Previous { get; set; }
        public Stone? Next { get; set; }
        public long Value { get; set; }
    }
}
