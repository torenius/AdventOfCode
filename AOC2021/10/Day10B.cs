using System;
using System.Collections.Generic;

namespace AOC2021._10
{
    public class Day10B : Day
    {
        public override void Run()
        {
            var input = GetInput();
            var okay = new Dictionary<char, char>
            {
                {'(', ')'},
                {'[', ']'},
                {'{', '}'},
                {'<', '>'}
            };

            var score = new Dictionary<char, (char closing, int score)>
            {
                {'(', (')', 1)},
                {'[', (']', 2)},
                {'{', ('}', 3)},
                {'<', ('>', 4)}
            };

            var sumList = new List<long>();
            foreach (var s in input)
            {
                var isBroken = false;
                var stack = new Stack<char>();
                stack.Push(s[0]);
                for (var i = 1; i < s.Length; i++)
                {
                    if (s[i] is '(' or '[' or '{' or '<')
                    {
                        stack.Push(s[i]);
                    }
                    else
                    {
                        var c = stack.Pop();;
                        if (okay[c] != s[i])
                        {
                            isBroken = true;
                            break;
                        }
                    }
                }

                if (!isBroken)
                {
                    long sumScore = 0;
                    while (stack.Count > 0)
                    {
                        var c = stack.Pop();
                        sumScore *= 5;
                        sumScore += score[c].score;
                        Console.Write(score[c].closing);
                    }
                    Console.WriteLine(sumScore);
                    sumList.Add(sumScore);
                }
            }
            
            sumList.Sort();

            var x = (int) Math.Floor(sumList.Count / 2.0);
            Console.WriteLine($"sum = {sumList[x]}");
        }
    }
}
