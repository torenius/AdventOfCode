using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AOC2021._12
{
    public class Day12A : Day
    {
        private Dictionary<string, Cave> _caves;
        private int _count = 0;
        public override void Run()
        {
            _caves = new Dictionary<string, Cave>();
            foreach (var input in GetInput())
            {
                var s = input.Split('-');

                Cave a;
                if (!_caves.TryGetValue(s[0], out a))
                {
                    a = new Cave(s[0]);
                    _caves.Add(s[0], a);
                }
                Cave b;
                if (!_caves.TryGetValue(s[1], out b))
                {
                    b = new Cave(s[1]);
                    _caves.Add(s[1], b);
                }
                
                a.AddConnection(b);
                b.AddConnection(a);
            }

            
            var start = _caves["start"];
            FindEnd(start, new Dictionary<string, Cave>(), start.Name);
            
            Console.WriteLine($"sum = {_count}");
        }
        
        private void FindEnd(Cave c, Dictionary<string, Cave> visited, string path)
        {
            if (c.IsEnd)
            {
                Console.WriteLine(path);
                _count++;
                return;
            }
            
            if (!visited.ContainsKey(c.Name))
            {
                visited.Add(c.Name, c);
            }
            
            foreach (var kvp in c.ConnectedCaves)
            {
                var v = visited.ToDictionary(d => d.Key, d => d.Value);
                if (v.TryGetValue(kvp.Key, out var cc))
                {
                    if(!cc.IsBig) continue;
                }
                FindEnd(kvp.Value, v,path + "," + kvp.Value.Name);
            }
        }
    }
}