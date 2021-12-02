namespace Aoc2021;

public sealed class Day02 : BaseDay
{
    private readonly string[] _instructions;

    public Day02()
    {
        _instructions = Utils.GetLines(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var horizontal = 0;
        var vertical = 0;

        foreach (var instruction in _instructions)
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

        return new ValueTask<string>((horizontal * vertical).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var horizontal = 0;
        var vertical = 0;
        var aim = 0;

        foreach (var instruction in _instructions)
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

        return new ValueTask<string>((horizontal * vertical).ToString());
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

public enum Direction
{
    Forward,
    Down,
    Up
}