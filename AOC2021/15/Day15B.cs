using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC2021._15
{
    public class Day15B : Day
    {
        public override void Run()
        {
            var input = GetInputAsIntMatrix();
            input = GenerateBiggerMap(input);
            var goalY = input.GetLength(0) - 1;
            var goalX = input.GetLength(1) - 1;
            
            var n = AStar(input, goalY, goalX);
            Console.WriteLine(n.ToString());
            
            var list = new List<(int y, int x)>();
            list.Add((n.Y, n.X));
            
            var risk = -1 * input[0,0];
            while (n != null)
            {
                list.Add((n.Y, n.X));
                risk += input[n.Y, n.X];
                
                n = n.Parent;
            }
            Console.WriteLine($"Risk = {risk}");

            for (var y = 0; y <= goalY; y++)
            {
                for (var x = 0; x <= goalX; x++)
                {
                    Console.ResetColor();
                    if (list.Contains((y, x)))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(input[y,x]);
                }
                Console.WriteLine();
            }
        }

        private Node AStar(int[,] input, int goalY, int goalX)
        {
            var openList = new HashSet<Node>();
            var closedList = new HashSet<Node>();

            openList.Add(new Node(0, 0));

            while (openList.Count > 0)
            {
                // Minsta kostnaden först
                var minF = openList.Min(m => m.F);
                var q = openList.First(w => w.F == minF);
                openList.Remove(q);
                closedList.Add(q);
                
                if (q.Y == goalY && q.X == goalX)
                {
                    // Hittat mål
                    return q;
                }
                
                // Hitta möjliga vägar att gå till
                var dir = new[]
                {
                    (q.Y - 1, q.X),
                    (q.Y + 1, q.X),
                    (q.Y, q.X - 1),
                    (q.Y, q.X + 1)
                };
                var children = new List<Node>();
                foreach (var d in dir)
                {
                    if (d.Item1 >= 0 && d.Item1 <= goalY)
                    {
                        if (d.Item2 >= 0 && d.Item2 <= goalX)
                        {
                            var n = new Node(d.Item1, d.Item2)
                            {
                                Parent = q
                            };
                            children.Add(n);
                        }
                    }
                }
                
                // Kontrollera om barnvägarna
                foreach (var child in children)
                {
                    if (closedList.Any(c => c.Y == child.Y && c.X == child.X))
                    {
                        continue;
                    }
                    
                    child.G = q.G + input[child.Y, child.X];
                    // Manhattan Distance
                    child.H = Math.Abs(child.Y - goalY) + Math.Abs(child.X - goalX);
                    child.F = child.G + child.H;

                    var e = openList.FirstOrDefault(c => c.Y == child.Y && c.X == child.X);
                    if (e != null)
                    {
                        if (child.G < e.G)
                        {
                            openList.Remove(e);
                        }
                        else
                        {
                            continue;    
                        }
                    }

                    openList.Add(child);
                }
            }

            return null;
        }

        private int[,] GenerateBiggerMap(int[,] input)
        {
            var newInput = new int[input.GetLength(0) * 5, input.GetLength(1) * 5];
            for (var a = 0; a < 5; a++)
            {
                var addY = a * input.GetLength(0);
                for (var b = 0; b < 5; b++)
                {
                    var addX = b * input.GetLength(1);
                    for (var y = 0; y < input.GetLength(0); y++)
                    {
                        for (var x = 0; x < input.GetLength(1); x++)
                        {
                            var newY = y + addY;
                            var newX = x + addX;
                            var newValue = input[y, x] + a + b;
                            if (newValue > 9)
                            {
                                newValue -= 9;
                            }

                            newInput[newY, newX] = newValue;
                        }
                    }
                }
            }
            return newInput;
        }
    }
}
