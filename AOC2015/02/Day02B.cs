namespace AOC2015._02;

public class Day02B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        var result = 0;
        foreach (var i in input)
        {
            var b = new Box(i);

            result += b.RibbonNeeded();
        }

        return "" + result;
    }
    
    private class Box
    {
        public Box(string input)
        {
            var b = input.Split('x');
            Length = b[0].ToInt();
            Width = b[1].ToInt();
            Height = b[2].ToInt();
        }

        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int SquareFootOfPaperNeeded()
        {
            var lw = Length * Width;
            var wh = Width * Height;
            var hl = Height * Length;

            var minSide = Helper.Min(lw, wh, hl);
            
            return 2 * lw + 2 * wh + 2 * hl + minSide;
        }

        public int RibbonNeeded()
        {
            var min = Helper.Min(Length * 2 + Width * 2, Length * 2 + Height * 2, Width * 2 + Height * 2);

            return min + (Length * Width * Height);
        }
    }
}