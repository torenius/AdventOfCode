using AOC.Common;

namespace AOC2025._01;

public class Day01B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var position = 50;
        var zeroCount = 0;
        var lastWasZero = false;
        foreach (var instruction in input)
        {
            var rotation = new Rotation(instruction);
            var steps = rotation.Steps;
            if (steps >= 100)
            {
                zeroCount += steps / 100;
                steps %= 100;
            }
            else if (steps <= -100)
            {
                zeroCount += -1 * steps / 100;
                steps %= 100;
            }
            
            position += steps;

            if (position is > 100 or < -100) zeroCount++;
            
            position %= 100;

            if (position < 0)
            {
                position += 100;
                if (!lastWasZero) zeroCount++;
            }

            if (position > 99)
            {
                position -= 100;
                
                if (!lastWasZero) zeroCount++;
            }

            if (position == 0)
            {
                zeroCount++;
                lastWasZero = true;
            }
            else
            {
                lastWasZero = false;
            }
            
            //Console.WriteLine($"Pos: {position} , Rotation: {steps}, Instruction: {instruction} ZeroCount: {zeroCount} lastWasZero: {lastWasZero}");
        }
        
        return zeroCount;
    }

    private class Rotation
    {
        public Rotation(string input)
        {
            var direction = input[0] == 'L' ? -1 : 1;
            Steps = direction * input[1..].ToInt();
        }

        public int Steps { get; set; }
    }
}
