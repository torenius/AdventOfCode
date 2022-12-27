namespace AOC2022._21;

public class Day21B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var monkeys = input.Select(i => new Monkey(i)).ToDictionary(m => m.Name, m => m);

        var humn = monkeys["humn"];
        humn.Value = null;
        humn.LeftMonkey = "humn";
        humn.RightMonkey = "humn";
        var root = monkeys["root"];

        GetValue(monkeys, root);

        var rootLeft = monkeys[root.LeftMonkey];
        var rootRight = monkeys[root.RightMonkey];

        var missingSideMonkey = rootLeft.Value is null ? rootLeft : rootRight;
        var valueToMatch = (rootLeft.Value ?? rootRight.Value)!.Value;

        GetHumn(monkeys, missingSideMonkey, valueToMatch);
        
        return "" + humn.Value;
    }

    private static void GetHumn(Dictionary<string, Monkey> monkeys, Monkey monkey, long valueToMatch)
    {
        if (monkey.Name == "humn")
        {
            return;
        }
        var left = monkeys[monkey.LeftMonkey];
        var right = monkeys[monkey.RightMonkey];

        long newValueToMatch;
        if (left.Value is not null)
        {
            newValueToMatch = monkey.Equation switch
            {
                EquationEnum.Plus => valueToMatch - left.Value.Value,
                EquationEnum.Minus => left.Value.Value - valueToMatch,
                EquationEnum.Multiplication => valueToMatch / left.Value.Value,
                EquationEnum.Division => left.Value.Value / valueToMatch,
                _ => throw new ArgumentOutOfRangeException()
            };

            right.Value = newValueToMatch;
            GetHumn(monkeys, right, newValueToMatch);
        }
        else if (right.Value is not null)
        {
            newValueToMatch = monkey.Equation switch
            {
                EquationEnum.Plus => valueToMatch - right.Value.Value,
                EquationEnum.Minus => right.Value.Value + valueToMatch,
                EquationEnum.Multiplication => valueToMatch / right.Value.Value,
                EquationEnum.Division => right.Value.Value * valueToMatch,
                _ => throw new ArgumentOutOfRangeException()
            };

            left.Value = newValueToMatch;
            GetHumn(monkeys, left, newValueToMatch);
        }
    }

    private static void GetValue(Dictionary<string, Monkey> monkeys, Monkey monkey)
    {
        if (monkey.Name == "humn")
        {
            return;
        }
        
        var left = monkeys[monkey.LeftMonkey];
        var right = monkeys[monkey.RightMonkey];
        
        var leftValue = left.Value;
        var rightValue = right.Value;

        if (leftValue is null)
        {
            GetValue(monkeys, left);
            leftValue = left.Value;
        }

        if (rightValue is null)
        {
            GetValue(monkeys, right);
            rightValue = right.Value;
        }

        if (leftValue is not null && rightValue is not null)
        {
            long? value;
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
        }
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