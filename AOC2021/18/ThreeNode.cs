namespace AOC2021._18
{
    public class ThreeNode
    {
        public ThreeNode Parent { get; set; }
        public ThreeNode Left { get; set; }
        public ThreeNode Right { get; set; }
        public int Value { get; set; }

        public bool IsLeaf()
        {
            return Left == null && Right == null;
        }

        public bool IsPair()
        {
            if (Left != null && Left.IsLeaf())
            {
                if (Right != null && Right.IsLeaf())
                {
                    return true;
                }
            }

            return false;
        }

        public override string ToString()
        {
            if (IsLeaf()) return Value.ToString();

            if (Left != null && Right == null) return "[" + Left + "," + Value + "]";

            if (Left == null && Right != null) return "[" + Value + "," + Right + "]";

            return "[" + Left + "," + Right + "]";
        }

        public int Magnitude()
        {
            if (IsLeaf()) return Value;

            var m = 0;
            if (Left != null)
            {
                m = 3 * Left.Magnitude();
            }

            if (Right != null)
            {
                m += 2 * Right.Magnitude();
            }

            return m;
        }
    }
}