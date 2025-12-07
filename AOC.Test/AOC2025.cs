using AOC2025._01;
using AOC2025._02;
using AOC2025._03;
using AOC2025._04;
using AOC2025._05;
using AOC2025._06;
using AOC2025._07;

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
 
    [Fact]
    public void Day03A() => Assert.Equal(17376, new Day03A().Test());
    
    [Fact]
    public void Day03B() => Assert.Equal(172119830406258, new Day03B().Test());
    
    [Fact]
    public void Day04A() => Assert.Equal(1486, new Day04A().Test());
    
    [Fact]
    public void Day04B() => Assert.Equal(9024, new Day04B().Test());
    
    [Fact]
    public void Day05A() => Assert.Equal(758, (int)new Day05A().Test());
    
    [Fact]
    public void Day05B() => Assert.Equal(343143696885053, new Day05B().Test());
    
    [Fact]
    public void Day06A() => Assert.Equal(4693419406682, new Day06A().Test());
    
    [Fact]
    public void Day06B() => Assert.Equal(9029931401920, new Day06B().Test());
    
    [Fact]
    public void Day07A() => Assert.Equal(1581, new Day07A().Test());
    
    [Fact]
    public void Day07B() => Assert.Equal(73007003089792, new Day07B().Test());
        
}
