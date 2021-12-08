using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021._08
{
    public class Entry
    {
        public Entry(string s)
        {
            var d = s.Split(" | ");
            SignalPattern = d[0].Split(" ").ToList();
            Output = d[1].Split(" ").ToList();

            for (var i = 0; i < SignalPattern.Count; i++)
            {
                SignalPattern[i] = Sort(SignalPattern[i]);
            }
            
            for (var i = 0; i < Output.Count; i++)
            {
                Output[i] = Sort(Output[i]);
            }
            
            Pattern = new Dictionary<int, string>();

            CalculatePattern();
        }

        public List<string> SignalPattern { get; set; }
        public List<string> Output { get; set; }
        public Dictionary<int, string> Pattern { get; set; }

        private void CalculatePattern()
        {
            Pattern.Add(1, SignalPattern.First(f => f.Length == 2));
            Pattern.Add(7, SignalPattern.First(f => f.Length == 3));
            Pattern.Add(4, SignalPattern.First(f => f.Length == 4));
            Pattern.Add(8, SignalPattern.First(f => f.Length == 7));
            
            // 9 är två mer än 4
            Pattern.Add(9, SignalPattern.First(f => Contains(Pattern[4], f) && f.Length == 6));
            
            // 3 innehåller 7 och är 5 långt
            Pattern.Add(3, SignalPattern.First(f => Contains(Pattern[7], f) && f.Length == 5));
            
            // 0 är 6 lång. Innehåller 7, men är också inte siffran 9
            Pattern.Add(0, SignalPattern.First(f => Contains(Pattern[7], f) && f.Length == 6 && !f.Equals(Pattern[9])));
            
            // 6 är 6 lång och inte redan hittad
            Pattern.Add(6, SignalPattern.First(f => f.Length == 6 && !Pattern.ContainsValue(f)));
            
            // 5 innehåller allt som 6, men är en kortare
            Pattern.Add(5, SignalPattern.First(f => Contains(f, Pattern[6]) && f.Length == 5));
            
            // 2 är den enda som inte är hittad
            Pattern.Add(2, SignalPattern.First(f => !Pattern.ContainsValue(f)));
        }

        private bool Contains(string a, string b)
        {
            var count = 0;
            for (var i = 0; i < a.Length; i++)
            {
                for (var k = 0; k < b.Length; k++)
                {
                    if (a[i] == b[k])
                    {
                        count++;
                    }
                }
            }

            return count == a.Length;
        }

        private string Sort(string s)
        {
            var temp = s.ToCharArray();
            Array.Sort(temp);

            return new string(temp);
        }

        public int OutputValue()
        {
            var value = "";
            foreach (var o in Output)
            {
                value += Pattern.First(f => f.Value.Equals(o)).Key;
            }

            return int.Parse(value);
        }
    }
}
