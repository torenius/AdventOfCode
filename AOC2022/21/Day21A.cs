namespace AOC2022._21;

public class Day21A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var monkeys = input.Select(i => new Monkey(i)).ToDictionary(m => m.Name, m => m);

        var root = monkeys["root"];

        var value = GetValue(monkeys, root);

        return "" + value;
    }

    private static long GetValue(Dictionary<string, Monkey> monkeys, Monkey monkey)
    {
        if (monkey.Value is not null)
        {
            return monkey.Value.Value;
        }

        var left = monkeys[monkey.LeftMonkey];
        var right = monkeys[monkey.RightMonkey];

        var leftValue = GetValue(monkeys, left);
        var rightValue = GetValue(monkeys, right);

        long value = 0;
        switch (monkey.Equation)
        {
            case EquationEnum.Plus:
                value = leftValue + rightValue;
                break;
            case EquationEnum.Minus:
                value = leftValue - rightValue;
                break;
            case EquationEnum.Multiplication:
                value = leftValue * rightValue;
                break;
            case EquationEnum.Division:
                value = leftValue / rightValue;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        monkey.Value = value;

        return value;
    }
    

    private class Monkey
    {
        public Monkey(string input)
        {
            var temp = input.Split(": ");
            Name = temp[0];

            if (int.TryParse(temp[1], out var value))
            {
                Value = value;
            }
            else
            {
                string[] monkeys;
                if (temp[1].Contains('+'))
                {
                    monkeys = temp[1].Split(" + ");
                    Equation = EquationEnum.Plus;
                }
                else if (temp[1].Contains('-'))
                {
                    monkeys = temp[1].Split(" - ");
                    Equation = EquationEnum.Minus;
                }
                else if (temp[1].Contains('*'))
                {
                    monkeys = temp[1].Split(" * ");
                    Equation = EquationEnum.Multiplication;
                }
                else
                {
                    monkeys = temp[1].Split(" / ");
                    Equation = EquationEnum.Division;
                }

                LeftMonkey = monkeys[0];
                RightMonkey = monkeys[1];
            }
        }
        
        public string Name { get; set; }
        public long? Value { get; set; }

        public string LeftMonkey { get; set; }
        public EquationEnum Equation { get; set; }
        public string RightMonkey { get; set; }
    }

    private enum EquationEnum
    {
        Plus,
        Minus,
        Multiplication,
        Division
    }
}