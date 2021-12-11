namespace Aoc2021;

public class Day11 : BaseDay
{
    private List<List<int>> _grid;
    private int _flashes;

    public Day11()
    {
        _grid = ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        SimulateSteps(100);
        return new ValueTask<string>(_flashes.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        _grid = ParseInput();
        return new ValueTask<string>(GetStepUntilSynchronized().ToString());
    }

    private List<List<int>> ParseInput()
    {
        var lines = File.ReadAllLines(InputFilePath);
        return lines.Select(line => line.Select(y => y - '0').ToList()).ToList();
    }

    private void SimulateSteps(int steps)
    {
        for (var i = 0; i < steps; i++)
        {
            SimulateStep();
        }
    }

    private void SimulateStep()
    {
        for (var row = 0; row < _grid.Count; row++)
        {
            for (var col = 0; col < _grid[row].Count; col++)
            {
                TryFlash(row, col, 0);
            }
        }

        SetFlashedToZero();
    }

    private void TryFlash(int row, int col, int increment = 1)
    {
        if (row < 0 || row >= _grid.Count || col < 0 || col >= _grid[0].Count)
            return;

        _grid[row][col]++;

        if (_grid[row][col] != 10) return;

        _flashes++;
        _grid[row][col]++;
        TryFlash(row - 1, col - 1);
        TryFlash(row - 1, col);
        TryFlash(row - 1, col + 1);
        TryFlash(row, col - 1);
        TryFlash(row, col + 1);
        TryFlash(row + 1, col - 1);
        TryFlash(row + 1, col);
        TryFlash(row + 1, col + 1);
    }


    private void SetFlashedToZero()
    {
        foreach (var t in _grid)
        {
            for (var col = 0; col < t.Count; col++)
            {
                if (t[col] > 9)
                    t[col] = 0;
            }
        }
    }


    private int GetStepUntilSynchronized()
    {
        var step = 0;
        while (true)
        {
            SimulateStep();
            step++;
            if (!AllFlashed()) continue;
            return step;
        }
    }

    private bool AllFlashed() => _grid.All(row => row.All(col => col == 0));
}