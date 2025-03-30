using AOC.Common;
using AOC2016._01;
using AOC2016._02;
using AOC2016._03;
using AOC2016._04;
using AOC2016._05;
using AOC2016._06;
using AOC2016._07;
using AOC2016._08;

namespace AOCTest;

public class Aoc2016
{
    [Fact]
    public void Day01A() => Assert.Equal(332, new Day01A().Test());
    
    [Fact]
    public void Day01B() => Assert.Equal(166, new Day01B().Test());
    
    [Fact]
    public void Day02A() => Assert.Equal("19636", new Day02A().Test());
    
    [Fact]
    public void Day02B() => Assert.Equal("3CC43", new Day02B().Test());
    
    [Fact]
    public void Day03A() => Assert.Equal(869, new Day03A().Test());
    
    [Fact]
    public void Day03B() => Assert.Equal(1544, new Day03B().Test());
    
    [Fact]
    public void Day04A() => Assert.Equal(173787, new Day04A().Test());
    
    [Fact]
    public void Day04B() => Assert.Equal(548, new Day04B().Test());
    
    [Fact]
    public void Day05A() => Assert.Equal("4543c154", new Day05A().Test());
    
    [Fact]
    public void Day05B() => Assert.Equal("1050cbbd", new Day05B().Test());
    
    [Fact]
    public void Day06A() => Assert.Equal("qoclwvah", new Day06A().Test());
    
    [Fact]
    public void Day06B() => Assert.Equal("ryrgviuv", new Day06B().Test());
    
    [Fact]
    public void Day07A() => Assert.Equal(115, new Day07A().Test());
    
    [Fact]
    public void Day07B() => Assert.Equal(231, new Day07B().Test());
    
    [Fact]
    public void Day08A() => Assert.Equal(128, new Day08A().Test());
    
    [Fact] // EOARGPHYAO
    public void Day08B() => Assert.Equal("####..##...##..###...##..###..#..#.#...#.##...##..\n#....#..#.#..#.#..#.#..#.#..#.#..#.#...##..#.#..#.\n###..#..#.#..#.#..#.#....#..#.####..#.#.#..#.#..#.\n#....#..#.####.###..#.##.###..#..#...#..####.#..#.\n#....#..#.#..#.#.#..#..#.#....#..#...#..#..#.#..#.\n####..##..#..#.#..#..###.#....#..#...#..#..#..##..".ReplaceNewLines(""), ((string)new Day08B().Test()).ReplaceNewLines(""));
}