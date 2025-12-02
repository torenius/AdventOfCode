using AOC2025._01;
using AOC2025._02;

namespace AOCTest;

public class AOC2025
{
    [Fact]
    public void Day01A() => Assert.Equal(1165, new Day01A().Test());
    
    [Fact]
    public void Day01B() => Assert.Equal(6496, new Day01B().Test());
    
    [Fact]
    public void Day02A() => Assert.Equal(21898734247, new Day02A().Test());
    
    [Fact]
    public void Day02B() => Assert.Equal(28915664389, new Day02B().Test());
        
}
