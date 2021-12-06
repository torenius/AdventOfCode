using System;

namespace AOC2021._06
{
    public class Day06A : Day
    {
        public override void Run()
        {
            var day = new int[9];
            foreach (var n in GetInputIntArray())
            {
                day[n]++;
            }

            for (var i = 0; i < 80; i++)
            {
                SimulateDay(day);
            }

            var sum = 0;
            for (var i = 0; i < day.Length; i++)
            {
                sum += day[i];
            }
            
            Console.WriteLine($"Sum = {sum}");
        }

        private void SimulateDay(int[] day)
        {
            var newFish = day[0];
            for (var i = 0; i < day.Length - 1; i++)
            {
                day[i] = day[i + 1];
            }

            day[6] += newFish;
            day[8] = newFish;
        }
    }
}
