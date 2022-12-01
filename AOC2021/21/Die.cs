namespace AOC2021._21
{
    public class Die
    {
        public Die()
        {
            RolledCount = 0;
            _lastRoll = 0;
        }
        public int RolledCount { get; private set; }

        private int _lastRoll;
        public int Roll()
        {
            if (_lastRoll == 100)
            {
                _lastRoll = 0;
            }

            RolledCount++;
            
            return ++_lastRoll;
        }

        public int RollThreeTimes()
        {
            var sum = Roll();
            sum += Roll();
            sum += Roll();

            return sum;
        }
    }
}