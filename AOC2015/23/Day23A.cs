using System.Runtime.Intrinsics.X86;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace AOC2015._23;

public class Day23A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var instructions = input.Select(x => new Instruction(x)).ToList();
        var index = 0;
        var a = 0;
        var b = 0;
        while (index >= 0 && index < instructions.Count)
        {
            //Console.WriteLine($"{instructions[index].OriginalValue} A:{a} B:{b} i: {index}");
            var (newA, newB, newIndex) = instructions[index].Execute(a, b, index);
            a = newA;
            b = newB;
            index = newIndex;
        }
        
        return "" + b;
    }

    private class Instruction
    {
        public Instruction(string input)
        {
            OriginalValue = input;
            var temp = input.Split(" ");
            switch (temp[0])
            {
                case "hlf":
                    InstructionType = InstructionType.hlf;
                    Registry = temp[1].ToCharArray()[0];
                    break;
                case "tpl":
                    InstructionType = InstructionType.tpl;
                    Registry = temp[1].ToCharArray()[0];
                    break;
                case "inc":
                    InstructionType = InstructionType.inc;
                    Registry = temp[1].ToCharArray()[0];
                    break;
                case "jmp":
                    InstructionType = InstructionType.jmp;
                    Offset = temp[1][1..].ToInt();
                    if (temp[1][0] == '-') Offset *= -1;
                    break;
                case "jie":
                    InstructionType = InstructionType.jie;
                    Offset = temp[2][1..].ToInt();
                    if (temp[2][0] == '-') Offset *= -1;
                    Registry = temp[1].ToCharArray()[0];
                    break;
                case "jio":
                    InstructionType = InstructionType.jio;
                    Offset = temp[2][1..].ToInt();
                    if (temp[2][0] == '-') Offset *= -1;
                    Registry = temp[1].ToCharArray()[0];
                    break;
                default:
                    throw new ArgumentOutOfRangeException("temp[0]");
            }
        }

        public string OriginalValue { get; set; }
        public InstructionType InstructionType { get; set; }
        public char Registry { get; set; }
        public int Offset { get; set; }

        public (int A, int B, int Index) Execute(int a, int b, int index)
        {
            if (Registry == 'a')
            {
                var (xA, indexA) = Execute(a, index);
                return (xA, b, indexA);
            }
            
            var (xB, indexB) = Execute(b, index);
            return (a, xB, indexB);
        }
        
        public (int X, int Index) Execute(int x, int index)
        {
            return InstructionType switch
            {
                InstructionType.hlf => (x / 2, index + 1),
                InstructionType.tpl => (x * 3, index + 1),
                InstructionType.inc => (x + 1, index + 1),
                InstructionType.jmp => (x, index + Offset),
                InstructionType.jie when x % 2 == 0 => (x, index + Offset),
                InstructionType.jie => (x, index + 1),
                InstructionType.jio when x == 1 => (x, index + Offset),
                InstructionType.jio => (x, index + 1),
                _ => (x, index)
            };
        }
    }

    private enum InstructionType
    {
        hlf,
        tpl,
        inc,
        jmp,
        jie,
        jio
    }
}