using Aoc2021.Puzzle1;
using Aoc2021.Shared;

var numbers = Utils.GetInputIntegers();
var answerCalculator = new AnswerCalculator();

Console.WriteLine($"Solution 1: {answerCalculator.Calculate1(numbers)}");
Console.WriteLine($"Solution 2: {answerCalculator.Calculate2(numbers)}");