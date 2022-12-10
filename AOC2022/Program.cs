using System.Diagnostics;
using AOC2022._01;
using AOC2022._02;
using AOC2022._03;
using AOC2022._04;
using AOC2022._05;
using AOC2022._06;
using AOC2022._07;
using AOC2022._08;
using AOC2022._09;
using AOC2022._10;

Console.ForegroundColor = ConsoleColor.DarkRed;
var day = new Day10B();

Console.ForegroundColor = ConsoleColor.Green;
var stop = Stopwatch.StartNew();
var result = day.Run();
stop.Stop();

Console.WriteLine();
Console.ForegroundColor = ConsoleColor.DarkGreen;
Console.WriteLine(result);
Console.WriteLine();

Console.ForegroundColor = ConsoleColor.White;
Console.WriteLine(stop.Elapsed);
