using System;
using System.Drawing;

namespace AOC2021._17
{
    public class Day17A : Day
    {
        private int _minX;
        private int _maxX;
        private int _minY;
        private int _maxY;
        
        public override void Run()
        {
            var input = GetInputAsString().Replace("\r", "").Replace("\n", "");
            //var input = "target area: x=20..30, y=-10..-5";
            input = input.Replace("target area: x=", "");
            var s = input.Split(", y=");
            var xRange = s[0].Split("..");
            var yRange = s[1].Split("..");
            _minX = int.Parse(xRange[0]);
            _maxX = int.Parse(xRange[1]);
            _minY = int.Parse(yRange[0]);
            _maxY = int.Parse(yRange[1]);
            
            var maxXvelocity = Math.Max(Math.Abs(_minX), Math.Abs(_maxX));
            var maxYvelocity = Math.Max(Math.Abs(_minY), Math.Abs(_maxY));

            var maxY = int.MinValue;
            var probe = new Point(0, 0);
            for (var x = -maxXvelocity; x <= maxXvelocity; x++)
            {
                for (var y = -maxYvelocity; y <= maxYvelocity; y++)
                {
                    if (x == 0 && y == 0) break;
                    
                    probe.X = 0;
                    probe.Y = 0;
                    var maxProbeY = 0;
                    var xVelocity = x;
                    var yVelocity = y;
                    while (!HaveGoneToFar(probe))
                    {
                        probe.X += xVelocity;
                        probe.Y += yVelocity;
                        if (xVelocity > 0) xVelocity--;
                        if (xVelocity < 0) xVelocity++;
                        yVelocity--;

                        if (probe.Y > maxProbeY)
                        {
                            maxProbeY = probe.Y;
                        }

                        if (IsPointInside(probe))
                        {
                            if (maxProbeY > maxY)
                            {
                                maxY = maxProbeY;
                            }
                            break;
                        }
                    }
                }
            }
            
            Console.WriteLine($"MaxY = {maxY}");
        }

        private bool IsPointInside(Point p)
        {
            if (p.X < _minX || p.X > _maxX) return false;
            return p.Y >= _minY && p.Y <= _maxY;
        }

        private bool HaveGoneToFar(Point p)
        {
            return p.X > _maxX || p.Y < _minY;
        }
    }
}
