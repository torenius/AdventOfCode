using System.Drawing;
using System.Text;

namespace AOC2022._23;

public class Day23B : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();

        var elves = new List<Elf>();
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] == '#')
                {
                    elves.Add(new Elf(x, y));
                }
            }
        }

        //Print(elves);
        var round = 0;
        do
        {
            round++;
            if (round % 50 == 0)
            {
                Console.WriteLine(round);
                PrintElapsedTime();
            }
            // Console.WriteLine(round);
            // PrintElapsedTime();
            // Console.WriteLine("CheckDirections");
            foreach (var elf in elves)
            {
                elf.CheckDirections(elves);
            }

            var diction = new[] {ProposalEnum.North, ProposalEnum.South, ProposalEnum.West, ProposalEnum.East};
            switch (round % 4)
            {
                case 0:
                    diction = new[] {ProposalEnum.East, ProposalEnum.North, ProposalEnum.South, ProposalEnum.West};
                    break;
                case 2:
                    diction = new[] {ProposalEnum.South, ProposalEnum.West, ProposalEnum.East, ProposalEnum.North};
                    break;
                case 3:
                    diction = new[] {ProposalEnum.West, ProposalEnum.East, ProposalEnum.North, ProposalEnum.South};
                    break;
            }

            // PrintElapsedTime();
            // Console.WriteLine("CalculateProposal");
            foreach (var elf in elves)
            {
                elf.CalculateProposal(diction);
            }

            var collisions = elves.GroupBy(e => e.Proposal)
                .Select(group => new
                {
                    SamePoint = group.Key,
                    Count = group.Count()
                })
                .Where(e => e.Count > 1)
                .ToList();
            
            foreach (var collision in collisions)
            {
                foreach (var elf in elves.Where(e => e.Proposal.Equals(collision.SamePoint)))
                {
                    elf.SetProposalToLocation();
                }
            }

            // PrintElapsedTime();
            // Console.WriteLine("MoveToProposal");
            foreach (var elf in elves)
            {
                elf.MoveToProposal();
            }

            //Print(elves);
            // PrintElapsedTime();
            // Console.WriteLine("Any");
        } while (elves.Any(e => e.DidMove));

        return "" + round;
    }

    private class Elf
    {
        public Elf(int x, int y)
        {
            Location = new Point(x, y);
            Proposal = new Point(x, y);
        }
        public Point Location { get; set; }
        public Point Proposal { get; set; }
        public bool DidMove { get; set; }

        public void SetProposalToLocation()
        {
            if (!Proposal.Equals(Location))
            {
                Proposal = new Point(Location.X, Location.Y);
            }
        }

        public void MoveToProposal()
        {
            if (!Proposal.Equals(Location))
            {
                Location = new Point(Proposal.X, Proposal.Y);
                DidMove = true;
            }
            else
            {
                DidMove = false;
            }
        }

        public bool NW { get; set; }
        public bool N { get; set; }
        public bool NE { get; set; }
        public bool W { get; set; }
        public bool E { get; set; }
        public bool SW { get; set; }
        public bool S { get; set; }
        public bool SE { get; set; }

        private void Reset()
        {
            NW = false;
            N  = false;
            NE = false;
            W  = false;
            E  = false;
            SW = false;
            S  = false;
            SE = false;
        }

        public void CheckDirections(List<Elf> elves)
        {
            Reset();

            var xLocations = new[] {Location.X - 1, Location.X, Location.X + 1};
            var yLocations = new[] {Location.Y - 1, Location.Y, Location.Y + 1};
            var around = elves.Where(e => xLocations.Contains(e.Location.X) && yLocations.Contains(e.Location.Y))
                .ToList();

            NW = IsOccupied(around, Location.X - 1, Location.Y - 1);
            N  = IsOccupied(around, Location.X, Location.Y - 1);
            NE = IsOccupied(around, Location.X + 1, Location.Y - 1);
            W  = IsOccupied(around, Location.X - 1, Location.Y);
            E  = IsOccupied(around, Location.X + 1, Location.Y);
            SW = IsOccupied(around, Location.X - 1, Location.Y + 1);
            S  = IsOccupied(around, Location.X, Location.Y + 1);
            SE = IsOccupied(around, Location.X + 1, Location.Y + 1);
        }

        private static bool IsOccupied(List<Elf> elves, int x, int y)
        {
            return elves.Any(e => e.Location.X == x && e.Location.Y == y);
        }

        public void CalculateProposal(ProposalEnum[] direction)
        {
            SetProposalToLocation();
            if (!NW && !N && !NE && !W && !E && !SW && !S && !SE)
            {
                return;
            }

            for(var i = 0; i < direction.Length; i++)
            {
                switch (direction[i])
                {
                    case ProposalEnum.North:
                        if (!NW && !N && !NE)
                        {
                            Proposal = new Point(Location.X, Location.Y - 1);
                            return;
                        }
                        break;
                    case ProposalEnum.South:
                        if (!SW && !S && !SE)
                        {
                            Proposal = new Point(Location.X, Location.Y + 1);
                            return;
                        }
                        break;
                    case ProposalEnum.West:
                        if (!NW && !W && !SW)
                        {
                            Proposal = new Point(Location.X - 1, Location.Y);
                            return;
                        }
                        break;
                    case ProposalEnum.East:
                        if (!NE && !E && !SE)
                        {
                            Proposal = new Point(Location.X + 1, Location.Y);
                            return;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
            }
        }
    }

    private enum ProposalEnum
    {
        North,
        South,
        West,
        East
    }

    private static void Print(List<Elf> elves)
    {
        var minY = elves.Min(e => e.Location.Y);
        var maxY = elves.Max(e => e.Location.Y);
        var minX = elves.Min(e => e.Location.X);
        var maxX = elves.Max(e => e.Location.X);

        var sb = new StringBuilder();
        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                if (elves.Any(e => e.Location.X == x && e.Location.Y == y))
                {
                    sb.Append('#');
                }
                else
                {
                    sb.Append('.');
                }
            }

            sb.AppendLine();
        }
        
        Console.WriteLine(sb.ToString());
    }
}