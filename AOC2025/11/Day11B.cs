using AOC.Common;

namespace AOC2025._11;

public class Day11B : Day
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

        var svrToFft = devices["svr"].CalculateNumberOfPaths("fft");
        foreach (var device in devices.Values)
        {
            device.ResetPath();
        }
        
        var svrToDac = devices["svr"].CalculateNumberOfPaths("dac");
        foreach (var device in devices.Values)
        {
            device.ResetPath();
        }
        
        var fftToDac = devices["fft"].CalculateNumberOfPaths("dac");
        foreach (var device in devices.Values)
        {
            device.ResetPath();
        }
        
        var dacToFft = devices["dac"].CalculateNumberOfPaths("fft");
        foreach (var device in devices.Values)
        {
            device.ResetPath();
        }
        
        var fftToOut = devices["fft"].CalculateNumberOfPaths("out");
        foreach (var device in devices.Values)
        {
            device.ResetPath();
        }
        
        var dacToOut = devices["dac"].CalculateNumberOfPaths("out");
        
        var svrToFftToDacToOut = svrToFft * fftToDac * dacToOut;
        var svrToDacToFftToOut = svrToDac * dacToFft * fftToOut;
        
        return svrToFftToDacToOut + svrToDacToFftToOut;
    }

    private class Device
    {
        public string Name { get; set; }
        public List<Device> Output { get; set; } = [];

        public long NumberOfPathsToOut { get; set; } = -1;

        public void ResetPath()
        {
            NumberOfPathsToOut = -1;
        }

        public long CalculateNumberOfPaths(string goal)
        {
            if (Name == goal) return 1;
            if (NumberOfPathsToOut != -1) return NumberOfPathsToOut;

            NumberOfPathsToOut = 0;
            foreach (var output in Output)
            {
                NumberOfPathsToOut += output.CalculateNumberOfPaths(goal);
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