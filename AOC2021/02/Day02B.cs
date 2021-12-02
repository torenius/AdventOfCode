using System;

namespace AOC2021._02
{
    public class Day02B : Day
    {
        public override void Run()
        {
            var input = GetInput();
            var hPos = 0;
            var depth = 0;
            var aim = 0;
            foreach (var s in input)
            {
                var i = new Instruction(s);
                switch (i.Direction)
                {
                    case "forward":
                        hPos += i.Step;
                        depth += (aim * i.Step);
                        break;
                    case "down":
                        aim += i.Step;
                        break;
                    case "up":
                        aim -= i.Step;
                        break;
                }
            }
            
            Console.WriteLine($"hPos = {hPos} depth = {depth} aim = {aim} = {hPos * depth}");
            
        }
    }
}
