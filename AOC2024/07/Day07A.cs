namespace AOC2024._07;

public class Day07A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var equations = input.Select(x => new Equation(x)).ToList();
        return equations.Where(x => x.IsValid()).Sum(x => x.Result);
    }

    private class Equation
    {
        public Equation(string input)
        {
            var x = input.Split(": ");
            Result = x[0].ToLong();
            Input = x[1].Split(" ").ToLongArray();
        }
        public long Result { get; set; }
        public long[] Input { get; set; }

        public bool IsValid()
        {
            foreach (var operations in Helper.GetPossibleCombinations(Input.Length - 1, '+', '*'))
            {
                if (Result == Execute(operations)) return true;
            }

            return false;
        }

        private long Execute(char[] operations)
        {
            var result = Input[0];
            for (var i = 0; i < operations.Length; i++)
            {
                switch (operations[i])
                {
                    case '+':
                        result += Input[i+1];
                        break;
                    case '*':
                        result *= Input[i+1];
                        break;
                }
            }
            
            return result;
        }
    }
}