using AOC.Common;

namespace AOC2025._01;

public class Day01A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var position = 50;
        var zeroCount = 0;
        foreach (var instruction in input)
        {
            var rotation = new Rotation(instruction);
            position += rotation.Steps;
            position %= 100;
            if (position < 0) position += 100;
            if (position > 99) position -= 100;
            if (position == 0) zeroCount++;
            
            //Console.WriteLine($"Pos: {position} , Rotation: {rotation.Steps}, Instruction: {instruction}");
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
