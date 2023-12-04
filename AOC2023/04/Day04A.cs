namespace AOC2023._04;

public class Day04A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var cards = input.Select(x => new Card(x)).ToList();
        var sum = 0;
        foreach (var card in cards)
        {
            sum += card.Score();
        }
        
        return "" + sum;
    }

    private class Card
    {
        public Card(string input)
        {
            var temp = input.Split(":");
            CardId = temp[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].ToInt();
            
            temp = temp[1].Split("|");
            WinningNumbers = temp[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToInt()).ToList();
            OurNumbers = temp[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToInt()).ToList();
        }

        public int CardId { get; set; }
        public List<int> WinningNumbers { get; set; } = new();
        public List<int> OurNumbers { get; set; } = new();

        public int Score()
        {
            var wining = OurNumbers.Where(x => WinningNumbers.Contains(x)).ToList();

            var score = wining.Count == 0 ? 0 : 1;
            for (var i = 1; i < wining.Count; i++)
            {
                score *= 2;
            }
            
            return score;
        }
    }
}
