using System;

namespace AOC2021._02
{
    public class Day02A : Day
    {
        public override void Run()
        {
            var input = GetInput();
            var hPos = 0;
            var depth = 0;
            foreach (var s in input)
            {
                var i = new Instruction(s);
                switch (i.Direction)
                {
                    case "forward":
                        hPos += i.Step;
                        break;
                    case "down":
                        depth += i.Step;
                        break;
                    case "up":
                        depth -= i.Step;
                        break;
                }
            }
            
            Console.WriteLine($"hPos = {hPos} depth = {depth} = {hPos * depth}");
            
        }
    }
}
