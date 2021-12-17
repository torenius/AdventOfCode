using System.Collections.Generic;
using System.Text;

namespace AOC2021._16
{
    public class Package
    {
        public Package()
        {
            Payload = new List<string>();
            Packages = new List<Package>();
            LengthTypeId = "";
            SubPackagesLength = "";
        }

        public string Version { get; set; }
        public string TypeId { get; set; }
        public string LengthTypeId { get; set; }
        public string SubPackagesLength { get; set; }
        public List<string> Payload { get; set; }
        public List<Package> Packages { get; set; }
        public Package Parent { get; set; }

        public int GetLength()
        {
            return ToString().Length;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Version);
            sb.Append(TypeId);
            sb.Append(LengthTypeId);
            sb.Append(SubPackagesLength);
            sb.Append(string.Join("", Payload));
            foreach (var package in Packages)
            {
                sb.Append(package);
            }

            return sb.ToString();
        }
    }
}
