using AOC.Common;

namespace AOC2025._10;

public class Day10A : Day
{
    protected override object Run()
    {
        var machines = GetInputAsStringArray()
            .Select(x => new Machine(x))
            .ToList();

        var sum = 0;
        foreach (var machine in machines)
        {
            var pressed = machine.GetPresses();
            sum += pressed.Count;
        }
        
        return sum;
    }

    private class Machine
    {
        public Machine(string input)
        {
            var data= input.Split(' ');
            LightsGoal = data[0][1..^1];

            Buttons = [];
            for (var i = 1; i < data.Length - 1; i++)
            {
                Buttons.Add(new Button(data[i][1..^1]));
            }

            Joltage = data[^1][1..^1].Split(',').ToIntList();
        }

        public string LightsGoal { get; set; }
        public List<Button> Buttons { get; set; }
        public List<int> Joltage { get; set; }

        private Dictionary<string, List<Button>> Presses = [];

        public List<Button> GetPresses()
        {
            var currentCharArray = new char[LightsGoal.Length];
            for (var i = 0; i < LightsGoal.Length; i++)
            {
                currentCharArray[i] = '.';
            }

            var current = new string(currentCharArray);
            
            Presses.Add(current, []);

            while (true)
            {
                foreach (var previous in Presses.ToDictionary(x => x.Key, x => x.Value))
                {
                    foreach (var button in Buttons)
                    {
                        var click = Click(previous.Key, button);
                        if (!Presses.ContainsKey(click))
                        {
                            var sequence = previous.Value.ToList();
                            sequence.Add(button);
                            Presses.Add(click, sequence);
                            
                            if (click == LightsGoal)
                            {
                                return sequence;
                            }
                        }
                    }    
                }
            }
        }
        
        private static string Click(string current, Button button)
        {
            var afterClick = new char[current.Length];
            for (var i = 0; i < current.Length; i++)
            {
                if (button.Buttons.Contains(i))
                {
                    afterClick[i] = current[i] == '.' ? '#' : '.';
                }
                else
                {
                    afterClick[i] = current[i];
                }
            }
            
            return new string(afterClick);
        }
        
    }

    private class Button
    {
        public Button(string input)
        {
            Buttons = input.Split(',').ToIntList();
        }
        
        public List<int> Buttons { get; set; }
    }
}