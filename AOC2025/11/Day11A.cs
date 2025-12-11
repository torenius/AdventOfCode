using AOC.Common;

namespace AOC2025._11;

public class Day11A : Day
{
    protected override object Run()
    {
        var input = GetInputAsStringArray()
            .Select(x => new RawDevice(x))
            .ToList();

        var devices = input
            .Select(x => new Device{Name = x.Name})
            .ToDictionary(x => x.Name, x => x);
        
        devices.Add("out", new Device{Name = "out"});
        
        foreach (var raw in input)
        {
            var device = devices[raw.Name];
            foreach (var output in raw.Output)
            {
                var outputDevice = devices[output];
                device.Output.Add(outputDevice);
            }
        }

        var you = devices["you"];

        return you.CalculateNumberOfPaths();
    }

    private class Device
    {
        public string Name { get; set; }
        public List<Device> Output { get; set; } = [];

        public int NumberOfPathsToOut { get; set; } = -1;

        public int CalculateNumberOfPaths()
        {
            if (Name == "out") return 1;
            if (NumberOfPathsToOut != -1) return NumberOfPathsToOut;

            NumberOfPathsToOut = 0;
            foreach (var output in Output)
            {
                NumberOfPathsToOut += output.CalculateNumberOfPaths();
            }
    
            return NumberOfPathsToOut;
        }
    }

    private class RawDevice
    {
        public RawDevice(string input)
        {
            var data =  input.Split(": ");
            Name = data[0];
            Output = data[1].Split(' ').ToList();
        }
        public string Name { get; set; }
        public List<string> Output { get; set; }
    }
}