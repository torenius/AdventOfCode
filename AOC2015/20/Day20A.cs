namespace AOC2015._20;

public class Day20A : Day
{
    protected override string Run()
    {
        const int input = 36000000;
        
        var house = 0;
        var result = 0;
        do
        {
            house++;
            var divisors = Helper.GetDivisors(house);
            result = divisors.Select(x => x * 10).Sum();

        } while (result < input);

        return "" + house;
    }
}