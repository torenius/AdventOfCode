namespace AOC2021._11
{
    public class Octopus
    {
        public Octopus(int y, int x)
        {
            this.y = y;
            this.x = x;
        }

        public int y { get; set; }
        public int x { get; set; }
        public bool Flashed { get; set; }
        public int FlashCount { get; set; }
    }
}