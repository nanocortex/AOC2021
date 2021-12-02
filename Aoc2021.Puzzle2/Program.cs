using Aoc2021.Puzzle2;
using Aoc2021.Shared;

var lines = Utils.GetInputStrings();
var answerCalculator = new AnswerCalculator();

Console.WriteLine($"Solution 1: {answerCalculator.Calculate1(lines)}");
Console.WriteLine($"Solution 2: {answerCalculator.Calculate2(lines)}");