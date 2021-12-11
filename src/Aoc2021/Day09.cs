namespace Aoc2021;

public sealed class Day09 : BaseDay
{
    private readonly Map _map;

    public Day09()
    {
        _map = Map.Parse(File.ReadAllLines(InputFilePath).ToList());
    }

    public override ValueTask<string> Solve_1()
    {
        return new ValueTask<string>(_map.SumLowPoints().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        return new ValueTask<string>(_map.MultiplyLargestBasins().ToString());
    }

    private class Map
    {
        private readonly List<List<int>> _map;
        private const int Visited = -1;

        private Map(List<List<int>> map)
        {
            _map = map;
        }

        public static Map Parse(IEnumerable<string> lines)
        {
            var map = lines.Select(line => line.Select(x => x - '0').ToList()).ToList();
            return new Map(map);
        }

        public int SumLowPoints() => GetLowPoints().Sum(x => x.Height + 1);

        public int MultiplyLargestBasins()
        {
            var lowPoints = GetLowPoints().ToList();
            var basinSizes = lowPoints.Select(x => VisitFrom(x.Row, x.Col)).ToList();
            var top3Basins = basinSizes.OrderByDescending(x => x).Take(3);
            return top3Basins.Aggregate(1, (x, y) => x * y);
        }

        private int VisitFrom(int row, int col)
        {
            if (row < 0 || row >= _map.Count || col < 0 || col >= _map[0].Count || _map[row][col] == 9 || _map[row][col] == -1)
                return 0;

            var sum = 1;
            _map[row][col] = Visited;

            sum += VisitFrom(row, col - 1);
            sum += VisitFrom(row, col + 1);
            sum += VisitFrom(row - 1, col);
            sum += VisitFrom(row + 1, col);

            return sum;
        }

        private bool IsLowPoint(int value, int row, int col)
        {
            if (row > 0 && _map[row - 1][col] <= value)
                return false;

            if (row < _map.Count - 1 && _map[row + 1][col] <= value)
                return false;

            if (col > 0 && _map[row][col - 1] <= value)
                return false;

            if (col < _map[0].Count - 1 && _map[row][col + 1] <= value)
                return false;

            return true;
        }

        private IEnumerable<Location> GetLowPoints()
        {
            for (var row = 0; row < _map.Count; row++)
            {
                for (var col = 0; col < _map[row].Count; col++)
                {
                    if (IsLowPoint(_map[row][col], row, col))
                        yield return new Location(row, col, _map[row][col]);
                }
            }
        }
    }

    private record Location(int Row, int Col, int Height);
}