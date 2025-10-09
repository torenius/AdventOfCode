using AOC.Common;

namespace AOC2016._10;

public class Day10B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var values = new List<string>();
        var bots = new Dictionary<(bool, int), Bot>();

        foreach (var line in input)
        {
            if (line.StartsWith("value"))
            {
                values.Add(line);
            }
            else
            {
                var words = line.Split(' ');
                var botNumber = words[1].ToInt();
                var isLowOutput = words[5] == "output";
                var lowNumber = words[6].ToInt();
                var isHighOutput = words[10] == "output";
                var highNumber = words[11].ToInt();

                var bot = GetOrCreate(false, botNumber);
                bot.Lower = GetOrCreate(isLowOutput, lowNumber);
                bot.Higher = GetOrCreate(isHighOutput, highNumber);
            }
        }

        foreach (var line in values)
        {
            var words = line.Split(' ');
            var value = words[1].ToInt();
            var botNumber=  words[5].ToInt();

            var bot = bots[(false, botNumber)];
            bot.GiveValue(value);
        }

        var result = 1;
        foreach (var output in bots.Values.Where(x => x.IsOutput && x.Number <= 2))
        {
            result *= output.History[0];
        }

        return result;

        Bot GetOrCreate(bool isOutput, int botNumber)
        {
            if (!bots.TryGetValue((isOutput, botNumber), out var bot))
            {
                bot = new Bot
                {
                    IsOutput = isOutput,
                    Number = botNumber
                };
                bots.Add((isOutput, botNumber), bot);
            }

            return bot;
        }
    }

    private class Bot
    {
        public bool IsOutput { get; set; }
        public int Number { get; set; }
        public Bot Lower { get; set; }
        public Bot Higher { get; set; }
        public int? LowerValue { get; set; }
        public int? HigherValue { get; set; }

        public List<int> History { get; set; } = [];

        public void GiveValue(int value)
        {
            History.Add(value);
            
            if (LowerValue is null)
            {
                LowerValue = value;
            }
            else
            {
                if (value >= LowerValue)
                {
                    HigherValue = value;
                }
                else
                {
                    HigherValue = LowerValue;
                    LowerValue = value;
                }
                
                Lower.GiveValue(LowerValue.Value);
                LowerValue = null;
                
                Higher.GiveValue(HigherValue.Value);
                HigherValue = null;
            }
        }
    }
}