namespace AOC2015._20;

public class Day20B : Day
{
    protected override string Run()
    {
        const int input = 36000000;

        var visit = new Dictionary<int, int>();
        var house = 0;
        var result = 0;
        do
        {
            house++;
            result = 0;
            visit.Add(house, 0);
            var divisors = Helper.GetDivisors(house);
            foreach (var div in divisors)
            {
                var v = visit[div];
                if (v < 50)
                {
                    result += div * 11;
                    visit[div]++;
                }
            }
        } while (result < input);
        
        return "" + house;
    }
}