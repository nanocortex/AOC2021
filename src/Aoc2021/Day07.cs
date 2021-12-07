namespace Aoc2021;

public sealed class Day07 : BaseDay
{
    private readonly List<int> _positions;

    public Day07()
    {
        _positions = File.ReadAllText(InputFilePath)
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var tries = _positions.Distinct();
        var minFuel = tries.Select(GetFuelForNumber).Prepend(int.MaxValue).Min();
        return new ValueTask<string>(minFuel.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var minFuel = int.MaxValue;

        for (var i = 0; i < _positions.Max(); i++)
        {
            var fuel = GetFuelForNumberExpensive(i);
            if (fuel < minFuel)
                minFuel = fuel;
        }

        return new ValueTask<string>(minFuel.ToString());
    }

    private int GetFuelForNumber(int number)
    {
        return _positions.Sum(position => Math.Abs(position - number));
    }

    private int GetFuelForNumberExpensive(int number)
    {
        return _positions.Sum(position => GetSum(position, number));
    }

    private int GetSum(int position, int number)
    {
        var diff = Math.Abs(position - number);
        double n = diff;
        return (int)(n * ((1.0 + n) / 2.0));
    }
}