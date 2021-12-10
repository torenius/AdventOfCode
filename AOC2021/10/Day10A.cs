using System;
using System.Collections.Generic;

namespace AOC2021._10
{
    public class Day10A : Day
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

            var score = new Dictionary<char, int>
            {
                {')', 3},
                {']', 57},
                {'}', 1197},
                {'>', 25137}
            };

            var sum = 0;
            foreach (var s in input)
            {
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
                            Console.WriteLine($"start: {c} end: {s[i]} score: {score[s[i]]}");
                            sum += score[s[i]];
                            break;
                        }
                    }
                }
            }
            
            Console.WriteLine($"sum = {sum}");
        }
    }
}
