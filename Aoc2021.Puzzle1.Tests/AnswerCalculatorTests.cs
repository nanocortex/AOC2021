using Shouldly;
using Xunit;

namespace Aoc2021.Puzzle1.Tests;

public class AnswerCalculatorTests
{
    private static readonly int[] ExampleData =
    {
        199,
        200,
        208,
        210,
        200,
        207,
        240,
        269,
        260,
        263
    };

    private static readonly int[] ExampleData2 =
    {
        607,
        618,
        618,
        617,
        647,
        716,
        769,
        792,
    };

    [Fact]
    public void ShouldFindCorrectAnswerForExample1()
    {
        var result = new AnswerCalculator().Calculate1(ExampleData);
        result.ShouldBe(7);
    }

    [Fact]
    public void ShouldFindCorrectAnswerForExample2()
    {
        var result = new AnswerCalculator().Calculate2(ExampleData2);
        result.ShouldBe(5);
    }
}