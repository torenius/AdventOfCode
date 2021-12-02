namespace AOC2021._02
{
    public class Instruction
    {
        public Instruction(string s)
        {
            var x = s.Split(" ");
            Direction = x[0];
            Step = int.Parse(x[1]);
        }

        public string Direction { get; set; }
        public int Step { get; set; }
    }
}
