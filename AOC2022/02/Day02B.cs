namespace AOC2022._02;

public class Day02B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        var yourScore = 0;
        
        foreach (var round in input)
        {
            var move = round.Split(" ");

            var opponent = move[0] switch
            {
                "A" => RockPaperScissor.Rock,
                "B" => RockPaperScissor.Paper,
                "C" => RockPaperScissor.Scissor,
                _ => throw new ArgumentOutOfRangeException()
            };

            var you = move[1] switch
            {
                "X" => YouLose[opponent],
                "Y" => opponent,
                "Z" => YouWin[opponent],
                _ => throw new ArgumentOutOfRangeException()
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

    private static readonly Dictionary<RockPaperScissor, RockPaperScissor> YouLose = new()
    {
        {RockPaperScissor.Rock, RockPaperScissor.Scissor},
        {RockPaperScissor.Paper, RockPaperScissor.Rock},
        {RockPaperScissor.Scissor, RockPaperScissor.Paper}
    };
    
    private static readonly Dictionary<RockPaperScissor, RockPaperScissor> YouWin = new()
    {
        {RockPaperScissor.Rock, RockPaperScissor.Paper},
        {RockPaperScissor.Paper, RockPaperScissor.Scissor},
        {RockPaperScissor.Scissor, RockPaperScissor.Rock}
    };

    private enum RockPaperScissor
    {
        Rock = 1,
        Paper = 2,
        Scissor = 3
    }
}
