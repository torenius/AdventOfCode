namespace AOC2015._25;

public class Day25A : Day
{
    protected override string Run()
    {
        var input = GetInputAsString()[80..].TrimEnd('.', '\n');
        var temp = input.Split(", ");
        
        Console.WriteLine();
        
        var targetColumn = temp[1][6..].ToInt();
        var targetRow = temp[0].ToInt();

        // targetColumn = 5;
        // targetRow = 3;
        
        var numberOfGenerations = 0;
        
        // Sum of target column number and all column numbers before
        // is the amount of numbers needed to be generate for the first value in that column.
        for (var i = 1; i <= targetColumn; i++)
        {
            numberOfGenerations += i;
        }

        // To travel down the rows we need to add target column and x - 1 columns after it
        for (var i = targetColumn; i < targetColumn + (targetRow - 1); i++)
        {
            numberOfGenerations += i;
        }

        Console.WriteLine($"NumberOfGenerations: {numberOfGenerations}");
        long code = 20151125;
        var index = 1;
        while (index < numberOfGenerations)
        {
            //Console.WriteLine($"Index: {index} Code: {code}");
            code = GetNextNumber(code);
            index++;
        }
        
        return "" + code;
    }

    private static long GetNextNumber(long current)
    {
        var temp = current * 252533;
        var longDivision = temp / 33554393;
        var reminder = temp - (longDivision * 33554393);

        return reminder;
    }
}