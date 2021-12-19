using System;
using System.Text.RegularExpressions;

namespace AOC2021._18
{
    public class Day18A : Day
    {
        public override void Run()
        {
            var input = GetInput();
            
            var a = Convert(null, input[0]);
            for (var i = 1; i < input.Length; i++)
            {
                var b = Convert(null, input[i]);
                a = Add(a, b);
                Reduce(a);
            }
            Console.WriteLine(a);
            Console.WriteLine($"Magnitude = {a.Magnitude()}");
        }

        private void Reduce(ThreeNode tn)
        {
            if (Explode(tn, 0))
            {
                Reduce(tn);
            }

            if (Split(tn))
            {
                Reduce(tn);
            }
        }
        
        private bool Explode(ThreeNode tn, int depth)
        {
            var exploded = false;
            if (depth == 4)
            {
                if (tn.IsPair())
                {
                    var a = tn.Left.Value;
                    var b = tn.Right.Value;

                    tn.Left = null;
                    tn.Right = null;
                    tn.Value = 0;

                    AddValueToLeft(tn, a);
                    AddValueToRight(tn, b);

                    return true;
                }
            }
            else
            {
                if (tn.Left != null)
                {
                    exploded = Explode(tn.Left, depth + 1);
                }

                if (!exploded && tn.Right != null)
                {
                    exploded = Explode(tn.Right, depth + 1);
                }
            }

            return exploded;
        }
        
        private bool AddValueToLeft(ThreeNode parent, int value)
        {
            
            var tn = parent.Parent; // 3

            if (tn.Left != null && tn.Left.IsLeaf() && tn.Left != parent)
            {
                tn.Left.Value += value;
                return true;
            }

            var from = parent;

            while (tn != null)
            {
                if (tn?.Left != from)
                {
                    if (AddValueToLeftChild(tn.Left, value)) return true;    
                }

                from = tn;
                tn = tn.Parent;
            }

            return false;
        }
        
        private bool AddValueToLeftChild(ThreeNode tn, int value)
        {
            if (tn == null) return false;

            if (tn.IsLeaf())
            {
                tn.Value += value;
                return true;
            }

            return AddValueToLeftChild(tn.Right, value) || AddValueToLeftChild(tn.Left, value);
        }

        private bool AddValueToRight(ThreeNode parent, int value)
        {
            
            var tn = parent.Parent; // 3

            if (tn.Right != null && tn.Right.IsLeaf() && tn.Right != parent)
            {
                tn.Right.Value += value;
                return true;
            }

            var from = parent;

            while (tn != null)
            {
                if (tn?.Right != from)
                {
                    if (AddValueToRightChild(tn.Right, value)) return true;    
                }

                from = tn;
                tn = tn.Parent;
            }

            return false;
        }
        
        private bool AddValueToRightChild(ThreeNode tn, int value)
        {
            if (tn == null) return false;

            if (tn.IsLeaf())
            {
                tn.Value += value;
                return true;
            }

            return AddValueToRightChild(tn.Left, value) || AddValueToRightChild(tn.Right, value);
        }

        private ThreeNode Convert(ThreeNode parent, string s)
        {
            var tn = new ThreeNode
            {
                Parent = parent
            };

            if (int.TryParse(s, out var n))
            {
                tn.Value = n;
                return tn;
            }

            s = s.Substring(1, s.Length - 2);
            var count = 1;
            var a = "";
            var b = "";

            var last = s.Split(",");

            if (last.Length == 2)
            {
                a = last[0];
                b = last[1];
            }
            if (Regex.IsMatch(s, "^[0-9]"))
            {
                var firstComma = s.IndexOf(",");
                a = s[..firstComma];
                b = s[(firstComma + 1)..];
            }
            else
            {
                for (var i = s.IndexOf("[") + 1; i < s.Length; i++)
                {
                    if (count == 0)
                    {
                        a = s[..i];
                        b = s[(i + 1)..];
                        break;
                    }

                    if (s[i] == '[') count++;
                    if (s[i] == ']') count--;
                }
            }

            if(a != "") tn.Left = Convert(tn, a);
            if(b != "") tn.Right = Convert(tn, b);
            
            return tn;
        }

        private bool Split(ThreeNode tn)
        {
            if (tn.IsLeaf())
            {
                if (tn.Value >= 10)
                {
                    var a = (int)Math.Floor(tn.Value / 2.0);
                    var b = (int)Math.Ceiling(tn.Value / 2.0);

                    tn.Left = new ThreeNode
                    {
                        Parent = tn,
                        Value = a
                    };
                    
                    tn.Right = new ThreeNode
                    {
                        Parent = tn,
                        Value = b
                    };

                    return true;
                }

                return false;
            }
            
            if (tn.Left != null)
            {
                if (Split(tn.Left)) return true;
            }
            
            if (tn.Right != null)
            {
                if (Split(tn.Right)) return true;
            }
            
            return false;
        }

        private ThreeNode Add(ThreeNode a, ThreeNode b)
        {
            var tn = new ThreeNode
            {
                Left = a,
                Right = b
            };

            tn.Left.Parent = tn;
            tn.Right.Parent = tn;

            return tn;
        }
    }
}