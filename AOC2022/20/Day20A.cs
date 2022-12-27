using System.Data.SqlTypes;
using System.Threading.Channels;

namespace AOC2022._20;

public class Day20A : Day
{
    protected override string Run()
    {
        var input = GetInputAsIntArray();

        var links = new List<LinkedListNode<int>>();
        var sequence = new LinkedList<int>();
        for (var i = 0; i < input.Length; i++)
        {
            var node = sequence.AddLast(input[i]);
            links.Add(node);
        }

        foreach (var link in links)
        {
            var value = link.Value;
            if (value == 0)
            {
                continue;
            }
            
            if (value > 0)
            {
                var move = link.Next ?? sequence.First;
                sequence.Remove(link);
                for (var i = 1; i < value; i++)
                {
                    move = move.Next ?? sequence.First;
                }

                sequence.AddAfter(move, link);
            }
            else
            {
                var move = link.Previous ?? sequence.Last;
                sequence.Remove(link);
                for (var i = -1; i > value; i--)
                {
                    move = move.Previous ?? sequence.Last;
                }

                sequence.AddBefore(move, link);
            }
        }
        
        var zero = sequence.Find(0);
        var _1000 = 0;
        var _2000 = 0;
        var _3000 = 0;

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

        return "" + (_1000 + _2000 + _3000);
    }
}