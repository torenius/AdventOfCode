using System.Diagnostics;
using AOC2022._01;
using AOC2022._02;

var day = new Day02B();

var stop = Stopwatch.StartNew();
var result = day.Run();
stop.Stop();

Console.WriteLine();
Console.WriteLine(result);
Console.WriteLine();

Console.WriteLine(stop.Elapsed);
