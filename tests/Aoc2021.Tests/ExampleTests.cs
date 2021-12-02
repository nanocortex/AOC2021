using System;
using System.Threading.Tasks;
using Aoc2021.Inputs;
using AoCHelper;
using Shouldly;
using Xunit;
using Xunit.Sdk;

namespace Aoc2021.Tests;

public class ExampleTests
{
    [Theory]
    [InlineData(typeof(Day01), "1342", "1378")]
    [InlineData(typeof(Day02), "2102357", "2101031224")]
    public async Task Test(Type type, string solution1, string solution2)
    {
        if (Activator.CreateInstance(type) is BaseDay instance)
        {
            var answer1 = await instance.Solve_1();
            var answer2 = await instance.Solve_2();

            answer1.ShouldBe(solution1);
            answer2.ShouldBe(solution2);
        }
        else
        {
            throw new XunitException();
        }
    }
}