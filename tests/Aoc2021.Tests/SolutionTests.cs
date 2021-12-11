using System;
using System.Threading.Tasks;
using AoCHelper;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace Aoc2021.Tests;

public class SolutionTests
{
    [Theory]
    [InlineData(typeof(Day01), "1342", "1378")]
    [InlineData(typeof(Day02), "2102357", "2101031224")]
    [InlineData(typeof(Day03), "841526", "4790390")]
    [InlineData(typeof(Day04), "60368", "17435")]
    [InlineData(typeof(Day05), "6397", "22335")]
    [InlineData(typeof(Day06), "380243", "1708791884591")]
    [InlineData(typeof(Day07), "336131", "92676646")]
    [InlineData(typeof(Day08), "521", "1016804")]
    [InlineData(typeof(Day09), "537", "1142757")]
    public async Task Test(Type type, string solution1, string solution2)
    {
        if (Activator.CreateInstance(type) is BaseDay instance)
        {
            var answer1 = await instance.Solve_1();
            var answer2 = await instance.Solve_2();

            if (!string.IsNullOrWhiteSpace(solution1))
                answer1.ShouldBe(solution1);
            if (!string.IsNullOrWhiteSpace(solution2))
                answer2.ShouldBe(solution2);
        }
        else
        {
            throw new XunitException();
        }
    }
}