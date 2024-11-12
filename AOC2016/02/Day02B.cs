namespace AOC2016._02;

public class Day02B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        
        char[,] keypad = {
            {'0', '0', '1', '0', '0'},
            {'0', '2', '3', '4', '0'},
            {'5', '6', '7', '8', '9'},
            {'0', 'A', 'B', 'C', '0'},
            {'0', '0', 'D', '0', '0'},
        };
        var keypadWidth = keypad.GetLength(0);
        var keypadHeight = keypad.GetLength(1);
        var x = 0;
        var y = 2;

        var code = "";
        foreach (var instructions in input)
        {
            foreach (var instruction in instructions.ToCharArray())
            {
                var xInc = instruction switch
                {
                    'R' => 1,
                    'L' => -1,
                    _ => 0
                };
                
                var yInc = instruction switch
                {
                    'U' => -1,
                    'D' => 1,
                    _ => 0
                };
                
                if (x + xInc >= 0 &&
                    x + xInc < keypadWidth &&
                    y + yInc >= 0 &&
                    y + yInc < keypadHeight &&
                    keypad[x + xInc, y + yInc] != '0')
                {
                    x += xInc;
                    y += yInc;
                }
            }
            
            code += keypad[y, x];
        }

        return code;
    }
}