using System.Collections.Generic;

namespace AOC2021._12
{
    public class Cave
    {
        public Cave(string name)
        {
            Name = name;
            if (name.Equals("start"))
            {
                IsStart = true;
            }

            if (name.Equals("end"))
            {
                IsEnd = true;
            }

            if (name.Equals(name.ToUpper()))
            {
                IsBig = true;
            }

            ConnectedCaves = new Dictionary<string, Cave>();
        }

        public string Name { get; private set; }
        public bool IsStart { get; private set; }
        public bool IsEnd { get; private set; }
        public bool IsBig { get; private set; }
        public Dictionary<string, Cave> ConnectedCaves { get; private set; }

        public void AddConnection(Cave c)
        {
            if (!ConnectedCaves.ContainsKey(c.Name))
            {
                ConnectedCaves.Add(c.Name, c);
            }
        }
    }
}