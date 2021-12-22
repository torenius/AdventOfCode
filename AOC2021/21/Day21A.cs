using System;

namespace AOC2021._21
{
    public class Day21A : Day
    {
        public override void Run()
        {
            var input = GetInput();
            var p1 = new Player
            {
                StartingPosition = int.Parse(input[0][28..])
            };
            var p2 = new Player
            {
                StartingPosition = int.Parse(input[1][28..])
            };

            var dice = new Die();
            while (true)
            {
                var roll = dice.RollThreeTimes();
                p1.Move(roll);
                
                if(p1.Score >= 1000) break;
                
                roll = dice.RollThreeTimes();
                p2.Move(roll);
                
                if(p2.Score >= 1000) break;
            }
            
            Console.WriteLine($"P1 = {p1.Score} P2 = {p2.Score} Dice = {dice.RolledCount}");
            var min = Math.Min(p1.Score, p2.Score);
            Console.WriteLine($"min = {min} min * dice = {min * dice.RolledCount}");
        }
    }
}