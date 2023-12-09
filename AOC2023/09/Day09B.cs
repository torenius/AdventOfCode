namespace AOC2023._09;

public class Day09B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var readings = input.Select(x => new Reading(x)).ToList();
        var sum = 0;
        foreach (var reading in readings)
        {
            var history = reading.GetFullHistory();
            var value = history.Extrapolate();
            sum += value;
        }
        
        return sum;
    }

    public class Reading
    {
        private Reading()
        {
            
        }
        public Reading(string input)
        {
            var temp = input.Split(" ");
            Values = new int[temp.Length];
            for (var i = 0; i < temp.Length; i++)
            {
                Values[i] = temp[i].ToInt();
            }
        }

        public int[] Values { get; set; }
        public Reading? Parent { get; set; }
        public Reading? Child { get; set; }

        public Reading GetChild()
        {
            var values = new int[Values.Length-1];
            for (var i = 0; i < Values.Length - 1; i++)
            {
                values[i] = Values[i + 1] - Values[i];
            }

            var child = new Reading
            {
                Values = values,
                Parent = this
            };

            Child = child;
            return child;
        }

        public Reading GetFullHistory()
        {
            if (Values.All(x => x == 0))
            {
                return this;
            }
            
            if (Child == null)
            {
                Child = GetChild();
            }
            
            return Child.GetFullHistory();
        }

        public int Extrapolate(int sum = 0)
        {
            var left = Values[0];
            
            if (Parent == null)
            {
                return left - sum;
            }
            
            return Parent.Extrapolate(left - sum);
        }
    }
}