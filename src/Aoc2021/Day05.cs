namespace Aoc2021;

public class Day05 : BaseDay
{
    private readonly List<Line> _lines = new();

    public Day05()
    {
        ParseInput();
    }

    public override ValueTask<string> Solve_1()
    {
        var lines = _lines.Where(x => x.HorizontalOrVertical).ToList();
        var map = new Map(lines);
        return new ValueTask<string>(map.SumOverlappingPoints().ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var map = new Map(_lines);
        return new ValueTask<string>(map.SumOverlappingPoints().ToString());
    }

    private void ParseInput()
    {
        var lines = File.ReadAllLines(InputFilePath);
        foreach (var line in lines)
        {
            _lines.Add(new Line(line));
        }
    }


    private class Line
    {
        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get; }

        public Line(string input)
        {
            var split = input.Split("->", StringSplitOptions.RemoveEmptyEntries);
            X1 = int.Parse(split[0].Trim().Split(",")[0]);
            Y1 = int.Parse(split[0].Trim().Split(",")[1]);
            X2 = int.Parse(split[1].Trim().Split(",")[0]);
            Y2 = int.Parse(split[1].Trim().Split(",")[1]);
        }

        public bool HorizontalOrVertical => X1 == X2 || Y1 == Y2;
    }

    private class Map
    {
        private readonly List<List<int>> _points;

        public Map(List<Line> lines)
        {
            var width = Math.Max(lines.Max(x => x.X1), lines.Max(x => x.X2)) + 1;
            var height = Math.Max(lines.Max(x => x.Y1), lines.Max(x => x.Y2)) + 1;
            _points = new List<List<int>>();
            for (var i = 0; i < height; i++)
            {
                var row = new List<int>();
                for (var j = 0; j < width; j++)
                {
                    row.Add(0);
                }

                _points.Add(row);
            }

            foreach (var line in lines)
            {
                ApplyLine(line);
            }
        }

        private void ApplyLine(Line line)
        {
            int startY, endY, startX, endX;

            if (line.Y1 >= line.Y2)
            {
                startY = line.Y2;
                endY = line.Y1;
                startX = line.X2;
                endX = line.X1;
            }
            else
            {
                startY = line.Y1;
                endY = line.Y2;
                startX = line.X1;
                endX = line.X2;
            }

            var xSlope = startX == endX ? 0 : startX > endX ? -1 : 1;
            for (int y = startY, x = startX; y <= endY && (xSlope >= 0 ? x <= endX : x >= endX); y += y == endY && xSlope != 0 ? 0 : 1, x += xSlope)
            {
                _points[y][x]++;
            }
        }

        public int SumOverlappingPoints() => _points.SelectMany(x => x.Select(y => y)).Count(x => x > 1);
    }
}