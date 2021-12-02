using System;
using Shouldly;
using Xunit;

namespace Aoc2021.Puzzle2.Tests;

public class AnswerCalculatorTests
{
    private const string ExampleData = @"
forward 5
down 5
forward 8
up 3
down 8
forward 2
";

    [Fact]
    public void ShouldFindCorrectAnswerForExample1()
    {
        var result = new AnswerCalculator().Calculate1(ExampleData.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
        result.ShouldBe(150);
    }

    [Fact]
    public void ShouldFindCorrectAnswerForExample2()
    {
        var result = new AnswerCalculator().Calculate2(ExampleData.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries));
        result.ShouldBe(900);
    }
}