namespace Aoc2021;

public sealed class Day01 : BaseDay
{
    private readonly int[] _input;

    public Day01()
    {
        _input = Utils.GetIntegers(InputFilePath);
    }

    public override ValueTask<string> Solve_1()
    {
        var increments = 0L;
        for (var i = 1; i < _input.Length; i++)
        {
            if (_input[i] > _input[i - 1])
                increments++;
        }

        return new ValueTask<string>(increments.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var increments = 0L;
        var previousSum = _input[0] + _input[1] + _input[2];
        for (var i = 0; i < _input.Length; i++)
        {
            if (i + 3 > _input.Length)
                break;

            var sum = _input[i] + _input[i + 1] + _input[i + 2];

            if (sum > previousSum)
                increments++;

            previousSum = sum;
        }

        return new ValueTask<string>(increments.ToString());
    }
}