using AOC2024._01;
using AOC2024._02;
using AOC2024._03;
using AOC2024._04;
using AOC2024._05;
using AOC2024._06;
using AOC2024._07;
using AOC2024._08;
using AOC2024._09;
using AOC2024._10;
using AOC2024._11;
using AOC2024._12;
using AOC2024._13;
using AOC2024._14;
using AOC2024._15;
using AOC2024._16;
using AOC2024._17;
using AOC2024._18;
using AOC2024._19;
using AOC2024._20;
using AOC2024._21;
using AOC2024._22;
using AOC2024._23;
using AOC2024._24;
using AOC2024._25;

namespace AOCTest;

public class Aoc2024
{
    [Fact]
    public void Day01A() => Assert.Equal(1879048, new Day01A().Test());
    
    [Fact]
    public void Day01B() => Assert.Equal(21024792, new Day01B().Test());
    
    [Fact]
    public void Day02A() => Assert.Equal(242, new Day02A().Test());
    
    [Fact]
    public void Day02B() => Assert.Equal(311, new Day02B().Test());
    
    [Fact]
    public void Day03A() => Assert.Equal(175700056, new Day03A().Test());
    
    [Fact]
    public void Day03B() => Assert.Equal(71668682, new Day03B().Test());
    
    [Fact]
    public void Day04A() => Assert.Equal(2496, new Day04A().Test());
    
    [Fact]
    public void Day04B() => Assert.Equal(1967, new Day04B().Test());
    
    [Fact]
    public void Day05A() => Assert.Equal(6267, new Day05A().Test());
    
    [Fact]
    public void Day05B() => Assert.Equal(5184, new Day05B().Test());
    
    [Fact]
    public void Day06A() => Assert.Equal(4973, new Day06A().Test());
    
    [Fact]
    public void Day06B() => Assert.Equal(1482, new Day06B().Test());
    
    [Fact]
    public void Day07A() => Assert.Equal(538191549061, new Day07A().Test());
    
    [Fact]
    public void Day07B() => Assert.Equal(34612812972206, new Day07B().Test());
    
    [Fact]
    public void Day08A() => Assert.Equal(390, new Day08A().Test());
    
    [Fact]
    public void Day08B() => Assert.Equal(1246, new Day08B().Test());
    
    [Fact]
    public void Day09A() => Assert.Equal(6283404590840, new Day09A().Test());
    
    [Fact]
    public void Day09B() => Assert.Equal(6304576012713, new Day09B().Test());
    
    [Fact]
    public void Day10A() => Assert.Equal(789, new Day10A().Test());
    
    [Fact]
    public void Day10B() => Assert.Equal(1735, new Day10B().Test());
    
    [Fact]
    public void Day11A() => Assert.Equal((long)199986, new Day11A().Test());
    
    [Fact]
    [Trait("Slow", "About 1 min")]
    public void Day11B() => Assert.Equal(236804088748754, new Day11B().Test());
    
    [Fact]
    public void Day12A() => Assert.Equal(1471452, new Day12A().Test());
    
    [Fact]
    public void Day12B() => Assert.Equal(863366, new Day12B().Test());
    
    [Fact]
    public void Day13A() => Assert.Equal(35082, new Day13A().Test());
    
    [Fact]
    public void Day13B() => Assert.Equal(82570698600470, new Day13B().Test());
    
    [Fact]
    public void Day14A() => Assert.Equal(231019008, new Day14A().Test());
    
    // [Fact]
    // [Trait("Print out result", "Christmas tree")]
    // public void Day14B() => Assert.Equal(8280, new Day14B().Test());
    
    [Fact]
    public void Day15A() => Assert.Equal(1499739, new Day15A().Test());
    
    [Fact]
    public void Day15B() => Assert.Equal(1522215, new Day15B().Test());
    
    [Fact]
    [Trait("Slow", "About 18 seconds")]
    public void Day16A() => Assert.Equal(109496, new Day16A().Test());
    
    [Fact]
    public void Day16B() => Assert.Equal(551, new Day16B().Test());
    
    [Fact]
    public void Day17A() => Assert.Equal("2,0,7,3,0,3,1,3,7", new Day17A().Test());
    
    // [Fact]
    // public void Day17B() => Assert.Equal("", new Day17B().Test());
    
    [Fact]
    public void Day18A() => Assert.Equal(304, new Day18A().Test());
    
    [Fact]
    public void Day18B() => Assert.Equal("50,28", new Day18B().Test());
    
    [Fact]
    public void Day19A() => Assert.Equal(324, new Day19A().Test());
    
    [Fact]
    public void Day19B() => Assert.Equal(575227823167869, new Day19B().Test());
    
    [Fact]
    public void Day20A() => Assert.Equal(1490, new Day20A().Test());
    
    [Fact]
    [Trait("Slow", "About 1.5 minutes")]
    public void Day20B() => Assert.Equal(1011325, new Day20B().Test());
    
    [Fact]
    public void Day21A() => Assert.Equal(278568, new Day21A().Test());
    
    [Fact]
    public void Day21B() => Assert.Equal(341460772681012, new Day21B().Test());
    
    [Fact]
    public void Day22A() => Assert.Equal(16999668565, new Day22A().Test());
    
    // [Fact]
    // [Trait("Super Slow", "About 8 minutes")]
    // public void Day22B() => Assert.Equal((long)1898, new Day22B().Test());
    
    [Fact]
    [Trait("Slow", "About 15 seconds")]
    public void Day23A() => Assert.Equal(1046, new Day23A().Test());
    
    [Fact]
    [Trait("Slow", "About 24 seconds")]
    public void Day23B() => Assert.Equal("de,id,ke,ls,po,sn,tf,tl,tm,uj,un,xw,yz", new Day23B().Test());
    
    [Fact]
    public void Day24A() => Assert.Equal(55114892239566, new Day24A().Test());
    
    // [Fact]
    // public void Day24B() => Assert.Equal(0, new Day24B().Test());
    
    [Fact]
    public void Day25() => Assert.Equal(3291, new Day25().Test());
}