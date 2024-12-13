namespace AOC2024._13;

public class Day13B : Day
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
            clawMachine.PriceX += 10000000000000;
            clawMachine.PriceY += 10000000000000;
            Calculate(clawMachine);
        }
        
        return clawMachines.Where(cm => cm.Victory).Sum(cm => cm.ACount * 3 + cm.BCount);
    }

    private static void Calculate(ClawMachine clawMachine)
    {
        var b = (clawMachine.Ax * clawMachine.PriceY - clawMachine.Ay * clawMachine.PriceX) / (clawMachine.Ax * clawMachine.By - clawMachine.Bx * clawMachine.Ay);
        var a = (clawMachine.PriceX - b * clawMachine.Bx) / clawMachine.Ax;

        if (clawMachine.Ax * a + clawMachine.Bx * b == clawMachine.PriceX &&
            clawMachine.Ay * a + clawMachine.By * b == clawMachine.PriceY)
        {
            clawMachine.Victory = true;
            clawMachine.ACount = a;
            clawMachine.BCount = b;
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
        public long PriceX { get; set; }
        public long PriceY { get; set; }
        public long ACount { get; set; }
        public long BCount { get; set; }
        public bool Victory { get; set; }
    }
}
