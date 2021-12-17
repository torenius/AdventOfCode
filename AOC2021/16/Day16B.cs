using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOC2021._16
{
    public class Day16B : Day
    {
        private Dictionary<char, string> _HexToBinary;
        public override void Run()
        {
            var input = GetInputAsString().Replace("\r", "").Replace("\n", ""); // 184487454837
            //var input = "C200B40A82"; // 3
            //var input = "04005AC33890"; // 54
            //var input = "880086C3E88112"; // 7
            //var input = "CE00C43D881120"; // 9
            //var input = "D8005AC2A8F0"; // 1
            //var input = "F600BC2D8F"; // 0
            //var input = "9C005AC2F8F0"; // 0
            //var input = "9C0141080250320F1802104A08"; // 1

            _HexToBinary = new Dictionary<char, string>()
            {
                {'0', "0000"},
                {'1', "0001"},
                {'2', "0010"},
                {'3', "0011"},
                {'4', "0100"},
                {'5', "0101"},
                {'6', "0110"},
                {'7', "0111"},
                {'8', "1000"},
                {'9', "1001"},
                {'A', "1010"},
                {'B', "1011"},
                {'C', "1100"},
                {'D', "1101"},
                {'E', "1110"},
                {'F', "1111"}
            };

            var sb = new StringBuilder(input.Length);
            foreach (var c in input)
            {
                sb.Append(_HexToBinary[c]);
            }
            
            var index = 0;
            var p = GetPackage(sb.ToString(), ref index);
            
            Console.WriteLine($"Result = {Result(p)}");
            
        }

        private long Result(Package p)
        {
            long result = 0;
            long p1 = 0;
            long p2 = 0;
            switch (p.TypeId)
            {
                case "000": // 0
                    result += p.Packages.Sum(Result);
                    break;
                case "001": // 1
                    result = 1;
                    foreach (var package in p.Packages)
                    {
                        result *= Result(package);
                    }
                    break;
                case "010": // 2
                    result += p.Packages.Min(Result);
                    break;
                case "011": // 3
                    result += p.Packages.Max(Result);
                    break;
                case "100": // 4
                    var bits = "";
                    foreach (var payload in p.Payload)
                    {
                        bits += payload[1..5];
                    }
                    return BitsToLong(bits);
                case "101": // 5
                    p1 = Result(p.Packages[0]);
                    p2 = Result(p.Packages[1]);
                    if (p1 > p2)
                    {
                        result = 1;
                    }
                    break;
                case "110": // 6
                    p1 = Result(p.Packages[0]);
                    p2 = Result(p.Packages[1]);
                    if (p1 < p2)
                    {
                        result = 1;
                    }
                    break;
                case "111": // 7
                    p1 = Result(p.Packages[0]);
                    p2 = Result(p.Packages[1]);
                    if (p1 == p2)
                    {
                        result = 1;
                    }
                    break;
            }

            return result;
        }

        private Package GetPackage(string input, ref int index)
        {
            var p = new Package();
            p.Version = input[index..(index + 3)];
            index += 3;
            p.TypeId = input[index..(index + 3)];
            index += 3;
            
            // Literal value
            if (p.TypeId.Equals("100")) // 4
            {
                p.Payload = GetPayload(input, ref index);
                return p;
            }
            
            p.LengthTypeId = input[index++].ToString();
            var subPackageBitLength = p.LengthTypeId == "0" ? 15 : 11;
            p.SubPackagesLength = input[index..(index + subPackageBitLength)];
            var numberOfBits = BitsToInt(p.SubPackagesLength);
            index += subPackageBitLength;
            
            if (p.LengthTypeId == "0")
            {
                var length = 0;
                while (length < numberOfBits)
                {
                    var package = GetPackage(input, ref index);
                    package.Parent = p;
                    length += package.GetLength();
                    p.Packages.Add(package);
                }
            }
            else
            {
                var count = 0;
                while (count < numberOfBits)
                {
                    var package = GetPackage(input, ref index);
                    package.Parent = p;
                    p.Packages.Add(package);
                    count++;
                }
            }

            return p;
        }
        
        private List<string> GetPayload(string input, ref int index)
        {
            var i = index - 5;
            var stringList = new List<string>();
            do
            {
                i += 5;
                stringList.Add(input[i..(i + 5)]);

            } while (input[i] != '0');

            index = i + 5;
            return stringList;
        }
        
        private int BitsToInt(string bits)
        {
            return Convert.ToInt32(bits, 2);
        }
        
        private long BitsToLong(string bits)
        {
            return Convert.ToInt64(bits, 2);
        }
    }
}
