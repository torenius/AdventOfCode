namespace AOC2023._07;

public class Day07B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();
        var hands = input.Select(x => new Hand(x)).ToList();
        //Console.WriteLine(string.Join(", ", hands));
        hands.Sort();
        //Console.WriteLine(string.Join(", ", hands));
        var sum = 0;
        for (var i = 0; i < hands.Count; i++)
        {
            var hand = hands[i];
            sum += hand.Bid * (i + 1);
        }
        
        return sum;
    }

    private class Hand : IComparable<Hand>
    {
        public Hand(string input)
        {
            var temp = input.Split(" ");
            Cards = temp[0].ToCharArray();
            CardValues = new int[Cards.Length];
            for (var i = 0; i < Cards.Length; i++)
            {
                var value = Cards[i] switch
                {
                    'A' => 14,
                    'K' => 13,
                    'Q' => 12,
                    'J' => 0,
                    'T' => 10,
                    _ => (int)char.GetNumericValue(Cards[i])
                };
                CardValues[i] = value;
            }
            
            Bid = temp[1].ToInt();

            var number = Cards.GroupBy(x => x).Select(x => new
                {
                    Card = x.Key,
                    Count = x.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToArray();

            if (number.Length == 1)
            {
                HandType = HandType.FiveOfAKind;
            }
            else if (number.Any(x => x.Card == 'J'))
            {
                var first = number.First(x => x.Card != 'J');
                var j = number.First(x => x.Card == 'J');
                HandType = j.Count switch
                {
                    4 => HandType.FiveOfAKind, // JJJJX
                    3 when first.Count == 2 => HandType.FiveOfAKind, // JJJXX
                    3 => HandType.FourOfAKind, // JJJXY
                    2 when first.Count == 3 => HandType.FiveOfAKind, // JJXXX
                    2 when first.Count == 2 => HandType.FourOfAKind, // JJXXY
                    2 => HandType.ThreeOfAKind, // JJXYZ
                    1 when first.Count == 4 => HandType.FiveOfAKind, // JXXXX
                    1 when first.Count == 3 => HandType.FourOfAKind, // JXXXY
                    1 when first.Count == 2 && number.Length == 3 => HandType.FullHouse, // JXXYY
                    1 when first.Count == 2 => HandType.ThreeOfAKind, // JXXYZ
                    _ => HandType.OnePair // JABCD
                };
            }
            else
            {
                var first = number[0];
                var second = number[1];

                HandType = first.Count switch
                {
                    4 => HandType.FourOfAKind,
                    3 when second.Count == 2 => HandType.FullHouse,
                    3 => HandType.ThreeOfAKind,
                    2 when second.Count == 2 => HandType.TwoPair,
                    2 => HandType.OnePair,
                    _ => HandType.HighCard
                };
            }
        }
        
        private char[] Cards { get; set; }
        private int[] CardValues { get; set; }
        public int Bid { get; set; }
        public HandType HandType { get; set; }

        public int CompareTo(Hand? other)
        {
            var type = HandType.CompareTo(other.HandType);
            if (type == 0)
            {
                for (var i = 0; i < CardValues.Length; i++)
                {
                    var compare = CardValues[i].CompareTo(other.CardValues[i]);
                    if (compare != 0)
                    {
                        return compare;
                    }
                }
            }

            return type;
        }

        public override string ToString()
        {
            return string.Join("", Cards);
        }
    }

    private enum HandType
    {
        FiveOfAKind = 6,
        FourOfAKind = 5,
        FullHouse = 4,
        ThreeOfAKind = 3,
        TwoPair = 2,
        OnePair = 1,
        HighCard = 0
    }
}
