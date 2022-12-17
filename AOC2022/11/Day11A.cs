using System.ComponentModel;

namespace AOC2022._11;

public class Day11A : Day
{
    protected override string Run()
    {
        var input = GetInputAsString().Split("\n\n");
        var monkeys = input.Select(i => new Monkey(i)).ToList();

        for (var r = 0; r < 20; r++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Any())
                {
                    monkey.InspectionTime++;
                    
                    var item = monkey.Items.Dequeue();
                    item = monkey.Operation(item);
                    item /= 3;

                    if (item % monkey.TestValue == 0)
                    {
                        monkeys[monkey.TrueMonkeyIndex].Items.Enqueue(item);
                    }
                    else
                    {
                        monkeys[monkey.FalseMonkeyIndex].Items.Enqueue(item);
                    }
                }
            }
        }

        var highestTwo = monkeys.Select(m => m.InspectionTime).OrderByDescending(m => m).Take(2).ToArray();

        return "" + highestTwo[0] * highestTwo[1];
    }

    private class Monkey
    {
        public Monkey(string data)
        {
            var rows = data.Split("\n");
            Index = rows[0].Replace("Monkey ", "").Replace(":", "").ToInt();
            Items = new Queue<int>(rows[1].Replace("  Starting items: ", "")
                .Split(", ").Select(i => i.ToInt()).ToList());
            Operation = GetOperation(rows[2].Replace("  Operation: new = ", ""));
            TestValue = rows[3].Replace("  Test: divisible by ", "").ToInt();
            TrueMonkeyIndex = rows[4].Replace("    If true: throw to monkey ", "").ToInt();
            FalseMonkeyIndex = rows[5].Replace("    If false: throw to monkey ", "").ToInt();
        }

        public int Index { get; set; }
        public Queue<int> Items { get; set; }
        public Func<int, int> Operation { get; set; }
        public int TestValue { get; set; }
        public int TrueMonkeyIndex { get; set; }
        public int FalseMonkeyIndex { get; set; }
        public int InspectionTime { get; set; }

        private static Func<int, int> GetOperation(string operation)
        {
            if (operation == "old * old")
            {
                return old => old * old;
            }

            if (operation.StartsWith("old * "))
            {
                return old => old * operation.Replace("old * ", "").ToInt();
            }
            
            if (operation.StartsWith("old + "))
            {
                return old => old + operation.Replace("old + ", "").ToInt();
            }

            throw new InvalidEnumArgumentException(operation);
        }
    }
}