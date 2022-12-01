﻿namespace AOC2022;

public abstract class Day
{
    protected Day()
    {
        Console.WriteLine(GetType().Name);    
    }
    
    private string GetBasePath()
    {
        const string basePath = @"C:\project\AdventOfCode\";
        var folder = GetType().Namespace.Replace("._", "\\");

        return Path.Combine(basePath, folder);
    }

    protected string GetInputAsString(string filename = "input.txt")
    {
        return File.ReadAllText(Path.Combine(GetBasePath(), filename));
    }

    protected string[] GetInputAsStringArray(string filename = "input.txt")
    {
        return File.ReadAllLines(Path.Combine(GetBasePath(), filename));
    }

    protected int[] GetInputAsInt(string filename = "input.txt")
    {
        return File.ReadLines(Path.Combine(GetBasePath(), filename)).Select(int.Parse).ToArray();
    }

    protected int[] GetInputIntArray(string filename = "input.txt", string separator = ",")
    {
        var input = GetInputAsStringArray(filename);
        return input[0].Split(separator).Select(int.Parse).ToArray();
    }

    protected int[,] GetInputAsIntMatrix(string filename = "input.txt")
    {
        var input = GetInputAsStringArray(filename);
        var matrix = new int[input.Length, input[0].Length];
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                matrix[y, x] = (int)char.GetNumericValue(input[y][x]);
            }
        }

        return matrix;
    }

    public abstract long Run();
}