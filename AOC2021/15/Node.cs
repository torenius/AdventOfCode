using System.Text;

namespace AOC2021._15
{
    public class Node
    {
        public Node(int y, int x)
        {
            Y = y;
            X = x;
        }
        public int X { get; set; }
        public int Y { get; set; }
        public Node Parent { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public int F { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Y = ").Append(Y);
            sb.Append(" X = ").Append(X);
            sb.Append(" G = ").Append(G);
            sb.Append(" H = ").Append(H);
            sb.Append(" F = ").Append(F);

            return sb.ToString();
        }
    }
}
