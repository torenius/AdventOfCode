namespace AOC2024._17;
 
public class Day17A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var computer = new Computer
        {
            A = input[0][11..].ToInt(),
            B = input[1][11..].ToInt(),
            C = input[2][11..].ToInt(),
            Program = input[4][9..].Split(",").ToIntArray().ToList(),
        };

        // computer.A = 2024;
        // computer.B = 0;
        // computer.C = 0;
        // computer.Program = [0,1,5,4,3,0];
        
        computer.Execute();
         
        return string.Join(",", computer.Output);
    }
 
    private class Computer
    {
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public List<int> Program { get; set; } = [];
        public List<int> Output { get; set; } = [];
 
        public int InstructionPointer { get; set; }
 
        public void Execute()
        {
            while (true)
            {
                if (InstructionPointer + 1 >= Program.Count) return;
                var opCode = Program[InstructionPointer];
                var literalOperand = Program[InstructionPointer + 1];

                var comboOperand = literalOperand switch
                {
                    4 => A,
                    5 => B,
                    6 => C,
                    _ => literalOperand
                };

                switch (opCode)
                {
                    case 0: // adv
                        A /= (int)Math.Pow(2, comboOperand);
                        break;
                    case 1: // bxl
                        B ^= literalOperand;
                        break;
                    case 2: // bst
                        B = comboOperand % 8;
                        break;
                    case 3: // jnz
                        if (A != 0)
                        {
                            InstructionPointer = literalOperand;
                            continue;
                        }
                        break;
                    case 4: // bxc
                        B ^= C;
                        break;
                    case 5: // out
                        Output.Add(comboOperand % 8);
                        break;
                    case 6: // bdv
                        B = A / (int)Math.Pow(2, comboOperand);
                        break;
                    case 7: // cdv
                        C = A / (int)Math.Pow(2, comboOperand);
                        break;
                }

                InstructionPointer += 2;
            }
        }
    }
}