namespace AOC2022._02;

public class Day02A : Day
{
    public override string Run()
    {
        var input = GetInputAsStringArray();

        var yourScore = 0;
        
        foreach (var round in input)
        {
            var move = round.Split(" ");

            var opponent = RockPaperScissor.Rock;
            var you = RockPaperScissor.Rock;

            opponent = move[0] switch
            {
                "B" => RockPaperScissor.Paper,
                "C" => RockPaperScissor.Scissor,
                _ => opponent
            };

            you = move[1] switch
            {
                "Y" => RockPaperScissor.Paper,
                "Z" => RockPaperScissor.Scissor,
                _ => you
            };

            yourScore += YourScore(opponent, you);
        }

        return yourScore.ToString();
    }

    private static int YourScore(RockPaperScissor opponent, RockPaperScissor you)
    {
        var score = (int)you;

        if (opponent == you)
        {
            score += 3; // Draw
        }
        else switch (opponent)
        {
            case RockPaperScissor.Rock:
            {
                if (you == RockPaperScissor.Paper)
                {
                    score += 6; // you win
                }
                break;
            }
            case RockPaperScissor.Paper:
            {
                if (you == RockPaperScissor.Scissor)
                {
                    score += 6; // you win
                }
                break;
            }
            case RockPaperScissor.Scissor:
                if (you == RockPaperScissor.Rock)
                {
                    score += 6; // you win
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(opponent), opponent, null);
        }

        return score;
    }

    private enum RockPaperScissor
    {
        Rock = 1,
        Paper = 2,
        Scissor = 3
    }
}
