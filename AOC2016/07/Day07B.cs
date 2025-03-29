using AOC.Common;

namespace AOC2016._07;

public class Day07B : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray();

        var result = new List<string>();
        for (var i = 0; i < input.Length; i++)
        {
            var insideBrackets = false;
            var foundSsl = false;
            for (var k = 0; k < input[i].Length - 2; k++)
            {
                if (input[i][k] == '[')
                {
                    insideBrackets = true;
                }
                else if (input[i][k] == ']')
                {
                    insideBrackets = false;
                }
                else
                {
                    if (!insideBrackets
                        && input[i][k] == input[i][k + 2]
                        && input[i][k] != input[i][k + 1])
                    {
                        for (var j = 0; j < input[i].Length - 2; j++)
                        {
                            if (input[i][j] == '[')
                            {
                                insideBrackets = true;
                            }
                            else if (input[i][j] == ']')
                            {
                                insideBrackets = false;
                            }
                            else if (insideBrackets
                                     && input[i][j] == input[i][j + 2]
                                     && input[i][j + 1] == input[i][k]
                                     && input[i][j] == input[i][k + 1])
                            {
                                foundSsl = true;
                                break;
                            }
                        }

                        if (foundSsl)
                        {
                            break;
                        }
                    }
                }
            }

            if (foundSsl)
            {
                result.Add(input[i]);
            }
        }
        
        return result.Count;
    }
}