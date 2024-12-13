namespace AOC2024._13;

public class Day13A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var clawMachines = new List<ClawMachine>();
        for (var i = 0; i < input.Length; i+=4)
        {
            clawMachines.Add(new ClawMachine(input[i], input[i + 1], input[i + 2]));
        }

        foreach (var clawMachine in clawMachines)
        {
            BruteForce(clawMachine);
        }

        return clawMachines.Where(cm => cm.Victory).Sum(cm => cm.ACount * 3 + cm.BCount);
    }

    private static void BruteForce(ClawMachine clawMachine)
    {
        var costMin = int.MaxValue;
        var costMinA = 0;
        var costMinB = 0;
        
        for (var a = 0; a <= 100; a++)
        {
            for (var b = 0; b <= 100; b++)
            {
                if (clawMachine.Ax * a + clawMachine.Bx * b == clawMachine.PriceX &&
                    clawMachine.Ay * a + clawMachine.By * b == clawMachine.PriceY)
                {
                    clawMachine.Victory = true;
                    if (b * 3 + a < costMin)
                    {
                        costMin = b * 3 + a;
                        costMinA = a;
                        costMinB = b;
                    }
                }
            }
        }

        if (clawMachine.Victory)
        {
            clawMachine.ACount = costMinA;
            clawMachine.BCount = costMinB;
        }
    }

    private class ClawMachine
    {
        public ClawMachine(string a, string b, string price)
        {
            var aa = a[10..].Split(", ");
            var bb = b[10..].Split(", ");
            var pp = price[7..].Split(", ");

            Ax = aa[0][2..].ToInt();
            Ay = aa[1][2..].ToInt();
            Bx = bb[0][2..].ToInt();
            By = bb[1][2..].ToInt();
            PriceX = pp[0][2..].ToInt();
            PriceY = pp[1][2..].ToInt();
        }
        public int Ax { get; set; }
        public int Ay { get; set; }
        public int Bx { get; set; }
        public int By { get; set; }
        public int PriceX { get; set; }
        public int PriceY { get; set; }
        public int ACount { get; set; }
        public int BCount { get; set; }
        public bool Victory { get; set; }
    }
}
