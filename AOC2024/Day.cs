using System.Diagnostics;
using TextCopy;

namespace AOC2024;

public abstract class Day
{
    private Stopwatch _stopwatch = null!;
    
    protected Day()
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine(GetType().Name);
    }
    
    private string GetBasePath()
    {
        const string basePath = @"C:\project\AdventOfCode\";
        var folder = GetType().Namespace!.Replace("._", "\\");

        return Path.Combine(basePath, folder);
    }

    protected string GetInputAsString(string filename = "input.txt")
    {
        return File.ReadAllText(Path.Combine(GetBasePath(), filename)).Replace(Environment.NewLine, "\n");
    }

    protected string[] GetInputAsStringArray(string filename = "input.txt")
    {
        return File.ReadAllLines(Path.Combine(GetBasePath(), filename));
    }

    protected int[,] GetInputAsIntMatrix(string filename = "input.txt")
    {
        var input = GetInputAsStringArray(filename);
        var matrix = new int[input.Length, input[0].Length];
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                matrix[y, x] = input[y][x].ToInt();
            }
        }

        return matrix;
    }
    
    protected char[,] GetInputAsCharMatrix(string filename = "input.txt")
    {
        var input = GetInputAsStringArray(filename);
        var matrix = new char[input.Length, input[0].Length];
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                matrix[y, x] = input[y][x];
            }
        }

        return matrix;
    }
    
    protected Grid<char> GetInputAsCharGrid(string filename = "input.txt") => new(GetInputAsCharMatrix(filename));

    protected abstract object Run();

    public void Start()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        
        _stopwatch = Stopwatch.StartNew();
        var result = Run();
        _stopwatch.Stop();
        
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(result);
        Console.WriteLine();

        PrintElapsedTime();
        
        ClipboardService.SetText(result.ToString()!);
    }

    public object Test()
    {
        _stopwatch = new Stopwatch();
        return Run();
    }

    private long _elapsedSinceLastPrint;
    protected void PrintElapsedTime(object comment, TimeSpan minElapsedTime)
    {
        if (_stopwatch.ElapsedTicks > _elapsedSinceLastPrint + minElapsedTime.Ticks)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(_stopwatch.Elapsed);
            Console.ForegroundColor = color;
            Console.WriteLine(" " + comment);
            _elapsedSinceLastPrint = _stopwatch.ElapsedTicks;
        }
    }

    protected void PrintElapsedTime(object? comment = null)
    {
        var color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(_stopwatch.Elapsed);
        Console.ForegroundColor = color;

        if (comment is not null)
        {
            Console.WriteLine(" " + comment);
        }
        else
        {
            Console.WriteLine();
        }
    }
}
