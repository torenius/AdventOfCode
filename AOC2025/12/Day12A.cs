using AOC.Common;

namespace AOC2025._12;

public class Day12A : Day
{
    private readonly List<Shape> _shapes = [];
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        
        for (var i = 0; i <= 25; i += 5)
        {
            _shapes.Add(new Shape(input[i..(i + 4)]));
        }

        var regions = new List<Region>();
        for (var i = 30; i < input.Length; i++)
        {
            regions.Add(new Region(input[i]));
        }

        var result = new List<int> { 0, 0, 0};
        foreach (var region in regions)
        {
            var scenario = CanFillRegion(region);
            result[scenario]++;
        }
        
        Console.WriteLine($"Not enough space: {result[0]} Excess space: {result[1]} Need to calculate: {result[2]}");

        return result[1];
    }

    private int CanFillRegion(Region region)
    {
        // Will the required shapes take up more space than exists?
        var minArea = 0;
        for (var i = 0; i < region.Quantity.Count; i++)
        {
            var shape = _shapes[i];
            minArea += shape.TakeUpSpace *  region.Quantity[i];
        }

        if (minArea > region.Area) return 0;
        
        // Do we have excess space?
        var excessWidth = (region.Width / 3) * 3;
        var excessHeight = (region.Height / 3) * 3;
        var max3X3Area = excessWidth * excessHeight;
        var areaNeededIfGivenFull3X3Space = region.Quantity.Sum() * 9;
        if (areaNeededIfGivenFull3X3Space <= max3X3Area) return 1;
        
        // ToDo: Something magic...

        return 2;
    }

    private class Shape
    {
        public Shape(string[] input)
        {
            Index = input[0][0].ToInt();
            Original = new char[3,3];
            Original[0, 0] = input[1][0];
            Original[0, 1] = input[1][1];
            Original[0, 2] = input[1][2];
            Original[1, 0] = input[2][0];
            Original[1, 1] = input[2][1];
            Original[1, 2] = input[2][2];
            Original[2, 0] = input[3][0];
            Original[2, 1] = input[3][1];
            Original[2, 2] = input[3][2];

            TakeUpSpace = input[1].Count(c => c == '#') + 
                          input[2].Count(c => c == '#') + 
                          input[3].Count(c => c == '#');

            var tempOptions = new List<char[,]> { Original };
            var oneTurn = Rotate(Original);
            tempOptions.Add(oneTurn);
            var twoTurn = Rotate(oneTurn);
            tempOptions.Add(twoTurn);
            var threeTurn = Rotate(twoTurn);
            tempOptions.Add(threeTurn);

            var flippedOptions = tempOptions.Select(Flip).ToList();

            tempOptions.AddRange(flippedOptions);
            
            // Console.WriteLine(Index);
            // foreach (var option in tempOptions)
            // {
            //     Helper.Print(option, []);
            // }

            Options = [];
            foreach (var a in tempOptions)
            {
                var isUnique = Options.All(b => !IsEqual(a, b));

                if (isUnique)
                {
                    Options.Add(a);
                }
            }
            
            // Console.WriteLine(Index);
            // foreach (var option in Options)
            // {
            //     Helper.Print(option, []);
            // }
        }
        
        public int Index { get; }
        public int TakeUpSpace { get; }
        
        public char[,] Original { get; }
        
        public List<char[,]> Options { get; }

        private char[,] Rotate(char[,] original)
        {
            var result = new char[3, 3];
            result[0, 2] = original[0, 0];
            result[1, 2] = original[0, 1];
            result[2, 2] = original[0, 2];
            
            result[0, 1] = original[1, 0];
            result[1, 1] = original[1, 1];
            result[2, 1] = original[1, 2];
            
            result[0, 0] = original[2, 0];
            result[1, 0] = original[2, 1];
            result[2, 0] = original[2, 2];

            return result;
        }

        private static char[,] Flip(char[,] original)
        {
            var result = new char[3, 3];
            result[0, 2] = original[0, 0];
            result[0, 1] = original[0, 1];
            result[0, 0] = original[0, 2];
            
            result[1, 2] = original[1, 0];
            result[1, 1] = original[1, 1];
            result[1, 0] = original[1, 2];
            
            result[2, 2] = original[2, 0];
            result[2, 1] = original[2, 1];
            result[2, 0] = original[2, 2];

            return result;
        }

        private static bool IsEqual(char[,] a, char[,] b)
        {
            if (a[0, 0] != b[0, 0]) return false;
            if (a[0, 1] != b[0, 1]) return false;
            if (a[0, 2] != b[0, 2]) return false;
            
            if (a[1, 0] != b[1, 0]) return false;
            if (a[1, 1] != b[1, 1]) return false;
            if (a[1, 2] != b[1, 2]) return false;
            
            if (a[2, 0] != b[2, 0]) return false;
            if (a[2, 1] != b[2, 1]) return false;
            if (a[2, 2] != b[2, 2]) return false;

            return true;
        }
    }

    private class Region
    {
        public Region(string input)
        {
            var split = input.Split(": ");
            var wh = split[0].Split('x');
            Width = wh[0].ToInt();
            Height = wh[1].ToInt();
            Area = Width * Height;

            Quantity = split[1].Split(' ').ToIntList();
        }
        
        public int Width { get; }
        public int Height { get; }
        public int Area { get; }
        public List<int> Quantity { get; }
    }
    
}