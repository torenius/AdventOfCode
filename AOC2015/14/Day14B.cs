namespace AOC2015._14;

public class Day14B : Day
{
    protected override string Run()
    {
        const int raceDuration = 2503;
        var input = GetInputAsStringArray();
        var reindeers = input.Select(x => new Reindeer(x)).ToList();

        for (var i = 0; i < raceDuration; i++)
        {
            foreach (var reindeer in reindeers)
            {
                reindeer.Increment();
                //Console.WriteLine($"{i} {reindeer.Name} {reindeer.Distance}");
            }

            var maxDistance = reindeers.Max(x => x.Distance);
            foreach (var reindeer in reindeers.Where(x => x.Distance == maxDistance))
            {
                reindeer.Points++;
            }
        }

        return "" + reindeers.Max(x => x.Points);
    }

    private class Reindeer
    {
        public Reindeer(string input)
        {
            var temp = input.Split(" ");
            Name = temp[0];
            Speed = temp[3].ToInt();
            Duration = temp[6].ToInt();
            Rest = temp[13].ToInt();
        }

        public string Name { get; set; }
        public int Speed { get; set; }
        public int Duration { get; set; }
        public int Rest { get; set; }

        public int Points { get; set; }
        public int Distance { get; set; }
        private int Counter { get; set; }
        private State State { get; set; }

        public void Increment()
        {
            Counter++;
            if (State == State.NotStarted)
            {
                Distance += Speed;
                State = State.Flying;
            }
            else if (State == State.Flying)
            {
                Distance += Speed;
                if (Counter == Duration)
                {
                    State = State.Resting;
                    Counter = 0;
                }
            }
            else if (State == State.Resting)
            {
                if (Counter == Rest)
                {
                    State = State.Flying;
                    Counter = 0;
                }
            }
        }
    }

    private enum State
    {
        NotStarted,
        Flying,
        Resting
    }
}