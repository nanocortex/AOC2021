namespace Aoc2021.Puzzle2;

public class AnswerCalculator
{
    public long Calculate1(IEnumerable<string> instructions)
    {
        var horizontal = 0;
        var vertical = 0;

        foreach (var instruction in instructions)
        {
            var direction = GetDirection(instruction.Split(" ")[0]);
            var step = int.Parse(instruction.Split(" ")[1]);

            switch (direction)
            {
                case Direction.Up:
                    vertical -= step;
                    break;
                case Direction.Down:
                    vertical += step;
                    break;
                case Direction.Forward:
                    horizontal += step;
                    break;
            }
        }

        return horizontal * vertical;
    }

    public long Calculate2(IEnumerable<string> instructions)
    {
        var horizontal = 0;
        var vertical = 0;
        var aim = 0;

        foreach (var instruction in instructions)
        {
            var direction = GetDirection(instruction.Split(" ")[0]);
            var step = int.Parse(instruction.Split(" ")[1]);

            switch (direction)
            {
                case Direction.Up:
                    aim -= step;
                    break;
                case Direction.Down:
                    aim += step;
                    break;
                case Direction.Forward:
                    horizontal += step;
                    vertical += aim * step;
                    break;
            }
        }

        return horizontal * vertical;
    }

    private Direction GetDirection(string directionText)
    {
        return directionText switch
        {
            "forward" => Direction.Forward,
            "up" => Direction.Up,
            "down" => Direction.Down,
            _ => throw new ArgumentException("", nameof(directionText))
        };
    }
}