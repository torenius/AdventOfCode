using System.ComponentModel;

namespace AOC2022._11;

public class Day11B : Day
{
    protected override string Run()
    {
        var input = GetInputAsString().Split("\n\n");
        var monkeys = input.Select(i => new Monkey(i)).ToList();

        var lcm = (long)monkeys.Select(m => (int) m.TestValue).LCM();

        for (var r = 1; r <= 10000; r++)
        {
            foreach (var monkey in monkeys)
            {
                while (monkey.Items.Any())
                {
                    monkey.InspectionTime++;

                    var item = monkey.Items.Dequeue();
                    item = monkey.Operation(item);

                    var monkeyIndex = monkey.FalseMonkeyIndex;
                    if (item % monkey.TestValue == 0)
                    {
                        monkeyIndex = monkey.TrueMonkeyIndex;
                    }

                    monkeys[monkeyIndex].Items.Enqueue(item % lcm);
                }
            }

            if (r is 1 or 20 or 1000 or 2000 or 3000 or 4000 or 5000 or 6000 or 7000 or 8000 or 9000 or 10000)
            {
                Console.WriteLine($"== After round {r} ==");
                foreach (var monkey in monkeys)
                {
                    Console.WriteLine($"Monkey {monkey.Index} inspected items {monkey.InspectionTime} times.");
                }
                Console.WriteLine();
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
            Items = new Queue<long>(rows[1].Replace("  Starting items: ", "")
                .Split(", ").Select(i => i.ToLong()));
            OperationString = rows[2].Replace("  Operation: new = ", "");
            Operation = GetOperation(OperationString);
            TestValue = rows[3].Replace("  Test: divisible by ", "").ToLong();
            TrueMonkeyIndex = rows[4].Replace("    If true: throw to monkey ", "").ToInt();
            FalseMonkeyIndex = rows[5].Replace("    If false: throw to monkey ", "").ToInt();
        }

        public int Index { get; set; }
        public Queue<long> Items { get; set; }
        public string OperationString { get; set; }
        public Func<long, long> Operation { get; set; }
        public long TestValue { get; set; }
        public int TrueMonkeyIndex { get; set; }
        public int FalseMonkeyIndex { get; set; }
        public long InspectionTime { get; set; }

        private static Func<long, long> GetOperation(string operation)
        {
            if (operation == "old * old")
            {
                return old => old * old;
            }

            if (operation.StartsWith("old * "))
            {
                return old => old * operation.Replace("old * ", "").ToLong();
            }
            
            if (operation.StartsWith("old + "))
            {
                return old => old + operation.Replace("old + ", "").ToLong();
            }

            throw new InvalidEnumArgumentException(operation);
        }
    }
}