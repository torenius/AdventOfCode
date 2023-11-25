using System.Numerics;

namespace AOC2022._18;

public class Day18A : Day
{
    protected override string Run()
    {
        var input = GetInputAsStringArray();
        var cubes = input.Select(x => new Cube(x)).ToList();

        
        for (var a = 0; a < cubes.Count - 1; a++)
        {
            var cubeA = cubes[a];
            for (var b = a + 1; b < cubes.Count; b++)
            {
                var cubeB = cubes[b];
                cubeA.CompareCube(cubeB);
            }
        }
        
        var exposedCount = cubes.Sum(cube => cube.NumberOfExposedSides);

        return "" + exposedCount;
    }

    private class Cube
    {
        public Cube(string input)
        {
            var coordinates = input.Split(",");
            var x = coordinates[0].ToInt();
            var y = coordinates[1].ToInt();
            var z = coordinates[2].ToInt();

            FrontBottomLeft = new Vector3(x, y, z);
            FrontTopLeft = new Vector3(x, y + 1, z);
            FrontBottomRight = new Vector3(x + 1, y, z);
            FrontTopRight = new Vector3(x + 1, y + 1, z);

            BackBottomLeft = new Vector3(x, y, z + 1);
            BackTopLeft = new Vector3(x, y + 1, z + 1);
            BackBottomRight = new Vector3(x + 1, y, z + 1);
            BackTopRight = new Vector3(x + 1, y + 1, z + 1);
        }

        public Vector3 FrontBottomLeft { get; set; }
        public Vector3 FrontTopLeft { get; set; }
        public Vector3 FrontBottomRight { get; set; }
        public Vector3 FrontTopRight { get; set; }
        
        public Vector3 BackBottomLeft { get; set; }
        public Vector3 BackTopLeft { get; set; }
        public Vector3 BackBottomRight { get; set; }
        public Vector3 BackTopRight { get; set; }

        public Cube? FrontConnectedWith { get; set; }
        public Cube? BackConnectedWith { get; set; }
        public Cube? LeftConnectedWith { get; set; }
        public Cube? RightConnectedWith { get; set; }
        public Cube? TopConnectedWith { get; set; }
        public Cube? BottomConnectedWith { get; set; }

        public int NumberOfExposedSides =>
            (FrontConnectedWith is null ? 1 : 0) +
            (BackConnectedWith is null ? 1 : 0) +
            (LeftConnectedWith is null ? 1 : 0) +
            (RightConnectedWith is null ? 1 : 0) +
            (TopConnectedWith is null ? 1 : 0) +
            (BottomConnectedWith is null ? 1 : 0);

        public void CompareCube(Cube other)
        {
            // This Top, Other Bottom
            if (TopConnectedWith is null && other.BottomConnectedWith is null
                                && FrontTopLeft.Equals(other.FrontBottomLeft)
                                && FrontTopRight.Equals(other.FrontBottomRight)
                                && BackTopLeft.Equals(other.BackBottomLeft)
                                && BackTopRight.Equals(other.BackBottomRight))
            {
                TopConnectedWith = other;
                other.BottomConnectedWith = this;
                return;
            }
            
            // This bottom, Other Top
            if (BottomConnectedWith is null && other.TopConnectedWith is null
                                            && other.FrontTopLeft.Equals(FrontBottomLeft)
                                            && other.FrontTopRight.Equals(FrontBottomRight)
                                            && other.BackTopLeft.Equals(BackBottomLeft)
                                            && other.BackTopRight.Equals(BackBottomRight))
            {
                BottomConnectedWith = other;
                other.TopConnectedWith = this;
                return;
            }
            
            // This Left, Other Right
            if (LeftConnectedWith is null && other.RightConnectedWith is null
                                          && BackTopLeft.Equals(other.BackTopRight)
                                          && FrontTopLeft.Equals(other.FrontTopRight)
                                          && BackBottomLeft.Equals(other.BackBottomRight)
                                          && FrontBottomLeft.Equals(other.FrontBottomRight))
            {
                LeftConnectedWith = other;
                other.RightConnectedWith = this;
                return;
            }
            
            // This Right, Other Left
            if (RightConnectedWith is null && other.LeftConnectedWith is null
                                           && other.BackTopLeft.Equals(BackTopRight)
                                           && other.FrontTopLeft.Equals(FrontTopRight)
                                           && other.BackBottomLeft.Equals(BackBottomRight)
                                           && other.FrontBottomLeft.Equals(FrontBottomRight))
            {
                RightConnectedWith = other;
                other.LeftConnectedWith = this;
                return;
            }
            
            // This Back, Other Front
            if (BackConnectedWith is null && other.FrontConnectedWith is null
                                          && BackTopLeft.Equals(other.FrontTopLeft)
                                          && BackTopRight.Equals(other.FrontTopRight)
                                          && BackBottomLeft.Equals(other.FrontBottomLeft)
                                          && BackBottomRight.Equals(other.FrontBottomRight))
            {
                BackConnectedWith = other;
                other.FrontConnectedWith = this;
                return;
            }
            
            // This Front, Other Back
            if (FrontConnectedWith is null && other.BackConnectedWith is null
                                           && other.BackTopLeft.Equals(FrontTopLeft)
                                           && other.BackTopRight.Equals(FrontTopRight)
                                           && other.BackBottomLeft.Equals(FrontBottomLeft)
                                           && other.BackBottomRight.Equals(FrontBottomRight))
            {
                FrontConnectedWith = other;
                other.BackConnectedWith = this;
            }
        }
    }
}