using System;
using System.Collections.Generic;
using System.Text;

namespace AOC2021._16
{
    public class Day16A : Day
    {
        private Dictionary<char, string> _HexToBinary;
        public override void Run()
        {
            var input = GetInputAsString().Replace("\r", "").Replace("\n", "");
            //var input = "D2FE28";
            //var input = "38006F45291200";
            //var input = "EE00D40C823060";
            //var input = "8A004A801A8002F478";
            //var input = "620080001611562C8802118E34";
            //var input = "C0015000016115A2E0802F182340";
            //var input = "A0016C880162017C3686B18A3D4780";

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
            
            Console.WriteLine($"sum = {SumVersion(p)}");
            
        }

        private int SumVersion(Package p)
        {
            var sum = BitsToInt(p.Version);
            foreach (var package in p.Packages)
            {
                sum += SumVersion(package);
            }

            return sum;
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
    }
}
