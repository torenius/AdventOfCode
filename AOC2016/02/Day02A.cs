using AOC.Common;

namespace AOC2016._02;

public class Day02A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        
        int[,] keypad = {{1, 2, 3}, {4, 5, 6 }, {7, 8, 9}};
        var keypadWidth = keypad.GetLength(0);
        var keypadHeight = keypad.GetLength(1);
        var x = 1;
        var y = 1;

        var code = "";
        foreach (var instructions in input)
        {
            foreach (var instruction in instructions.ToCharArray())
            {
                x += instruction switch
                {
                    'R' => 1,
                    'L' => -1,
                    _ => 0
                };
                
                x = x < 0 ? 0 : x;
                x = x >= keypadWidth ? keypadWidth - 1 : x;
                
                y += instruction switch
                {
                    'U' => -1,
                    'D' => 1,
                    _ => 0
                };
                
                y = y < 0 ? 0 : y;
                y = y >= keypadHeight ? keypadHeight -1 : y;
            }
            
            code += keypad[y, x];
        }

        return code;
    }
}