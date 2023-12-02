namespace AOC2023._02;

public class Day02B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var data = input.Select(x => new GameData(x)).ToList();

        var sum = data.Sum(game => game.PowerOfMinSet());

        return "" + sum;
    }

    private class GameData
    {
        public GameData(string row)
        {
            var game = row.Split(":");
            GameId = game[0].Replace("Game ", "").ToInt();

            var sets = game[1].Split(";");
            foreach (var set in sets)
            {
                Sets.Add(new SetData(set));
            }
        }

        public int GameId { get; set; }
        public List<SetData> Sets { get; set; } = new();

        public int PowerOfMinSet()
        {
            var maxRed = Sets.Max(x => x.Red);
            var maxGreen = Sets.Max(x => x.Green);
            var maxBlue = Sets.Max(x => x.Blue);
            
            return maxRed * maxGreen * maxBlue;
        }
    }

    private class SetData
    {
        public SetData(string set)
        {
            var colors = set.Split(",");
            foreach (var color in colors)
            {
                var numberColor = color.Trim().Split(" ");
                switch (numberColor[1])
                {
                    case "red":
                        Red = numberColor[0].ToInt();
                        break;
                    case "green":
                        Green = numberColor[0].ToInt();
                        break;
                    case "blue":
                        Blue = numberColor[0].ToInt();
                        break;
                }
            }
        }

        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }
}