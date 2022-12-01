using System;

namespace AOC2021._21
{
    public class Player
    {
        private int _startingPosition;
        public int StartingPosition
        {
            get => _startingPosition;
            set
            {
                _startingPosition = value;
                CurrentPosition = value;
            }
        }

        public int CurrentPosition { get; set; }
        public int Score { get; set; }

        public void Move(int steps)
        {
            CurrentPosition += steps;
            if (CurrentPosition > 10)
            {
                var temp = CurrentPosition % 10;
                if (temp == 0)
                {
                    CurrentPosition = 10;
                }
                else
                {
                    CurrentPosition = temp;    
                }
            }

            Score += CurrentPosition;
        }
    }
}