namespace Aoc2021;

public sealed class Day08 : BaseDay
{
    private readonly List<Entry> _entries;

    public Day08()
    {
        _entries = File.ReadAllLines(InputFilePath).Select(Entry.Parse).ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var sum = _entries.Sum(entry => entry.CountUniqueOutputs());
        return new ValueTask<string>(sum.ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var sum = _entries.Sum(entry => entry.DecodeAndSumOutputValues());
        return new ValueTask<string>(sum.ToString());
    }

    public class Entry
    {
        private readonly Dictionary<int, int> _uniqueMappings = new()
        {
            { 2, 1 },
            { 4, 4 },
            { 3, 7 },
            { 7, 8 },
        };

        private readonly Dictionary<int, string> _discovered = new();

        private List<string> Patterns { get; }
        private List<string> Outputs { get; }

        private Entry(List<string> patterns, List<string> outputs)
        {
            Patterns = patterns;
            Outputs = outputs;
        }

        public static Entry Parse(string line)
        {
            var tokens = line.Split("|", StringSplitOptions.RemoveEmptyEntries);
            var patterns = tokens[0].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            var outputs = tokens[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            return new Entry(patterns, outputs);
        }

        public int CountUniqueOutputs() => Outputs.Count(IsUniquePattern);

        public long DecodeAndSumOutputValues()
        {
            var allPatterns = Patterns.OrderBy(x => x.Length).Select(x => new string(x.OrderBy(y => y).ToArray())).ToList();
            var uniquePatterns = allPatterns.Where(IsUniquePattern);

            foreach (var pattern in uniquePatterns)
            {
                _discovered[_uniqueMappings[pattern.Length]] = pattern;
            }

            DiscoverNonUniquePatterns(allPatterns);

            var sum = string.Empty;
            foreach (var output in Outputs)
            {
                var o = new string(output.OrderBy(x => x).ToArray());
                var correct = _discovered.First(x => x.Value == o).Key;
                sum += correct;
            }

            return int.Parse(sum);
        }

        private void DiscoverNonUniquePatterns(List<string> allPatterns)
        {
            var fivePatterns = allPatterns.Where(x => x.Length == 5).ToList();

            _discovered[2] = fivePatterns.First(x => GetDiffs(x, _discovered[4]) == 5);
            _discovered[5] = fivePatterns.First(x => x != _discovered[2] && GetDiffs(x, _discovered[7]) == 4);
            _discovered[3] = fivePatterns.First(x => x != _discovered[2] && GetDiffs(x, _discovered[7]) == 2);

            var sixPatterns = allPatterns.Where(x => x.Length == 6).ToList();

            _discovered[6] = sixPatterns.First(x => GetDiffs(x, _discovered[7]) == 5);
            _discovered[0] = sixPatterns.First(x => x != _discovered[6] && GetDiffs(x, _discovered[4]) == 4);
            _discovered[9] = sixPatterns.First(x => x != _discovered[6] && GetDiffs(x, _discovered[4]) == 2);
        }


        private bool IsUniquePattern(string pattern) => pattern.Length is 2 or 3 or 4 or 7;

        private int GetDiffs(string a, string b) => a.Count(c => !b.Contains(c)) + b.Count(c => !a.Contains(c));
    }
}