
namespace AOC2022._20;

public class Day20B : Day
{
    protected override string Run()
    {
        var input = GetInputAsIntArray();

        var links = new List<LinkedListNode<long>>();
        var sequence = new LinkedList<long>();
        
        var makeItSmaller = 811589153 % (input.Length - 1);
        for (var i = 0; i < input.Length; i++)
        {
            var node = sequence.AddLast(input[i] * makeItSmaller);
            links.Add(node);
        }

        for (var m = 0; m < 10; m++)
        {
            //Print(sequence, makeItSmaller);
            foreach (var link in links)
            {
                var value = link.Value;
                var steps = value % (input.Length - 1);
                if (value == 0 || steps == 0)
                {
                    continue;
                }

                if (value > 0)
                {
                    var move = link.Next ?? sequence.First;
                    sequence.Remove(link);
                    for (var i = 1; i < steps; i++)
                    {
                        move = move.Next ?? sequence.First;
                    }

                    sequence.AddAfter(move, link);
                }
                else
                {
                    var move = link.Previous ?? sequence.Last;
                    sequence.Remove(link);
                    for (var i = -1; i > steps; i--)
                    {
                        move = move.Previous ?? sequence.Last;
                    }

                    sequence.AddBefore(move, link);
                }
            }
        }

        var zero = sequence.Find(0);
        long _1000 = 0;
        long _2000 = 0;
        long _3000 = 0;

        var temp = zero.Next ?? sequence.First;
        for (var i = 2; i <= 3000; i++)
        {
            temp = temp.Next ?? sequence.First;
            if (i == 1000)
            {
                _1000 = temp.Value;
            }
            else if (i == 2000)
            {
                _2000 = temp.Value;
            }
            else if (i == 3000)
            {
                _3000 = temp.Value;
            }
        }
        
        Console.WriteLine($"1000th = {_1000}");
        Console.WriteLine($"2000th = {_2000}");
        Console.WriteLine($"3000th = {_3000}");

        var a = _1000 / makeItSmaller * 811589153;
        var b = _2000 / makeItSmaller * 811589153;
        var c = _3000 / makeItSmaller * 811589153;
        
        Console.WriteLine($"1000th = {a}");
        Console.WriteLine($"2000th = {b}");
        Console.WriteLine($"3000th = {c}");

        return "" + (a + b + c);
    }

    private void Print(LinkedList<long> sequence, long test)
    {
        var first = sequence.First;
        Console.Write((first.Value / test) * 811589153);
        while (first.Next is not null)
        {
            first = first.Next;
            Console.Write(", " + (first.Value / test) * 811589153);
        }
        
        Console.WriteLine();
    }
}