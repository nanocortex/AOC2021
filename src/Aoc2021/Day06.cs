namespace Aoc2021;

public sealed class Day06 : BaseDay
{
    private readonly List<Fish> _fishes;

    public Day06()
    {
        _fishes = ParseInput(File.ReadAllText(InputFilePath));
    }

    public override ValueTask<string> Solve_1()
    {
        var count = new Simulator().SimulateDays(_fishes, 80);
        return new ValueTask<string>(count.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var count = new Simulator().SimulateDays(_fishes, 256);
        return new ValueTask<string>(count.ToString());
    }

    private List<Fish> ParseInput(string input)
    {
        return input
            .Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(x => new Fish(int.Parse(x))).ToList();
    }

    private class Simulator
    {
        private readonly Dictionary<int, long> _values = new();

        private void SimulateDay()
        {
            var zeroes = _values[0];
            _values[0] = _values[1];
            _values[1] = _values[2];
            _values[2] = _values[3];
            _values[3] = _values[4];
            _values[4] = _values[5];
            _values[5] = _values[6];
            _values[6] = _values[7] + zeroes;
            _values[7] = _values[8];
            _values[8] = zeroes;
        }

        public long SimulateDays(List<Fish> fishes, int days)
        {
            for (var i = 0; i <= 8; i++)
            {
                _values[i] = fishes.Count(x => x.Cycle == i);
            }

            for (var i = 0; i < days; i++)
            {
                SimulateDay();
            }

            return _values.Values.Sum();
        }
    }

    private record Fish(int Cycle);
}