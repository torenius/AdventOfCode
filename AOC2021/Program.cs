using System;
using System.Diagnostics;
using AOC2021._01;
using AOC2021._02;
using AOC2021._03;
using AOC2021._04;
using AOC2021._05;
using AOC2021._06;
using AOC2021._07;
using AOC2021._08;
using AOC2021._09;
using AOC2021._10;
using AOC2021._11;
using AOC2021._12;
using AOC2021._13;
using AOC2021._14;
using AOC2021._15;
using AOC2021._16;
using AOC2021._17;
using AOC2021._18;
using AOC2021._20;

namespace AOC2021
{
    class Program
    {
        private static void Main(string[] args)
        {
            var d = new Day20B();

            var stop = Stopwatch.StartNew();
            d.Run();
            stop.Stop();
            Console.WriteLine(stop.Elapsed);
        }
    }
}
