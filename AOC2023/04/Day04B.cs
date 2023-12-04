namespace AOC2023._04;

public class Day04B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var cards = input.Select(x => new Card(x)).ToList();
        var winningCards = cards.Where(x => x.NumberOfWins > 0).ToList();

        var allCards = cards.Where(x => x.NumberOfWins == 0).ToList();
        var newWinners = new List<Card>();
        newWinners.AddRange(winningCards);
        do
        {
            allCards.AddRange(newWinners);
            newWinners = GetNewCards(cards, newWinners.Where(x => x.NumberOfWins > 0).ToList());
            
        } while (newWinners.Count != 0);

        var test = allCards.GroupBy(x => x.CardId).Select(x => new
        {
            CardId = x.Key,
            Count = x.Count()
        })
            .ToList();
        
        return "" + allCards.Count;
    }

    private List<Card> GetNewCards(List<Card> allCards, List<Card> winners)
    {
        var newWinners = new List<Card>();
        foreach (var winner in winners)
        {
            for (var i = winner.CardId; i < allCards.Count && i < winner.CardId + winner.NumberOfWins; i++)
            {
                //Console.WriteLine($"{winner.CardId} {allCards[i].CardId}");
                var card = new Card
                {
                    CardId = allCards[i].CardId,
                    NumberOfWins = allCards[i].NumberOfWins
                };
                newWinners.Add(card);
            }
        }

        return newWinners;
    }

    private class Card
    {
        public Card()
        {
            
        }
        public Card(string input)
        {
            var temp = input.Split(":");
            CardId = temp[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].ToInt();
            
            temp = temp[1].Split("|");
            var winningNumbers = temp[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToInt()).ToList();
            var ourNumbers = temp[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToInt()).ToList();
            NumberOfWins = ourNumbers.Count(x => winningNumbers.Contains(x));
        }

        public int CardId { get; set; }
        public int NumberOfWins { get; set; }
        
    }
}
