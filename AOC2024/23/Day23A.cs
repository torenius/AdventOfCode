namespace AOC2024._23;

public class Day23A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var computers = new List<Computer>();
        foreach (var row in input)
        {
            var com = row.Split("-");
            var left = GetOrCreateComputer(com[0]);
            var right = GetOrCreateComputer(com[1]);
            left.AddIfNotExists(right);
            right.AddIfNotExists(left);
            computers.Add(left);
            computers.Add(right);
        }

        var setsOfThree = new List<Computer[]>();
        foreach (var one in computers)
        {
            foreach (var two in one.ConnectedTo)
            {
                foreach (var three in two.ConnectedTo)
                {
                    if (three.ConnectedTo.Any(c => c.Name == one.Name))
                    {
                        var set = new[] { one, two, three };
                        Array.Sort(set, (a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

                        if (!setsOfThree.Any(sof => sof[0] == set[0] && sof[1] == set[1] && sof[2] == set[2]))
                        {
                            setsOfThree.Add(set);
                        }
                    }
                }
            }
        }

        var atLeastOneComputerStartWithT = 0;
        foreach (var com in setsOfThree)
        {
            for (var i = 0; i < com.Length; i++)
            {
                if (com[i].Name.StartsWith('t'))
                {
                    atLeastOneComputerStartWithT++;
                    break;
                }
            }
        }

        return atLeastOneComputerStartWithT;

        Computer GetOrCreateComputer(string computerName)
        {
            var computer = computers.FirstOrDefault(c => c.Name == computerName);
            return computer ?? new Computer{Name = computerName};
        }
    }

    private class Computer
    {
        public string Name { get; set; }
        public List<Computer> ConnectedTo { get; set; } = [];

        public void AddIfNotExists(Computer computer)
        {
            if (ConnectedTo.All(c => c.Name != computer.Name))
            {
                ConnectedTo.Add(computer);
            }
        }
    }
}